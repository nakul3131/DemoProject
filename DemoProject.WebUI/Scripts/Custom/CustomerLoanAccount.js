'use strict'
$(document).ready(function () {
    const DISABLE = 'D';
    const NOT_REQUIRED = 'N';
    const PURCHASE_NEW_VEHICLE = 'NEW';
    const SALARIED = 'SLRD';
    const SELF_EMPLOYED_BUSINESS = 'SEMB';
    const SELF_EMPLOYED_PROFESSION = 'SEMP';
    const LOAN_AGAINTS_PROPERTY = 'LAP';
    const HOME_LOAN = 'HML';
    const GOLD_LOAN = 'GDL';
    const LOAN_AGAINST_DEPOSIT = 'LAD';
    const EMPLOYEE_LOAN = 'EPL'
    const SALARY_LOAN = 'SRL'
    const VEHICLE_LOAN = 'VHL';
    const PERSONAL_LOAN = 'PRL';
    const SHORT_TERM_BUSINESS_LOAN = 'SBL';
    const EDUCATION_LOAN = 'EDU';
    const CASH_CREDIT_LOAN = 'CCL';
    const CONSUMER_DURABLE_LOAN = 'CDL';
    const GUARANTOR_LOAN = 'GRL';
    const OTHER_INCOME = 'Other Income';
    const OTHER_INCOME_TEXT = 'Other Income ---> इतर उत्पन्न';

    const ADDRESS_TYPE_DROPDOWN_LIST = $('#address-type-id').html();
    const TWO_WHEELER = '2WHEEL';
    const THREE_WHEELER = '3WHEEL';

    // DropdownList Variables
    let personDropdownListData = '';
    let personDropdownListDataForJointAccount = '';
    let personDropdownListDataForNominee = '';
    let personDropdownListDataForFamily = '';
    let personDropdownListDataForGuardian = '';
    let guarantorDropdownListData = '';

    let sysNameOfLoanType = '';
    let documentDropdownList = '';
    let editedDepositAccountId = '';
    let depositAccountDropdownItems = '';

    // Array
    let finalDropdownListArray = [];

    // Count
    let dropDownListItemCount = 0;

    // Global 
    // Common Variables
    let result = true;
    let isVerifyView = false;
    let isAmendView = false;
    let minimum = 0;
    let maximum = 0;
    let minimumLength = 0;
    let maximumLength = 0;
    let occupationId;
    let previousSelectedOccupationId = '';
    let maximumTenureIdDays = 0;
    let minMaxResult = true;
    let requiredDocumentObj;
    let selectedDocumentObject;
    let objSchemeDocumentViewModel;
    let requiredDocumentArray = new Array();
    let listItemCount = 0;
    let dropdownListItems = '';
    let dataTableRecordCount = 0;
    let isUpdate = false;
    let prevPersonId = '';
    let prevBusinessOfficeId = '0';
    let prevSchemeId = '0';
    let selectedPersonId = '';
    let selectedjointPersonId = '';
    let selectedjointPersonText = '';
    let contactType = '';
    let schemeId;
    let isMobile = false;
    let isEmail = false;
    let time = 0;
    let today;
    let meridian;
    let hours = 0;
    let minutes = 0;
    let birthDate = '';
    let rowNum = 0;
    let minimumJointAccountHolder;
    let maximumJointAccountHolder;
    let minimumNumberOfGuarantor;
    let maximumNumberOfGuarantor;
    let minimumNominee = 0;
    let maximumNominee = 0;
    let guarantorId = '';
    let guarantorName = '';
    let editedGuarantorId = '';
    let minimumBusinessExperience = 0;
    let minimumTurnOverAmount = 0;
    let capturePreviousProfitMakingYears = 0;

    //Added By dhanshri wagh
    let vehicleCompanyEditedId = '';
    let educationalCourseEditedId = '';
    let instituteEditedId = '';
    let schemePrmkey;
    let lastVehicleMakeId;
    let vehicleAge = '';
    let vehicleTypePrmKey = '';
    let indexOfSchemeVehicleParameter = '';
    let sharesAmount = 0;

    let objSchemeLoanAccountParameter;
    let objSchemeNewVehicleParameter;
    let objSchemePreOwnedVehicleParameter;

    let isApplicableAllUniversities;
    let isApplicableAllCourse;

    // @@@@@@@@@@ Data Table Related Varible Declaration
    let tag = '';
    let id = '';
    let myModal;
    let selectedRowIndex;
    let row;
    let rowData;
    let checked;
    let columnValues;
    let columnValues1;

    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let arr = new Array();
    let photoTypeId;
    let photoTypeText;
    let photoCaption;

    // Nominee Detail
    let customerId = '';
    let nominationNumber = 0;
    let nominationDate = '';
    let sequenceNumber = 0;
    let nameOfNominee = '';
    let transnameOfNominee = '';
    let nomineePersonInformationNumber = 0;
    let nomineePersonInformationNumberText = '';
    let fullAddressDetails = '';
    let transFullAddress = '';
    let contactDetails;
    let transContactDetails;
    let relationId = '';
    let relationIdText = '';
    let holdingPercentage = 0;
    let proportionateAmountForEachNominee = 0;
    let customerDropdownForNominne = '';
    let activationDate = '';
    let guardianBirthDate = '';
    let expiryDate = '';
    let closeDate = '';
    let note = '';
    let transNote = '';
    let reasonForModification = '';
    let guardianFullName = '';
    let transGuardianFullName = '';
    let guardianNomineePersonInformationNumber = 0;
    let guardianNomineePersonInformationNumberText = '';
    let guardianTypeId = '';
    let guardianTypeIdText = '';
    let guardianNomineeBirthDate = '';
    let guardianNomineeFullAddress = '';
    let transGuardianNomineeFullAddress = '';
    let guardianContactDetails;
    let transGuardianContactDetails;
    let ageProofSubmissionStatusOfTheMinor;
    let ageProofSubmissionStatusOfTheMinorText;
    let appointedDateOfContact;
    let appointedTimeOfContact;
    let guardianNote;
    let transGuardianNote;
    let guardianReasonForModification;
    let filteredDataForNomineeNumber;
    let jointCustomerAccountId = '';
    let customerIdText = '';
    let personName = '';
    let scheduletime = 0;
    let seqNo = 0;
    let options;

    //Customer Acquaitance Detail
    let relationAcquaitanceId = '';
    let relationAcquaitanceText = '';
    let sequenceNumberAcquaitance = 0;
    let minimumAcquaintance;
    let maximumAcquaintance;
    let personInformationNumber = 0;
    let personInformationNumberText = '';

    // Loan Against Deposit
    let depositLoanAccountId = '';
    let depositLoanAccountText = '';
    let mortgageAmountLoanDeposit;

    //Person Borrowing Detail
    let nameOfOrganization = '';
    let transNameOfOrganization = '';
    let branch = '';
    let transBranch;
    let referenceNumber = 0;
    let openingDate = '';
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
    let registrationDate = '';
    let registrationNumber = 0;
    let filingDate = '';
    let filingNumber = 0;
    let cNRNumber = 0;
    let personBorrowingDetailPrmKey = 0;

    // Contact
    let contactTypeText;
    let fieldValue = 0;
    let isVerified = true;
    let verificationCode = '';
    let contactDetailPrmKey = 0;
    let personContactDetailPrmKey = 0;
    let hasDivClass;
    let entryStatus;
    let isDuplicateContact = false;
    let isDuplicateSequenceNumber = false;
    let isDuplicateNomineeNumber = false;
    let customerAccountPrmKey = 0;

    // Address Detail
    let addressType = '';
    let addressTypeText = '';
    let flatDoorNo = 0;
    let transFlatDoorNo;
    let buildingName = '';
    let transBuildingName = '';
    let roadName = '';
    let transRoadName = '';
    let areaName = '';
    let transAreaName = '';
    let city = '';
    let cityText = '';
    let residenceType = '';
    let residenceTypeText = '';
    let residenceOwnership = '';
    let residenceOwnershipText = '';
    let personAddressPrmKey = 0;
    let editedAddressTypeId = '';

    // income Detail
    let incomeSource = '';
    let incomeSourceText = '';
    let otherDetails = '';
    let annualIncome = 0;
    let assessmentYear = 0;
    let taxAmount = 0;
    let personIncomeTaxDetailPrmKey = 0;

    //Court Case
    let courtCaseTypeId = '';
    let courtCaseTypeIdText = '';
    let cnrNumber = 0;
    let amountOfDecree = 0;
    let collateralAmount = 0;
    let courtCaseStageId = '';
    let courtCaseStageIdText = '';
    let personCourtCasePrmKey = 0;

    // Joint Account
    let personId = '';
    let personIdText = '';
    let jointAccountHolderId = '';
    let jointAccountHolderIdText = '';
    let jointAccountHolderActivationDate = '';
    let jointAccountHolderExpiryDate = '';
    let editedSequenceNumber = 0;
    let editedNomineeNumber = 0;
    let guaranteePercentage = 0;

    // NoticeSchedule
    let noticeTypeId = '';
    let noticeScheduleText = '';
    let communicationMediaId = '';
    let communicationMediaText = '';
    let scheduleId = '';
    let scheduleText = '';

    //Movable Asset
    let fileNameDocument = '';
    let localStoragePath = '';
    let vehicleMakeId = '';
    let vehicleModelId = '';
    let vehicleModelEditedId = '';
    let vehicleVariantEditedId = '';

    // AdditionalIncome
    let personAdditionalIncomeDetailPrmKey = 0;

    // GOLD Collateral Detail
    let goldLoanRate = 0;
    let minimumGoldPhoto;
    let maximumGoldPhoto;
    let jewelAssayerId = '';
    let jewelAssayerText = '';
    let goldOrnamentId = '';
    let goldOrnamentText = '';
    let metalPurity = '';
    let metalPurityText = '';
    let huid = '';
    let qty = '';
    let hasAnyDamage = '';
    let damageDescription = '';
    let damageWeight = '';
    let hasAnyWestage = '';
    let westageDescription = '';
    let westageWeight = '';
    let metalGrossWeight = '';
    let hasDiamond = '';
    let isDiamondDeductable = '';
    let numberOfDiamond = '';
    let diamondCarat = '';
    let clarityColour = '';
    let diamondWeight = '';
    let diamondPrice = '';
    let diamondValuation = '';
    let metalNetWeight = '';
    let custodyStatus = '';
    let custodyStatusText = '';
    let jewelAssayerRemark = '';
    let valuationAmount = '';
    let customerGoldLoanCollateralPhotoPrmKey = 0;

    //CutomerConsumnerCollatralDetail
    let consumerDurableItemId = '';;
    let consumerDurableItemIdText = '';;
    let consumerDurableItemBrandId = '';;
    let consumerDurableItemBrandIdText = '';;
    let itemOtherDetail;
    let manufactureYear;
    let serialNumber;
    let productAmount;
    let downPayment;
    let warrantyInMonth;
    let guaranteeInMonth;
    let downPaymentMargin;

    // Document
    let fileUploaderInput;
    let fileObj;
    let documentId;
    let documentText = '';
    let filePath = '';
    let fileUploader;
    let fileUploaderInputHtml = '';
    let imageTagHtml = '';
    let fileCaption;
    let fileUploaderId = '';
    let fileId = '';
    let editedDocumentId = '';
    let editedDocumentSequenceNumber = 0;
    let counter = 100;
    let customerAccountDocumentPrmKey = 0;
    let personIncomeTaxDetailDocumentPrmKey = 0;

    //Vehicle Loan Photo
    let minimumVehiclePhoto = 0;
    let maximumVehiclePhoto = 0;
    let allowFileFormatForDb;
    let maximumFileSizeForDb;
    let allowFileFormatForLocalStorage;
    let maximumFileSizeForLocalStorage;
    let enablePhotoUploadInLocalStorage = false;
    let updatedPlaceholder;
    let customerVehicleLoanPhotoPrmKey = 0;
    let isDuplicatePolicyNumber = true;

    //Fixed Deposit As Collateral
    let mortgageAmountDeposit = 0;
    let depositAccountId;
    let depositAccountText;
    let isLoanClosed = true;
    let isEmployee = false;
    let isDbRecord = false;
    let isChangedPhoto = false;

    let isDisplayAlert = false;
    let myExpensesValue = 0;

    //  ************** Create Data Table  **************
    let addressDataTable = CreateDataTable('person-address');
    let contactDataTable = CreateDataTable('contact');
    let jointAccountDataTable = CreateDataTable('joint-account');
    let nomineeDataTable = CreateDataTable('account-nominee');
    let noticeScheduleDataTable = CreateDataTable('notice-schedule');
    let guarantorDetailDataTable = CreateDataTable('guarantor-detail');
    let goldCollateralDetailDataTable = CreateDataTable('gold-collateral-detail');
    let vehicleLoanPhotoDataTable = CreateDataTable('vehicle-loan-photo');
    let goldLoanPhotoDataTable = CreateDataTable('gold-collateral-photo');
    let documentDataTable = CreateDataTable('document');
    let borrowingDetailDataTable = CreateDataTable('borrowing-detail');
    let courtCaseDataTable = CreateDataTable('court-case');
    let incomeDatatable = CreateDataTable('income-detail');
    let incomeTaxDataTable = CreateDataTable('income-tax');
    let depositCollateralDetailDataTable = CreateDataTable('deposit-collateral-detail');
    let consumerLoanDetailDataTable = CreateDataTable('consumer-loan-detail');
    let fixedCollateralDetailDataTable = CreateDataTable('fix-deposit-collateral');
    let acquaitanceDataTable = CreateDataTable('customer-acquaitance-detail');

    // Page Loading Default Values (Usually For Amend)
    SetPageLoadingDefaultValues();
    function AttachFileUploader() {
        debugger;
        let dataTransferObj = new DataTransfer();

        // Get Uploaded Files In File Object
        fileObj = fileUploader.files[0];

        // Add This File Into Data Transfer Object

        //Breakdown of slice Method
        //The slice method creates a new Blob object containing data from a portion of the original file (f). It is a method available on Blob and File objects in JavaScript.
        //    Parameters:
        //0 (start):
        //The starting byte offset of the slice. 0 means the slice starts from the beginning of the file.
        //f.size (end):
        //The ending byte offset of the slice. f.size indicates the end of the file, meaning the slice includes all the data from the start to the end of the file.
        //f.type (content type):
        //An optional parameter that specifies the MIME type of the slice. f.type takes the type of the original file (f). This ensures the newly created slice retains the same content type as the original file.

        dataTransferObj.items.add(new File([fileObj.slice(0, fileObj.size, fileObj.type)], fileObj.name))

        fileUploaderInput = $('#' + fileUploaderId).get(0);

        fileUploaderInput.files = dataTransferObj.files;
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Sms Send - Display SMS Delilety Status
    $('#send-code').click(function () {
        let _mobileNumber = $('#field-value').val();
        $('#send-code').addClass('d-none');

        $.get('/SMS/SendVerificationToken', { MobileNumber: _mobileNumber, async: false }, function (data) {
            if (data == 'success') {
                $('#t-body').removeClass('bg-danger');
                $('#t-body').addClass('bg-success');

                $('#t-icon').removeClass('fa-times');
                $('#t-icon').addClass('fa-check');

                $('#t-text').text('Sms Send Successfully');
            }
            else {
                $('#t-body').addClass('bg-danger');
                $('#t-body').removeClass('bg-success');

                $('#t-icon').addClass('fa-times');
                $('#t-icon').removeClass('fa-check');

                $('#t-text').text('Sms Sending Failed');
            }

            $('#resend').removeClass('d-none');

            // For Display Toaster Message
            $('.link').fadeOut('slow').delay(30000).fadeIn('slow');
            $('#myToast').toast('show').css({ 'z-index': '100', 'margin-top': '1%' });
        });
    });

    // Sms Resend - Display SMS Delilety Status
    $('#resend').click(function () {
        let _mobileNumber = $('#field-value').val()

        $.get('/SMS/SendVerificationToken', { MobileNumber: _mobileNumber, async: false }, function (data) {
            if (data == 'success') {
                $('#t-body').removeClass('bg-danger');
                $('#t-body').addClass('bg-success');

                $('#t-icon').removeClass('fa-times');
                $('#t-icon').addClass('fa-check');

                $('#t-text').text('Sms Send Successfully');
                $('#send-code').addClass('d-none');
                $('#resend').removeClass('d-none');
            }

            // For Display Toaster Message
            $('.link').fadeOut('slow').delay(30000).fadeIn('slow');
            $('#myToast').toast('show').css({ 'z-index': '100', 'margin-top': '1%' });
        });
    });

    // Enable All Services Of SMS 
    $('#enable-all-service').click(function () {
        if ($(this).is(':checked')) {
            $('#enable-credit-transaction-sms').prop('checked', true);
            $('#enable-debit-transaction-sms').prop('checked', true);
            $('#enable-insufficient-balance-sms').prop('checked', true);
        }
        else {
            $('#enable-credit-transaction-sms').prop('checked', false);
            $('#enable-debit-transaction-sms').prop('checked', false);
            $('#enable-insufficient-balance-sms').prop('checked', false);
        }
    });

    // If All Service Enabled Then Enable All Sms Services
    $('.enable-service').click(function () {
        if ($('#enable-credit-transaction-sms').is(':checked') && $('#enable-debit-transaction-sms').is(':checked') && $('#enable-insufficient-balance-sms').is(':checked'))
            $('#enable-all-service').prop('checked', true);
        else
            $('#enable-all-service').prop('checked', false);
    });

    // Enable All Services Of EMAIL 
    $('#enable-all-email-service').click(function () {
        debugger;
        if ($(this).is(':checked')) {
            $('#enable-credit-transaction-email').prop('checked', true);
            $('#enable-debit-transaction-email').prop('checked', true);
            $('#enable-insufficient-balance-email').prop('checked', true);
            $('#enable-statement-email').prop('checked', true);
        }
        else {
            $('#enable-credit-transaction-email').prop('checked', false);
            $('#enable-debit-transaction-email').prop('checked', false);
            $('#enable-insufficient-balance-email').prop('checked', false);
            $('#enable-statement-email').prop('checked', false);
        }
    });

    // If All Service Enabled Then Enable All Email Services
    $('.enable-email-service').click(function () {
        if ($('#enable-credit-transaction-email').is(':checked') && $('#enable-debit-transaction-email').is(':checked') && $('#enable-insufficient-balance-email').is(':checked') && $('#enable-statement-email').is(':checked'))
            $('#enable-all-email-service').prop('checked', true);
        else
            $('#enable-all-email-service').prop('checked', false);
    });

    //permit type
    $('#permit-type').focusout(function () {
        $('#permit-amount-per-month-error').addClass('d-none');
    });

    // Update max attribute based on Mortgage Amount
    $('#mortgage-amount').focusout(function () {
        $('#sanction-loan-amount').attr('max', $(this).val());

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
    $('#activation-open-date').focusout(function () {
        $('#activation-filing-date').val('');
        $('#close-date-borrowing').val('');
        EnableTakingAnyCourtActionClickEventFunction();
    });

    // Filling Date
    $('#activation-filing-date').click(function () {
        let openingDate = new Date($('#activation-open-date').val());

        openingDate.setDate(openingDate.getDate() + 1);

        $('#activation-filing-date').attr('min', GetInputDateFormat(openingDate));

        $('#expiry-filing-date').val('');
    });

    // Registration Date
    $('#expiry-filing-date').click(function () {
        debugger;
        let filingDate = new Date($('#activation-filing-date').val());

        $('#expiry-filing-date').attr('min', GetInputDateFormat(filingDate));

        let maxRegistrationDate = new Date(filingDate.setMonth(filingDate.getMonth() + 1));

        if (maxRegistrationDate > new Date()) {
            maxRegistrationDate = new Date();
        }

        $('#expiry-filing-date').attr('max', GetInputDateFormat(maxRegistrationDate));
    });

    // Function to enable/disable based on close-date-borrowing value
    function EnableTakingAnyCourtActionClickEventFunction() {
        let closeDate = $('#close-date-borrowing').val();
        let today = new Date();
        let openDate = $('#activation-open-date').val();
        today = GetInputDateFormat(today);

        if (closeDate !== '' || openDate === today) {
            $('#taking-any-court-action-block').addClass('d-none');
            $('#enable-taking-any-court-action').prop('checked', false)
            $('#enable-taking-any-court-action').prop('disabled', true)

        } else {
            $('#enable-taking-any-court-action').prop('disabled', false).removeClass('d-none');
        }
    }

    // Add event listener for click
    $('#close-date-borrowing').focusout(function () {
        EnableTakingAnyCourtActionClickEventFunction();
    });

    // Court Case Filing Date
    $('#activation-filing-dates').click(function () {
        $('#expiry-filing-dates').val('');
    });

    // Court case Registration Date
    $('#expiry-filing-dates').click(function () {
        let today = new Date();

        let filingDate = new Date($('#activation-filing-dates').val());

        $('#expiry-filing-dates').attr('min', GetInputDateFormat(filingDate));

        let maxRegistrationDate = new Date(filingDate);
        maxRegistrationDate.setMonth(maxRegistrationDate.getMonth() + 1);

        if (maxRegistrationDate > today) {
            maxRegistrationDate = today;
        }

        $('#expiry-filing-dates').attr('max', GetInputDateFormat(maxRegistrationDate));
    });

    //carpet area
    $('#construction-area').focusout(function () {
        $('#carpet-area').attr('max', ($(this).val() - 1));
    });

    $('#financial-organization-type').focusout(function () {
        $('#insurance-company-id').val('');
        $('#name-of-financial-organization').val('');
        $('#trans-name-of-financial-organization').val('');
        $('#name-of-branch').val('');
        $('#trans-name-of-branch').val('');
        $('#address-details').val('');
        $('#trans-address-details').val('');
        $('#contact-details').val('');
        $('#trans-contact-details').val('');
        $('#opening-dates').val('');
        $('#maturity-date').val('');
        $('#financial-asset-type').val('');
        $('#financial-asset-description').val('');
        $('#trans-financial-asset-description').val('');
        $('#references-number').val('');
        $('#trans-references-number').val('');
        $('#invested-amount').val('');
        $('#monthly-interest-income-amount').val('');
        $('#current-market-values').val('');
        $('#has-any-mortgage-financial').prop('checked', false);
        $('#finance-file-uploader').val('');
        $('#finance-file-uploader-image-preview').attr('src', '');
        $('#file-caption-finance').val('');
        $('#note-financial-asset').val('');
        $('#trans-note-financial-asset').val('');
        $('.modal-input-error').addClass('d-none');
    });

    $('#income-source-id').focusout(function () {
        $('#annual-incomes').val('');
        $('#other-details').val('');
        $('#note-income-detail').val('');
        $('.modal-input-error').addClass('d-none');
    });

    //Business Office Id
    $('#business-office-id').focusout(function () {
        if (isVerifyView === false) {
            let businessOfficeId = $('#business-office-id option:selected').val();

            if (prevBusinessOfficeId != businessOfficeId) {
                if (prevBusinessOfficeId != 0)
                    $('#business-office-id-error').addClass('d-none')

                prevBusinessOfficeId = businessOfficeId;

                // Clear Dependent Data
                $('#loan-type-id').prop('selectedIndex', 0);
                $('#scheme-id').prop("selectedIndex", 0);
                $('#loan-type-id').prop("selectedIndex", 0);
                $('#person-id').val('');

                addressDataTable.clear().draw();
                contactDataTable.clear().draw();
                jointAccountDataTable.clear().draw();
                nomineeDataTable.clear().draw();
            }
            else {
                $('#business-office-id-error').removeClass('d-none')
                prevBusinessOfficeId = $('#business-office-id option:selected').val();
            }
        }
    });

    // On Changing Business Office, All Dependent Setting (Loan Type , Genreral Ledger, Scheme) Required To Be Clear.
    $('#loan-type-id').focusout(function () {
        debugger;
        if (isVerifyView === false) {
            SetLoanTypeSetting();

            // Clear Document Data Table Record
            documentDataTable.clear().draw();

            // Mark Out Select All Check Box Of All Datatables.
            $('input[name="select-all"]').prop('checked', false);

            // Clear Accordion Title Error Messages
            $('.accordion-title-error').addClass('d-none');
            $('.optional-section').addClass('d-none');
            $('.optional-input').addClass('d-none');

            // Make False All Toggle Switch
            $('.switch-input').prop('checked', false);
            $('.sms-service-input').val('');
            $('.email-service-input').val('');

            SetGeneralLedgerDropdownList();
        }
    });

    // On Changing General Ledger, All Dependent Setting (Scheme) Required To Be Clear.
    $('#general-ledger-id').focusout(function () {
        debugger
        if (isVerifyView === false) {
            SetSchemeDropdownList();
        }
    });

    // Scheme Id
    $('#scheme-id').focusout(function () {
        if (isVerifyView === false) {
            debugger;
            schemeId = $('#scheme-id option:selected').val();

            if (schemeId != '00000000-0000-0000-0000-000000000000') {
                $('#scheme-id-error').addClass('d-none');

                if (prevSchemeId != schemeId) {
                    // Clear Dependent Data
                    noticeScheduleDataTable.clear().draw();

                    $('#person-id').val('');
                    $('#day, #month, #year').val('');
                    $('#maturity-date').val('');

                    // Input Visiblity Base On Selected Scheme
                    SetSchemeSetting();

                    prevSchemeId = schemeId;
                }
                else
                    prevSchemeId = $("#scheme-id option:selected").val();
            }
            else
                $('#scheme-id-error').removeClass('d-none');
        }
    });

    $('#person-information-numbers').autocomplete(
        {
            minLength: 0,
            appendTo: '#person-information',
            scroll: true,
            autoFocus: true,
            source: function (request, response) {
                debugger;
                let responseDropdownListArray = [];
                dropDownListItemCount = finalDropdownListArray.length;

                if (parseInt(dropDownListItemCount) > 0) {
                    responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                        if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1)
                            return { label: key.Text, valueId: key.Value }
                        else
                            return null;
                    });

                    // The slice() method returns selected elements in an array, as a new array. 
                    // The slice() method selects from a given start, up to a (not inclusive) given end.
                    response(responseDropdownListArray.slice(0, 10));
                }
                else
                    response([{ label: 'No Records Found', value: -1 }]);
            },
            select: function (event, ui) {
                event.preventDefault();
                $('#person-information-numbers').val(ui.item.label);
                personInformationNumber = ui.item.valueId;
                personInformationNumberText = ui.item.label;

            },
        }).focus(function () {
            debugger;
            personInformationNumber = '';
            personInformationNumberText = '';
            $('#person-information-numbers').val('');

            let dataTablePersonIdArray = [];

            // Assign Array Without Reference  *** Use Slice Method
            finalDropdownListArray = personDropdownListDataForFamily.slice();

            dropDownListItemCount = finalDropdownListArray.length;

            // Get Added Person Id For Remove From List
            $('#tbl-customer-acquaitance-detail > tbody > tr').each(function () {
                let currentRow = $(this).closest("tr");
                let columnValues = (acquaitanceDataTable.row(currentRow).data());

                if (typeof columnValues !== 'undefined' && columnValues != null)
                    dataTablePersonIdArray.push({ 'Value': columnValues[1], 'Text': columnValues[2] })
            });

            if (parseInt(dropDownListItemCount) > 0 && parseInt(dataTablePersonIdArray.length) > 0) {
                while (dropDownListItemCount--) {
                    // Remove Added Joint Account Person Id From List
                    for (let personId of dataTablePersonIdArray) {
                        if (finalDropdownListArray[dropDownListItemCount].Value === personId.Value)
                            // splice - remove item from array at a choosen index
                            finalDropdownListArray.splice(dropDownListItemCount, 1);
                    }
                }
            }


            $(this).autocomplete('search');
        });

    // Person Autocomplete FocusOut Event
    $('#person-id').focusout(function () {
        if (isVerifyView === false) {
            debugger;
            $(this).val($(this).val().trim());
            SetPersonData();
            DepositAccountDropdownList();
        }
    });

    function DepositAccountDropdownList() {
        debugger;
        let selectedPersonId = $('#person-id1').val();

        $.get('/DynamicDropdownList/GetDepositCustomerDropdownListByScheme', { _personId: selectedPersonId, _schemeId: schemeId, async: false }, function (data) {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Customer Name --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#deposit-account-id').html(dropdownListItems);

            depositAccountDropdownItems = $('#deposit-account-id').html();

            listItemCount = $('#deposit-account-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#deposit-account-id').prop('selectedIndex', 1);
            }
        });

    }
    function EducationalCourseDropdownList() {
        debugger;
        $.get('/DynamicDropdownList/GetEducationalCourseDropdownListBySchemePrmKey', { _schemePrmKey: schemePrmkey, _isApplicableAllUniversities: isApplicableAllUniversities, async: false }, function (data) {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Educational Course Name --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#educational-course-id').html(dropdownListItems);

            listItemCount = $('#educational-course-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#educational-course-id').prop('selectedIndex', 1);
            }
            else {
                if (educationalCourseEditedId !== '') {
                    $('#educational-course-id').val(educationalCourseEditedId);
                }
            }
        });

    }
    function InstituteDropdownList() {
        debugger;
        $.get('/DynamicDropdownList/GetInstituteDropdownListBySchemePrmKey', { _schemePrmKey: schemePrmkey, _isApplicableAllCourse: isApplicableAllCourse, async: false }, function (data) {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Institute Name --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#institude-id').html(dropdownListItems);

            listItemCount = $('#institude-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#institude-id').prop('selectedIndex', 1);
            }
            else {
                if (instituteEditedId !== '') {
                    $('#institude-id').val(instituteEditedId);
                }
            }
        });

    }

    // Sanction-Amount
    $('#sanction-amount').focusout(function () {
        debugger;
        let sanctionAmount = parseFloat($(this).val());
        let sharesRatioWithLoan = objSchemeLoanAccountParameter.SharesRatioWithLoan;

        if (parseFloat(sharesRatioWithLoan) > 0) {
            //Calculate Deducted Shares Amount By  SharesRatioWithLoan In Parameter
            sharesAmount = Math.round((sharesRatioWithLoan / 100) * sanctionAmount);

            $('#deducted-shares-amount').val(sharesAmount);
        }
        else {
            $('#shares-amount-input').addClass('d-none');
            $('#deducted-shares-amount').val(0);
        }

    });

    // Duducted Shares Amount
    $('#deducted-shares-amount').focusout(function () {
        debugger;
        let userName = loanCustomerAccountDetailViewModel.NameOfUser;
        let changedDeductedSharesAmount = parseFloat($(this).val());

        //If Deducted Shares Amount Is Changed By Authorized User Then Display Deduction Remark Else Hide It.
        if (sharesAmount === changedDeductedSharesAmount) {
            $('#deduction-remark-input').addClass('d-none');
            $('#deduction-remark').val('None');
        }
        else {
            $('#deduction-remark-input').removeClass('d-none');
            $('#deduction-remark').val('Changed Amount From ' + sharesAmount + ' To ' + changedDeductedSharesAmount + ' By ' + userName);
        }
    });

    // Joint Account Person Dropdown List FocusOut Event
    $('#person-id-joint-account-holder').focusout(function () {
        $(this).val($(this).val().trim());
    });

    function SetValidDocumentFileFormat() {
        // $.grep() - Nothing But Work As Filter; It Return Data Only Meet A Condition
        selectedDocumentObject = $.grep(objSchemeDocumentViewModel, function (element) { return element.DocumentId == $('#document-id option:selected').val() });
    }

    // Document Id Focusout
    $('#document-id').focusout(function () {
        SetValidDocumentFileFormat();
    });

    //cashcredit Input Validation
    $('.cash-credit-input').focusout(function () {
        IsValidCashCreditLoanAccordionInput();
    });

    // Clear Depended Inputs
    $('#account-opening-date').focusout(function () {
        if (isVerifyView === false) {
            $('#tenure').val('');
            $('#maturity-date').val('');
        }
    });

    // Hide Guardian Details If User Input Nominee Birtdate As Adults
    $('#nominee-birth-date').focusout(function () {

        $.get('/PersonChildAction/GetAgeFromBirthDate', { _birthDate: $('#nominee-birth-date').val(), async: false }, function (data) {
            if (data < 18)
                $('#guardian-card').removeClass('d-none');
            else
                $('#guardian-card').addClass('d-none');
        });
    });

    // Hide Nominee Person Information Number, If User Manually Input Nominee Name 
    $('#name-of-nominee').focusout(function () {
        let nameOfNominee = $('#name-of-nominee').val();

        if ((nameOfNominee != 'None') && (nameOfNominee.length > 3)) {
            $('#nominee-person-id').prop('selectedIndex', 0);
            $('#nominee-person-information-number-input').addClass('d-none');
        }
        else
            $('#nominee-person-information-number-input').removeClass('d-none');
    });

    // Hide Nominee Manually Inputs, If User Selects Adult Person
    $('#nominee-person-id').focusout(function () {
        debugger;
        if ($('#nominee-person-id').val() == 0 || typeof $('#nominee-person-id').val() == null)
            $('#nomineedetails').removeClass('d-none');
        else {
            $('#nomineedetails').addClass('d-none');

            //let personInformationNumbers = $(this).val();
            $.get('/PersonChildAction/GetPersonCurrentAge', { _personInformationNumber: nomineePersonInformationNumber, async: false }, function (data) {
                if (data <= 18) {
                    $('#guardian-card').removeClass('d-none');
                    $('#collapse-guardian').addClass('show');
                }
                else {
                    $('#guardian-card').addClass('d-none');
                }
            });
        }
    })

    // Hide Proportionate Amount If Holding Percentage Is Greater Than 0
    function ProportionateAmountHideSection() {
        debugger;
        let holdingPercentage = parseFloat($('#holding-percentage').val());

        if (!isNaN(holdingPercentage) || parseFloat(holdingPercentage) > 0)
            $('#proportionate-amount-for-each-nominee-input').addClass('d-none');
        else
            $('#proportionate-amount-for-each-nominee-input').removeClass('d-none');
    }

    // Holding Percentage Focus-Out (Hide-Show)
    $('#holding-percentage').focusout(function () {
        debugger;
        ProportionateAmountHideSection();
    })

    // Hide Holding Percentage If Proportionate Amount Is Greater Than 0
    function HoldingPercentagetHideSection() {
        debugger;
        let proportionateAmount = parseFloat($('#proportionate-amount-for-each-nominee').val());

        if (!isNaN(proportionateAmount) || parseFloat(proportionateAmount) > 0)
            $('#holding-percentage-input').addClass('d-none');
        else
            $('#holding-percentage-input').removeClass('d-none');
    }

    // Proportionate Amount FocusOut (Hide-Show) 
    $('#proportionate-amount-for-each-nominee').focusout(function () {
        debugger;
        HoldingPercentagetHideSection();
    })

    // Hide Guardian Details If User Select Adult Person Name As Nominee (** This List Contains Only Adult Person Name)
    $('#nominee-guardian-person-information-number').focusout(function () {
        let personInfoNumber = $('#nominee-guardian-person-information-number').val();

        if (personInfoNumber == '')
            $('.nominee-guardian-details').removeClass('d-none');
        else
            $('.nominee-guardian-details').addClass('d-none');
    });

    $('#guardian-full-name').focusout(function () {
        let nameOfGuardian = $('#guardian-full-name').val();

        if ((nameOfGuardian != 'None') && (nameOfGuardian.length > 3)) {
            $('#nominee-guardian-person-information-number').prop('selectedIndex', 0);
            $('#nominee-guardian-person-information-number-input').addClass('d-none');
        }
        else
            $('#nominee-guardian-person-information-number-input').removeClass('d-none');
    });

    // Contact Type Validation
    $('#field-value').focusout(function () {
        debugger;
        $(this).val($(this).val().trim());

        // If Contact Type Is Mobile
        if (isMobile) {
            $('#verification-code').val('');

            if ($('#field-value').val().length == 10) {
                $('#field-value-mobile-error').addClass('d-none');

                // Check Whether Enter Mobile Number Is Existed Or Not For Mobile Contact Type
                let filteredData = contactDataTable
                    .rows()
                    .indexes()
                    .filter(function (value) {
                        return contactDataTable.row(value).data()[3] == $('#field-value').val() && contactDataTable.row(value).data()[2].includes('Mobile');
                    });

                if (contactDataTable.rows(filteredData).count() > 0) {
                    isDuplicateContact = true;
                    $('#field-value-duplicate-error').removeClass('d-none');
                }
                else {
                    $('#field-value-duplicate-error').addClass('d-none');
                    isDuplicateContact = false;
                }

                if (!isDuplicateContact) {
                    $('#send-code').removeClass('d-none');
                    $('#resend').addClass('d-none');
                    $('#field-value-error').addClass('d-none');
                }
            }
            else {
                $('#send-code').addClass('d-none');
                $('#field-value-error').removeClass('d-none');
                $('#field-value-mobile-error').removeClass('d-none');
            }
        }
        else {
            isDuplicateContact = false;
            $('#field-value-error').addClass('d-none');
            $('#field-value-duplicate-error').addClass('d-none');
            $('#verification-code').val('0');
            $('#send-code').addClass('d-none');
        }
    });
    function SetOccupationDetails() {
        debugger;
        occupationId = $('#occupation-id').val();
        isEmployee = $('#enable-employee').is(':checked') ? true : false;
        $.get('/PersonChildAction/GetSysNameOfOccupation', { _occupationId: occupationId, async: false }, function (data) {
            debugger;
            if (data === SALARIED) {
                $('#is-employee-input').removeClass('d-none');
                $('#employer-name').siblings('label').text('Name Of Employer');
                $('#employer-name').attr('placeholder', 'Enter Name Of Employer');
                $('#employment-type-id').siblings('label').text('Employment Type');
                $('#employment-type-id option:first').text('--- Select Employment Type ---');
                $('#nature-of-employer-id').siblings('label').text('Nature Of Employer');
                $('#nature-of-employer-id option:first').text('--- Select Nature Of Employer ---');
                $('#employer-nature-other-details').siblings('label').text('Employer Nature Other Details');
                $('#employer-nature-other-details').attr('placeholder', 'Enter Employer Nature Other Details');
                HideEmployeeSection();
            }
            else if (data === SELF_EMPLOYED_BUSINESS) {
                $('#is-employee-input').removeClass('d-none');
                $('#employee-block').removeClass('d-none');
                $('#employer-name').siblings('label').text('Name Of Business');
                $('#employer-name').attr('placeholder', 'Enter Name Of Business');
                $('#employment-type-id').siblings('label').text('Business Type');
                $('#employment-type-id option:first').text('--- Select Business Type ---');
                $('#nature-of-employer-id').siblings('label').text('Nature Of Business');
                $('#nature-of-employer-id option:first').text('--- Select Nature Of Business ---');
                $('#employer-nature-other-details').siblings('label').text('Business Nature Other Details');
                $('#employer-nature-other-details').attr('placeholder', 'Enter Business Nature Other Details');
                HideEmployeeSection();
            }
            else if (data === SELF_EMPLOYED_PROFESSION) {
                $('#is-employee-input').removeClass('d-none');
                $('#employee-block').removeClass('d-none');
                $('#employer-name').siblings('label').text('Name Of Profession');
                $('#employer-name').attr('placeholder', 'Enter Name Of Profession');
                $('#employment-type-id').siblings('label').text('Profession Type');
                $('#employment-type-id option:first').text('--- Select Profession Type ---');
                $('#nature-of-employer-id').siblings('label').text('Nature Of Profession');
                $('#nature-of-employer-id option:first').text('--- Select Nature Of Profession ---');
                $('#employer-nature-other-details').siblings('label').text('Profession Nature Other Details');
                $('#employer-nature-other-details').attr('placeholder', 'Enter Profession Nature Other Details');
                HideEmployeeSection();
            }
            else {
                $('#employee-block').addClass('d-none');
                $('#is-employee-input').addClass('d-none');
            }
        });
    }
    function IsValidOccupation() {
        let nameOfEmployer = $('#employer-name').val();
        let transNameOfEmployer = $('#trans-employer-name').val();
        let employerAddressDetails = $('#employer-address-details').val();
        let transEmployerAddressDetails = $('#trans-employer-address-details').val();

        let employerContactDetails = $('#employer-contact-details').val();
        let transEmployerContactDetails = $('#trans-employer-contact-details').val();
        let annualIncome = parseFloat($('#annual-income').val());
        let epfNumber = $('#epf-number').val();
        let transEpfNumber = $('#trans-epf-number').val();

        let noteEmployement = $('#note-employment-detail').val();
        let transNoteEmployement = $('#trans-note-employment-detail').val();
        let employedSince = parseInt($('#employed-since').val());
        let employerNatureOtherDetails = $('#employer-nature-other-details').val();
        let transEmployerNatureOtherDetails = $('#trans-employer-nature-other-details').val();

        if (noteEmployement === '') {
            noteEmployement = 'None';
        }

        if (transNoteEmployement === '') {
            transNoteEmployement = 'None';
        }
        // Validate Occupation 
        if ($('#occupation-id').prop('selectedIndex') > 1) {
            // Validate If Person Is Salaried
            if ($('#employee-block').hasClass('d-none') === false) {
                // Validate Name of Employer
                minimumLength = parseInt($('#employer-name').attr('minlength'));
                maximumLength = parseInt($('#employer-name').attr('maxlength'));

                if (parseInt(nameOfEmployer.length) < parseInt(minimumLength) || parseInt(nameOfEmployer.length) > parseInt(maximumLength)) {
                    result = false;
                }

                // Validate Trans Name of Employer
                minimumLength = parseInt($('#trans-employer-name').attr('minlength'));
                maximumLength = parseInt($('#trans-employer-name').attr('maxlength'));

                if (parseInt(transNameOfEmployer.length) < parseInt(minimumLength) || parseInt(transNameOfEmployer.length) > parseInt(maximumLength)) {
                    result = false;
                }

                if (IsValidInputDate('#date-of-incorporation') === false) {
                    result = false;
                }

                if ($('#employment-type-id').prop('selectedIndex') < 1) {
                    result = false;
                }

                if ($('#nature-of-employer-id').prop('selectedIndex') < 1) {
                    result = false;
                }

                if ($('#employer-city-id').prop('selectedIndex') < 1) {
                    result = false;
                }

                if ($('#designation-id').prop('selectedIndex') < 1) {
                    result = false;
                }

                // Annual Income
                if (isNaN(annualIncome) === false) {

                    minimum = parseFloat($('#annual-income').attr('min'));
                    maximum = parseFloat($('#annual-income').attr('max'));

                    if (parseFloat(annualIncome) < parseFloat(minimum) || parseFloat(annualIncome) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Validate EPF Number
                minimumLength = parseInt($('#epf-number').attr('minlength'));
                maximumLength = parseInt($('#epf-number').attr('maxlength'));

                if (parseInt(epfNumber.length) < parseInt(minimumLength) || parseInt(epfNumber.length) > parseInt(maximumLength)) {
                    result = false;
                }

                // Validate EPF Number
                minimumLength = parseInt($('#trans-epf-number').attr('minlength'));
                maximumLength = parseInt($('#trans-epf-number').attr('maxlength'));

                if (parseInt(transEpfNumber.length) < parseInt(minimumLength) || parseInt(transEpfNumber.length) > parseInt(maximumLength)) {
                    result = false;
                }

                // Annual Income
                if (isNaN(employedSince) === false) {

                    minimum = parseFloat($('#employed-since').attr('min'));
                    maximum = parseFloat($('#employed-since').attr('max'));
                    if (parseFloat(employedSince) < parseFloat(minimum) || parseFloat(employedSince) > parseFloat(maximum))
                        result = false;
                }
                else {
                    result = false;
                }

                // Validate Employer Nature Other Details
                minimumLength = parseInt($('#employer-nature-other-details').attr('minlength'));
                maximumLength = parseInt($('#employer-nature-other-details').attr('maxlength'));

                if (parseInt(employerNatureOtherDetails.length) < parseInt(minimumLength) || parseInt(employerNatureOtherDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }

                // Validate Trans Employer Nature Other Details
                minimumLength = parseInt($('#trans-employer-nature-other-details').attr('minlength'));
                maximumLength = parseInt($('#trans-employer-nature-other-details').attr('maxlength'));

                if (parseInt(transEmployerNatureOtherDetails.length) < parseInt(minimumLength) || parseInt(transEmployerNatureOtherDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }

                // Validate Employer Address Details
                minimumLength = parseInt($('#employer-address-details').attr('minlength'));
                maximumLength = parseInt($('#employer-address-details').attr('maxlength'));

                if (parseInt(employerAddressDetails.length) < parseInt(minimumLength) || parseInt(employerAddressDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }

                // Validate Trans Employer Address Details
                minimumLength = parseInt($('#trans-employer-address-details').attr('minlength'));
                maximumLength = parseInt($('#trans-employer-address-details').attr('maxlength'));

                if (parseInt(transEmployerAddressDetails.length) < parseInt(minimumLength) || parseInt(transEmployerAddressDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }

                // Validate Employer Contact Details
                minimumLength = parseInt($('#employer-contact-details').attr('minlength'));
                maximumLength = parseInt($('#employer-contact-details').attr('maxlength'));

                if (parseInt(employerContactDetails.length) < parseInt(minimumLength) || parseInt(employerContactDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }

                // Validate Trans Employer Contact Details
                minimumLength = parseInt($('#trans-employer-contact-details').attr('minlength'));
                maximumLength = parseInt($('#trans-employer-contact-details').attr('maxlength'));

                if (parseInt(transEmployerContactDetails.length) < parseInt(minimumLength) || parseInt(transEmployerContactDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }
            }
        }
        else {
            result = false;
        }

    }
    function HideEmployeeSection() {
        debugger;
        if (isEmployee === false) {
            $('#employee-block').removeClass('d-none');

        }
        else {
            $('#employee-block').addClass('d-none');
        }
    }

    // Event listener for when occupation dropdown changes
    $('#occupation-id').focusout(function () {
        debugger;
        let occupationId = $(this).val();

        if (previousSelectedOccupationId !== occupationId) {
            SetOccupationDetails();

            $('.employer-input').val('');
            $('#enable-employee').prop('checked', false);

            previousSelectedOccupationId = occupationId;
            IsValidOccupation();
        }
    });

    $('#epf-number').focusout(function () {
        // Regular expression for EPF number (Example format: MH/BAN/0001234/5678901)
        let epfRegex = /^[A-Z]{2}\/[A-Z]{3}\/\d{7}\/\d{7}$|^[A-Z]{3}\d{2}\d{7}\d{7}$/;
        let epfNumber = $('#epf-number').val().trim();

        // Trim the EPF number to a maximum of 22 characters
        epfNumber = epfNumber.substring(0, 22);

        $('#epf-number').val(epfNumber); // Update the input field with the trimmed value

        if (epfRegex.test(epfNumber)) {
            $('#epf-number-error').addClass('d-none');
        }
        else {
            $('#epf-number-error').removeClass('d-none');
        }
    });

    // Verification Code Visibility & Input Type Visibility Based On Contact Type
    $('#contact-type').focusout(function () {
        $('#field-value').val('');

        contactType = $('#contact-type option:selected').text();
        isMobile = contactType.includes('Mobile');
        isEmail = contactType.includes('Email');

        // Add Field Value Attribute Type As Number For Mobile Contact Type
        if (isMobile) {
            $('#field-value').attr('type', 'number');
            $('#field-value').addClass('real-number');
            $('.is-verified-field').addClass('d-none');
            $('#send-code').removeClass('d-none');
            $('.verification-code').removeClass('d-none');
        }
        else {
            $('#field-value').removeAttr('type');
            $('#field-value').removeClass('real-number');
            $('#send-code').addClass('d-none');
            $('.is-verified-field').removeClass('d-none');
            $('#resend').addClass('d-none');
            $('#field-value-mobile-error').addClass('d-none');
            $('.verification-code').addClass('d-none');
            $('#verification-code').val('0');
        }
    });
    function SetPolicySumInsured() {
        debugger;
        if (isVerifyView === false) {
            let insurancePremium = $('#insurance-premium').val();

            $('#policy-sum-insured').attr('min', insurancePremium)
        }
    }

    // Insurance Premium
    $('#insurance-premium').focusout(function () {
        debugger;
        $('#policy-sum-insured').val('');
        SetPolicySumInsured();
    });

    $('#commencement-date').focusout(function () {
        $('#expiry-date-vehicle-insurance').val('');
    });

    $('#expiry-date-vehicle-insurance').click(function () {
        debugger;
        let minExpiryDate = new Date($('#commencement-date').val());

        minExpiryDate.setMonth(minExpiryDate.getMonth() + 6);

        $('#expiry-date-vehicle-insurance').attr('min', GetInputDateFormat(minExpiryDate));

        let maxExpiryDate = new Date($('#commencement-date').val());
        maxExpiryDate.setFullYear(maxExpiryDate.getFullYear() + 5);

        $('#expiry-date-vehicle-insurance').attr('max', GetInputDateFormat(maxExpiryDate));

    });

    function DuplicatepolicyNumber() {
        let policyNumber = $('#policy-number').val();
        $.get('/AccountChildAction/GetPolicyNumber', { _inputedPolicyNumber: policyNumber, async: false }, function (data) {
            debugger;
            if (data === 'True') {
                isDuplicatePolicyNumber = true;
            }
            else {
                isDuplicatePolicyNumber = false;
            }
        });
    }

    //Duplicate Policy Number
    $('#policy-number').focusout(function () {
        debugger;
        DuplicatepolicyNumber();
    });

    //ToggleSwitch hide show based of Type Of Coverage
    $('#type-of-coverage-id').focusout(function () {
        debugger;
        let data = $('#type-of-coverage-id option:selected').val();
        if (data === 'TPR') {
            $('#type-of-coverage-id-block').removeClass('d-none');
        }
        else {
            $('#type-of-coverage-id-block').addClass('d-none');
        }
    });

    //Vehicle Contract Detail Focusout Events
    //vehicle contract detail
    $('#start-date').focusout(function () {
        debugger;
        let today = new Date();
        let maxDate = today;
        let startDate = new Date($('#start-date').val());

        // Clear End Date
        $('#end-date').val('');

        maxDate.setFullYear(maxDate.getFullYear() - 5);

        if (GetInputDateFormat(startDate) < GetInputDateFormat(maxDate)) {
            $('#start-date-error').removeClass('d-none');
        }
        else {
            $('#start-date-error').addClass('d-none');
        }
    });

    $('#end-date').click(function () {
        debugger;
        let minDate = new Date($('#start-date').val());

        minDate.setMonth(minDate.getMonth() + 6);

        $('#end-date').attr('min', GetInputDateFormat(minDate));

        let maxDate = new Date($('#start-date').val());
        maxDate.setFullYear(maxDate.getFullYear() + 5);
        $('#end-date').attr('max', GetInputDateFormat(maxDate));

    });

    //ToggleSwitch hide show based of contract nature id
    $('#contract-nature-id').focusout(function () {
        debugger;
        let data = $('#contract-nature-id option:selected').val();
        if (data === 'OTH') {
            $('#contract-nature-id-block').removeClass('d-none');
        }
        else {
            $('#contract-nature-id-block').addClass('d-none');
        }
    });

    $('#other-contract-nature-detail').focusout(function () {
        debugger;
        let data = $('#other-contract-nature-detail').val();
        if (data === 'None' || data === 'none' || data === 'Other' || data === 'other') {
            $('#other-contract-nature-detail').val('');
        }
    });

    $('#institude-id').focusout(function () {
        debugger;
        let institudeName = $('#institude-id option:selected').text();
        if (institudeName === 'Other ---> इतर') {
            $('#other-institude-input').removeClass('d-none');
        }
        else {
            $('#other-institude-input').addClass('d-none');
        }

    });

    $('.relation-with-applicant').click(function () {
        debugger;
        let relationType = $('.relation-with-applicant:checked').val();
        if (relationType === 'OTH') {
            $('#other-relation-input').removeClass('d-none');
        } else {
            $('#other-relation-input').addClass('d-none');
            $('#other-relation-title').val('None');
        }
    });

    $('#installment-amount').focusout(function () {
        debugger;
        $('#contract-monthly-amount').attr('min', ($(this).val()) * 2);

    });

    //contract detail Input Validation
    $('.vehicle-contract-detail-input').focusout(function () {
        debugger;
        IsValidVehicleContractDetailAccordionInputs();
    });
    function GetConsumerLoanMargin() {
        debugger;
        let consumerDurableId = $('#consumer-durable-item-id option:selected').val();
        $.get('/AccountChildAction/GetConsumerLoanMargin', { _schemePrmKey: schemePrmkey, _consumerDurableItemId: consumerDurableId, async: false }, function (data) {
            debugger;
            productAmount = parseFloat($('#product-amount').val());
            downPayment = parseFloat($('#down-payment').val());
            downPaymentMargin = parseFloat((productAmount * data) / 100);

            $('#down-payment').attr('min', downPaymentMargin);
            $('#down-payment').attr('max', productAmount);

            if ((downPayment < downPaymentMargin) || (downPayment > productAmount)) {
                debugger;
                $('#product-down-payment-margin-error').text(`Please Enter A Valid Down Payment Between ${downPaymentMargin} And ${productAmount}.`).removeClass('d-none');
                result = false;
            }
            else {
                $('#product-down-payment-margin-error').addClass('d-none');
            }
        });
    }

    // Down Payment Consumer Loan
    $('#down-payment').focusout(function () {
        debugger;
        GetConsumerLoanMargin();
    });

    // Product Amount Consumer Loan
    $('#product-amount').focusout(function () {
        debugger;
        $('#down-payment').val('');
        GetConsumerLoanMargin();
    });

    $('#property-type').focusout(function () {
        // If Property Type Is Other Then Add TextField For Discription.
        let propertyType = $('#property-type').val();
        if (propertyType === 'OTH') {
            $('#other-property-type-input').removeClass('d-none');
            $('#other-property-type').attr('minlength', '5');
        } else {
            $('#other-property-type-input').addClass('d-none');
            $('#other-property-type').val('None');
            $('#other-property-type').attr('minlength', '4');
        }
    });

    $('#property-ownership-status').focusout(function () {
        // If Property Ownership Status Is Other Then Add TextField For Discription.
        let propertyOwnershipStatus = $('#property-ownership-status option:selected').val();
        if (propertyOwnershipStatus === 'OTH') {
            $('#other-ownership-status-input').removeClass('d-none');
        } else {
            $('#other-ownership-status-input').addClass('d-none');
            $('#other-ownership-status').val('None');
        }
    });

    //  ************** A U T O      C O M P L E T E   **************
    $("#person-id").autocomplete(
        {
            minLength: 0,
            appendTo: '#person-name',
            scroll: true,
            autoFocus: true,
            source: function (request, response) {
                let responseDropdownListArray = [];
                dropDownListItemCount = finalDropdownListArray.length;

                if (parseInt(dropDownListItemCount) > 0) {
                    responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                        if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1)
                            return { label: key.Text, valueId: key.Value }
                        else
                            return null;
                    });

                    // The slice() method returns selected elements in an array, as a new array. 
                    // The slice() method selects from a given start, up to a (not inclusive) given end.
                    response(responseDropdownListArray.slice(0, 10));
                }
                else
                    response([{ label: 'No Records Found', value: -1 }]);
            },
            select: function (event, ui) {
                event.preventDefault();
                $('#person-id1').val(ui.item.valueId);
                $('#person-id').val(ui.item.label);
                selectedPersonId = ui.item.valueId;
            }
        }).focus(function () {
            if (isVerifyView === false) {
                $('#person-id').val('');
                selectedPersonId = '';

                // Assign Array Without Reference  *** Use Slice Method
                finalDropdownListArray = personDropdownListData.slice();

                $(this).autocomplete('search');
            }
        });

    // Joint Account Person Autocomplete
    $("#person-id-joint-account-holder").autocomplete(
        {
            minLength: 0,
            appendTo: '#person-id-joint-account-holder-div',
            scroll: true,
            autoFocus: true,
            source: function (request, response) {
                let responseDropdownListArray = [];
                dropDownListItemCount = finalDropdownListArray.length;

                if (parseInt(dropDownListItemCount) > 0) {
                    responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                        if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1) {
                            // Remove Main Customer Person Id
                            if (key.Value != selectedPersonId)
                                return { label: key.Text, valueId: key.Value }
                        }
                        else
                            return null;
                    });

                    // The slice() method returns selected elements in an array, as a new array. 
                    // The slice() method selects from a given start, up to a (not inclusive) given end.
                    response(responseDropdownListArray.slice(0, 10));
                }
                else
                    response([{ label: 'No Records Found', value: -1 }]);
            },
            select: function (event, ui) {
                event.preventDefault();
                $('#person-id-joint-account-holder').val(ui.item.label);
                selectedjointPersonId = ui.item.valueId;
                selectedjointPersonText = ui.item.label;
            },
        }).focus(function () {
            // Clear Data Table Added Person Id Data
            // Clear Selected Person Id
            $('#person-id-joint-account-holder').val('');

            selectedjointPersonId = '';
            selectedjointPersonText = '';
            let dataTablePersonIdArray = [];

            // Assign Array Without Reference  *** Use Slice Method
            finalDropdownListArray = personDropdownListDataForJointAccount.slice();
            dropDownListItemCount = finalDropdownListArray.length;

            // Get Added Person Id For Remove From List
            $('#tbl-joint-account > tbody > tr').each(function () {
                let currentRow = $(this).closest("tr");
                let columnValues = (jointAccountDataTable.row(currentRow).data());

                if (typeof columnValues !== 'undefined' && columnValues != null)
                    dataTablePersonIdArray.push({ 'Value': columnValues[1], 'Text': columnValues[2] })
            });

            if (parseInt(dropDownListItemCount) > 0 && parseInt(dataTablePersonIdArray.length) > 0) {
                while (dropDownListItemCount--) {
                    // Remove Added Joint Account Person Id From List
                    for (let jointAccountPersonId of dataTablePersonIdArray) {
                        if (finalDropdownListArray[dropDownListItemCount].Value === jointAccountPersonId.Value)
                            // splice - remove item from array at a choosen index
                            finalDropdownListArray.splice(dropDownListItemCount, 1);
                    }
                }
            }

            $(this).autocomplete('search');
        });

    // Nominee Person AutoComplete 
    // While Adding Nominee Hide Selected Customer Name In Nominee Dropdown List.
    $('#nominee-person-id').autocomplete(
        {
            minLength: 0,
            appendTo: '#nominee-person-information-number-input',
            scroll: true,
            autoFocus: true,
            source: function (request, response) {
                let responseDropdownListArray = [];
                dropDownListItemCount = finalDropdownListArray.length;

                if (parseInt(dropDownListItemCount) > 0) {
                    responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                        if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1) {
                            // Skip Selected Customer Name From Dropdownlist
                            if (key.Text != $('#customer-person-id option:selected').text())
                                return { label: key.Text, valueId: key.Value }
                        }
                        else
                            return null;
                    });

                    // The slice() method returns selected elements in an array, as a new array. 
                    // The slice() method selects from a given start, up to a (not inclusive) given end.
                    response(responseDropdownListArray.slice(0, 10));
                }
                else
                    response([{ label: 'No Records Found', value: -1 }]);
            },
            select: function (event, ui) {
                event.preventDefault();
                $('#nominee-person-id').val(ui.item.label);
                nomineePersonInformationNumber = ui.item.valueId;
                nomineePersonInformationNumberText = ui.item.label;
            },
        }).focus(function () {
            $('#nominee-person-id').val('');
            nomineePersonInformationNumber = '';
            nomineePersonInformationNumberText = '';

            // Assign Array Without Reference  *** Use Slice Method
            finalDropdownListArray = personDropdownListDataForNominee.slice();

            $(this).autocomplete('search');
        });

    // Nominee Guardian Person AutoComplete 
    $('#nominee-guardian-person-information-number').autocomplete(
        {
            minLength: 0,
            appendTo: '#nominee-guardian-person-information-number-input',
            scroll: true,
            autoFocus: true,
            source: function (request, response) {
                let responseDropdownListArray = [];
                dropDownListItemCount = finalDropdownListArray.length;

                if (parseInt(dropDownListItemCount) > 0) {
                    responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                        if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1)
                            return { label: key.Text, valueId: key.Value }
                        else
                            return null;
                    });

                    // The slice() method returns selected elements in an array, as a new array. 
                    // The slice() method selects from a given start, up to a (not inclusive) given end.
                    response(responseDropdownListArray.slice(0, 10));
                }
                else
                    response([{ label: 'No Records Found', value: -1 }]);

            },
            select: function (event, ui) {
                event.preventDefault();
                $('#nominee-guardian-person-information-number').val(ui.item.label);
                guardianNomineePersonInformationNumber = ui.item.valueId;
                guardianNomineePersonInformationNumberText = ui.item.label;
            },
        }).focus(function () {
            // Clear Selected Guardian Nominee Person
            $('#nominee-guardian-person-information-number').val('');
            guardianNomineePersonInformationNumber = '';
            guardianNomineePersonInformationNumberText = '';

            // Assign Array Without Reference  *** Use Slice Method
            finalDropdownListArray = personDropdownListDataForGuardian.slice();

            $(this).autocomplete('search');
        });

    $("#guarantor-name-id").autocomplete(
        {
            minLength: 0,
            appendTo: '#guarantor-person-input',
            scroll: true,
            autoFocus: true,
            source: function (request, response) {
                let responseDropdownListArray = [];
                dropDownListItemCount = finalDropdownListArray.length;

                if (parseInt(dropDownListItemCount) > 0) {
                    responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                        if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1) {
                            // Remove Main Customer Person Id
                            if (key.Value != selectedPersonId)
                                return { label: key.Text, valueId: key.Value }
                        }
                        else
                            return null;
                    });

                    // The slice() method returns selected elements in an array, as a new array. 
                    // The slice() method selects from a given start, up to a (not inclusive) given end.
                    response(responseDropdownListArray.slice(0, 10));
                }
                else
                    response([{ label: 'No Records Found', value: -1 }]);
            },
            select: function (event, ui) {
                event.preventDefault();
                $('#guarantor-name-id').val(ui.item.label);
                guarantorId = ui.item.valueId;
                guarantorName = ui.item.label;
            },
        }).focus(function () {
            // Clear Data Table Added Person Id Data
            // Clear Selected Person Id
            $('#guarantor-name-id').val('');
            guarantorId = '';
            guarantorName = '';
            let dataTablePersonIdArray = [];

            // Assign Array Without Reference  *** Use Slice Method
            finalDropdownListArray = guarantorDropdownListData.slice();
            dropDownListItemCount = finalDropdownListArray.length;

            // Get Added Person Id For Remove From List
            $('#tbl-guarantor-detail > tbody > tr').each(function () {
                let currentRow = $(this).closest("tr");
                let columnValues = (guarantorDetailDataTable.row(currentRow).data());

                if (typeof columnValues !== 'undefined' && columnValues != null)
                    dataTablePersonIdArray.push({ 'Value': columnValues[1], 'Text': columnValues[2] })
            });

            if (parseInt(dropDownListItemCount) > 0 && parseInt(dataTablePersonIdArray.length) > 0) {
                while (dropDownListItemCount--) {
                    // Remove Added Joint Account Person Id From List
                    for (let guarantorDetailPersonId of dataTablePersonIdArray) {
                        if (finalDropdownListArray[dropDownListItemCount].Value === guarantorDetailPersonId.Value)
                            // splice - remove item from array at a choosen index
                            finalDropdownListArray.splice(dropDownListItemCount, 1);
                    }
                }
            }

            $(this).autocomplete('search');
        });

    //  ************** Accordion Input Validation  **************

    // Gold Loan Toggle Switch Change Function
    $('#enable-any-damage').change(function () {
        debugger;
        $('#metal-net-weight').val('');
        $('#damage-weight').val(0);
        SetNetWeightOfGold();
    });
    function SetNetWeightOfGold() {
        debugger;
        let grossWeight = $('#metal-gross-weight').val();
        let damageWeight = $('#damage-weight').val();

        if (!isNaN(grossWeight) && !isNaN(damageWeight)) {
            $('#metal-net-weight').attr('max', Math.max(0, grossWeight - damageWeight));
        }
    }
    function GoldCollateralHideSection() {
        if ($('#enable-any-damage').is(':checked') == false) {
            $('#any-damage-block').addClass('d-none');
        }
        else {
            $('#any-damage-block').removeClass('d-none');
        }
        if ($('#enable-any-westage').is(':checked') == false) {
            $('#any-westage-block').addClass('d-none');
        }
        else {
            $('#any-westage-block').removeClass('d-none');
        }
        if ($('#enable-diamond').is(':checked') == false) {
            $('#diamond-block').addClass('d-none');
        }
        else {
            $('#diamond-block').removeClass('d-none');
        }
    }

    $('#metal-net-weight').focusout(function () {
        valuationAmount = $(this).val() * goldLoanRate;
        $('#gold-valuation-amount').val(valuationAmount);
    });

    $('#metal-purity-id').focusout(function () {
        debugger;
        metalPurity = $('#metal-purity-id option:selected').val();
        GetGoldLoanRate();
    });

    // Gross Weight 
    $('#metal-gross-weight').focusout(function () {
        $('#metal-net-weight').val('');
        SetNetWeightOfGold();
    });

    // Damage Weight 
    $('#damage-weight').focusout(function () {
        $('#metal-net-weight').val('');
        SetNetWeightOfGold();
    });

    function GetGoldLoanRate() {
        $.get('/AccountChildAction/GetGoldLoanRateOfMetal', { _metalPurity: metalPurity, async: false }, function (data) {
            debugger;
            goldLoanRate = data;
        });
    }

    // SMS Service Detail Input Validation
    $('.sms-service-input').focusout(function () {
        if (IsValidSMSServiceDetailAccordionInputs())
            $('#sms-service-accordion-error').addClass('d-none');
    });

    // Email Service Detail Input Validation
    $('.email-service-input').focusout(function () {
        if (isVerifyView === false) {
            IsValidEmailServiceDetailAccordionInputs();
        }
    });

    // Email Service Radio Button Input Validation
    $('.email-service-radio-input').change(function () {
        IsValidEmailServiceDetailAccordionInputs();
    });

    // field investigation
    $('.field-investigation-input').focusout(function () {
        debugger;
        IsValidFieldInvestigationAccordionInputs()
    });

    $('.preowned-vehicle-loan-inspection-input').focusout(function () {
        debugger;
        IsValidPreOwnedVehicleLoanInspectionAccordionInputs()
    });

    $('.vehicle-loan-collateral-detail-input').focusout(function () {
        IsValidVehicleLoanCollateralDetailAccordionInputs();
    });

    // Standing Instruction Input Validation
    $('.standing-instruction-input').focusout(function () {
        if (isVerifyView === false)
            IsValidStandingInstructionAccordionInputs();
    });

    $('.vehicle-insurance-detail-input').focusout(function () {
        debugger;
        IsValidVehicleInsuranceDetailAccordionInputs();

    });

    //CustomerVehicleLoanPermitDetail
    $('.vehicle-loan-permit-detail-input').focusout(function () {
        debugger;
        IsValidCustomerLoanPermitDeatilAccordionInputs();
    });

    //CustomerEducationLoanDetail
    $('.educational-loan-input').focusout(function () {
        debugger;
        IsValidCustomerEducationLoanDetailAccordionInputs();
    });

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

    // Debt To Income Ratio Input Validation
    $('.income-ratio-input').focusout(function () {
        debugger;
        myExpensesValue = $(this).val();
        IsValidDebtToIncomeRatioAccordionInputs();
        if (isDisplayAlert === true) {
            $(this).val(0);
        }
    });

    $('.income-ratio-input').change(function () {
        isDisplayAlert = false;
    });

    //Loan Against Property Collateral Detail
    $('.loan-against-property-input').focusout(function () {
        debugger;
        IsValidLoanAgainstPropertyAccordionInputs();
    });

    //Business Loan Collateral Detail
    $('.business-loan-input').focusout(function () {
        debugger;
        IsValidBusinessLoanCollateralDetailAccordionInputs();
    });
    //Vehicle Loan Registration Date
    function RegistrationDateClickEventFunction() {

        let today = new Date();

        // Allow Older Date Upto Manufacture Year
        let minRegistrationDate = new Date($('#manufacture-year').val() + '-01-01');
        let maxRegistrationDate = today;

        $('#registration-date').attr('min', GetInputDateFormat(minRegistrationDate));
        $('#registration-date').attr('max', GetInputDateFormat(maxRegistrationDate));
    }

    // Registration Date
    $('#registration-date').click(function () {
        RegistrationDateClickEventFunction();
    });

    // Vehicle Make
    $('#vehicle-make-id').focusout(function () {
        if (isVerifyView === false) {
            debugger;
            vehicleMakeId = $(this).val();
            if (vehicleMakeId !== lastVehicleMakeId) {
                $('#vehicle-colour-id').prop('selectedIndex', 0);
                $('#vehicle-variant-id').prop('selectedIndex', 0);
            }
            lastVehicleMakeId = vehicleMakeId;
            SetVehicleModelDropdownList();
        }
    });

    // Vehicle Model
    $('#vehicle-model-id').focusout(function () {
        if (isVerifyView === false) {
            SetVehicleVariantDropdownList();
        }
    });

    // Loan Purpose
    $('.loan-purpose').change(function () {
        debugger;
        $('#loan-purpose-new-block').removeClass('d-none');
        $('#loan-purpose-error').addClass('d-none');

        if ($('#vehicle-model-id').prop('selectedIndex') > 0) {
            SetVehicleType();
        }
    });

    // Set Vehicle Type
    function SetVehicleType() {
        vehicleModelId = $('#vehicle-model-id').val();

        $.get('/AccountChildAction/GetVehicleTypePrmKey', { _vehicleModelId: vehicleModelId, async: false }, function (data) {
            debugger;
            let objSelectedSchemeRecord;
            let loanPurpose = $('.loan-purpose:checked').val();

            vehicleTypePrmKey = data;

            // Setting Based On Loan Purpose
            if (loanPurpose !== PURCHASE_NEW_VEHICLE) {
                // $.grep() - Nothing But Work As Filter; It Return Data Only Meet A Condition
                objSelectedSchemeRecord = $.grep(objSchemePreOwnedVehicleParameter, function (element) { return element.VehicleTypePrmKey === vehicleTypePrmKey });
                debugger;
                // Check EnableVehicleInspection For Visibility
                if (objSelectedSchemeRecord[0].EnableVehicleInspection === true) {
                    debugger;
                    $('#loan-purpose-new-block').removeClass('d-none');
                }
                else {
                    $('#loan-purpose-new-block').addClass('d-none');
                }
            }
            else {
                // $.grep() - Nothing But Work As Filter; It Return Data Only Meet A Condition
                objSelectedSchemeRecord = $.grep(objSchemeNewVehicleParameter, function (element) { return element.VehicleTypePrmKey === vehicleTypePrmKey });
            }

            // Photo Accordion Visiblity 
            if (objSelectedSchemeRecord[0].PhotoUpload === DISABLE) {
                $('#vehicle-photo-card').addClass('d-none');
            }
            else {
                $('#vehicle-photo-card').removeClass('d-none');

                minimumVehiclePhoto = objSelectedSchemeRecord[0].MinimumNumberOfPhoto;
                maximumVehiclePhoto = objSelectedSchemeRecord[0].MaximumNumberOfPhoto;
                allowFileFormatForDb = objSelectedSchemeRecord[0].AllowedFileFormatsForDb;
                maximumFileSizeForDb = objSelectedSchemeRecord[0].MaximumFileSizeForDb;
                allowFileFormatForLocalStorage = objSelectedSchemeRecord[0].AllowedFileFormatsForLocalStorage;
                maximumFileSizeForLocalStorage = objSelectedSchemeRecord[0].MaximumFileSizeForLocalStorage;
                enablePhotoUploadInLocalStorage = objSelectedSchemeRecord[0].EnablePhotoUploadInLocalStorage;
            }

            // Set Vehicle Type Condition
            $.get('/AccountChildAction/GetSysNameOfVehicleType', { _vehicleTypePrmKey: vehicleTypePrmKey, async: false }, function (data) {
                debugger;
                let vehicleTypeSysName = data;

                if (vehicleTypeSysName === TWO_WHEELER) {
                    $('#seating-capacity').val(2);
                    $('#registered-laden-weight').val(0);
                    $('#business-experience').val(0);
                    $('#number-of-tyres').val(2).attr({ 'min': 2, 'max': 2 }).addClass('read-only');
                }
                else {
                    if (vehicleTypeSysName === THREE_WHEELER) {
                        $('#number-of-tyres').val(3).attr({ 'min': 3, 'max': 3 }).addClass('read-only');
                    }
                    else {
                        $('#number-of-tyres').attr({ 'min': 4, 'max': 210 }).removeClass('read-only');
                    }
                }
            });
        });
    }

    // Commercial Use
    $('#is-used-for-commercial').change(function () {
        debugger;
        IsUsedForCommercialChangeEventFunction();
    });

    function IsUsedForCommercialChangeEventFunction() {
        debugger;
        if ($('#is-used-for-commercial').is(':checked')) {
            $('.commercial-vehicle').removeClass('d-none');
            $('#vehicle-loan-permit-detail-card, #vehicle-contract-detail-card').removeClass('d-none');
            $('#registered-laden-weight').attr('min', 1000);
            $('#registered-laden-weight').attr('max', 99999);
            $('#business-experience').attr('min', 1);
            $('#business-experience').attr('max', 600);

        }
        else {
            $('.commercial-vehicle').addClass('d-none');
            $('#has-contract-block').addClass('d-none');

            $('#has-contract').prop('checked', false);
            $('#registered-laden-weight').val(0);
            $('#business-experience').val(0);

            $('#vehicle-loan-permit-detail-card, #vehicle-contract-detail-card').addClass('d-none');
        }
    }

    // Registration Number
    $('#vehicle-registration-number').focusout(function () {
        IsValidRegistrationNumber();
    });

    // Maintenance Remark
    $('#odo-meter-reading').focusout(function () {
        let odoMeterReading = $('#odo-meter-reading').val();

        if (odoMeterReading > 100000) {
            $('.maintenance-remark').removeClass('d-none');
        }
        else {
            $('.maintenance-remark').addClass('d-none');
        }

    });

    //vehicleLoanCollateral registration number
    function IsValidRegistrationNumber() {
        debugger;
        let regNumber = $("#vehicle-registration-number").val();
        let regExp = /^[A-Z]{2}[-\s]?\d{2}[-\s]?[A-Z]{1,2}[-\s]?\d{4}$/;
        let result = true;
        if (regExp.test(regNumber)) {
            $('#vehicle-registration-number-error').addClass('d-none');
        }
        else {
            result = false;
            $('#vehicle-registration-number-error').removeClass('d-none');
        }

        return result;
    }

    //Set Attribute Values
    $('#ex-showroom-price').focusout(function () {
        $('#onroad-price').attr('min', $(this).val());
        $('#additional-accessories-amount').attr('max', ($(this).val()) * 0.20);
    });

    //vehicleLoanCollateral Engine Number
    $('#engine-number').focusout(function () {
        debugger;
        IsValidEngineNumber();
    });

    //vehicleLoanCollateral Engine Number
    function IsValidEngineNumber() {
        debugger;
        let engNumber = $("#engine-number").val().toUpperCase();
        let regExp = /^[A-Z0-9]{5,17}$/;

        if (regExp.test(engNumber)) {
            $('#engine-number-error').addClass('d-none');
        }
        else {
            result = false;
            $('#engine-number-error').removeClass('d-none');
        }
    }

    //vehicleLoanCollateral Chasis Number
    $('#chasis-number').focusout(function () {
        IsValidChasisNumber();
    });

    //vehicleLoanCollateral Chasis Number
    function IsValidChasisNumber() {
        debugger;
        let chasNumber = $("#chasis-number").val().toUpperCase();
        let regExp = /^[A-Z0-9]{5,17}$/;
        if (regExp.test(chasNumber)) {
            $('#chasis-number-error').addClass('d-none');
        }
        else {
            result = false;
            $('#chasis-number-error').removeClass('d-none');
        }
    }

    //Clear Registration Date
    $('#manufacture-year').focusout(function () {
        debugger;
        let manufactureYear = $(this).val();
        let currentDate = new Date();
        let currentYear = currentDate.getFullYear();
        let CurrentMonth = currentDate.getMonth();
        let vehicleAgeYear = currentYear - manufactureYear;
        vehicleAge = (vehicleAgeYear * 12) + CurrentMonth;
        SetVehicleTenure();
        $('#registration-date').val('');
    });

    //Clear Registration Date
    $('#current-valuation-amount').focusout(function () {

        SetVehicleTenure();
    });

    //Colour Description For Other
    $('#vehicle-colour-id').focusout(function () {
        debugger;
        let otherColour = $('#other-colour').val();
        let colourText = $('#vehicle-colour-id option:selected').text()

        if (colourText.includes('Other')) {
            $('#other-colour-input').removeClass('d-none');
        }
        else {
            $('#other-colour-input').addClass('d-none');
            otherColour = 'None';
        }
    });

    // Change Event On Is Used For Commercial
    $('#enable-is-used-for-commercial').change(function () {
        //Hide Permit Accordion 
        $('#vehicle-loan-permit-detail-card').addClass('d-none');
        //Hide Contract Accordion
        $('#vehicle-contract-detail-card').addClass('d-none');
    });

    //Set Attribute Values
    $('#onroad-price').focusout(function () {
        $('#insurance-premium').val('');
        $('#insurance-premium').attr('min', ($(this).val()) * 0.05);
        $('#insurance-premium').attr('max', ($(this).val()) * 0.10);
    });

    // Function To Set difference between the account opening date and the maturity date.
    function SetTenure() {
        // Get the values of account opening date and maturity date from the input fields
        const accountOpeningDate = $('#account-opening-date').val();
        const maturityDate = $('#maturity-date').val();

        // Check if both dates are provided
        if (accountOpeningDate && maturityDate) {
            // Convert the date strings to Date objects
            const startDate = new Date(accountOpeningDate);
            const endDate = new Date(maturityDate);

            // Calculate the differences in years, months, and days
            let years = endDate.getFullYear() - startDate.getFullYear();
            let months = endDate.getMonth() - startDate.getMonth();
            let days = endDate.getDate() - startDate.getDate();

            // Adjust the days and months if necessary
            if (days < 0) {
                months -= 1;
                days += new Date(endDate.getFullYear(), endDate.getMonth(), 0).getDate();
            }

            if (months < 0) {
                years -= 1;
                months += 12;
            }

            // Update the input fields with the calculated differences
            $('#year').val(years);
            $('#month').val(months);
            $('#day').val(days);
        }
    }

    // Function to update the maturity date based on the tenure (years, months, and days).
    function SetMaturityDate() {
        let maximumTenure = 0;

        // Get the tenure values from the input fields, default to 0 if empty
        const years = parseInt($('#year').val()) || 0;
        const months = parseInt($('#month').val()) || 0;
        const days = parseInt($('#day').val()) || 0;

        if (!$('#day').hasClass('read-only'))
            maximumTenure = parseInt($('#day').attr('max')) || 0;

        if (!$('#month').hasClass('read-only'))
            maximumTenure = parseInt($('#month').attr('max')) || 0;

        if (!$('#year').hasClass('read-only'))
            maximumTenure = parseInt($('#year').attr('max')) || 0;

        // Get the account opening date from the input field
        const accountOpeningDate = $('#account-opening-date').val();

        // Check if the account opening date is provided
        if (accountOpeningDate) {
            // Convert the date string to a Date object
            let startDate = new Date(accountOpeningDate);

            // Create a new Date object for the maturity date, starting from the account opening date
            let maturityDate = new Date(startDate);

            // Adjust the maturity date based on the tenure values
            maturityDate.setFullYear(maturityDate.getFullYear() + years);
            maturityDate.setMonth(maturityDate.getMonth() + months);
            maturityDate.setDate(maturityDate.getDate() + days);

            // Set Maximum Mature Date
            startDate.setDate(parseInt(startDate.getDate()) + parseInt(maximumTenureIdDays));

            $('#maturity-date').attr('max', GetInputDateFormat(startDate));

            // Format the new maturity date to YYYY-MM-DD and set it in the input field
            $('#maturity-date').val(GetInputDateFormat(maturityDate));
        }
    }

    // Focusout in  SetTenure  Function
    $('#maturity-date').focusout(function () {
        if (!isVerifyView)
            SetTenure();
    });

    //Focusout in  SetMaturityDate
    $('.tenure').focusout(function () {
        if (!isVerifyView)
            SetMaturityDate();
        $('#vehicle-tenure-error').addClass('d-none');
    });

    //validation For Negative Number
    $('#year, #month, #day').keypress(function (e) {
        let value = parseInt($(this).val() + e.key);

        let min = $(this).attr('min');
        let max = $(this).attr('max');

        if (parseInt(value) < parseInt(min)) {
            $(this).val(parseInt(min));
            e.preventDefault();
        }

        if (parseInt(value) > parseInt(max)) {
            $(this).val(parseInt(max));
            e.preventDefault();
        }
    });

    $('#monthly-income').focusout(function () {
        $('#monthly-rent-payments').attr('max', ($(this).val() * 0.50));
        $('#monthly-taxes-expense').attr('max', ($(this).val() * 0.30));
        $('#monthly-insurance-expense').attr('max', ($(this).val() * 0.10));
        $('#educational-loan-emi').attr('max', ($(this).val() * 0.20));
        $('#personal-loan-emi').attr('max', ($(this).val() * 0.30));
        $('#co-signed-loan-emi').attr('max', ($(this).val() * 0.15));
        $('#vehicle-loan-emi').attr('max', ($(this).val() * 0.20));
        $('#minimum-credit-card-payments').attr('max', ($(this).val() * 0.15));
        $('#monthly-car-payments').attr('max', ($(this).val() * 0.25));
        $('#monthly-time-share-payments').attr('max', ($(this).val() * 0.05));
        $('#monthly-child-support-payment').attr('max', ($(this).val() * 0.20));
        $('#monthly-alimony-payment').attr('max', ($(this).val() * 0.15));

    });

    // Total Business Experience focusout
    $('#total-business-experience').focusout(function () {
        $('#total-business-experience').attr('min', minimumBusinessExperience);
    })

    // Business Loan Annual Turn Over focusout
    $('#business-loan-annual-turn-over').focusout(function () {
        $('#business-loan-annual-turn-over').attr('min', minimumTurnOverAmount);
    })

    // Property Value focusout
    $('#property-value').focusout(function () {
        debugger;
        $('#down-payment-amount').val('');
        $('#outstanding-loan-amount').val('');
        $('#monthly-repayment-amount').val('');
    })

    // Down Payment Amount focusout
    $('#down-payment-amount').focusout(function () {
        debugger;
        let propertyMaximum = $('#property-value').val();
        $('#down-payment-amount').attr('max', propertyMaximum);
    });

    // Outstanding Loan Amount focusout
    $('#outstanding-loan-amount').focusout(function () {
        debugger;
        let propertyMaximum = $('#property-value').val();
        $('#outstanding-loan-amount').attr('max', propertyMaximum);
        $('#monthly-repayment-amount').val('');
        $('#monthly-repayment-amount').attr('max', $(this).val());
    });

    // Standing Instruction 
    $('#enable-auto-debit').change(function () {
        $('#auto-debit-block').removeClass('d-none');
    });

    // ****** Remove After Following doc-upload working Successfully Document File Uploader

    //$('#document-file-uploader').change(function ()
    //{
    //    debugger;
    //    $('#document-file-uploader-error').addClass('d-none');    
    //    if (this.files.length > 0)
    //    {
    //        const uploadFile = this.files[0];
    //        // Upload File
    //        if (IsValidFile('Document', uploadFile))
    //        {
    //            let reader = new FileReader();
    //            reader.onload = function (e) {
    //                $('#document-file-uploader-image-preview').attr('src', e.target.result);
    //            }
    //            reader.readAsDataURL(uploadFile);
    //        }
    //    }
    //    // ****************** Review Code Then Remove
    //    //else {
    //    //    $('#document-file-uploader-image-preview').attr('src', '#');
    //    //    $(this).val('');
    //    //}
    //});

    // Document File Uploader
    $('.doc-upload').change(function () {
        debugger;
        let docInput = '';
        let myId = $(this).attr('id');

        // Document
        switch (myId) {
            case 'document-file-uploader':
                docInput = 'Document';
                break;

            case 'vehicle-file-uploader':
                docInput = 'VehiclePhoto';
                break;

            case 'gold-file-uploader':
                docInput = 'GoldPhoto';
                break;

            case 'income-tax-file-uploader':
                docInput = 'IncomeTax';
                break;

            default:
                docInput = 'None';
        }

        // $('#document-file-uploader-error').addClass('d-none');
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0) {
            const uploadFile = this.files[0];

            // Upload File
            if (IsValidFile(docInput, uploadFile)) {
                let reader = new FileReader();

                reader.onload = function (e) {
                    if (docInput === 'VehiclePhoto') {
                        $('#vehicle-file-uploader-image-preview').attr('src', e.target.result);
                    }
                    else if (docInput === 'Document') {
                        $('#document-file-uploader-image-preview').attr('src', e.target.result);
                    }
                    else if (docInput === 'GoldPhoto') {
                        $('#gold-file-uploader-image-preview').attr('src', e.target.result);
                    }
                    else if (docInput === 'IncomeTax') {
                        $('#income-tax-file-uploader-image-preview').attr('src', e.target.result);
                    }

                }

                reader.readAsDataURL(uploadFile);
            }
        }
    });

    // Validate Uploaded File
    function IsValidFile(_inputSource, _uploadFile) {
        let result = true;

        isChangedPhoto = true;

        if (_uploadFile) {
            // .pop(): - Removes and returns the last element from the array, which is the file extension in this case.
            const fileName = _uploadFile.name;
            const fileSize = Math.round(_uploadFile.size / 1024);
            const fileExtension = fileName.split('.').pop().toLowerCase();

            let validDocumentFileExtensions = '';
            let maxDocumentFileSize = 0;

            let uploaderId = _inputSource.replace('Asset').toLowerCase();

            // Document
            if (_inputSource === 'Document') {
                validDocumentFileExtensions = (selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].DocumentAllowedFileFormatsForDb : selectedDocumentObject[0].DocumentAllowedFileFormatsForLocalStorage).toLowerCase().replace('.', '');
                maxDocumentFileSize = selectedDocumentObject[0].EnableDocumentUploadInDb ? selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInDb : selectedDocumentObject[0].MaximumFileSizeForDocumentUploadInLocalStorage;

                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                    $('#document-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + 'Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#document-file-uploader-error').removeClass('d-none');

                    $('#document-file-uploader-image-preview').attr('src', '#');
                    $('#document-file-uploader').val('');

                    result = false;
                }
            }
            else if (_inputSource === 'VehiclePhoto') {
                uploaderId = 'vehicle';

                validDocumentFileExtensions = enablePhotoUploadInLocalStorage ? allowFileFormatForLocalStorage : allowFileFormatForDb;
                maxDocumentFileSize = enablePhotoUploadInLocalStorage ? maximumFileSizeForLocalStorage : maximumFileSizeForDb;

                // Check Valid File Formats Or Size
                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                    $('#' + uploaderId + '-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + ' Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#' + uploaderId + '-file-uploader-error').removeClass('d-none');

                    $('#' + uploaderId + '-file-uploader-image-preview').attr('src', '#');
                    $('#' + uploaderId + '-file-uploader').val('');

                    result = false;
                }

            }
            else if (_inputSource === 'GoldPhoto') {
                uploaderId = 'gold';

                validDocumentFileExtensions = enablePhotoUploadInLocalStorage ? allowFileFormatForLocalStorage : allowFileFormatForDb;
                maxDocumentFileSize = enablePhotoUploadInLocalStorage ? maximumFileSizeForLocalStorage : maximumFileSizeForDb;

                // Check Valid File Formats Or Size
                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                    $('#' + uploaderId + '-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + ' Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#' + uploaderId + '-file-uploader-error').removeClass('d-none');

                    $('#' + uploaderId + '-file-uploader-image-preview').attr('src', '#');
                    $('#' + uploaderId + '-file-uploader').val('');

                    result = false;
                }

            }
            else if (_inputSource === 'IncomeTax') {
                uploaderId = 'income-tax';

                validDocumentFileExtensions = enablePhotoUploadInLocalStorage ? allowFileFormatForLocalStorage : allowFileFormatForDb;
                maxDocumentFileSize = enablePhotoUploadInLocalStorage ? maximumFileSizeForLocalStorage : maximumFileSizeForDb;

                // Check Valid File Formats Or Size
                if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                    $('#' + uploaderId + '-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + ' Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                    $('#' + uploaderId + '-file-uploader-error').removeClass('d-none');

                    $('#' + uploaderId + '-file-uploader-image-preview').attr('src', '#');
                    $('#' + uploaderId + '-file-uploader').val('');

                    result = false;
                }

            }
        }

        return result;
    }

    // ###########################  U S E R      D E F I N E D       F U N C T I O N S    ###########################   
    // Set All Required Default Values On Page Loading
    function SetPageLoadingDefaultValues() {
        let selectedSchemeId = $('#scheme-id').val();

        $('.optional-section').addClass('d-none');
        $('.optional-input').addClass('d-none');

        // Disalbe Events On Verify View
        if ($('#verify-view').length > 0) {
            isVerifyView = true;
        }

        if ($('#amend-view').length > 0) {
            isAmendView = true;
        }

        debugger;
        SetLoanTypeSetting();
        SetSchemeSetting();
        SetOccupationDetails();
        SetPolicySumInsured();
        DuplicatepolicyNumber();

        if ($('#vehicle-model-id').prop('selectedIndex') > 0) {
            SetVehicleType();
        }

        // Check Shares Deduction Change Authorization
        if (loanCustomerAccountDetailViewModel.IsAuthorizedUser) {
            $('#deducted-shares-amount').removeClass('read-only');
        }
        else {
            $('#deducted-shares-amount').addClass('read-only');
        }

        // Select Default Record, If Dropdown Has Only One Record
        listItemCount = $('#business-office-id > option').not(':first').length;

        // Select Default First Record, If Dropdown Has Only One Record
        if (listItemCount == 1) {
            $('#business-office-id').prop('selectedIndex', 1);
            $('#business-office-id').change();

            SetGeneralLedgerDropdownList();
        }

        // Enable All Services Of SMS   
        if ($('#enable-credit-transaction-sms').is(':checked') && $('#enable-debit-transaction-sms').is(':checked') && $('#enable-insufficient-balance-sms').is(':checked'))
            $('#enable-all-service').prop('checked', true);
        else
            $('#enable-all-service').prop('checked', false);

        // Enable All Services Of
        if ($('#enable-credit-transaction-email').is(':checked') && $('#enable-debit-transaction-email').is(':checked') && $('#enable-insufficient-balance-email').is(':checked') && $('#enable-statement-email').is(':checked'))
            $('#enable-all-email-service').prop('checked', true);
        else
            $('#enable-all-email-service').prop('checked', false);

        // Get Main Customer Person Dropdown
        // Get Person Dropdown
        $.get('/DynamicDropdownList/GetPersonDropdownListForLoanAccountOpening', { _schemeId: selectedSchemeId, async: false }, function (data) {
            personDropdownListData = data;
        });

        // Get Group Dropdown
        $.get('/DynamicDropdownList/GetPersonDropdownListForBusinessLoanAccountOpening', { _schemeId: selectedSchemeId, async: false }, function (data) {
            debugger;
            personDropdownListData = data;
        });

        // Get Person Dropdown For Joint Account
        $.get('/DynamicDropdownList/GetNonMemberPersonDropdownList', function (data) {
            personDropdownListDataForJointAccount = data;
        });

        // Get Person Dropdown For Nominee
        $.get('/DynamicDropdownList/GetPersonDropdownListForNominee', function (data) {
            personDropdownListDataForNominee = data;
        });

        // Get Person Dropdown For Nominee
        $.get('/DynamicDropdownList/GetPersonDropdownListForNominee', function (data) {
            debugger;
            personDropdownListDataForFamily = data;
        });

        // Get Person Dropdown For Guardian
        $.get('/DynamicDropdownList/GetPersonDropdownListForGuardian', function (data) {
            personDropdownListDataForGuardian = data;
        });

        // Check Whether Element Exist OR Not ----- Applicable For Only Amend
        if (isAmendView === true) {
            if ($('#person-id-value').length) {
                let personIdValueOnAmend = $('#person-id-value').attr('class').toString().replace('d-none', '');

                $('#person-id').val(personIdValueOnAmend);
                SetStandingInstructionDropdownList();
                DepositAccountDropdownList();
            }

            vehicleCompanyEditedId = $('#vehicle-make-id option:selected').val();
            vehicleModelEditedId = $('#vehicle-model-id option:selected').val();
            vehicleVariantEditedId = $('#vehicle-variant-id option:selected').val();

            educationalCourseEditedId = $('#educational-course-id option:selected').val();
            instituteEditedId = $('#institude-id option:selected').val();
        }

        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();
    }

    //Set Scheme Setting
    function SetSchemeSetting() {
        debugger;
        schemeId = $('#scheme-id option:selected').val();

        // Input Visiblity Base On Selected Scheme
        $.get('/AccountChildAction/GetLoanSchemeDetailBySchemeId', { _schemeId: schemeId, async: false }, function (data) {
            debugger;
            if (data) {
                // Global Value Assignment
                sysNameOfLoanType = data.SchemeLoanAccountParameterViewModel.SysNameOfLoanType;
                objSchemeLoanAccountParameter = data.SchemeLoanAccountParameterViewModel;
                schemePrmkey = data.SchemeLoanAccountParameterViewModel.SchemePrmKey;
                objSchemeNewVehicleParameter = data.SchemeVehicleTypeLoanParameterViewModel;
                objSchemePreOwnedVehicleParameter = data.SchemePreownedVehicleLoanParameterViewModels;

                if (data.SchemeEducationLoanParameterViewModel != null || data.SchemeEducationLoanParameterViewModel != null) {
                    isApplicableAllUniversities = data.SchemeEducationLoanParameterViewModel.IsApplicableAllUniversities;
                    isApplicableAllCourse = data.SchemeEducationLoanParameterViewModel.IsApplicableAllCourse;
                }

                // Enable Borrowing Detail
                if (data.SchemeLoanAccountParameterViewModel.EnableBorrowingDetail === true) {
                    $('#borrowing-detail-card').removeClass('d-none');
                }
                else {
                    $('#borrowing-detail-card').addClass('d-none');
                }

                // Enable Deposit As Collateral
                if (data.SchemeLoanAccountParameterViewModel.EnableDepositAsCollateral === false) {
                    $('#loan-against-deposit-collateral-detail-card').addClass('d-none');
                }

                // Enable Additional Income Detail
                if (data.SchemeLoanAccountParameterViewModel.EnableAdditionalIncomeDetail === true) {
                    $('#income-details-card').removeClass('d-none');
                }
                else {
                    $('#income-details-card').addClass('d-none');
                }

                // Enable Court Case Detail
                if (data.SchemeLoanAccountParameterViewModel.EnableCourtCaseDetail === true) {
                    $('#court-case-card').removeClass('d-none');
                }
                else {
                    $('#court-case-card').addClass('d-none');
                }

                // Enable Income Tax Detail
                if (data.SchemeLoanAccountParameterViewModel.EnableIncomeTaxDetail === true) {
                    $('#income-tax-card').removeClass('d-none');
                }
                else {
                    $('#income-tax-card').addClass('d-none');
                }

                // Enable Income Tax Detail   
                if (data.SchemeLoanAccountParameterViewModel.EnableIncomeTaxDetail === true) {
                    $('#income-tax-card').removeClass('d-none');
                    enablePhotoUploadInLocalStorage = personInformationParameterViewModel[`EnableIncomeTaxDocumentUploadInLocalStorage`];

                    // Get File Formats And File Size By Storage
                    allowFileFormatForLocalStorage = personInformationParameterViewModel[`IncomeTaxDocumentAllowedFileFormatsForLocalStorage`].toLowerCase().replace('.', '');;
                    maximumFileSizeForLocalStorage = personInformationParameterViewModel[`MaximumFileSizeForIncomeTaxDocumentUploadInLocalStorage`];
                    allowFileFormatForDb = personInformationParameterViewModel[`IncomeTaxDocumentAllowedFileFormatsForDb`].toLowerCase().replace('.', '');;
                    maximumFileSizeForDb = personInformationParameterViewModel[`MaximumFileSizeForIncomeTaxDocumentUploadInDb`];
                }
                else {
                    $('#income-tax-card').addClass('d-none');
                }

                // Clear Tenure
                $('#year, #month, #day').val('');

                // Tenure Stop On Verify View
                if (isVerifyView === false) {
                    if (data.SchemeAccountParameterViewModel.EnableTenure) {
                        let minimumTenure = parseInt(data.SchemeTenureViewModel.MinimumTenure);
                        let maximumTenure = parseInt(data.SchemeTenureViewModel.MaximumTenure);
                        let timePeriodUnitPrmKey = parseInt(data.SchemeTenureViewModel.TimePeriodUnitPrmKey);

                        // Add Sys Name Of Time Period Unit
                        $.get('/AccountChildAction/GetTimePeriodUnitSysNameByPrmKey', { _timePeriodUnitPrmKey: timePeriodUnitPrmKey, async: false }, function (data1) {
                            // Time Period Unit - Day
                            if (data1 === 'Day') {
                                $('#day').attr('min', minimumTenure);
                                $('#day').attr('max', maximumTenure);
                                $('#day').attr('type', 'number');
                                $('#day').removeClass('read-only');

                                maximumTenureIdDays = maximumTenure;

                                $('#month').val(0);
                                $('#month').removeAttr('type');
                                $('#month').addClass('read-only');

                                $('#year').val(0);
                                $('#year').removeAttr('type');
                                $('#year').addClass('read-only');
                            }

                            // Time Period Unit - Month
                            if (data1 === 'Month') {
                                $('#day').attr('min', 0);
                                $('#day').attr('max', 31);
                                $('#day').attr('type', 'number');
                                $('#day').removeClass('read-only');

                                $('#month').attr('min', minimumTenure);
                                $('#month').attr('max', maximumTenure);
                                $('#month').attr('type', 'number');
                                $('#month').removeClass('read-only');

                                maximumTenureIdDays = maximumTenure * 31;

                                $('#year').val(0);
                                $('#year').removeAttr('type');
                                $('#year').addClass('read-only');
                            }

                            // Time Period Unit - Year
                            if (data1 === 'Year') {
                                $('#day').attr('min', 0);
                                $('#day').attr('max', 31);
                                $('#day').attr('type', 'number');
                                $('#day').removeClass('read-only');

                                $('#month').attr('min', 1);
                                $('#month').attr('max', 12);
                                $('#month').attr('type', 'number');
                                $('#month').removeClass('read-only');

                                $('#year').attr('min', minimumTenure);
                                $('#year').attr('max', maximumTenure);
                                $('#year').attr('type', 'number');
                                $('#year').removeClass('read-only');

                                maximumTenureIdDays = maximumTenure * 366;
                            }
                        });
                    }
                }

                SetTenure();

                // Auto Account Number
                if (data.SchemeCustomerAccountNumberViewModel.EnableAutoAccountNumber === true) {
                    $('#acc-number-input').addClass('d-none');
                }
                else {
                    $('#acc-number-input').removeClass('d-none');
                }

                // Auto Application Number
                if (data.SchemeAccountParameterViewModel.EnableApplication === true) {
                    if (data.SchemeApplicationParameterViewModel.EnableAutoApplicationNumber === true) {
                        $('#application-number-input').addClass('d-none');
                    }
                    else {
                        $('#application-number-input').removeClass('d-none');
                    }
                }
                else {
                    $('#application-number-input').addClass('d-none');
                }

                // Agreement Number 
                if (data.SchemeLoanAccountParameterViewModel.EnableAgreementNumber === true) {
                    if (data.SchemeLoanAgreementNumberViewModel.EnableAutoAgreementNumber === true) {
                        $('#agreement-number-input').addClass('d-none');
                    }
                    else {
                        $('#agreement-number-input').removeClass('d-none');
                    }
                }
                else {
                    $('#agreement-number-input').addClass('d-none');
                }

                // Account Number 2
                if (data.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 === true) {
                    $('#account-number2').removeClass('d-none');
                }
                else {
                    $('#account-number2').addClass('d-none');
                }

                // Account Number 3
                if (data.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 === true) {
                    $('#account-number3').removeClass('d-none');
                }
                else {
                    $('#account-number3').addClass('d-none');
                }

                // Passbook
                if (data.SchemeAccountParameterViewModel.EnablePassbookDetail === true) {
                    if (data.SchemePassbookViewModel.EnableAutoPassbookNumber === true) {
                        $('#passbook-number-input').addClass('d-none');
                    }
                    else {
                        $('#passbook-number-input').removeClass('d-none');
                    }
                }
                else {
                    $('#passbook-number-input').addClass('d-none');
                }

                // Shares Deduction Visiblity
                if (parseFloat(objSchemeLoanAccountParameter.SharesRatioWithLoan) > 0) {
                    $('#shares-amount-input').removeClass('d-none');
                }
                else {
                    $('#shares-amount-input').addClass('d-none');
                }

                // Joint Account
                if (data.SchemeAccountParameterViewModel.MaximumJointAccountHolder == 0) {
                    $('.joint-account').addClass('d-none');
                }
                else {
                    $('.joint-account').removeClass('d-none');
                    minimumJointAccountHolder = data.SchemeAccountParameterViewModel.MinimumJointAccountHolder;
                    maximumJointAccountHolder = data.SchemeAccountParameterViewModel.MaximumJointAccountHolder;
                }

                // Nominee
                if (data.SchemeAccountParameterViewModel.MaximumNominee == 0) {
                    $('.account-nominee').addClass('d-none');
                }
                else {
                    $('.account-nominee').removeClass('d-none');
                    minimumNominee = data.SchemeAccountParameterViewModel.MinimumNominee;
                    maximumNominee = data.SchemeAccountParameterViewModel.MaximumNominee;
                }

                objSchemeDocumentViewModel = data.SchemeDocumentViewModel;

                // $.grep() - Nothing But Work As Filter; It Return Data Only Meet A Condition
                requiredDocumentObj = $.grep(data.SchemeDocumentViewModel, function (element) { return element.IsRequired });

                // map() - creates a new array from calling a function for every array element.
                //          - does not execute the function for empty elements.
                //          - does not change the original array.

                requiredDocumentArray = requiredDocumentObj.map(function (id) { return id.DocumentId; });

                // Document Upload
                if (data.SchemeAccountParameterViewModel.EnableDocumentUpload) {
                    $('#document-card').removeClass('d-none');
                }
                else {
                    $('#document-card').addClass('d-none');
                }

                // SMS Service
                if (data.SchemeAccountParameterViewModel.EnableSmsService === false)
                    $('.customer-account-sms-service').addClass('d-none');
                else
                    $('.customer-account-sms-service').removeClass('d-none');

                // Email Service
                if (data.SchemeAccountParameterViewModel.EnableEmailService === false)
                    $('.customer-account-email-service').addClass('d-none');
                else
                    $('.customer-account-email-service').removeClass('d-none');

                // Notice Schedule
                if (data.SchemeAccountParameterViewModel.EnableSmsService === false && data.SchemeAccountParameterViewModel.EnableEmailService == false)
                    $('#notice-schedule-card').addClass('d-none');
                else
                    $('#notice-schedule-card').removeClass('d-none');


                // === LOAN ACCOUNT PARAMETER  ===

                // Enable Guarantor Detail
                if (data.SchemeLoanAccountParameterViewModel.EnableGuarantorDetail == false)
                    $('#guarantor-detail-card').addClass('d-none');
                else
                    $('#guarantor-detail-card').removeClass('d-none');

                // Guarantors
                if (data.SchemeLoanAccountParameterViewModel.MaximumNumberOfGuarantors == 0) {
                    $('#guarantor-detail-card').addClass('d-none');
                }
                else {
                    $('#guarantor-detail-card').removeClass('d-none');
                    minimumNumberOfGuarantor = data.SchemeLoanAccountParameterViewModel.MinimumNumberOfGuarantors;
                    maximumNumberOfGuarantor = data.SchemeLoanAccountParameterViewModel.MaximumNumberOfGuarantors;
                }

                let sanctionAmountMin = data.SchemeLoanAccountParameterViewModel.MinimumLoanAmountForIndividual;
                let sanctionAmountMax = data.SchemeLoanAccountParameterViewModel.MaximumLoanAmountForIndividual;

                $('#sanction-amount').attr({ 'min': sanctionAmountMin, 'max': sanctionAmountMax });

                $('#demand-amount').attr({ 'min': sanctionAmountMin, 'max': sanctionAmountMax });

                //Added By Dhanashri Wagh 12/09/2024 (EnableSWOTAnalysis,EnablePastCreditHistory,EnableLegalAndRegulatoryCompliance)
                // Swot Analysis Input Visibility
                if (data.SchemeLoanAccountParameterViewModel.EnableSWOTAnalysis) {
                    $('#swot-analysis-input').removeClass('d-none');
                    $('#strengths-factors, #weaknesses-factors, #opportunities-factors, #threats-factors').attr('minLength', data.SchemeLoanAccountParameterViewModel.SWOTAnalysisMinimumLength)
                }
                else {
                    $('#swot-analysis-input').addClass('d-none');
                }

                // Past Credit History Visibility
                if (data.SchemeLoanAccountParameterViewModel.EnablePastCreditHistory) {
                    $('#credit-history-input').removeClass('d-none');
                    $('#past-credit-history').attr('minLength', data.SchemeLoanAccountParameterViewModel.PastCreditHistoryMinimumLength)
                }
                else {
                    $('#credit-history-input').addClass('d-none');
                }

                // Legal And Regulatory Compliance Visibility
                if (data.SchemeLoanAccountParameterViewModel.EnableLegalAndRegulatoryCompliance) {
                    $('#legal-regulatory-compliance-input').removeClass('d-none');
                    $('#legal-and-regulatory-compliance').attr('minLength', data.SchemeLoanAccountParameterViewModel.LegalAndRegulatoryComplianceMinimumLength)
                }
                else {
                    $('#legal-regulatory-compliance-input').addClass('d-none');
                }

                let rateOfInterestMin = data.SchemeLoanInterestParameterViewModel.MinimumInterestRate;
                let rateOfInterestMax = data.SchemeLoanInterestParameterViewModel.MaximumInterestRate;

                $('#rate-of-interest').attr({ 'min': rateOfInterestMin, 'max': rateOfInterestMax });

                // Enable Field Investigation
                if (data.SchemeLoanAccountParameterViewModel.EnableFieldInvestigation === false) {
                    $('#field-investigation-card').addClass('d-none');
                }
                else {
                    $('#field-investigation-card').removeClass('d-none');
                }

                // Debt To Income Ratio
                if (data.SchemeLoanAccountParameterViewModel.EnableCaptureDebtToIncomeRatio === false) {
                    $('#debt-to-income-ratio-card').addClass('d-none');
                }
                else {
                    $('#debt-to-income-ratio-card').removeClass('d-none');
                }

                //  Acquaintance Details
                if (data.SchemeLoanAccountParameterViewModel.EnableAcquaintanceDetails === false) {
                    $('#acquaitance-detail-card').addClass('d-none');
                }
                else {
                    $('#acquaitance-detail-card').removeClass('d-none');
                }



                // Call Document Dropdown List
                SetDocumentDropdownList();

                // Call Person Dropdown List
                SetGuarantorDropdownList();

                // Business Loan
                if (sysNameOfLoanType === SHORT_TERM_BUSINESS_LOAN) {
                    debugger;
                    minimumBusinessExperience = data.SchemeBusinessLoanViewModel.MinimumBusinessExperience;
                    minimumTurnOverAmount = data.SchemeBusinessLoanViewModel.MinimumTurnOverAmount;
                    capturePreviousProfitMakingYears = data.SchemeBusinessLoanViewModel.CapturePreviousProfitMakingYears;

                    for (let i = 0; i <= parseInt(capturePreviousProfitMakingYears); i++) {
                        $("#profit-group-" + i).removeClass("d-none");
                    }

                    SetPersonGroupDropdownList();
                }
                else {
                    SetPersonDropdownList();
                }

                // CASH CREDIT
                if (sysNameOfLoanType === CASH_CREDIT_LOAN) {
                    debugger;
                    if (data.SchemeCashCreditLoanParameterViewModel.EnableFixedDepositAsCollateral === false) {
                        $('#loan-against-deposit-collateral-detail-card').addClass('d-none');
                    }
                    else {
                        $('#loan-against-deposit-collateral-detail-card').removeClass('d-none');
                    }

                    if (data.SchemeCashCreditLoanParameterViewModel.CapturePreviousYearTurnOver === NOT_REQUIRED) {
                        $('#previous-year-turnover-input').addClass('d-none');
                    }
                    else {
                        $('#previous-year-turnover-input').removeClass('d-none');
                    }

                    if (data.SchemeCashCreditLoanParameterViewModel.CapturePreviousSecondYearTurnOver === NOT_REQUIRED) {
                        $('#second-year-turnover-input').addClass('d-none');
                    }
                    else {
                        $('#second-year-turnover-input').removeClass('d-none');
                    }

                    if (data.SchemeCashCreditLoanParameterViewModel.CapturePreviousThirdYearTurnOver === NOT_REQUIRED) {
                        $('#third-year-turnover-input').addClass('d-none');
                    }
                    else {
                        $('#third-year-turnover-input').removeClass('d-none');
                    }

                    if (data.SchemeCashCreditLoanParameterViewModel.CapturePreviousYearGrossProfitMargin === NOT_REQUIRED) {
                        $('#gross-profit-margin-input').addClass('d-none');
                    }
                    else {
                        $('#gross-profit-margin-input').removeClass('d-none');
                    }

                    if (data.SchemeCashCreditLoanParameterViewModel.CapturePreviousSecondYearGrossProfitMargin === NOT_REQUIRED) {
                        $('#second-year-gross-profit-margin-input').addClass('d-none');
                    }
                    else {
                        $('#second-year-gross-profit-margin-input').removeClass('d-none');
                    }

                    if (data.SchemeCashCreditLoanParameterViewModel.CapturePreviousThirdYearGrossProfitMargin === NOT_REQUIRED) {
                        $('#third-year-gross-profit-margin-input').addClass('d-none');
                    }
                    else {
                        $('#third-year-gross-profit-margin-input').removeClass('d-none');
                    }

                    if (data.SchemeCashCreditLoanParameterViewModel.CapturePreviousYearNetProfitMargin === NOT_REQUIRED) {
                        $('#net-profit-margin-input').addClass('d-none');
                    }
                    else {
                        $('#net-profit-margin-input').removeClass('d-none');
                    }

                    if (data.SchemeCashCreditLoanParameterViewModel.CapturePreviousSecondYearNetProfitMargin === NOT_REQUIRED) {
                        $('#second-year-net-profit-margin-input').addClass('d-none');
                    }
                    else {
                        $('#second-year-net-profit-margin-input').removeClass('d-none');
                    }

                    if (data.SchemeCashCreditLoanParameterViewModel.CapturePreviousThirdYearNetProfitMargin === NOT_REQUIRED) {
                        $('#third-year-net-profit-margin-input').addClass('d-none');
                    }
                    else {
                        $('#third-year-net-profit-margin-input').removeClass('d-none');
                    }
                }

                // Consumer Loan
                if (sysNameOfLoanType === CONSUMER_DURABLE_LOAN) {
                    GetConsumerDurableItemDropdownList();

                }

                //Gold Loan
                if (sysNameOfLoanType === GOLD_LOAN) {

                    $('#gold-loan-collateral-detail-card').removeClass('d-none');

                    if (data.SchemeGoldLoanParameterViewModel.EnableGoldPhoto == true) {
                        $('#gold-collateral-photo-card').removeClass('d-none');

                        minimumGoldPhoto = data.SchemeGoldLoanParameterViewModel.MinimumGoldPhoto;
                        maximumGoldPhoto = data.SchemeGoldLoanParameterViewModel.MaximumGoldPhoto;
                        allowFileFormatForDb = data.SchemeGoldLoanParameterViewModel.GoldPhotoAllowedFileFormatsForDb;
                        maximumFileSizeForDb = data.SchemeGoldLoanParameterViewModel.MaximumFileSizeForGoldPhotoUploadInDb;
                        allowFileFormatForLocalStorage = data.SchemeGoldLoanParameterViewModel.GoldPhotoAllowedFileFormatsForLocalStorage;
                        maximumFileSizeForLocalStorage = data.SchemeGoldLoanParameterViewModel.MaximumFileSizeForGoldPhotoUploadInLocalStorage;
                        enablePhotoUploadInLocalStorage = data.SchemeGoldLoanParameterViewModel.EnableGoldPhotoUploadInLocalStorage;
                    }
                }

                // VEHICLE LOAN
                if (sysNameOfLoanType === VEHICLE_LOAN) {
                    debugger;
                    if (isVerifyView === false) {
                        SetVehicleCompanyDropdownList();
                    }

                    // Check Pre-owned Vehicle Loan Type Is Available
                    if (data.SchemePreownedVehicleLoanParameterViewModels.length > 0) {
                        $('.purchace-new').removeClass('d-none');
                        $('.purchace-pre').removeClass('d-none');
                        $('.purchace-tov').removeClass('d-none');
                    }
                    else {
                        $('.purchace-new').removeClass('d-none');
                        $('.purchace-pre').addClass('d-none');
                        $('.purchace-tov').addClass('d-none');
                    }
                }

                //Education Loan
                if (sysNameOfLoanType === EDUCATION_LOAN) {
                    if (isVerifyView === false) {
                        EducationalCourseDropdownList();
                        InstituteDropdownList();
                    }
                }
            }
            else {
                $('#scheme-id-error').removeClass('d-none');
            }
        });
    }

    //Set Loan Type Setting
    function SetLoanTypeSetting() {
        let loanTypeId = $('#loan-type-id option:selected').val();

        // Assuming nameOfScheme is defined somewhere
        $.get('/AccountChildAction/GetLoanTypeSysNameByLoanTypeId', { _loanTypeId: loanTypeId, async: false }, function (data) {
            debugger;
            let loanType = data

            // Business Loan
            if (loanType === SHORT_TERM_BUSINESS_LOAN) {
                $('#business-loan-detail-card').removeClass('d-none');
                $('#minute-of-meeting-agenda-input').removeClass('d-none');
            }

            // Cash Credit Loan
            if (loanType === CASH_CREDIT_LOAN) {
                $('#cash-credit-loan-account-card').removeClass('d-none');
                $('#minute-of-meeting-agenda-input').removeClass('d-none');
            }

            // Consumer Loan
            if (loanType === CONSUMER_DURABLE_LOAN) {
                $('#consumer-loan-detail-card').removeClass('d-none');
                $('#minute-of-meeting-agenda-input').removeClass('d-none');
            }

            // Education Loan
            if (loanType === EDUCATION_LOAN) {
                $('#educational-loan-detail-card').removeClass('d-none');
                $('#minute-of-meeting-agenda-input').removeClass('d-none');
            }

            // Employee Loan
            if (loanType === EMPLOYEE_LOAN) {
                $('#minute-of-meeting-agenda-input').removeClass('d-none');
            }

            // Guarantor Loan
            if (loanType === GUARANTOR_LOAN) {
                $('#minute-of-meeting-agenda-input').removeClass('d-none');
            }

            // Personal Loan
            if (loanType === PERSONAL_LOAN) {
                $('#minute-of-meeting-agenda-input').removeClass('d-none');
            }

            // Personal Loan
            if (loanType === LOAN_AGAINST_DEPOSIT) {
                $('#loan-against-deposit-collateral-detail-card').removeClass('d-none');
            }

            // Salary Loan
            if (loanType === SALARY_LOAN) {
                $('#minute-of-meeting-agenda-input').removeClass('d-none');
            }

            // Loan Against Property
            if (loanType === LOAN_AGAINTS_PROPERTY || loanType === HOME_LOAN) {
                debugger;
                $('#loan-against-property-collateral-card').removeClass('d-none');
                $('#minute-of-meeting-agenda-input').removeClass('d-none');
                if (loanType == LOAN_AGAINTS_PROPERTY) {
                    $('#add-text-property').html();
                    $('#add-text-property').removeClass('d-none');
                    $('#add-text-home').addClass('d-none');
                }
                else if (loanType === HOME_LOAN) {
                    $('#add-text-home').html();
                    $('#add-text-home').removeClass('d-none');
                    $('#add-text-property').addClass('d-none');
                }
            }

            // Vehicle Loan
            if (loanType === VEHICLE_LOAN) {
                debugger;
                $('.commercial-vehicle').addClass('d-none');

                $('#vehicle-loan-section').removeClass('d-none');
                $('#minute-of-meeting-agenda-input').removeClass('d-none');

                if (isAmendView || isVerifyView) {
                    IsUsedForCommercialChangeEventFunction();
                    SetVehicleType();
                }
            }
        });
    }

    function SetGeneralLedgerDropdownList() {
        let loanTypeId = $('#loan-type-id option:selected').val();
        let businessOfficeId = $('#business-office-id option:selected').val();

        // Get Value For Only For Amend Operation
        let generalLedgerPageLoadId = $('#general-ledger-id option:selected').val();

        $.get('/DynamicDropdownList/GetLoanGeneralLedgerDropdownListByBusinessOfficeId', { _businessOfficeId: businessOfficeId, _loanTypeId: loanTypeId, async: false }, function (data) {
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000">Select General Ledger</option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#general-ledger-id').html(dropdownListItems);

            listItemCount = $('#general-ledger-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#general-ledger-id').prop('selectedIndex', 1);
                $('#general-ledger-id').change();
            }
            else {
                if (isAmendView)
                    $('#general-ledger-id').val(generalLedgerPageLoadId);
            }

            SetSchemeDropdownList();
        });
    }

    function GetConsumerDurableItemDropdownList() {
        debugger;
        $.get('/DynamicDropdownList/GetConsumerDurableLoanItemDropdownListBySchemePrmKey', { _schemePrmKey: schemePrmkey, async: false }, function (data) {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Consumer Durable Item --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#consumer-durable-item-id').html(dropdownListItems);

            listItemCount = $('#consumer-durable-item-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#consumer-durable-item-id').prop('selectedIndex', 1);
            }
        });


    }

    function SetSchemeDropdownList() {
        debugger
        let selectedGeneralLedgerId = $('#general-ledger-id option:selected').val();

        // Get Value For Only For Amend Operation
        let schemePageLoadId = $('#scheme-id option:selected').val();

        // Set Scheme Dropdown List Based On Selected General Ledger
        $.get('/DynamicDropdownList/GetSchemeDropdownListByGeneralLedger', { _generalLedgerId: selectedGeneralLedgerId, async: false }, function (data) {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000">--- Select Scheme --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#scheme-id').html(dropdownListItems);

            listItemCount = $('#scheme-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#scheme-id').prop('selectedIndex', 1);
                $('#scheme-id').change();
            }
            else {
                // Set Value On Page Loading
                if (isAmendView)
                    $('#scheme-id').val(schemePageLoadId);
            }
        });
    }

    function SetGuarantorDropdownList() {
        schemeId = $('#scheme-id option:selected').val();

        // Get Guarantor Dropdownlist
        $.get('/DynamicDropdownList/GetGuarantorDropdownList', { _schemeId: schemeId, async: false }, function (data) {
            guarantorDropdownListData = data;
        });

    }

    // Other Than Business Loan
    function SetPersonDropdownList() {
        schemeId = $('#scheme-id option:selected').val();

        // Set PersonDropdownList Based On Scheme
        $.get('/DynamicDropdownList/GetPersonDropdownListForLoanAccountOpening', { _schemeId: schemeId, async: false }, function (data) {
            personDropdownListData = data;

            if (isAmendView) {
                if ($('#enable-auto-debit').is(':checked') === true) {
                    SetStandingInstructionDropdownList();
                }
            }
        });
    }

    // For Only Business Loan
    function SetPersonGroupDropdownList() {
        schemeId = $('#scheme-id option:selected').val();

        // Set PersonDropdownList Based On Scheme
        $.get('/DynamicDropdownList/GetPersonDropdownListForBusinessLoanAccountOpening', { _schemeId: schemeId, async: false }, function (data) {
            personDropdownListData = data;

            if (isAmendView) {
                if ($('#enable-auto-debit').is(':checked') === true) {
                    SetStandingInstructionDropdownList();
                }
            }
        });
    }
    function SetDocumentDropdownList() {
        let schemeId = $('#scheme-id option:selected').val();

        let text = '';
        let classText = '';

        debugger;
        $.get('/DynamicDropdownList/GetDocumentDropdownListBySchemeId', { _schemeId: schemeId, async: false }, function (data) {
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000">Select Valid Document</option>';
            debugger;

            $.each(data, function (index, selectListItemObj) {
                debugger;

                // jquery's inArray operator used to get keys in an object.
                // Use '|' Mandatory Marks To Indicate Required
                if ($.inArray(selectListItemObj.Value, requiredDocumentArray) > -1) {
                    text = '| ' + selectListItemObj.Text;
                    classText = 'text-danger';
                }
                else {
                    text = selectListItemObj.Text;
                    classText = '';
                }

                dropdownListItems += '<option class="' + classText + ' ' + '" value="' + selectListItemObj.Value + '">' + text + '</option>';
            });

            $('#document-id').html(dropdownListItems);

            documentDropdownList = $('#document-id').html();

            listItemCount = $('#document-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#document-id').prop('selectedIndex', 1);
                $('#document-id').change();
            }
        });
    }

    // Set All Personal Details Based On Selected Person
    function SetPersonData() {
        debugger;
        // Change Setting If Person Actually Changed
        if (selectedPersonId != prevPersonId) {
            // Clear
            if (prevPersonId != '')
                $('#person-change-info').removeClass('d-none');

            // Clear Related Datatables
            addressDataTable.clear().draw();
            borrowingDetailDataTable.clear().draw();
            contactDataTable.clear().draw();
            courtCaseDataTable.clear().draw();
            incomeDatatable.clear().draw();
            incomeTaxDataTable.clear().draw();
            jointAccountDataTable.clear().draw();
            nomineeDataTable.clear().draw();

            // Add Contact Details Of Selected Person
            $.get('/PersonChildAction/GetContactDetailByPersonId', { _personId: selectedPersonId, async: false }, function (data) {
                rowNum = 0;

                $.each(data, function (index, personContactDetail) {
                    isMobile = personContactDetail.NameOfContactType.includes('Mobile');
                    isEmail = personContactDetail.NameOfContactType.includes('Email');

                    if (isMobile || isEmail) {
                        tag = '<input type="checkbox" name="check_all" class="checks"/>';

                        row = contactDataTable.row.add([
                            tag,
                            personContactDetail.ContactTypeId,
                            personContactDetail.NameOfContactType,
                            personContactDetail.FieldValue,
                            personContactDetail.IsVerified,
                            personContactDetail.VerificationCode,
                            personContactDetail.Note,
                            personContactDetail.ReasonForModification,
                            personContactDetail.PersonContactDetailPrmKey,
                            personContactDetail.CustomerAccountPrmKey
                        ]).draw();

                        rowNum++;

                        row.nodes().to$().attr('id', 'tr' + rowNum);

                        contactDataTable.column(1).visible(false);
                        contactDataTable.column(7).visible(false);
                        contactDataTable.column(8).visible(false);
                        contactDataTable.column(9).visible(false);

                        contactDataTable.columns.adjust().draw();
                    }
                });
            });

            // Add Address Details Of Selected Person
            $.get('/PersonChildAction/GetAddressDetailByPersonId', { _personId: selectedPersonId, async: false }, function (data) {
                rowNum = 0;
                $.each(data, function (index, personAddressDetail) {
                    tag = '<input id="select-all-person-address" class="checks" type="checkbox" name="check_all"/>';
                    row = addressDataTable.row.add([
                        tag,
                        personAddressDetail.AddressTypeId,
                        personAddressDetail.NameOfAddressType,
                        personAddressDetail.FlatDoorNo,
                        personAddressDetail.TransFlatDoorNo,
                        personAddressDetail.NameOfBuilding,
                        personAddressDetail.TransNameOfBuilding,
                        personAddressDetail.NameOfRoad,
                        personAddressDetail.TransNameOfRoad,
                        personAddressDetail.NameOfArea,
                        personAddressDetail.TransNameOfArea,
                        personAddressDetail.CityId,
                        personAddressDetail.NameOfCenter,
                        personAddressDetail.ResidenceTypeId,
                        personAddressDetail.NameOfResidenceType,
                        personAddressDetail.OwnershipTypeId,
                        personAddressDetail.NameOfOwnershipType,
                        false,
                        personAddressDetail.Note,
                        personAddressDetail.TransNote,
                        personAddressDetail.ReasonForModification,
                        personAddressDetail.PersonAddressPrmKey,
                        personAddressDetail.CustomerAccountPrmKey,
                    ]).draw();

                    rowNum++;
                    row.nodes().to$().attr('id', 'tr' + rowNum);

                    addressDataTable.column(1).visible(false);
                    addressDataTable.column(11).visible(false);
                    addressDataTable.column(13).visible(false);
                    addressDataTable.column(15).visible(false);
                    addressDataTable.column(20).visible(false);
                    addressDataTable.column(21).visible(false);
                    addressDataTable.column(22).visible(false);

                    addressDataTable.columns.adjust().draw();
                    $('#address-type-id').find("option[value='" + personAddressDetail.AddressTypeId + "']").hide();

                });
            });

            // Add Additional Income Details Of Selected Person
            $.get('/PersonChildAction/GetAdditionalIncomeDetailByPersonId', { _personId: selectedPersonId, async: false }, function (data) {
                rowNum = 0;
                $.each(data, function (index, personAdditionalIncomeDetail) {
                    tag = '<input id="select-all-income-detail" class="checks" type="checkbox" name="check_all"/>';
                    row = incomeDatatable.row.add([
                        tag,
                        personAdditionalIncomeDetail.IncomeSourceId,
                        personAdditionalIncomeDetail.NameOfIncomeSource,
                        personAdditionalIncomeDetail.AnnualIncome,
                        personAdditionalIncomeDetail.OtherDetails,
                        personAdditionalIncomeDetail.Note,
                        personAdditionalIncomeDetail.ReasonForModification,
                        personAdditionalIncomeDetail.PersonAdditionalIncomeDetailPrmKey,
                        personAdditionalIncomeDetail.CustomerAccountPrmKey,
                    ]).draw();
                    rowNum++;
                    row.nodes().to$().attr('id', 'tr' + rowNum);
                    incomeDatatable.column(1).visible(false);
                    incomeDatatable.column(6).visible(false);
                    incomeDatatable.column(7).visible(false);
                    incomeDatatable.column(8).visible(false);

                    incomeDatatable.columns.adjust().draw();
                    $('#income-source-id').find("option[value='" + personAdditionalIncomeDetail.IncomeSourceId + "']").hide();

                });
            });

            // Add court case Details Of Selected Person
            $.get('/PersonChildAction/GetCourtCaseByPersonId', { _personId: selectedPersonId, async: false }, function (data) {
                rowNum = 0;
                $.each(data, function (index, personCourtCase) {
                    tag = '<input id="select-all-court-case" class="checks" type="checkbox" name="check_all"/>';
                    row = courtCaseDataTable.row.add([
                        tag,
                        personCourtCase.CourtCaseTypeId,
                        personCourtCase.NameOfCourtCaseType,
                        GetInputDateFormat(new Date(parseInt(personCourtCase.FilingDate.substr(6)))),
                        personCourtCase.FilingNumber,
                        GetInputDateFormat(new Date(parseInt(personCourtCase.RegistrationDate.substr(6)))),
                        personCourtCase.RegistrationNumber,
                        personCourtCase.CNRNumber,
                        personCourtCase.AmountOfDecree,
                        personCourtCase.CollateralAmount,
                        personCourtCase.CourtCaseStageId,
                        personCourtCase.NameOfCourtCaseStage,
                        personCourtCase.Note,
                        personCourtCase.ReasonForModification,
                        personCourtCase.PersonCourtCasePrmKey,
                        personCourtCase.CustomerAccountPrmKey,
                    ]).draw();
                    rowNum++;
                    row.nodes().to$().attr('id', 'tr' + rowNum);
                    courtCaseDataTable.column(1).visible(false);
                    courtCaseDataTable.column(10).visible(false);
                    courtCaseDataTable.column(13).visible(false);
                    courtCaseDataTable.column(14).visible(false);
                    courtCaseDataTable.column(15).visible(false);
                    $('#court-case-types-id').find("option[value='" + personCourtCase.CourtCaseTypeId + "']").hide();

                });
            });

            // Add Income Tax Details Of Selected Person
            $.get('/PersonChildAction/GetIncomeTaxDetailByPersonId', { _personId: selectedPersonId, async: false }, function (data) {
                rowNum = 0;
                $.each(data, function (index, personIncomeTaxDetail) {
                    tag = '<input id="select-all-income-tax" class="checks" type="checkbox" name="check_all"/>';
                    row = incomeTaxDataTable.row.add([
                        tag,
                        personIncomeTaxDetail.AssessmentYear,
                        personIncomeTaxDetail.TaxAmount,
                        personIncomeTaxDetail.PhotoPathTax,
                        personIncomeTaxDetail.Photo,
                        personIncomeTaxDetail.FileCaption,
                        personIncomeTaxDetail.Note,
                        personIncomeTaxDetail.ReasonForModification,
                        personIncomeTaxDetail.NameOfFile,
                        personIncomeTaxDetail.PersonIncomeTaxDetailDocumentPrmKey,
                        personIncomeTaxDetail.LocalStoragePath,
                        personIncomeTaxDetail.PersonIncomeTaxDetailPrmKey,
                        personIncomeTaxDetail.CustomerAccountPrmKey,
                    ]).draw();
                    rowNum++;
                    row.nodes().to$().attr('id', 'tr' + rowNum);
                    incomeTaxDataTable.column(7).visible(false);
                    incomeTaxDataTable.column(8).visible(false);
                    incomeTaxDataTable.column(9).visible(false);
                    incomeTaxDataTable.column(10).visible(false);
                    incomeTaxDataTable.column(11).visible(false);
                    incomeTaxDataTable.column(12).visible(false);

                });
            });

            // Add Borrowing Details Of Selected Person
            $.get('/PersonChildAction/GetBorrowingDetailByPersonId', { _personId: selectedPersonId, async: false }, function (data) {
                debugger;
                rowNum = 0;
                $.each(data, function (index, personBorrowingDetail) {
                    debugger;
                    if (personBorrowingDetail.CloseDate !== null) {
                        debugger;
                        GetInputDateFormat(new Date(parseInt(personBorrowingDetail.CloseDate.substr(6))));
                    }
                    else {
                        personBorrowingDetail.CloseDate = '';
                    }

                    tag = '<input id="select-all-borrowing-detail" class="checks" type="checkbox" name="check_all"/>';
                    row = borrowingDetailDataTable.row.add([
                        tag,

                        personBorrowingDetail.NameOfOrganization,
                        personBorrowingDetail.TransNameOfOrganization,
                        personBorrowingDetail.Branch,
                        personBorrowingDetail.TransBranch,
                        personBorrowingDetail.ReferenceNumber,
                        GetInputDateFormat(new Date(parseInt(personBorrowingDetail.OpeningDate.substr(6)))),
                        GetInputDateFormat(new Date(parseInt(personBorrowingDetail.MatureDate.substr(6)))),
                        GetInputDateFormat(new Date(parseInt(personBorrowingDetail.CloseDate.substr(6)))),
                        personBorrowingDetail.LoanDetails,
                        personBorrowingDetail.TransLoanDetails,
                        personBorrowingDetail.MortgageDetails,
                        personBorrowingDetail.TransMortgageDetails,
                        personBorrowingDetail.MortgageAmount,
                        personBorrowingDetail.SanctionLoanAmount,
                        personBorrowingDetail.InstallmentAmount,
                        personBorrowingDetail.LoanBalanceAmount,
                        personBorrowingDetail.OverduesInstallment,
                        personBorrowingDetail.OverduesAmount,
                        personBorrowingDetail.IsTakingAnyCourtAction,
                        personBorrowingDetail.CourtCaseTypeId,
                        personBorrowingDetail.NameOfCourtCaseType,
                        GetInputDateFormat(new Date(parseInt(personBorrowingDetail.FilingDate.substr(6)))),
                        personBorrowingDetail.FilingNumber,
                        GetInputDateFormat(new Date(parseInt(personBorrowingDetail.RegistrationDate.substr(6)))),
                        personBorrowingDetail.RegistrationNumber,
                        personBorrowingDetail.CNRNumber,
                        personBorrowingDetail.CourtCaseStageId,
                        personBorrowingDetail.NameOfCourtCaseStage,
                        personBorrowingDetail.Note,
                        personBorrowingDetail.TransNote,
                        personBorrowingDetail.ReasonForModification,
                        personBorrowingDetail.PersonBorrowingDetailPrmKey,
                        personBorrowingDetail.CustomerAccountPrmKey,

                    ]).draw();
                    rowNum++;
                    row.nodes().to$().attr('id', 'tr' + rowNum);
                    borrowingDetailDataTable.column(20).visible(false);
                    borrowingDetailDataTable.column(27).visible(false);
                    borrowingDetailDataTable.column(31).visible(false);
                    borrowingDetailDataTable.column(32).visible(false);
                    borrowingDetailDataTable.column(33).visible(false);
                    $('#court-case-types-id').find("option[value='" + personBorrowingDetail.CourtCaseTypeId + "']").hide();
                });
            });

            SetStandingInstructionDropdownList();
            prevPersonId = selectedPersonId;
        }
        else {
            $('#person-change-info').addClass('d-none');
            prevPersonId = selectedPersonId;
        }
    }
    function SetVehicleCompanyDropdownList() {
        debugger;
        $.get('/DynamicDropdownList/GetVehicleCompanyDropdownListBySchemePrmKey', { _schemePrmKey: schemePrmkey, async: false }, function (data) {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Vehicle Company --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#vehicle-make-id').html(dropdownListItems);

            listItemCount = $('#vehicle-make-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#vehicle-make-id').prop('selectedIndex', 1);
            }
            else {
                if (vehicleCompanyEditedId !== '') {
                    $('#vehicle-make-id').val(vehicleCompanyEditedId);
                    SetVehicleModelDropdownList();
                }
            }
        });
    }
    function SetVehicleModelDropdownList() {
        vehicleMakeId = $('#vehicle-make-id option:selected').val();
        $.get('/DynamicDropdownList/GetVehicleModelDropdownListByVehicleMakeId', { _vehicleMakeId: vehicleMakeId, async: false }, function (data) {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Vehicle Model --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#vehicle-model-id').html(dropdownListItems);

            listItemCount = $('#vehicle-model-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#vehicle-model-id').prop('selectedIndex', 1);
            }
            else {
                if (vehicleModelEditedId !== '') {
                    $('#vehicle-model-id').val(vehicleModelEditedId);
                    SetVehicleVariantDropdownList();
                }
            }
        });
    }
    function SetVehicleVariantDropdownList() {
        debugger;
        vehicleModelId = $('#vehicle-model-id option:selected').val();

        $.get('/DynamicDropdownList/GetVehicleVariantDropdownListByVehicleModelId', { _vehicleModelId: vehicleModelId, async: false }, function (data) {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Valid Vehicle Variant --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#vehicle-variant-id').html(dropdownListItems);

            listItemCount = $('#vehicle-variant-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#vehicle-variant-id').prop('selectedIndex', 1);
            }
            else {
                if (vehicleVariantEditedId !== '') {
                    $('#vehicle-variant-id').val(vehicleVariantEditedId);
                }
            }
        });
    }
    function SetVehicleTenure() {
        debugger;
        // To Check Loan Purpose Must Be Other Than Purchase New Vehicle
        let loanPurpose = $('.loan-purpose').val();
        if (loanPurpose !== 'NEW') {
            //To Check SchemePreownedVehicleLoanParameterViewModel Is Not Null
            if (objSchemeVehicleParameter && objSchemeVehicleParameter.length > 0) {

                if (indexOfSchemeVehicleParameter !== -1) {

                    let vehicleAge1 = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].VehicleLife1;
                    let vehicleAge2 = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].VehicleLife2;
                    let vehicleAge3 = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].VehicleLife3;
                    let vehicleAge4 = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].VehicleLife4;
                    let maxTenureForVehicle;
                    let maximumLoanSanctionPercentage;
                    vehicleAge = 15; // remove later it' for testing

                    if (vehicleAge > vehicleAge4) {
                        $('#manufacture-year-tenure-error').removeClass('d-none');
                    }
                    else {
                        // Compaire Current Vehicle Age With  PreownedVehicleLoanParameter Vehicle Age And Select It's Corresponding MaximumTenure
                        if (vehicleAge <= vehicleAge1) {
                            maxTenureForVehicle = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].MaximumTenure1;
                            maximumLoanSanctionPercentage = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].MaximumLoanSanctionPercentage1;
                        } else if (vehicleAge > vehicleAge1 && vehicleAge <= vehicleAge2) {
                            maxTenureForVehicle = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].MaximumTenure2;
                            maximumLoanSanctionPercentage = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].MaximumLoanSanctionPercentage2;
                        } else if (vehicleAge > vehicleAge2 && vehicleAge <= vehicleAge3) {
                            maxTenureForVehicle = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].MaximumTenure3;
                            maximumLoanSanctionPercentage = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].MaximumLoanSanctionPercentage3;
                        } else if (vehicleAge > vehicleAge3 && vehicleAge <= vehicleAge4) {
                            maxTenureForVehicle = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].MaximumTenure4;
                            maximumLoanSanctionPercentage = objSchemeVehicleParameter[indexOfSchemeVehicleParameter].MaximumLoanSanctionPercentage4;
                        }

                        // To Compaire Current Tenure With PreownedVehicleLoanParameter's MaximumTenure
                        let currentTenureYear = $('#year').val();
                        let currentTenureMonth = $('#month').val();
                        let tenureInMonth = (currentTenureYear * 12) + currentTenureMonth;
                        //tenureInMonth = 32;

                        // If Current Tenure Is Greater Than MaximumTenure Then Display Error Msg And Clear Tenure Values
                        if (tenureInMonth > 0) {
                            if (tenureInMonth > maxTenureForVehicle) {
                                $('#vehicle-tenure-error').removeClass('d-none');
                                $('#year').val(0);
                                $('#month').val(0);
                                $('#day').val(0);
                            }
                            else {
                                $('#vehicle-tenure-error').addClass('d-none');
                            }
                        }
                        //Check MaximumLoanSanctionPercentage sanction-amount-error

                        let loanSanctionAmount = $('#sanction-amount').val();
                        let valuationAmount = $('#current-valuation-amount').val();
                        let maxLoanAmount = (maximumLoanSanctionPercentage / 100) * valuationAmount;
                        if (valuationAmount > 0) {
                            if (loanSanctionAmount > maxLoanAmount) {
                                $('#sanction-amount-error').removeClass('d-none');
                            }
                            else {
                                $('#sanction-amount-error').addClass('d-none');
                            }
                        }
                    }
                }
            }

        }
    }
    function SetStandingInstructionDropdownList() {
        // On Page Loading Amend View Assign Person Id Value
        if (isAmendView || isVerifyView)
            selectedPersonId = $('#person-id1').val();

        $.get("/DynamicDropdownList/GetDemandDepositAccountHolderDropdownListByPerson", { _personId: selectedPersonId, async: false }, function (data) {
            $('#customer-saving-account-debit').html('');

            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000">--- Select Saving Account --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#customer-saving-account-debit').append(dropdownListItems);

            listItemCount = $('#customer-saving-account-debit > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#customer-saving-account-debit').prop('selectedIndex', 1);
            }
            else {
                if (isVerifyView || isAmendView) {
                    $('#customer-saving-account-debit').val($('#debit-account-id').text());
                }
            }

            // Required To Event Execution
            isAmendView = false;
        });
    }

    $('#vehicle-variant-id').focusout(function () {
        listItemCount = $('#vehicle-supplier-id > option').not(':first').length;
        if (listItemCount == 1) {
            $('#vehicle-supplier-id').prop('selectedIndex', 1);
        }

    });

    // ###############   FUNCTIONS FOR NORMAL ACCORDION VALIDITY  

    // Sms Service Detail
    function IsValidSMSServiceDetailAccordionInputs() {
        result = true;

        if ($('#customer-account-sms-service-card').hasClass('d-none') === false) {
            if (IsValidInputDate('#activation-date-sms') == false || IsValidInputDate('#expiry-date-sms') == false)
                result = false;
        }

        if (result)
            $('#sms-service-accordion-error').addClass('d-none');
        else
            $('#sms-service-accordion-error').removeClass('d-none');

        return result;
    }

    // Email Service Detail
    function IsValidEmailServiceDetailAccordionInputs() {
        result = true;

        if ($('#customer-account-email-service-card').hasClass('d-none') === false) {
            // Activation Date
            if (IsValidInputDate('#activation-date-email') === false) {
                result = false;
            }

            // Expiry Date
            if (IsValidInputDate('#expiry-date-email') === false) {
                result = false;
            }

            // Statement Frequency
            if ($('.statement-frequency:checked').length === 0) {
                result = false;
            }
        }

        if (result)
            $('#email-service-accordion-error').addClass('d-none');
        else
            $('#email-service-accordion-error').removeClass('d-none');

        return result;
    }

    // Vehicle Loan Collateral Detail    changes done by Dhanashri  
    function IsValidVehicleLoanCollateralDetailAccordionInputs() {
        debugger;

        let manufactureYear = $('#manufacture-year').val();
        let registrationNumber = $('#vehicle-registration-number').val();
        let exShowroomPrice = parseFloat($('#ex-showroom-price').val());
        let onRoadPrice = parseFloat($('#onroad-price').val());
        let additionalAccessoriesAmount = parseFloat($('#additional-accessories-amount').val());
        let engineNumber = $('#engine-number').val();
        let chasisNumber = $('#chasis-number').val();
        let numberOfTyres = parseInt($('#number-of-tyres').val());
        let registeredLadenWeight = $('#registered-laden-weight').val();
        let businessExperience = $('#business-experience').val();
        let seatingCapacity = $('#seating-capacity').val();
        let note = $('#note-collateral-detail').val();
        let reasonForModification = $('#reason-for-modification').val();

        let result = true;

        if ($('#vehicle-loan-section').hasClass('d-none') === false) {

            // Set Default Value if Empty
            if (note === '') {
                note = 'None';
            }

            // Set Default Value if Empty
            if (reasonForModification === '') {
                reasonForModification = 'None';
            }

            //loan purpose radio button
            if ($('.loan-purpose:checked').length === 0) {
                result = false;
            }

            //vehicle supplier Id
            if ($('#vehicle-supplier-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //vehicle colour Id
            if ($('#vehicle-colour-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //vehicle make Id
            if ($('#vehicle-make-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //vehicle model Id
            if ($('#vehicle-model-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //vehicle variant Id
            if ($('#vehicle-variant-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //manufacture Year
            if (isNaN(manufactureYear) === false) {
                minimum = parseInt($('#manufacture-year').attr('min'));
                maximum = parseInt($('#manufacture-year').attr('max'));

                if (parseInt(manufactureYear) < parseInt(minimum) || parseInt(manufactureYear) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //registration Date
            if (IsValidInputDate('#registration-date') === false) {
                result = false;
            }

            //Registration Number
            minimumLength = parseInt($('#vehicle-registration-number').attr('minlength'));
            maximumLength = parseInt($('#vehicle-registration-number').attr('maxlength'));

            if (parseInt(registrationNumber.length) < parseInt(minimumLength) || parseInt(registrationNumber.length) > parseInt(maximumLength)) {
                result = false;
            }
            else {
                result = IsValidRegistrationNumber();
            }

            //ExShowRoomPrice
            if (isNaN(exShowroomPrice) === false) {
                minimum = parseFloat($('#ex-showroom-price').attr('min'));
                maximum = parseFloat($('#ex-showroom-price').attr('max'));

                if (parseFloat(exShowroomPrice) < parseFloat(minimum) || parseFloat(exShowroomPrice) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //On Road Price
            if (isNaN(onRoadPrice) === false) {
                minimum = parseFloat($('#onroad-price').attr('min'));
                maximum = parseFloat($('#onroad-price').attr('max'));

                if (parseFloat(onRoadPrice) < parseFloat(minimum) || parseFloat(onRoadPrice) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //additional Accessories Amount
            if (isNaN(additionalAccessoriesAmount) === false) {
                minimum = parseFloat($('#additional-accessories-amount').attr('min'));
                maximum = parseFloat($('#additional-accessories-amount').attr('max'));

                if (parseFloat(additionalAccessoriesAmount) < parseFloat(minimum) || parseFloat(additionalAccessoriesAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //engine number
            minimumLength = parseInt($('#engine-number').attr('minlength'));
            maximumLength = parseInt($('#engine-number').attr('maxlength'));

            if (parseInt(engineNumber.length) < parseInt(minimumLength) || parseInt(engineNumber.length) > parseInt(maximumLength)) {
                result = false;
            }

            //chasis number
            minimumLength = parseInt($('#chasis-number').attr('minlength'));
            maximumLength = parseInt($('#chasis-number').attr('maxlength'));
            if (parseInt(chasisNumber.length) < parseInt(minimumLength) || parseInt(chasisNumber.length) > parseInt(maximumLength)) {
                result = false;
            }

            //Number of tyre
            if (isNaN(numberOfTyres) === false) {
                minimum = parseInt($('#number-of-tyres').attr('min'));
                maximum = parseInt($('#number-of-tyres').attr('max'));

                if (parseInt(numberOfTyres) < parseInt(minimum) || parseInt(numberOfTyres) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            if ($('#is-used-for-commercial').is(':checked') === true) {
                //Registered Laden Weight
                if (isNaN(registeredLadenWeight) === false) {
                    minimum = parseInt($('#registered-laden-weight').attr('min'));
                    maximum = parseInt($('#registered-laden-weight').attr('max'));

                    if (parseInt(registeredLadenWeight) < parseInt(minimum) || parseInt(registeredLadenWeight) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                //Business Experience
                if (isNaN(businessExperience) === false) {
                    minimum = parseInt($('#business-experience').attr('min'));
                    maximum = parseInt($('#business-experience').attr('max'));

                    if (parseInt(businessExperience) < parseInt(minimum) || parseInt(businessExperience) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                //seating Capacity
                if (isNaN(seatingCapacity) === false) {
                    minimum = parseInt($('#seating-capacity').attr('min'));
                    maximum = parseInt($('#seating-capacity').attr('max'));

                    if (parseInt(seatingCapacity) < parseInt(minimum) || parseInt(seatingCapacity) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

            }
        }

        if (result) {
            $('#loan-collateral-detail-accordion-error').addClass('d-none');
        }
        else {
            $('#loan-collateral-detail-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // VehicleInsuranceDetail                changes done by indrayani
    function IsValidVehicleInsuranceDetailAccordionInputs() {


        let policyNumber = $('#policy-number').val();
        let policyPremium = parseFloat($('#insurance-premium').val());
        let sumInsured = parseFloat($('#policy-sum-insured').val());
        note = $('#note-vehicle-insurance-detail').val();

        let result = true;

        if ($('#vehicle-insurance-detail-card').hasClass('d-none') === false) {

            //insurance company Id
            if ($('#insurance-company-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //commencement date
            if (IsValidInputDate('#commencement-date') === false) {
                result = false;
            }

            //expiry date
            if (IsValidInputDate('#expiry-date-vehicle-insurance') === false) {
                result = false;
            }

            //policy Number
            minimumLength = parseInt($('#policy-number').attr('minlength'));
            maximumLength = parseInt($('#policy-number').attr('maxlength'));

            if (parseInt(policyNumber.length) < parseInt(minimumLength) || parseInt(policyNumber.length) > parseInt(maximumLength)) {
                result = false;
            }
            else {
                if (isDuplicatePolicyNumber === true) {
                    result = false;
                }
            }

            //type of coverage Id
            if ($('#type-of-coverage-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //Policy Premium
            if (isNaN(policyPremium) === false) {
                minimum = parseFloat($('#insurance-premium').attr('min'));
                maximum = parseFloat($('#insurance-premium').attr('max'));

                if (parseFloat(policyPremium) < parseFloat(minimum) || parseFloat(policyPremium) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //policy sum Insured
            if (isNaN(sumInsured) === false) {
                minimum = parseFloat($('#policy-sum-insured').attr('min'));
                maximum = parseFloat($('#policy-sum-insured').attr('max'));

                if (parseFloat(sumInsured) < parseFloat(minimum) || parseFloat(sumInsured) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Note
            maximumLength = $('#note-vehicle-insurance-detail').attr('maxlength');
            if (parseInt(note) > parseInt(maximumLength)) {
                result = false;
            }

        }
        if (result) {
            $('#vehicle-insurance-detail-accordion-error').addClass('d-none');
        }
        else {
            $('#vehicle-insurance-detail-accordion-error').removeClass('d-none');
        }

        return result;
    }

    //CustomerLoanPermitDeatil
    function IsValidCustomerLoanPermitDeatilAccordionInputs() {
        debugger;
        let permitType = $('#permit-type').val();
        let permitDetails = $('#permit-details').val();
        let issuingAuthority = $('#issuing-authority').val();
        let note = $('#note-vehicle-permit-detail').val();
        let reasonForModification = $('#permit-detail-reason-for-modification').val();
        let permitAmountPerMonth = parseFloat($('#permit-amount-per-month').val());

        result = true;

        if ($('#vehicle-loan-permit-detail-card').hasClass('d-none') === false) {

            // Set Default Value if Empty
            if (note === '') {
                note = 'None';
            }

            // Set Default Value if Empty
            if (reasonForModification === '') {
                reasonForModification = 'None';
            }

            // permitType
            if ($('#permit-type').prop('selectedIndex') < 1) {
                result = false;
            }

            //permitDetails
            if (isNaN(permitDetails.length) === false) {
                minimumLength = parseInt($('#permit-details').attr('minlength'));
                maximumLength = parseInt($('#permit-details').attr('maxlength'));

                if (parseInt(permitDetails.length) < parseInt(minimumLength) || parseInt(permitDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }
            }

            //PermitIssueDate
            if (IsValidInputDate('#activation-date-permit') === false) {
                result = false;
            }

            //PermitExpiryDate
            if (IsValidInputDate('#expiry-date-permit') === false) {
                result = false;
            }

            //issuingAuthority
            if (isNaN(issuingAuthority.length) === false) {

                minimumLength = parseInt($('#issuing-authority').attr('minlength'));
                maximumLength = parseInt($('#issuing-authority').attr('maxlength'));

                if (parseInt(issuingAuthority.length) < parseInt(minimumLength) || parseInt(issuingAuthority.length) > parseInt(maximumLength)) {
                    result = false;
                }
            }

            //permitAmountPerMonth
            if (isNaN(permitAmountPerMonth) === false) {
                if (permitType == "NTP") {

                    $('#permit-amount-per-month').attr('min', 5000);  // Set minimum value
                    $('#permit-amount-per-month').attr('max', 15000); // Set maximum value

                    minimum = parseFloat($('#permit-amount-per-month').attr('min'));
                    maximum = parseFloat($('#permit-amount-per-month').attr('max'));

                    if (parseFloat(permitAmountPerMonth) < parseFloat(minimum) || parseFloat(permitAmountPerMonth) > parseFloat(maximum)) {
                        result = false;
                    }

                }
                else if (permitType == "STP") {

                    $('#permit-amount-per-month').attr('min', 2000);  // Set minimum value
                    $('#permit-amount-per-month').attr('max', 10000); // Set maximum value

                    minimum = parseFloat($('#permit-amount-per-month').attr('min'));
                    maximum = parseFloat($('#permit-amount-per-month').attr('max'));

                    if (parseFloat(permitAmountPerMonth) < parseFloat(minimum) || parseFloat(permitAmountPerMonth) > parseFloat(maximum)) {
                        result = false;
                    }

                }
                else if (permitType == "TMP") {

                    $('#permit-amount-per-month').attr('min', 500);  // Set minimum value
                    $('#permit-amount-per-month').attr('max', 5000); // Set maximum value

                    minimum = parseFloat($('#permit-amount-per-month').attr('min'));
                    maximum = parseFloat($('#permit-amount-per-month').attr('max'));

                    if (parseFloat(permitAmountPerMonth) < parseFloat(minimum) || parseFloat(permitAmountPerMonth) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    // Other permit types
                    $('#permit-amount-per-month').attr('min', 10);  // Set minimum value
                    $('#permit-amount-per-month').attr('max', 15000); // Set maximum value

                    minimum = parseFloat($('#permit-amount-per-month').attr('min'));
                    maximum = parseFloat($('#permit-amount-per-month').attr('max'));

                    if (parseFloat(permitAmountPerMonth) < parseFloat(minimum) || parseFloat(permitAmountPerMonth) > parseFloat(maximum)) {
                        result = false;
                    }
                }
            }
            else {
                result = false;
            }
        }


        if (result) {
            $('#vehicle-permit-detail-accordion-error').addClass('d-none');
        } else {
            $('#vehicle-permit-detail-accordion-error').removeClass('d-none');
        }
    }

    //CustomerEducationLoanDetail
    function IsValidCustomerEducationLoanDetailAccordionInputs() {
        debugger;
        let otherNameOfInstitute = $('#other-name-of-institute').val();
        let transOtherNameOfInstitute = $('#trans-other-name-of-institute').val();
        let otherInstituteContactDetails = $('#other-institute-contact-details').val();
        let transOtherInstituteContactDetails = $('#trans-other-institute-contact-details').val();
        let otherInstituteAddressDetails = $('#other-institute-address-details').val();
        let transOtherInstituteAddressDetails = $('#trans-other-institute-address-details').val();
        let totalCourseFees = parseFloat($('#total-course-fees').val());
        let accommodationFees = parseFloat($('#accommodation-fees').val());
        let booksOrEquipmentsExpenses = parseFloat($('#books-or-equipments-expenses').val());
        let travellingExpenses = parseFloat($('#travelling-expenses').val());
        let refundableDeposit = parseFloat($('#refundable-deposit').val());
        let otherFees = parseFloat($('#other-fees').val());
        let otherFeesDetails = $('#other-fees-details').val();
        let transOtherFeesDetails = $('#trans-other-fees-details').val();
        let contactPersonName = $('#contact-person-name').val();
        let transContactPersonName = $('#trans-contact-person-name').val();
        let contactPersonContactDetails = $('#contact-person-contact-details').val();
        let transContactPersonContactDetails = $('#trans-contact-person-contact-details').val();
        let note = $('#educational-loan-note').val();
        let transnote = $('#trans-educational-loan-note').val();
        let ReasonForModification = $('#educational-loan-reason-for-modification').val();
        let TransReasonForModification = $('#trans-educational-loan-reason-for-modification').val();

        result = true;

        if ($('#educational-loan-detail-card').hasClass('d-none') === false) {

            //Default Value
            if (note === '') {
                note = 'None';
            }

            if (transnote === '') {
                transnote = 'None';
            }

            if (ReasonForModification === '') {
                ReasonForModification = 'None';
            }

            if (TransReasonForModification === '') {
                TransReasonForModification = 'None';
            }

            //Educational Course
            if ($('#educational-course-id').prop('selectedIndex') < 1) {
                result = false;
            }

            // Course Approved By
            if ($('.course-approved-by:checked').length === 0) {
                result = false;
            }

            //Institute
            if ($('#institude-id').prop('selectedIndex') < 1) {
                result = false;
            }

            if ($('#other-institude-input').hasClass('d-none') === false) {

                //Other Name Of Institute
                minimumLength = parseInt($('#other-name-of-institute').attr('minlength'));
                maximumLength = parseInt($('#other-name-of-institute').attr('maxlength'));

                if (parseInt(otherNameOfInstitute.length) < parseInt(minimumLength) || parseInt(otherNameOfInstitute.length) > parseInt(maximumLength)) {
                    result = false;
                }

                //Trans Other Name Of Institute
                minimumLength = parseInt($('#trans-other-name-of-institute').attr('minlength'));
                maximumLength = parseInt($('#trans-other-name-of-institute').attr('maxlength'));

                if (parseInt(transOtherNameOfInstitute.length) < parseInt(minimumLength) || parseInt(transOtherNameOfInstitute.length) > parseInt(maximumLength)) {
                    result = false;
                }

                //Other Institute Contact Details
                minimumLength = parseInt($('#other-institute-contact-details').attr('minlength'));
                maximumLength = parseInt($('#other-institute-contact-details').attr('maxlength'));

                if (parseInt(otherInstituteContactDetails.length) < parseInt(minimumLength) || parseInt(otherInstituteContactDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }

                //Trans Other Institute Contact Details
                minimumLength = parseInt($('#trans-other-institute-contact-details').attr('minlength'));
                maximumLength = parseInt($('#trans-other-institute-contact-details').attr('maxlength'));

                if (parseInt(transOtherInstituteContactDetails.length) < parseInt(minimumLength) || parseInt(transOtherInstituteContactDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }

                //Other Institute Address Details
                minimumLength = parseInt($('#other-institute-address-details').attr('minlength'));
                maximumLength = parseInt($('#other-institute-address-details').attr('maxlength'));

                if (parseInt(otherInstituteAddressDetails.length) < parseInt(minimumLength) || parseInt(otherInstituteAddressDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }

                //Trnas Other Institute Address Details
                minimumLength = parseInt($('#trans-other-institute-address-details').attr('minlength'));
                maximumLength = parseInt($('#trans-other-institute-address-details').attr('maxlength'));

                if (parseInt(transOtherInstituteAddressDetails.length) < parseInt(minimumLength) || parseInt(transOtherInstituteAddressDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }

            }
            else {
                otherNameOfInstitute = 'None';
                transOtherNameOfInstitute = 'None';
                otherInstituteContactDetails = 'None';
                transOtherInstituteContactDetails = 'None';
                otherInstituteAddressDetails = 'None';
                transOtherInstituteAddressDetails = 'None';
            }

            //City
            if ($('#education-city').prop('selectedIndex') < 1) {
                result = false;
            }

            //Total Course Fees
            if (isNaN(totalCourseFees) === false) {
                minimum = parseFloat($('#total-course-fees').attr('min'));
                maximum = parseFloat($('#total-course-fees').attr('max'));

                if (parseFloat(totalCourseFees) < parseFloat(minimum) || parseFloat(totalCourseFees) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Accommodation Fees
            if (isNaN(accommodationFees) === false) {
                minimum = parseFloat($('#accommodation-fees').attr('min'));
                maximum = parseFloat($('#accommodation-fees').attr('max'));

                if (parseFloat(accommodationFees) < parseFloat(minimum) || parseFloat(accommodationFees) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Books Or Equipments Expenses
            if (isNaN(booksOrEquipmentsExpenses) === false) {
                minimum = parseFloat($('#books-or-equipments-expenses').attr('min'));
                maximum = parseFloat($('#books-or-equipments-expenses').attr('max'));

                if (parseFloat(booksOrEquipmentsExpenses) < parseFloat(minimum) || parseFloat(booksOrEquipmentsExpenses) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Travelling Expenses
            if (isNaN(travellingExpenses) === false) {
                minimum = parseFloat($('#travelling-expenses').attr('min'));
                maximum = parseFloat($('#travelling-expenses').attr('max'));

                if (parseFloat(travellingExpenses) < parseFloat(minimum) || parseFloat(travellingExpenses) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Refundable Deposit
            if (isNaN(refundableDeposit) === false) {
                minimum = parseFloat($('#refundable-deposit').attr('min'));
                maximum = parseFloat($('#refundable-deposit').attr('max'));

                if (parseFloat(refundableDeposit) < parseFloat(minimum) || parseFloat(refundableDeposit) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Other Fees
            if (isNaN(otherFees) === false) {
                minimum = parseFloat($('#other-fees').attr('min'));
                maximum = parseFloat($('#other-fees').attr('max'));

                if (parseFloat(otherFees) < parseFloat(minimum) || parseFloat(otherFees) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Other Fees Details
            minimumLength = parseInt($('#other-fees-details').attr('minlength'));
            maximumLength = parseInt($('#other-fees-details').attr('maxlength'));

            if (parseInt(otherFeesDetails.length) < parseInt(minimumLength) || parseInt(otherFeesDetails.length) > parseInt(maximumLength)) {
                result = false;
            }

            //Trans Other Fees Details
            minimumLength = parseInt($('#trans-other-fees-details').attr('minlength'));
            maximumLength = parseInt($('#trans-other-fees-details').attr('maxlength'));

            if (parseInt(transOtherFeesDetails.length) < parseInt(minimumLength) || parseInt(transOtherFeesDetails.length) > parseInt(maximumLength)) {
                result = false;
            }

            // Admission Through
            if ($('.admission-through:checked').length === 0) {
                result = false;
            }

            //Contact Person Name
            minimumLength = parseInt($('#contact-person-name').attr('minlength'));
            maximumLength = parseInt($('#contact-person-name').attr('maxlength'));

            if (parseInt(contactPersonName.length) < parseInt(minimumLength) || parseInt(contactPersonName.length) > parseInt(maximumLength)) {
                result = false;
            }

            //Contact Person Name
            minimumLength = parseInt($('#trans-contact-person-name').attr('minlength'));
            maximumLength = parseInt($('#trans-contact-person-name').attr('maxlength'));

            if (parseInt(transContactPersonName.length) < parseInt(minimumLength) || parseInt(transContactPersonName.length) > parseInt(maximumLength)) {
                result = false;
            }

            //Contact Person Contact Details
            minimumLength = parseInt($('#contact-person-contact-details').attr('minlength'));
            maximumLength = parseInt($('#contact-person-contact-details').attr('maxlength'));

            if (parseInt(contactPersonContactDetails.length) < parseInt(minimumLength) || parseInt(contactPersonContactDetails.length) > parseInt(maximumLength)) {
                result = false;
            }

            //Contact Person Contact Details
            minimumLength = parseInt($('#trans-contact-person-contact-details').attr('minlength'));
            maximumLength = parseInt($('#trans-contact-person-contact-details').attr('maxlength'));

            if (parseInt(transContactPersonContactDetails.length) < parseInt(minimumLength) || parseInt(transContactPersonContactDetails.length) > parseInt(maximumLength)) {
                result = false;
            }

            // Course Start Date
            if (IsValidInputDate('#course-start-date') === false) {
                result = false;
            }

            // Course End Date
            if (IsValidInputDate('#course-end-date') === false) {
                result = false;
            }

        }

        if (result) {
            $('#educational-loan-detail-accordion-error').addClass('d-none');
        } else {
            $('#educational-loan-detail-accordion-error').removeClass('d-none');
        }
    }

    // PreOwnedVehicleLoanInspection      changes done by Dhanashri
    function IsValidPreOwnedVehicleLoanInspectionAccordionInputs() {
        let result = true;
        debugger;
        let numberOfOwner = parseInt($('#number-of-owner').val());
        let odoMeterReading = parseInt($('#odo-meter-reading').val());
        let hypothecationInstitutionOtherDetails = $('#hypothecation-institution-other-details').val();
        let remarkOfValuer = $('#remark-of-valuer').val();
        let hypothecationInstitutionName = $('#hypothecation-institution-name').val();
        let reasonForUnavailability = $('#reason-for-unavailability').val();
        let isHypothecation = $('#enable-is-under-any-hypothecation').is(':checked') ? true : false;
        let rcAvailability = $('#enable-rc-avaialbility').is(':checked') ? true : false;

        if ($('#loan-purpose-new-block').hasClass('d-none') === false) {

            //Number Of Owner
            if (isNaN(numberOfOwner) === false) {
                minimum = parseInt($('#number-of-owner').attr('min'));
                maximum = parseInt($('#number-of-owner').attr('max'));

                if (parseInt(numberOfOwner) < parseInt(minimum) || parseInt(numberOfOwner) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }


            // Odo Meter Reading
            if (isNaN(odoMeterReading) === false) {
                minimum = parseInt($('#odo-meter-reading').attr('min'));
                maximum = parseInt($('#odo-meter-reading').attr('max'));

                if (parseInt(odoMeterReading) < parseInt(minimum) || parseInt(odoMeterReading) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Engine Condition Score
            if ($('#engine-condition').prop('selectedIndex') < 1) {
                result = false;
            }

            // Gear Box Condition Score
            if ($('#gear-box-condition').prop('selectedIndex') < 1) {
                result = false;
            }

            // Brake Condition 
            if ($('#brake-condition').prop('selectedIndex') < 1) {
                result = false;
            }

            //Seat Condition
            if ($('#seat-condition').prop('selectedIndex') < 1) {
                result = false;
            }

            // Body Cabin Condition
            if ($('#body-cabin-condition').prop('selectedIndex') < 1) {
                result = false;
            }

            //Tyres Condition
            if ($('#tyres-condition').prop('selectedIndex') < 1) {
                result = false;
            }

            //Battery Condition
            if ($('#battery-condition').prop('selectedIndex') < 1) {
                result = false;
            }

            if (isHypothecation === true) {
                //Hypothecation Institution Name
                minimumLength = parseInt($('#hypothecation-institution-name').attr('minlength'));
                maximumLength = parseInt($('#hypothecation-institution-name').attr('maxlength'));

                if (parseInt(hypothecationInstitutionName.length) < parseInt(minimumLength) || parseInt(hypothecationInstitutionName.length) > parseInt(maximumLength)) {
                    result = false;
                }

                //Hypothecation Institution Details
                minimumLength = parseInt($('#hypothecation-institution-other-details').attr('minlength'));
                maximumLength = parseInt($('#hypothecation-institution-other-details').attr('maxlength'));

                if (parseInt(hypothecationInstitutionOtherDetails.length) < parseInt(minimumLength) || parseInt(hypothecationInstitutionOtherDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }
            }
            else {
                hypothecationInstitutionName = 'None';
                hypothecationInstitutionOtherDetails = 'None';
            }

            if (rcAvailability === false) {
                // Reason For Unavailability
                minimumLength = parseInt($('#reason-for-unavailability').attr('minlength'));
                maximumLength = parseInt($('#reason-for-unavailability').attr('maxlength'));

                if (parseInt(reasonForUnavailability.length) < parseInt(minimumLength) || parseInt(reasonForUnavailability.length) > parseInt(maximumLength)) {
                    result = false;
                }
            }
            else {
                reasonForUnavailability = 'None';
            }

            //remarkOfValuer
            if (remarkOfValuer === '') {
                remarkOfValuer = 'None';
            }

        }
        if (result) {
            $('#loan-inspection-accordion-error').addClass('d-none');
        }
        else {
            $('#loan-inspection-accordion-error').removeClass('d-none');
        }
        return result;
    }

    // FieldInvestigation    changes done by indrayani
    function IsValidFieldInvestigationAccordionInputs() {
        debugger;
        let nameOfContactedPerson = $('#name-of-contacted-person').val();
        let otherRelationTitle = $('#other-relation-title').val();
        let localityRemark = $('#locality-remark').val();

        let firstReferenceName = $('#first-reference-name').val();
        let firstReferenceAddress = $('#first-reference-address').val();

        let secondReferenceName = $('#second-reference-name').val();
        let secondReferenceAddress = $('#second-reference-address').val();

        let thirdReferenceName = $('#third-reference-name').val();
        let thirdReferenceAddress = $('#third-reference-address').val();

        let positiveObservations = $('#positive-observations').val();
        let negativeObservations = $('#negative-observations').val();
        let nonRecommendationReason = $('#non-recommendation-reason').val();

        result = true;
        if ($('#field-investigation-card').hasClass('d-none') === false) {
            // date of investigation
            if (IsValidInputDate('#date-of-investigation') === false) {
                result = false;
            }

            //investigation officer id
            if ($('#investigation-officer-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //Name of Contacted Person
            maximumLength = parseInt($('#name-of-contacted-person').attr('maxlength'));
            if (parseInt(nameOfContactedPerson.length) === 0 || parseInt(nameOfContactedPerson.length) > parseInt(maximumLength)) {
                result = false;
            }

            //check relation with applicant radio buttons
            if ($('.relation-with-applicant:checked').length === 0) {
                result = false;
            }

            //other Relation Title
            maximumLength = parseInt($('#other-relation-title').attr('maxlength'));
            if (parseInt(otherRelationTitle.length) === 0 || parseInt(otherRelationTitle.length) > parseInt(maximumLength)) {
                result = false;
            }

            //locality Remark
            maximumLength = parseInt($('#locality-remark').attr('maxlength'));
            if (parseInt(localityRemark.length) === 0 || parseInt(localityRemark.length) > parseInt(maximumLength)) {
                result = false;
            }

            //first Reference Name
            maximumLength = parseInt($('#first-reference-name').attr('maxlength'));
            if (parseInt(firstReferenceName.length) === 0 || parseInt(firstReferenceName.length) > parseInt(maximumLength)) {
                result = false;
            }

            //first Reference Address
            maximumLength = parseInt($('#first-reference-address').attr('maxlength'));
            if (parseInt(firstReferenceAddress.length) === 0 || parseInt(firstReferenceAddress.length) > parseInt(maximumLength)) {
                result = false;
            }
            //second Reference Name
            maximumLength = parseInt($('#second-reference-name').attr('maxlength'));
            if (parseInt(secondReferenceName.length) === 0 || parseInt(secondReferenceName.length) > parseInt(maximumLength)) {
                result = false;
            }

            //second Reference Address
            maximumLength = parseInt($('#second-reference-address').attr('maxlength'));
            if (parseInt(secondReferenceAddress.length) === 0 || parseInt(secondReferenceAddress.length) > parseInt(maximumLength)) {
                result = false;
            }
            //third Reference Name
            maximumLength = parseInt($('#third-reference-name').attr('maxlength'));
            if (parseInt(thirdReferenceName.length) === 0 || parseInt(thirdReferenceName.length) > parseInt(maximumLength)) {
                result = false;
            }

            //third Reference Address
            maximumLength = parseInt($('#third-reference-address').attr('maxlength'));
            if (parseInt(thirdReferenceAddress.length) === 0 || parseInt(thirdReferenceAddress.length) > parseInt(maximumLength)) {
                result = false;
            }

            //positive Observations
            maximumLength = parseInt($('#positive-observations').attr('maxlength'));
            if (parseInt(positiveObservations.length) === 0 || parseInt(positiveObservations.length) > parseInt(maximumLength)) {
                result = false;
            }

            //negative Observations
            maximumLength = parseInt($('#negative-observations').attr('maxlength'));
            if (parseInt(negativeObservations.length) === 0 || parseInt(negativeObservations.length) > parseInt(maximumLength)) {
                result = false;
            }

            //non Recommendation Reason
            maximumLength = parseInt($('#non-recommendation-reason').attr('maxlength'));
            if (parseInt(nonRecommendationReason.length) === 0 || parseInt(nonRecommendationReason.length) > parseInt(maximumLength)) {
                result = false;
            }
        }

        if (result) {
            $('#field-investigation-accordion-error').addClass('d-none');
        }
        else {
            $('#field-investigation-accordion-error').removeClass('d-none');
        }

        return result;

    }

    // Document Validation - Check Whether All Required Record (Documents) Added Or Not
    function IsAddedAllRequiredDocument() {
        debugger;
        let result = true;
        let documentTableValueArray = new Array();
        let i = 0;

        dataTableRecordCount = documentDataTable.rows().count();

        if (parseInt(dataTableRecordCount) >= requiredDocumentObj.length) {
            // Hide Added Joint Account DropdownList Items
            $('#tbl-document > tbody > tr').each(function () {
                currentRow = $(this).closest('tr');

                let myColumnValues = (documentDataTable.row(currentRow).data());

                documentTableValueArray.push(myColumnValues[1]);

                // Check All Entered 
                //if ($.inArray(selectListItemObj.Value, requiredDocumentObj.DocumentId) > -1)
                //    if (myColumnValues[1] != editedAddressTypeId)
                //        $('#address-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            });

            for (i = 0; i < requiredDocumentArray.length; i++) {
                debugger;
                if ($.inArray(requiredDocumentArray[i], documentTableValueArray) == -1) {
                    result = false;
                    break;
                }
            }
        }
        else
            result = false;

        // Remove Main Error
        $('#document-error').addClass('d-none');

        if (result)
            $('#required-document-error').addClass('d-none');
        else
            $('#required-document-error').removeClass('d-none');

        return result;
    }
    function IsValidDebtToIncomeRatioAccordionInputs() {

        debugger;
        let monthlyIncome = parseFloat($('#monthly-income').val());
        let monthlyRentPayments = parseFloat($('#monthly-rent-payments').val());
        let monthlyExpenseForTaxes = parseFloat($('#monthly-taxes-expense').val());
        let monthlyExpenseForInsurance = parseFloat($('#monthly-insurance-expense').val());
        let educationalLoanEMI = parseFloat($('#educational-loan-emi').val());
        let personalLoanEMI = parseFloat($('#personal-loan-emi').val());
        let coSignedLoanEMI = parseFloat($('#co-signed-loan-emi').val());
        let vehicleLoanEMI = parseFloat($('#vehicle-loan-emi').val());
        let minimumCreditCardPayments = parseFloat($('#minimum-credit-card-payments').val());
        let monthlyCarPayments = parseFloat($('#monthly-car-payments').val());
        let monthlyTimeSharePayments = parseFloat($('#monthly-time-share-payments').val());
        let monthlyChildSupportPayment = parseFloat($('#monthly-child-support-payment').val());
        let monthlyAlimonyPayment = parseFloat($('#monthly-alimony-payment').val());
        let ratio = parseFloat($('#debt-to-income-ratio').val());

        let eligibleExpenses = 0;
        let totalExpenses = 0;

        let isExceededDebtToIncomeRatio = false;
        let result = true;

        if ($('#debt-to-income-ratio-card').hasClass('d-none') === false) {
            //monthly Income
            if (isNaN(monthlyIncome) === false) {
                minimum = parseFloat($('#monthly-income').attr('min'));
                maximum = parseFloat($('#monthly-income').attr('max'));

                if (parseFloat(monthlyIncome) < parseFloat(minimum) || parseFloat(monthlyIncome) > parseFloat(maximum)) {
                    result = false;
                }
                else {
                    // For Eligible DTI Raton (i.e. Below 50%)
                    eligibleExpenses = Math.round(parseFloat(monthlyIncome) / 2);
                }
            }
            else {
                result = false;
            }

            // For Following Validation Monthly Income Must Be Valid
            if (result === true) {
                $('.income-ratio-input').not('#monthly-income').removeClass('read-only');
                //monthly Rent Payments
                if (isNaN(monthlyRentPayments) === false) {
                    minimum = parseFloat($('#monthly-rent-payments').attr('min'));
                    maximum = parseFloat($('#monthly-rent-payments').attr('max'));

                    if (parseFloat(monthlyRentPayments) < parseFloat(minimum) || parseFloat(monthlyRentPayments) > parseFloat(maximum)) {
                        $('#monthly-rent-payments').val(0);
                    }
                    else {
                        totalExpenses = monthlyRentPayments;
                    }
                } else {
                    $('#monthly-rent-payments').val(0);
                }

                //monthly Expense For Taxes
                if (isNaN(monthlyExpenseForTaxes) === false) {
                    minimum = parseFloat($('#monthly-taxes-expense').attr('min'));
                    maximum = parseFloat($('#monthly-taxes-expense').attr('max'));

                    if (parseFloat(monthlyExpenseForTaxes) < parseFloat(minimum) || parseFloat(monthlyExpenseForTaxes) > parseFloat(maximum)) {

                        $('#monthly-taxes-expense').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(monthlyExpenseForTaxes);
                    }
                } else {
                    $('#monthly-taxes-expense').val(0);
                }

                //monthly Expense For Insurance
                if (isNaN(monthlyExpenseForInsurance) === false) {
                    minimum = parseFloat($('#monthly-insurance-expense').attr('min'));
                    maximum = parseFloat($('#monthly-insurance-expense').attr('max'));

                    if (parseFloat(monthlyExpenseForInsurance) < parseFloat(minimum) || parseFloat(monthlyExpenseForInsurance) > parseFloat(maximum)) {

                        $('#monthly-insurance-expense').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(monthlyExpenseForInsurance);
                    }
                } else {
                    $('#monthly-insurance-expense').val(0);
                }

                //educational Loan EMI
                if (isNaN(educationalLoanEMI) === false) {
                    minimum = parseFloat($('#educational-loan-emi').attr('min'));
                    maximum = parseFloat($('#educational-loan-emi').attr('max'));
                    if (parseFloat(educationalLoanEMI) < parseFloat(minimum) || parseFloat(educationalLoanEMI) > parseFloat(maximum)) {

                        $('#educational-loan-emi').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(educationalLoanEMI);
                    }
                } else {
                    $('#educational-loan-emi').val(0);
                }

                //personal Loan EMI
                if (isNaN(personalLoanEMI) === false) {
                    minimum = parseFloat($('#personal-loan-emi').attr('min'));
                    maximum = parseFloat($('#personal-loan-emi').attr('max'));
                    if (parseFloat(personalLoanEMI) < parseFloat(minimum) || parseFloat(personalLoanEMI) > parseFloat(maximum)) {

                        $('#personal-loan-emi').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(personalLoanEMI);
                    }
                } else {
                    $('#personal-loan-emi').val(0);
                }

                //co-Signed Loan EMI
                if (isNaN(coSignedLoanEMI) === false) {
                    minimum = parseFloat($('#co-signed-loan-emi').attr('min'));
                    maximum = parseFloat($('#co-signed-loan-emi').attr('max'));
                    if (parseFloat(coSignedLoanEMI) < parseFloat(minimum) || parseFloat(coSignedLoanEMI) > parseFloat(maximum)) {

                        $('#co-signed-loan-emi').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(coSignedLoanEMI);
                    }

                } else {
                    $('#co-signed-loan-emi').val(0);
                }

                //vehicle Loan EMI
                if (isNaN(vehicleLoanEMI) === false) {
                    minimum = parseFloat($('#vehicle-loan-emi').attr('min'));
                    maximum = parseFloat($('#vehicle-loan-emi').attr('max'));
                    if (parseFloat(vehicleLoanEMI) < parseFloat(minimum) || parseFloat(vehicleLoanEMI) > parseFloat(maximum)) {

                        $('#vehicle-loan-emi').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(vehicleLoanEMI);
                    }
                } else {
                    $('#vehicle-loan-emi').val(0);
                }

                //minimum Credit Card Payments
                if (isNaN(minimumCreditCardPayments) === false) {
                    minimum = parseFloat($('#minimum-credit-card-payments').attr('min'));
                    maximum = parseFloat($('#minimum-credit-card-payments').attr('max'));
                    if (parseFloat(minimumCreditCardPayments) < parseFloat(minimum) || parseFloat(minimumCreditCardPayments) > parseFloat(maximum)) {

                        $('#minimum-credit-card-payments').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(minimumCreditCardPayments);
                    }
                } else {
                    $('#minimum-credit-card-payments').val(0);
                }

                //monthly Car Payments
                if (isNaN(monthlyCarPayments) === false) {
                    minimum = parseFloat($('#monthly-car-payments').attr('min'));
                    maximum = parseFloat($('#monthly-car-payments').attr('max'));

                    if (parseFloat(monthlyCarPayments) < parseFloat(minimum) || parseFloat(monthlyCarPayments) > parseFloat(maximum)) {

                        $('#monthly-car-payments').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(monthlyCarPayments);
                    }
                } else {
                    $('#monthly-car-payments').val(0);
                }

                //monthly Time Share Payments
                if (isNaN(monthlyTimeSharePayments) === false) {
                    minimum = parseFloat($('#monthly-time-share-payments').attr('min'));
                    maximum = parseFloat($('#monthly-time-share-payments').attr('max'));

                    if (parseFloat(monthlyTimeSharePayments) < parseFloat(minimum) || parseFloat(monthlyTimeSharePayments) > parseFloat(maximum)) {

                        $('#monthly-time-share-payments').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(monthlyTimeSharePayments);
                    }
                } else {
                    $('#monthly-time-share-payments').val(0);
                }

                //monthly Child Support Payment
                if (isNaN(monthlyChildSupportPayment) === false) {
                    minimum = parseFloat($('#monthly-child-support-payment').attr('min'));
                    maximum = parseFloat($('#monthly-child-support-payment').attr('max'));

                    if (parseFloat(monthlyChildSupportPayment) < parseFloat(minimum) || parseFloat(monthlyChildSupportPayment) > parseFloat(maximum)) {

                        $('#monthly-child-support-payment').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(monthlyChildSupportPayment);
                    }
                } else {
                    $('#monthly-child-support-payment').val(0);
                }

                //monthly Alimony Payment
                if (isNaN(monthlyAlimonyPayment) === false) {
                    minimum = parseFloat($('#monthly-alimony-payment').attr('min'));
                    maximum = parseFloat($('#monthly-alimony-payment').attr('max'));

                    if (parseFloat(monthlyAlimonyPayment) < parseFloat(minimum) || parseFloat(monthlyAlimonyPayment) > parseFloat(maximum)) {
                        $('#monthly-alimony-payment').val(0);
                    }
                    else {
                        totalExpenses = parseFloat(totalExpenses) + parseFloat(monthlyAlimonyPayment);
                    }
                } else {
                    $('#monthly-alimony-payment').val(0);
                }


                let debtIncomeRatio = Math.round((totalExpenses / monthlyIncome) * 100);

                minimum = parseFloat($('#debt-to-income-ratio').attr('min'));
                maximum = parseFloat($('#debt-to-income-ratio').attr('max'));

                $('#debt-to-income-ratio').val(debtIncomeRatio);

                if (parseFloat(debtIncomeRatio) > parseFloat(maximum)) {
                    totalExpenses = totalExpenses - myExpensesValue;
                    debtIncomeRatio = Math.round((totalExpenses / monthlyIncome) * 100);
                    $('#debt-to-income-ratio').val(debtIncomeRatio);
                    isExceededDebtToIncomeRatio = true;
                }
            }
            else {
                $('.income-ratio-input').not('#monthly-income').addClass('read-only');
            }
        }


        if (isExceededDebtToIncomeRatio === true && isDisplayAlert === false) {
            alert('Debt To Income Ratio Is At High Risk Level Larger Than 50%');
            isDisplayAlert = true;
        }
        else { isDisplayAlert = false; }

        if (result) {
            $('#debt-to-income-ratio-accordion-error').addClass('d-none');
        }
        else {
            $('#debt-to-income-ratio-accordion-error').removeClass('d-none');
        }

        return result;
    }

    //created by Dhanashri Wagh --14/11/2024
    function IsValidLoanAgainstPropertyAccordionInputs() {

        debugger;
        let propertyType = $('#property-type').val();
        let otherPropertyType = $('#other-property-type').val();
        let propertyAddressLine1 = $('#property-address-line-1').val();
        let propertyOwnershipStatus = $('#property-ownership-status option:selected').val();
        let otherPropertyOwnershipStatus = $('#other-ownership-status').val();
        let propertyAge = parseInt($('#property-age').val());
        let propertyValue = parseFloat($('#property-value').val());
        let downPaymentAmount = parseFloat($('#down-payment-amount').val());
        let hasExistingPropertyLiabilities = $('#enable-has-existing-property-liabilities').is(':checked') ? true : false;
        let outstandingLoanAmount = parseFloat($('#outstanding-loan-amount').val());
        let lenderName = $('#lender-name').val();
        let remainingTerm = parseInt($('#remaining-term').val());
        let monthlyRepaymentAmount = parseFloat($('#monthly-repayment-amount').val());
        let AnyAdditionalLiens = $('#any-additional-liens').val();
        let isPropertyFreeOfAnyLegalDisputes = $('#enable-is-property-free-of-any-legal-disputes').is(':checked') ? true : false;
        let legalDisputeDetails = $('#legal-dispute-details').val();
        let securityFeatures = $('#security-features').val();
        let utilityAvailability = $('#utility-availability').val();
        let hasMortgageInsurance = $('#enable-has-mortgage-insurance').is(':checked') ? true : false;
        let mortgageInsuranceAmount = parseFloat($('#mortgage-insurance-amount').val());
        let note = $('#property-note').val();
        let reasonForModification = $('#property-reason-for-modification').val();

        let result = true;
        if ($('#loan-against-property-collateral-card').hasClass('d-none') === false) {

            // Set Default Value if Empty
            if (note === '')
                note = 'None';

            // Set Default Value if Empty
            if (reasonForModification === '')
                reasonForModification = 'None';

            //propertyType
            if ($('#property-type').prop('selectedIndex') < 1) {
                result = false;
            }

            //otherPropertyType
            if (propertyType === 'OTH') {
                minimumLength = parseInt($('#other-property-type').attr('minlength'));
                maximumLength = parseInt($('#other-property-type').attr('maxlength'));
                if (parseInt(otherPropertyType.length) < parseInt(minimumLength) || parseInt(otherPropertyType.length) > parseInt(maximumLength)) {
                    result = false;
                }
            }

            //propertyAddressLine1
            minimumLength = parseInt($('#property-address-line-1').attr('minlength'));
            maximumLength = parseInt($('#property-address-line-1').attr('maxlength'));

            if (parseInt(propertyAddressLine1.length) < parseInt(minimumLength) || parseInt(propertyAddressLine1.length) > parseInt(maximumLength)) {
                result = false;
            }

            //City
            if ($('#city-loan-against-property').prop('selectedIndex') < 1) {
                result = false;
            }

            //PropertyOwnershipStatus
            if ($('#property-ownership-status').prop('selectedIndex') < 1) {
                result = false;
            }

            //otherPropertyOwnershipStatus
            if (propertyOwnershipStatus === 'OTH') {
                minimumLength = parseInt($('#other-ownership-status').attr('minlength'));
                maximumLength = parseInt($('#other-ownership-status').attr('maxlength'));
                if (parseInt(otherPropertyOwnershipStatus.length) < parseInt(minimumLength) || parseInt(otherPropertyOwnershipStatus.length) > parseInt(maximumLength)) {
                    result = false;
                }
            }

            // Property Age
            if (isNaN(propertyAge) === false) {
                minimum = parseInt($('#property-age').attr('min'));
                maximum = parseInt($('#property-age').attr('max'));

                if (parseInt(propertyAge) < parseInt(minimum) || parseInt(propertyAge) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Property Value
            if (isNaN(propertyValue) === false) {
                minimum = parseFloat($('#property-value').attr('min'));
                maximum = parseFloat($('#property-value').attr('max'));

                if (parseFloat(propertyValue) < parseFloat(minimum) || parseFloat(propertyValue) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //Down Payment Amount
            if (isNaN(downPaymentAmount) === false) {
                minimum = parseFloat($('#down-payment-amount').attr('min'));
                maximum = parseFloat($('#down-payment-amount').attr('max'));

                if (parseFloat(downPaymentAmount) < parseFloat(minimum) || parseFloat(downPaymentAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            //propertyUsage
            if ($('#property-usage').prop('selectedIndex') < 1) {
                result = false;
            }

            //hasExistingPropertyLiabilities
            if (hasExistingPropertyLiabilities === true) {

                // Out Standing Loan Amount
                if (isNaN(outstandingLoanAmount) === false) {
                    minimum = parseFloat($('#outstanding-loan-amount').attr('min'));
                    maximum = parseFloat($('#outstanding-loan-amount').attr('max'));

                    if (parseFloat(outstandingLoanAmount) < parseFloat(minimum) || parseFloat(outstandingLoanAmount) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                //lenderName
                minimumLength = parseInt($('#lender-name').attr('minlength'));
                maximumLength = parseInt($('#lender-name').attr('maxlength'));

                if (parseInt(lenderName.length) < parseInt(minimumLength) || parseInt(lenderName.length) > parseInt(maximumLength)) {
                    result = false;
                }

                // Remaining Term
                if (isNaN(remainingTerm) === false) {
                    minimum = parseInt($('#remaining-term').attr('min'));
                    maximum = parseInt($('#remaining-term').attr('max'));

                    if (parseInt(remainingTerm) < parseInt(minimum) || parseInt(remainingTerm) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Monthly Repayment Amount
                if (isNaN(monthlyRepaymentAmount) === false) {
                    minimum = parseFloat($('#monthly-repayment-amount').attr('min'));
                    maximum = parseFloat($('#monthly-repayment-amount').attr('max'));

                    if (parseFloat(monthlyRepaymentAmount) < parseFloat(minimum) || parseFloat(monthlyRepaymentAmount) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                //AnyAdditionalLiens
                minimumLength = parseInt($('#any-additional-liens').attr('minlength'));
                maximumLength = parseInt($('#any-additional-liens').attr('maxlength'));
                if (parseInt(AnyAdditionalLiens.length) < parseInt(minimumLength) || parseInt(AnyAdditionalLiens.length) > parseInt(maximumLength)) {
                    result = false;
                }


            }
            else if (hasExistingPropertyLiabilities === false) {
                outstandingLoanAmount = 'None';
                lenderName = 'None';
                remainingTerm = 'None';
                monthlyRepaymentAmount = 'None';
                AnyAdditionalLiens = 'None';
            }

            //isPropertyFreeOfAnyLegalDisputes
            if (isPropertyFreeOfAnyLegalDisputes === true) {

                minimumLength = parseInt($('#legal-dispute-details').attr('minlength'));
                maximumLength = parseInt($('#legal-dispute-details').attr('maxlength'));

                if (parseInt(legalDisputeDetails.length) < parseInt(minimumLength) || parseInt(legalDisputeDetails.length) > parseInt(maximumLength)) {
                    result = false;
                }
            }
            else if (isPropertyFreeOfAnyLegalDisputes === false) {
                legalDisputeDetails = 'None';
            }

            //propertyCondition
            if ($('#property-condition-id').prop('selectedIndex') < 1) {
                result = false;
            }

            //securityFeatures
            minimumLength = parseInt($('#security-features').attr('minlength'));
            maximumLength = parseInt($('#security-features').attr('maxlength'));
            if (parseInt(securityFeatures.length) < parseInt(minimumLength) || parseInt(securityFeatures.length) > parseInt(maximumLength)) {
                result = false;
            }

            //utilityAvailability
            minimumLength = parseInt($('#utility-availability').attr('minlength'));
            maximumLength = parseInt($('#utility-availability').attr('maxlength'));
            if (parseInt(utilityAvailability.length) < parseInt(minimumLength) || parseInt(utilityAvailability.length) > parseInt(maximumLength)) {
                result = false;
            }

            //hasMortgageInsurance
            if (hasMortgageInsurance === true) {

                // Mortgage Insurance Amount
                if (isNaN(mortgageInsuranceAmount) === false) {
                    minimum = parseFloat($('#mortgage-insurance-amount').attr('min'));
                    maximum = parseFloat($('#mortgage-insurance-amount').attr('max'));

                    if (parseFloat(mortgageInsuranceAmount) < parseFloat(minimum) || parseFloat(mortgageInsuranceAmount) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }
            else if (hasMortgageInsurance === false) {
                mortgageInsuranceAmount == 'None';
            }
        }

        if (result) {
            $('#property-loan-collateral-detail-accordion-error').addClass('d-none');
        }
        else {
            $('#property-loan-collateral-detail-accordion-error').removeClass('d-none');
        }

        return result;
    }

    //created by indrayani ghadage
    function IsValidCashCreditLoanAccordionInput() {
        debugger;
        let percentageofOwnership = parseFloat($('#percentage-of-ownership').val());
        let annualTurnOver = parseFloat($('#annual-turn-over').val());
        let currentYearTurnover = parseFloat($('#current-year-project-turnover').val());
        let profitLossAmount = parseFloat($('#profit-loss-amount').val());
        let previousYearTurnover = parseFloat($('#previous-year-turnover').val());
        let previousSecondYearTurnover = parseFloat($('#previous-second-year-turnover').val());
        let previousThirdYearTurnover = parseFloat($('#previous-third-year-turnover').val());
        let grossProfitMargin = parseFloat($('#previous-year-gross-profit-margin').val());
        let secondYearGrossProfitMargin = parseFloat($('#previous-second-year-gross-profit-margin').val());
        let thirdYearGrossProfitMargin = parseFloat($('#previous-third-year-gross-profit-margin').val());
        let yearNetProfitMargin = parseFloat($('#previous-year-net-profit-margin').val());
        let secondYearNetProfitMargin = parseFloat($('#previous-second-year-gross-profit-margin').val());
        let thirdYearNetProfitMargin = parseFloat($('#previous-third-year-gross-profit-margin').val());
        let debtEquityRatio = parseFloat($('#debt-equity-ratio').val());
        let workingCapitalCycle = parseInt($('#working-capital-cycle').val());
        let valueCollateral = parseFloat($('#value-of-collateral').val());
        let note = $('#note-cash-credit').val();

        let result = true;

        if ($('#cash-credit-loan-account-card').hasClass('d-none') === false) {

            // Set Default Value if Empty
            if (note === '')
                note = 'None';

            // Percentage Of Ownership
            if (isNaN(percentageofOwnership) === false) {
                minimum = parseFloat($('#percentage-of-ownership').attr('min'));
                maximum = parseFloat($('#percentage-of-ownership').attr('max'));

                if (parseFloat(percentageofOwnership) < parseFloat(minimum) || parseFloat(percentageofOwnership) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Annual Turn Over
            if (isNaN(annualTurnOver) === false) {
                minimum = parseFloat($('#annual-turn-over').attr('min'));
                maximum = parseFloat($('#annual-turn-over').attr('max'));

                if (parseFloat(annualTurnOver) < parseFloat(minimum) || parseFloat(annualTurnOver) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Current Year Turn over
            if (isNaN(currentYearTurnover) === false) {
                minimum = parseFloat($('#current-year-project-turnover').attr('min'));
                maximum = parseFloat($('#current-year-project-turnover').attr('max'));

                if (parseFloat(currentYearTurnover) < parseFloat(minimum) || parseFloat(currentYearTurnover) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Profit Loss Amount
            if (isNaN(profitLossAmount) === false) {
                minimum = parseFloat($('#profit-loss-amount').attr('min'));
                maximum = parseFloat($('#profit-loss-amount').attr('max'));

                if (parseFloat(profitLossAmount) < parseFloat(minimum) || parseFloat(profitLossAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Previous Year Turn over
            if ($('#previous-year-turnover-input').hasClass('d-none') === false) {
                if (isNaN(previousYearTurnover) === false) {
                    minimum = parseFloat($('#previous-year-turnover').attr('min'));
                    maximum = parseFloat($('#previous-year-turnover').attr('max'));

                    if (parseFloat(previousYearTurnover) < parseFloat(minimum) || parseFloat(previousYearTurnover) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Previous Second Year Turn over
            if ($('#second-year-turnover-input').hasClass('d-none') === false) {

                if (isNaN(previousSecondYearTurnover) === false) {
                    minimum = parseFloat($('#previous-second-year-turnover').attr('min'));
                    maximum = parseFloat($('#previous-second-year-turnover').attr('max'));

                    if (parseFloat(previousSecondYearTurnover) < parseFloat(minimum) || parseFloat(previousSecondYearTurnover) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Previous Third Year Turn over
            if ($('#third-year-turnover-input').hasClass('d-none') === false) {
                if (isNaN(previousThirdYearTurnover) === false) {
                    minimum = parseFloat($('#previous-third-year-turnover').attr('min'));
                    maximum = parseFloat($('#previous-third-year-turnover').attr('max'));

                    if (parseFloat(previousThirdYearTurnover) < parseFloat(minimum) || parseFloat(previousThirdYearTurnover) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Gross Profit Margin
            if ($('#gross-profit-margin-input').hasClass('d-none') === false) {
                if (isNaN(grossProfitMargin) === false) {
                    minimum = parseFloat($('#previous-year-gross-profit-margin').attr('min'));
                    maximum = parseFloat($('#previous-year-gross-profit-margin').attr('max'));

                    if (parseFloat(grossProfitMargin) < parseFloat(minimum) || parseFloat(grossProfitMargin) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Second Year Gross Profit Margin
            if ($('#second-year-gross-profit-margin-input').hasClass('d-none') === false) {
                if (isNaN(secondYearGrossProfitMargin) === false) {
                    minimum = parseFloat($('#previous-second-year-gross-profit-margin').attr('min'));
                    maximum = parseFloat($('#previous-second-year-gross-profit-margin').attr('max'));

                    if (parseFloat(secondYearGrossProfitMargin) < parseFloat(minimum) || parseFloat(secondYearGrossProfitMargin) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Third Year Gross Profit Margin
            if ($('#third-year-gross-profit-margin-input').hasClass('d-none') === false) {
                if (isNaN(thirdYearGrossProfitMargin) === false) {
                    minimum = parseFloat($('#previous-third-year-gross-profit-margin').attr('min'));
                    maximum = parseFloat($('#previous-third-year-gross-profit-margin').attr('max'));

                    if (parseFloat(thirdYearGrossProfitMargin) < parseFloat(minimum) || parseFloat(thirdYearGrossProfitMargin) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Previous Year Net Profit Margin
            if ($('#net-profit-margin-input').hasClass('d-none') === false) {
                if (isNaN(yearNetProfitMargin) === false) {
                    minimum = parseFloat($('#previous-year-net-profit-margin').attr('min'));
                    maximum = parseFloat($('#previous-year-net-profit-margin').attr('max'));

                    if (parseFloat(yearNetProfitMargin) < parseFloat(minimum) || parseFloat(yearNetProfitMargin) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Second Year Net Profit Margin
            if ($('#second-year-net-profit-margin-input').hasClass('d-none') === false) {
                if (isNaN(secondYearNetProfitMargin) === false) {
                    minimum = parseFloat($('#previous-second-year-gross-profit-margin').attr('min'));
                    maximum = parseFloat($('#previous-second-year-gross-profit-margin').attr('max'));

                    if (parseFloat(secondYearNetProfitMargin) < parseFloat(minimum) || parseFloat(secondYearNetProfitMargin) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Third Year Net Profit Margin
            if ($('#third-year-net-profit-margin-input').hasClass('d-none') === false) {
                if (isNaN(thirdYearNetProfitMargin) === false) {
                    minimum = parseFloat($('#previous-third-year-gross-profit-margin').attr('min'));
                    maximum = parseFloat($('#previous-third-year-gross-profit-margin').attr('max'));

                    if (parseFloat(thirdYearNetProfitMargin) < parseFloat(minimum) || parseFloat(thirdYearNetProfitMargin) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Debt Equity Ratio
            if (isNaN(debtEquityRatio) === false) {
                minimum = parseFloat($('#debt-equity-ratio').attr('min'));
                maximum = parseFloat($('#debt-equity-ratio').attr('max'));

                if (parseFloat(debtEquityRatio) < parseFloat(minimum) || parseFloat(debtEquityRatio) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Value Collateral
            if (isNaN(valueCollateral) === false) {
                minimum = parseFloat($('#value-of-collateral').attr('min'));
                maximum = parseFloat($('#value-of-collateral').attr('max'));

                if (parseFloat(valueCollateral) < parseFloat(minimum) || parseFloat(valueCollateral) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Working Capital Cycle
            if (isNaN(workingCapitalCycle) === false) {
                minimum = parseInt($('#working-capital-cycle').attr('min'));
                maximum = parseInt($('#working-capital-cycle').attr('max'));

                if (parseInt(workingCapitalCycle) < parseInt(minimum) || parseInt(workingCapitalCycle) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }
        if (result) {
            $('#cash-credit-accordion-error').addClass('d-none');
        }
        else {
            $('#cash-credit-accordion-error').removeClass('d-none');
        }

        return result;
    }

    function IsValidVehicleContractDetailAccordionInputs() {
        let contractObligation = $('#contract-obligations').val();
        let companyName = $('#company-name').val();
        let companyDetail = $('#company-details').val();
        let contactDetail = $('#contact-details').val();
        let addressDetail = $('#address-details').val();
        let contractMonthlyAmount = parseFloat($('#contract-monthly-amount').val());
        let paymentDay = parseInt($('#payment-day').val());
        note = $('#note-vehicle-contract-detail').val();

        let result = true;

        if ($('#has-contract-block').hasClass('d-none') === false) {

            //contract nature id
            if ($('#contract-nature-id').prop('selectedIndex') < 1) {
                result = false;
                $('#contract-nature-id-error').removeClass('d-none');
            }
            else {
                $('#contract-nature-id-error').addClass('d-none');
            }

            //start date
            if (IsValidInputDate('#start-date') === false) {
                result = false;
                $('#start-date-error').removeClass('d-none');
            }
            else {
                $('#start-date-error').addClass('d-none');
            }

            //End date
            if (IsValidInputDate('#end-date') === false) {
                result = false;
                $('#end-date-error').removeClass('d-none');
            }
            else {
                $('#end-date-error').addClass('d-none');
            }

            //contract Obligation
            minimumLength = parseInt($('#contract-obligations').attr('minlength'));
            maximumLength = parseInt($('#contract-obligations').attr('maxlength'));
            if (parseInt(contractObligation.length) < parseInt(minimumLength) || parseInt(contractObligation.length) > parseInt(maximumLength)) {
                result = false;
                $('#contract-obligations-error').removeClass('d-none');
            }
            else {
                $('#contract-obligations-error').addClass('d-none');
            }

            //company Name
            minimumLength = parseInt($('#company-name').attr('minlength'));
            maximumLength = parseInt($('#company-name').attr('maxlength'));
            if (parseInt(companyName.length) < parseInt(minimumLength) || parseInt(companyName.length) > parseInt(maximumLength)) {
                result = false;
                $('#company-name-error').removeClass('d-none');
            }
            else {
                $('#company-name-error').addClass('d-none');
            }

            //company Detail
            minimumLength = parseInt($('#company-details').attr('minlength'));
            maximumLength = parseInt($('#company-details').attr('maxlength'));
            if (parseInt(companyDetail.length) < parseInt(minimumLength) || parseInt(companyDetail.length) > parseInt(maximumLength)) {
                result = false;
                $('#company-details-error').removeClass('d-none');
            }
            else {
                $('#company-details-error').addClass('d-none');
            }

            //contact Detail
            minimumLength = parseInt($('#contact-details').attr('minlength'));
            maximumLength = parseInt($('#contact-details').attr('maxlength'));
            if (parseInt(contactDetail.length) < parseInt(minimumLength) || parseInt(contactDetail.length) > parseInt(maximumLength)) {
                result = false;
                $('#contact-details-error').removeClass('d-none');
            }
            else {
                $('#contact-details-error').addClass('d-none');
            }

            //address Detail
            minimumLength = parseInt($('#address-details').attr('minlength'));
            maximumLength = parseInt($('#address-details').attr('maxlength'));
            if (parseInt(addressDetail.length) < parseInt(minimumLength) || parseInt(addressDetail.length) > parseInt(maximumLength)) {
                result = false;
                $('#address-details-error').removeClass('d-none');
            }
            else {
                $('#address-details-error').addClass('d-none');
            }

            //contract Monthly Amount
            if (isNaN(contractMonthlyAmount) === false) {
                minimum = parseFloat($('#contract-monthly-amount').attr('min'));
                maximum = parseFloat($('#contract-monthly-amount').attr('max'));
                if (parseFloat(contractMonthlyAmount) < parseFloat(minimum) || parseFloat(contractMonthlyAmount) > parseFloat(maximum)) {
                    result = false;
                    $('#contract-monthly-amount-error').removeClass('d-none');
                }
                else {
                    $('#contract-monthly-amount-error').addClass('d-none');
                }
            }
            else {
                result = false;
                $('#contract-monthly-amount-error').removeClass('d-none');
            }

            //payment Day
            if (isNaN(paymentDay) === false) {
                minimum = parseInt($('#payment-day').attr('min'));
                maximum = parseInt($('#payment-day').attr('max'));
                if (parseInt(paymentDay) < parseInt(minimum) || parseInt(paymentDay) > parseInt(maximum)) {
                    result = false;
                    $('#payment-day-error').removeClass('d-none');
                }
                else {
                    $('#payment-day-error').addClass('d-none');
                }
            }
            else {
                result = false;
                $('#payment-day-error').removeClass('d-none');
            }

            //payment frequency
            if ($('#payment-frequency-id').prop('selectedIndex') < 1) {
                result = false;
                $('#payment-frequency-id-error').removeClass('d-none');
            }
            else {
                $('#payment-frequency-id-error').addClass('d-none');
            }

            //payment mode
            if ($('#payment-mode-id').prop('selectedIndex') < 1) {
                result = false;
                $('#payment-mode-id-error').removeClass('d-none');
            }
            else {
                $('#payment-mode-id-error').addClass('d-none');
            }

            //note
            maximumLength = parseInt($('#note-vehicle-contract-detail').attr('maxlength'));
            if (parseInt(note.length) > parseInt(maximumLength)) {
                result = false;
                $('#note-vehicle-contract-detail-error').removeClass('d-none');
            }
            else {
                $('#note-vehicle-contract-detail-error').addClass('d-none');
            }
        }
        if (result) {
            $('#vehicle-contract-detail-accordion-error').addClass('d-none');
        }
        else {
            $('#vehicle-contract-detail-accordion-error').removeClass('d-none');
        }
    }

    function IsValidBusinessLoanCollateralDetailAccordionInputs() {
        debugger;
        let totalBusinessExperience = parseInt($('#total-business-experience').val());
        let annualTurnOver = parseFloat($('#business-loan-annual-turn-over').val());
        let previousYearProfit1 = parseFloat($('#previous-year-profit-1').val());
        let previousYearProfit2 = parseFloat($('#previous-year-profit-2').val());
        let previousYearProfit3 = parseFloat($('#previous-year-profit-3').val());
        let previousYearProfit4 = parseFloat($('#previous-year-profit-4').val());
        let previousYearProfit5 = parseFloat($('#previous-year-profit-5').val());
        note = $('#business-loan-note').val();
        let reasonForModification = $('#business-loan-reason-for-modification').val();

        let result = true;

        if ($('#business-loan-detail-card').hasClass('d-none') === false) {
            // Set Default Value if Empty
            if (note === '') {
                note = 'None';
            }

            // Set Default Value if Empty
            if (reasonForModification === '') {
                reasonForModification = 'None';
            }

            //TotalBusinessExperience
            if (isNaN(totalBusinessExperience) === false) {
                minimum = parseInt($('#total-business-experience').attr('min'));
                maximum = parseInt($('#total-business-experience').attr('max'));

                if (parseInt(totalBusinessExperience) < parseInt(minimum) || parseInt(totalBusinessExperience) > parseInt(maximum)) {
                    result = false;
                }

            } else {
                result = false;
            }

            //AnnualTurnOver
            if (isNaN(annualTurnOver) === false) {
                minimum = parseFloat($('#business-loan-annual-turn-over').attr('min'));
                maximum = parseFloat($('#business-loan-annual-turn-over').attr('max'));

                if (parseFloat(annualTurnOver) < parseFloat(minimum) || parseFloat(annualTurnOver) > parseFloat(maximum)) {
                    result = false;
                }

            } else {
                result = false;
            }

            //PreviousYearProfit1
            if ($('#profit-group-1').hasClass('d-none') === false) {
                if (isNaN(previousYearProfit1) === false) {
                    minimum = parseFloat($('#previous-year-profit-1').attr('min'));
                    maximum = parseFloat($('#previous-year-profit-1').attr('max'));

                    if (parseFloat(previousYearProfit1) < parseFloat(minimum) || parseFloat(previousYearProfit1) > parseFloat(maximum)) {
                        result = false;
                    }

                } else {
                    result = false;
                }
            }

            //PreviousYearProfit2
            if ($('#profit-group-2').hasClass('d-none') === false) {
                if (isNaN(previousYearProfit2) === false) {
                    minimum = parseFloat($('#previous-year-profit-2').attr('min'));
                    maximum = parseFloat($('#previous-year-profit-2').attr('max'));

                    if (parseFloat(previousYearProfit2) < parseFloat(minimum) || parseFloat(previousYearProfit2) > parseFloat(maximum)) {
                        result = false;
                    }

                } else {
                    result = false;
                }
            }

            //PreviousYearProfit3
            if ($('#profit-group-3').hasClass('d-none') === false) {
                if (isNaN(previousYearProfit3) === false) {
                    minimum = parseFloat($('#previous-year-profit-3').attr('min'));
                    maximum = parseFloat($('#previous-year-profit-3').attr('max'));

                    if (parseFloat(previousYearProfit3) < parseFloat(minimum) || parseFloat(previousYearProfit3) > parseFloat(maximum)) {
                        result = false;
                    }

                } else {
                    result = false;
                }
            }

            //PreviousYearProfit4
            if ($('#profit-group-4').hasClass('d-none') === false) {
                if (isNaN(previousYearProfit4) === false) {
                    minimum = parseFloat($('#previous-year-profit-4').attr('min'));
                    maximum = parseFloat($('#previous-year-profit-4').attr('max'));

                    if (parseFloat(previousYearProfit4) < parseFloat(minimum) || parseFloat(previousYearProfit4) > parseFloat(maximum)) {
                        result = false;
                    }

                } else {
                    result = false;
                }
            }

            //PreviousYearProfit5
            if ($('#profit-group-5').hasClass('d-none') === false) {
                if (isNaN(previousYearProfit5) === false) {
                    minimum = parseFloat($('#previous-year-profit-5').attr('min'));
                    maximum = parseFloat($('#previous-year-profit-5').attr('max'));

                    if (parseFloat(previousYearProfit5) < parseFloat(minimum) || parseFloat(previousYearProfit5) > parseFloat(maximum)) {
                        result = false;
                    }

                } else {
                    result = false;
                }
            }

            if (result) {
                $('#business-loan-detail-accordion-error').addClass('d-none');
            }
            else {
                $('#business-loan-detail-accordion-error').removeClass('d-none');
            }

        }
    }

    // Standing Instruction
    function IsValidStandingInstructionAccordionInputs() {
        result = true;
        note = $('#customer-account-standing-instruction-note').val();
        // Standing Instruction
        if ($('#auto-debit-block').hasClass('d-none') === false) {

            if (note === '') {
                note = "None";
            }

            // Debit Account
            if ($('#customer-saving-account-debit').prop('selectedIndex') < 1) {
                result = false;
            }
        }

        if (result) {
            $('#standing-instruction-accordion-error').addClass('d-none');
        }
        else {
            $('#standing-instruction-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // #################  Join Account - DataTable Code 

    // DataTable Add Button 
    $('#btn-add-joint-account-dt').click(function (event) {
        // Display Alert Message Only When Modal Present On Page (i.e. Create And Amend) Hide On Verify
        if ($('#joint-account-modal').length) {
            editedSequenceNumber = 0;
            selectedjointPersonId = '';
            selectedjointPersonText = '';

            // Hide Joint Account Change Notification
            $('#joint-account-holder-change-info').addClass('d-none');

            dataTableRecordCount = jointAccountDataTable.rows().count();

            if (parseInt(dataTableRecordCount) >= parseInt(maximumJointAccountHolder)) {
                $('#joint-account-modal').modal('hide');
                alert('Number Of Joint Account Holders Are Allowed Between ' + minimumJointAccountHolder + ' And ' + maximumJointAccountHolder);
            }
            else {
                event.preventDefault();
                SetModalTitle('joint-account', 'Add');
            }
        }
    });

    // DataTable Edit Button 
    $('#btn-edit-joint-account-dt').click(function () {
        SetModalTitle('joint-account', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            $('#joint-account-holder-change-info').removeClass('d-none');

            columnValues = $('#btn-edit-joint-account-dt').data('rowindex');

            id = $('#joint-account-modal').attr('id');
            myModal = $('#' + id).modal();

            selectedjointPersonId = columnValues[1];
            selectedjointPersonText = columnValues[2];

            jointAccountHolderActivationDate = GetOnlyDate(columnValues[6]);
            jointAccountHolderExpiryDate = GetOnlyDate(columnValues[7]);

            jointAccountHolderActivationDate = new Date(columnValues[6]);
            jointAccountHolderExpiryDate = new Date(columnValues[7]);

            // Display Value In Modal Inputs
            $('#person-id-joint-account-holder', myModal).val(columnValues[2]);
            $('#joint-account-holder-id', myModal).val(columnValues[3]);
            $('#joint-account-sequence-number', myModal).val(columnValues[5]);
            $('#activation-date-joint-account-holder', myModal).val(GetInputDateFormat(jointAccountHolderActivationDate));
            $('#expiry-date-joint-account-holder', myModal).val(GetInputDateFormat(jointAccountHolderExpiryDate));
            $('#note-joint-account-holder', myModal).val(columnValues[8]);
            $('#reason-for-modification-joint-account-holder', myModal).val(columnValues[9]);

            editedSequenceNumber = columnValues[5];

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-joint-account-dt').addClass('read-only');
            $('#joint-account-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-joint-account-modal').click(function () {
        debugger;
        if (IsValidAccountHolderModal()) {
            row = jointAccountDataTable.row.add([
                tag,
                selectedjointPersonId,
                selectedjointPersonText,
                jointAccountHolderId,
                jointAccountHolderIdText,
                sequenceNumber,
                jointAccountHolderActivationDate,
                jointAccountHolderExpiryDate,
                note,
                reasonForModification,
            ]).draw();

            HideJointAccountDataTableColumns();

            jointAccountDataTable.columns.adjust().draw();

            $('#joint-account-modal').modal('hide');

            EnableNewOperation('joint-account');
        }
    });

    // Modal update Button Event
    $('#btn-update-joint-account-modal').click(function () {
        $('#select-all-joint-account').prop('checked', false);

        if (IsValidAccountHolderModal()) {
            jointAccountDataTable.row(selectedRowIndex).data([
                tag,
                selectedjointPersonId,
                selectedjointPersonText,
                jointAccountHolderId,
                jointAccountHolderIdText,
                sequenceNumber,
                jointAccountHolderActivationDate,
                jointAccountHolderExpiryDate,
                note,
                reasonForModification,
            ]).draw();

            // Clear Nominee Data Table And Display Notification, On Changing Joint Account Record
            nomineeDataTable.clear().draw();

            HideJointAccountDataTableColumns();

            jointAccountDataTable.columns.adjust().draw();

            $('#joint-account-modal').modal('hide');

            EnableNewOperation('joint-account');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-joint-account-dt').click(function () {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-joint-account tbody input[type="checkbox"]:checked').each(function () {
                    jointAccountDataTable.row($('#tbl-joint-account tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    EnableNewOperation('joint-account');

                    $('#select-all-joint-account').prop('checked', false);
                }));

                // Validate Required Number Of Joint Account Holders.
                dataTableRecordCount = jointAccountDataTable.rows().count();

                if (parseInt(dataTableRecordCount) < parseInt(minimumJointAccountHolder)) {
                    result = false;
                    minMaxResult = false;

                    $('#joint-account-accordion-min-max-error').html('Number Of Joint Account Holders Are Allowed Between ' + minimumJointAccountHolder + ' And ' + maximumJointAccountHolder);

                    $('#joint-account-accordion-error').addClass('d-none');
                    $('#joint-account-accordion-min-max-error').removeClass('d-none');
                }

                // Clear Nominee Data Table And Display Notification, On Changing Joint Account Record
                nomineeDataTable.clear().draw();
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-joint-account').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-joint-account tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = jointAccountDataTable.row(row).index();

                rowData = (jointAccountDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-joint-account-dt').data('rowindex', arr);
                EnableDeleteOperation('joint-account')
            });
        }
        else {
            EnableNewOperation('joint-account')

            $('#tbl-joint-account tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-joint-account tbody').click('input[type="checkbox"]', function () {
        $('#tbl-joint-account input[type="checkbox"]:checked').each(function () {
            debugger
            isChecked = $(this).prop('checked');

            if (isChecked) {
                debugger;
                row = $(this).closest('tr');

                selectedRowIndex = jointAccountDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (jointAccountDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('joint-account');

                    $('#btn-update-joint-account-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-joint-account-dt').data('rowindex', rowData);
                    $('#btn-delete-joint-account-dt').data('rowindex', arr);
                    $('#select-all-joint-account').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-joint-account tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('joint-account');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('joint-account');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('joint-account');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-joint-account').prop('checked', true);
        else
            $('#select-all-joint-account').prop('checked', false);
    });

    // Validate Account Holder Module
    function IsValidAccountHolderModal() {
        minMaxResult = true;
        result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        jointAccountHolderId = $('#joint-account-holder-id option:selected').val();
        jointAccountHolderIdText = $('#joint-account-holder-id option:selected').text();
        sequenceNumber = $('#joint-account-sequence-number').val();
        jointAccountHolderActivationDate = $('#activation-date-joint-account-holder').val();
        jointAccountHolderExpiryDate = $('#expiry-date-joint-account-holder').val();
        note = $('#note-joint-account-holder').val();
        reasonForModification = $('#reason-for-modification-joint-account-holder').val();

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        // Check Whether Enter Mobile Number Is Existed Or Not
        let filteredData = jointAccountDataTable
            .rows()
            .indexes()
            .filter(function (value) {
                return jointAccountDataTable.row(value).data()[5] == $('#joint-account-sequence-number').val();
            });

        if (jointAccountDataTable.rows(filteredData).count() > 0 && editedSequenceNumber != $('#joint-account-sequence-number').val()) {
            isDuplicateSequenceNumber = true;
            result = false;
            $('#joint-account-sequence-number-error').removeClass('d-none');
        }
        else {
            isDuplicateSequenceNumber = false;
            $('#joint-account-sequence-number-error').addClass('d-none');
        }

        // Validation Person Id
        if (selectedjointPersonId === '') {
            result = false;
            $('#person-id-joint-account-holder-error').removeClass('d-none');
        }

        //Validation Joint Account Holder Id
        if ($('#joint-account-holder-id').prop('selectedIndex') < 1) {
            result = false;
            $('#joint-account-holder-id-error').removeClass('d-none');
        }
        else
            $('#joint-account-holder-id-error').addClass('d-none');

        //Validation Sequence Number
        if (sequenceNumber === '' || isDuplicateSequenceNumber === true || parseInt(sequenceNumber) < 1 || parseInt(sequenceNumber) > 199) {
            result = false;
            $('#joint-account-sequence-number-error').removeClass('d-none');
        }
        else
            $('#joint-account-sequence-number-error').addClass('d-none');

        // Validation Activation Date
        if (IsValidInputDate('#activation-date-joint-account-holder') === false) {
            result = false;
            $('#activation-date-joint-account-holder-error').removeClass('d-none');
        }
        else
            $('#activation-date-joint-account-holder-error').addClass('d-none');

        // Validation Expiry Date
        if (IsValidInputDate('#expiry-date-joint-account-holder') === false) {
            result = false;
            $('#expiry-date-joint-account-holder-error').removeClass('d-none');
        }
        else
            $('#expiry-date-joint-account-holder-error').addClass('d-none');

        debugger;
        // Validate Required Number Of Joint Accounts
        dataTableRecordCount = jointAccountDataTable.rows().count();

        // Add + 1 (i.e. Current Row Count)
        if (editedSequenceNumber == 0)
            dataTableRecordCount = dataTableRecordCount + 1;

        if (parseInt(dataTableRecordCount) < parseInt(minimumJointAccountHolder)) {
            minMaxResult = false;
            $('#joint-account-accordion-min-max-error').html('Number Of Joint Account Holders Are Allowed Between ' + minimumJointAccountHolder + ' And ' + maximumJointAccountHolder);
        }

        if (result) {
            if (minMaxResult == false) {
                $('#joint-account-accordion-error').addClass('d-none');
                $('#joint-account-accordion-min-max-error').removeClass('d-none');
            }
            else {
                $('#joint-account-accordion-error').addClass('d-none');
                $('#joint-account-accordion-min-max-error').addClass('d-none');
            }
        }

        return result;
    }

    function HideJointAccountDataTableColumns() {
        jointAccountDataTable.column(1).visible(false);
        jointAccountDataTable.column(3).visible(false);
        jointAccountDataTable.column(9).visible(false);
    }

    /// #################  Customer Account Nominee - DataTable Code 

    // DataTable Add Button 
    $('#btn-add-account-nominee-dt').click(function (event) {
        $('#guardian-card').addClass('d-none');

        $('#collapse-guardian').addClass('show');

        HoldingPercentagetHideSection();
        ProportionateAmountHideSection();

        // Display Alert Message Only When Modal Present On Page (i.e. Create And Amend) Hide On Verify
        if ($('#account-nominee-modal').length) {
            editedNomineeNumber = 0;

            nomineePersonInformationNumber = '';
            nomineePersonInformationNumberText = '';
            guardianNomineePersonInformationNumber = '';
            guardianNomineePersonInformationNumber = '';
            guardianNomineePersonInformationNumberText = '';

            event.preventDefault();

            // Get Joint Customer Names From Joint Account DataTable
            jointCustomerAccountId = new Array();

            $('#tbl-joint-account > TBODY> TR').each(function () {
                currentRow = $(this).closest('tr');
                columnValues = (jointAccountDataTable.row(currentRow).data());

                // Handling Code If Row Is Undefined Or Null
                if (typeof columnValues == 'undefined' && columnValues == null) {
                    return false;
                }

                let td0 = columnValues[5];
                let td1 = columnValues[2];

                jointCustomerAccountId.push({ td0, td1 });
            });

            customerDropdownForNominne = $('#customer-person-id');
            customerDropdownForNominne.html('');

            options = '<option value="00000000-0000-0000-0000-000000000000">--- Select Person ---</option>';

            // Get Customer Dropdown List Item For Nominnee From Joint Account Holder
            $.each(jointCustomerAccountId, function (key, value) {
                options += '<option value="' + value.td0 + '">' + value.td1 + '</option>';
            });

            // Add Joint Customers In Nominne Dropdownlist
            customerDropdownForNominne.append(options);
            customerDropdownForNominne.append('<option value="' + 0 + '">' + $('#person-id').val() + '</option>');
            customerDropdownForNominne.prop('selectedIndex', 0);

            // Hide inserted personid 
            $('#tbl-account-nominee > tbody > tr').each(function () {
                currentRow = $(this).closest('tr');
                columnValues = (nomineeDataTable.row(currentRow).data());

                // Handling Code If Row Is Undefined Or Null
                if (typeof columnValues == 'undefined' && columnValues == null) {
                    return false;
                }
                else {
                    // Hide Dropdown List Item Based On Column[5] i.e. Sequence Number
                    if (columnValues[5] == 0)
                        $('#customer-person-id').find('option[value="' + columnValues[5] + '"]').remove();
                    else {
                        // Hide All Joint Accounts In Nominee Person Dropdown List
                        $.each(jointCustomerAccountId, function (key, value) {
                            if (columnValues[5] == value.td0) {
                                $('#customer-person-id').find('option[value="' + value.td0 + '"]').remove();
                            }
                        });
                    }
                }
            });


            let nomineeDataTableCount = nomineeDataTable.rows().count();

            // Get Adding Nominee Limit By SchemeId
            // Raise Error If Add Nominee Out Of Range
            if (parseInt(nomineeDataTableCount) >= parseInt(maximumNominee)) {
                $('#account-nominee-modal').modal('hide');
                alert('Number Of Nominee Allowed Between' + minimumNominee + ' And ' + maximumNominee);
            }
            else
                SetModalTitle('account-nominee', 'Add');

        }
    });

    // DataTable Edit Button 
    $('#btn-edit-account-nominee-dt').click(function () {
        debugger;

        SetModalTitle('account-nominee', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-account-nominee-dt').data('rowindex');
            id = $('#account-nominee-modal').attr('id');
            myModal = $('#' + id).modal();

            nominationDate = GetOnlyDate(columnValues[4]);
            birthDate = GetOnlyDate(columnValues[10]);
            activationDate = GetOnlyDate(columnValues[19]);
            expiryDate = GetOnlyDate(columnValues[20]);
            closeDate = GetOnlyDate(columnValues[21]);
            guardianBirthDate = GetOnlyDate(columnValues[31]);
            appointedDateOfContact = GetOnlyDate(columnValues[38]);

            nominationDate = new Date(columnValues[4]);
            birthDate = new Date(columnValues[10]);
            activationDate = new Date(columnValues[19]);
            expiryDate = new Date(columnValues[20]);
            closeDate = new Date(columnValues[21]);
            guardianBirthDate = new Date(columnValues[31]);
            appointedDateOfContact = new Date(columnValues[38]);

            $('#nomination-date', myModal).val(GetInputDateFormat(nominationDate));
            $('#activation-date-general-ledger', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-general-ledger', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-general-ledger', myModal).val(GetInputDateFormat(closeDate));
            $('#appointed-date-of-contact', myModal).val(GetInputDateFormat(appointedDateOfContact));

            // Get Only Appointed Time Of Contact
            [time, meridian] = columnValues[39].split(' ');
            [hours, minutes] = time.split(':');

            if (hours === '12')
                hours = '00';

            if (meridian === 'PM')
                hours = parseInt(hours, 10) + 12;

            scheduletime = hours + ':' + minutes;

            // If Nominee Is Existing Customer i.e. Has Valid Person Information Number
            if ((columnValues[6] == 'None')) {
                $('#nomineedetails').addClass('d-none');

                $('#nominee-person-information-number-input').removeClass('d-none');

                // Get Age Of Customer
                $.get('/PersonChildAction/GetPersonCurrentAge', { _personInformationNumber: columnValues[8], async: false }, function (data) {
                    if (data <= 18) {
                        debugger;
                        $('#guardian-card').removeClass('d-none');
                        $('#collapse-guardian').addClass('show');
                    }
                    else {
                        $('#guardian-card').addClass('d-none');
                    }
                });
            }
            else   // User Enter Manullay Nominee Details 
            {
                $('#nomineedetails').removeClass('d-none');
                $('#nominee-person-information-number-input').addClass('d-none');

                let dob = new Date(birthDate);
                today = new Date();

                // ****** Call Function From Configure Repository To Calculate Age
                let age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));

                if (age <= 18)
                    $('#guardian-card').removeClass('d-none');
                else
                    $('#guardian-card').addClass('d-none');
            }

            // If Guardian Person Information Number Is Valid, Hide Manually Input Fields For Guardian Details
            if (columnValues[25] == 'None') {
                $('.nominee-guardian-details').addClass('d-none');
                $('#nominee-guardian-person-information-number-input').removeClass('d-none');
            }
            else {
                $('.nominee-guardian-details').removeClass('d-none');
                $('#nominee-guardian-person-information-number-input').addClass('d-none');
            }

            // Get Customer Account Name Of Nominee

            personName = '';
            personName = $('#person-id').val();

            customerDropdownForNominne = $('#customer-person-id');
            customerDropdownForNominne.html('');
            options = '';

            // Find Sequence Number Of Nominee To Find Joint Account Holder
            seqNo = columnValues[5];

            // Main Account Customer Name
            if (seqNo == '0')
                customerDropdownForNominne.append('<option value="' + 0 + '">' + personName + '</option>');
            else {
                //Get Main Customer Name Of Joint Account Holder
                if (seqNo == columnValues[5]) {

                    customerDropdownForNominne.append('<option value="' + columnValues[5] + '">' + columnValues[2] + '</option>');
                    customerDropdownForNominne.find("option[value='" + columnValues[5] + "']").prop("selected", true);
                }
            }

            // Display Value In Modal Inputs
            $('#customer-person-id', myModal).val(columnValues[1]);
            $('#nomination-number', myModal).val(columnValues[3]);
            $('#nomination-date', myModal).val(GetInputDateFormat(nominationDate));
            $('#sequence-number', myModal).val(columnValues[5]);
            $('#name-of-nominee', myModal).val(columnValues[6]);
            $('#trans-name-of-nominee', myModal).val(columnValues[7]);
            nomineePersonInformationNumber = columnValues[8];
            nomineePersonInformationNumberText = columnValues[9];
            $('#nominee-person-id', myModal).val(columnValues[9])
            $('#nominee-birth-date', myModal).val(GetInputDateFormat(birthDate));
            $('#nominee-full-address-details', myModal).val(columnValues[11]);
            $('#trans-nominee-full-address-details', myModal).val(columnValues[12]);
            $('#nominee-contact-details', myModal).val(columnValues[13]);
            $('#trans-nominee-contact-details', myModal).val(columnValues[14]);
            $('#relation-id', myModal).val(columnValues[15]);
            $('#holding-percentage', myModal).val(columnValues[17]);
            $('#proportionate-amount-for-each-nominee', myModal).val(columnValues[18]);
            $('#activation-date-nominee', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-nominee', myModal).val(GetInputDateFormat(expiryDate));
            $('#nominee-close-date', myModal).val(GetInputDateFormat(closeDate));
            $('#nominee-note', myModal).val(columnValues[22]);
            $('#trans-nominee-note', myModal).val(columnValues[23]);
            $('#reason-for-modification-nominee', myModal).val(columnValues[24]);
            $('#guardian-full-name', myModal).val(columnValues[25]);
            $('#trans-guardian-full-name', myModal).val(columnValues[26]);
            guardianNomineePersonInformationNumber = columnValues[27],
                guardianNomineePersonInformationNumberText = columnValues[28],
                $('#nominee-guardian-person-information-number', myModal).val(columnValues[28]);
            $('#guardian-type-id', myModal).val(columnValues[29]);
            $('#guardian-nominee-birth-date', myModal).val(GetInputDateFormat(guardianBirthDate));
            $('#guardian-nominee-full-address-details', myModal).val(columnValues[32]);
            $('#trans-guardian-nominee-full-address-details', myModal).val(columnValues[33]);
            $('#guardian-nominee-contact-details', myModal).val(columnValues[34]);
            $('#trans-guardian-nominee-contact-details', myModal).val(columnValues[35]);
            $('.age-proof-sub-status-minor[value="' + columnValues[36] + '"]').prop('checked', true);
            $('#appointed-date-of-contact', myModal).val(GetInputDateFormat(appointedDateOfContact));
            $('#appointed-time-of-contact', myModal).val(scheduletime);
            $('#guardian-nominee-note').val(columnValues[40]);
            $('#trans-guardian-nominee-note').val(columnValues[41]);
            $('#reason-for-modification-guardian-nominee').val(columnValues[42]);

            $('#customer-person-id').val(seqNo);

            editedNomineeNumber = columnValues[3];

            // Hide Holding Percentage  / Proportionate Amount For Each Nominee Input
            if (isNaN(parseFloat(columnValues[17])) || parseFloat(columnValues[17]) == 0) {
                $('#holding-percentage-input').addClass('d-none');
                $('#proportionate-amount-for-each-nominee-input').removeClass('d-none');
            }
            else {
                $('#holding-percentage-input').removeClass('d-none');
                $('#proportionate-amount-for-each-nominee-input').addClass('d-none');
            }

            // Hide Guardion If Nominee Is Adult
            if (columnValues[29] == '')
                $('#guardian-card').addClass('d-none');
            else
                $('#guardian-card').removeClass('d-none');

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-account-nominee-dt').addClass('read-only');
            $('#account-nominee-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-account-nominee-modal').click(function () {
        isUpdate = false;

        if (IsValidAccountNomineeDataTableModal()) {
            row = nomineeDataTable.row.add([
                tag,
                customerId,
                customerIdText,
                nominationNumber,
                nominationDate,
                sequenceNumber,
                nameOfNominee,
                transnameOfNominee,
                nomineePersonInformationNumber,
                nomineePersonInformationNumberText,
                birthDate,
                fullAddressDetails,
                transFullAddress,
                contactDetails,
                transContactDetails,
                relationId,
                relationIdText,
                holdingPercentage,
                proportionateAmountForEachNominee,
                activationDate,
                expiryDate,
                closeDate,
                note,
                transNote,
                reasonForModification,
                guardianFullName,
                transGuardianFullName,
                guardianNomineePersonInformationNumber,
                guardianNomineePersonInformationNumberText,
                guardianTypeId,
                guardianTypeIdText,
                guardianNomineeBirthDate,
                guardianNomineeFullAddress,
                transGuardianNomineeFullAddress,
                guardianContactDetails,
                transGuardianContactDetails,
                ageProofSubmissionStatusOfTheMinor,
                ageProofSubmissionStatusOfTheMinorText,
                appointedDateOfContact,
                appointedTimeOfContact,
                guardianNote,
                transGuardianNote,
                guardianReasonForModification,

            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideAccountNomineeDataTableColumns()



            nomineeDataTable.columns.adjust().draw();

            $('#account-nominee-modal').modal('hide');

            EnableNewOperation('account-nominee');
        }
    });

    // Modal Update Button Event
    $('#btn-update-account-nominee-modal').click(function () {
        debugger;
        isUpdate = true;
        $('#select-all-account-nominee').prop('checked', false);

        if (IsValidAccountNomineeDataTableModal()) {
            debugger;
            nomineeDataTable.row(selectedRowIndex).data([
                tag,
                customerId,
                customerIdText,
                nominationNumber,
                nominationDate,
                sequenceNumber,
                nameOfNominee,
                transnameOfNominee,
                nomineePersonInformationNumber,
                nomineePersonInformationNumberText,
                birthDate,
                fullAddressDetails,
                transFullAddress,
                contactDetails,
                transContactDetails,
                relationId,
                relationIdText,
                holdingPercentage,
                proportionateAmountForEachNominee,
                activationDate,
                expiryDate,
                closeDate,
                note,
                transNote,
                reasonForModification,
                guardianFullName,
                transGuardianFullName,
                guardianNomineePersonInformationNumber,
                guardianNomineePersonInformationNumberText,
                guardianTypeId,
                guardianTypeIdText,
                guardianNomineeBirthDate,
                guardianNomineeFullAddress,
                transGuardianNomineeFullAddress,
                guardianContactDetails,
                transGuardianContactDetails,
                ageProofSubmissionStatusOfTheMinor,
                ageProofSubmissionStatusOfTheMinorText,
                appointedDateOfContact,
                appointedTimeOfContact,
                guardianNote,
                transGuardianNote,
                guardianReasonForModification,
            ]).draw();

            HideAccountNomineeDataTableColumns()

            nomineeDataTable.columns.adjust().draw();

            $('#account-nominee-modal').modal('hide');

            EnableNewOperation('account-nominee');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-account-nominee-dt').click(function () {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-account-nominee tbody input[type="checkbox"]:checked').each(function () {
                    nomineeDataTable.row($('#tbl-account-nominee tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    EnableNewOperation('account-nominee');

                    $('#select-all-account-nominee').prop('checked', false);
                }));

                // Validate Required Number Of Joint Account Holders.
                dataTableRecordCount = nomineeDataTable.rows().count();

                if (parseInt(dataTableRecordCount) < parseInt(minimumNominee)) {
                    result = false;
                    minMaxResult = false;

                    $('#account-nominee-accordion-min-max-error').html('Number Of Nominee Are Allowed Between ' + minimumNominee + ' And ' + maximumNominee);

                    $('#account-nominee-accordion-error').addClass('d-none');
                    $('#account-nominee-accordion-min-max-error').removeClass('d-none');
                }

            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-account-nominee').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-account-nominee tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = nomineeDataTable.row(row).index();

                rowData = (nomineeDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });


                $('#btn-delete-account-nominee-dt').data('rowindex', arr);
                EnableDeleteOperation('account-nominee')
            });
        }
        else {
            EnableNewOperation('account-nominee')
            $('#tbl-account-nominee tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-account-nominee tbody').click('input[type=checkbox]', function () {
        $('#tbl-account-nominee input[type="checkbox"]:checked').each(function () {
            isChecked = $(this).prop('checked');
            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = nomineeDataTable.row(row).index();

                rowData = (nomineeDataTable.row(selectedRowIndex).data());
                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('account-nominee');

                $('#btn-update-account-nominee-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-account-nominee-dt').data('rowindex', rowData);
                $('#btn-delete-account-nominee-dt').data('rowindex', arr);
                $('#select-all-account-nominee').data('rowindex', arr);
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-account-nominee tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('account-nominee');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('account-nominee');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('account-nominee');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-account-nominee').prop('checked', true);
        else
            $('#select-all-account-nominee').prop('checked', false);
    });

    // Validate Account Nominee Module
    function IsValidAccountNomineeDataTableModal() {
        debugger;
        let isValidGuardianDetails = true;
        let nominationNumberCount = 0;
        let isSelectedPersonInformationNumberForNominee = false;
        let isSelectedPersonInformationNumberForGuardian = false;

        result = true;
        minMaxResult = true;

        // Increase Count On Update Operation
        if (isUpdate)
            nominationNumberCount = 1;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        customerId = $('#customer-person-id option:selected').val();
        customerIdText = $('#customer-person-id option:selected').text();
        nominationNumber = $('#nomination-number').val();
        nominationDate = $('#nomination-date').val();

        if (nomineePersonInformationNumber === '' || nomineePersonInformationNumber === 'None') {
            nameOfNominee = $('#name-of-nominee').val();
            transnameOfNominee = $('#trans-name-of-nominee').val();
            birthDate = $('#nominee-birth-date').val();
            fullAddressDetails = $('#nominee-full-address-details').val();
            transFullAddress = $('#trans-nominee-full-address-details').val();
            contactDetails = $('#nominee-contact-details').val();
            transContactDetails = $('#trans-nominee-contact-details').val();
        }
        else {
            nameOfNominee = 'None';
            transnameOfNominee = 'None';
            birthDate = '1900-01-01';
            fullAddressDetails = 'None';
            transFullAddress = 'None';
            contactDetails = 'None';
            transContactDetails = 'None';
        }

        sequenceNumber = customerId;
        relationId = $('#relation-id option:selected').val();
        relationIdText = $('#relation-id option:selected').text();
        holdingPercentage = parseFloat($('#holding-percentage').val());
        proportionateAmountForEachNominee = parseFloat($('#proportionate-amount-for-each-nominee').val());
        activationDate = $('#activation-date-nominee').val();
        expiryDate = $('#expiry-date-nominee').val();
        closeDate = $('#nominee-close-date').val();
        note = $('#nominee-note').val();
        transNote = $('#trans-nominee-note').val();
        reasonForModification = $('#reason-for-modification-nominee').val();

        // Assign Default Values
        if (birthDate === '')
            birthDate = '1900-01-01';

        if (nomineePersonInformationNumber === '' || typeof nomineePersonInformationNumber === 'undefined')
            nomineePersonInformationNumber = 'None';

        if (guardianNomineePersonInformationNumber === '' || typeof guardianNomineePersonInformationNumber === 'undefined')
            guardianNomineePersonInformationNumber = 'None';

        if (nameOfNominee === '')
            nameOfNominee = 'None';

        if (fullAddressDetails === '')
            fullAddressDetails = 'None';

        if (transnameOfNominee === '')
            transnameOfNominee = 'None';

        if (transContactDetails === '')
            transContactDetails = 'None';

        if (contactDetails === '')
            contactDetails = 'None';

        if (transFullAddress === '')
            transFullAddress = 'None';

        if (note === '')
            note = 'None';

        if (transNote === '')
            transNote = 'None';

        if (guardianNomineeFullAddress === '')
            guardianNomineeFullAddress = 'None';

        if (transGuardianNomineeFullAddress === '')
            transGuardianNomineeFullAddress = 'None';

        if (guardianContactDetails === '')
            guardianContactDetails = 'None';

        if (transGuardianContactDetails === '')
            transGuardianContactDetails = 'None';

        if (guardianNote === '')
            guardianNote = 'None';

        if (transGuardianNote === '')
            transGuardianNote = 'None';

        if (isNaN(holdingPercentage))
            holdingPercentage = 0;

        if (isNaN(proportionateAmountForEachNominee))
            proportionateAmountForEachNominee = 0;

        // Check Whether Nominee Is Adult Or Minor 
        let isAdult = $('#guardian-card').hasClass('d-none');

        // Asssign Default Values To Guardian Field, If Nominee Is Adult 
        if (isAdult) {
            guardianFullName = 'None';
            transGuardianFullName = 'None';
            ageProofSubmissionStatusOfTheMinor = '';
            ageProofSubmissionStatusOfTheMinorText = '';
            guardianNomineePersonInformationNumber = '';
            guardianNomineePersonInformationNumberText = '';

            guardianNomineeFullAddress = 'None';
            transGuardianNomineeFullAddress = 'None';
            guardianContactDetails = 'None';
            transGuardianContactDetails = 'None';

            guardianTypeId = '';
            guardianTypeIdText = '';
            guardianNomineeBirthDate = '';
            appointedDateOfContact = '';
            appointedTimeOfContact = '';
            guardianNote = '';
            transGuardianNote = '';
            guardianReasonForModification = '';
        }
        else {
            // Check Whether Guardian Is Existing Customer Or Not (i.e. Select Person Or Input Details Manually)
            let isExistingCustomer = $('.nominee-guardian-details').hasClass('d-none');

            // Assign Default Values If Guardian Is Existing Customer
            if (isExistingCustomer) {
                guardianFullName = 'None';
                transGuardianFullName = 'None';
                guardianNomineeBirthDate = '1900-01-01';
                guardianNomineeFullAddress = 'None';
                transGuardianNomineeFullAddress = 'None';
                guardianContactDetails = 'None';
                transGuardianContactDetails = 'None';
            }
            else {
                guardianFullName = $('#guardian-full-name').val();
                transGuardianFullName = $('#trans-guardian-full-name').val();
                guardianNomineeBirthDate = $('#guardian-nominee-birth-date').val();
                guardianNomineeFullAddress = $('#guardian-nominee-full-address-details').val();
                transGuardianNomineeFullAddress = $('#trans-guardian-nominee-full-address-details').val();
                guardianContactDetails = $('#guardian-nominee-contact-details').val();
                transGuardianContactDetails = $('#trans-guardian-nominee-contact-details').val();
            }

            guardianTypeId = $('#guardian-type-id option:selected').val();
            guardianTypeIdText = $('#guardian-type-id option:selected').text();
            ageProofSubmissionStatusOfTheMinor = $('.age-proof-sub-status-minor:checked').val();
            ageProofSubmissionStatusOfTheMinorText = $('.age-proof-sub-status-minor:checked').next('label').text();
            appointedDateOfContact = $('#appointed-date-of-contact').val();
            appointedTimeOfContact = $('#appointed-time-of-contact').val();
            guardianNote = $('#guardian-nominee-note').val();
            transGuardianNote = $('#trans-guardian-nominee-note').val();
            guardianReasonForModification = $('#reason-for-modification-guardian-nominee').val();
        }

        filteredDataForNomineeNumber = nomineeDataTable
            .rows()
            .indexes()
            .filter(function (value) {
                return nomineeDataTable.row(value).data()[3] == $('#nomination-number').val();
            });

        if (nomineeDataTable.rows(filteredDataForNomineeNumber).count() > 0 && editedNomineeNumber != $('#nomination-number').val()) {
            isDuplicateNomineeNumber = true;
            result = false;
            $('#nomination-number-error').removeClass('d-none');
        }
        else {
            isDuplicateNomineeNumber = false;
            $('#nomination-number-error').addClass('d-none');
        }

        if (nominationNumber === '' || isDuplicateNomineeNumber === true || parseInt(nominationNumber.length) > 50) {
            result = false;
            $('#nomination-number-error').removeClass('d-none');
        }
        else
            $('#nomination-number-error').addClass('d-none');

        if (IsValidInputDate('#nomination-date') === false) {
            result = false;
            $('#nomination-date-error').removeClass('d-none');
        }
        else
            $('#nomination-date-error').addClass('d-none');

        if (typeof sequenceNumber === 'undefined' || $('#customer-person-id option:selected').text().trim() === 'Select Person') {
            result = false;
            $('#customer-person-id-error').removeClass('d-none');
        }
        else
            $('#customer-person-id-error').addClass('d-none');

        // Check Whether Nominee Is Added Or Not?
        if (nomineeDataTable.rows(filteredDataForNomineeNumber).count() > nominationNumberCount)
            $('#nomination-number-error').removeClass('d-none')

        // Check Whether Person Information Number Selected For Nominee Or Not?
        if (nomineePersonInformationNumber === 'None' || typeof nomineePersonInformationNumber === 'undefined')
            isSelectedPersonInformationNumberForNominee = false;
        else
            isSelectedPersonInformationNumberForNominee = true;

        if ((isSelectedPersonInformationNumberForNominee == false && nameOfNominee === 'None') || parseInt(nameOfNominee.length) < 3 || parseInt(nameOfNominee.length) > 150) {
            result = false;

            $('#nominee-person-id-error').removeClass('d-none');
            $('#name-of-nominee-error').removeClass('d-none');
        }
        else {
            $('#nominee-person-id-error').addClass('d-none');
            $('#name-of-nominee-error').addClass('d-none');
        }

        if (isSelectedPersonInformationNumberForNominee === false) {
            if (transnameOfNominee === '' || transnameOfNominee === 'None' || parseInt(transnameOfNominee.length) > 150) {
                result = false;
                $('#trans-name-of-nominee-error').removeClass('d-none');
            }
            else
                $('#trans-name-of-nominee-error').addClass('d-none');

            if (birthDate == '1900-01-01') {
                result = false;
                $('#nominee-birth-date-error').removeClass('d-none');
            }
            else
                $('#nominee-birth-date-error').addClass('d-none');

            if (fullAddressDetails === 'None' || parseInt(fullAddressDetails.length) > 500) {
                result = false;
                $('#nominee-full-address-details-error').removeClass('d-none');
            }
            else
                $('#nominee-full-address-details-error').addClass('d-none');

            if (transFullAddress === 'None' || parseInt(transFullAddress.length) > 500) {
                result = false;
                $('#trans-nominee-full-address-details-error').removeClass('d-none');
            }
            else
                $('#trans-nominee-full-address-details-error').addClass('d-none');

            if (contactDetails === 'None' || parseInt(contactDetails.length) > 150) {
                result = false;
                $('#nominee-contact-details-error').removeClass('d-none');
            }
            else
                $('#nominee-contact-details-error').addClass('d-none');

            if (transContactDetails === '' || transContactDetails == 'None') {
                result = false;
                $('#trans-nominee-contact-details-error').removeClass('d-none');
            }
            else
                $('#trans-nominee-contact-details-error').addClass('d-none');

        }

        // Validate Relation Id
        if ($('#relation-id').prop('selectedIndex') < 1) {
            result = false;
            $('#relation-id-error').removeClass('d-none');
        }

        // Validate Holding Percentage, If Visible
        if ($('#holding-percentage-input').hasClass('d-none') === false) {
            if (isNaN(holdingPercentage) === false) {
                minimum = parseFloat($('#holding-percentage').attr('min'));
                maximum = parseFloat($('#holding-percentage').attr('max'));
                if (parseFloat(holdingPercentage) < parseFloat(minimum) || parseFloat(holdingPercentage) > parseFloat(maximum)) {
                    result = false;
                    $('#holding-percentage-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#holding-percentage-error').removeClass('d-none');
            }
        }

        // Validate Holding Percentage, If Visible
        if ($('#proportionate-amount-for-each-nominee-input').hasClass('d-none') === false) {
            if (isNaN(proportionateAmountForEachNominee) === false) {
                minimum = parseFloat($('#proportionate-amount-for-each-nominee').attr('min'));
                maximum = parseFloat($('#proportionate-amount-for-each-nominee').attr('max'));
                if (parseFloat(proportionateAmountForEachNominee) < parseFloat(minimum) || parseFloat(proportionateAmountForEachNominee) > parseFloat(maximum)) {
                    result = false;
                    $('#proportionate-amount-for-each-nominee-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#proportionate-amount-for-each-nominee-error').removeClass('d-none');
            }
        }

        // Validate Date
        if (IsValidInputDate('#activation-date-nominee') === false) {
            result = false;
            $('#nominee-activation-date-error').removeClass('d-none');
        }
        else
            $('#activation-date-nominee-error').addClass('d-none');

        if (IsValidInputDate('#expiry-date-nominee') === false) {
            result = false;
            $('#nominee-expiry-date-error').removeClass('d-none');
        }
        else
            $('#expiry-date-nominee-error').addClass('d-none');


        // If Nominee Is Minor (i.e. Adult)
        if ((isAdult) === false) {
            $('#customer-nominee-guardian-accordion-error').removeClass('d-none');

            if ($('#guardian-type-id').prop('selectedIndex') < 1) {
                result = false;
                $('#guardian-type-id-error').removeClass('d-none');
            }

            // Check Whether Person Information Number Selected For Nominee Or Not?
            if (guardianNomineePersonInformationNumber === 'None' || typeof guardianNomineePersonInformationNumber === 'undefined')
                isSelectedPersonInformationNumberForGuardian = false;
            else
                isSelectedPersonInformationNumberForGuardian = true;

            if ((isSelectedPersonInformationNumberForGuardian === false && guardianFullName === 'None') || parseInt(guardianFullName.length) < 3 || parseInt(guardianFullName.length) > 150) {
                debugger;
                result = false;
                isValidGuardianDetails = false;

                $('#nominee-guardian-person-information-number-error').removeClass('d-none');
                $('#guardian-full-name-error').removeClass('d-none');
            }
            else {
                $('#nominee-guardian-person-information-number-error').addClass('d-none');
                $('#guardian-full-name-errorr').addClass('d-none');
            }

            if (isSelectedPersonInformationNumberForGuardian === false) {
                if (transGuardianFullName === '' || transGuardianFullName === 'None' || parseInt(transGuardianFullName.length) > 150) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#trans-guardian-full-name-error').removeClass('d-none');
                }

                if (IsValidInputDate('#guardian-nominee-birth-date') === false) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#guardian-nominee-birth-date-error').removeClass('d-none');
                }

                maximumLength = parseInt($('#guardian-nominee-full-address-details').attr('maxLength'));

                if (parseInt(guardianNomineeFullAddress.length) === 0 || guardianNomineeFullAddress === 'None' || parseInt(guardianNomineeFullAddress.length) > parseInt(maximumLength)) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#guardian-nominee-full-address-details-error').removeClass('d-none');
                }

                maximumLength = parseInt($('#trans-guardian-nominee-full-address-details').attr('maxLength'));

                if (parseInt(transGuardianNomineeFullAddress.length) === 0 || transGuardianNomineeFullAddress == 'None' || parseInt(transGuardianNomineeFullAddress.length) > parseInt(maximumLength)) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#trans-guardian-nominee-full-address-details-error').removeClass('d-none');
                }

                maximumLength = parseInt($('#guardian-nominee-contact-details').attr('maxLength'));

                if (parseInt(guardianContactDetails.length) === 0 || guardianContactDetails == 'None' || parseInt(guardianContactDetails.length) > parseInt(maximumLength)) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#guardian-nominee-contact-details-error').removeClass('d-none');
                }

                maximumLength = parseInt($('#trans-guardian-nominee-contact-details').attr('maxLength'));

                if (parseInt(transGuardianContactDetails.length) === 0 || transGuardianContactDetails == 'None' || parseInt(transGuardianContactDetails.length) > parseInt(maximumLength)) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#trans-guardian-nominee-contact-details-error').removeClass('d-none');
                }
            }

            if ($('.age-proof-sub-status-minor:checked').length === 0) {
                result = false;
                isValidGuardianDetails = false;
                $('#age-proof-sub-status-minor-error').removeClass('d-none');
            }

            if (appointedDateOfContact === '') {
                result = false;
                isValidGuardianDetails = false;
                $('#appointed-date-of-contact-error').removeClass('d-none');
            }

            if (appointedTimeOfContact === '') {
                result = false;
                isValidGuardianDetails = false;
                $('#appointed-time-of-contact-error').removeClass('d-none');
            }
        }
        else
            $('#customer-nominee-guardian-accordion-error').addClass('d-none');

        dataTableRecordCount = parseInt(nomineeDataTable.rows().count());

        // Add Current
        if (editedNomineeNumber == 0)
            dataTableRecordCount = dataTableRecordCount + 1;

        // Check Required Number Of Nominees
        if (parseInt(dataTableRecordCount) < parseInt(minimumNominee))
            minMaxResult = false;

        if (result) {
            if (minMaxResult == false) {
                $('#account-nominee-accordion-error').addClass('d-none');
                $('#customer-nominee-guardian-accordion-error').addClass('d-none');
                $('#account-nominee-accordion-min-max-error').html('Number Of Nominee Are Allowed Between ' + minimumNominee + ' And ' + maximumNominee);
                $('#account-nominee-accordion-min-max-error').removeClass('d-none');
            }
            else {
                $('#account-nominee-accordion-error').addClass('d-none');
                $('#customer-nominee-guardian-accordion-error').addClass('d-none');
                $('#account-nominee-accordion-min-max-error').addClass('d-none');
            }
        }
        else {
            $('#account-nominee-accordion-error').removeClass('d-none');

            if (!isValidGuardianDetails)
                $('#customer-nominee-guardian-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideAccountNomineeDataTableColumns() {
        nomineeDataTable.column(1).visible(false);
        nomineeDataTable.column(5).visible(false);
        nomineeDataTable.column(8).visible(false);
        nomineeDataTable.column(15).visible(false);
        nomineeDataTable.column(21).visible(false);
        nomineeDataTable.column(24).visible(false);
        nomineeDataTable.column(27).visible(false);
        nomineeDataTable.column(29).visible(false);
        nomineeDataTable.column(36).visible(false);
        nomineeDataTable.column(42).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@   Contact Details - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-contact-dt').click(function (event) {
        event.preventDefault();
        $('#btn-add-contact-modal').removeClass('read-only');
        $('#send-code').addClass('d-none');
        $('#resend').addClass('d-none');
        SetModalTitle('contact', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-contact-dt').click(function () {
        SetModalTitle('contact', 'Edit');
        isChecked = $('.checks').is(':checked');
        $('#btn-update-contact-modal').removeClass('read-only');

        if (isChecked) {
            columnValues = $('#btn-edit-contact-dt').data('rowindex');
            id = $('#contact-modal').attr('id');
            myModal = $('#' + id).modal();

            $.get('/PersonChildAction/GetPersonContactDetailEntryStatus', { _personContactDetailPrmKey: columnValues[8], async: false }, function (data) {
                entryStatus = data;
            });

            //// Display Value In Modal Inputs
            isMobile = columnValues[2].includes('Mobile');
            isEmail = columnValues[2].includes('Email');

            $('#resend').addClass('d-none');

            if ((columnValues[8] != 0) && (columnValues[4] == true)) {
                myModal.modal({ show: false });
            }
            else {
                if ((columnValues[8] != 0) && entryStatus == 'VRF') {
                    $('#contact-detail-new').addClass('read-only');
                }
                else {
                    $('#contact-detail-new').removeClass('read-only');
                }

                myModal.modal({ show: true });
            }

            $('#send-code').removeClass('d-none');


            // Add Field Value Attribute Type As Number For Mobile Contact Type
            if (isMobile) {
                $('#field-value').attr('type', 'number');
                $('.is-verified-field').addClass('d-none');
                $('#send-code').removeClass('d-none');
                $('.verification-code').removeClass('d-none');
                $('#verification-code').val('');
            }
            else {
                $('#field-value').removeAttr('type');
                $('#send-code').addClass('d-none');
                $('.is-verified-field').removeClass('d-none');
                $('#resend').addClass('d-none');
                $('.verification-code').addClass('d-none');
                $('#verification-code').val('0');
            }

            // Display Value In Modal Inputs
            $('#contact-type', myModal).val(columnValues[1]);
            $('#field-value', myModal).val(columnValues[3]);

            if (columnValues[4] == 'True' || columnValues[4] == 'Yes') {
                $('#is-verified').prop('checked', true)
            }
            else {
                $('#is-verified').prop('checked', false)
            }

            $('#verification-code', myModal).val('');
            $('#note-contact-detail', myModal).val(columnValues[6]);

            personContactDetailPrmkey = columnValues[8];
            customerAccountPrmKey = columnValues[9];

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-contact-dt').addClass('read-only');
            $('#contact-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-contact-modal').click(function () {
        if (IsValidContactDataTableModal()) {
            if (isMobile) {
                $('#btn-add-contact-modal').addClass('read-only');

                $.get('/SMS/ValidateVerificationToken', { MobileNumber: fieldValue, Token: verificationCode, async: false }, function (data) {
                    if (data) {
                        $('#verification-token-error').addClass('d-none');

                        row = contactDataTable.row.add([
                            tag,
                            contactType,
                            contactTypeText,
                            fieldValue,
                            isVerified,
                            verificationCode,
                            note,
                            reasonForModification,
                            personContactDetailPrmKey,
                            customerAccountPrmKey
                        ]).draw();

                        HideContactDataTableColumns();

                        contactDataTable.columns.adjust().draw();

                        // Hide Table Required Data Error Message
                        $('#contact-accordion-error').addClass('d-none');

                        $('#contact-modal').modal('hide');

                        EnableNewOperation('contact');
                    }
                    else {
                        $('#verification-token-error').removeClass('d-none');
                    }
                })
            }
            else {
                $('#verification-token-error').addClass('d-none');

                row = contactDataTable.row.add([
                    tag,
                    contactType,
                    contactTypeText,
                    fieldValue,
                    isVerified,
                    verificationCode,
                    note,
                    reasonForModification,
                    personContactDetailPrmKey,
                    customerAccountPrmKey
                ]).draw();

                HideContactDataTableColumns();

                contactDataTable.columns.adjust().draw();

                // Hide Table Required Data Error Message
                $('#contact-accordion-error').addClass('d-none');

                $('#contact-modal').modal('hide');

                EnableNewOperation('contact');
            }
        }
    });

    // Modal update Button Event
    $('#btn-update-contact-modal').click(function () {
        $('#select-all-contact').prop('checked', false);

        if (IsValidContactDataTableModal()) {
            if (isMobile) {
                $('#btn-update-contact-modal').addClass('read-only');
                $.get('/SMS/ValidateVerificationToken', { MobileNumber: fieldValue, Token: verificationCode, async: false }, function (data) {
                    if (data) {
                        $('#verification-token-error').addClass('d-none');

                        row = contactDataTable.row(selectedRowIndex).data([
                            tag,
                            contactType,
                            contactTypeText,
                            fieldValue,
                            isVerified,
                            verificationCode,
                            note,
                            reasonForModification,
                            personContactDetailPrmKey,
                            customerAccountPrmKey
                        ]).draw();

                        HideContactDataTableColumns();

                        contactDataTable.columns.adjust().draw();

                        // Hide Table Required Data Error Message
                        $('#contact-accordion-error').addClass('d-none');

                        $('#contact-modal').modal('hide');

                        EnableNewOperation('contact');
                    }
                    else {
                        $('#verification-token-error').removeClass('d-none');
                    }
                })
            }
            else {
                $('#verification-token-error').addClass('d-none');

                row = contactDataTable.row(selectedRowIndex).data([
                    tag,
                    contactType,
                    contactTypeText,
                    fieldValue,
                    isVerified,
                    verificationCode,
                    note,
                    reasonForModification,
                    personContactDetailPrmKey,
                    customerAccountPrmKey
                ]).draw();

                HideContactDataTableColumns();

                contactDataTable.columns.adjust().draw();

                // Hide Table Required Data Error Message
                $('#contact-accordion-error').addClass('d-none');

                $('#contact-modal').modal('hide');

                EnableNewOperation('contact');
            }
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-contact-dt').click(function () {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-contact tbody input[type="checkbox"]:checked').each(function () {
                    contactDataTable.row($('#tbl-contact tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-contact-dt').data('rowindex');
                    EnableNewOperation('contact');

                    $('#select-all-contact').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!contactDataTable.data().any())
                        $('#contact-accordion-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
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
    $('#tbl-contact tbody').click('input[type=checkbox]', function () {
        $('#tbl-contact input[type="checkbox"]:checked').each(function () {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');

                selectedRowIndex = contactDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (contactDataTable.row(selectedRowIndex).data());

                    contactDetailPrmKey = rowData[8];

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
        if (checked.length == 1) {
            if (contactDetailPrmKey > 0)
                EnableDeleteOperation('contact');
            else
                EnableEditDeleteOperation('contact');
        }


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

        // Check For Duplicate Contact Number
        if (isDuplicateContact === false) {
            // Get Modal Inputs In Local letiable
            tag = '<input type="checkbox" name="select-all" class="checks"/>';
            contactType = $('#contact-type option:selected').val();
            contactTypeText = $('#contact-type option:selected').text();
            fieldValue = $('#field-value').val();
            isVerified = $('input[name="PersonContactDetailViewModel.IsVerified"]').is(':checked') ? 'True' : "False";
            note = $('#note-contact-detail').val();
            verificationCode = $('#verification-code').val();
            personContactDetailPrmKey = 0;
            customerAccountPrmKey = 0;
            reasonForModification = $('#reason-for-modification-contact').val();
            hasDivClass = $('#contact-div').hasClass('d-none');

            // Set Default Value, If Empty
            if (note === '')
                note = 'None';

            if ($('#contact-type').prop('selectedIndex') < 1) {
                result = false;
                $('#contact-type-error').removeClass('d-none');
            }
            else
                $('#contact-type-error').addClass('d-none');


            // Validate If Contact Type Is Mobile
            if (isMobile) {
                // Define a regular expression pattern for a 10-digit mobile number
                let regex = /^\d{10}$/;
                let verificationCode = $('#verification-code').val();

                // mobileNumber
                if (regex.test(fieldValue) === false) {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
                else
                    $('#field-value-error').addClass('d-none');

                // Mobile OTP Validation
                if (verificationCode === '' || verificationCode === '0') {
                    result = false;
                    $('#verification-token-error').removeClass('d-none');
                }
            }
            else {
                if (fieldValue == '' || parseInt(fieldValue.length) > 50) {
                    result = false;
                    $('#verification-token-error').addClass('d-none');
                    $('#field-value-error').removeClass('d-none');
                }
            }
        }
        else
            result = false;

        return result;
    }

    // Hide Unnecessary Columns
    function HideContactDataTableColumns() {
        contactDataTable.column(1).visible(false);
        contactDataTable.column(7).visible(false);
        contactDataTable.column(8).visible(false);
        contactDataTable.column(9).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@   Person  Address Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-person-address-dt').click(function (event) {
        event.preventDefault();
        editedAddressTypeId = '';
        SetAddressTypeUniqueDropdownList();
        SetModalTitle('person-address', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-person-address-dt').click(function () {
        SetModalTitle('person-address', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-person-address-dt').data('rowindex');
            id = $('#person-address-modal').attr('id');
            editedAddressTypeId = columnValues[1];

            myModal = $('#' + id).modal();

            //// Display Value In Modal Inputs
            // Get Person Address EntryStatus
            $.get('/PersonChildAction/GetDocumentValidations', { _personAddressDetailPrmKey: columnValues[21], async: false }, function (data) {
                entryStatus = data;
            });

            if ((columnValues[21] != 0) && (entryStatus == 'VRF'))
                $('.person-address-details').addClass('read-only');
            else
                $('.person-address-details').removeClass('read-only');

            SetAddressTypeUniqueDropdownList();

            // Display Value In Modal Inputs
            $('#address-type-id', myModal).val(columnValues[1]);
            $('#flat-door-no', myModal).val(columnValues[3]);
            $('#trans-flat-door-no', myModal).val(columnValues[4]);
            $('#building-name', myModal).val(columnValues[5]);
            $('#trans-building-name', myModal).val(columnValues[6]);
            $('#road-name', myModal).val(columnValues[7]);
            $('#trans-road-name', myModal).val(columnValues[8]);
            $('#area-name', myModal).val(columnValues[9]);
            $('#trans-area-name', myModal).val(columnValues[10]);
            $('#city-id', myModal).val(columnValues[11]);
            $('#residence-type-id', myModal).val(columnValues[13]);
            $('#residence-ownership-id', myModal).val(columnValues[15]);
            $('#note-address', myModal).val(columnValues[18]);
            $('#trans-note-address', myModal).val(columnValues[19]);
            $('#reason-for-modification-address', myModal).val(columnValues[20]);

            $('#is-verified-address').prop('checked', columnValues[17].toString().toLowerCase() === 'true' ? true : false);

            personAddressPrmKey = columnValues[21];
            customerAccountPrmKey = columnValues[22];

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-person-address-dt').addClass('read-only');
            $('#person-address-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-person-address-modal').click(function () {
        if (IsValidAddressDataTableModal()) {
            row = addressDataTable.row.add([
                tag,
                addressType,
                addressTypeText,
                flatDoorNo,
                transFlatDoorNo,
                buildingName,
                transBuildingName,
                roadName,
                transRoadName,
                areaName,
                transAreaName,
                city,
                cityText,
                residenceType,
                residenceTypeText,
                residenceOwnership,
                residenceOwnershipText,
                isVerified,
                note,
                transNote,
                reasonForModification,
                personAddressPrmKey,
                customerAccountPrmKey
            ]).draw();

            HideAddressDataTableColumns();

            addressDataTable.columns.adjust().draw();

            // Hide Table Required Data Error Message
            $('#address-accordion-error').addClass('d-none');

            $('#person-address-modal').modal('hide');

            EnableNewOperation('person-address');
        }
    });

    // Modal update Button Event
    $('#btn-update-person-address-modal').click(function () {
        $('#select-all-person-address').prop('checked', false);
        if (IsValidAddressDataTableModal()) {
            addressDataTable.row(selectedRowIndex).data([
                tag,
                addressType,
                addressTypeText,
                flatDoorNo,
                transFlatDoorNo,
                buildingName,
                transBuildingName,
                roadName,
                transRoadName,
                areaName,
                transAreaName,
                city,
                cityText,
                residenceType,
                residenceTypeText,
                residenceOwnership,
                residenceOwnershipText,
                isVerified,
                note,
                transNote,
                reasonForModification,
                personAddressPrmKey,
                customerAccountPrmKey
            ]).draw();

            HideAddressDataTableColumns();

            addressDataTable.columns.adjust().draw();

            $('#person-address-modal').modal('hide');

            EnableNewOperation('person-address');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-person-address-dt').click(function () {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-person-address tbody input[type="checkbox"]:checked').each(function () {
                    addressDataTable.row($('#tbl-person-address tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-person-address-dt').data('rowindex');

                    EnableNewOperation('person-address');

                    SetAddressTypeUniqueDropdownList();

                    $('#select-all-person-address').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!addressDataTable.data().any())
                        $('#address-accordion-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-person-address').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-person-address tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = addressDataTable.row(row).index();

                rowData = (addressDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-person-address-dt').data('rowindex', arr);
                EnableDeleteOperation('person-address')
            });
        }
        else {
            EnableNewOperation('person-address')

            $('#tbl-person-address tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-person-address tbody').click('input[type=checkbox]', function () {
        $('#tbl-person-address tbody input[type="checkbox"]:checked').each(function () {
            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = addressDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (addressDataTable.row(selectedRowIndex).data());

                    personAddressPrmKey = rowData[21];

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('person-address');

                    $('#btn-update-person-address-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-person-address-dt').data('rowindex', rowData);
                    $('#btn-delete-person-address-dt').data('rowindex', arr);
                    $('#select-all-person-address').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-person-address tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('person-address');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1) {
            if (personAddressPrmKey > 0)
                EnableDeleteOperation('person-address');
            else
                EnableEditDeleteOperation('person-address');
        }

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('person-address');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-person-address').prop('checked', true);
        else
            $('#select-all-person-address').prop('checked', false);
    });

    // Validate Agent Incentive Module
    function IsValidAddressDataTableModal() {
        result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        addressType = $('#address-type-id option:selected').val();
        addressTypeText = $('#address-type-id option:selected').text();
        flatDoorNo = $('#flat-door-no').val();
        transFlatDoorNo = $('#trans-flat-door-no').val();
        buildingName = $('#building-name').val();
        transBuildingName = $('#trans-building-name').val();
        roadName = $('#road-name').val();
        transRoadName = $('#trans-road-name').val();
        areaName = $('#area-name').val();
        transAreaName = $('#trans-area-name').val();
        city = $('#city-id option:selected').val();
        cityText = $('#city-id option:selected').text();
        residenceType = $('#residence-type-id option:selected').val();
        residenceTypeText = $('#residence-type-id option:selected').text();
        residenceOwnership = $('#residence-ownership-id option:selected').val();
        residenceOwnershipText = $('#residence-ownership-id option:selected').text();
        isVerified = $('input[name="PersonAddressViewModel.IsVerified"]').is(':checked') ? 'True' : "False";
        note = $('#note-address').val();
        transNote = $('#trans-note-address').val();
        reasonForModification = $('#reason-for-modification-address').val();
        hasDivClass = $('#address-div').hasClass('d-none');
        personAddressPrmKey = 0;
        customerAccountPrmKey = 0;

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        if (transNote === '')
            transNote = 'None';

        if (hasDivClass == true) {
            reasonForModification = 'None';
        }
        else {
            if (reasonForModification == '')
                reasonForModification = 'None';
        }


        //Validation Address Type
        if ($('#address-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#address-type-id-error').removeClass('d-none');
        }

        //Validation FlatDoor No Min Length - 3 And Max Length = 50
        if (isNaN(flatDoorNo.length) === false) {
            minimumLength = parseInt($('#flat-door-no').attr('minlength'));
            maximumLength = parseInt($('#flat-door-no').attr('maxlength'));
            if (parseInt(flatDoorNo.length) < parseInt(minimumLength) || parseInt(flatDoorNo.length) > parseInt(maximumLength)) {
                result = false;
                $('#flat-door-no-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#flat-door-no-error').removeClass('d-none');
        }

        //Validation Trans FlatDoor No
        if (transFlatDoorNo === '') {
            result = false;
            $('#trans-flat-door-no-error').removeClass('d-none');
        }
        else {
            $('#trans-flat-door-no-error').addClass('d-none');
        }


        //Validation Building Name
        if (isNaN(buildingName.length) === false) {
            minimumLength = parseInt($('#building-name').attr('minlength'));
            maximumLength = parseInt($('#building-name').attr('maxlength'));
            if (parseInt(buildingName.length) < parseInt(minimumLength) || parseInt(buildingName.length) > parseInt(maximumLength)) {
                result = false;
                $('#building-name-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#building-name-error').removeClass('d-none');
        }

        //Validation Trans Building Name
        if (transBuildingName === '') {
            result = false;
            $('#trans-building-name-error').removeClass('d-none');
        } else {
            $('#trans-building-name-error').addClass('d-none');
        }

        //Validation Road Name
        if (isNaN(roadName.length) === false) {
            minimumLength = parseInt($('#road-name').attr('minlength'));
            maximumLength = parseInt($('#road-name').attr('maxlength'));
            if (parseInt(roadName.length) < parseInt(minimumLength) || parseInt(roadName.length) > parseInt(maximumLength)) {
                result = false;
                $('#road-name-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#road-name-error').removeClass('d-none');
        }

        //Validation Road Name
        if (transRoadName === '') {
            result = false;
            $('#trans-road-name-error').removeClass('d-none');
        } else {
            $('#trans-road-name-error').addClass('d-none');
        }


        //Validation Area Name
        if (isNaN(areaName.length) === false) {
            minimumLength = parseInt($('#area-name').attr('minlength'));
            maximumLength = parseInt($('#area-name').attr('maxlength'));
            if (parseInt(areaName.length) < parseInt(minimumLength) || parseInt(areaName.length) > parseInt(maximumLength)) {
                result = false;
                $('#area-name-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#area-name-error').removeClass('d-none');
        }

        //Validation Trans Area Name
        if (transAreaName === '') {
            result = false;
            $('#trans-area-name-error').removeClass('d-none');
        } else {
            $('#trans-area-name-error').addClass('d-none');
        }

        //Validation City
        if ($('#city-id').prop('selectedIndex') < 1) {
            result = false;
            $('#city-id-error').removeClass('d-none');
        }

        //Validation Residence Type
        if ($('#residence-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#residence-type-id-error').removeClass('d-none');
        }

        //Validation Residence Ownership
        if ($('#residence-ownership-id').prop('selectedIndex') < 1) {
            result = false;
            $('#residence-ownership-id-error').removeClass('d-none');
        }
        return result;
    }

    // Hide Unnecessary Columns
    function HideAddressDataTableColumns() {
        addressDataTable.column(1).visible(false);
        addressDataTable.column(11).visible(false);
        addressDataTable.column(13).visible(false);
        addressDataTable.column(15).visible(false);
        addressDataTable.column(20).visible(false);
        addressDataTable.column(21).visible(false);
        addressDataTable.column(22).visible(false);
    }

    // Address Type
    function SetAddressTypeUniqueDropdownList() {
        // Show All List Items
        $('#address-type-id').html('');
        $('#address-type-id').append(ADDRESS_TYPE_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-person-address > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (addressDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
                if (myColumnValues[1] != editedAddressTypeId)
                    $('#address-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    // Document Unique Dropdown
    // Address Type Unique Dropdown
    function SetDocumentUniqueDropdownList() {
        // Show All List Items
        $('#document-id').html('');
        $('#document-id').append(documentDropdownList);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-document > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (documentDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedDocumentId)
                    $('#document-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme  Notice Schedule - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

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
            $('#btn-edit-notice-schedule-dt').addClass('read-only');
            $('#notice-schedule-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-notice-schedule-modal').click(function () {
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

            $('#notice-schedule-modal').modal('hide');

            EnableNewOperation('notice-schedule');
        }
    });

    // Modal update Button Event
    $('#btn-update-notice-schedule-modal').click(function () {
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

            $('#notice-schedule-modal').modal('hide');

            EnableNewOperation('notice-schedule');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-notice-schedule-dt').click(function () {
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
            alert('Please Select A Checkbox');
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
                $('input[type="checkbox"]:checked').each(function () {
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

    // Validate Agent Incentive Module
    function IsValidNoticeScheduleDataTableModal() {
        // Get Modal Inputs In Local letiables
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        noticeTypeId = $('#notice-type-id option:selected').val();
        noticeScheduleText = $('#notice-type-id option:selected').text();
        communicationMediaId = $('#comunication-media-id option:selected').val();
        communicationMediaText = $('#comunication-media-id option:selected').text();
        scheduleId = $('#schedule-id option:selected').val();
        scheduleText = $('#schedule-id option:selected').text();
        note = $('#note-notice-type').val();

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        // Validate Modal Inputs
        if ((noticeTypeId.trim().length < 36) || (communicationMediaId.trim().length < 36) || (scheduleId.trim().length < 36)) {
            if (noticeTypeId.trim().length < 36)
                $('#notice-type-id-error').removeClass('d-none');

            if (communicationMediaId.trim().length < 36)
                $('#comunication-media-id-error').removeClass('d-none');

            if (scheduleId.trim().length < 36)
                $('#schedule-id-error').removeClass('d-none');

            return false;
        }
        else
            return true;
    }

    // Hide Unnecessary Columns
    function HideNoticeScheduleDataTableColumns() {
        noticeScheduleDataTable.column(1).visible(false);
        noticeScheduleDataTable.column(3).visible(false);
        noticeScheduleDataTable.column(5).visible(false);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Garantor Detail DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-guarantor-detail-dt').click(function (event) {
        debugger;
        editedSequenceNumber = 0;
        guarantorId = '';
        guarantorName = '';

        // Hide Joint Account Change Notification
        $('#guarantor-detail-change-info').addClass('d-none');

        dataTableRecordCount = guarantorDetailDataTable.rows().count();

        if (parseInt(dataTableRecordCount) >= parseInt(maximumNumberOfGuarantor)) {
            $('#guarantor-detail-modal').modal('hide');
            alert('Number Of Guarantor Are Allowed Between ' + minimumNumberOfGuarantor + ' And ' + maximumNumberOfGuarantor);
        }
        else {
            event.preventDefault();
            SetModalTitle('guarantor-detail', 'Add');
        }
    });

    // DataTable Edit Button 
    $('#btn-edit-guarantor-detail-dt').click(function () {
        debugger;
        SetModalTitle('guarantor-detail', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-guarantor-detail-dt').data('rowindex');
            id = $('#guarantor-detail-modal').attr('id');
            myModal = $('#' + id).modal();

            editedGuarantorId = columnValues[1];
            guarantorId = columnValues[1];
            guarantorName = columnValues[2];

            // Display Value In Modal Inputs
            $('#guarantor-name-id', myModal).val(columnValues[2]);
            $('#guarantee-percentage', myModal).val(columnValues[4]);
            $('#sequence-number-guarantor-detail', myModal).val(columnValues[3]);

            editedSequenceNumber = columnValues[3];

            $('#note-account-guarantor-detail', myModal).val(columnValues[5]);

            // Show Modals
            $('#guarantor-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-guarantor-detail-dt').addClass('read-only');
            $('#guarantor-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-guarantor-detail-modal').click(function () {
        debugger;
        if (IsValidGuarantorDetailDataTableModal()) {
            row = guarantorDetailDataTable.row.add([
                tag,
                guarantorId,
                guarantorName,
                sequenceNumber,
                guaranteePercentage,
                note,
            ]).draw();

            // Error Message In Span
            $('#guarantor-detail-validation span').html('');

            HideGuarantorDetailDataTableColumns();

            guarantorDetailDataTable.columns.adjust().draw();

            ClearModal('guarantor-detail');

            $('#guarantor-detail-modal').modal('hide');

            EnableNewOperation('guarantor-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-guarantor-detail-modal').click(function () {
        debugger;
        $('#select-all-guarantor-detail').prop('checked', false);
        if (IsValidGuarantorDetailDataTableModal()) {
            guarantorDetailDataTable.row(selectedRowIndex).data([
                tag,
                guarantorId,
                guarantorName,
                sequenceNumber,
                guaranteePercentage,
                note,

            ]).draw();
            // Error Message In Span
            $('#guarantor-detail-validation span').html('');

            HideGuarantorDetailDataTableColumns();

            guarantorDetailDataTable.columns.adjust().draw();

            $('#guarantor-detail-modal').modal('hide');

            EnableNewOperation('guarantor-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-guarantor-detail-dt').click(function () {
        debugger;
        isChecked = $("input[type='checkbox']").is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-guarantor-detail tbody input[type='checkbox']:checked").each(function () {
                    guarantorDetailDataTable.row($("#tbl-guarantor-detail tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-guarantor-detail-dt').data('rowindex');
                    EnableNewOperation('guarantor-detail');

                    $('#select-all-guarantor-detail').prop('checked', false);
                }));

                // Validate Required Number Of Joint Account Holders.
                dataTableRecordCount = guarantorDetailDataTable.rows().count();

                if (parseInt(dataTableRecordCount) < parseInt(minimumNumberOfGuarantor)) {
                    result = false;
                    minMaxResult = false;

                    $('#guarantor-detail-min-max-error').html('Number Of Guarantor Are Allowed Between ' + minimumNumberOfGuarantor + ' And ' + maximumNumberOfGuarantor);

                    $('#guarantor-detail-accordion-error').addClass('d-none');
                    $('#guarantor-detail-min-max-error').removeClass('d-none');
                }
            }


        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-guarantor-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-guarantor-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = guarantorDetailDataTable.row(row).index();

                rowData = (guarantorDetailDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-guarantor-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('guarantor-detail')
            });
        }
        else {
            EnableNewOperation('guarantor-detail')

            $('#tbl-guarantor-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-guarantor-detail tbody').click("input[type=checkbox]", function () {
        $('#tbl-guarantor-detail input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = guarantorDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (guarantorDetailDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('guarantor-detail');

                    $('#btn-update-guarantor-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-guarantor-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-guarantor-detail-dt').data('rowindex', arr);
                    $('#select-all-guarantor-detail').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-guarantor-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('guarantor-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('guarantor-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('guarantor-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-guarantor-detail').prop('checked', true);
        else
            $('#select-all-guarantor-detail').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-guarantor-detail > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (guarantorDetailDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#guarantor-name-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate  Garantor Detail Module
    function IsValidGuarantorDetailDataTableModal() {
        debugger;
        minMaxResult = true;
        result = true;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        sequenceNumber = parseInt($('#sequence-number-guarantor-detail').val());
        guaranteePercentage = parseFloat($('#guarantee-percentage').val());
        note = $('#note-account-guarantor-detail').val();

        // Set Default Value, If Empty
        if (reasonForModification === '')
            reasonForModification = 'None';

        if (note === '')
            note = 'None';

        if (guarantorId === '') {
            result = false;
            $('#guarantor-name-id-error').removeClass('d-none');
        }

        //Validation Sequence Number
        if (isNaN(sequenceNumber) === false) {
            if (parseInt(sequenceNumber) < 1 || parseInt(sequenceNumber) > 199) {
                result = false;
                $('#sequence-number-guarantor-detail-error').removeClass('d-none');
            }
            else {
                // Get Entered Sequence Number
                let filteredData = guarantorDetailDataTable
                    .rows()
                    .indexes()
                    .filter(function (value) { return guarantorDetailDataTable.row(value).data()[3] == $('#sequence-number-guarantor-detail').val(); });

                // Validate Sequence Number For Duplication
                if (filteredData.length > 0 && parseInt(editedSequenceNumber) !== sequenceNumber) {
                    isDuplicateSequenceNumber = true;
                    result = false;
                    $('#sequence-number-guarantor-detail-error').text('Sequence Number Already Exists.').removeClass('d-none');
                } else {
                    isDuplicateSequenceNumber = false;
                    $('#sequence-number-guarantor-detail-error').addClass('d-none');
                }
            }
        }
        else {
            result = false;
            $('#sequence-number-guarantor-detail-error').removeClass('d-none');
        }

        if (isNaN(guaranteePercentage) === false) {
            minimum = $('#guarantee-percentage').attr('min');
            maximum = $('#guarantee-percentage').attr('max');

            if (parseFloat(guaranteePercentage) < parseFloat(minimum) || parseFloat(guaranteePercentage) > parseFloat(maximum)) {
                result = false;
                $('#guarantee-percentage-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#guarantee-percentage-error').removeClass('d-none');
        }

        // Add + 1 (i.e. Current Row Count)
        if (editedSequenceNumber == 0)
            dataTableRecordCount = dataTableRecordCount + 1;

        if (parseInt(dataTableRecordCount) < parseInt(minimumNumberOfGuarantor)) {
            minMaxResult = false;
            $('#guarantor-detail-min-max-error').html('Number Of Guarantor Detail Are Allowed Between ' + minimumNumberOfGuarantor + ' And ' + maximumNumberOfGuarantor);
        }

        if (result) {
            if (minMaxResult === false) {
                $('#guarantor-detail-accordion-error').addClass('d-none');
                $('#guarantor-detail-min-max-error').removeClass('d-none');
            }
            else {
                $('#guarantor-detail-accordion-error').addClass('d-none');
                $('#guarantor-detail-min-max-error').addClass('d-none');
            }
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideGuarantorDetailDataTableColumns() {
        guarantorDetailDataTable.column(1).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@ Gold Loan collateral Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-gold-collateral-detail-dt').click(function (event) {
        debugger;
        editedSequenceNumber = 0;
        event.preventDefault();
        GoldCollateralHideSection();
        SetModalTitle('gold-collateral-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-gold-collateral-detail-dt').click(function () {
        debugger;
        SetModalTitle('gold-collateral-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            myModal = $('#gold-collateral-detail-modal').modal();

            columnValues = $('#btn-edit-gold-collateral-detail-dt').data('rowindex');

            $('#metal-net-weight').attr('max', Math.max(0, columnValues[10] - columnValues[12]));

            // Display Value In Modal Inputs
            $('#jewel-assayer-id', myModal).val(columnValues[1]);
            $('#gold-ornament-id', myModal).val(columnValues[3]);
            $('#sequence-number-gold-collateral', myModal).val(columnValues[5]);
            editedSequenceNumber = columnValues[5];
            $('#metal-purity-id', myModal).val(columnValues[6]);
            $('#huid', myModal).val(columnValues[8]);
            $('#qty', myModal).val(columnValues[9]);
            $('#metal-gross-weight', myModal).val(columnValues[10]);

            $('#enable-any-damage').prop('checked', columnValues[11].toString().toLowerCase() === 'true' ? true : false);

            $('#damage-description', myModal).val(columnValues[13]);
            $('#damage-weight', myModal).val(columnValues[12]);

            $('#enable-any-westage').prop('checked', columnValues[14].toString().toLowerCase() === 'true' ? true : false);

            $('#westage-description', myModal).val(columnValues[16]);
            $('#westage-weight', myModal).val(columnValues[15]);

            $('#enable-diamond').prop('checked', columnValues[18].toString().toLowerCase() === 'true' ? true : false);

            $('#enable-diamond-deductable').prop('checked', columnValues[17].toString().toLowerCase() === 'true' ? true : false);

            $('#number-of-diamond', myModal).val(columnValues[19]);
            $('#diamond-carat', myModal).val(columnValues[20]);
            $('#clarity-colour', myModal).val(columnValues[21]);
            $('#diamond-weight', myModal).val(columnValues[22]);
            $('#diamond-price', myModal).val(columnValues[23]);
            $('#diamond-valuation', myModal).val(columnValues[24]);
            $('#metal-net-weight', myModal).val(columnValues[25]);
            $('input[name="CustomerGoldLoanCollateralDetailViewModel.CustodyStatus"][value=' + columnValues[27] + ']').prop('checked', true);
            $('#jewel-assayer-remark', myModal).val(columnValues[29]);
            $('#note-gold-loan-collateral-detail', myModal).val(columnValues[30]);
            $('#reason-for-modification-gold-loan', myModal).val(columnValues[31]);

            GoldCollateralHideSection();

            // Show Modals
            $('#gold-collateral-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-gold-collateral-detail-dt').addClass('read-only');
            $('#gold-collateral-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-gold-collateral-detail-modal').click(function () {
        debugger;
        if (IsValidgoldLoanCollateralDetailModal()) {
            row = goldCollateralDetailDataTable.row.add([

                tag,
                jewelAssayerId,
                jewelAssayerText,
                goldOrnamentId,
                goldOrnamentText,
                sequenceNumber,
                metalPurity,
                metalPurityText,
                huid,
                qty,
                metalGrossWeight,
                hasAnyDamage,
                damageWeight,
                damageDescription,
                hasAnyWestage,
                westageWeight,
                westageDescription,
                isDiamondDeductable,
                hasDiamond,
                numberOfDiamond,
                diamondCarat,
                clarityColour,
                diamondWeight,
                diamondPrice,
                diamondValuation,
                metalNetWeight,
                valuationAmount,
                custodyStatus,
                custodyStatusText,
                jewelAssayerRemark,
                note,
                reasonForModification,
                //valuationAmount,
                //marketValue,
            ]).draw();

            HidegoldLoanCollateralDetail();

            goldCollateralDetailDataTable.columns.adjust().draw();

            // Hide Table Required Data Error Message
            $('#gold-collateral-detail-accordion-error').addClass('d-none');

            $('#gold-collateral-detail-modal').modal('hide');

            EnableNewOperation('gold-collateral-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-gold-collateral-detail-modal').click(function () {
        debugger;
        $('#select-all-gold-collateral-detail').prop('checked', false);
        if (IsValidgoldLoanCollateralDetailModal()) {
            goldCollateralDetailDataTable.row(selectedRowIndex).data([

                tag,
                jewelAssayerId,
                jewelAssayerText,
                goldOrnamentId,
                goldOrnamentText,
                sequenceNumber,
                metalPurity,
                metalPurityText,
                huid,
                qty,
                metalGrossWeight,
                hasAnyDamage,
                damageWeight,
                damageDescription,
                hasAnyWestage,
                westageWeight,
                westageDescription,
                isDiamondDeductable,
                hasDiamond,
                numberOfDiamond,
                diamondCarat,
                clarityColour,
                diamondWeight,
                diamondPrice,
                diamondValuation,
                metalNetWeight,
                valuationAmount,
                custodyStatus,
                custodyStatusText,
                jewelAssayerRemark,
                note,
                reasonForModification,
                //valuationAmount,
                //marketValue,


            ]).draw();

            HidegoldLoanCollateralDetail();

            goldCollateralDetailDataTable.columns.adjust().draw();

            $('#gold-collateral-detail-modal').modal('hide');

            EnableNewOperation('gold-collateral-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-gold-collateral-detail-dt').click(function () {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-gold-collateral-detail tbody input[type="checkbox"]:checked').each(function () {
                    goldCollateralDetailDataTable.row($('#tbl-gold-collateral-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-gold-collateral-detail-dt').data('rowindex');
                    EnableNewOperation('gold-collateral-detail');
                    $('#select-all-gold-collateral-detail').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!goldCollateralDetailDataTable.data().any())
                        $('#gold-collateral-detail-accordion-error').removeClass('d-none');


                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-gold-collateral-detail').click(function () {
        debugger;
        if ($(this).prop('checked')) {
            $('#tbl-gold-collateral-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (goldCollateralDetailDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-gold-collateral-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('gold-collateral-detail')
            });
        }
        else {
            EnableNewOperation('gold-collateral-detail')

            $('#tbl-gold-collateral-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-gold-collateral-detail tbody').click('input[type="checkbox"]', function () {
        $('#tbl-gold-collateral-detail input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = goldCollateralDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (goldCollateralDetailDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('gold-collateral-detail');

                    $('#btn-update-gold-collateral-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-gold-collateral-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-gold-collateral-detail-dt').data('rowindex', arr);
                    $('#select-all-gold-collateral-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-gold-collateral-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('gold-collateral-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('gold-collateral-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('gold-collateral-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-gold-collateral-detail').prop('checked', true);
        else
            $('#select-all-gold-collateral-detail').prop('checked', false);
    });

    //// On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    //$('#tbl-gold-collateral-detail > tbody > tr').each(function () {
    //    currentRow = $(this).closest('tr');
    //    columnValues = (goldCollateralDetailDataTable.row(currentRow).data());

    //    if (typeof columnValues != 'undefined' && columnValues != null)
    //        $('#jewel-assayer-id').find("option[value='" + columnValues[1] + "']").hide();
    //    else
    //        return true;
    //});

    // Validate Fund Module        changes done by indrayani
    function IsValidgoldLoanCollateralDetailModal() {
        debugger;

        // Get Modal Inputs In Local letiable    
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        jewelAssayerId = $('#jewel-assayer-id option:selected').val();
        jewelAssayerText = $('#jewel-assayer-id option:selected').text();
        goldOrnamentId = $('#gold-ornament-id option:selected').val();
        goldOrnamentText = $('#gold-ornament-id option:selected').text();
        sequenceNumber = parseInt($('#sequence-number-gold-collateral').val());
        metalPurity = $('#metal-purity-id option:selected').val();
        metalPurityText = $('#metal-purity-id option:selected').text();
        huid = $('#huid').val();
        qty = parseInt($('#qty').val());
        hasAnyDamage = $('#enable-any-damage').is(':checked') ? 'True' : 'False';
        damageDescription = $('#damage-description').val();
        damageWeight = $('#damage-weight').val();
        hasAnyWestage = $('#enable-any-westage').is(':checked') ? 'True' : 'False';
        westageDescription = $('#westage-description').val();
        westageWeight = $('#westage-weight').val();
        metalGrossWeight = parseFloat($('#metal-gross-weight').val());
        isDiamondDeductable = $('#enable-diamond-deductable').is(':checked') ? 'True' : 'False';
        hasDiamond = $('#enable-diamond').is(':checked') ? 'True' : 'False';
        numberOfDiamond = parseInt($('#number-of-diamond').val());
        diamondCarat = parseFloat($('#diamond-carat').val());
        clarityColour = $('#clarity-colour').val();
        diamondWeight = parseFloat($('#diamond-weight').val());
        diamondPrice = parseFloat($('#diamond-price').val());
        diamondValuation = parseFloat($('#diamond-valuation').val());
        metalNetWeight = parseFloat($('#metal-net-weight').val());
        custodyStatus = $('.custody-status:checked').val();
        custodyStatusText = $('.custody-status:checked').next('label').text();
        jewelAssayerRemark = $('#jewel-assayer-remark').val();
        note = $('#note-gold-loan-collateral-detail').val();
        reasonForModification = $('#reason-for-modification-gold-loan').val();

        valuationAmount = $('#gold-valuation-amount').val();;
        // marketValue;

        result = true;

        // Set Default Value To Note, If Empty
        if (note === '')
            note = 'None';

        if (reasonForModification === '')
            reasonForModification = 'None';

        if (jewelAssayerRemark === '')
            jewelAssayerRemark = 'None';

        //jewel assayer Id
        if ($('#jewel-assayer-id').prop('selectedIndex') < 1) {
            result = false;
            $('#jewel-assayer-id-error').removeClass('d-none');
        }

        //gold ornament Id
        if ($('#gold-ornament-id').prop('selectedIndex') < 1) {
            result = false;
            $('#gold-ornament-id-error').removeClass('d-none');
        }

        //Validation Sequence Number
        if (isNaN(sequenceNumber) === false) {
            minimum = $('#sequence-number-gold-collateral').attr('min');
            maximum = $('#sequence-number-gold-collateral').attr('max');
            if (parseInt(sequenceNumber) < parseInt(minimum) || parseInt(sequenceNumber) > parseInt(maximum)) {
                result = false;
                $('#sequence-number-gold-collateral-error').removeClass('d-none');
            }
            else {
                let filteredData = goldCollateralDetailDataTable.rows().indexes().filter(function (value) {
                    return goldCollateralDetailDataTable.row(value).data()[5] == sequenceNumber;
                });

                // Check if a matching row exists and if the sequence number is different from the edited one
                if (filteredData.length > 0 && parseInt(editedSequenceNumber) !== sequenceNumber) {
                    isDuplicateSequenceNumber = true;
                    result = false;
                    $('#sequence-number-gold-collateral-error').text('Sequence Number Already Exists.').removeClass('d-none');
                } else {
                    isDuplicateSequenceNumber = false;
                    $('#sequence-number-gold-collateral-error').addClass('d-none');
                }
            }
        }
        else {
            result = false;
            $('#sequence-number-gold-collateral-error').removeClass('d-none');
        }

        //metal purity
        if ($('#metal-purity-id').prop('selectedIndex') < 1) {
            result = false;
            $('#metal-purity-id-error').removeClass('d-none');
        }

        //HUID
        minimumLength = $('#huid').attr('minlength');
        maximumLength = $('#huid').attr('maxlength');

        if (parseInt(huid.length) < parseInt(minimumLength) || parseInt(huid.length) > parseInt(maximumLength)) {
            result = false;
            $('#huid-error').removeClass('d-none');
        }

        //qty
        if (isNaN(qty) === false) {
            minimum = $('#qty').attr('min');
            maximum = $('#qty').attr('max');
            if (parseInt(qty) < parseInt(minimum) || parseInt(qty) > parseInt(maximum)) {
                result = false;
                $('#qty-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#qty-error').removeClass('d-none');
        }

        //metal gross weight
        if (isNaN(metalGrossWeight) === false) {
            minimum = $('#metal-gross-weight').attr('min');
            maximum = $('#metal-gross-weight').attr('max');
            if (parseFloat(metalGrossWeight) < parseFloat(minimum) || parseFloat(metalGrossWeight) > parseFloat(maximum)) {
                result = false;
                $('#metal-gross-weight-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#metal-gross-weight-error').removeClass('d-none');
        }

        //hasAnyDamage
        if (hasAnyDamage === "True") {

            if (isNaN(damageWeight) === false) {
                minimum = parseFloat($('#damage-weight').attr('min'));
                maximum = parseFloat($('#damage-weight').attr('max'));

                if (parseFloat(damageWeight) < parseFloat(minimum) || parseFloat(damageWeight) > parseFloat(maximum)) {
                    result = false;
                    $('#damage-weight-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#damage-weight-error').removeClass('d-none');
            }

            // Damage Description
            minimumLength = $('#damage-description').attr('minlength');
            maximumLength = $('#damage-description').attr('maxlength');

            if (parseInt(damageDescription.length) < parseInt(minimumLength) || parseInt(damageDescription.length) > parseInt(maximumLength)) {
                result = false;
                $('#damage-description-error').removeClass('d-none');
            }

        }
        else {
            damageWeight = 0;
            damageDescription = 'None';
        }

        //hasAnyWestage
        if (hasAnyWestage === "True") {
            if (isNaN(westageWeight) === false) {
                minimum = parseFloat($('#westage-weight').attr('min'));
                maximum = parseFloat($('#westage-weight').attr('max'));

                if (parseFloat(westageWeight) < parseFloat(minimum) || parseFloat(westageWeight) > parseFloat(maximum)) {
                    result = false;
                    $('#westage-weight-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#westage-weight-error').removeClass('d-none');
            }

            // Westage Description
            minimumLength = $('#westage-description').attr('minlength');
            maximumLength = $('#westage-description').attr('maxlength');

            if (parseInt(westageDescription.length) < parseInt(minimumLength) || parseInt(westageDescription.length) > parseInt(maximumLength)) {
                result = false;
                $('#westage-description-error').removeClass('d-none');
            }

        }
        else {
            westageWeight = 0;
            westageDescription = 'None';
        }

        if (hasDiamond === "True") {
            //number of diamond
            if (isNaN(numberOfDiamond) === false) {
                minimum = parseInt($('#number-of-diamond').attr('min'));
                maximum = parseInt($('#number-of-diamond').attr('max'));

                if (parseInt(numberOfDiamond) < parseInt(minimum) || parseInt(numberOfDiamond) > parseInt(maximum)) {
                    result = false;
                    $('#number-of-diamond-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#number-of-diamond-error').removeClass('d-none');
            }
            //diamond carat
            if (isNaN(diamondCarat) === false) {
                minimum = parseFloat($('#diamond-carat').attr('min'));
                maximum = parseFloat($('#diamond-carat').attr('max'));

                if (parseFloat(diamondCarat) < parseFloat(minimum) || parseFloat(diamondCarat) > parseFloat(maximum)) {
                    result = false;
                    $('#diamond-carat-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#diamond-carat-error').removeClass('d-none');
            }

            //clarity colour
            maximumLength = $('#clarity-colour').attr('maxlength');
            if (parseInt(clarityColour.length) > (maximumLength)) {
                result = false;
                $('#clarity-colour-error').removeClass('d-none');
            }

            //diamondWeight
            if (isNaN(diamondWeight) === false) {
                minimum = parseFloat($('#diamond-weight').attr('min'));
                maximum = parseFloat($('#diamond-weight').attr('max'));

                if (parseFloat(diamondWeight) < parseFloat(minimum) || parseFloat(diamondWeight) > parseFloat(maximum)) {
                    result = false;
                    $('#diamond-weight-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#diamond-weight-error').removeClass('d-none');
            }

            //DiamondPrice
            if (isNaN(diamondPrice) === false) {
                minimum = parseFloat($('#diamond-price').attr('min'));
                maximum = parseFloat($('#diamond-price').attr('max'));

                if (parseFloat(diamondPrice) < parseFloat(minimum) || parseFloat(diamondPrice) > parseFloat(maximum)) {
                    result = false;
                    $('#diamond-price-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#diamond-price-error').removeClass('d-none');
            }
            //diamondValuation
            if (isNaN(diamondValuation) === false) {
                minimum = parseFloat($('#diamond-valuation').attr('min'));
                maximum = parseFloat($('#diamond-valuation').attr('max'));

                if (parseFloat(diamondValuation) < parseFloat(minimum) || parseFloat(diamondValuation) > parseFloat(maximum)) {
                    result = false;
                    $('#diamond-valuation-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#diamond-valuation-error').removeClass('d-none');
            }

        }
        else {
            numberOfDiamond = 0;
            diamondCarat = 0;
            clarityColour = 'None';
            diamondWeight = 0;
            diamondPrice = 0;
            diamondValuation = 0;
        }

        //metalNetWeight
        if (isNaN(metalNetWeight) === false) {
            minimum = parseFloat($('#metal-net-weight').attr('min'));
            maximum = parseFloat($('#metal-net-weight').attr('max'));

            if (parseFloat(metalNetWeight) < parseFloat(minimum) || parseFloat(metalNetWeight) > parseFloat(maximum)) {
                result = false;
                $('#metal-net-weight-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#metal-net-weight-error').removeClass('d-none');
        }

        if (parseFloat(metalGrossWeight) < parseFloat(metalNetWeight)) {
            result = false;
            $('#metal-net-weight-error').text('Please Enter Value Less Than Or Equal To Total Gross Weight').removeClass('d-none');
        }

        //custody Status
        if ($('.custody-status:checked').length === 0) {
            result = false;
            $('#custody-status-error').removeClass('d-none');
        }

        if (result) {
            $('#gold-collateral-detail-accordion-error').addClass('d-none');
        }
        else {
            $('#gold-collateral-detail-accordion-error').removeClass('d-none');
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HidegoldLoanCollateralDetail() {
        goldCollateralDetailDataTable.column(1).visible(false);
        goldCollateralDetailDataTable.column(3).visible(false);
        goldCollateralDetailDataTable.column(6).visible(false);
        goldCollateralDetailDataTable.column(27).visible(false);
        // goldCollateralDetailDataTable.column(30).visible(false);
        goldCollateralDetailDataTable.column(31).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Gold Loan Collateral Photo  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-gold-collateral-photo-dt').click(function (event) {
        debugger;
        event.preventDefault();
        isDbRecord = false;
        isChangedPhoto = false;
        customerGoldLoanCollateralPhotoPrmKey = 0;
        localStoragePath = 'None';
        fileNameDocument = 'None';
        isChangedPhoto = false;

        GoldOrnamentDropdown();
        // Get Phot Upload Range i.e. Minimum Or Maximum Normal Photo
        debugger;
        let a = goldLoanPhotoDataTable.rows().count();

        if (a > 0 && a >= minimumGoldPhoto && a == maximumGoldPhoto) {
            $('#gold-collateral-photo-modal').modal('hide');

            alert('Upload Gold Collateral Photo Between' + minimumGoldPhoto + ' And ' + maximumGoldPhoto);
        }
        else {
            if (maximumGoldPhoto == 0) {
                $('#gold-collateral-photo-modal').modal('hide');

            }
            else {
                $('#gold-collateral-photo-modal').modal('show');
                SetModalTitle('gold-collateral-photo', 'Add');
            }
        }
    });

    // DataTable Edit Button 
    $('#btn-edit-gold-collateral-photo-dt').click(function () {
        SetModalTitle('gold-collateral-photo', 'Edit');
        isChecked = $('.checks').is(':checked');

        isDbRecord = false;
        isChangedPhoto = false;

        if (isChecked) {
            debugger;
            isDbRecord = false;
            isChangedPhoto = false;

            columnValues = $('#btn-edit-gold-collateral-photo-dt').data('rowindex');
            GoldOrnamentDropdown();
            $('#gold-ornament-id-photo', myModal).val(columnValues[1]);
            $('#photo-type-id', myModal).val(columnValues[3]);
            $('#photo-caption-gold', myModal).val(columnValues[7]);
            $('#note-gold-collateral-photo', myModal).val(columnValues[8]);


            fileUploader = $('#' + $(columnValues[5]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'gold-file-uploader';

            // columnValues[4] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[5]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[5]).attr('class') === 'db-record' ? true : false;

            // columnValues[5] - Image Tag Html
            filePath = $('#' + $(columnValues[6]).attr('id')).attr('src');

            fileNameDocument = columnValues[9];
            customerGoldLoanCollateralPhotoPrmKey = columnValues[10];
            localStoragePath = columnValues[11];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                localStoragePath = 'None';
                fileNameDocument = 'None';
                AttachFileUploader();
            }

            $('#gold-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#gold-collateral-photo-modal').modal('show');
        }
        else {
            $('#btn-edit-gold-collateral-photo-dt').addClass('read-only');
            $('#gold-collateral-photo-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-gold-collateral-photo-modal').click(function () {
        if (IsValidGoldLoanPhotoDataTableModal()) {
            row = goldLoanPhotoDataTable.row.add([
                tag,
                sequenceNumber,
                goldOrnamentText,
                photoTypeId,
                photoTypeText,
                fileUploaderInputHtml,
                imageTagHtml,
                photoCaption,
                note,
                fileNameDocument,
                customerGoldLoanCollateralPhotoPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideGoldLoanPhotoDataTableColumns();

            goldLoanPhotoDataTable.columns.adjust().draw();

            // Remove Required Error Message
            $('#gold-collateral-photo-accordion-error').addClass('d-none');

            ClearModal('gold-collateral-photo');

            $('#gold-collateral-photo-modal').modal('hide');

            EnableNewOperation('gold-collateral-photo');
        }
    });

    // Modal update Button Event
    $('#btn-update-gold-collateral-photo-modal').click(function () {
        $('#select-all-gold-collateral-photo').prop('checked', false);

        if (IsValidGoldLoanPhotoDataTableModal()) {
            goldLoanPhotoDataTable.row(selectedRowIndex).data([
                tag,
                sequenceNumber,
                goldOrnamentText,
                photoTypeId,
                photoTypeText,
                fileUploaderInputHtml,
                imageTagHtml,
                photoCaption,
                note,
                fileNameDocument,
                customerGoldLoanCollateralPhotoPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideGoldLoanPhotoDataTableColumns();

            goldLoanPhotoDataTable.columns.adjust().draw();

            $('#gold-collateral-photo-modal').modal('hide');

            EnableNewOperation('gold-collateral-photo');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-gold-collateral-photo-dt').click(function () {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-gold-collateral-photo tbody input[type="checkbox"]:checked').each(function () {
                    goldLoanPhotoDataTable.row($('#tbl-fund tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    // ******* Check Usage Otherwise Remove It
                    // rowData = $('#btn-delete-fund-dt').data('rowindex');

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!goldLoanPhotoDataTable.data().any())
                        $('#gold-collateral-photo-accordion-error').removeClass('d-none');

                    EnableNewOperation('gold-collateral-photo');

                    $('#select-all-gold-collateral-photo').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-gold-collateral-photo').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-gold-collateral-photo tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                // ******* Check Usage Otherwise Remove It
                row = $(this).closest('tr');

                selectedRowIndex = goldLoanPhotoDataTable.row(row).index();

                rowData = (goldLoanPhotoDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[5] });

                $('#btn-delete-gold-collateral-photo-dt').data('rowindex', arr);

                // ***********************

                EnableDeleteOperation('gold-collateral-photo')
            });
        }
        else {
            EnableNewOperation('gold-collateral-photo')

            $('#tbl-gold-collateral-photo tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-gold-collateral-photo tbody').click('input[type=checkbox]', function () {
        $('#tbl-gold-collateral-photo input[type="checkbox"]:checked').each(function () {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');

                selectedRowIndex = goldLoanPhotoDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (goldLoanPhotoDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[5] });

                    EnableEditDeleteOperation('gold-collateral-photo');

                    $('#btn-update-gold-collateral-photo-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-gold-collateral-photo-dt').data('rowindex', rowData);
                    $('#btn-delete-gold-collateral-photo-dt').data('rowindex', arr);
                    $('#select-all-gold-collateral-photo').data('rowindex', arr);
                }
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-gold-collateral-photo tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('gold-collateral-photo');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('gold-collateral-photo');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('gold-collateral-photo');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-gold-collateral-photo').prop('checked', true);
        else
            $('#select-all-gold-collateral-photo').prop('checked', false);
    });

    // Validate  Fund Module
    function IsValidGoldLoanPhotoDataTableModal() {
        debugger;
        counter++;
        fileUploaderId = "data-table-gold-file-uploader" + counter;
        fileId = "photo-id" + counter;
        result = true;

        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        sequenceNumber = $('#gold-ornament-id-photo').val();
        goldOrnamentText = $('#gold-ornament-id-photo option:selected').text();
        photoTypeId = $('#photo-type-id option:selected').val();
        photoTypeText = $('#photo-type-id option:selected').text();
        photoCaption = $('#photo-caption-gold').val();
        note = $('#note-gold-collateral-photo').val();
        filePath = $('#gold-file-uploader-image-preview').prop('src');
        fileUploader = $('#gold-file-uploader').get(0);

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#gold-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        //File Caption
        if (photoCaption === '')
            photoCaption = 'None';

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            // Validate Only New Record ** (i.e. Skip Uploaded From Database)
            if (isDbRecord === false || isChangedPhoto === true) {
                result = false;
                $('#gold-file-uploader-error').removeClass('d-none');
            }
        }

        //Sequence Number
        if (isNaN(sequenceNumber) === false) {
            minimum = parseInt($('#gold-ornament-id-photo').attr('min'));
            maximum = parseInt($('#gold-ornament-id-photo').attr('max'));

            if (parseInt(sequenceNumber) < parseInt(minimum) || parseInt(sequenceNumber) > parseInt(maximum)) {
                result = false;
                $('#gold-ornament-id-photo-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#gold-ornament-id-photo-error').removeClass('d-none');
        }

        //photoTypeId
        if ($('#photo-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#photo-type-id-error').removeClass('d-none');
        }

        //File Caption
        maximumLength = parseInt($('#photo-caption-gold').attr('maxlength'));
        if (parseInt(photoCaption.length) === 0 || parseInt(photoCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#photo-caption-gold-error').removeClass('d-none');
        }

        //note
        maximumLength = parseInt($('#note-gold-collateral-photo').attr('maxlength'));
        if (parseInt(note.length) === 0 || parseInt(note.length) > parseInt(maximumLength)) {
            result = false;
            $('#note-gold-collateral-photo-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideGoldLoanPhotoDataTableColumns() {
        goldLoanPhotoDataTable.column(1).visible(false);
        goldLoanPhotoDataTable.column(3).visible(false);
        goldLoanPhotoDataTable.column(9).visible(false);
        goldLoanPhotoDataTable.column(10).visible(false);
        goldLoanPhotoDataTable.column(11).visible(false);
    }

    function GoldOrnamentDropdown() {
        let loanSanctionAmount = $('#sanction-amount').val();
        let goldcollateralValuationAmount = 0;
        let isValidGoldLoanValuation = true;
        let goldOrnamentId = new Array();

        // Validate Gold Loan Valuation Amount 
        $('#tbl-gold-collateral-detail > TBODY > TR').each(function () {
            debugger;
            let currentRow = $(this).closest('tr');
            let rowIndex = goldCollateralDetailDataTable.row(currentRow).index();
            columnValues1 = (goldCollateralDetailDataTable.row(rowIndex).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnValues1 == 'undefined' && columnValues1 == null) {
                return false;
            }
            else {
                goldcollateralValuationAmount = +columnValues1[26];

                let td0 = columnValues1[5];
                let td1 = columnValues1[4];

                goldOrnamentId.push({ td0, td1 });

                // Hide Photo Type Dropdown According Inputs
                if (columnValues1[11] == 'True')
                    $('#photo-type-id').find('option[value="DMG"]').removeClass('d-none');
                else
                    $('#photo-type-id').find('option[value="DMG"]').addClass('d-none');

                // Westage
                if (columnValues1[14] == 'True')
                    $('#photo-type-id').find('option[value="WST"]').removeClass('d-none');
                else
                    $('#photo-type-id').find('option[value="WST"]').addClass('d-none');

                // Diamond
                if (columnValues1[18] == 'True')
                    $('#photo-type-id').find('option[value="DMN"]').removeClass('d-none');
                else
                    $('#photo-type-id').find('option[value="DMN"]').addClass('d-none');
            }
        });

        // Validate Valuation Amount Whether Sufficient Or Not?
        if (parseFloat(loanSanctionAmount) > parseFloat(goldcollateralValuationAmount)) {
            isValidGoldLoanValuation = false;
            alert(' InSufficient Pledge Amount : ' + goldcollateralValuationAmount + ' For Sanction Loan Amount : ' + loanSanctionAmount);
        }
        else {
            isValidGoldLoanValuation = true;
        }

        // Get Dropdown Of Gold Ornaments Uploaded In Collateral
        let goldOrnamentList = $('#gold-ornament-id-photo');
        goldOrnamentList.html('');

        let options = '<option value="0"> Select Gold Ornament </option>';
        //goldOrnamentId = $('#gold-ornament-id');
        $.each(goldOrnamentId, function (key, value) {
            debugger;
            options += '<option value="' + value.td0 + '">' + value.td1 + '</option>';
        });

        goldOrnamentList.append(options);
        goldOrnamentList.prop('selectedIndex', 0);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Document - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // DataTable Add Button 
    $('#btn-add-document-dt').click(function (event) {
        event.preventDefault();

        isDbRecord = false;

        editedDocumentId = '';
        editedDocumentSequenceNumber = 0;

        customerAccountDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

        SetDocumentUniqueDropdownList();

        SetModalTitle('document', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-document-dt').click(function () {
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('document', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-document-dt').data('rowindex');

            editedDocumentId = columnValues[1];
            editedDocumentSequenceNumber = columnValues[3];

            debugger;
            SetDocumentUniqueDropdownList();

            $('#document-sequence-number', myModal).val(columnValues[3]);
            $('#document-id', myModal).val(columnValues[1]);

            fileUploader = $('#' + $(columnValues[4]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'document-file-uploader';

            // columnValues[4] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[4]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[4]).attr('class') === 'db-record' ? true : false;

            // columnValues[5] - Image Tag Html
            filePath = $('#' + $(columnValues[5]).attr('id')).attr('src');

            fileNameDocument = columnValues[9];
            customerAccountDocumentPrmKey = columnValues[10];
            localStoragePath = columnValues[11];

            // Set Valid File Format And Size Of Selected Document
            SetValidDocumentFileFormat();

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            $('#document-file-uploader-image-preview').attr('src', filePath);

            $('#file-caption-document', myModal).val(columnValues[6]);
            $('#note-document', myModal).val(columnValues[7]);

            // Show Modals
            $('#document-modal').modal('show');
        }
    });

    // Modal Add Button Event
    $('#btn-add-document-modal').click(function () {
        debugger;
        if (IsValidDocumentDataTableModal()) {
            row = documentDataTable.row.add([
                tag,
                documentId,
                documentText,
                sequenceNumber,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                customerAccountDocumentPrmKey,
                localStoragePath

            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideDocumentDataTableColumns();

            documentDataTable.columns.adjust().draw();

            // Check Whether All Required Documents Added Or Not
            IsAddedAllRequiredDocument();

            $('#document-modal').modal('hide');

            EnableNewOperation('document');
        }
    });

    // Modal update Button Event
    $('#btn-update-document-modal').click(function () {
        $('#select-all-document').prop('checked', false);

        if (IsValidDocumentDataTableModal()) {
            documentDataTable.row(selectedRowIndex).data([
                tag,
                documentId,
                documentText,
                sequenceNumber,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                customerAccountDocumentPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideDocumentDataTableColumns();

            documentDataTable.columns.adjust().draw();

            columnValues = $('#btn-update-document').data('rowindex');

            // Check Whether All Required Documents Added Or Not
            IsAddedAllRequiredDocument();

            $('#document-modal').modal('hide');

            EnableNewOperation('document');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-document-dt').click(function () {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-document tbody input[type="checkbox"]:checked').each(function () {
                    documentDataTable.row($('#tbl-document tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-document-dt').data('rowindex');

                    // Check Whether All Required Documents Added Or Not
                    IsAddedAllRequiredDocument();

                    EnableNewOperation('document');

                    SetDocumentUniqueDropdownList();

                    $('#select-all-document').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!documentDataTable.data().any()) {
                        $('#required-document-error').addClass('d-none');
                        $('#document-error').removeClass('d-none');
                    }
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-document').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-document tbody input[type="checkbox"]').each(function () {
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
    $('#tbl-document tbody').click("input[type=checkbox]", function () {
        debugger;
        $('#tbl-document input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                debugger;
                let row = $(this).closest('tr');
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
        if (checked.length == 0)
            EnableNewOperation('document');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
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

    // Validate Document Data Table
    function IsValidDocumentDataTableModal() {
        debugger;
        result = true;

        counter++;
        fileUploaderId = "data-table-document-file-uploader" + counter;
        fileId = "photo-id" + counter;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        // Capture the currently selected value and text
        documentId = $('#document-id option:selected').val();
        documentText = $('#document-id option:selected').text();//NameOfDocument; // Get the text of the selected option

        sequenceNumber = parseInt($('#document-sequence-number').val());
        filePath = $('#document-file-uploader-image-preview').prop('src');
        fileUploader = $('#document-file-uploader').get(0);
        fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
        imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        fileCaption = $('#file-caption-document').val();
        note = $('#note-document').val();
        reasonForModification = $('#reason-for-modification-document').val();

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#document-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#document-file-uploader').get(0);

        // Validate Document
        if ($('#document-id').prop('selectedIndex') < 1) {
            result = false;
            $('#document-id-error').removeClass('d-none');
        }

        // Validate Sequence Number
        if (isNaN(sequenceNumber) === false) {
            minimum = parseInt($('#document-sequence-number').attr('min'));
            maximum = parseInt($('#document-sequence-number').attr('max'));

            if (parseInt(sequenceNumber) < parseInt(minimum) || parseInt(sequenceNumber) > parseInt(maximum)) {
                result = false;
                $('#document-sequence-number-error').removeClass('d-none');
            }
            else {
                if (IsDuplicateDocumentSequenceNumber()) {
                    result = false;
                    $('#document-sequence-number-error').removeClass('d-none');
                }
            }
        }
        else {
            result = false;
            $('#document-sequence-number-error').removeClass('d-none');
        }

        // file Caption
        if (isNaN(fileCaption.length) === false) {

            maximumLength = parseInt($('#file-caption-document').attr('maxlength'));

            if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
                result = false;
                $('#file-caption-document-error').removeClass('d-none');
            }
        } else {
            result = false;
            $('#file-caption-document-error').removeClass('d-none');
        }

        //Set Default Value if Empty
        if (fileCaption === '') {
            fileCaption = 'None';
        }

        if (note === '') {
            note = 'None';
        }

        if (reasonForModification === '') {
            reasonForModification = 'None';
        }


        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (isDbRecord === false || isChangedPhoto === true) {
                result = false;
                $('#document-file-uploader-error').removeClass('d-none');
            }
        }

        if (result) {
            $('#document-accordion-error').addClass('d-none');
        }
        else {
            $('#document-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideDocumentDataTableColumns() {
        documentDataTable.column(1).visible(false);
        documentDataTable.column(8).visible(false);
        documentDataTable.column(9).visible(false);
        documentDataTable.column(10).visible(false);
        documentDataTable.column(11).visible(false);
    }

    function IsDuplicateDocumentSequenceNumber() {
        debugger;
        // Validate Sequence Number
        let sequenceNumber = $('#document-sequence-number').val(); // Get the value of the document sequence number input

        // Filter rows in the DataTable that match the sequence number
        let filteredDataForDocumentSequenceNumber = documentDataTable
            .rows()
            .indexes()
            .filter(function (value) {
                return documentDataTable.row(value).data()[3] == sequenceNumber;
            });

        // Check if there are any matching rows and if the nominee number is different from the sequence number
        let isDuplicate = documentDataTable.rows(filteredDataForDocumentSequenceNumber).count() > 0 && editedDocumentSequenceNumber != sequenceNumber;

        // Show or hide the error message based on the result
        if (isDuplicate) {
            $('#document-sequence-number-error').removeClass('d-none');
        }
        else {
            $('#document-sequence-number-error').addClass('d-none');
        }

        return isDuplicate;
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Borrowing Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-borrowing-detail-dt').click(function (event) {
        debugger;
        event.preventDefault();

        $('#taking-any-court-action-block').addClass('d-none');

        // For Court Case
        EnableTakingAnyCourtActionClickEventFunction();
        SetModalTitle('borrowing-detail', 'Add');

    });

    // DataTable Edit Button 
    $('#btn-edit-borrowing-detail-dt').click(function () {
        debugger;
        SetModalTitle('borrowing-detail', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
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

            if (columnValues[19] === true) {
                $('#taking-any-court-action-block').removeClass('d-none');
            }
            else {
                $('#taking-any-court-action-block').addClass('d-none');
            }

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

            personBorrowingDetailPrmKey = columnValues[32]
            customerAccountPrmKey = columnValues[33]

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
    $('#btn-add-borrowing-detail-modal').click(function () {
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
                reasonForModification,
                personBorrowingDetailPrmKey,
                customerAccountPrmKey
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
    $('#btn-update-borrowing-detail-modal').click(function () {
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
                reasonForModification,
                personBorrowingDetailPrmKey,
                customerAccountPrmKey
            ]).draw();

            HideColumnsborrowingDetailDataTable();

            borrowingDetailDataTable.columns.adjust().draw();

            $('#borrowing-detail-modal').modal('hide');

            EnableNewOperation('borrowing-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-borrowing-detail-dt').click(function () {
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
    $('#select-all-borrowing-detail').click(function () {
        if ($(this).prop('checked')) {
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
        else {
            EnableNewOperation('borrowing-detail');

            $('#tbl-borrowing-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-borrowing-detail tbody').click('input[type=checkbox]', function () {
        $('#tbl-borrowing-detail input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = borrowingDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (borrowingDetailDataTable.row(selectedRowIndex).data());

                    personBorrowingDetailPrmKey = rowData[32];

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
        if (checked.length == 1) {
            if (personBorrowingDetailPrmKey > 0)
                EnableDeleteOperation('borrowing-detail');
            else
                EnableEditDeleteOperation('borrowing-detail');
        }

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

        personBorrowingDetailPrmKey = 0;
        customerAccountPrmKey = 0;

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
        borrowingDetailDataTable.column(31).visible(false);
        borrowingDetailDataTable.column(32).visible(false);
        borrowingDetailDataTable.column(33).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Additional Income Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    //Modify By Suraj
    // DataTable Add Button 
    $('#btn-add-income-detail-dt').click(function (event) {
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

            personAdditionalIncomeDetailPrmKey = columnValues[7];
            customerAccountPrmKey = columnValues[8];

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
    $('#btn-add-income-detail-modal').click(function () {

        if (IsValidIncomeDetailModal()) {
            row = incomeDatatable.row.add([
                tag,
                incomeSource,
                incomeSourceText,
                annualIncome,
                otherDetails,
                note,
                reasonForModification,
                personAdditionalIncomeDetailPrmKey,
                customerAccountPrmKey
            ]).draw();

            // Error Message In Span
            $('#income-details-accordion-error').addClass('d-none');

            HideColumnsIncomeDetailDatatable();

            incomeDatatable.columns.adjust().draw();

            $('#income-detail-modal').modal('hide');

            EnableNewOperation('income-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-income-detail-modal').click(function () {
        $('#select-all-income-detail').prop('checked', false);
        if (IsValidIncomeDetailModal()) {
            incomeDatatable.row(selectedRowIndex).data([
                tag,
                incomeSource,
                incomeSourceText,
                annualIncome,
                otherDetails,
                note,
                reasonForModification,
                personAdditionalIncomeDetailPrmKey,
                customerAccountPrmKey
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
    $('#btn-delete-income-detail-dt').click(function () {

        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-income-detail tbody input[type="checkbox"]:checked').each(function () {
                    incomeDatatable.row($('#tbl-income-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-income-detail-dt').data('rowindex');
                    EnableNewOperation('income-detail');

                    $('#select-all-income-detail').prop('checked', false);
                    //if (!incomeDatatable.data().any())
                    //$('#income-details-accordion-error').removeClass('d-none');

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

        $('#tbl-income-detail input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = incomeDatatable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (incomeDatatable.row(selectedRowIndex).data());

                    personAdditionalIncomeDetailPrmKey = rowData[7];

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
        if (checked.length == 1) {
            if (personAdditionalIncomeDetailPrmKey > 0)
                EnableDeleteOperation('income-detail');
            else
                EnableEditDeleteOperation('income-detail');
        }

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

        personAdditionalIncomeDetailPrmKey = 0;
        customerAccountPrmKey = 0;

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
            if (!otherDetails) { // This also checks for empty strings
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
        incomeDatatable.column(6).visible(false);
        incomeDatatable.column(7).visible(false);
        incomeDatatable.column(8).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Court Case - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-court-case-dt').click(function (event) {
        event.preventDefault();

        SetModalTitle('court-case', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-court-case-dt').click(function () {
        debugger;

        SetModalTitle('court-case', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
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

            personCourtCasePrmKey = columnValues[14];
            customerAccountPrmKey = columnValues[15];

            // Show Modals
            $('#court-case-modal').modal('show');
        }
        else {
            $('#btn-edit-court-case-edit-dt').addClass('read-only');
            $('#court-case-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-court-case-modal').click(function () {
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
                personCourtCasePrmKey,
                customerAccountPrmKey
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
    $('#btn-update-court-case-modal').click(function () {
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
                reasonForModification,
                personCourtCasePrmKey,
                customerAccountPrmKey
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
    $('#btn-delete-court-case-dt').click(function () {
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
        $('#tbl-court-case input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = courtCaseDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (courtCaseDataTable.row(selectedRowIndex).data());

                    personCourtCasePrmKey = rowData[14];

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
        if (checked.length == 1) {
            if (personCourtCasePrmKey > 0)
                EnableDeleteOperation('court-case');
            else
                EnableEditDeleteOperation('court-case');
        }

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

        personCourtCasePrmKey = 0;
        customerAccountPrmKey = 0;

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
        if (isNaN(filingNumber.length) === false) {
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
        if (isNaN(registrationNumber.length) === false) {
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
        if (isNaN(cnrNumber.length) === false) {
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
        if (isNaN(amountOfDecree) === false) {
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
        if (isNaN(collateralAmount) === false) {
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
        courtCaseDataTable.column(13).visible(false);
        courtCaseDataTable.column(14).visible(false);
        courtCaseDataTable.column(15).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   Person Income Tax Detail  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-income-tax-dt').click(function (event) {
        debugger;
        event.preventDefault();

        isDbRecord = false;

        personIncomeTaxDetailDocumentPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

        SetModalTitle('income-tax', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-income-tax-dt').click(function () {

        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('income-tax', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#income-tax-modal').modal();

            columnValues = $('#btn-edit-income-tax-dt').data('rowindex');
            // Display Value In Modal Inputs
            $('#assessments-year-income-tax', myModal).val(columnValues[1]);
            $('#tax-amounts', myModal).val(columnValues[2]);
            $('#file-caption-tax', myModal).val(columnValues[5]);
            $('#note-income-tax-detail', myModal).val(columnValues[6]);
            $('#reason-for-modification-tax-detail', myModal).val(columnValues[7]);

            fileUploader = $('#' + $(columnValues[3]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'income-tax-file-uploader';

            // columnValues[3] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[3]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[3]).attr('class') === 'db-record' ? true : false;

            // columnValues[4] - Image Tag Html
            filePath = $('#' + $(columnValues[4]).attr('id')).attr('src');

            fileNameDocument = columnValues[8];
            personIncomeTaxDetailDocumentPrmKey = columnValues[9];
            localStoragePath = columnValues[10];

            personIncomeTaxDetailPrmKey = columnValues[11];
            customerAccountPrmKey = columnValues[12];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            $('#income-tax-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#income-tax-modal').modal('show');
        }
        else {
            $('#btn-edit-income-tax-edit-dt').addClass('read-only');
            $('#income-tax-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-income-tax-modal').click(function () {

        if (IsValidIncomeTaxModal()) {
            row = incomeTaxDataTable.row.add([
                tag,
                assessmentYear,
                taxAmount,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personIncomeTaxDetailDocumentPrmKey,
                localStoragePath,
                personIncomeTaxDetailPrmKey,
                customerAccountPrmKey
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#income-tax-accordion-error').addClass('d-none');

            HideColumnsIncomeTaxDataTable();

            incomeTaxDataTable.columns.adjust().draw();

            $('#income-tax-modal').modal('hide');

            EnableNewOperation('income-tax');
        }
    });

    // Modal update Button Event
    $('#btn-update-income-tax-modal').click(function () {
        debugger
        $('#select-all-income-tax').prop('checked', false);
        debugger
        if (IsValidIncomeTaxModal()) {
            debugger
            incomeTaxDataTable.row(selectedRowIndex).data([
                tag,
                assessmentYear,
                taxAmount,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                reasonForModification,
                fileNameDocument,
                personIncomeTaxDetailDocumentPrmKey,
                localStoragePath,
                personIncomeTaxDetailPrmKey,
                customerAccountPrmKey
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsIncomeTaxDataTable();

            incomeTaxDataTable.columns.adjust().draw();
            columnValues = $('#btn-update-income-tax').data('rowindex');

            $('#income-tax-modal').modal('hide');

            EnableNewOperation('income-tax');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-income-tax-dt').click(function () {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-income-tax tbody input[type="checkbox"]:checked').each(function () {
                    incomeTaxDataTable.row($('#tbl-income-tax tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-income-tax-dt').data('rowindex');
                    EnableNewOperation('income-tax');

                    $('#select-all-income-tax').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-income-tax').click(function () {

        if ($(this).prop('checked')) {
            $('#tbl-income-tax tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = incomeTaxDataTable.row(row).index();

                rowData = (incomeTaxDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-income-tax-dt').data('rowindex', arr);
                EnableDeleteOperation('income-tax')
            });
        }
        else {
            EnableNewOperation('income-tax')

            $('#tbl-income-tax tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-income-tax tbody').click('input[type=checkbox]', function () {
        $('#tbl-income-tax input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = incomeTaxDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (incomeTaxDataTable.row(selectedRowIndex).data());

                    personIncomeTaxDetailPrmKey = rowData[11];

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('income-tax');

                    $('#btn-update-income-tax-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-income-tax-dt').data('rowindex', rowData);
                    $('#btn-delete-income-tax-dt').data('rowindex', arr);
                    $('#select-all-income-tax').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-income-tax tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('income-tax');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1) {
            if (personIncomeTaxDetailPrmKey > 0)
                EnableDeleteOperation('income-tax');
            else
                EnableEditDeleteOperation('income-tax');
        }

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('income-tax');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-income-tax').prop('checked', true);
        else
            $('#select-all-income-tax').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidIncomeTaxModal() {
        debugger;
        result = true;
        counter++;
        fileUploaderId = 'data-table-income-tax-file-uploader' + counter;
        fileId = "photo-id" + counter;

        // Get Modal Inputs In Local letiable
        $('#select-all-asset-document').prop('checked', false);

        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        assessmentYear = parseInt($('#assessments-year-income-tax').val());
        taxAmount = parseFloat($('#tax-amounts').val());
        fileCaption = $('#file-caption-tax').val();
        note = $('#note-income-tax-detail').val();
        reasonForModification = $('#reason-for-modification-tax-detail').val();
        personIncomeTaxDetailPrmKey = 0;
        customerAccountPrmKey = 0;

        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#income-tax-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';

        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        fileUploader = $('#income-tax-file-uploader').get(0);

        // Set Default Value, If Empty
        if (note === '') {
            $('#note-income-tax-detail').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-tax-detail').val('None');
            reasonForModification = 'None';
        }

        if (fileCaption === '') {
            $('#file-caption-tax').val('None');
            fileCaption = 'None';
        }

        // Get the current year
        let currentYear = new Date().getFullYear();

        // Calculate the minimum allowed year
        let minAllowedYear = currentYear - 20;

        if (isNaN(assessmentYear) === false) {
            if (parseInt(assessmentYear) < parseInt(minAllowedYear) || parseInt(assessmentYear) > parseInt(currentYear)) {
                result = false;
                $('#assessments-year-income-tax-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#assessments-year-income-tax-error').removeClass('d-none');
        }

        //tax Amount
        if (isNaN(taxAmount) === false) {
            minimum = parseFloat($('#tax-amounts').attr('min'));
            maximum = parseFloat($('#tax-amounts').attr('max'));

            if (parseFloat(taxAmount) < parseFloat(minimum) || parseFloat(taxAmount) > parseFloat(maximum)) {
                result = false;
                $('#tax-amounts-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#tax-amounts-error').removeClass('d-none');
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            if (personInformationParameterViewModel.IncomeTaxDocumentUpload === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isDbRecord === false || isChangedPhoto === true) {
                    result = false;
                    $('#income-tax-file-uploader-error').removeClass('d-none');
                }
            }
            else {
                let photoSrc = $('#income-tax-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    localStoragePath = 'None';
                }
            }
        }

        //filecaption
        maximumLength = parseInt($('#file-caption-tax').attr('maxlength'));

        if (parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#file-caption-tax-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsIncomeTaxDataTable() {
        incomeTaxDataTable.column(7).visible(false);
        incomeTaxDataTable.column(8).visible(false);
        incomeTaxDataTable.column(9).visible(false);
        incomeTaxDataTable.column(10).visible(false);
        incomeTaxDataTable.column(11).visible(false);
        incomeTaxDataTable.column(12).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Vehicle Loan Photo @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-vehicle-loan-photo-dt').click(function (event) {
        debugger;
        event.preventDefault();

        isDbRecord = false;

        customerVehicleLoanPhotoPrmKey = 0;
        fileNameDocument = 'None';
        localStoragePath = 'None';

        let captionString = 'Enter Clearly Described The Photo Context (For Ex. “Front View, Chassis Number, Close-Up, Interior Dashboard, Odometer, Dent, Rust, Seat, Tyres”).';

        let count = vehicleLoanPhotoDataTable.rows().count();

        if (count > 0) {
            $('#tbl-vehicle-loan-photo > tbody > tr').each(function () {
                debugger;
                currentRow = $(this).closest('tr');

                columnValues = (vehicleLoanPhotoDataTable.row(currentRow).data());
                let value = columnValues[3];
                let enteredCaptionsArray = value.split(',').map(item => item.trim());
                enteredCaptionsArray.forEach(function (value) {
                    if (captionString.includes(value)) {
                        captionString = captionString.replace(new RegExp(value + ',?', 'g'), '').trim();
                        $('#file-caption').attr('placeholder', captionString);
                        updatedPlaceholder = $('#file-caption').attr('placeholder');

                    }
                    captionString = updatedPlaceholder;
                });

            });
        }
        else {
            $('#file-caption').attr('placeholder', captionString);
        }

        //validate the no.of photos limit from database record
        if (parseInt(count) < parseInt(maximumVehiclePhoto)) {
            SetModalTitle('vehicle-loan-photo', 'Add');

            $('#vehicle-loan-photo').modal('show');
        }
        else {
            $('#vehicle-loan-photo').modal('hide');
            alert('Upload Vehicle PhotoVehicle Photo Between ' + minimumVehiclePhoto + ' And ' + maximumVehiclePhoto + ' Photos');
        }
    });

    // DataTable Edit Button 
    $('#btn-edit-vehicle-loan-photo-dt').click(function () {
        debugger;
        isDbRecord = false;
        isChangedPhoto = false;

        SetModalTitle('vehicle-loan-photo', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#vehicle-loan-photo-modal').modal();

            columnValues = $('#btn-edit-vehicle-loan-photo-dt').data('rowindex');

            $('#file-caption', myModal).val(columnValues[3]);
            $('#note-vehicle-loan-photo', myModal).val(columnValues[4]);

            fileUploader = $('#' + $(columnValues[1]).attr('id')).get(0);

            // Destination File Uploader Id (i.e. Modal File Uploader)
            fileUploaderId = 'vehicle-file-uploader';

            // columnValues[4] - File Uploader Html
            fileUploaderInput = $('#' + $(columnValues[1]).attr('id')).get(0);

            // Check Selected Record Is Database Record Or Not
            isDbRecord = $(columnValues[1]).attr('class') === 'db-record' ? true : false;

            // columnValues[5] - Image Tag Html
            filePath = $('#' + $(columnValues[2]).attr('id')).attr('src');

            fileNameDocument = columnValues[5]
            customerVehicleLoanPhotoPrmKey = columnValues[6];
            localStoragePath = columnValues[7];

            // Attach File Uploader if Not Old Photo
            // Perform Operation Only If Photo Has Been Changed
            if (fileUploader.files.length > 0) {
                fileNameDocument = 'None';
                localStoragePath = 'None';

                AttachFileUploader();
            }

            $('#vehicle-file-uploader-image-preview').attr('src', filePath);

            // Show Modals
            $('#vehicle-loan-photo-modal').modal('show');
        }
        else {
            $('#btn-edit-vehicle-loan-photo-edit-dt').addClass('read-only');
            $('#vehicle-loan-photo-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-vehicle-loan-photo-modal').click(function () {
        debugger;
        if (IsValidVehicleLoanPhotoModal()) {
            debugger;
            row = vehicleLoanPhotoDataTable.row.add([
                tag,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                fileNameDocument,
                customerVehicleLoanPhotoPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#vehicle-loan-photo-data-table-error').addClass('d-none');

            HideColumnsVehicleLoanPhotoDataTable();

            vehicleLoanPhotoDataTable.columns.adjust().draw();

            ClearModal('vehicle-loan-photo');

            $('#vehicle-loan-photo-modal').modal('hide');

            EnableNewOperation('vehicle-loan-photo');
        }
    });

    // Modal update Button Event
    $('#btn-update-vehicle-loan-photo-modal').click(function () {
        $('#select-all-vehicle-loan-photo').prop('checked', false);
        if (IsValidVehicleLoanPhotoModal()) {
            vehicleLoanPhotoDataTable.row(selectedRowIndex).data([
                tag,
                fileUploaderInputHtml,
                imageTagHtml,
                fileCaption,
                note,
                fileNameDocument,
                customerVehicleLoanPhotoPrmKey,
                localStoragePath
            ]).draw();

            // Perform Operation Only If Photo Has Been Changed Or New Record With Mandatory Upload
            if ((isDbRecord === false || isChangedPhoto === true) && (fileCaption !== 'NotApplicable')) {
                AttachFileUploader();
            }

            HideColumnsVehicleLoanPhotoDataTable();

            vehicleLoanPhotoDataTable.columns.adjust().draw();

            $('#vehicle-loan-photo-modal').modal('hide');

            EnableNewOperation('vehicle-loan-photo');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-vehicle-loan-photo-dt').click(function () {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-vehicle-loan-photo tbody input[type="checkbox"]:checked').each(function () {
                    vehicleLoanPhotoDataTable.row($('#tbl-vehicle-loan-photo tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-vehicle-loan-photo-dt').data('rowindex');
                    EnableNewOperation('vehicle-loan-photo');

                    $('#select-all-vehicle-loan-photo').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-vehicle-loan-photo').click(function () {
        debugger;
        if ($(this).prop('checked')) {
            $('#tbl-vehicle-loan-photo tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = vehicleLoanPhotoDataTable.row(row).index();

                rowData = (vehicleLoanPhotoDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-vehicle-loan-photo-dt').data('rowindex', arr);
                EnableDeleteOperation('vehicle-loan-photo');
            });
        }
        else {
            EnableNewOperation('vehicle-loan-photo');

            $('#tbl-vehicle-loan-photo tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-vehicle-loan-photo tbody').click('input[type=checkbox]', function () {
        debugger;
        $('#tbl-vehicle-loan-photo input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = vehicleLoanPhotoDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (vehicleLoanPhotoDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('vehicle-loan-photo');

                    $('#btn-update-vehicle-loan-photo-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-vehicle-loan-photo-dt').data('rowindex', rowData);
                    $('#btn-delete-vehicle-loan-photo-dt').data('rowindex', arr);
                    $('#select-all-vehicle-loan-photo').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-vehicle-loan-photo tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('vehicle-loan-photo');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('vehicle-loan-photo');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('vehicle-loan-photo');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-vehicle-loan-photo').prop('checked', true);
        else
            $('#select-all-vehicle-loan-photo').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidVehicleLoanPhotoModal() {
        debugger;
        result = true;

        counter++;
        fileUploaderId = 'data-table-vehicle-file-uploader' + counter;
        fileId = "photo-Id" + counter;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        note = $('#note-vehicle-loan-photo').val();
        fileCaption = $('#file-caption').val();
        filePath = $('#vehicle-file-uploader-image-preview').prop('src');
        fileUploader = $('#vehicle-file-uploader').get(0);


        if (isDbRecord === false || isChangedPhoto === true) {
            filePath = $('#vehicle-file-uploader-image-preview').prop('src');
            fileUploaderInputHtml = '<input type="file", id="' + fileUploaderId + '", class="new-record", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img src="' + filePath + '", width="50", height="50", id= "' + fileId + '" />';
        }
        else {
            fileUploaderInputHtml = '<input id="' + fileUploaderId + '", class="db-record", type="file", name = "PhotoPath", disabled="true"/>';
            imageTagHtml = '<img id= "' + fileId + '", class="db-record", src="' + filePath + '", width="50", height="50" />';
        }

        //set default values
        if (note === '') {
            note = 'None';
        }

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            // Validate Only New Record ** (i.e. Skip Uploaded From Database)
            if (isDbRecord === false || isChangedPhoto === true) {
                result = false;
                $('#vehicle-file-uploader-error').removeClass('d-none');
            }
        }

        minimumLength = parseInt($('#file-caption').attr('minlength'));
        maximumLength = parseInt($('#file-caption').attr('maxlength'));

        if (parseInt(fileCaption.length) < parseInt(minimumLength) || parseInt(fileCaption.length) > parseInt(maximumLength)) {
            result = false;
            $('#file-caption-error').removeClass('d-none');
        }

        if (result) {
            $('#vehicle-collateral-photo-accordion-error').addClass('d-none');
        }
        else {
            $('#vehicle-collateral-photo-accordion-error').removeClass('d-none');
        }

        return result;
    }
    function HideColumnsVehicleLoanPhotoDataTable() {
        vehicleLoanPhotoDataTable.column(5).visible(false);
        vehicleLoanPhotoDataTable.column(6).visible(false);
        vehicleLoanPhotoDataTable.column(7).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   Loan Against Deposit Collateral Detail  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-deposit-collateral-detail-dt').click(function (event) {
        event.preventDefault();
        editedDepositAccountId = '';
        SetDepositAccountUniqueDropdownList();
        SetModalTitle('deposit-collateral-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-deposit-collateral-detail-dt').click(function () {
        debugger;

        SetModalTitle('deposit-collateral-detail', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#deposit-collateral-detail-modal').modal();

            columnValues = $('#btn-edit-deposit-collateral-detail-dt').data('rowindex');

            editedDepositAccountId = columnValues[1];

            SetDepositAccountUniqueDropdownList();

            $('#deposit-account-id', myModal).val(columnValues[1]);

            $('#mortgage-amount-deposit-collateral', myModal).val(columnValues[3]);

            $('#is-loan-closed-deposit').prop('checked', columnValues[4].toString().toLowerCase() === 'true' ? true : false);

            $('#note-deposit-collateral', myModal).val(columnValues[5]);

            // Show Modals
            $('#deposit-collateral-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-deposit-collateral-detail-edit-dt').addClass('read-only');
            $('#deposit-collateral-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-deposit-collateral-detail-modal').click(function () {
        debugger;
        if (IsValidLoanAgainstDepositCollateralDetailModal()) {
            row = depositCollateralDetailDataTable.row.add([
                tag,
                depositLoanAccountId,
                depositLoanAccountText,
                mortgageAmountLoanDeposit,
                isLoanClosed,
                note,
            ]).draw();

            // Error Message In Span
            $('#loan-against-deposit-collateral-accordion-error').addClass('d-none');

            HideColumnsLoanDepositCollateralDetailDataTable();

            depositCollateralDetailDataTable.columns.adjust().draw();

            $('#deposit-collateral-detail-modal').modal('hide');

            EnableNewOperation('deposit-collateral-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-deposit-collateral-detail-modal').click(function () {
        $('#select-all-deposit-collateral-detail').prop('checked', false);
        if (IsValidLoanAgainstDepositCollateralDetailModal()) {
            depositCollateralDetailDataTable.row(selectedRowIndex).data([
                tag,
                depositLoanAccountId,
                depositLoanAccountText,
                mortgageAmountLoanDeposit,
                isLoanClosed,
                note,
            ]).draw();
            // Error Message In Span
            $('#loan-against-deposit-collateral-detail span').html('');

            HideColumnsLoanDepositCollateralDetailDataTable();

            depositCollateralDetailDataTable.columns.adjust().draw();

            $('#deposit-collateral-detail-modal').modal('hide');

            EnableNewOperation('deposit-collateral-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-deposit-collateral-detail-dt').click(function () {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-deposit-collateral-detail tbody input[type="checkbox"]:checked').each(function () {
                    depositCollateralDetailDataTable.row($('#tbl-deposit-collateral-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-deposit-collateral-detail-dt').data('rowindex');
                    EnableNewOperation('deposit-collateral-detail');

                    SetDepositAccountUniqueDropdownList();

                    $('#select-all-deposit-collateral-detail').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-deposit-collateral-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-deposit-collateral-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = depositCollateralDetailDataTable.row(row).index();

                rowData = (depositCollateralDetailDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-deposit-collateral-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('deposit-collateral-detail');
            });
        }
        else {
            EnableNewOperation('deposit-collateral-detail')

            $('#tbl-deposit-collateral-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-deposit-collateral-detail tbody').click('input[type=checkbox]', function () {
        $('#tbl-deposit-collateral-detail input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = depositCollateralDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (depositCollateralDetailDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('deposit-collateral-detail');

                    $('#btn-update-deposit-collateral-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-deposit-collateral-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-deposit-collateral-detail-dt').data('rowindex', arr);
                    $('#select-all-deposit-collateral-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-deposit-collateral-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('deposit-collateral-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('deposit-collateral-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('deposit-collateral-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-deposit-collateral-detail').prop('checked', true);
        else
            $('#select-all-deposit-collateral-detail').prop('checked', false);
    });

    // Validate Loan Against Deposit Module
    function IsValidLoanAgainstDepositCollateralDetailModal() {
        debugger;
        result = true;
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        mortgageAmountLoanDeposit = parseFloat($('#mortgage-amount-deposit-collateral').val());
        depositLoanAccountId = $('#deposit-account-id option:selected').val();
        depositLoanAccountText = $('#deposit-account-id option:selected').text();
        isLoanClosed = $('#is-loan-closed-deposit').is(':checked') ? true : false;
        note = $('#note-deposit-collateral').val().trim();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-deposit-collateral').val('None');
            note = 'None';
        }

        if ($('#deposit-account-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // Mortgage amount 
        if (isNaN(mortgageAmountLoanDeposit) === false) {
            minimum = parseFloat($('#mortgage-amount-deposit-collateral').attr('min'));
            maximum = parseFloat($('#mortgage-amount-deposit-collateral').attr('max'));

            if (parseFloat(mortgageAmountLoanDeposit) < parseFloat(minimum) || parseFloat(mortgageAmountLoanDeposit) > parseFloat(maximum)) {
                result = false;
                $('#mortgage-amount-deposit-collateral-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#mortgage-amount-deposit-collateral-error').removeClass('d-none');
        }

        return result;
    }
    function HideColumnsLoanDepositCollateralDetailDataTable() {
        depositCollateralDetailDataTable.column(1).visible(false);
    }
    function SetDepositAccountUniqueDropdownList() {
        debugger;
        // Show All Dropdownlist Items
        $('#deposit-account-id').html('');
        $('#deposit-account-id').append(depositAccountDropdownItems)

        // Hide Inserted Dropdownlist Items
        $('#tbl-deposit-collateral-detail > tbody > tr').each(function () {
            debugger;
            currentRow = $(this).closest('tr');
            let myColumnValues = (depositCollateralDetailDataTable.row(currentRow).data());

            if (typeof myColumnValues !== 'undefined' && myColumnValues !== null) {
                debugger;
                if (myColumnValues[1] != editedDepositAccountId) {
                    debugger;
                    $('#deposit-account-id').find('option[value="' + myColumnValues[1] + '"]').remove();
                }
            }
        });
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   Customer Consumer Loan Collateral Detail  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btn-add-consumer-loan-detail-dt').click(function (event) {
        event.preventDefault();
        GetConsumerLoanMargin();
        SetModalTitle('consumer-loan-detail', 'Add');
    });

    $('#btn-edit-consumer-loan-detail-dt').click(function () {
        SetModalTitle('consumer-loan-detail', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#consumer-loan-detail-modal').modal();

            columnValues = $('#btn-edit-consumer-loan-detail-dt').data('rowindex');

            // Display Value In Modal Inputs
            $('#person-id-consumer-loan-detail', myModal).val(columnValues[1]);
            $('#consumer-durable-item-id', myModal).val(columnValues[3]);
            $('#consumer-durable-item-brand-id', myModal).val(columnValues[5]);
            $('#item-other-detail', myModal).val(columnValues[7]);
            $('#manufacture-year-consumer-loan-detail', myModal).val(columnValues[8]);
            $('#serial-number', myModal).val(columnValues[9]);
            $('#product-amount', myModal).val(columnValues[10]);
            $('#down-payment', myModal).val(columnValues[11]);
            $('#warranty-in-month', myModal).val(columnValues[12]);
            $('#guarantee-in-month', myModal).val(columnValues[13]);
            $('#note-consumer-loan-detail', myModal).val(columnValues[14]);
            $('#reason-for-modification-consumer-loan-detail', myModal).val(columnValues[15]);

            GetConsumerLoanMargin();

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-consumer-loan-detail-edit-dt').addClass('read-only');
            $('#consumer-loan-detail-modal').modal('hide');
        }
    });

    $('#btn-add-consumer-loan-detail-modal').click(function () {
        debugger;
        if (IsValidConsumerLoanDetailModal()) {
            row = consumerLoanDetailDataTable.row.add([
                tag,
                personId,
                personIdText,
                consumerDurableItemId,
                consumerDurableItemIdText,
                consumerDurableItemBrandId,
                consumerDurableItemBrandIdText,
                itemOtherDetail,
                manufactureYear,
                serialNumber,
                productAmount,
                downPayment,
                warrantyInMonth,
                guaranteeInMonth,
                note,
                reasonForModification
            ]).draw();

            // Error Message In Span
            //$('#consumer-loan-collateral-accordion-error').addClass('d-none');
            debugger;
            HideColumnsConsumerLoanDetailDataTable();

            consumerLoanDetailDataTable.columns.adjust().draw();

            $('#consumer-loan-detail-modal').modal('hide');

            EnableNewOperation('consumer-loan-detail');
        }
    });

    $('#btn-update-consumer-loan-detail-modal').click(function () {
        $('#select-all-consumer-loan-detail').prop('checked', false);
        if (IsValidConsumerLoanDetailModal()) {
            consumerLoanDetailDataTable.row(selectedRowIndex).data([
                tag,
                personId,
                personIdText,
                consumerDurableItemId,
                consumerDurableItemIdText,
                consumerDurableItemBrandId,
                consumerDurableItemBrandIdText,
                itemOtherDetail,
                manufactureYear,
                serialNumber,
                productAmount,
                downPayment,
                warrantyInMonth,
                guaranteeInMonth,
                note,
                reasonForModification
            ]).draw();

            // Error Message In Span
            //$('#consumer-loan-detail span').html('');

            HideColumnsConsumerLoanDetailDataTable();

            consumerLoanDetailDataTable.columns.adjust().draw();

            $('#consumer-loan-detail-modal').modal('hide');

            EnableNewOperation('consumer-loan-detail');
        }
    });

    $('#btn-delete-consumer-loan-detail-dt').click(function () {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-consumer-loan-detail tbody input[type="checkbox"]:checked').each(function () {
                    consumerLoanDetailDataTable.row($('#tbl-consumer-loan-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-consumer-loan-detail-dt').data('rowindex');
                    EnableNewOperation('consumer-loan-detail');

                    $('#select-all-consumer-loan-detail').prop('checked', false);
                }));
            }
        }
        else {
            alert('Please Select A Checkbox');
        }

    });

    $('#select-all-consumer-loan-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-consumer-loan-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = consumerLoanDetailDataTable.row(row).index();

                rowData = (consumerLoanDetailDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-consumer-loan-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('consumer-loan-detail');
            });
        }
        else {
            EnableNewOperation('consumer-loan-detail')

            $('#tbl-consumer-loan-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    $('#tbl-consumer-loan-detail').click('input[type=checkbox]', function () {
        $('#tbl-consumer-loan-detail input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = consumerLoanDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (consumerLoanDetailDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('consumer-loan-detail');

                    $('#btn-update-consumer-loan-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-consumer-loan-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-consumer-loan-detail-dt').data('rowindex', arr);
                    $('#select-all-consumer-loan-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-consumer-loan-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('consumer-loan-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('consumer-loan-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('consumer-loan-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length) {
            $('#select-all-consumer-loan-detail').prop('checked', true);
        }
        else {
            $('#select-all-consumer-loan-detail').prop('checked', false);
        }

    });
    function IsValidConsumerLoanDetailModal() {
        debugger;
        result = true;
        tag = '<input type="checkbox" name="check_all" class="checks"/>';

        personId = $('#person-id-consumer-loan-detail option:selected').val();
        personIdText = $('#person-id-consumer-loan-detail option:selected').text();
        consumerDurableItemId = $('#consumer-durable-item-id option:selected').val();
        consumerDurableItemIdText = $('#consumer-durable-item-id option:selected').text();
        consumerDurableItemBrandId = $('#consumer-durable-item-brand-id option:selected').val();
        consumerDurableItemBrandIdText = $('#consumer-durable-item-brand-id option:selected').text();
        itemOtherDetail = $('#item-other-detail').val();
        manufactureYear = parseInt($('#manufacture-year-consumer-loan-detail').val());
        serialNumber = $('#serial-number').val();
        productAmount = parseFloat($('#product-amount').val());
        downPayment = parseFloat($('#down-payment').val());
        warrantyInMonth = parseInt($('#warranty-in-month').val());
        guaranteeInMonth = parseInt($('#guarantee-in-month').val());
        note = $('#note-consumer-loan-detail').val();
        reasonForModification = $('#reason-for-modification-consumer-loan-detail').val();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-consumer-loan-detail').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-consumer-loan-detail').val('None');
            reasonForModification = 'None';
        }

        //Person
        if ($('#person-id-consumer-loan-detail').prop('selectedIndex') < 1) {
            result = false;
            $('#person-id-consumer-loan-detail-error').removeClass('d-none');
        }

        //ConsumerDurableItem
        if ($('#consumer-durable-item-id').prop('selectedIndex') < 1) {
            result = false;
            $('#consumer-durable-item-id-error').removeClass('d-none');
        }

        //ConsumerDurableItemBrand
        if ($('#consumer-durable-item-brand-id').prop('selectedIndex') < 1) {
            result = false;
            $('#consumer-durable-item-brand-id-error').removeClass('d-none');
        }

        //itemOtherDetail
        if (isNaN(itemOtherDetail.length) === false) {
            minimumLength = parseInt($('#item-other-detail').attr('minlength'));
            maximumLength = parseInt($('#item-other-detail').attr('maxlength'));

            if (parseInt(itemOtherDetail.length) < parseInt(minimumLength) || parseInt(itemOtherDetail.length) > parseInt(maximumLength)) {
                result = false;
                $('#item-other-detail-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#item-other-detail-error').removeClass('d-none');
        }

        //manufactureYear
        if (isNaN(manufactureYear) === false) {
            minimum = parseInt($('#manufacture-year-consumer-loan-detail').attr('min'));
            maximum = parseInt($('#manufacture-year-consumer-loan-detail').attr('max'));

            if (parseInt(manufactureYear) < parseInt(minimum) || parseInt(manufactureYear) > parseInt(maximum)) {
                result = false;
                $('#manufacture-year-consumer-loan-detail-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#manufacture-year-consumer-loan-detail-error').removeClass('d-none');
        }

        //serialNumber
        if (isNaN(serialNumber.length) === false) {
            minimumLength = parseInt($('#serial-number').attr('minlength'));
            maximumLength = parseInt($('#serial-number').attr('maxlength'));

            if (parseInt(serialNumber.length) < parseInt(minimumLength) || parseInt(serialNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#serial-number-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#serial-number-error').removeClass('d-none');
        }

        //productAmount
        if (isNaN(productAmount) === false) {
            minimum = parseInt($('#product-amount').attr('min'));
            maximum = parseInt($('#product-amount').attr('max'));

            if (parseInt(productAmount) < parseInt(minimum) || parseInt(productAmount) > parseInt(maximum)) {
                result = false;
                $('#product-amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#product-amount-error').removeClass('d-none');
        }

        //DownPayment
        if (isNaN(downPayment) === false) {
            minimum = parseInt($('#down-payment').attr('min'));
            maximum = parseInt($('#down-payment').attr('max'));
            if (parseInt(downPayment) < parseInt(minimum) || parseInt(downPayment) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
            $('#down-payment-error').removeClass('d-none');
        }


        //warrantyInMonth
        if (isNaN(warrantyInMonth) === false) {
            minimum = parseInt($('#warranty-in-month').attr('min'));
            maximum = parseInt($('#warranty-in-month').attr('max'));

            if (parseInt(warrantyInMonth) < parseInt(minimum) || parseInt(warrantyInMonth) > parseInt(maximum)) {
                result = false;
                $('#warranty-in-month-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#warranty-in-month-error').removeClass('d-none');
        }

        //guaranteeInMonth
        if (isNaN(guaranteeInMonth) === false) {
            minimum = parseInt($('#guarantee-in-month').attr('min'));
            maximum = parseInt($('#guarantee-in-month').attr('max'));

            if (parseInt(guaranteeInMonth) < parseInt(minimum) || parseInt(guaranteeInMonth) > parseInt(maximum)) {
                result = false;
                $('#guarantee-in-month-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#guarantee-in-month-error').removeClass('d-none');
        }

        return result;
    }
    function HideColumnsConsumerLoanDetailDataTable() {
        consumerLoanDetailDataTable.column(1).visible(false);
        consumerLoanDetailDataTable.column(3).visible(false);
        consumerLoanDetailDataTable.column(5).visible(false);
        consumerLoanDetailDataTable.column(15).visible(false);
    }

    // Hide Unnecessary Columns

    /// @@@@@@@@@@@@@@@@@@@@@@   Fixed Deposit Collateral  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-fix-deposit-collateral-dt').click(function (event) {
        event.preventDefault();

        SetModalTitle('fix-deposit-collateral', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-fix-deposit-collateral-dt').click(function () {
        debugger;

        SetModalTitle('fix-deposit-collateral', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#fix-deposit-collateral-modal').modal();

            columnValues = $('#btn-edit-fix-deposit-collateral-dt').data('rowindex');

            $('#fix-deposit-account-id', myModal).val(columnValues[1]);

            $('#mortgage-amount-fix-deposit', myModal).val(columnValues[3]);

            $('#is-loan-closed').prop('checked', columnValues[4].toString().toLowerCase() === 'true' ? true : false);

            $('#note-deposit-collateral', myModal).val(columnValues[5]);

            $('#reason-for-modification-deposit-collateral', myModal).val(columnValues[6]);

            // Show Modals
            $('#fix-deposit-collateral-modal').modal('show');
        }
        else {
            $('#btn-edit-fix-deposit-collateral-edit-dt').addClass('read-only');
            $('#fix-deposit-collateral-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-fix-deposit-collateral-modal').click(function () {
        debugger;
        if (IsValidDepositCollateralDetailModal()) {
            row = fixedCollateralDetailDataTable.row.add([
                tag,
                depositAccountId,
                depositAccountText,
                mortgageAmountDeposit,
                isLoanClosed,
                note,
                reasonForModification,
            ]).draw();

            // Error Message In Span
            $('#fix-deposit-collateral-detail-accordion-error').addClass('d-none');

            HideColumnsDepositCollateralDetailDataTable();

            fixedCollateralDetailDataTable.columns.adjust().draw();

            $('#fix-deposit-collateral-modal').modal('hide');

            EnableNewOperation('fix-deposit-collateral');
        }
    });

    // Modal update Button Event
    $('#btn-update-fix-deposit-collateral-modal').click(function () {
        $('#select-all-fix-deposit-collateral').prop('checked', false);
        if (IsValidDepositCollateralDetailModal()) {
            fixedCollateralDetailDataTable.row(selectedRowIndex).data([
                tag,
                depositAccountId,
                depositAccountText,
                mortgageAmountDeposit,
                isLoanClosed,
                note,
                reasonForModification
            ]).draw();
            // Error Message In Span
            $('#fix-deposit-collateral-detail span').html('');

            HideColumnsDepositCollateralDetailDataTable();

            fixedCollateralDetailDataTable.columns.adjust().draw();

            $('#fix-deposit-collateral-modal').modal('hide');

            EnableNewOperation('fix-deposit-collateral');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-fix-deposit-collateral-dt').click(function () {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-fix-deposit-collateral tbody input[type="checkbox"]:checked').each(function () {
                    fixedCollateralDetailDataTable.row($('#tbl-fix-deposit-collateral tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-fix-deposit-collateral-dt').data('rowindex');
                    EnableNewOperation('fix-deposit-collateral');

                    $('#select-all-fix-deposit-collateral').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-fix-deposit-collateral').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-fix-deposit-collateral tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = fixedCollateralDetailDataTable.row(row).index();

                rowData = (fixedCollateralDetailDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-fix-deposit-collateral-dt').data('rowindex', arr);
                EnableDeleteOperation('fix-deposit-collateral');
            });
        }
        else {
            EnableNewOperation('fix-deposit-collateral')

            $('#tbl-fix-deposit-collateral tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-fix-deposit-collateral tbody').click('input[type=checkbox]', function () {
        $('#tbl-fix-deposit-collateral input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = fixedCollateralDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (fixedCollateralDetailDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('fix-deposit-collateral');

                    $('#btn-update-fix-deposit-collateral-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-fix-deposit-collateral-dt').data('rowindex', rowData);
                    $('#btn-delete-fix-deposit-collateral-dt').data('rowindex', arr);
                    $('#select-all-fix-deposit-collateral').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-fix-deposit-collateral tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('fix-deposit-collateral');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('fix-deposit-collateral');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('fix-deposit-collateral');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-fix-deposit-collateral').prop('checked', true);
        else
            $('#select-all-fix-deposit-collateral').prop('checked', false);
    });

    // Validate Court Case Module
    function IsValidDepositCollateralDetailModal() {
        result = true;
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        depositAccountId = $('#fix-deposit-account-id option:selected').val();
        depositAccountText = $('#fix-deposit-account-id option:selected').text();
        mortgageAmountDeposit = parseFloat($('#mortgage-amount-fix-deposit').val());
        isLoanClosed = $('#is-loan-closed').is(':checked') ? true : false;
        note = $('#note-deposit-collateral').val().trim();
        reasonForModification = $('#reason-for-modification-deposit-collateral').val();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-deposit-collateral').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-deposit-collateral').val('None');
            reasonForModification = 'None';
        }

        if ($('#fix-deposit-account-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // Mortgage amount 
        if (isNaN(mortgageAmountDeposit) === false) {
            minimum = parseFloat($('#mortgage-amount-fix-deposit').attr('min'));
            maximum = parseFloat($('#mortgage-amount-fix-deposit').attr('max'));

            if (parseFloat(mortgageAmountDeposit) < parseFloat(minimum) || parseFloat(mortgageAmountDeposit) > parseFloat(maximum)) {
                result = false;
                $('#mortgage-amount-fix-deposit-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#mortgage-amount-fix-deposit-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsDepositCollateralDetailDataTable() {
        fixedCollateralDetailDataTable.column(1).visible(false);
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   Customer Acquaitance Detail  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-customer-acquaitance-detail-dt').click(function (event) {
        debugger;
        event.preventDefault();
        editedSequenceNumber = 0;
        let count = acquaitanceDataTable.rows().count()

        //validate the no.of photos limit from database record
        if (parseInt(count) < parseInt(minimumAcquaintance)) {
            minimumAcquaintance
            SetModalTitle('customer-acquaitance-detail', 'Add');
            $('#customer-acquaitance-detail').modal('show');
            alert('Minimum ' + minimumAcquaintance + ' Acquaintance Required.');
        }
        else if (parseInt(count) >= parseInt(maximumAcquaintance)) {
            $('#customer-acquaitance-detail').modal('hide');
            alert('Maximum ' + maximumAcquaintance + ' Acquaintance Required.');
        }
        else
            SetModalTitle('customer-acquaitance-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-customer-acquaitance-detail-dt').click(function () {
        debugger;
        SetModalTitle('customer-acquaitance-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            myModal = $('#customer-acquaitance-detail-modal').modal();

            columnValues = $('#btn-edit-customer-acquaitance-detail-dt').data('rowindex');

            personInformationNumber = columnValues[1];
            personInformationNumberText = columnValues[2];

            $('#person-information-numbers', myModal).val(columnValues[2]);
            $('#relation-acquaitance-id', myModal).val(columnValues[3]);
            $('#sequence-number-acquaitance-detail', myModal).val(columnValues[5]);
            $('#note-acquaitance-detail', myModal).val(columnValues[6]);

            editedSequenceNumber = columnValues[5];

            // Show Modals
            $('#customer-acquaitance-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-customer-acquaitance-detail-dt').addClass('read-only');
            $('#customer-acquaitance-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-customer-acquaitance-detail-modal').click(function () {
        debugger;
        if (IsValidCustomerAcquaitanceDetailModal()) {
            row = acquaitanceDataTable.row.add([
                tag,
                personInformationNumber,
                personInformationNumberText,
                relationAcquaitanceId,
                relationAcquaitanceText,
                sequenceNumberAcquaitance,
                note,
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideCustomerAcquaitanceDetail();

            acquaitanceDataTable.columns.adjust().draw();

            // Hide Table Required Data Error Message
            $('#acquaitance-detail-accordion-error').addClass('d-none');

            $('#customer-acquaitance-detail-modal').modal('hide');

            EnableNewOperation('customer-acquaitance-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-customer-acquaitance-detail-modal').click(function () {
        debugger;
        $('#select-all-customer-acquaitance-detail').prop('checked', false);
        if (IsValidCustomerAcquaitanceDetailModal()) {
            acquaitanceDataTable.row(selectedRowIndex).data([

                tag,
                personInformationNumber,
                personInformationNumberText,
                relationAcquaitanceId,
                relationAcquaitanceText,
                sequenceNumberAcquaitance,
                note,
            ]).draw();

            HideCustomerAcquaitanceDetail();

            acquaitanceDataTable.columns.adjust().draw();

            $('#customer-acquaitance-detail-modal').modal('hide');

            EnableNewOperation('customer-acquaitance-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-customer-acquaitance-detail-dt').click(function () {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-customer-acquaitance-detail tbody input[type="checkbox"]:checked').each(function () {
                    acquaitanceDataTable.row($('#tbl-customer-acquaitance-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-customer-acquaitance-detail-dt').data('rowindex');
                    EnableNewOperation('customer-acquaitance-detail');
                    $('#select-all-customer-acquaitance-detail').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!acquaitanceDataTable.data().any())
                        $('#acquaitance-detail-accordion-error').removeClass('d-none');


                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-customer-acquaitance-detail').click(function () {
        debugger;
        if ($(this).prop('checked')) {
            $('#tbl-customer-acquaitance-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (acquaitanceDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-customer-acquaitance-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('customer-acquaitance-detail')
            });
        }
        else {
            EnableNewOperation('customer-acquaitance-detail')

            $('#tbl-customer-acquaitance-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-customer-acquaitance-detail tbody').click('input[type="checkbox"]', function () {
        $('#tbl-customer-acquaitance-detail input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = acquaitanceDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (acquaitanceDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('customer-acquaitance-detail');

                    $('#btn-update-customer-acquaitance-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-customer-acquaitance-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-customer-acquaitance-detail-dt').data('rowindex', arr);
                    $('#select-all-customer-acquaitance-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-customer-acquaitance-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('customer-acquaitance-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('customer-acquaitance-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('customer-acquaitance-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-customer-acquaitance-detail').prop('checked', true);
        else
            $('#select-all-customer-acquaitance-detail').prop('checked', false);
    });

    // Validate Fund Module        changes done by indrayani
    function IsValidCustomerAcquaitanceDetailModal() {
        debugger;

        // Get Modal Inputs In Local letiable    
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        relationAcquaitanceId = $('#relation-acquaitance-id option:selected').val();
        relationAcquaitanceText = $('#relation-acquaitance-id option:selected').text();
        sequenceNumberAcquaitance = parseInt($('#sequence-number-acquaitance-detail').val());
        note = $('#note-acquaitance-detail').val();

        let result = true;

        if (note === '') {
            note = 'None';
        }

        //Person Id
        if (personInformationNumberText === '') {
            result = false;
            $('#person-information-numbers-error').removeClass('d-none');
        }

        //Relation Id
        if ($('#relation-acquaitance-id').prop('selectedIndex') < 1) {
            result = false;
            $('#relation-acquaitance-id-error').removeClass('d-none');
        }

        //sequence Number
        if (isNaN(sequenceNumberAcquaitance) === false) {
            minimum = $('#sequence-number-acquaitance-detail').attr('min');
            maximum = $('#sequence-number-acquaitance-detail').attr('max');
            if (parseInt(sequenceNumberAcquaitance) < parseInt(minimum) || parseInt(sequenceNumberAcquaitance) > parseInt(maximum)) {
                result = false;
                $('#sequence-number-acquaitance-detail-error').removeClass('d-none');
            }
            else {
                let filteredData = acquaitanceDataTable
                    .rows()
                    .indexes()
                    .filter(function (value) {
                        return acquaitanceDataTable.row(value).data()[5] == $('#sequence-number-acquaitance-detail').val();
                    });

                if (filteredData.length > 0 && parseInt(editedSequenceNumber) !== sequenceNumberAcquaitance) {
                    isDuplicateSequenceNumber = true;
                    result = false;
                    $('#sequence-number-acquaitance-detail-error').text('Sequence Number Already Exists.').removeClass('d-none');
                }
                else {
                    isDuplicateSequenceNumber = false;

                    $('#sequence-number-acquaitance-detail-error').addClass('d-none');
                }

            }
        }
        else {
            result = false;
            $('#sequence-number-acquaitance-detail-error').removeClass('d-none');
        }


        if (result) {
            $('#acquaitance-detail-accordion-error').addClass('d-none');
        }
        else {
            $('#acquaitance-detail-accordion-error').removeClass('d-none');
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HideCustomerAcquaitanceDetail() {
        acquaitanceDataTable.column(1).visible(false);
        acquaitanceDataTable.column(3).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function (event) {
        debugger;
        let isValidAllInputs = true;

        // Validate Inputs Of Full Page 
        if ($('form').valid()) {
            debugger;
            // not add event.preventDefault
            $('.lastrow').remove();

            // Return List Object, Hence Create Array
            let jointAccountArray = new Array();
            let nomineeDetailArray = new Array();
            let contactDetailArray = new Array();
            let addressDetailArray = new Array();
            let noticeScheduleArray = new Array();
            let guarantorDetailArray = new Array();
            let goldCollateralDetailArray = new Array();
            let fixedCollateralDetailArray = new Array();
            let borrowingDetailArray = new Array();
            let personCourtCaseArray = new Array();
            let personAdditionalIncomeDetailArray = new Array();
            let consumerLoanDetailArray = new Array();
            let acquaitanceDetailArray = new Array();
            let depositCollateralArray = new Array();

            let customerLoanFormData = new FormData($("#form")[0]);

            // To Get All Records From Data Table
            addressDataTable.page.len(-1).draw();
            acquaitanceDataTable.page.len(-1).draw();
            borrowingDetailDataTable.page.len(-1).draw();
            consumerLoanDetailDataTable.page.len(-1).draw();
            contactDataTable.page.len(-1).draw();
            courtCaseDataTable.page.len(-1).draw();
            depositCollateralDetailDataTable.page.len(-1).draw();
            documentDataTable.page.len(-1).draw();
            guarantorDetailDataTable.page.len(-1).draw();
            goldCollateralDetailDataTable.page.len(-1).draw();
            goldLoanPhotoDataTable.page.len(-1).draw();
            incomeDatatable.page.len(-1).draw();
            incomeTaxDataTable.page.len(-1).draw();
            jointAccountDataTable.page.len(-1).draw();
            nomineeDataTable.page.len(-1).draw();
            noticeScheduleDataTable.page.len(-1).draw();
            vehicleLoanPhotoDataTable.page.len(-1).draw();

            // 1. Address Detail - Create Array For Person Address Detail Data Table To Pass Data
            if ($('#address-details-card').hasClass('d-none') === false) {
                if (addressDataTable.data().any()) {
                    $('#address-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {
                        // Get Data Table Values In Turn Over Limit Array
                        $('#tbl-person-address > TBODY > TR').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (addressDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                addressDetailArray.push({
                                    'AddressTypeId': columnValues[1],
                                    'FlatDoorNo': columnValues[3],
                                    'TransFlatDoorNo': columnValues[4],
                                    'NameOfBuilding': columnValues[5],
                                    'TransNameOfBuilding': columnValues[6],
                                    'NameOfRoad': columnValues[7],
                                    'TransNameOfRoad': columnValues[8],
                                    'NameOfArea': columnValues[9],
                                    'TransNameOfArea': columnValues[10],
                                    'CityId': columnValues[11],
                                    'ResidenceTypeId': columnValues[13],
                                    'OwnershipTypeId': columnValues[15],
                                    'IsVerified': columnValues[17],
                                    'Note': columnValues[18],
                                    'TransNote': columnValues[19],
                                    'ReasonForModification': columnValues[20],
                                    'PersonAddressPrmKey': columnValues[21],
                                    'CustomerAccountPrmKey': columnValues[22],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#address-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // 2. Contact Detail - Create Array For contact Data Table To Pass Data
            if ($('#contact-details-card').hasClass('d-none') === false) {
                if (contactDataTable.data().any()) {
                    $('#contact-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {
                        // Get Data Table Values In Turn Over Limit Array
                        $('#tbl-contact > TBODY > TR').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (contactDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                contactDetailArray.push({
                                    'ContactTypeId': columnValues[1],
                                    'FieldValue': columnValues[3],
                                    'IsVerified': columnValues[4],
                                    'VerificationCode': columnValues[5],
                                    'Note': columnValues[6],
                                    'ReasonForModification': columnValues[7],
                                    'PersonContactDetailPrmKey': columnValues[8],
                                    'CustomerAccountPrmKey': columnValues[9],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#contact-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // 3. Joint Account Detail - Create Array For Joint Account Holder Data Table To Pass Data
            if ($('#joint-account-card').hasClass('d-none') === false) {
                dataTableRecordCount = parseInt(jointAccountDataTable.rows().count());

                if (jointAccountDataTable.data().any()) {
                    // Check Required Number Of Joint Accounts
                    if (parseInt(dataTableRecordCount) < parseInt(minimumJointAccountHolder)) {
                        isValidAllInputs = false
                        $('#joint-account-accordion-min-max-error').html('Number Of Joint Account Holders Are Allowed Between ' + minimumJointAccountHolder + ' And ' + maximumJointAccountHolder);
                        $('#joint-account-accordion-min-max-error').removeClass('d-none');
                    }
                    else {
                        $('#joint-account-accordion-error').addClass('d-none');
                        $('#joint-account-accordion-min-max-error').addClass('d-none');

                        if (isValidAllInputs) {
                            // Get Data Table Values In Turn Over Limit Array
                            $('#tbl-joint-account > TBODY > TR').each(function () {
                                currentRow = $(this).closest('tr');

                                columnValues = (jointAccountDataTable.row(currentRow).data());

                                // Handling Code If Row Is Undefined Or Null
                                if (typeof columnValues != 'undefined' && columnValues != null) {
                                    jointAccountArray.push({
                                        'PersonId': columnValues[1],
                                        'JointAccountHolderTypeId': columnValues[3],
                                        'SequenceNumber': columnValues[5],
                                        'ActivationDate': columnValues[6],
                                        'ExpiryDate': columnValues[7],
                                        'Note': columnValues[8],
                                        'ReasonForModification': columnValues[9],
                                    });
                                }
                                else
                                    return false;
                            });
                        }
                    }
                }
                else {
                    if (parseInt(minimumJointAccountHolder) > 0) {
                        isValidAllInputs = false;
                        $('#joint-account-accordion-min-max-error').html('Number Of Joint Account Holders Are Allowed Between ' + minimumJointAccountHolder + ' And ' + maximumJointAccountHolder);
                        $('#joint-account-accordion-min-max-error').removeClass('d-none');
                    }
                    else {
                        $('#joint-account-accordion-error').addClass('d-none');
                        $('#joint-account-accordion-min-max-error').addClass('d-none');
                    }
                }
            }

            // 4. Customer Account Nominee - Create Array For Joint Account Holder Data Table To Pass Data
            if ($('#account-nominee-card').hasClass('d-none') === false) {
                dataTableRecordCount = parseInt(nomineeDataTable.rows().count());

                if (nomineeDataTable.data().any()) {
                    // Check Required Number Of Nominees
                    if (parseInt(dataTableRecordCount) < parseInt(minimumNominee)) {
                        isValidAllInputs = false;
                        $('#account-nominee-accordion-min-max-error').html('Number Of Nominee Are Allowed Between ' + minimumNominee + ' And ' + maximumNominee);
                        $('#account-nominee-accordion-min-max-error').removeClass('d-none');
                    }
                    else {
                        $('#account-nominee-accordion-error').addClass('d-none');
                        $('#account-nominee-accordion-min-max-error').addClass('d-none');

                        if (isValidAllInputs) {
                            // Get Data Table Values In Turn Over Limit Array
                            $('#tbl-account-nominee > TBODY > TR').each(function () {
                                currentRow = $(this).closest('tr');

                                columnValues = (nomineeDataTable.row(currentRow).data());

                                // Handling Code If Row Is Undefined Or Null
                                if (typeof columnValues != 'undefined' && columnValues != null) {
                                    let nomineeGuardianViewModelList = new Array();

                                    if (columnValues[29] != '') {
                                        nomineeGuardianViewModelList.push(
                                            {
                                                'NominationNumber': columnValues[3],
                                                'FullName': columnValues[25],
                                                'TransFullName': columnValues[26],
                                                'PersonInformationNumber': columnValues[27],
                                                'GuardianTypeId': columnValues[29],
                                                'BirthDate': columnValues[31],
                                                'FullAddress': columnValues[32],
                                                'TransFullAddress': columnValues[33],
                                                'ContactDetails': columnValues[34],
                                                'TransContactDetails': columnValues[35],
                                                'AgeProofSubmissionStatusOfTheMinor': columnValues[36],
                                                'AppointedDateOfContact': columnValues[38],
                                                'AppointedTimeOfContact': columnValues[39],
                                                'Note': columnValues[40],
                                                'TransNote': columnValues[41],
                                                'ReasonForModification': columnValues[42],
                                            });
                                    }

                                    nomineeDetailArray.push(
                                        {
                                            'CustomerId': columnValues[1],
                                            'CustomerId': columnValues[2],
                                            'NominationNumber': columnValues[3],
                                            'NominationDate': columnValues[4],
                                            'SequenceNumber': columnValues[5],
                                            'NameOfNominee': columnValues[6],
                                            'TransNameOfNominee': columnValues[7],
                                            'NomineePersonInformationNumber': columnValues[8],
                                            'BirthDate': columnValues[10],
                                            'FullAddressDetails': columnValues[11],
                                            'TransFullAddressDetails': columnValues[12],
                                            'ContactDetails': columnValues[13],
                                            'TransContactDetails': columnValues[14],
                                            'RelationId': columnValues[15],
                                            'HoldingPercentage': columnValues[17],
                                            'ProportionateAmountForEachNominee': columnValues[18],
                                            'ActivationDate': columnValues[19],
                                            'ExpiryDate': columnValues[20],
                                            'CloseDate': columnValues[21],
                                            'Note': columnValues[22],
                                            'TransNote': columnValues[23],
                                            'ReasonForModification': columnValues[24],
                                            'CustomerAccountNomineeGuardianViewModelList': nomineeGuardianViewModelList
                                        });
                                }
                                else
                                    return false;
                            });
                        }
                    }
                }
                else {
                    if (parseInt(minimumNominee) > 0) {
                        isValidAllInputs = false;

                        $('#account-nominee-accordion-min-max-error').html('Number Of Nominee Are Allowed Between ' + minimumNominee + ' And ' + maximumNominee);
                        $('#account-nominee-accordion-min-max-error').removeClass('d-none');
                    }
                    else {
                        $('#account-nominee-accordion-error').addClass('d-none');
                        $('#account-nominee-accordion-min-max-error').addClass('d-none');
                    }
                }
            }

            // Accordion 6 - Acquaitance Detail Data Table Validation
            if ($('#acquaitance-detail-card').hasClass('d-none') === false) {
                if (acquaitanceDataTable.data().any()) {
                    $('#acquaitance-detail-accordion-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In Notice Schedule Array
                        $('#tbl-customer-acquaitance-detail tbody tr').each(function () {
                            currentRow = $(this).closest('tr');
                            columnValues = (acquaitanceDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                acquaitanceDetailArray.push(
                                    {
                                        'PersonInformationNumber': columnValues[1],
                                        'RelationId': columnValues[3],
                                        'SequenceNumber': columnValues[5],
                                        'Note': columnValues[6],
                                    });
                            }
                            else {
                                $('#acquaitance-detail-accordion-error').removeClass('d-none');
                                isValidAllInputs = false;
                            }
                        });
                    }
                }
            }

            // Create Array For Gold Loan Photo Table To Pass Data
            if ($('#gold-collateral-photo-card').hasClass('d-none') === false) {
                debugger;
                if (goldLoanPhotoDataTable.data().any()) {

                    $('#gold-collateral-photo-accordion-error').addClass('d-none');

                    let dataTableRecordCount = parseInt(goldLoanPhotoDataTable.rows().count());
                    // Check Required Number Of photos
                    if (parseInt(dataTableRecordCount) < parseInt(minimumGoldPhoto)) {
                        isValidAllInputs = false;
                        $('#gold-collateral-photo-accordion-error').html('Upload Gold Collateral Photo Between' + minimumGoldPhoto + ' And ' + maximumGoldPhoto);
                        $('#gold-collateral-photo-accordion-error').removeClass('d-none');
                    }
                    else {
                        $('#gold-collateral-photo-accordion-error').addClass('d-none');
                    }

                    if (isValidAllInputs) {

                        $('#tbl-gold-collateral-photo > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (goldLoanPhotoDataTable.row(currentRow).data());

                            if (typeof columnValues == 'undefined' && columnValues == null) {
                                return false;
                            }
                            else {

                                let row = $(this);
                                customerLoanFormData.append("_goldLoanCollateralPhoto[" + i + "].SequenceNumber", columnValues[1]);
                                customerLoanFormData.append("_goldLoanCollateralPhoto[" + i + "].PhotoType", columnValues[3]);
                                customerLoanFormData.append("_goldLoanCollateralPhoto[" + i + "].PhotoPath", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                customerLoanFormData.append("_goldLoanCollateralPhoto[" + i + "].Photo", columnValues[6]);
                                customerLoanFormData.append("_goldLoanCollateralPhoto[" + i + "].PhotoCaption", columnValues[7]);
                                customerLoanFormData.append("_goldLoanCollateralPhoto[" + i + "].Note", columnValues[8]);
                                customerLoanFormData.append("_goldLoanCollateralPhoto[" + i + "].NameOfFile", columnValues[9]);
                                customerLoanFormData.append("_goldLoanCollateralPhoto[" + i + "].CustomerGoldLoanCollateralPhotoPrmKey", columnValues[10]);
                                customerLoanFormData.append("_goldLoanCollateralPhoto[" + i + "].LocalStoragePath", columnValues[11]);
                            }
                        });
                    }
                }
                else {
                    $('#gold-collateral-photo-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // 1. Gold Loan Collateral Detail - Create Array For Gold Loan Collateral Detail Data Table To Pass Data
            if ($('#gold-loan-collateral-detail-card').hasClass('d-none') === false) {
                if (goldCollateralDetailDataTable.data().any()) {
                    $('#gold-collateral-detail-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {
                        // Get Data Table Values In Turn Over Limit Array
                        $('#tbl-gold-collateral-detail > TBODY > TR').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (goldCollateralDetailDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                goldCollateralDetailArray.push({
                                    'JewelAssayerId': columnValues[1],
                                    'GoldOrnamentId': columnValues[3],
                                    'SequenceNumber': columnValues[5],
                                    'MetalPurity': columnValues[6],
                                    'HUID': columnValues[8],
                                    'Qty': columnValues[9],
                                    'MetalGrossWeight': columnValues[10],
                                    'HasAnyDamage': columnValues[11],
                                    'DamageWeight': columnValues[12],
                                    'DamageDescription': columnValues[13],
                                    'HasAnyWestage': columnValues[14],
                                    'WestageWeight': columnValues[15],
                                    'WestageDescription': columnValues[16],
                                    'IsDiamondDeductable': columnValues[17],
                                    'HasDiamond': columnValues[18],
                                    'NumberOfDiamond': columnValues[19],
                                    'DiamondCarat': columnValues[20],
                                    'ClarityColour': columnValues[21],
                                    'DiamondWeight': columnValues[22],
                                    'DiamondPrice': columnValues[23],
                                    'DiamondValuation': columnValues[24],
                                    'MetalNetWeight': columnValues[25],
                                    'GoldValuationAmount': columnValues[26],
                                    'CustodyStatus': columnValues[27],
                                    'JewelAssayerRemark': columnValues[29],
                                    'Note': columnValues[30],
                                    'ReasonForModification': columnValues[31],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#gold-collateral-detail-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // 1. loan against Deposit Collateral Detail - Create Array For loan against Deposit Collateral Detail Data Table To Pass Data
            if ($('#loan-against-deposit-collateral-detail-card').hasClass('d-none') === false) {
                if (depositCollateralDetailDataTable.data().any()) {
                    $('#loan-against-deposit-collateral-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {
                        // Get Data Table Values In Turn Over Limit Array
                        $('#tbl-deposit-collateral-detail > TBODY > TR').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (depositCollateralDetailDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                depositCollateralArray.push({
                                    'DepositAccountId': columnValues[1],
                                    'MortgageAmount': columnValues[3],
                                    'IsLoanClosed': columnValues[4],
                                    'Note': columnValues[5],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#loan-against-deposit-collateral-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // 1. Fixed Deposit Collateral Detail - Create Array For Fixed Deposit Collateral Detail Data Table To Pass Data
            if ($('#fix-deposit-collateral-detail-card').hasClass('d-none') === false) {
                if (fixedCollateralDetailDataTable.data().any()) {
                    $('#fix-deposit-collateral-detail-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {
                        // Get Data Table Values In Turn Over Limit Array
                        $('#tbl-fix-deposit-collateral > TBODY > TR').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (fixedCollateralDetailDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                fixedCollateralDetailArray.push({
                                    'DepositAccountId': columnValues[1],
                                    'MortgageAmount': columnValues[3],
                                    'IsLoanClosed': columnValues[4],
                                    'Note': columnValues[5],
                                    'ReasonForModification': columnValues[6],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#fix-deposit-collateral-detail-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For Document detail Table To Pass Data
            if ($('#document-card').hasClass('d-none') === false) {
                debugger;
                if (documentDataTable.data().any()) {

                    $('#document-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-document > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (documentDataTable.row(currentRow).data());

                            if (typeof columnValues == 'undefined' && columnValues == null) {
                                return false;
                            }
                            else {

                                let row = $(this);
                                customerLoanFormData.append("_document[" + i + "].DocumentId", columnValues[1]);
                                customerLoanFormData.append("_document[" + i + "].SequenceNumber", columnValues[3]);
                                customerLoanFormData.append("_document[" + i + "].FileUploader", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                customerLoanFormData.append("_document[" + i + "].Photo", columnValues[5]);
                                customerLoanFormData.append("_document[" + i + "].FileCaption", columnValues[6]);
                                customerLoanFormData.append("_document[" + i + "].Note", columnValues[7]);
                                customerLoanFormData.append("_document[" + i + "].ReasonForModification", columnValues[8]);
                                customerLoanFormData.append("_document[" + i + "].NameOfFile", columnValues[9]);
                                customerLoanFormData.append("_document[" + i + "].CustomerAccountDocumentPrmKey", columnValues[10]);
                                customerLoanFormData.append("_document[" + i + "].LocalStoragePath", columnValues[11]);
                            }
                        });
                    }
                }
                else {
                    $('#document-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Accordion 6 - Notice Schedule Data Table Validation (Optional, Not Mandatory)
            if ($('#notice-schedule-card').hasClass('d-none') === false) {
                if (noticeScheduleDataTable.data().any()) {
                    $('#notice-schedule-accordion-error').addClass('d-none');
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
                            else {
                                $('#notice-schedule-accordion-error').removeClass('d-none');
                                isValidAllInputs = false;
                            }
                        });
                    }
                }
            }

            // 3. Guarantor Detail - Create Array For Joint Account Holder Data Table To Pass Data
            if ($('#guarantor-detail-card').hasClass('d-none') === false) {
                debugger;
                dataTableRecordCount = parseInt(guarantorDetailDataTable.rows().count());

                if (guarantorDetailDataTable.data().any()) {
                    // Check Required Number Of Joint Accounts
                    if (parseInt(dataTableRecordCount) < parseInt(minimumNumberOfGuarantor)) {
                        isValidAllInputs = false
                        $('#guarantor-detail-min-max-error').html('Number Of Guarantor Detail Are Allowed Between ' + minimumNumberOfGuarantor + ' And ' + maximumNumberOfGuarantor);
                        $('#guarantor-detail-min-max-error').removeClass('d-none');
                    }
                    else {
                        $('#guarantor-detail-accordion-error').addClass('d-none');
                        $('#guarantor-detail-min-max-error').addClass('d-none');

                        if (isValidAllInputs) {
                            // Get Data Table Values In Turn Over Limit Array
                            $('#tbl-guarantor-detail > TBODY > TR').each(function () {
                                currentRow = $(this).closest('tr');

                                columnValues = (guarantorDetailDataTable.row(currentRow).data());

                                // Handling Code If Row Is Undefined Or Null
                                if (typeof columnValues != 'undefined' && columnValues != null) {
                                    guarantorDetailArray.push({
                                        'PersonId': columnValues[1],
                                        'SequenceNumber': columnValues[3],
                                        'GuaranteePercentage': columnValues[4],
                                        'Note': columnValues[5],
                                    });
                                }
                                else
                                    return false;
                            });
                        }
                    }
                }
                else {
                    if (parseInt(minimumNumberOfGuarantor) > 0) {
                        isValidAllInputs = false;
                        $('#guarantor-detail-min-max-error').html('Number Of Guarantor Details Are Allowed Between ' + minimumNumberOfGuarantor + ' And ' + maximumNumberOfGuarantor);
                        $('#guarantor-detail-min-max-error').removeClass('d-none');
                    }
                    else {
                        $('#guarantor-detail-accordion-error').addClass('d-none');
                        $('#guarantor-detail-min-max-error').addClass('d-none');
                    }
                }
            }

            // Create Array For borrowing detail Table To Pass Data
            if ($('#borrowing-detail-card').hasClass('d-none') === false) {
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
                                borrowingDetailArray.push(
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
                                        'ReasonForModification': columnValues[31],
                                        'PersonBorrowingDetailPrmKey': columnValues[32],
                                        'CustomerAccountPrmKey': columnValues[33],

                                    });
                            }
                            else
                                return false;
                        });
                    }

                }
                else {
                    $('#borrowing-detail-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For person income detail Table To Pass Data
            if ($('#income-details-card').hasClass('d-none') === false) {

                if (incomeDatatable.data().any()) {

                    $('#income-details-accordion-error').addClass('d-none');

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
                                        'ReasonForModification': columnValues[6],
                                        'PersonAdditionalIncomeDetailPrmKey': columnValues[7],
                                        'CustomerAccountPrmKey': columnValues[8],
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                //else {
                //    $('#income-details-accordion-error').removeClass('d-none');
                //    isValidAllInputs = false;
                //}
            }

            // Create Array For person income tax detail case Table To Pass Data
            if ($('#income-tax-card').hasClass('d-none') === false) {

                if (incomeTaxDataTable.data().any()) {

                    $('#income-tax-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-income-tax > tbody > tr').each(function (i) {
                            currentRow = $(this).closest('tr');

                            columnValues = (incomeTaxDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {

                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].AssessmentYear", columnValues[1]);
                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].TaxAmount", columnValues[2]);
                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].PhotoPathTax", $(currentRow).find("TD").find("input[type='file']").get(0).files[0]);
                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].FileCaption", columnValues[5]);
                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].Note", columnValues[6]);
                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].ReasonForModification", columnValues[7]);
                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].NameOfFile", columnValues[8]);
                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].PersonIncomeTaxDetailDocumentPrmKey", columnValues[9]);
                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].LocalStoragePath", columnValues[10]);
                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].PersonIncomeTaxDetailPrmKey", columnValues[11]);
                                customerLoanFormData.append("_personIncomeTaxDetail[" + i + "].CustomerAccountPrmKey", columnValues[12]);
                            }
                        });
                    }
                }
                //else {
                //    $('#income-tax-accordion-error').removeClass('d-none');
                //    isValidAllInputs = false;
                //}
            }

            // Create Array For person court case Table To Pass Data
            if ($('#court-case-card').hasClass('d-none') === false) {

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
                                        'ReasonForModification': columnValues[13],
                                        'PersonCourtCasePrmKey': columnValues[14],
                                        'CustomerAccountPrmKey': columnValues[15]
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
            }

            // Create Array For consumer Loan Detail Table To Pass Data
            if ($('#consumer-loan-detail-card').hasClass('d-none') === false) {
                debugger;
                if (consumerLoanDetailDataTable.data().any()) {

                    $('#consumer-loan-collateral-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-consumer-loan-detail > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (consumerLoanDetailDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                debugger;
                                consumerLoanDetailArray.push(
                                    {
                                        'PersonId': columnValues[1],
                                        'ConsumerDurableItemId': columnValues[3],
                                        'ConsumerDurableItemBrandId': columnValues[5],
                                        'ItemOtherDetail': columnValues[7],
                                        'ManufactureYear': columnValues[8],
                                        'SerialNumber': columnValues[9],
                                        'ProductAmount': columnValues[10],
                                        'DownPayment': columnValues[11],
                                        'WarrantyInMonth': columnValues[12],
                                        'GuaranteeInMonth': columnValues[13],
                                        'Note': columnValues[14],
                                        'ReasonForModification': columnValues[15]
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
            }

            // 1. vehicle Loan Photo - Create Array For vehicle Loan Photo Data Table To Pass Data
            if ($('#vehicle-photo-card').hasClass('d-none') === false) {
                if (vehicleLoanPhotoDataTable.data().any()) {
                    $('#vehicle-collateral-photo-accordion-error').addClass('d-none');
                    let dataTableRecordCount = parseInt(vehicleLoanPhotoDataTable.rows().count());
                    // Check Required Number Of photos
                    if (parseInt(dataTableRecordCount) < parseInt(minimumVehiclePhoto)) {
                        isValidAllInputs = false;
                        $('#vehicle-collateral-photo-accordion-error').html('Upload Vehicle Photo Between ' + minimumVehiclePhoto + ' And ' + maximumVehiclePhoto + ' photos');
                        $('#vehicle-collateral-photo-accordion-error').removeClass('d-none');
                    }
                    else {
                        $('#vehicle-collateral-photo-accordion-error').addClass('d-none');
                    }


                    if (isValidAllInputs) {
                        // Get Data Table Values In vehicle Loan Photo Array
                        $('#tbl-vehicle-loan-photo > TBODY > TR').each(function (i) {
                            debugger;
                            currentRow = $(this).closest('tr');

                            columnValues = (vehicleLoanPhotoDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues === 'undefined' && columnValues === null) {
                                return false;
                            }
                            else {
                                row = $(this);
                                customerLoanFormData.append("_vehicleLoanPhoto[" + i + "].PhotoPath", $(row).find("TD").find("input[type='file']").get(0).files[0]);
                                customerLoanFormData.append("_vehicleLoanPhoto[" + i + "].PhotoCaption", columnValues[3]);
                                customerLoanFormData.append("_vehicleLoanPhoto[" + i + "].Note", columnValues[4]);
                                customerLoanFormData.append("_vehicleLoanPhoto[" + i + "].NameOfFile", columnValues[5]);
                                customerLoanFormData.append("_vehicleLoanPhoto[" + i + "].CustomerVehicleLoanPhotoPrmKey", columnValues[6]);
                                customerLoanFormData.append("_vehicleLoanPhoto[" + i + "].LocalStoragePath", columnValues[7]);
                            }
                        });
                    }
                }
                else {
                    $('#vehicle-collateral-photo-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // 6. SMS - Normal Accordion
            if (IsValidSMSServiceDetailAccordionInputs() === false) {
                isValidAllInputs = false;
            }

            // 7. Email - Normal Accordion
            if (IsValidEmailServiceDetailAccordionInputs() === false) {
                isValidAllInputs = false;
            }

            // If Loan Type Is Vehicle
            if ($('#vehicle-loan-section').hasClass('d-none') === false) {
                // Vehicle Loan Collateral Detail Accordion Inputs
                if (IsValidVehicleLoanCollateralDetailAccordionInputs() === false) {
                    isValidAllInputs = false;
                }

                // PreOwned Vehicle Loan Inspection Accordion Inputs
                if (IsValidPreOwnedVehicleLoanInspectionAccordionInputs() === false) {
                    isValidAllInputs = false;
                }

                // Vehicle Insurance Detail Accordion Inputs
                if (IsValidVehicleInsuranceDetailAccordionInputs() === false) {
                    isValidAllInputs = false;
                }

                // Vehicle contract Detail Accordion Inputs
                if (IsValidVehicleContractDetailAccordionInputs() === false) {
                    isValidAllInputs = false;
                }

            }

            // Field Investigation Accordion Inputs
            if (IsValidFieldInvestigationAccordionInputs() === false) {
                isValidAllInputs = false;
            }

            // Debt To Income Ratio Accordion Inputs
            if (IsValidDebtToIncomeRatioAccordionInputs() === false) {
                isValidAllInputs = false;
            }

            // Loan Against Property Collateral Detail Accordion Inputs
            if (IsValidLoanAgainstPropertyAccordionInputs() === false) {
                isValidAllInputs = false;
            }

            // Cash Credit Loan
            if (IsValidCashCreditLoanAccordionInput() === false) {
                isValidAllInputs = false;
            }

            // Accordion 6 - Standing Instruction Validation, If Enable
            if (IsValidStandingInstructionAccordionInputs() === false) {
                isValidAllInputs = false;
            }

            //CustomerEducationLoanDetail
            if (IsValidCustomerEducationLoanDetailAccordionInputs() === false) {
                isValidAllInputs = false;
            }

            // Call Cantroller Save Data Table Method 
            if (isValidAllInputs) {
                debugger;
                $.ajax(
                    {
                        url: customerAccountDataTableUrl,
                        type: 'POST',
                        dataType: 'json',
                        async: false,
                        data: {
                            '_customerJointAccountHolder': jointAccountArray, '_customerAccountNominee': nomineeDetailArray, '_acquaintanceDetail': acquaitanceDetailArray, '_personContactDetail': contactDetailArray, '_personAddress': addressDetailArray, '_customerAccountNoticeSchedule': noticeScheduleArray, '_customerAccountGuarantorDetail': guarantorDetailArray, '_goldLoanCollateralDetail': goldCollateralDetailArray, '_fixedDepositCollateral': fixedCollateralDetailArray, '_borrowingDetail': borrowingDetailArray, '_personCourtCase': personCourtCaseArray, '_personAdditionalIncomeDetail': personAdditionalIncomeDetailArray, '_consumerLoanCollateralDetail': consumerLoanDetailArray, '_loanAgainstDepositCollateralDetail': depositCollateralArray
                        },
                        ContentType: 'application/json;',

                        success: function () {
                            CustomerLoanImagesDataTable();
                        },
                        error: function (xhr, status, error) {
                            alert('An Error Has Occured In Customer Account DataTable!!! Error Message - ' + error.toString());
                        }

                    });

            }
            else {
                alert('Oops, Something Went Wrong!, Please Provide Valid Data');
                event.preventDefault();
            }

            //CustomerLoanImagesDataTable
            function CustomerLoanImagesDataTable() {
                debugger;
                $.ajax({
                    url: customerLoanImageDataTableUrl,
                    type: 'POST',
                    async: false,
                    data: customerLoanFormData,
                    contentType: false, // Not to set any content header 
                    processData: false, // Not to process data 
                    cache: false,
                    //ContentType: "application/json; charset=utf-8",
                    dataType: 'JSON',
                    enctype: 'multipart/form-data',

                    success: function () {

                        alert('Ok');
                    },

                    error: function (xhr, status, error) {

                        alert("An Error Has Occured In Images DataTable!!! Error Message - " + error.toString());
                    }
                });
            }

        }
        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });
});
