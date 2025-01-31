'use strict'
$(document).ready(function () {
    debugger;
    const DEMAND_DEPOSIT = 'DMN';
    const TERM_DEPOSIT = 'FDP';
    const RECURRING_DEPOSIT = 'REC';
    const AT_MATURITY = 'MAT';
    const MONTHLY = 'MON';
    const QUARTERLY = 'QRT';
    const SEMI_ANNUALLY = 'SAN';
    const ANNUALLY = 'ANL';
    const DO_NOT_RENEW = 'DNR';

    const ADDRESS_TYPE_DROPDOWN_LIST = $('#address-type-id').html();
    const AGENT_DROPDOWN_LIST = $('#agent-id').html();
    const INTEREST_PAYOUT_FREQUENCY_DROPDOWN_LIST = $('#interest-payout-frequency').html();

    // DropdownList Variables
    let personDropdownListData = '';
    let personDropdownListDataForJointAccount = '';
    let personDropdownListDataForNominee = '';
    let personDropdownListDataForGuardian = '';
    let personDropdownListDataForReference = '';

    let isVerifyView = false;
    let isAmendView = false;
    // Array
    let finalDropdownListArray = [];

    // Count
    let dropDownListItemCount = 0;

    // Global Variable
    let minimum = 0;
    let maximum = 0;
    let minimumLength = 0;
    let maximumLength = 0;
    let result = true;
    let minMaxResult = true;
    let listItemCount = 0;
    let dropdownListItems = '';
    let dataTableRecordCount = 0;
    let selectedPersonText;
    let isUpdate = false;
    let prevPersonId = $('#person-id option:selected').val();
    let prevBusinessOfficeId = '0';
    let prevGeneralLedger;
    let prevDepositTypeId = '0';
    let prevGeneralLedgerId = '0';
    let prevSchemeId = '0';
    let contactType;
    let isMobile;
    let isEmail;
    let day;
    let month;
    let year;
    let time;
    let today;
    let meridian;
    let hours;
    let datepart;
    let minutes;
    let birthDate;
    let rowNum = 0;
    let minimumJointAccountHolder = 0;
    let maximumJointAccountHolder = 0;
    let minimumNominee = 0;
    let maximumNominee = 0;
    let maximumTenureIdDays = 0;
    let maxPhotoFileSize = 0;
    let validPhotoFileExtensions = '';
    let maxSignFileSize = 0;
    let validSignFileExtensions = '';
    let isEnablePayoutInterest = false;

    // @@@@@@@@@@ Data Table Related Varible Declaration
    let tag = '';
    let id;
    let myModal;
    let selectedRowIndex;
    let row;
    let rowData;
    let checked;
    let columnValues;

    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;

    let arr = new Array();
    let dateSplitArray;

    // Nominee Detail
    let selectedPersonId = '';
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
    let editedNomineeCustomerId = '';

    // Contact
    let contactTypeText;
    let fieldValue = 0;
    let isVerified = true;
    let verificationCode = '';
    let contactDetailPrmKey = 0;
    let hasDivClass;
    let entryStatus;
    let isDuplicateContact = false;
    let isDuplicateSequenceNumber = false;
    let isDuplicateNomineeNumber = false;

    //Address Detail
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
    let personContactDetailPrmkey = 0;
    let customerAccountPrmKey = 0;
    let editedAddressTypeId = '';

    // Joint Account
    let personId = '';
    let personIdText = '';
    let jointAccountHolderId = '';
    let jointAccountHolderIdText = '';
    let jointAccountHolderActivationDate = '';
    let jointAccountHolderExpiryDate = '';
    let editedSequenceNumber = 0;
    let editedNomineeNumber = 0;
    let editedJointAccountPersonId = '';
    let editedJointAccountPersonText = '';
    let selectedjointPersonId = '';
    let selectedjointPersonText = '';

    //Turn Over Limit
    let frequency = 0;
    let frequencyText = '';
    let transactionType = '';
    let transactionTypeText = '';
    let amount = 0;

    // NoticeSchedule
    let noticeTypeId = '';
    let noticeScheduleText = '';
    let communicationMediaId = '';
    let communicationMediaText = '';
    let scheduleId = '';
    let scheduleText = '';

    // agent
    let agentId = '';
    let agentText = '';
    let agentActivationDate = '';
    let agentExpiryDate = 0;
    let rVisibility = '';
    let editedAgentId = '';

    // Document
    let documentId = '';
    let documentText = '';
    let dochtml = '';
    let dochtml1 = '';
    let expectedSubmitDate = '';
    let actualSubmitDate = '';
    let photo;
    let newID = '';
    let photoPathDocument = '';
    let allowedExtensionsArr = '';
    let requiredDocument = 0;

    // BeneficiaryDetails
    let nameOfBeneficiaryCode = '';
    let nameOfBeneficiary = '';
    let shortName = '';
    let customerAccountTypeId = '';
    let customerAccountTypeIdText = '';
    let accountNumber = 0;
    let ifscCode = 0;
    let bankName = '';
    let branch = '';
    let customerNumber = 0;
    let mobileNumber = 0;
    let emailId = '';
    let virtualPrivateAddress = '';
    let tempDate = '';

    let isDuplicateBeneficiaryAccountNumber = false;

    // ReferencePersonDetail 
    let customerAccountNumberId;
    let customerAccountNumberText;
    let isValidateSign;
    let minimumReferencePerson = 0;
    let maximumReferencePerson = 0;
    let selectedReferencePersonId = ''
    let selectedReferencePersonText = ''

    // Term / Fixed Deposit
    let depositMultipleOfThereAfter = 0;
    let oldDepositAmount = 0;
    let renewTypeSysName = '';

    // Recurring Deposit
    let installmentAmountMultipleOf = 0;

    // Deposit Inteest Parameter
    let minimumOverrideInterestAmount = 0;
    let maximumOverrideInterestAmount = 0;

    let url = window.location.href;
    let index = url.lastIndexOf('/') + 1;
    let filenameWithExtension = url.substr(index);
    let filename = filenameWithExtension.split('?')[0]  // <-- added this line
    let winname = filename;

    let nomineeDataTable = CreateDataTable('account-nominee');
    let jointAccountDataTable = CreateDataTable('joint-account');
    let contactDataTable = CreateDataTable('contact');
    let addressDataTable = CreateDataTable('person-address');
    let turnOverLimitDataTable = CreateDataTable('turn-over-limit');
    let agentDataTable = CreateDataTable('agent');
    let noticeScheduleDataTable = CreateDataTable('notice-schedule');
    let beneficiaryDetailDataTable = CreateDataTable('beneficiary-detail');
    let referencePersonDetailDataTable = CreateDataTable('reference-person-detail');
    let multiAccountTermDepositInput;

    let schemeId = $('#scheme-id').val();

    // Set Page Loading Default Values (Usually For Amend Page)
    SetPageLoadingDefaultValues();

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Sms Send - Display SMS Delilety Status
    $('#send-code').click(function (event) {
        let _mobileNumber = $('#field-value').val();
        $('#send-code').addClass('d-none');

        $.get('/SMS/SendVerificationToken', { MobileNumber: _mobileNumber, async: false }, function (data, textStatus, jqXHR) {
            debugger;
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
    $('#resend').click(function (event) {
        let _mobileNumber = $('#field-value').val()

        $.get('/SMS/SendVerificationToken', { MobileNumber: _mobileNumber, async: false }, function (data, textStatus, jqXHR) {
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

    // Enable Auto Renew
    $('#enable-auto-renew-on-maturity').change(function (event) {
        EnableAutoRenewOnMaturityClickHandlerFunction();
    });

    // Auto Debit On Maturity
    $('#enable-auto-debit').change(function (event) {
        if ($(this).is(':checked'))
            $('.customer-account-standing-instruction').removeClass('d-none');
        else {
            if (!$('#enable-auto-close-on-maturity').is(':checked') && renewTypeSysName != 'Principal')
                $('.customer-account-standing-instruction').addClass('d-none');

            $('#customer-saving-account-debit').prop('selectedIndex', 0);
        }
    });

    // Auto Close On Maturity
    $('#enable-auto-close-on-maturity').change(function (event) {
        EnableAutoCloseOnMaturityClickHandlerFunction();
    });

    // Enable All Services Of SMS 
    $('#enable-all-service').change(function (event) {
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
    $('.enable-service').change(function (event) {
        if ($('#enable-credit-transaction-sms').is(':checked') && $('#enable-debit-transaction-sms').is(':checked') && $('#enable-insufficient-balance-sms').is(':checked'))
            $('#enable-all-service').prop('checked', true);
        else
            $('#enable-all-service').prop('checked', false);
    });

    // Enable All Services Of EMAIL 
    $('#enable-all-email-service').change(function (event) {
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
    $('.enable-email-service').change(function (event) {
        if ($('#enable-credit-transaction-email').is(':checked') && $('#enable-debit-transaction-email').is(':checked') && $('#enable-insufficient-balance-email').is(':checked') && $('#enable-statement-email').is(':checked'))
            $('#enable-all-email-service').prop('checked', true);
        else
            $('#enable-all-email-service').prop('checked', false);
    });

    // Clear Turnover Limit Datatable
    $('#enable-turn-over-limit').change(function (event) {
        turnOverLimitDataTable.clear().draw();
    });

    // Calculate All Deposit Amount
    $('.multi-deposit').keyup(function () {
        debugger;
        let total = 0;
        let array = $('input[name="CustomerTermDepositAccountDetailViewModel.NoOfDepositsAmount[]"]').map(function (idx, ele) {
            total += parseInt($(this).val()) || 0;
            return total;
        }).get();

        $('.no-of-inputs').next('div.schedule-time').remove();
        $('.no-of-inputs').after('<div class="form-group schedule-time"><label class="font-weight-bold">Total Deposit Amount</label><input value=' + total + '  name="TotalDepositAccounts" type ="text" class="form-control mandatory-mark" /></div></div>');
    })


    // ############# F O C U S   O U T    E V E N T

    //Business Office Id
    $('#business-office-id').focusout(function (event) {
        if (!isVerifyView) {
            let businessOfficeId = $('#business-office-id option:selected').val();

            if (prevBusinessOfficeId != businessOfficeId) {
                if (prevBusinessOfficeId != 0)
                    $('#business-office-id-error').addClass('d-none')

                prevBusinessOfficeId = businessOfficeId;

                // Clear Dependent Data
                $('#general-ledger-id').prop('selectedIndex', 0);
                $('#scheme-id').prop("selectedIndex", -1);
                $('#deposit-type-id').prop("selectedIndex", -1);
                $('#person-id').val('');

                addressDataTable.clear().draw();
                contactDataTable.clear().draw();
                jointAccountDataTable.clear().draw();
                nomineeDataTable.clear().draw();
                referencePersonDetailDataTable.clear().draw();
            }
            else {
                $('#business-office-id-error').removeClass('d-none')
                prevBusinessOfficeId = $('#business-office-id option:selected').val();
            }
        }
    });

    // On Changing Business Office, All Dependent Setting (Loan Type , Genreral Ledger, Scheme) Required To Be Clear.
    $('#deposit-type-id').focusout(function (event) {
        if (!isVerifyView) {
            debugger;
            let depositTypeId = $('#deposit-type-id option:selected').val();
            let businessOfficeId = $('#business-office-id option:selected').val();

            if (prevDepositTypeId != depositTypeId) {
                SetGeneralLedgerDropdownList();

                if (prevDepositTypeId != '')
                    $('#deposit-type-id-error').addClass('d-none')

                prevDepositTypeId = depositTypeId;

                SetDepositeTypeSetting();

                // Clear Dependent Data
                $('#general-ledger-id').prop('selectedIndex', 0);
                $('#scheme-id').prop('selectedIndex', 0);
                $('#person-id').val('');

                addressDataTable.clear().draw();
                contactDataTable.clear().draw();
                jointAccountDataTable.clear().draw();
                nomineeDataTable.clear().draw();
                turnOverLimitDataTable.clear().draw();
            }
            else {
                $('#deposit-type-id-error').removeClass('d-none');
                prevDepositTypeId = $('#deposit-type-id option:selected').val();
            }
        }
    });

    // On Changing General Ledger, All Dependent Setting (Scheme) Required To Be Clear.
    $('#general-ledger-id').focusout(function (event) {
        if (!isVerifyView) {
            let selectedGeneralLedgerId = $('#general-ledger-id option:selected').val();

            if (prevGeneralLedgerId != selectedGeneralLedgerId) {
                // Clear
                if (prevGeneralLedgerId != '0')
                    $('#general-ledger-change-info').removeClass('d-none');

                // Set Scheme Dropdown List Based On Selected General Ledger
                SetSchemeDropdownList();

                if (prevGeneralLedgerId != 0)
                    $('#general-ledger-change-info').addClass('d-none');

                prevGeneralLedgerId = selectedGeneralLedgerId;

                // Clear Dependent Data
                $('#person-id').val('');

                addressDataTable.clear().draw();
                contactDataTable.clear().draw();
                jointAccountDataTable.clear().draw();
                nomineeDataTable.clear().draw();
            }
            else {
                $('#general-ledger-change-info').addClass('d-none');
                prevGeneralLedgerId = $('#general-ledger-id option:selected').val();
            }
        }
    });

    // Scheme Id
    $('#scheme-id').focusout(function (event) {
        if (!isVerifyView) {
            debugger;
            schemeId = $('#scheme-id option:selected').val();

            if (schemeId != '00000000-0000-0000-0000-000000000000') {
                $('#scheme-id-error').addClass('d-none');

                if (prevSchemeId != schemeId) {
                    // Clear Dependent Data
                    noticeScheduleDataTable.clear().draw();
                    agentDataTable.clear().draw();

                    $('.term-deposit-account-input').val('');
                    $('#person-id').val('');
                    $('#day, #month, #year').val('');
                    $('#maturity-date').val('');

                    // Input Visiblity Base On Selected Scheme
                    SetSchemeSetting();

                    // Set PersonDropdownList Based On Scheme
                    // Call Person Dropdown List
                    if ($('#deposit-type-id').val() == DEMAND_DEPOSIT)
                        SetPersonDropdownListForDemandDeposit();
                    else
                        SetPersonDropdownList();

                    prevSchemeId = schemeId;
                }
                else
                    prevSchemeId = $("#scheme-id option:selected").val();
            }
            else
                $('#scheme-id-error').removeClass('d-none');
        }

    });

    // Person Autocomplete FocusOut Event
    $('#person-id').focusout(function (event) {
        if (!isVerifyView) {
            $(this).val($(this).val().trim());
            SetPersonData();
        }
    });

    // Clear Depended Inputs
    $('#account-opening-date').focusout(function (event) {
        {
            if (!isVerifyView) {
                $('#maturity-date').val('');

                CalculateDepositInterest(parseFloat($('#deposit-amount').val()));
            }
        }
    });

    // Call Interest Calculation Function
    $('#year, #month, #day, #maturity-date, #rate-of-interest').focusout(function () {
        if (!isVerifyView)
            CalculateDepositInterest(parseFloat($('#deposit-amount').val()));

        let monthDiff = MonthDiffernece($('#account-opening-date').val(), $('#maturity-date').val());

        if (!isNaN(monthDiff)) {
            $('#interest-payout-frequency').html('');
            $('#interest-payout-frequency').append(INTEREST_PAYOUT_FREQUENCY_DROPDOWN_LIST);

            if (monthDiff < 3) {
                $('#interest-payout-frequency').find('option[value="' + QUARTERLY + '"]').remove();
                $('#interest-payout-frequency').find('option[value="' + SEMI_ANNUALLY + '"]').remove();
                $('#interest-payout-frequency').find('option[value="' + ANNUALLY + '"]').remove();
            }

            if (monthDiff < 6) {
                $('#interest-payout-frequency').find('option[value="' + SEMI_ANNUALLY + '"]').remove();
                $('#interest-payout-frequency').find('option[value="' + ANNUALLY + '"]').remove();
            }

            if (monthDiff < 12)
                $('#interest-payout-frequency').find('option[value="' + ANNUALLY + '"]').remove();
        }
    });

    // Deposit Installment 
    $('#deposit-installment-amount').focusout(function (event) {
        {
            if (!isVerifyView) {
                let installmentAmount = parseFloat($('#deposit-installment-amount').val());
                let minimumInstallmentAmount = $('#deposit-installment-amount').attr('min');

                if (parseFloat(installmentAmount) < parseFloat(minimumInstallmentAmount)) {
                    $('#deposit-installment-amount').val(minimumInstallmentAmount);
                    installmentAmount = minimumInstallmentAmount;
                }

                if (!isNaN(installmentAmount)) {
                    if (parseFloat(installmentAmount) % parseFloat(installmentAmountMultipleOf) !== 0 || parseFloat(installmentAmount) < parseFloat(minimumInstallmentAmount)) {
                        $('#deposit-installment-amount').val(Math.floor(parseFloat(installmentAmount) / parseFloat(installmentAmountMultipleOf)) * parseFloat(installmentAmountMultipleOf));

                        $('#deposit-installment-amount-error').html('Installment Amount In Multiple Of ' + parseFloat(installmentAmountMultipleOf));
                        $('#deposit-installment-amount-error').removeClass('d-none');
                    }
                    else
                        $('#deposit-installment-amount-error').addClass('d-none');
                }
            }
        }
    });

    // To Get Focus Time Deposit Amount For Splitting Deposit Amount
    $('#deposit-amount').focus(function (event) {
        {
            if (!isVerifyView) {
                oldDepositAmount = parseFloat($(this).val());

                if (isNaN(oldDepositAmount))
                    oldDepositAmount = 0;
            }
        }
    });

    // Deposit Amount
    $('#deposit-amount').focusout(function (event) {
        {
            if (!isVerifyView) {
                let depositAmount = parseFloat($('#deposit-amount').val());
                let minimumDepositAmount = $('#deposit-amount').attr('min');
                let maxNumberOfAccount = 0;

                if (parseFloat(depositAmount) < parseFloat(minimumDepositAmount)) {
                    $('#deposit-amount').val(minimumDepositAmount);
                    depositAmount = minimumDepositAmount;
                }

                if (!isNaN(depositAmount)) {
                    if (parseFloat(depositAmount) % parseFloat(depositMultipleOfThereAfter) !== 0 || parseFloat(depositAmount) < parseFloat(minimumDepositAmount)) {
                        $('#deposit-amount').val(Math.floor(parseFloat(depositAmount) / parseFloat(depositMultipleOfThereAfter)) * parseFloat(depositMultipleOfThereAfter));

                        $('#deposit-amount-error').html('Deposit Amount In Multiple Of ' + parseFloat(depositMultipleOfThereAfter));
                        $('#deposit-amount-error').removeClass('d-none');
                    }
                    else {
                        CalculateDepositInterest(parseFloat($('#deposit-amount').val()));

                        $('#deposit-amount-error').addClass('d-none');

                        // Hide Number Of Account On Amend Operation
                        if ($('#amend-view').length == 0) {
                            maxNumberOfAccount = Math.floor(depositAmount / minimumDepositAmount);

                            // Allow Only 49 Split Deposit Accounts
                            if (maxNumberOfAccount > 49) {
                                maxNumberOfAccount = 49;
                                $('#no-of-accounts-error').removeClass('d-none');
                                $('#no-of-accounts-error').html('Maximum 49 Split Deposits Allowed');
                            }
                            else
                                $('#no-of-accounts-error').addClass('d-none');

                            if (parseInt(maxNumberOfAccount) > 1) {
                                $('#no-of-accounts-input').removeClass('d-none');
                                $('#no-of-accounts').attr('max', maxNumberOfAccount);
                            }
                            else
                                $('#no-of-accounts-input').addClass('d-none');
                        }
                    }

                    if (oldDepositAmount != depositAmount) {
                        $('#no-of-accounts').val('');
                        // The empty() method removes all child nodes and content 
                        // from the selected elements.
                        $('.no-of-inputs').empty();
                        $('.schedule-time').empty();
                    }
                }
            }
        }
    });

    // No Of Account Validity - For Fixed Deposit  If Multiple Account Create Same Time
    $('#no-of-accounts').focusout(function () {
        debugger;
        let numberOfWrapInput = parseInt($(this).val()); //Input boxes wrapper ID
        let count = 0;
        let wrapInputHtml = '';
        let depositAmount = $('#deposit-amount').val();
        let isValidSplitAmount = true;
        let splitAmount = Math.round(parseFloat(depositAmount) / parseInt(numberOfWrapInput));
        let maximumOfNumberOfAccount = $('#no-of-accounts').attr('max');

        if (parseInt(numberOfWrapInput) <= parseInt(maximumOfNumberOfAccount)) {
            $('#no-of-accounts-error').addClass('d-none');

            // Allow Deposit Amount To Split, If Deposit Amount Is Larger Than Two Times Of Minimum Deposit Amount
            if (parseFloat(splitAmount) % parseFloat(depositMultipleOfThereAfter) != 0)
                splitAmount = Math.floor(parseFloat(splitAmount) / parseFloat(depositMultipleOfThereAfter)) * parseFloat(depositMultipleOfThereAfter);

            for (let i = 0; i < parseInt(numberOfWrapInput) ; i++) {
                // The empty() method removes all child nodes and content 
                // from the selected elements.
                $('.no-of-inputs').empty();
                $('.schedule-time').empty();

                //Increment field counter
                count += 1;

                wrapInputHtml += '<div class="form-group"><label class="font-weight-bold">Deposit Amount ' + count + '</label><input id="deposit-multi' + count + '" class="form-control mandatory-mark schedule-time multi-deposit" name="CustomerTermDepositAccountDetailViewModel.NoOfDepositsAmount[]" type="number" seq = ' + count + ' value= ' + splitAmount + ' /><span id="deposit-multi' + count + '-error" class="error d-none"> Deposit Amount In Multiple Of  ' + depositMultipleOfThereAfter + '  </span></div>';
            }

            $('.no-of-inputs').append(wrapInputHtml);

            if ((parseFloat(splitAmount) * parseInt(numberOfWrapInput)) != parseFloat(depositAmount)) {
                let tmpAmount = parseFloat(depositAmount) - ((parseFloat(splitAmount) * parseInt(numberOfWrapInput)) - parseFloat(splitAmount))
                $('#deposit-multi' + numberOfWrapInput).val(tmpAmount);
            }
        }
        else {
            $('#no-of-accounts').val(maximumOfNumberOfAccount);
            $('#no-of-accounts-error').html('You Cannot Split Deposit Amount More Than ' + maximumOfNumberOfAccount)
            $('#no-of-accounts-error').removeClass('d-none');
        }

    }).keyup(function (event) {
        if (!isVerifyView) {
            // The empty() method removes all child nodes and content 
            // from the selected elements.
            $('.no-of-inputs').empty();
            $('.schedule-time').empty();
        }
    });

    //Dynamic event bind For Multi Deposit Amount  
    $(document).on('focusout', '.multi-deposit', function () {
        debugger;
        let termDepositTotal = 0;
        let depositAmount = parseFloat($('#deposit-amount').val());
        let seqNo = $(this).attr('seq');
        let myVal = $(this).val();
        let splitBalanceAmount = 0;
        let numberOfSplitAccount = $('#no-of-accounts').val();
        let minimumDepositAmount = $('#deposit-amount').attr('min');

        let splitAmount = Math.round(parseFloat(depositAmount) / parseInt(numberOfSplitAccount));

        if (parseFloat(myVal) % parseFloat(depositMultipleOfThereAfter) == 0) {
            $('#deposit-multi' + seqNo + '-error').addClass('d-none');

            if (parseFloat(myVal) >= parseFloat(depositAmount) || parseFloat(myVal) < parseFloat(minimumDepositAmount) || parseFloat(myVal) > parseFloat(splitAmount)) {
                $(this).val(splitAmount);
                myVal = splitAmount;
            }

            if (!isNaN(depositAmount) && parseFloat(depositAmount) > 0) {
                $('#deposit-split-amount-error').addClass('d-none');

                for (let i = 1; i <= numberOfSplitAccount; i++) {
                    // Make Total Upto Current Inputed Text Input
                    if (parseInt(i) <= parseInt(seqNo)) {
                        termDepositTotal = parseFloat(termDepositTotal) + parseFloat($('#deposit-multi' + i).val());

                        if (parseFloat(termDepositTotal) > parseFloat(depositAmount))
                            $('#deposit-multi' + i).val(parseFloat(depositAmount) - (parseFloat(termDepositTotal) - parseFloat(myVal)));

                        // If Last Split Account Input And Total Is Less Than Deposit Amount
                        if (parseInt(i) == parseInt(numberOfSplitAccount)) {
                            if (parseFloat(termDepositTotal) < parseFloat(depositAmount)) {
                                $('#deposit-multi' + i).val(parseFloat(depositAmount) - (parseFloat(termDepositTotal) - parseFloat(myVal)));
                                termDepositTotal = depositAmount;
                            }
                        }
                    }
                    else {
                        if (parseFloat(depositAmount) <= parseFloat(termDepositTotal))
                            splitBalanceAmount = 0;

                        // Split Deposit Amount In Remaining 
                        if (parseFloat(depositAmount) > parseFloat(termDepositTotal) && splitBalanceAmount == 0) {
                            splitBalanceAmount = Math.round(parseFloat(depositAmount) - parseFloat(termDepositTotal));
                            splitBalanceAmount = Math.round(parseFloat(splitBalanceAmount) / (parseInt(numberOfSplitAccount) - parseInt(seqNo)));

                            if (parseFloat(splitBalanceAmount) < parseFloat(minimumDepositAmount)) {
                                splitBalanceAmount = parseFloat(depositAmount) - parseFloat(termDepositTotal)
                                termDepositTotal = parseFloat(depositAmount);
                            }
                            else
                                splitBalanceAmount = Math.floor(parseFloat(splitBalanceAmount) / parseFloat(minimumDepositAmount)) * parseFloat(minimumDepositAmount);
                        }

                        $('#deposit-multi' + i).val(splitBalanceAmount < 0 ? 0 : splitBalanceAmount);
                    }
                }

                let totalSplitDeposit = termDepositTotal + (parseFloat(splitBalanceAmount) * (parseInt(numberOfSplitAccount) - parseInt(seqNo)));
                let lastVal = $('#deposit-multi' + numberOfSplitAccount).val();

                if (parseFloat(totalSplitDeposit) != parseFloat(depositAmount)) {
                    let remainingAmount = parseFloat(depositAmount) - (parseFloat(totalSplitDeposit) - parseFloat(lastVal));
                    $('#deposit-multi' + numberOfSplitAccount).val(parseFloat(remainingAmount) < 0 ? 0 : parseFloat(remainingAmount));
                }
            }
            else
                $('#deposit-split-amount-error').removeClass('d-none');
        }
        else {
            $('#deposit-multi' + seqNo).val(splitAmount);
            $('#deposit-multi' + seqNo + '-error').removeClass('d-none');
        }

        CalculateDepositInterest(parseFloat($(this).val()));
    });

    //Dynamic event bind on read-only class  
    $(document).on('keydown', '.read-only', function (e) {
        // Skip Only Tab (9 - Keycode)
        if (e.which != 9)
            e.preventDefault();
    });

    // Maturity Instruction
    $('#maturity-instruction').focusout(function (event) {
        if (!isVerifyView) {
            MaturityInstructionFocusoutHandlerFunction();
        }
    });

    // Custom Renew Deposit Amount
    $('#custom-renew-amount').focusout(function (event) {
        if (!isVerifyView) {
            debugger;
            let depositAmount = parseFloat($('#custom-renew-amount').val());
            let minimumDepositAmount = $('#custom-renew-amount').attr('min');
            let maxNumberOfAccount = 0;

            if (!isNaN(depositAmount)) {
                if (parseFloat(depositAmount) % parseFloat(depositMultipleOfThereAfter) !== 0 || parseFloat(depositAmount) < parseFloat(minimumDepositAmount)) {
                    $('#custom-renew-amount').val(Math.floor(parseFloat(depositAmount) / parseFloat(depositMultipleOfThereAfter)) * parseFloat(depositMultipleOfThereAfter));

                    $('#custom-renew-amount-error').html('Custom Deposit Renew Amount In Multiple Of ' + parseFloat(depositMultipleOfThereAfter));
                    $('#custom-renew-amount-error').removeClass('d-none');
                }
            }
        }
    });

    // Interest Payout Frequency  
    $('#interest-payout-frequency').focusout(function (event) {
        if (!isVerifyView) {
            CalculateDepositInterest(parseFloat($('#deposit-amount').val()));
        }
    });

    // Hide Guardian Details If User Select Adult Person Name As Nominee (** This List Contains Only Adult Person Name)
    $('#nominee-guardian-person-information-number').focusout(function (event) {
        if (!isVerifyView) {
            let personInfoNumber = $('#nominee-guardian-person-information-number').val();

            if (personInfoNumber == '')
                $('.nominee-guardian-details').removeClass('d-none');
            else
                $('.nominee-guardian-details').addClass('d-none');
        }
    });

    // Hide Guardian Details If User Input Nominee Birtdate As Adults
    $('#nominee-birth-date').focusout(function (event) {
        if (!isVerifyView) {
            $.get('/PersonChildAction/GetAgeFromBirthDate', { _birthDate: $('#nominee-birth-date').val(), async: false }, function (data) {

                if (data < 18)
                    $('#guardian-card').removeClass('d-none');
                else
                    $('#guardian-card').addClass('d-none');
                return false;
            });
        }
    });

    // Hide Nominee Person Information Number, If User Manually Input Nominee Name 
    $('#name-of-nominee').focusout(function (event) {
        if (!isVerifyView) {
            let nameOfNominee = $('#name-of-nominee').val();

            if ((nameOfNominee != 'None') && (nameOfNominee.length > 3)) {
                $('#person-nominee-id').prop('selectedIndex', 0);
                $('#nominee-person-information-number-input').addClass('d-none');
            }
            else {
                $('#nominee-person-information-number-input').removeClass('d-none');
            }
        }
    });

    // Hide Nominee Manually Inputs, If User Selects Adult Person
    $('#nominee-person-id').focusout(function (event) {
        if (!isVerifyView) {
            debugger;
            if ($('#nominee-person-id').val() == 0 || typeof $('#nominee-person-id').val() == null)
                $('#nomineedetails').removeClass('d-none');
            else {
                $('#nomineedetails').addClass('d-none');

                //let personInformationNumbers = $(this).val();
                $.get('/PersonChildAction/GetPersonCurrentAge', { _personInformationNumber: nomineePersonInformationNumber, async: false }, function (data, textStatus, jqXHR) {
                    if (data <= 18)
                        $('#guardian-card').removeClass('d-none');
                    else
                        $('#guardian-card').addClass('d-none');
                });
            }
        }
    })

    // Hide Proportionate Amount If Holding Percentage Is Greater Than 0
    $('#holding-percentage').focusout(function (event) {
        if (!isVerifyView) {
            let holdingPercentage = parseFloat($(this).val());

            if (!isNaN(holdingPercentage) || parseFloat(holdingPercentage) > 0)
                $('#proportionate-amount-for-each-nominee-input').addClass('d-none');
            else
                $('#proportionate-amount-for-each-nominee-input').removeClass('d-none');
        }
    })

    // Hide Holding Percentage If Proportionate Amount Is Greater Than 0
    $('#proportionate-amount-for-each-nominee').focusout(function (event) {
        if (!isVerifyView) {
            let proportionateAmount = parseFloat($(this).val());

            if (!isNaN(proportionateAmount) || parseFloat(proportionateAmount) > 0)
                $('#holding-percentage-input').addClass('d-none');
            else
                $('#holding-percentage-input').removeClass('d-none');
        }
    })

    $('#guardian-full-name').focusout(function (event) {
        if (!isVerifyView) {
            let nameOfGuardian = $('#guardian-full-name').val();

            if ((nameOfGuardian != 'None') && (nameOfGuardian.length > 3)) {
                $('#nominee-guardian-person-information-number').prop('selectedIndex', 0);
                $('#nominee-guardian-person-information-number-input').addClass('d-none');
            }
            else
                $('#nominee-guardian-person-information-number-input').removeClass('d-none');
        }
    });

    // Verification Code Visibility & Input Type Visibility Based On Contact Type
    $('#contact-type').focusout(function (event) {
        if (!isVerifyView) {
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
        }
    });

    // Contact Type Validation
    $('#field-value').focusout(function (event) {
        if (!isVerifyView) {
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
                        .filter(function (value, index) {
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
                    $('#field-value-error').addClass('d-none');
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
        }
    });

    // Interest Credited Account Hide/Show
    $('#renew-type-id').focusout(function (event) {
        if (!isVerifyView) {
            RenewTypeFocusoutHandlerFunction();
        }
    });


    // Validate Unique Record Based On Account Number
    $('#customer-beneficiary-account-number').focusout(function (event) {
        if (!isVerifyView) {
            let myVal = $(this).val();

            // Check Whether Entered Account Number Is Existed Or Not
            let filteredData = beneficiaryDetailDataTable
                .rows()
                .indexes()
                .filter(function (value, index) {
                    return beneficiaryDetailDataTable.row(value).data()[6] == myVal;
                });

            if (beneficiaryDetailDataTable.rows(filteredData).count() > 0) {
                isDuplicateBeneficiaryAccountNumber = true;
                $('#customer-beneficiary-account-number-error').removeClass('d-none');
            }
            else {
                isDuplicateBeneficiaryAccountNumber = false;
                $('#customer-beneficiary-account-number-error').addClass('d-none');
            }
        }
    });

    // ################# (FOCUSOUT) FOR NORMAL ACCORDION VALIDITY 

    // SMS Service Detail Input Validation
    $('.sms-service-input').focusout(function () {
        if (!isVerifyView)
            IsValidSMSServiceDetailAccordionInputs();
    });

    // Email Service Detail Input Validation
    $('.email-service-input').focusout(function () {
        if (isVerifyView === false) {
            IsValidEmailServiceDetailAccordionInputs();
        }
    });

    $('.email-service-radio-input').change(function () {
        IsValidEmailServiceDetailAccordionInputs();
    });

    // Cheque Detail Input Validation
    $('.cheque-detail-input').focusout(function () {
        if (!isVerifyView) {
            if (IsValidChequeDetailAccordionInputs())
                $('#cheque-detail-accordion-error').addClass('d-none');
        }
    });

    // Sweep Detail Input Validation
    $('.sweep-detail-input').focusout(function () {
        if (!isVerifyView) {
            if (IsValidSweepDetailAccordionInputs())
                $('#sweep-detail-accordion-error').addClass('d-none');
        }
    });

    // Term Deposit Account Input Validation
    $('.term-deposit-account-input').focusout(function () {
        if (!isVerifyView) {
            IsValidTermDepositAccordionInputs()
        }
    });

    // Standing Instruction Input Validation
    $('.standing-instruction-input').focusout(function () {
        if (!isVerifyView)
            IsValidStandingInstructionAccordionInputs();
    });

    //  #################   (FUNCTIONS)  User Defined Functions  #################

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
        if (isVerifyView === false) {
            SetTenure();
        }
    });

    //Focusout in  SetMaturityDate
    $('.tenure').focusout(function () {
        if (isVerifyView === false)
            SetMaturityDate();
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

    function CalculateDepositInterest(_depositAmount) {
        let payoutFrequency = $('#interest-payout-frequency').val();

        let acOpeningDate = new Date($('#account-opening-date').val());
        let maturityDate = new Date($('#maturity-date').val());
        let diffTime = Math.abs(maturityDate - acOpeningDate);
        let diffDays = Math.floor(diffTime / (1000 * 60 * 60 * 24));
        let diffMonths = 0;
        let interestRate = parseFloat($('#rate-of-interest').val());
        let depositAmount = _depositAmount < 1 ? parseFloat($('#deposit-amount').val()) : _depositAmount;
        let interestAmount = 0;
        let minValue = 0;
        let maxValue = 0;

        if (!isNaN(depositAmount) && !isNaN(interestRate) && !isNaN(diffDays)) {
            interestAmount = Math.round((depositAmount * interestRate * diffDays) / 36500);

            if (parseFloat(minimumOverrideInterestAmount) < 1)
                minValue = parseFloat(interestAmount) - parseFloat(minimumOverrideInterestAmount)
            else
                minValue = interestAmount;

            if (parseFloat(maximumOverrideInterestAmount) < 1)
                maxValue = parseFloat(interestAmount) - parseFloat(maximumOverrideInterestAmount)
            else
                maxValue = parseFloat(interestAmount) + parseFloat(maximumOverrideInterestAmount);

            $('#total-interest-amount').attr('min', minValue);
            $('#total-interest-amount').attr('max', maxValue);

            // Total Interest And Maturity Amount
            $('#total-interest-amount').val(interestAmount);
            $('#maturity-amount').val(parseFloat(depositAmount) + parseFloat(interestAmount));

            // If Interest Less Than 1200 Then Hide Periodic Interest Payout
            if (isEnablePayoutInterest) {
                $('#interest-payout').removeClass('d-none');

                // if Interest Payout Frequency Enabled
                if (payoutFrequency != '') {
                    diffMonths = (maturityDate.getFullYear() - acOpeningDate.getFullYear()) * 12;
                    diffMonths -= acOpeningDate.getMonth();
                    diffMonths += maturityDate.getMonth();

                    diffMonths = diffMonths <= 0 ? 0 : diffMonths;

                    // At Maturity
                    if (payoutFrequency == AT_MATURITY) {
                        $('#interest-payout-amount').val(0);
                        $('#interest-payout-day').val(0);
                        $('#interest-payout-amount-input').addClass('d-none');
                        $('#interest-payout-day-input').addClass('d-none');
                    }
                    else {
                        $('#interest-payout-amount-input').removeClass('d-none');
                        $('#interest-payout-day-input').removeClass('d-none');
                    }

                    // Monthly
                    if (payoutFrequency == MONTHLY)
                        $('#interest-payout-amount').val(interestAmount / diffMonths);

                    // Quarterly
                    if (payoutFrequency == QUARTERLY)
                        $('#interest-payout-amount').val(interestAmount / (diffMonths / 4));

                    // Semi Anually
                    if (payoutFrequency == SEMI_ANNUALLY)
                        $('#interest-payout-amount').val(interestAmount / (diffMonths / 6));

                    // Anually
                    if (payoutFrequency == ANNUALLY)
                        $('#interest-payout-amount').val(interestAmount / (diffMonths / 12));

                    $('#interest-payout-amount').val(Math.round($('#interest-payout-amount').val()));
                }
            }
            else {
                $('#interest-payout').addClass('d-none');

                $('#interest-payout-frequency').prop('selectedIndex', 0);
                $('#interest-payout-amount').val(0);
                $('#interest-payout-day').val(0);
            }
        }
    }

    function SetGeneralLedgerDropdownList() {
        debugger;
        let depositTypeId = $('#deposit-type-id option:selected').val();
        let businessOfficeId = $('#business-office-id option:selected').val();
        // Get Value For Only For Amend Operation
        let generalLedgerPageLoadId = $('#general-ledger-id option:selected').val();

        $.get('/DynamicDropdownList/GetAuthorizedDepositGeneralLedgerDropdownList', { _businessOfficeId: businessOfficeId, _depositType: depositTypeId, async: false }, function (data) {
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

            prevDepositTypeId = $('#deposit-type-id option:selected').val();

            SetSchemeDropdownList();
        });
    }

    function SetSchemeDropdownList() {
        let selectedGeneralLedgerId = $('#general-ledger-id option:selected').val();

        // Get Value For Only For Amend Operation
        let schemePageLoadId = $('#scheme-id option:selected').val();

        $.get('/DynamicDropdownList/GetSchemeDropdownListByGeneralLedger', { _generalLedgerId: selectedGeneralLedgerId, async: false }, function (data, textStatus, jqXHR) {
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

            SetPersonDropdownList();
        });
    }

    function SetPersonDropdownList() {
        schemeId = $('#scheme-id option:selected').val();

        // Set PersonDropdownList Based On Scheme
        $.get('/DynamicDropdownList/GetPersonDropdownListBySchemeId', { _schemeId: schemeId, async: false }, function (data) {
            personDropdownListData = data;

            if (isAmendView)
                SetStandingInstructionDropdownList();
        });
    }

    function SetPersonDropdownListForDemandDeposit() {
        schemeId = $('#scheme-id option:selected').val();

        // Get Main Customer Person Dropdown
        // Get Person Dropdown Whose Demand Deposit Account Not Opened
        $.get('/DynamicDropdownList/GetPersonDropdownListForDemandDepositAccountOpening', { _schemeId: schemeId, async: false }, function (data, textStatus, jqXHR) {
            personDropdownListData = data;
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
            contactDataTable.clear().draw();
            jointAccountDataTable.clear().draw();
            nomineeDataTable.clear().draw();
            referencePersonDetailDataTable.clear().draw();

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

                    //let addressdataTable =$('#person-address-data-table').DataTable();
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

            // For Only Demand Deposit
            if ($('#deposit-type-id').val() == DEMAND_DEPOSIT) {
                $.get("/PersonChildAction/GetPhotoSignByPersonId", { _personId: personId, async: false }, function (data) {
                    let photoImage = ArrayBufferToBase64(data.PhotoCopy);
                    $('#image-preview-photo').attr('src', "data:image/jpg;base64," + photoImage);
                    $('#photo-file-caption').val(data.PhotoFileCaption);

                    let personSignImage = ArrayBufferToBase64(data.PersonSign);
                    $('#image-preview-one').attr('src', "data:image/jpg;base64," + personSignImage);
                    $('#sign-file-caption').val(data.SignFileCaption);
                    $('#note-photo-sign').val(data.Note);
                });
            }
                // For Standing Instruction Except Demand Deposit
            else
                SetStandingInstructionDropdownList();

            prevPersonId = selectedPersonId;
        }
        else {
            $('#person-change-info').addClass('d-none');
            prevPersonId = selectedPersonId;
        }
    }

    // Standing Instruction Dropdown i.e. Demand Deposit
    function SetStandingInstructionDropdownList() {
        // On Page Loading Amend View Assign Person Id Value
        if (isAmendView || isVerifyView)
            selectedPersonId = $('#person-id1').val();

        $.get("/DynamicDropdownList/GetDemandDepositAccountHolderDropdownListByPerson", { _personId: selectedPersonId, async: false }, function (data) {
            $('#customer-saving-account-debit').html('');
            $('#customer-saving-account-credit').html('');
            $('#customer-saving-account-interest').html('');

            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000">--- Select Saving Account --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#customer-saving-account-debit').append(dropdownListItems);
            $('#customer-saving-account-credit').append(dropdownListItems);
            $('#customer-saving-account-interest').append(dropdownListItems);

            listItemCount = $('#customer-saving-account-debit > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1) {
                $('#customer-saving-account-debit').prop('selectedIndex', 1);
                $('#customer-saving-account-credit').prop('selectedIndex', 1);
                $('#customer-saving-account-interest').prop('selectedIndex', 1);
            }
            else {
                if (isVerifyView || isAmendView) {
                    $('#customer-saving-account-debit').val($('#debit-account-id').text());
                    $('#customer-saving-account-credit').val($('#credit-account-id').text());
                    $('#customer-saving-account-interest').val($('#interest-account-id').text());
                }
            }

            // Required To Event Execution
            isAmendView = false;
        });
    }

    //To convert ByteArray to Base64
    function ArrayBufferToBase64(buffer) {
        let binary = '';
        let bytes = new Uint8Array(buffer);
        let len = bytes.byteLength;

        for (let i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }

        return window.btoa(binary);
    }

    // Validatae Email Id
    function IsValidEmailId(email) {
        if (email.length < 5)
            return false;

        let atIndex = email.indexOf("@");
        let dotIndex = email.lastIndexOf(".");

        if (atIndex < 1 || dotIndex < atIndex + 2 || dotIndex + 2 >= email.length)
            return false;

        let domain = email.substring(atIndex + 1);
        let dotCount = domain.split('.').length - 1;

        if (dotCount < 1)
            return false;

        let parts = domain.split(".");

        for (let part of parts) {
            if (part.length === 0)
                return false;
        }

        return true;
    }

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
            selectedPersonText = ui.item.label;
        }
        }).focus(function (event, ui) {
            $('#person-id').val('');
        selectedPersonId = '';
        selectedPersonText = '';

        // Assign Array Without Reference  *** Use Slice Method
        finalDropdownListArray = personDropdownListData.slice();

        $(this).autocomplete('search');
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
    }).focus(function (event, ui) {
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
                for (let jointAccountPersonId of dataTablePersonIdArray)
                {
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
        }).focus(function (event, ui) {
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
    }).focus(function (event, ui) {
        // Clear Selected Guardian Nominee Person
        $('#nominee-guardian-person-information-number').val('');
        guardianNomineePersonInformationNumber = '';
        guardianNomineePersonInformationNumberText = '';

        // Assign Array Without Reference  *** Use Slice Method
        finalDropdownListArray = personDropdownListDataForGuardian.slice();

        $(this).autocomplete('search');
    });

    // Reference Person
    $('#reference-customer-account-number').autocomplete(
    {
        minLength: 0,
        appendTo: '#customer-account-number-div',
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
            $('#reference-customer-account-number').val(ui.item.label);
            selectedReferencePersonId = ui.item.valueId;
            selectedReferencePersonText = ui.item.label;
        },
    }).focus(function (event, ui) {
        // Clear Data Table Added Person Id Data
        // Clear Selected Person Id
        $('#reference-customer-account-number').val('');
        selectedReferencePersonId = '';
        selectedReferencePersonText = '';
        let dataTablePersonIdArray = [];

        // Assign Array Without Reference  *** Use Slice Method
        finalDropdownListArray = personDropdownListDataForReference.slice();
        dropDownListItemCount = finalDropdownListArray.length;

        // Get Added Person Id For Remove From List
        $('#tbl-reference-person-detail > tbody > tr').each(function () {
            let currentRow = $(this).closest("tr");
            let columnValues = (referencePersonDetailDataTable.row(currentRow).data());

            if (typeof columnValues !== 'undefined' && columnValues != null)
                dataTablePersonIdArray.push({ 'Value': columnValues[1], 'Text': columnValues[2] })
        });

        if (parseInt(dropDownListItemCount) > 0 && parseInt(dataTablePersonIdArray.length) > 0) {
            while (dropDownListItemCount--) {
                // Remove Added Joint Account Person Id From List
                for (let referencePersonId of dataTablePersonIdArray)
                {
                    if (finalDropdownListArray[dropDownListItemCount].Value === referencePersonId.Value)
                        // splice - remove item from array at a choosen index
                        finalDropdownListArray.splice(dropDownListItemCount, 1);
                }
            }
        }

        $(this).autocomplete('search');
    });

    //Event handler for keyup event on the IFSC code input field
    $('#customer-beneficiary-ifsc-code').keyup(function () {
        let inputValues = $(this).val();
        let reg = /[A-Za-z]{4}\d{7}$/;

        if (inputValues.match(reg)) {
            $.getJSON('https://ifsc.razorpay.com/' + inputValues, function (data) {
                $('#customer-beneficiary-bank-name').val(data.BANK);

                $('#customer-beneficiary-branch').val(data.BRANCH);
                $('#customer-beneficiary-city').val(data.CITY);
                $('#customer-beneficiary-ifsc-code--error').addClass('d-none');
            }).fail(function (jqXHR, textStatus, errorThrown) {
                if (jqXHR.status == 404) {
                    $('#customer-beneficiary-ifsc-code-error').removeClass('d-none');
                    $('.ifsc-detail').val('');
                }
            });
        } else {
            $('#customer-beneficiary-ifsc-code-error').removeClass('d-none');
            $('.ifsc-detail').val('');
        }
    });

    //  Validate Confirm Account Number'
    $('#confirm-account-number').keyup(function (event) {
        let txtAccountNumber = $('#customer-beneficiary-account-number').val();

        let txtConfirmAccountNumber = $(this).val();

        // Check if both input fields are not empty and if their values match
        let isValid = txtAccountNumber != '' && txtConfirmAccountNumber != '' && txtAccountNumber == txtConfirmAccountNumber;

        // If the inputs are not valid, show the error message
        if (!isValid)
            $('#confirm-account-number-error').removeClass('d-none');
        else
            $('#confirm-account-number-error').addClass('d-none');

        return isValid;
    });

    // Email Address
    $('#customer-beneficiary-email-id').focusout(function () {
        if (!isVerifyView) {
            let emailId = $('#customer-beneficiary-email-id').val().trim(); // Trim whitespace from the beginning and end of the email

            $('#customer-beneficiary-email-id').val(emailId);

            if (!IsValidEmailId(emailId))
                $('#customer-beneficiary-email-id-error').removeClass('d-none');
            else
                $('#customer-beneficiary-email-id-error').addClass('d-none');
        }
    });

    //Clear Ifsc text box
    $('#ifsc-code').keyup(function () {
        $('#container').next('div.error').remove();
        $('#container').empty();
    });

    //keypress events on the input field with id 'customer-beneficiary-mobile-number'
    $('#customer-beneficiary-mobile-number').keyup(function (e) {
        let value = $(this).val();

        // Remove all whitespace characters
        value = value.replace(/\s+/g, '');
        /^(\+91)?[6-9]\d{9}$/
        // Allow only numbers and limit length to 10
        if (isNaN(e.key) || value.length >= 10) {
            e.preventDefault();
            $('#customer-beneficiary-mobile-number-error').removeClass('d-none').text("Please Enter Valid Mobile Number.");
        } else {
            $('#customer-beneficiary-mobile-number-error').addClass('d-none');
        }
    });

    //focusout events on the input field with id 'customer-beneficiary-mobile-number'
    $('#customer-beneficiary-mobile-number').focusout(function () {
        if (!isVerifyView) {
            let value = $(this).val();

            // Remove all whitespace characters
            value = value.replace(/\s+/g, '');

            // Display error if the input length is not exactly 10
            if (value.length === 10)
                $('#customer-beneficiary-mobile-number-error').addClass('d-none');
            else
                $('#customer-beneficiary-mobile-number-error').removeClass('d-none');
        }
    });

    //@@@@@@@@@@@@@@@@@@@@@@@@@@@  U S E R  D E F I N E D  F U N C T I O N S  @@@@@@@@@@@@@@@@@@@@@@@@@@@   

    // Set All Required Default Values On Page Loading
    function SetPageLoadingDefaultValues()
    {
        debugger;
        // Disalbe Events On Verify View
        if ($('#verify-view').length > 0)
            isVerifyView = true;

        if ($('#amend-view').length > 0)
            isAmendView = true;

        //$('#deposit-type-id option:selected').text()
        if ($('#deposit-type-id').val() === DEMAND_DEPOSIT) {
            $('.term-deposit').addClass('d-none');
            $('.recurring-deposit').addClass('d-none');
            $('.demand-deposit').removeClass('d-none');
        }

        if ($('#deposit-type-id').val() === RECURRING_DEPOSIT) {
            debugger;
            $('.demand-deposit').addClass('d-none');
            $('.term-deposit').addClass('d-none');
            $('.recurring-deposit').removeClass('d-none');
            $('.debit-account').addClass('d-none');
            $('.deposit-credit-account').removeClass('d-none');
            $('.interest-credit-account').addClass('d-none');
        }

        if ($('#deposit-type-id').val() === TERM_DEPOSIT) {
            $('.recurring-deposit').addClass('d-none');
            $('.demand-deposit').addClass('d-none');
            $('.term-deposit').removeClass('d-none');
            $('.debit-account').addClass('d-none');
            $('.deposit-credit-account').addClass('d-none');
            $('.interest-credit-account').addClass('d-none');
        }

        schemeId = $('#scheme-id').val();

        let payoutFrequency = $('#interest-payout-frequency').val();

        if (payoutFrequency == AT_MATURITY)
            $('#interest-payout-amount-input').addClass('d-none');

        // Input Visiblity Base On Selected Scheme
        SetSchemeSetting();

        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();

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

        // Call Person Dropdown List
        if ($('#deposit-type-id').val() == DEMAND_DEPOSIT)
            SetPersonDropdownListForDemandDeposit();
        else
            SetPersonDropdownList();

        // Get Person Dropdown For Joint Account
        $.get('/DynamicDropdownList/GetPersonDropdownList', function (data) {
            personDropdownListDataForJointAccount = data;
        });

        // Get Person Dropdown For Nominee
        $.get('/DynamicDropdownList/GetPersonDropdownListForNominee', function (data) {
            personDropdownListDataForNominee = data;
        });

        // Get Person Dropdown For Guardian
        $.get('/DynamicDropdownList/GetPersonDropdownListForGuardian', function (data) {
            personDropdownListDataForGuardian = data;
        });

        // Get Person Dropdown For Reference Person
        $.get('/DynamicDropdownList/GetDemandDepositAccountHolderDropdownList', function (data) {
            personDropdownListDataForReference = data;
        });

        // Check Whether Element Exist OR Not ***** Applicable For Only Amend
        if ($('#person-id-value').length) {
            let personIdValueOnAmend = $('#person-id-value').attr('class').toString().replace('d-none', '');

            selectedPersonId = $('#person-id1').val();

            $('#person-id').val(personIdValueOnAmend);
        }

        // CALL NECESSARY EVENT HANDLER FUNCTION ON VERIFY PAG
        if (isVerifyView || isAmendView) {
            RenewTypeFocusoutHandlerFunction();
            MaturityInstructionFocusoutHandlerFunction();
            EnableAutoCloseOnMaturityClickHandlerFunction();
            EnableAutoRenewOnMaturityClickHandlerFunction();

            // Call Dropdown Functions Only On Amend View
            if (isAmendView)
                SetGeneralLedgerDropdownList();
        }
    }

    //Set Deposite Type Setting
    function SetDepositeTypeSetting() {
        // Mark Out Select All Check Box Of All Datatables.
        $('input[name="select-all"]').prop('checked', false);

        // Clear Accordion Title Error Messages
        $('.accordion-title-error').addClass('d-none');

        // Make False All Toggle Switch
        $('.switch-input').prop('checked', false);

        // Clear Radio Button
        $('.statement-frequency').prop('checked', false);
        $('.cheque-status').prop('checked', false);

        $('.cheque-detail-input').val('');
        $('.sweep-detail-input').val('');
        $('.term-deposit-account-input').val('');

        if ($('#deposit-type-id').val() == DEMAND_DEPOSIT) {
            $('.term-deposit').addClass('d-none');
            $('.recurring-deposit').addClass('d-none');
            $('.demand-deposit').removeClass('d-none');
        }

        if ($('#deposit-type-id').val() == RECURRING_DEPOSIT) {
            debugger;
            $('.demand-deposit').addClass('d-none');
            $('.term-deposit').addClass('d-none');
            $('.recurring-deposit').removeClass('d-none');
            $('.auto-debit-block').addClass('d-none');
            $('.deposit-credited-account-input').removeClass('d-none');
            $('.interest-credited-account-input').addClass('d-none');
        }

        if ($('#deposit-type-id').val() == TERM_DEPOSIT) {
            $('.recurring-deposit').addClass('d-none');
            $('.demand-deposit').addClass('d-none');
            $('.term-deposit').removeClass('d-none');
            $('.auto-debit-block').addClass('d-none');
            $('.deposit-credited-account-input').addClass('d-none');
            $('.interest-credited-account-input').addClass('d-none');
        }

        // Hide Standing Instruction Accordion
        $('#customer-account-standing-instruction-card').addClass('d-none');
    }

    //Set Scheme Setting
    function SetSchemeSetting() {
        debugger;
        schemeId = $('#scheme-id option:selected').val();

        // Input Visiblity Base On Selected Scheme
        $.get('/AccountChildAction/GetDepositSchemeDetailBySchemeId', { _schemeId: schemeId, async: false }, function (data) {
            if (data) {
                // Clear Tenure
                $('#year, #month, #day').val('');

                // Tenure Stop On Verify View
                if (!isVerifyView) {
                    if (data.SchemeAccountParameterViewModel.EnableTenure) {
                        let minimumTenure = parseInt(data.SchemeTenureViewModel.MinimumTenure);
                        let maximumTenure = parseInt(data.SchemeTenureViewModel.MaximumTenure);
                        let timePeriodUnitPrmKey = parseInt(data.SchemeTenureViewModel.TimePeriodUnitPrmKey);

                        // Add Sys Name Of Time Period Unit
                        $.get('/AccountChildAction/GetTimePeriodUnitSysNameByPrmKey', { _timePeriodUnitPrmKey: timePeriodUnitPrmKey, async: false }, function (data1) {
                            // Time Period Unit - Day
                            if (data1 == 'Day') {
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
                            if (data1 == 'Month') {
                                $('#day').attr('min', 0);
                                $('#day').attr('max', 31);
                                $('#day').attr('type', 'number');
                                $('#day').removeClass('read-only');

                                $('#month').attr('min', minimumTenure);
                                $('#month').attr('max', maximumTenure);
                                $('#month').attr('type', 'number');
                                $('#month').removeClass('read-only');

                                maximumTenureIdDays = maximumTenure * 30;

                                $('#year').val(0);
                                $('#year').removeAttr('type');
                                $('#year').addClass('read-only');
                            }

                            // Time Period Unit - Year
                            if (data1 == 'Year') {
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

                                maximumTenureIdDays = maximumTenure * 365;
                            }
                        });
                    }
                }

                SetTenure();

                // Auto Application Number
                if (data.SchemeAccountParameterViewModel.EnableApplication)
                {
                    if (data.SchemeApplicationParameterViewModel.EnableAutoApplicationNumber) {
                        $('#application-number-input').addClass('d-none');
                    }
                    else
                    {
                        $('#application-number-input').removeClass('d-none');
                    }
                }
                else
                    {
                        $('#application-number-input').addClass('d-none');
                    }

                // Auto Account Number
                if (data.SchemeCustomerAccountNumberViewModel.EnableAutoAccountNumber)
                    $('#acc-number-grp').addClass('d-none');
                else
                    $('#acc-number-grp').removeClass('d-none');

                // AccountNumber2
                if (data.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2)
                    $('#account-number2').removeClass('d-none');
                else
                    $('#account-number2').addClass('d-none');

                // AccountNumber3
                if (data.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3)
                    $('#account-number3').removeClass('d-none');
                else
                    $('#account-number3').addClass('d-none');

                // Cheque Book Detail
                if (data.SchemeAccountParameterViewModel.EnableChequeBook)
                    $('.customer-account-cheque-detail').removeClass('d-none');
                else
                    $('.customer-account-cheque-detail').addClass('d-none');

                // Document Upload
                if (data.SchemeAccountParameterViewModel.EnableDocumentUpload)
                    $('.account-document-detail').removeClass('d-none');
                else
                    $('.account-document-detail').addClass('d-none');

                // Passbook
                if (data.SchemeAccountParameterViewModel.EnablePassbookDetail)
                {
                    if (data.SchemePassbookViewModel.EnableAutoPassbookNumber)
                    {
                        $('.passbook-number-field').addClass('d-none');
                    }
                    else
                    {
                        $('.passbook-number-field').removeClass('d-none');
                    }
                }
                else
                {
                    $('.passbook-number-field').addClass('d-none');
                }

                // Scheme Deposit Interest Parameter
                let monthDiff = MonthDiffernece($('#account-opening-date').val(), $('#maturity-date').val());

                if (data.SchemeDepositInterestParameterViewModel.EnablePeriodicInterestPayout) {
                    // Check Account Age Validity For Periodic Interest
                    if (monthDiff < data.SchemeDepositInterestParameterViewModel.MinimumMonthForPeriodicInterestPayout) {
                        $('#interest-payout').addClass('d-none');
                        isEnablePayoutInterest = false;
                    }
                    else {
                        $('#interest-payout').removeClass('d-none');
                        isEnablePayoutInterest = true;
                    }

                    // Customize Day For Payout Interest
                    if (data.SchemeDepositInterestParameterViewModel.EnableCustomisePayoutInterestDayInAccountOpening)
                        $('#interest-payout-day-input').addClass('d-none');
                    else
                        $('#interest-payout-day-input').removeClass('d-none');
                }
                else {
                    $('#interest-payout').addClass('d-none');
                    $('#interest-payout-day-input').addClass('d-none');
                    isEnablePayoutInterest = false;
                }

                // Interest Parameter
                if (data.SchemeDepositInterestParameterViewModel.MaximumOverrideInterestAmount) {
                    minimumOverrideInterestAmount = data.SchemeDepositInterestParameterViewModel.MinimumOverrideInterestAmount;
                    maximumOverrideInterestAmount = data.SchemeDepositInterestParameterViewModel.MaximumOverrideInterestAmount;
                }

                // Joint Account
                if (data.SchemeAccountParameterViewModel.MaximumJointAccountHolder == 0)
                    $('.joint-account').addClass('d-none');
                else {
                    $('.joint-account').removeClass('d-none');
                    minimumJointAccountHolder = data.SchemeAccountParameterViewModel.MinimumJointAccountHolder;
                    maximumJointAccountHolder = data.SchemeAccountParameterViewModel.MaximumJointAccountHolder;
                }

                // Nominee
                if (data.SchemeAccountParameterViewModel.MaximumNominee == 0)
                    $('.account-nominee').addClass('d-none');
                else {
                    $('.account-nominee').removeClass('d-none');
                    minimumNominee = data.SchemeAccountParameterViewModel.MinimumNominee;
                    maximumNominee = data.SchemeAccountParameterViewModel.MaximumNominee;
                }

                // Agent
                if (data.SchemeDepositAccountParameterViewModel.EnableAgent)
                    $('.agent-accordion').removeClass('d-none');
                else
                    $('.agent-accordion').addClass('d-none');

                // SMS Service
                if (data.SchemeAccountParameterViewModel.EnableSmsService == false)
                    $('.customer-account-sms-service').addClass('d-none');
                else
                    $('.customer-account-sms-service').removeClass('d-none');

                // Email Service
                if (data.SchemeAccountParameterViewModel.EnableEmailService == false)
                    $('.customer-account-email-service').addClass('d-none');
                else
                    $('.customer-account-email-service').removeClass('d-none');

                // Notice Schedule
                if (data.SchemeAccountParameterViewModel.EnableSmsService == false && data.SchemeAccountParameterViewModel.EnableEmailService == false)
                    $('#notice-schedule-card').addClass('d-none');
                else
                    $('#notice-schedule-card').removeClass('d-none');

                // Demand Deposit
                if (data.SchemeDemandDepositDetailViewModel != null) {
                    // Enable Reference Person Detail
                    if (data.SchemeDemandDepositDetailViewModel.EnableReferencePersonDetail) {
                        $('#reference-person-detail-card').removeClass('d-none');

                        // Reference Person
                        minimumReferencePerson = data.SchemeDemandDepositDetailViewModel.MinimumNumberOfReferencePerson;
                        maximumReferencePerson = data.SchemeDemandDepositDetailViewModel.MaximumNumberOfReferencePerson;
                    }
                    else
                        $('#reference-person-detail-card').addClass('d-none');

                    // Enable Beneficiary Detail
                    if (data.SchemeDemandDepositDetailViewModel.EnableBeneficiaryDetail)
                        $('#customer-account-beneficiary-detail-card').removeClass('d-none');
                    else
                        $('#customer-account-beneficiary-detail-card').addClass('d-none');

                    // Photo Sign
                    if (data.SchemeDemandDepositDetailViewModel.EnablePhotoSign)
                        $('.customer-account-photo-sign').removeClass('d-none');
                    else
                        $('.customer-account-photo-sign').addClass('d-none');

                    // Sweep Detail
                    if (data.SchemeDemandDepositDetailViewModel.EnableSweepOut)
                        $('.customer-account-sweep-detail').removeClass('d-none');
                    else
                        $('.customer-account-sweep-detail').addClass('d-none');
                }

                // Term / Fixed Deposit
                if (data.SchemeFixedDepositParameterViewModel != null) {
                    // Certificate Number
                    if (data.SchemeDepositCertificateParameterViewModel != null)
                    {
                        if (data.SchemeDepositCertificateParameterViewModel.EnableAutoCertificateNumber)
                            $('#certificate-input').addClass('d-none');
                        else
                            $('#certificate-input').removeClass('d-none');
                    }

                    // Deposit Amount
                    $('#deposit-amount').attr('min', data.SchemeFixedDepositParameterViewModel.MinimumDepositAmount);
                    $('#deposit-amount').attr('max', data.SchemeFixedDepositParameterViewModel.MaximumDepositAmount);

                    // Custom Deposit Renewal Amount
                    $('#custom-renew-amount').attr('min', data.SchemeFixedDepositParameterViewModel.MinimumDepositAmount);
                    $('#custom-renew-amount').attr('max', data.SchemeFixedDepositParameterViewModel.MaximumDepositAmount);

                    depositMultipleOfThereAfter = data.SchemeFixedDepositParameterViewModel.DepositMultipleOfThereAfter;

                    // Account Renwal 
                    if (data.SchemeDepositAccountRenewalParameterViewModel != null) {
                        $('#grace-period-for-renewal').attr('min', 0);
                        $('#grace-period-for-renewal').attr('max', data.SchemeDepositAccountRenewalParameterViewModel.MaximumRenewalDurationAfterMaturityInDays)

                        // Auto Renewal
                        if (data.SchemeDepositAccountRenewalParameterViewModel.EnableAutoRenewal) {
                            $('#auto-renwal').removeClass('d-none');
                            $('#auto-renew-waiting-time-period').attr('min', data.SchemeDepositAccountRenewalParameterViewModel.MinimumDurationForAutoRenewal);
                            $('#auto-renew-waiting-time-period').attr('max', data.SchemeDepositAccountRenewalParameterViewModel.MaximumDurationForAutoRenewal);
                        }
                        else {
                            $('#enable-auto-renew-on-maturity').prop('checked', false);
                            $('#auto-renwal').addClass('d-none');
                            $('#auto-renew-waiting-time-period').attr('min', 0);
                            $('#auto-renew-waiting-time-period').attr('max', 0);
                        }
                    }
                }

                // Recurring Deposit  - Installment
                if (data.SchemeDepositInstallmentParameterViewModel != null) {
                    $('#deposit-installment-amount').attr('min', data.SchemeDepositInstallmentParameterViewModel.MinimumInstallment);
                    $('#deposit-installment-amount').attr('max', data.SchemeDepositInstallmentParameterViewModel.MaximumInstallment);
                    installmentAmountMultipleOf = data.SchemeDepositInstallmentParameterViewModel.InstallmentMultipleOf;
                }
            }
            else
                $('#scheme-id-error').removeClass('d-none');
        });
    }

    //Resend SMS
    function ResendSMS() {
        let mobileNumber = $('#field-value').val();
        $.get('/SMS/ReSendTeleVerificataionToken', { MobileNumber: mobileNumber }, function (data) {
            debugger;
            if (data === 'success') {
                $('.link').fadeOut('slow').delay(30000).fadeIn('slow');
                $('#myToast').toast('show').css({ 'z-index': '100', 'margin-top': '1%' });
            }
        });
    }

    function PhotoSignInput(input, imgPreview, fileType) {
        debugger;
        let storagePath = input.value;

        if (storagePath === '') {
            alert("Please upload an image");
            imgPreview.attr("src", ""); // Clear image source
            return;
        }

        schemeId = $('#scheme-id').val();
        if (schemeId <= 0) {
            debugger;
            alert("Please select Deposit Type");
            imgPreview.attr("src", ""); // Clear image source
            input.value = ""; // Clear file input value
            return;
        }

        $.getJSON('/AccountChildAction/GetDepositSchemeDetailBySchemeId', { _schemeId: schemeId }, function (data, textStatus, jqXHR) {
            debugger;
            let maxFileSize, validFileFormats;

            if (fileType === "photo") {
                maxFileSize = data.SchemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInLocalStorage ? data.SchemeDemandDepositDetailViewModel.MaximumFileSizeForPhotoDocumentUploadInLocalStorage : data.SchemeDemandDepositDetailViewModel.MaximumFileSizeForPhotoDocumentUploadInDb;
                validFileFormats = data.SchemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInLocalStorage ? data.SchemeDemandDepositDetailViewModel.PhotoDocumentAllowedFileFormatsForLocalStorage : data.SchemeDemandDepositDetailViewModel.PhotoDocumentAllowedFileFormatsForDb;
            } else if (fileType === "sign") {
                maxFileSize = data.SchemeDemandDepositDetailViewModel.EnableSignDocumentUploadInLocalStorage ? data.SchemeDemandDepositDetailViewModel.MaximumFileSizeForSignDocumentUploadInLocalStorage : data.SchemeDemandDepositDetailViewModel.MaximumFileSizeForSignDocumentUploadInDb;
                validFileFormats = data.SchemeDemandDepositDetailViewModel.EnableSignDocumentUploadInLocalStorage ? data.SchemeDemandDepositDetailViewModel.SignDocumentAllowedFileFormatsForLocalStorage : data.SchemeDemandDepositDetailViewModel.SignDocumentAllowedFileFormatsForDb;
            }

            if (input.files.length === 0) {
                alert("Please select a file");
                imgPreview.attr("src", ""); // Clear image source
                return false;
            }

            if ((input.files[0].size / 1024) >= maxFileSize) {
                alert("File size exceeds the maximum allowed size of " + maxFileSize + " KB");
                imgPreview.attr("src", ""); // Clear image source
                input.value = ""; // Clear file input value
                return;
            }

            let fileExtension = storagePath.split('.').pop().toLowerCase();
            let validExtensions = validFileFormats.split(',').map(function (ext) {
                return ext.trim().toLowerCase();
            });

            if (!validExtensions.includes(fileExtension)) {
                alert("Invalid file format. Allowed formats are: " + validFileFormats);
                imgPreview.attr("src", ""); // Clear image source
                input.value = ""; // Clear file input value
                return;
            }

            let reader = new FileReader();

            reader.onload = function (e) {
                imgPreview.attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        });
    }

    $('#photo-path').change(function (event) {
        let imgPreview = $('#image-preview-photo');
        PhotoSignInput(this, imgPreview, 'photo');
    });

    $('#sign-path').change(function (event) {
        debugger;
        let imgPreview = $('#image-preview-one');
        PhotoSignInput(this, imgPreview, 'sign');
    });

    // ##########   FUNCTIONS FOR NORMAL ACCORDION VALIDITY  

    // Sms Service Detail
    function IsValidSMSServiceDetailAccordionInputs() {
        result = true;

        if ($('#customer-account-sms-service-card').hasClass('d-none') === false) {
            if (IsValidInputDate('#activation-date-sms') === false) {
                result = false;
            }

            if (IsValidInputDate('#expiry-date-sms') === false) {
                result = false;
            }
        }

        if (result) {
            $('#sms-service-accordion-error').addClass('d-none');
        }
        else {
            $('#sms-service-accordion-error').removeClass('d-none');
        }

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

    // Cheque Detail
    function IsValidChequeDetailAccordionInputs() {
        let chequeNumber = parseInt($('#cheque-number').val());

        result = true;

        if ($('#customer-account-cheque-detail-card').hasClass('d-none') === false) {
            if ($('#cheqeue-book-id').prop('selectedIndex') < 1) {
                result = false;
            }

            // Cheque Number 
            if (isNaN(chequeNumber) === false) {

                minimum = parseInt($('#cheque-number').attr('min'));
                maximum = parseInt($('#cheque-number').attr('max'));

                if (parseInt(chequeNumber) < parseInt(minimum) || parseInt(chequeNumber) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }
        }

        if (result)
            $('#cheque-detail-accordion-error').addClass('d-none');
        else
            $('#cheque-detail-accordion-error').removeClass('d-none');

        return result;
    }

    // Term Deposit
    function IsValidTermDepositAccordionInputs() {
        debugger;
        let certificateNumber = parseInt($('#certificate-number').val());
        let noOfAccounts = parseInt($('#no-of-accounts').val());
        let interestPayoutAmount = parseFloat($('#interest-payout-amount').val());
        let interestPayoutDay = parseInt($('#interest-payout-day').val());
        let totalInterestAmount = parseFloat($('#total-interest-amount').val());
        let maturityAmount = parseFloat($('#maturity-amount').val());
        let gracePeriodForRenewal = parseInt($('#grace-period-for-renewal').val());
        let depositAmount = parseFloat($('#deposit-amount').val());
        let autoRenewWaitingTime = parseInt($('#auto-renew-waiting-time-period').val());
        let customRenewAmount = parseFloat($('#custom-renew-amount').val());
        let renewTenure = parseInt($('#renew-tenure').val());

        result = true;

        if ($('#term-deposit-account-card').hasClass('d-none') === false) {
            // Certificate Number 
            if ($('#certificate-input').hasClass('d-none') === false) {
                if (isNaN(certificateNumber) === false) {
                    minimum = parseInt($('#certificate-number').attr('min'));
                    maximum = parseInt($('#certificate-number').attr('max'));

                    if (parseInt(certificateNumber) < parseInt(minimum) || parseInt(certificateNumber) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // No Of Accounts
            if ($('#no-of-accounts-input').hasClass('d-none') === false) {
                if (isNaN(noOfAccounts) === false) {
                    minimum = parseInt($('#no-of-accounts').attr('min'));
                    maximum = parseInt($('#no-of-accounts').attr('max'));

                    if (parseInt(noOfAccounts) < parseInt(minimum) || parseInt(noOfAccounts) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

                // Deposit Amount
                if (isNaN(depositAmount) === false) {
                    minimum = parseFloat($('#deposit-amount').attr('min'));
                    maximum = parseFloat($('#deposit-amount').attr('max'));

                    if (parseFloat(depositAmount) < parseFloat(minimum) || parseFloat(depositAmount) > parseFloat(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }
            }

            // Interest Payout Frequency
            if ($('#interest-payout').hasClass('d-none') === false) {
                if ($('#interest-payout-frequency').prop('selectedIndex') < 1) {
                    result = false;
                }

                // Interest Payout Amount
                if ($('#interest-payout-amount-input').hasClass('d-none') === false) {
                    if (isNaN(interestPayoutAmount) === false) {
                        minimum = parseFloat($('#interest-payout-amount').attr('min'));
                        maximum = parseFloat($('#interest-payout-amount').attr('max'));

                        if (parseFloat(interestPayoutAmount) < parseFloat(minimum) || parseFloat(interestPayoutAmount) > parseFloat(maximum)) {
                            result = false;
                        }
                    }
                    else {
                        result = false;
                    }
                }

                // Interest Payout Day
                if ($('#interest-payout-day-input').hasClass('d-none') === false) {
                    if (isNaN(interestPayoutDay) === false) {
                        minimum = parseInt($('#interest-payout-day').attr('min'));
                        maximum = parseInt($('#interest-payout-day').attr('max'));

                        if (parseInt(interestPayoutDay) < parseInt(minimum) || parseInt(interestPayoutDay) > parseInt(maximum)) {
                            result = false;
                        }
                    }
                    else {
                        result = false;
                    }
                }
            }

            // Total Interest Amount
            if (isNaN(totalInterestAmount) === false) {
                minimum = parseFloat($('#total-interest-amount').attr('min'));
                maximum = parseFloat($('#total-interest-amount').attr('max'));

                if (parseFloat(totalInterestAmount) < parseFloat(minimum) || parseFloat(totalInterestAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = false;
            }

            // Maturity Amount
            if (isNaN(maturityAmount) === false) {
                minimum = parseFloat($('#maturity-amount').attr('min'));
                maximum = parseFloat($('#maturity-amount').attr('max'));

                if (parseFloat(maturityAmount) < parseFloat(minimum) || parseFloat(maturityAmount) > parseFloat(maximum)) { result = false; }
            }
            else {
                result = false;
            }

            // Renew Type
            if ($('#auto-renew-on-maturity-block').hasClass('d-none') === false) {
                if (isNaN(autoRenewWaitingTime) === false) {
                    // Auto Renew Waiting Time Period
                    minimum = parseInt($('#auto-renew-waiting-time-period').attr('min'));
                    maximum = parseInt($('#auto-renew-waiting-time-period').attr('max'));

                    if (parseInt(autoRenewWaitingTime) < parseInt(minimum) || parseInt(autoRenewWaitingTime) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }


                // Renew Type id
                if ($('#renew-type-id').prop('selectedIndex') < 1) {
                    result = false;
                }

                // Custom Renew Amount
                if ($('#custom-amount-input').hasClass('d-none') === false) {
                    if (isNaN(customRenewAmount) === false) {
                        minimum = parseFloat($('#custom-renew-amount').attr('min'));
                        maximum = parseFloat($('#custom-renew-amount').attr('max'));

                        if (parseFloat(customRenewAmount) < parseFloat(minimum) || parseFloat(customRenewAmount) > parseFloat(maximum)) {
                            result = false;
                        }
                    }
                    else {
                        result = false;
                    }

                    // Renew Tenure
                    if (isNaN(renewTenure) === false) {
                        minimum = parseInt($('#renew-tenure').attr('min'));
                        maximum = parseInt($('#renew-tenure').attr('max'));

                        if (parseInt(renewTenure) < parseInt(minimum) || parseInt(renewTenure) > parseInt(maximum)) {
                            result = false;
                        }
                    }
                    else {
                        result = false;
                    }
                }
            }


            // Maturity Instruction
            if ($('#auto-close-on-maturity-block').hasClass('d-none') === false) {
                if ($('#maturity-instruction').prop('selectedIndex') < 1) {
                    result = false;
                }
            }

            // Grace Period For Renewal
            if ($('#grace-period-for-renewal-input').hasClass('d-none') === false) {
                if (isNaN(gracePeriodForRenewal) === false) {
                    minimum = parseInt($('#grace-period-for-renewal').attr('min'));
                    maximum = parseInt($('#grace-period-for-renewal').attr('max'));

                    if (parseInt(gracePeriodForRenewal) < parseInt(minimum) || parseInt(gracePeriodForRenewal) > parseInt(maximum))
                        result = false;
                }
                else {
                    result = false;
                }
            }
        }

        if (result)
            $('#term-deposit-account-error').addClass('d-none');
        else
            $('#term-deposit-account-error').removeClass('d-none');

        return result;
    }

    // Standing Instruction
    function IsValidStandingInstructionAccordionInputs() {
        result = true;

        // Standing Instruction
        if ($('#customer-account-standing-instruction-card').hasClass('d-none') === false) {
            // Debit Account
            if ($('#auto-debit-block').hasClass('d-none') === false) {
                if ($('#customer-saving-account-debit').prop('selectedIndex') < 1) {
                    result = false;
                }
            }

            // Auto Close On Maturity
            if ($('#auto-close-on-maturity-input').hasClass('d-none') === false) {
                // Credit Account
                if ($('#deposit-credited-account-input').hasClass('d-none') === false) {
                    if ($('#customer-saving-account-credit').prop('selectedIndex') < 1) {
                        result = false;
                    }
                }

                // Interest Credit Account
                if ($('#interest-credited-account-input').hasClass('d-none') === false) {
                    if ($('#customer-saving-account-interest').prop('selectedIndex') < 1) {
                        result = false;
                    }

                }
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

    // Sweep Detail
    function IsValidSweepDetailAccordionInputs() {
        debugger;
        let minimumBalanceToTriggerSweepIn = parseFloat($('#minimum-balance-to-trigger-sweep-in').val());
        let maximumAmountToTriggerSweep = parseFloat($('#maximum-amount-to-trigger-sweep').val());
        let amount = parseFloat($('#minimum-term-deposit-amount').val());
        let maximumTermDepositAmount = parseFloat($('#maximum-term-deposit-amount').val());
        let minimumTermDepositTenure = parseInt($('#minimum-term-deposit-tenure').val());
        let maximumTermDepositTenure = parseInt($('#maximum-term-deposit-tenure').val());
        let defaultTermDepositTenure = parseInt($('#default-term-deposit-tenure').val());
        let maximumNumberOfSweepOut = parseInt($('#maximum-number-of-sweep-out').val());

        result = true;
        if ($('#customer-account-sweep-detail-card').hasClass('d-none') === false) {
            // Minimum Balance To Trigger Sweep In
            if (isNaN(minimumBalanceToTriggerSweepIn) === false) {
                minimum = parseFloat($('#minimum-balance-to-trigger-sweep-in').attr('min'));
                maximum = parseFloat($('#minimum-balance-to-trigger-sweep-in').attr('max'));

                if (parseFloat(minimumBalanceToTriggerSweepIn) < parseFloat(minimum) || parseFloat(minimumBalanceToTriggerSweepIn) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = true;
            }

            // Maximum Amount To Trigger Sweep
            if (isNaN(maximumAmountToTriggerSweep) === false) {
                minimum = parseFloat($('#maximum-amount-to-trigger-sweep').attr('min'));
                maximum = parseFloat($('#maximum-amount-to-trigger-sweep').attr('max'));

                if (parseFloat(maximumAmountToTriggerSweep) < parseFloat(minimum) || parseFloat(maximumAmountToTriggerSweep) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = true;
            }

            // Minimum Term deposit amount
            if (isNaN(amount) === false) {
                minimum = parseFloat($('#minimum-term-deposit-amount').attr('min'));
                maximum = parseFloat($('#minimum-term-deposit-amount').attr('max'));

                if (parseFloat(amount) < parseFloat(minimum) || parseFloat(amount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = true;
            }

            //  Maximum Term Deposit Amount
            if (isNaN(maximumTermDepositAmount) === false) {
                minimum = parseFloat($('#maximum-term-deposit-amount').attr('min'));
                maximum = parseFloat($('#maximum-term-deposit-amount').attr('max'));

                if (parseFloat(maximumTermDepositAmount) < parseFloat(minimum) || parseFloat(maximumTermDepositAmount) > parseFloat(maximum)) {
                    result = false;
                }
            }
            else {
                result = true;
            }

            //  Minimum Term Deposit Tenure
            if (isNaN(minimumTermDepositTenure) === false) {
                minimum = parseInt($('#minimum-term-deposit-tenure').attr('min'));
                maximum = parseInt($('#minimum-term-deposit-tenure').attr('max'));

                if (parseInt(minimumTermDepositTenure) < parseInt(minimum) || parseInt(minimumTermDepositTenure) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = true;
            }

            // Maximum Term Deposit Tenure
            if (isNaN(maximumTermDepositTenure) === false) {
                minimum = parseInt($('#maximum-term-deposit-tenure').attr('min'));
                maximum = parseInt($('#maximum-term-deposit-tenure').attr('max'));

                if (parseInt(maximumTermDepositTenure) < parseInt(minimum) || parseInt(maximumTermDepositTenure) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = true;
            }

            // Default Term Deposit Tenure
            if (isNaN(defaultTermDepositTenure) === false) {
                minimum = parseInt($('#default-term-deposit-tenure').attr('min'));
                maximum = parseInt($('#default-term-deposit-tenure').attr('max'));

                if (parseInt(defaultTermDepositTenure) < parseInt(minimum) || parseInt(defaultTermDepositTenure) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = true;
            }

            // Maximum Number Of Sweep Out
            if (isNaN(maximumNumberOfSweepOut) === false) {
                minimum = parseInt($('#maximum-number-of-sweep-out').attr('min'));
                maximum = parseInt($('#maximum-number-of-sweep-out').attr('max'));

                if (parseInt(maximumNumberOfSweepOut) < parseInt(minimum) || parseInt(maximumNumberOfSweepOut) > parseInt(maximum)) {
                    result = false;
                }
            }
            else {
                result = true;
            }

            //  Sweep Out Frequency
            if ($('#sweep-out-frequency-id').prop('selectedIndex') < 1) {
                result = false;
            }

            if (IsValidInputDate('#activation-date-sweep-detail') === false) {
                result = false;
            }
        }

        if (result)
            $('#sweep-detail-accordion-error').addClass('d-none');
        else
            $('#sweep-detail-accordion-error').removeClass('d-none');

        return result;
    }


    // ##########   FUNCTIONS FOR EVENT HANDLING  

    function RenewTypeFocusoutHandlerFunction() {
        let renewTypeId = $('#renew-type-id').val();

        // Get SysName Of Roll Over Type / Renew Type
        $.get('/AccountChildAction/GetRenewTypeSysNameById', { _renewTypeId: renewTypeId, async: false }, function (data, textStatus, jqXHR) {
            // Used In Show / Hide Standing Instruction On Auto Debit Toggle Switch
            renewTypeSysName = data;

            // Custom Amount
            if (data == 'CustomAmount')
                $('#custom-amount-input').removeClass('d-none');
            else {
                $('#custom-amount-input').addClass('d-none');
                $('#custom-renew-amount').val('');
            }

            // If Renew Only Principal Then Display Interest Credited In Account
            if (data == 'Principal') {
                $('.customer-account-standing-instruction').removeClass('d-none');
                $('#auto-close-on-maturity-input').removeClass('d-none');
                $('#deposit-credited-account-input').addClass('d-none');
                $('#interest-credited-account-input').removeClass('d-none');
            }
            else {
                if (!$('#enable-auto-close-on-maturity').is(':checked')) {
                    $('#interest-credited-account-input').addClass('d-none');

                    if (!$('#enable-auto-debit').is(':checked'))
                        $('.customer-account-standing-instruction').addClass('d-none');
                }
            }
        });
    }

    function MaturityInstructionFocusoutHandlerFunction() {
        if ($('#maturity-instruction').val() != DO_NOT_RENEW) {
            $('#grace-period-for-renewal-input').removeClass('d-none');
            $('#auto-renwal').removeClass('d-none');
        }
        else {
            $('#grace-period-for-renewal').val(0);
            $('#grace-period-for-renewal-input').addClass('d-none');
            $('#auto-renwal').addClass('d-none');
        }
    }

    function EnableAutoCloseOnMaturityClickHandlerFunction() {
        if ($('#enable-auto-close-on-maturity').is(':checked')) {
            $('.customer-account-standing-instruction').removeClass('d-none');
            $('#auto-renwal').addClass('d-none');
            $('#grace-period-for-renewal-input').addClass('d-none');
            $('#auto-close-on-maturity-input').removeClass('d-none');
            $('#deposit-credited-account-input').removeClass('d-none');
            $('#interest-credited-account-input').removeClass('d-none');

            $('#maturity-instruction').prop('selectedIndex', 0);
            $('#renew-type-id').prop('selectedIndex', 0);
        }
        else {
            if (!$('#enable-auto-debit').is(':checked'))
                $('.customer-account-standing-instruction').addClass('d-none');

            $('#auto-renwal').removeClass('d-none');
            $('#auto-close-on-maturity-input').addClass('d-none');
            $('#interest-credited-account-input').addClass('d-none');
            $('#customer-saving-account-credit').prop('selectedIndex', 0);
            $('#customer-saving-account-interest').prop('selectedIndex', 0);
        }
    }

    function EnableAutoRenewOnMaturityClickHandlerFunction() {
        if ($('#enable-auto-renew-on-maturity').is(':checked')) {
            $('#auto-close-maturity-input').addClass('d-none');
            $('#grace-period-for-renewal-input').addClass('d-none');
            $('#auto-close-on-maturity-block').addClass('d-none');

            $('#maturity-instruction').prop('selectedIndex', 0);
            $('#enable-auto-close-on-maturity').prop('checked', false);

            // Don't Clear While Loading Amend View, Clear After Loading Page.
            if (!isAmendView && !isVerifyView)
                $('#renew-type-id').prop('selectedIndex', 0);
        }
        else {
            if ($('#deposit-type-id').val() != DEMAND_DEPOSIT)
                $('#auto-close-maturity-input').removeClass('d-none');

            if (!$('#enable-auto-close-on-maturity').is(':checked')) {
                $('#auto-close-on-maturity-block').removeClass('d-none');

                if (!$('#enable-auto-debit').is(':checked'))
                    $('.customer-account-standing-instruction').addClass('d-none');
            }
        }
    }

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // #################  Join Account - DataTable Code 

    // DataTable Add Button 
    $('#btn-add-joint-account-dt').click(function (event) {
        // Display Alert Message Only When Modal Present On Page (i.e. Create And Amend) Hide On Verify
        if ($('#joint-account-modal').length) {
            editedSequenceNumber = 0;
            //editedJointAccountPersonId = '';
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

            editedJointAccountPersonId = columnValues[1];
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
    $('#btn-add-joint-account-modal').click(function (event) {
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
    $('#btn-update-joint-account-modal').click(function (event) {
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
    $('#btn-delete-joint-account-dt').click(function (event) {
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
        $('#tbl-joint-account input[type="checkbox"]:checked').each(function (index) {
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
        sequenceNumber = parseInt($('#joint-account-sequence-number').val());
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
            .filter(function (value, index) {
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

        //Validation Sequence Number
        if (isNaN(sequenceNumber) === false) {
            minimum = parseInt($('#joint-account-sequence-number').attr('min'));
            maximum = parseInt($('#joint-account-sequence-number').attr('max'));

            if (parseInt(sequenceNumber) < parseInt(minimum) || parseInt(sequenceNumber) > parseInt(maximum)) {
                result = false;
                $('#joint-account-sequence-number-error').removeClass('d-none');
            }

            if (isDuplicateSequenceNumber === true) {
                result = false;
                $('#joint-account-sequence-number-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#joint-account-sequence-number-error').removeClass('d-none');
        }

        // Validation Activation Date
        if (IsValidInputDate('#activation-date-joint-account-holder') === false) {
            result = false;
            $('#activation-date-joint-account-holder-error').removeClass('d-none');
        }
        else {
            $('#activation-date-joint-account-holder-error').addClass('d-none');
        }

        // Validation Expiry Date
        if (IsValidInputDate('#expiry-date-joint-account-holder') === false) {
            result = false;
            $('#expiry-date-joint-account-holder-error').removeClass('d-none');
        }
        else {
            $('#expiry-date-joint-account-holder-error').addClass('d-none');
        }

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

            $('#tbl-joint-account > TBODY> TR').each(function (index) {
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

            let schemeId = $('#scheme-id option:selected').val();

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

            editedNomineeCustomerId = columnValues[1];

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
                $.get('/PersonChildAction/GetPersonCurrentAge', { _personInformationNumber: columnValues[8], async: false }, function (data, textStatus, jqXHR) {
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
                    let td0 = columnValues[1];
                    let td1 = columnValues[2];

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
            $('#activation-date-nominee', myModal).val(activationDate);
            $('#expiry-date-nominee', myModal).val(expiryDate);
            $('#nominee-close-date', myModal).val(closeDate);
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
    $('#btn-add-account-nominee-modal').click(function (event) {
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
    $('#btn-update-account-nominee-modal').click(function (event) {
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
    $('#btn-delete-account-nominee-dt').click(function (event) {
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
        $('#tbl-account-nominee input[type="checkbox"]:checked').each(function (index) {
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
            .filter(function (value, index) {
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

        if (isNaN(nominationNumber.length) === false) {
            maximumLength = parseInt($('#nomination-number').attr('maxlength'));

            if (parseInt(nominationNumber.length) === 0 || parseInt(nominationNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#nomination-number-error').removeClass('d-none');
            }
            if (isDuplicateNomineeNumber === true) {
                result = false;
                $('#nomination-number-error').removeClass('d-none');
            }
        }

        if (IsValidInputDate('#nomination-date') === false) {
            result = false;
            $('#nomination-date-error').removeClass('d-none');
        }
        else {
            $('#nomination-date-error').addClass('d-none');
        }

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

        if ((isSelectedPersonInformationNumberForNominee === false && nameOfNominee === 'None') || parseInt(nameOfNominee.length) < 3 || parseInt(nameOfNominee.length) > 150) {
            result = false;

            $('#nominee-person-id-error').removeClass('d-none');
            $('#name-of-nominee-error').removeClass('d-none');
        }
        else {
            $('#nominee-person-id-error').addClass('d-none');
            $('#name-of-nominee-error').addClass('d-none');
        }

        if (isSelectedPersonInformationNumberForNominee === false) {
            if (transnameOfNominee == '' || transnameOfNominee == 'None' || parseInt(transnameOfNominee.length) > 150) {
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

            if (fullAddressDetails == 'None' || parseInt(fullAddressDetails.length) > 500) {
                result = false;
                $('#nominee-full-address-details-error').removeClass('d-none');
            }
            else
                $('#nominee-full-address-details-error').addClass('d-none');

            if (transFullAddress == 'None' || parseInt(transFullAddress.length) > 500) {
                result = false;
                $('#trans-nominee-full-address-details-error').removeClass('d-none');
            }
            else
                $('#trans-nominee-full-address-details-error').addClass('d-none');

            if (contactDetails == 'None' || parseInt(contactDetails.length) > 150) {
                result = false;
                $('#nominee-contact-details-error').removeClass('d-none');
            }
            else
                $('#nominee-contact-details-error').addClass('d-none');

            if (transContactDetails == '' || transContactDetails == 'None') {
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

        if (IsValidInputDate('#expiry-date-nominee') === false) {
            result = false;
            $('#nominee-expiry-date-error').removeClass('d-none');
        }

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

            if (IsValidInputDate('#appointed-date-of-contact') === false) {
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

            $.get('/PersonChildAction/GetPersonContactDetailEntryStatus', { _personContactDetailPrmKey: columnValues[8], async: false }, function (data, textStatus, jqXHR) {
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
    $('#btn-add-contact-modal').click(function (event) {
        if (IsValidContactDataTableModal()) {
            if (isMobile) {
                $('#btn-add-contact-modal').addClass('read-only');

                $.get('/SMS/ValidateVerificationToken', { MobileNumber: fieldValue, Token: verificationCode, async: false }, function (data, textStatus, jqXHR) {
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
                            personContactDetailPrmkey,
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
                    personContactDetailPrmkey,
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
    $('#btn-update-contact-modal').click(function (event) {
        $('#select-all-contact').prop('checked', false);

        if (IsValidContactDataTableModal()) {
            if (isMobile) {
                $('#btn-update-contact-modal').addClass('read-only');

                $.get('/SMS/ValidateVerificationToken', { MobileNumber: fieldValue, Token: verificationCode, async: false }, function (data, textStatus, jqXHR) {
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
                            personContactDetailPrmkey,
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
                    personContactDetailPrmkey,
                    customerAccountPrmKey,
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
    $('#btn-delete-contact-dt').click(function (event) {
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
        $('#tbl-contact input[type="checkbox"]:checked').each(function (index) {
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
            personContactDetailPrmkey = 0;
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
            else {
                $('#contact-type-error').addClass('d-none');
            }


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
                else {
                    $('#field-value-error').addClass('d-none');
                }

                // Mobile OTP Validation
                if (verificationCode === '' || verificationCode === '0') {
                    result = false;
                    $('#verification-token-error').removeClass('d-none');
                }
            }
            else {
                maximumLength = parseInt($('#field-value').attr('maxlength'));

                if (parseInt(fieldValue.length) === 0 || parseInt(fieldValue.length) > parseInt(maximumLength)) {
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

    /// #################   Person  Address Detail - DataTable Code 

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
            $.get('/PersonChildAction/GetDocumentValidations', { _personAddressDetailPrmKey: columnValues[21], async: false }, function (data, textStatus, jqXHR) {
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

            if (columnValues[17] === 'True')
                $('#is-verified-address').prop('checked', true);
            else
                $('#is-verified-address').prop('checked', false);

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
    $('#btn-add-person-address-modal').click(function (event) {
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
    $('#btn-update-person-address-modal').click(function (event) {
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
    $('#btn-delete-person-address-dt').click(function (event) {
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
        $('#tbl-person-address tbody input[type="checkbox"]:checked').each(function (index) {
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

        if (hasDivClass === true) {
            reasonForModification = 'None';
        }
        else {
            if (reasonForModification === '')
                reasonForModification = 'None';
        }


        //Validation Address Type
        if ($('#address-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#address-type-id-error').removeClass('d-none');
        }

        if (isNaN(flatDoorNo.length) === false) {
            maximumLength = parseInt($('#flat-door-no').attr('maxlength'));
            minimumLength = parseInt($('#flat-door-no').attr('minlength'));

            //Validation FlatDoor No Min Length - 3 And Max Length = 50
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
            $('#trans-flat-door-no-error').removeClass('d-none')
        }
        else {
            $('#trans-flat-door-no-error').addClass('d-none')
        }

        //Validation Building Name
        if (isNaN(buildingName.length) === false) {
            maximumLength = parseInt($('#building-name').attr('maxlength'));
            minimumLength = parseInt($('#building-name').attr('minlength'));

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
            $('#trans-building-name-error').removeClass('d-none')
        }
        else {
            $('#trans-building-name-error').addClass('d-none')
        }

        //Validation Road Name
        if (isNaN(roadName.length) === false) {
            maximumLength = parseInt($('#road-name').attr('maxlength'));
            minimumLength = parseInt($('#road-name').attr('minlength'));

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
            $('#trans-road-name-error').removeClass('d-none')
        }
        else {
            $('#trans-road-name-error').addClass('d-none')
        }

        //Validation Area Name
        if (isNaN(areaName.length) === false) {
            maximumLength = parseInt($('#area-name').attr('maxlength'));
            minimumLength = parseInt($('#area-name').attr('minlength'));

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
            $('#trans-area-name-error').removeClass('d-none')
        } else
            $('#trans-area-name-error').addClass('d-none')

        //Validation City
        if ($('#city-id').prop('selectedIndex') < 1) {
            result = false;
            $('#city-id-error').removeClass('d-none');
        }
        else {
            $('#city-id-error').addClass('d-none');
        }

        //Validation Residence Type
        if ($('#residence-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#residence-type-id-error').removeClass('d-none');
        }
        else {
            $('#residence-type-id-error').addClass('d-none');
        }

        //Validation Residence Ownership
        if ($('#residence-ownership-id').prop('selectedIndex') < 1) {
            result = false;
            $('#residence-ownership-id-error').removeClass('d-none')
        }
        else {
            $('#residence-ownership-id-error').addClass('d-none')
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

    // Address Type Unique Dropdown
    function SetAddressTypeUniqueDropdownList() {
        // Show All List Items
        $('#address-type-id').html('');
        $('#address-type-id').append(ADDRESS_TYPE_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-person-address > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (addressDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedAddressTypeId)
                    $('#address-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }

    function SetAgentUniqueDropdownList() {
        // Show All List Items
        $('#agent-id').html('');
        $('#agent-id').append(AGENT_DROPDOWN_LIST);

        // Hide Added Agent DropdownList Items
        $('#tbl-agent > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (agentDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null) {
                if (myColumnValues[1] != editedAgentId)
                    $('#agent-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }

    /// ##############   Scheme Turn Over Limit  - DataTable Code

    // DataTable Add Button 
    $('#btn-add-turn-over-limit-dt').click(function (event) {

        event.preventDefault();

        SetModalTitle('turn-over-limit', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-turn-over-limit-dt').click(function () {
        SetModalTitle('turn-over-limit', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-turn-over-limit-dt').data('rowindex');
            id = $('#turn-over-limit-modal').attr('id');
            myModal = $('#' + id).modal();

            activationDate = new Date(columnValues[6]);
            expiryDate = new Date(columnValues[7]);

            $('#frequency-id', myModal).val(columnValues[1]);
            $('#transaction-type-id', myModal).val(columnValues[3])
            $('#amount', myModal).val(columnValues[5]);
            $('#activation-date-turn-over-limit', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-turn-over-limit', myModal).val(GetInputDateFormat(expiryDate));
            $('#note-turn-over-limit', myModal).val(columnValues[8]);
            $('#reason-for-modification-turn-over-limit', myModal).val(columnValues[9]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-turn-over-limit-edit-dt').addClass('read-only');
            $('#turn-over-limit-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-turn-over-limit-modal').click(function (event) {
        if (IsValidTurnOverLimitModal()) {
            row = turnOverLimitDataTable.row.add([
                tag,
                frequency,
                frequencyText,
                transactionType,
                transactionTypeText,
                amount,
                activationDate,
                expiryDate,
                note,
                reasonForModification,
            ]).draw();

            HideColumnsturnOverLimitDataTable();

            turnOverLimitDataTable.columns.adjust().draw();

            // Hide Table Required Data Error Message
            $('#turn-over-limit-accordion-error').addClass('d-none');

            $('#turn-over-limit-modal').modal('hide');

            EnableNewOperation('turn-over-limit');
        }
    });

    // Modal update Button Event
    $('#btn-update-turn-over-limit-modal').click(function (event) {
        $('#select-all-turn-over-limit').prop('checked', false);

        if (IsValidTurnOverLimitModal()) {
            turnOverLimitDataTable.row(selectedRowIndex).data([
                tag,
                frequency,
                frequencyText,
                transactionType,
                transactionTypeText,
                amount,
                activationDate,
                expiryDate,
                note,
                reasonForModification,
            ]).draw();

            HideColumnsturnOverLimitDataTable();

            turnOverLimitDataTable.columns.adjust().draw();

            $('#turn-over-limit-modal').modal('hide');

            EnableNewOperation('turn-over-limit');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-turn-over-limit-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-turn-over-limit tbody input[type="checkbox"]:checked').each(function () {
                    turnOverLimitDataTable.row($('#tbl-turn-over-limit tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-turn-over-limit-dt').data('rowindex');
                    EnableNewOperation('turn-over-limit');

                    $('#select-all-turn-over-limit').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!turnOverLimitDataTable.data().any())
                        $('#turn-over-limit-accordion-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-turn-over-limit').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-turn-over-limit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = turnOverLimitDataTable.row(row).index();

                rowData = (turnOverLimitDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-turn-over-limit-dt').data('rowindex', arr);
                EnableDeleteOperation('turn-over-limit')
            });
        }
        else {
            EnableNewOperation('turn-over-limit')

            $('#tbl-turn-over-limit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-turn-over-limit tbody').click('input[type=checkbox]', function () {
        $('#tbl-turn-over-limit input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = turnOverLimitDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (turnOverLimitDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('turn-over-limit');

                    $('#btn-update-turn-over-limit-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-turn-over-limit-dt').data('rowindex', rowData);
                    $('#btn-delete-turn-over-limit-dt').data('rowindex', arr);
                    $('#select-all-turn-over-limit').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-turn-over-limit tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('turn-over-limit');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('turn-over-limit');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('turn-over-limit');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-turn-over-limit').prop('checked', true);
        else
            $('#select-all-turn-over-limit').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidTurnOverLimitModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        frequency = $('#frequency-id option:selected').val();
        frequencyText = $('#frequency-id option:selected').text();
        transactionType = $('#transaction-type-id option:selected').val();
        transactionTypeText = $('#transaction-type-id option:selected').text();
        amount = parseFloat($('#amount').val());
        activationDate = $('#activation-date-turn-over-limit').val();
        expiryDate = $('#expiry-date-turn-over-limit').val();
        note = $('#note-turn-over-limit').val();
        reasonForModification = $('#reason-for-modification-turn-over-limit').val();

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        // Validate General Ledger
        if ($('#frequency-id').prop('selectedIndex') < 1) {
            result = false;
            $('#frequency-id-error').removeClass('d-none');
        }

        // Validate General Ledger
        if ($('#transaction-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#transaction-type-id-error').removeClass('d-none');
        }

        // Validate amount
        if (isNaN(amount) === false) {
            minimum = parseFloat($('#amount').attr('min'));
            maximum = parseFloat($('#amount').attr('max'));
            if (parseFloat(amount) < parseFloat(minimum) || parseFloat(amount) > parseFloat(maximum)) {
                result = false;
                $('#amount-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#amount-error').removeClass('d-none');
        }

        // Validate Activation Date
        if (IsValidInputDate('#activation-date-turn-over-limit') === false) {
            result = false;
            $('#activation-date-turn-over-limit-error').removeClass('d-none');
        }
        else {
            $('#activation-date-turn-over-limit-error').addClass('d-none');
        }

        // Validate Expiry Date
        if (IsValidInputDate('#expiry-date-turn-over-limit') === false) {
            result = false;
            $('#expiry-date-turn-over-limit-error').removeClass('d-none');
        }
        else {
            $('#expiry-date-turn-over-limit-error').addClass('d-none');
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HideColumnsturnOverLimitDataTable() {
        turnOverLimitDataTable.column(1).visible(false);
        turnOverLimitDataTable.column(3).visible(false);
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
                note,
            ]).draw();

            HideNoticeScheduleDataTableColumns();

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
        let result = true;

        // Set Default Value, If Empty
        if (note === '')
            note = 'None';

        // Validate Modal Input
        if ($('#notice-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#notice-type-id-error').removeClass('d-none');
        }

        if ($('#comunication-media-id').prop('selectedIndex') < 1) {
            result = false;
            $('#comunication-media-id-error').removeClass('d-none');
        }

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


    /// ################ Customer Deposit Account Agent

    // DataTable Add Button 
    $('#btn-add-agent-dt').click(function (event) {
        event.preventDefault();
        editedAgentId = '';
        SetAgentUniqueDropdownList();
        SetModalTitle('agent', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-agent-dt').click(function () {
        SetModalTitle('agent', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-agent-dt').data('rowindex');
            id = $('#agent-modal').attr('id');
            editedAgentId = columnValues[1];
            myModal = $('#' + id).modal();

            // Display Edited Agent
            SetAgentUniqueDropdownList();

            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);

            // Display Value In Modal Inputs
            $('#agent-id', myModal).val(columnValues[1]);
            $('#activation-date-agent', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-agent', myModal).val(GetInputDateFormat(expiryDate));
            $('#note-agent', myModal).val(columnValues[5]);
            $('#reason-for-modification-agent', myModal).val(columnValues[6]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-agent-dt').addClass('read-only');
            $('#agent-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-agent-modal').click(function (event) {
        if (IsValidAgentDataTableModal()) {
            row = agentDataTable.row.add([
                          tag,
                          agentId,
                          agentText,
                          agentActivationDate,
                          agentExpiryDate,
                          note,
                          reasonForModification,
            ]).draw();

            // Error Message In Span
            $('#agent-error').addClass('d-none');

            HideAgentDataTableColumns();

            agentDataTable.columns.adjust().draw();

            ClearModal('agent');

            $('#agent-modal').modal('hide');

            EnableNewOperation('agent');
        }
    });

    // Modal update Button Event
    $('#btn-update-agent-modal').click(function (event) {

        $('#select-all-agent').prop('checked', false);
        if (IsValidAgentDataTableModal()) {
            agentDataTable.row(selectedRowIndex).data([
                          tag,
                          agentId,
                          agentText,
                          agentActivationDate,
                          agentExpiryDate,
                          note,
                          reasonForModification,
            ]).draw();
            // Error Message In Span
            $('#agent-validation span').html('');

            HideAgentDataTableColumns();

            agentDataTable.columns.adjust().draw();

            $('#agent-modal').modal('hide');

            EnableNewOperation('agent');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-agent-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-agent tbody input[type="checkbox"]:checked').each(function () {
                    agentDataTable.row($('#tbl-agent tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-agent-dt').data('rowindex');

                    EnableNewOperation('agent');

                    SetAgentUniqueDropdownList();

                    $('#select-all-agent').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!agentDataTable.data().any())
                        $('#agent-error').removeClass('d-none');
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-agent').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-agent tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = agentDataTable.row(row).index();

                rowData = (agentDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-agent-dt').data('rowindex', arr);
                EnableDeleteOperation('agent')
            });
        }
        else {
            EnableNewOperation('agent')

            $('#tbl-agent tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event  
    $('#tbl-agent tbody').click('input[type="checkbox"]', function () {
        debugger;
        $('#tbl-agent input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = agentDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (agentDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('agent');

                    $('#btn-update-agent-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-agent-dt').data('rowindex', rowData);
                    $('#btn-delete-agent-dt').data('rowindex', arr);
                    $('#select-all-agent').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-agent tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('agent');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('agent');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('agent');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-agent').prop('checked', true);
        else
            $('#select-all-agent').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-agent > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (agentDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null) {
            $('#agent-id').find("option[value='" + columnValues[1] + "']").hide();
        }
        else {
            return true;
        }
    });

    // Validate Fund Module
    function IsValidAgentDataTableModal() {
        result = true;
        debugger
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        agentId = $('#agent-id option:selected').val();
        agentText = $('#agent-id option:selected').text();
        agentActivationDate = $('#activation-date-agent').val();
        agentExpiryDate = $('#expiry-date-agent').val();
        note = $('#note-agent').val();
        reasonForModification = $('#reason-for-modification-agent').val();
        rVisibility = $('#agent-div').hasClass('d-none');

        if (rVisibility)
            reasonForModification = 'None';

        if (note === '')

            note = 'None';

        if ($('#agent-id').prop('selectedIndex') < 1) {
            result = false;
            $('#agent-id-error').removeClass('d-none');
        }
        else {
            $('#agent-id-error').addClass('d-none');
        }

        if (IsValidInputDate('#activation-date-agent') === false) {
            result = false;
            $('#agent-activation-date-error').removeClass('d-none');
        }
        else {
            $('#agent-activation-date-error').addClass('d-none');
        }

        if (IsValidInputDate('#expiry-date-agent') === false) {
            result = false;
            $('#agent-expiry-date-error').removeClass('d-none');
        }
        else {
            $('#agent-expiry-date-error').addClass('d-none');
        }
        return result;
    }

    // Hide Unnecessary Columns
    function HideAgentDataTableColumns() {
        agentDataTable.column(1).visible(false);
        agentDataTable.column(6).visible(false);
    }

    /// ################  Beneficiary Detail - DataTable Code ###################

    // DataTable Add Button 
    $('#btn-add-beneficiary-detail-dt').click(function (event) {

        event.preventDefault();

        SetModalTitle('beneficiary-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-beneficiary-detail-dt').click(function () {
        SetModalTitle('beneficiary-detail', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-beneficiary-detail-dt').data('rowindex');
            id = $('#beneficiary-detail-modal').attr('id');
            myModal = $('#' + id).modal();

            // Get Only Activation Date
            activationDate = new Date(columnValues[15]);
            expiryDate = new Date(columnValues[16]);

            // Display Value In Modal Inputs
            $('#name-of-beneficiary-code', myModal).val(columnValues[1]);
            $('#name-of-beneficiary', myModal).val(columnValues[2]);
            $('#short-name-of-beneficiary', myModal).val(columnValues[3]);
            $('#customer-account-type-id', myModal).val(columnValues[4]);
            $('#customer-beneficiary-account-number', myModal).val(columnValues[6]);
            $('#confirm-account-number').val(columnValues[6]);
            $('#customer-beneficiary-ifsc-code', myModal).val(columnValues[7]);
            $('#customer-beneficiary-bank-name', myModal).val(columnValues[8]);
            $('#customer-beneficiary-branch', myModal).val(columnValues[9]);
            $('#customer-beneficiary-city', myModal).val(columnValues[10]);
            $('#customer-number', myModal).val(columnValues[11]);
            $('#customer-beneficiary-mobile-number', myModal).val(columnValues[12]);
            $('#customer-beneficiary-email-id', myModal).val(columnValues[13]);
            $('#customer-beneficiary-virtual-private-address', myModal).val(columnValues[14]);
            $('#activation-date-customer-beneficiary', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-customer-beneficiary', myModal).val(GetInputDateFormat(expiryDate));
            $('#customer-beneficiary-note', myModal).val(columnValues[17]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-beneficiary-detail-dt').addClass('read-only');
            $('#beneficiary-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-beneficiary-detail-modal').click(function (event) {
        if (IsValidBeneficiaryDetailDataTableModal()) {
            row = beneficiaryDetailDataTable.row.add([
                           tag,
                           nameOfBeneficiaryCode,
                           nameOfBeneficiary,
                           shortName,
                           customerAccountTypeId,
                           customerAccountTypeIdText,
                           accountNumber,
                           ifscCode,
                           bankName,
                           branch,
                           city,
                           customerNumber,
                           mobileNumber,
                           emailId,
                           virtualPrivateAddress,
                           activationDate,
                           expiryDate,
                           note
            ]).draw();

            // Error Message In Span
            $('#beneficiary-detail-error').addClass('d-none');

            HideColumnsBeneficiaryDetailDataTable();

            beneficiaryDetailDataTable.columns.adjust().draw();

            ClearModal('beneficiary-detail');

            $('#beneficiary-detail-modal').modal('hide');

            EnableNewOperation('beneficiary-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-beneficiary-detail-modal').click(function (event) {

        $('#select-all-beneficiary-detail').prop('checked', false);
        if (IsValidBeneficiaryDetailDataTableModal()) {
            beneficiaryDetailDataTable.row(selectedRowIndex).data([
                           tag,
                           nameOfBeneficiaryCode,
                           nameOfBeneficiary,
                           shortName,
                           customerAccountTypeId,
                           customerAccountTypeIdText,
                           accountNumber,
                           ifscCode,
                           bankName,
                           branch,
                           city,
                           customerNumber,
                           mobileNumber,
                           emailId,
                           virtualPrivateAddress,
                           activationDate,
                           expiryDate,
                           note
            ]).draw();
            // Error Message In Span
            $('#beneficiary-detail-validation span').html('');

            HideColumnsBeneficiaryDetailDataTable();

            beneficiaryDetailDataTable.columns.adjust().draw();

            $('#beneficiary-detail-modal').modal('hide');

            EnableNewOperation('beneficiary-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-beneficiary-detail-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-beneficiary-detail tbody input[type="checkbox"]:checked').each(function () {
                    beneficiaryDetailDataTable.row($('#tbl-beneficiary-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-beneficiary-detail-dt').data('rowindex');
                  EnableNewOperation('beneficiary-detail');

                  $('#select-all-beneficiary-detail').prop('checked', false);
                    // Display Error, If Table Has Not Any Record
                    if (!beneficiaryDetailDataTable.data().any())
                        $('#beneficiary-detail-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-beneficiary-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-beneficiary-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = beneficiaryDetailDataTable.row(row).index();

                rowData = (beneficiaryDetailDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-beneficiary-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('beneficiary-detail')
            });
        }
        else {
            EnableNewOperation('beneficiary-detail')

            $('#tbl-beneficiary-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-beneficiary-detail tbody').click('input[type="checkbox"]', function () {
        debugger;
        $('#tbl-beneficiary-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = beneficiaryDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (beneficiaryDetailDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('beneficiary-detail');

                    $('#btn-update-beneficiary-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-beneficiary-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-beneficiary-detail-dt').data('rowindex', arr);
                    $('#select-all-beneficiary-detail').data('rowindex', arr);
                }
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-beneficiary-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('beneficiary-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('beneficiary-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('beneficiary-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-beneficiary-detail').prop('checked', true);
        else
            $('#select-all-beneficiary-detail').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidBeneficiaryDetailDataTableModal()
    {
        debugger
        result = true;

        // Get Modal Inputs In Localletiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        nameOfBeneficiaryCode = parseInt($('#name-of-beneficiary-code').val());
        nameOfBeneficiary = $('#name-of-beneficiary').val();
        shortName = $('#short-name-of-beneficiary').val();
        customerAccountTypeId = $('#customer-account-type-id option:selected').val();
        customerAccountTypeIdText = $('#customer-account-type-id option:selected').text();
        accountNumber = parseInt($('#customer-beneficiary-account-number').val());
        ifscCode = $('#customer-beneficiary-ifsc-code').val();
        bankName = $('#customer-beneficiary-bank-name').val();
        branch = $('#customer-beneficiary-branch').val();
        city = $('#customer-beneficiary-city').val();
        customerNumber = parseInt($('#customer-number').val());
        mobileNumber = $('#customer-beneficiary-mobile-number').val();
        emailId = $('#customer-beneficiary-email-id').val();
        virtualPrivateAddress = $('#customer-beneficiary-virtual-private-address').val();
        activationDate = $('#activation-date-customer-beneficiary').val();
        expiryDate = $('#expiry-date-customer-beneficiary').val();
        note = $('#customer-beneficiary-note').val();

        if (note === '')
            note = 'None';

        // Validate Activation Date
        if (IsValidInputDate('#activation-date-customer-beneficiary') === false) {
            result = false;
            $('#customer-beneficiary-activation-date-error').removeClass('d-none');
        }
        else {
            $('#customer-beneficiary-activation-date-error').addClass('d-none');
        }

        // Validate Expiry Date
        if (IsValidInputDate('#expiry-date-customer-beneficiary') === false) {
            result = false;
            $('#customer-beneficiary-expiry-date-error').removeClass('d-none');
        }
        else {
            $('#customer-beneficiary-expiry-date-error').addClass('d-none');
        }


        // Validate Customer Account Type
        if ($('#customer-account-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#customer-account-type-id-error').removeClass('d-none');
        }
        else
            $('#customer-account-type-id-error').addClass('d-none');


        let txtConfirmAccountNumber = $('#confirm-account-number').val();

        // Check if both input fields are not empty and if their values match
        if (isNaN(accountNumber) === false)
        {
            minimum = parseInt($('#customer-beneficiary-account-number').attr('min'));
            maximum = parseInt($('#customer-beneficiary-account-number').attr('max'));
            if (parseInt(accountNumber) < parseInt(minimum) || parseInt(accountNumber) > parseInt(maximum))
            {
                result = false;
                $('#customer-beneficiary-account-number-error').removeClass('d-none');
            } 
        } else
        {
            result = false;
            $('#customer-beneficiary-account-number-error').removeClass('d-none');
        }


        if (isNaN(txtConfirmAccountNumber) === false) {
            maximum = parseInt($('#customer-account-number').attr('max'));
            if (parseInt(txtConfirmAccountNumber) < 1 || parseInt(txtConfirmAccountNumber) > parseInt(maximum)) {
                result = false;
                $('#customer-account-number-error').removeClass('d-none');
            } 
        } else {
            result = false;
            $('#customer-account-number-error').removeClass('d-none');
        }

        if (parseInt(accountNumber) === parseInt(txtConfirmAccountNumber)) {
            $('#confirm-account-number-error').addClass('d-none');
        }
        else {
            result = false;
            $('#confirm-account-number-error').removeClass('d-none');
        }

        // Beneficiary Code
        if (isNaN(nameOfBeneficiaryCode) === false) {
            minimum = parseInt($('#name-of-beneficiary-code').attr('min'));
            maximum = parseInt($('#name-of-beneficiary-code').attr('max'));

            if (parseInt(nameOfBeneficiaryCode) < parseInt(minimum) || parseInt(nameOfBeneficiaryCode) > parseInt(maximum)) {
                result = false;
                $('#name-of-beneficiary-code-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#name-of-beneficiary-code-error').removeClass('d-none');
        }

        // Name OF Beneficiary
        if (nameOfBeneficiary.length > 0) {
            minimumLength = parseInt($('#name-of-beneficiary').attr('minlength'));
            maximumLength = parseInt($('#name-of-beneficiary').attr('maxlength'));

            if (parseInt(nameOfBeneficiary.length) < parseInt(minimumLength) || parseInt(nameOfBeneficiary.length) > parseInt(maximumLength)) {
                result = false;
                $('#name-of-beneficiary-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#name-of-beneficiary-error').removeClass('d-none');
        }

        // Assuming 'shortName' holds the short name of the beneficiary
        if (isNaN(shortName.length) === false) {
            minimumLength = parseInt($('#short-name-of-beneficiary').attr('minlength'));
            maximumLength = parseInt($('#short-name-of-beneficiary').attr('maxlength'));

            if (parseInt(shortName.length) < parseInt(minimumLength) || parseInt(shortName.length) > parseInt(maximumLength)) {
                result = false;
                $('#short-name-of-beneficiary-error').removeClass('d-none');

            }
        }
        else {
            result = false;
            $('#short-name-of-beneficiary-error').removeClass('d-none');
        }

        // mobileNumber
        if (mobileNumber.length != 10) {
            debugger;
            result = false;
            $('#customer-beneficiary-mobile-number-error').removeClass('d-none');
        }
        else
            $('#customer-beneficiary-mobile-number-error').addClass('d-none');

        // Ifsc Code Of Beneficiary
        maximumLength = parseInt($('#customer-beneficiary-ifsc-code').attr('maxlength'));

        if (parseInt(ifscCode.length) === 0 || parseInt(ifscCode.length) > parseInt(maximumLength)) {
            $('#customer-beneficiary-ifsc-code-error').removeClass('d-none');
            result = false;
        }
        else {
            $('#customer-beneficiary-ifsc-code-error').addClass('d-none');
        }

        // Validate Customer Number
        if (isNaN(customerNumber) === false) {
            minimum = parseInt($('#customer-number').attr('min'));
            maximum = parseInt($('#customer-number').attr('max'));
            if (parseInt(customerNumber) < parseInt(minimum) || parseInt(customerNumber) > parseInt(maximum)) {
                result = false;
                $('#customer-number-error').removeClass('d-none');
            }
        } else {
            result = false;
            $('#customer-number-error').removeClass('d-none');
        }

        //virtualPrivateAddress
        if (isNaN(virtualPrivateAddress.length) === false) {
            maximumLength = parseInt($('#customer-beneficiary-virtual-private-address').attr('maxlength'));

            if (parseInt(virtualPrivateAddress.length) === 0 || parseInt(virtualPrivateAddress.length) > parseInt(maximumLength)) {
                result = false;
                $('#customer-beneficiary-virtual-private-address-error').removeClass('d-none');

            }
        }
        else {
            result = false;
            $('#customer-beneficiary-virtual-private-address-error').removeClass('d-none');
        }

        // Email Id
        if (emailId === '' || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailId)) {
            result = false;
            $('#customer-beneficiary-email-id-error').removeClass('d-none');
        } else
            $('#customer-beneficiary-email-id-error').addClass('d-none');

        // Validate Duplicate Record By Account Number
        if (isDuplicateBeneficiaryAccountNumber) {
            result = false;
            $('#customer-beneficiary-account-number-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsBeneficiaryDetailDataTable() {
        beneficiaryDetailDataTable.column(4).visible(false);
    }


    /// ##############  Reference Person Detail - DataTable Code ####################

    // DataTable Add Button 
    $('#btn-add-reference-person-detail-dt').click(function (event) {
        selectedReferencePersonId = '';
        selectedReferencePersonText = '';

        dataTableRecordCount = referencePersonDetailDataTable.rows().count();

        if (parseInt(dataTableRecordCount) >= parseInt(maximumReferencePerson)) {
            $('#reference-person-detail-modal').modal('hide');
            alert('Number Of Reference Person Are Allowed Between ' + minimumReferencePerson + ' And ' + maximumReferencePerson);
        }
        else {
            event.preventDefault();
            SetModalTitle('reference-person-detail', 'Add');
        }
    });

    // DataTable Edit Button 
    $('#btn-edit-reference-person-detail-dt').click(function () {

        SetModalTitle('reference-person-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-reference-person-detail-dt').data('rowindex');
            id = $('#reference-person-detail-modal').attr('id');
            myModal = $('#' + id).modal();
            selectedReferencePersonId = columnValues[1];
            selectedReferencePersonText = columnValues[2];

            // Display Value In Modal Inputs
            $('#reference-customer-account-number', myModal).val(columnValues[2]);
            $('#is-validate-sign', myModal).val(columnValues[3]);
            $('#note-reference-person-detail', myModal).val(columnValues[4]);

            $('#is-validate-sign').prop('checked', columnValues[3].toString().toLowerCase() === 'true' ? true : false);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-reference-person-detail-dt').addClass('read-only');
            $('#reference-person-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-reference-person-detail-modal').click(function (event) {
        if (IsValidReferencePersonDetailDataTableModal()) {
            row = referencePersonDetailDataTable.row.add([
                          tag,
                          selectedReferencePersonId,
                          selectedReferencePersonText,
                          isValidateSign,
                          note,
            ]).draw();

            // Error Message In Span
            //$('#agent-error').addClass('d-none');

            HideReferencePersonDetailDataTableColumns();

            referencePersonDetailDataTable.columns.adjust().draw();

            ClearModal('reference-person-detail');

            $('#reference-person-detail-modal').modal('hide');

            EnableNewOperation('reference-person-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-reference-person-detail-modal').click(function (event) {

        $('#select-all-reference-person-detail').prop('checked', false);
        if (IsValidReferencePersonDetailDataTableModal()) {
            referencePersonDetailDataTable.row(selectedRowIndex).data([
                          tag,
                          selectedReferencePersonId,
                          selectedReferencePersonText,
                          isValidateSign,
                          note,
            ]).draw();
            // Error Message In Span
            //$('#agent-validation span').html('');

            HideReferencePersonDetailDataTableColumns();

            referencePersonDetailDataTable.columns.adjust().draw();

            $('#reference-person-detail-modal').modal('hide');

            EnableNewOperation('reference-person-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-reference-person-detail-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-reference-person-detail tbody input[type="checkbox"]:checked').each(function () {
                    referencePersonDetailDataTable.row($('#tbl-reference-person-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-reference-person-detail-dt').data('rowindex');
                    EnableNewOperation('reference-person-detail');

                    $('#select-all-reference-person-detail').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (!referencePersonDetailDataTable.data().any())
                        $('#reference-person-detail-error').removeClass('d-none');
                }));
            }

            // Validate Required Number Of Joint Account Holders.
            dataTableRecordCount = referencePersonDetailDataTable.rows().count();

            if (parseInt(dataTableRecordCount) < parseInt(minimumReferencePerson)) {
                result = false;
                minMaxResult = false;

                $('#reference-person-detail-min-max-error').html('Number Of Reference Person Must Be Between ' + minimumReferencePerson + ' And ' + maximumReferencePerson);

                $('#reference-person-detail-accordion-error').addClass('d-none');
                $('#reference-person-detail-min-max-error').removeClass('d-none');
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event  
    $('#select-all-reference-person-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-reference-person-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = referencePersonDetailDataTable.row(row).index();

                rowData = (referencePersonDetailDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-reference-person-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('reference-person-detail')
            });
        }
        else {
            EnableNewOperation('reference-person-detail')

            $('#tbl-reference-person-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event  
    $('#tbl-reference-person-detail tbody').click('input[type="checkbox"]', function () {
        debugger;
        $('#tbl-reference-person-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = referencePersonDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (referencePersonDetailDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('reference-person-detail');

                    $('#btn-update-reference-person-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-reference-person-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-reference-person-detail-dt').data('rowindex', arr);
                    $('#select-all-reference-person-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-reference-person-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('reference-person-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('reference-person-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('reference-person-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-reference-person-detail').prop('checked', true);
        else
            $('#select-all-reference-person-detail').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-reference-person-detail > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (referencePersonDetailDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null) {
            $('#customer-account-number-id').find("option[value='" + columnValues[1] + "']").hide();
        }
        else {
            return true;
        }
    });

    // Validate Fund Module
    function IsValidReferencePersonDetailDataTableModal() {
        result = true;
        minMaxResult = true;

        debugger
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        isValidateSign = $('#is-validate-sign').is(':checked') ? 'True' : 'False';
        note = $('#note-reference-person-detail').val();

        if (note === '')
            note = 'None';

        if (selectedReferencePersonId === '') {
            result = false;
            $('#reference-customer-account-number-error').removeClass('d-none');
        }
        else {
            $('#reference-customer-account-number-error').addClass('d-none');
        }

        // Validate Required Number Of Joint Accounts
        dataTableRecordCount = referencePersonDetailDataTable.rows().count();

        // Add + 1 (i.e. Current Row Count)

        dataTableRecordCount = dataTableRecordCount + 1;

        if (parseInt(dataTableRecordCount) < parseInt(minimumReferencePerson)) {
            minMaxResult = false;
            $('#reference-person-detail-min-max-error').html('Number Of Customer Account  Are Allowed Between ' + minimumReferencePerson + ' And ' + maximumReferencePerson);
        }

        if (result) {
            if (minMaxResult == false) {
                $('#reference-person-detail-accordion-error').addClass('d-none');
                $('#reference-person-detail-min-max-error').removeClass('d-none');
            }
            else {
                $('#reference-person-detail-accordion-error').addClass('d-none');
                $('#reference-person-detail-min-max-error').addClass('d-none');
            }
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideReferencePersonDetailDataTableColumns() {
        referencePersonDetailDataTable.column(1).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger;

        let isValidAllInputs = true;

        let schemeId = $('#scheme-id option:selected').val();
        let schemePrmkey = $('#scheme-id option:selected').text();

        // Validate Inputs Of Full Page 
        if ($('form').valid()) {
            $('.lastrow').remove();

            // Return List Object, Hence Create Array
            let jointAccountArray = new Array();
            let nomineeDetailArray = new Array();
            let contactDetailArray = new Array();
            let addressDetailArray = new Array();
            let turnOverLimitArray = new Array();
            let agentDetailArray = new Array();
            let noticeScheduleArray = new Array();
            let beneficiaryDetailArray = new Array();
            let referencePersonDetailArray = new Array();

            jointAccountDataTable.page.len(-1).draw();
            nomineeDataTable.page.len(-1).draw();
            contactDataTable.page.len(-1).draw();
            addressDataTable.page.len(-1).draw();
            turnOverLimitDataTable.page.len(-1).draw();
            agentDataTable.page.len(-1).draw();
            noticeScheduleDataTable.page.len(-1).draw();
            beneficiaryDetailDataTable.page.len(-1).draw();
            referencePersonDetailDataTable.page.len(-1).draw();

            // Accordion 1 -SMS Service Detail Validation, If Enable
            if (!IsValidSMSServiceDetailAccordionInputs())
                isValidAllInputs = false;

            // Accordion 2 - Email Service Detail Validation, If Enable
            if (!IsValidEmailServiceDetailAccordionInputs())
                isValidAllInputs = false;

            // Accordion 3 - Cheque Detail Validation, If Enable
            if (!IsValidChequeDetailAccordionInputs())
                isValidAllInputs = false;

            // Accordion 4 - Sweep Detail Validation, If Enable
            if (!IsValidSweepDetailAccordionInputs())
                isValidAllInputs = false;

            // Accordion 5 - Term Deposit Validation, If Enable
            if (!IsValidTermDepositAccordionInputs())
                isValidAllInputs = false;

            // Accordion 6 - Standing Instruction Validation, If Enable
            if (!IsValidStandingInstructionAccordionInputs())
                isValidAllInputs = false;

            // 1. Address Detail - Create Array For Person Address Detail Data Table To Pass Data
            if (!$('#address-details-card').hasClass('d-none')) {
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
            if (!$('#contact-details-card').hasClass('d-none')) {
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
            if (!$('#joint-account-card').hasClass('d-none')) {
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
                                debugger;
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
            if (!$('#account-nominee-card').hasClass('d-none')) {
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

            // Create Array For Turn Over Limit Data Table To Pass Data
            if ($('#enable-turn-over-limit').is(':checked')) {
                if (turnOverLimitDataTable.data().any()) {

                    if (isValidAllInputs) {
                        $('#turn-over-limit-accordion-error').addClass('d-none');

                        // Get Data Table Values In Turn Over Limit Array
                        $('#tbl-turn-over-limit > TBODY > TR').each(function () {
                            debugger;
                            currentRow = $(this).closest('tr');

                            columnValues = (turnOverLimitDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                turnOverLimitArray.push({
                                    'FrequencyId': columnValues[1],
                                    'TransactionTypeId': columnValues[3],
                                    'Amount': columnValues[5],
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

                else {
                    $('#turn-over-limit-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Accordion 6 - Notice Schedule Data Table Validation (Optional, Not Mandatory)
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

            // Agent - Create Array For Person Agent Detail Data Table To Pass Data
            if (!$('#agent-accordion-card').hasClass('d-none')) {
                if (agentDataTable.data().any()) {

                    if (isValidAllInputs) {

                        $('#agent-error').addClass('d-none');

                        $('#tbl-agent > TBODY > TR').each(function () {
                            currentRow = $(this).closest("tr");

                            columnValues = (agentDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                agentDetailArray.push({
                                    'AgentId': columnValues[1],
                                    'ActivationDate': columnValues[3],
                                    'ExpiryDate': columnValues[4],
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
                    $('#agent-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Reference Person - Create Array For Person Agent Detail Data Table To Pass Data
            if (!$('#reference-person-detail-card').hasClass('d-none')) {
                if (referencePersonDetailDataTable.data().any()) {
                    dataTableRecordCount = parseInt(referencePersonDetailDataTable.rows().count());

                    // Check Required Number Of Joint Accounts
                    if (parseInt(dataTableRecordCount) < parseInt(minimumReferencePerson)) {
                        isValidAllInputs = false
                        $('#reference-person-detail-min-max-error').html('Number Of Reference Person Are Allowed Between ' + minimumReferencePerson + ' And ' + maximumReferencePerson);
                        $('#reference-person-detail-min-max-error').removeClass('d-none');
                    }
                    else {
                        $('#reference-person-detail-accordion-error').addClass('d-none');
                        $('#reference-person-detail-min-max-error').addClass('d-none');

                        if (isValidAllInputs) {
                            $('#reference-person-detail-accordion-error').addClass('d-none');

                            $('#tbl-reference-person-detail > TBODY > TR').each(function () {
                                currentRow = $(this).closest("tr");

                                columnValues = (referencePersonDetailDataTable.row(currentRow).data());

                                // Handling Code If Row Is Undefined Or Null
                                if (typeof columnValues != 'undefined' && columnValues != null) {
                                    referencePersonDetailArray.push({
                                        'CustomerAccountId': columnValues[1],
                                        'IsValidateSign': columnValues[3],
                                        'Note': columnValues[4],
                                    });
                                }
                                else
                                    return false;
                            });
                        }
                    }
                }
                else {
                    if (parseInt(minimumReferencePerson) > 0) {
                        isValidAllInputs = false;
                        $('#reference-person-detail-min-max-error').html('Number Of Reference Persons Are Allowed Between ' + minimumReferencePerson + ' And ' + maximumReferencePerson);
                        $('#reference-person-detail-min-max-error').removeClass('d-none');
                    }
                    else {
                        $('#reference-person-detail-accordion-error').addClass('d-none');
                        $('#reference-person-detail-min-max-error').addClass('d-none');
                    }
                }
            }

            // Create Array For Person Beneficiary Detail Data Table To Pass Data
            if (!$('#customer-account-beneficiary-detail-card').hasClass('d-none')) {
                if (beneficiaryDetailDataTable.data().any()) {

                    if (isValidAllInputs) {
                        $('#beneficiary-detail-error').addClass('d-none');

                        $('#tbl-beneficiary-detail > TBODY > TR').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (beneficiaryDetailDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                beneficiaryDetailArray.push({
                                    'BeneficiaryCode': columnValues[1],
                                    'NameOfBeneficiary': columnValues[2],
                                    'ShortName': columnValues[3],
                                    'CustomerAccountTypeId': columnValues[4],
                                    'AccountNumber': columnValues[6],
                                    'IfscCode': columnValues[7],
                                    'BankName': columnValues[8],
                                    'Branch': columnValues[9],
                                    'City': columnValues[10],
                                    'CustomerNumber': columnValues[11],
                                    'MobileNumber': columnValues[12],
                                    'EmailId': columnValues[13],
                                    'VirtualPrivateAddress': columnValues[14],
                                    'ActivationDate': columnValues[15],
                                    'ExpiryDate': columnValues[16],
                                    'Note': columnValues[17],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#beneficiary-detail-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Call Cantroller Save Data Table Method 
            if (isValidAllInputs) {
                debugger;
                $.ajax({
                    url: customerDepositAccountDataTableUrl,
                    type: 'POST',
                    async: false,
                    data:
                    {
                        '_customerJointAccountHolder': jointAccountArray, '_customerAccountNominee': nomineeDetailArray, '_customerDepositAccountAgent': agentDetailArray, '_personContactDetail': contactDetailArray, '_personAddress': addressDetailArray, '_customerAccountTurnOverLimit': turnOverLimitArray,
                        '_customerAccountNoticeSchedule': noticeScheduleArray, '_customerAccountBeneficiaryDetail': beneficiaryDetailArray, '_referencePersonDetail': referencePersonDetailArray,
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Customer Account DataTable!!! Error Message - ' + error.toString());
                    }
                });
            }
            else {
                // Stop Create Post Method
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
