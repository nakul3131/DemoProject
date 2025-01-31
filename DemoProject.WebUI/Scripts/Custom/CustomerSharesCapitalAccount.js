'use strict'
$(document).ready(function () 
{
    // DropdownList Variables
    let personDropdownListData = '';
    let personDropdownListDataForJointAccount = '';
    let personDropdownListDataForNominee = '';
    let personDropdownListDataForGuardian = '';

    const ADDRESS_TYPE_DROPDOWN_LIST = $('#address-type-id').html();

    // Array
    let finalDropdownListArray = [];

    // Count
    let dropDownListItemCount = 0;

    // Global Variable
    let result = true;
    let minMaxResult = true;
    let listItemCount = 0;
    let dropdownListItems = '';
    let dataTableRecordCount = 0;
    let isVerifyView = false;

    let isUpdate = false;
    let prevPersonId = '';
    let prevBusinessOfficeId = '0';
    let prevGeneralLedgerId = '0';
    let prevSchemeId = '0';
    let selectedPersonId = '';
    let selectedPersonText = '';
    let selectedjointPersonId = '';
    let selectedjointPersonText = '';
    let personInformationNumber = '';
    let personInformationNumberText = '';
    let contactType = '';
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
    let minimumNominee = 0;
    let maximumNominee = 0;

    // @@@@@@@@@@ Data Table Related Varible Declaration

    let tag = '';
    let id = '';
    let myModal;
    let selectedRowIndex;
    let row;
    let rowData;
    let checked;
    let columnValues;
    let minimumLength = 0;
    let maximumLength = 0;
    let minimum = 0;
    let maximum = 0;

    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;

    let arr = new Array();
    let dateSplitArray;

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
    let editedNomineeCustomerId = '';

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
    let personContactDetailPrmkey = 0;
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

    // Turn Over Limit
    let frequency = '';
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

    let url = window.location.href;
    let index = url.lastIndexOf('/') + 1;
    let filenameWithExtension = url.substr(index);
    let filename = filenameWithExtension.split('?')[0]  // <-- added this line

    //  ************** Create Data Table  **************
    let addressDataTable = CreateDataTable('person-address');
    let contactDataTable = CreateDataTable('contact');
    let jointAccountDataTable = CreateDataTable('joint-account');
    let nomineeDataTable = CreateDataTable('account-nominee');
    let turnOverLimitDataTable = CreateDataTable('turn-over-limit');
    let noticeScheduleDataTable = CreateDataTable('notice-schedule');

    let schemeId = $('#scheme-id').val();

    // Page Loading Default Values (Usually For Amend)
    SetPageLoadingDefaultValues();

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Sms Send - Display SMS Delilety Status
    $('#send-code').click(function (event)
    {
        let _mobileNumber = $('#field-value').val();
        $('#send-code').addClass('d-none');

        $.get('/SMS/SendVerificationToken', { MobileNumber: _mobileNumber, async: false }, function (data, textStatus, jqXHR) {
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

    // Clear Turn Over Limit
    $('#enable-turn-over-limit').change(function (event)
    {
        turnOverLimitDataTable.clear().draw();
    });

    // On Changing Business Office, All Dependent Setting (Loan Type , Genreral Ledger, Scheme) Required To Be Clear.
    $('#business-office-id').focusout(function (event) {
        if (!isVerifyView)
        {
            debugger;
            let selectedBusinessOfficeId = $('#business-office-id option:selected').val();

            if (prevBusinessOfficeId != selectedBusinessOfficeId) {
                // Clear Dependent Content
                if (prevBusinessOfficeId != '0')
                    $('#business-office-change-info').removeClass('d-none');

                $.get('/DynamicDropdownList/GetSharesGeneralLedgerDropdownListByBusinessOfficeId', { _businessOfficeId: selectedBusinessOfficeId, async: false }, function (data, textStatus, jqXHR) {
                    dropdownListItems = '<option value="0">--- Select General Ledger --- </option>';
                    $.each(data, function (index, selectListItemObj) {
                        dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
                        SetSchemeDropdownList();
                    });

                    $('#general-ledger-id').html(dropdownListItems);

                    listItemCount = $('#general-ledger-id > option').not(':first').length;


                    // Select Default First Record, If Dropdown Has Only One Record
                    if (listItemCount == 1) {
                        $('#general-ledger-id').prop('selectedIndex', 1);
                        $('#general-ledger-id').change();

                        debugger;
                        SetSchemeDropdownList();

                        prevGeneralLedgerId = $('#general-ledger-id option:selected').val();
                    }
                });

                if (prevBusinessOfficeId != '0')
                    $('#business-office-change-info').addClass('d-none');

                prevBusinessOfficeId = selectedBusinessOfficeId;

                // Clear Dependent Data
                $('#general-ledger-id').prop('selectedIndex', -1);
                $('#scheme-id').prop('selectedIndex', -1);
                $('#person-id').val('');

                addressDataTable.clear().draw();
                contactDataTable.clear().draw();
                jointAccountDataTable.clear().draw();
                nomineeDataTable.clear().draw();
            }
            else {
                $('#business-office-change-info').addClass('d-none');
                prevBusinessOfficeId = $('#business-office-id option:selected').val();
            }
        }
    });

    // On Changing General Ledger, All Dependent Setting (Scheme) Required To Be Clear.
    $('#general-ledger-id').focusout(function (event) {
        if (!isVerifyView)
        {
            let selectedGeneralLedgerId = $('#general-ledger-id option:selected').val();

            if (prevGeneralLedgerId != selectedGeneralLedgerId) {
                // Clear
                if (prevGeneralLedgerId != '0')
                    $('#general-ledger-change-info').removeClass('d-none');

                // Set Scheme Dropdown List Based On Selected General Ledger
                $.get('/DynamicDropdownList/GetSchemeDropdownListByGeneralLedger', { _generalLedgerId: selectedGeneralLedgerId, async: false }, function (data, textStatus, jqXHR) {
                    dropdownListItems = '<option value="0">--- Select Scheme --- </option>';

                    $.each(data, function (index, selectListItemObj) {
                        dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
                    });

                    $('#scheme-id').html(dropdownListItems);

                    listItemCount = $('#scheme-id > option').not(':first').length;

                    // Select Default First Record, If Dropdown Has Only One Record
                    if (listItemCount == 1) {
                        $('#scheme-id').prop('selectedIndex', 1);
                        $('#scheme-id').change();

                        SetPersonDropdownList();
                    }
                });

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

    // On Changing Scheme, All Dependent Setting Required To Be Clear Or Change.
    $('#scheme-id').focusout(function (event)
    {
        debugger;
        if (!isVerifyView)
        {
             schemeId = $('#scheme-id option:selected').val();

            if (prevSchemeId != schemeId) {
                $('#person-id').prop('selectedIndex', -1);

                // Clear
                if (typeof prevSchemeId == 'undefined' || prevSchemeId == '0')
                    $('#scheme-change-info').addClass('d-none');
                else
                    $('#scheme-change-info').removeClass('d-none');

                // Set Page Input As Per Scheme Selection
                $.get('/AccountChildAction/GetSharesCapitalSchemeDetailBySchemeId', { _schemeId: schemeId, async: false }, function (data, textStatus, jqXHR) {
                    if (data) {
                        // Call Input Setting As Per Selected Scheme
                        SetSchemeSetting(data);

                        if (prevSchemeId != 0)
                            $('#scheme-id-error').addClass('d-none');

                        prevSchemeId = schemeId;
                    }
                    else {
                        prevSchemeId = $('#scheme-id option:selected').val();
                    }
                });

                // Get Person Dropdown
                $.get('/DynamicDropdownList/GetPersonDropdownListForSharesAccountOpening', { _schemeId: schemeId, async: false }, function (data, textStatus, jqXHR) {
                    debugger
                    personDropdownListData = data;
                });

                prevSchemeId = schemeId;

                // Clear Dependent Data
                $('#person-id').val('');
                addressDataTable.clear().draw();
                contactDataTable.clear().draw();
                jointAccountDataTable.clear().draw();
                nomineeDataTable.clear().draw();
            }
            else {
                $('#scheme-change-info').addClass('d-none');
                prevSchemeId = $('#scheme-id option:selected').val();
            }
        }
    });

    // Person Autocomplete FocusOut Event
    $('#person-id').focusout(function (event)
    {
        debugger;
        $(this).val($(this).val().trim());
        SetPersonData();
    });

    // Joint Account Person Dropdown List FocusOut Event
    $('#person-id-joint-account-holder').focusout(function (event)
    {
        $(this).val($(this).val().trim());
    });

    // Clear Depended Inputs
    $('#account-opening-date').focusout(function (event)
    {
        $('#tenure').val('');
        $('#maturity-date').val('');
    });

    // Hide Guardian Details If User Input Nominee Birtdate As Adults
    $('#nominee-birth-date').focusout(function (event) {

        $.get('/PersonChildAction/GetAgeFromBirthDate', { _birthDate: $('#nominee-birth-date').val(), async: false }, function (data, textStatus, jqXHR) {
            if (data < 18)
                $('#guardian-card').removeClass('d-none');
            else
                $('#guardian-card').addClass('d-none');
        });
    });

    // Hide Nominee Person Information Number, If User Manually Input Nominee Name 
    $('#name-of-nominee').focusout(function (event) {
        let nameOfNominee = $('#name-of-nominee').val();

        if ((nameOfNominee != 'None') && (nameOfNominee.length > 3)) {
            $('#nominee-person-id').prop('selectedIndex', -1);
            $('#nominee-person-information-number-input').addClass('d-none');
        }
        else
            $('#nominee-person-information-number-input').removeClass('d-none');
    });

    // Hide Nominee Manually Inputs, If User Selects Adult Person
    $('#nominee-person-id').focusout(function (event)
    {
        debugger;
        if ($('#nominee-person-id').val() == 0 || typeof $('#nominee-person-id').val() == null)
            $('#nomineedetails').removeClass('d-none');
        else
        {
            $('#nomineedetails').addClass('d-none');

            //let personInformationNumbers = $(this).val();
            $.get('/PersonChildAction/GetPersonCurrentAge', { _personInformationNumber: nomineePersonInformationNumber, async: false }, function (data, textStatus, jqXHR) {
                if (data <= 18) 
                    $('#guardian-card').removeClass('d-none');
                else 
                    $('#guardian-card').addClass('d-none');
            });
        }
    })

    // Hide Proportionate Amount If Holding Percentage Is Greater Than 0
    $('#holding-percentage').focusout(function (event) {
        let holdingPercentage = parseFloat($(this).val());

        if (!isNaN(holdingPercentage) || parseFloat(holdingPercentage) > 0)
            $('#proportionate-amount-for-each-nominee-input').addClass('d-none');
        else
            $('#proportionate-amount-for-each-nominee-input').removeClass('d-none');
    })

    // Hide Holding Percentage If Proportionate Amount Is Greater Than 0
    $('#proportionate-amount-for-each-nominee').focusout(function (event) {
        let proportionateAmount = parseFloat($(this).val());

        if (!isNaN(proportionateAmount) || parseFloat(proportionateAmount) > 0)
            $('#holding-percentage-input').addClass('d-none');
        else
            $('#holding-percentage-input').removeClass('d-none');
    })

    // Hide Guardian Details If User Select Adult Person Name As Nominee (** This List Contains Only Adult Person Name)
    $('#nominee-guardian-person-information-number').focusout(function (event) {
        let personInfoNumber = $('#nominee-guardian-person-information-number').val();

        if (personInfoNumber == '')
            $('.nominee-guardian-details').removeClass('d-none');
        else
            $('.nominee-guardian-details').addClass('d-none');
    });

    $('#guardian-full-name').focusout(function (event) {
        let nameOfGuardian = $('#guardian-full-name').val();

        if ((nameOfGuardian != 'None') && (nameOfGuardian.length > 3)) {
            $('#nominee-guardian-person-information-number').prop('selectedIndex', 0);
            $('#nominee-guardian-person-information-number-input').addClass('d-none');
        }
        else
            $('#nominee-guardian-person-information-number-input').removeClass('d-none');
    });

    // Contact Type Validation
    $('#field-value').focusout(function (event)
    {
        $(this).val($(this).val().trim());

        // If Contact Type Is Mobile
        if (isMobile) {
            $('#verification-code').val('');

            if ($('#field-value').val().length == 10)
            {
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

    // Verification Code Visibility & Input Type Visibility Based On Contact Type
    $('#contact-type').focusout(function (event) {
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

    //  ************** A U T O      C O M P L E T E   **************

    // Maint Customer Person Autocomplete
    dropdownListItems = '<option value="0">--- Select Person ---</option>';

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

    //  ************** Accordion Input Validation  **************

    // SMS Service Detail Input Validation
    $('.sms-service-input').focusout(function ()
    {
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

    // Get Selected Scheme For Other Than Create Operation
    let selectedSchemeId = $('#scheme-id option:selected').val();

    // Set Page Data According To Selected Scheme
    if (selectedSchemeId != '')
    {
        $.get('/AccountChildAction/GetSharesCapitalSchemeDetailBySchemeId', { _schemeId: selectedSchemeId, async: false }, function (data, textStatus, jqXHR) {
            if (data) {
                // Call Input Setting As Per Selected Scheme
                SetSchemeSetting(data);

                prevSchemeId = selectedSchemeId;
            }
            else {
                prevSchemeId = $('#scheme-id option:selected').val();
            }
        });
    }

    // ###########################  U S E R      D E F I N E D       F U N C T I O N S    ###########################   

    //Set Scheme Setting
    function SetSchemeSetting() {
        debugger;
        
        // Input Visiblity Base On Selected Scheme
        $.get('/AccountChildAction/GetSharesCapitalSchemeDetailBySchemeId', { _schemeId: schemeId, async: false }, function (data) {
            if (data)
            {
                debugger;

                // Input Visibility
                // Account Number EnableAutoAccountNumber = true
                // Account Number2
                if (data.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 == true) {
                    $('#account-number2').removeClass('d-none');
                }
                else {
                    $('#account-number2').addClass('d-none');
                    $('#account-number2').val('');
                }

                // Auto Application Number
                if (data.SchemeAccountParameterViewModel.EnableApplication === true)
                {
                    if (data.SchemeApplicationParameterViewModel.EnableAutoApplicationNumber === true)
                    {
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

                //AccountNumber3
                if (data.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 == true) {
                    $('#account-number3').removeClass('d-none');
                }
                else {
                    $('#account-number3').addClass('d-none');
                    $('#account-number3').val('');
                }

                // Auto Account Number
                if (data.SchemeCustomerAccountNumberViewModel.EnableAutoAccountNumber == true) {
                    $('#account-number-input').addClass('d-none');
                }
                else {
                    //$('#account-number').val('');
                    $('#account-number-input').removeClass('d-none');
                }

                // Auto Member Number
                if (data.SchemeSharesCapitalAccountParameterViewModel.EnableAutoMemberNumber == true) {
                    $('#member-number-grp').addClass('d-none');
                }
                else {
                    $('#member-number-grp').removeClass('d-none');
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

                // Joint Account
                if (data.SchemeAccountParameterViewModel.MaximumJointAccountHolder == 0)
                    $('.joint-account').addClass('d-none');
                else {
                    $('.joint-account').removeClass('d-none');
                    minimumJointAccountHolder = data.SchemeAccountParameterViewModel.MinimumJointAccountHolder;
                    maximumJointAccountHolder = data.SchemeAccountParameterViewModel.MaximumJointAccountHolder;
                }

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
            }
            else
                $('#scheme-id-error').removeClass('d-none');
        });
    }

    function SetPersonData()
    {
        debugger;
        // Change Setting If Person Actually Changed
        if (selectedPersonId != prevPersonId)
        {
            // Clear
            if (prevPersonId != '')
                $('#person-change-info').removeClass('d-none');

            // Clear Related Datatables
            addressDataTable.clear().draw();
            contactDataTable.clear().draw();
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

            prevPersonId = selectedPersonId;
        }
        else {
            $('#person-change-info').addClass('d-none');
            prevPersonId = selectedPersonId;
        }
    }

    function SetGeneralLedgerDropdownList()
    {
        debugger;
        let selectedBusinessOfficeId = $('#business-office-id option:selected').val();

        if (prevBusinessOfficeId != selectedBusinessOfficeId) {
            // Clear Dependent Content
            if (prevBusinessOfficeId != '0')
                $('#business-office-change-info').removeClass('d-none');

            $.get('/DynamicDropdownList/GetSharesGeneralLedgerDropdownListByBusinessOfficeId', { _businessOfficeId: selectedBusinessOfficeId, async: false }, function (data, textStatus, jqXHR) {
                dropdownListItems = '<option value="0">--- Select General Ledger --- </option>';
                $.each(data, function (index, selectListItemObj) {
                    dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
                    SetSchemeDropdownList();
                });

                $('#general-ledger-id').html(dropdownListItems);

                listItemCount = $('#general-ledger-id > option').not(':first').length;


                // Select Default First Record, If Dropdown Has Only One Record
                if (listItemCount == 1) {
                    $('#general-ledger-id').prop('selectedIndex', 1);
                    $('#general-ledger-id').change();

                    debugger;
                    SetSchemeDropdownList();

                    prevGeneralLedgerId = $('#general-ledger-id option:selected').val();
                }
            });

            if (prevBusinessOfficeId != '0')
                $('#business-office-change-info').addClass('d-none');

            prevBusinessOfficeId = selectedBusinessOfficeId;

            // Clear Dependent Data
            $('#general-ledger-id').prop('selectedIndex', -1);
            $('#scheme-id').prop('selectedIndex', -1);
            $('#person-id').val('');

            addressDataTable.clear().draw();
            contactDataTable.clear().draw();
            jointAccountDataTable.clear().draw();
            nomineeDataTable.clear().draw();
        }
        else {
            $('#business-office-change-info').addClass('d-none');
            prevBusinessOfficeId = $('#business-office-id option:selected').val();
        }
    }

    function SetSchemeDropdownList()
    {
        let selectedGeneralLedgerId = $('#general-ledger-id option:selected').val();

        if (prevGeneralLedgerId != selectedGeneralLedgerId) {
            // Clear
            if (prevGeneralLedgerId != '0')
                $('#general-ledger-change-info').removeClass('d-none');

            // Set Scheme Dropdown List Based On Selected General Ledger
            $.get('/DynamicDropdownList/GetSchemeDropdownListByGeneralLedger', { _generalLedgerId: selectedGeneralLedgerId, async: false }, function (data, textStatus, jqXHR)
            {
                dropdownListItems = '<option value="0">--- Select Scheme --- </option>';

                $.each(data, function (index, selectListItemObj) {
                    dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
                });

                $('#scheme-id').html(dropdownListItems);

                listItemCount = $('#scheme-id > option').not(':first').length;

                // Select Default First Record, If Dropdown Has Only One Record
                if (listItemCount == 1) {
                    $('#scheme-id').prop('selectedIndex', 1);
                    $('#scheme-id').change();

                    SetPersonDropdownList();
                }
            });

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
        else
        {
            $('#general-ledger-change-info').addClass('d-none');
            prevGeneralLedgerId = $('#general-ledger-id option:selected').val();
        }
    }

    function SetPersonDropdownList()
    {
        let selectedSchemeId = $('#scheme-id').val();

        if (prevSchemeId != selectedSchemeId)
        {
            $('#person-id').prop('selectedIndex', -1);

            // Clear
            if (typeof prevSchemeId == 'undefined' || prevSchemeId == '0')
                $('#scheme-change-info').addClass('d-none');
            else
                $('#scheme-change-info').removeClass('d-none');

            // Set Page Input As Per Scheme Selection
            $.get('/AccountChildAction/GetSharesCapitalSchemeDetailBySchemeId', { _schemeId: selectedSchemeId, async: false }, function (data, textStatus, jqXHR)
            {
                if (data)
                {
                    // Call Input Setting As Per Selected Scheme
                    SetSchemeSetting(data);

                    if (prevSchemeId != 0)
                        $('#scheme-id-error').addClass('d-none');

                    prevSchemeId = selectedSchemeId;
                }
                else
                {
                    prevSchemeId = $('#scheme-id option:selected').val();
                }
            });

            // Get Person Dropdown
            $.get('/DynamicDropdownList/GetPersonDropdownListForSharesAccountOpening', { _schemeId: selectedSchemeId, async: false }, function (data, textStatus, jqXHR)
            { debugger
                personDropdownListData = data;
            });

            prevSchemeId = selectedSchemeId;

            // Clear Dependent Data
            $('#person-id').val('');
            addressDataTable.clear().draw();
            contactDataTable.clear().draw();
            jointAccountDataTable.clear().draw();
            nomineeDataTable.clear().draw();
        }
        else
        {
            $('#scheme-change-info').addClass('d-none');
            prevSchemeId = $('#scheme-id option:selected').val();
        }
    }

    function ResendSMS() {
        let mobileNumber = $('#field-value').val()

        $.get('/SMS/ReSendTeleVerificataionToken', { MobileNumber: mobileNumber, async: false }, function (data, textStatus, jqXHR) {
            if (data == 'success') {
                $(".link").fadeOut('slow').delay(30000).fadeIn("slow");
                $("#myToast").toast('show').css({ "z-index": "100", 'margin-top': "1%" });
            }
        });

    }

    // ###############   FUNCTIONS FOR NORMAL ACCORDION VALIDITY  

    // Sms Service Detail
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
    $('#btn-add-account-nominee-dt').click(function (event)
    {
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

        if (nominationNumber.length > 0)
        {
            maximumLength = parseInt($('#nomination-number').attr('maxlength'));

            if (parseInt(nominationNumber.length) > parseInt(maximumLength)) {
                result = false;
                $('#nomination-number-error').removeClass('d-none');
            }

            if (isDuplicateNomineeNumber === true) {
                result = false;
                $('#nomination-number-error').removeClass('d-none');
            }
        }
        else
        {
            result = false;
            $('#nomination-number-error').removeClass('d-none');
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

            if (isSelectedPersonInformationNumberForGuardian === false)
            {
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

                if (parseInt(guardianNomineeFullAddress.length) === 0 || guardianNomineeFullAddress === 'None' || parseInt(guardianNomineeFullAddress.length) > parseInt(maximumLength))
                {
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

                if (parseInt(guardianContactDetails.length) === 0 ||guardianContactDetails == 'None' || parseInt(guardianContactDetails.length) > parseInt(maximumLength)) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#guardian-nominee-contact-details-error').removeClass('d-none');
                }

                maximumLength = parseInt($('#trans-guardian-nominee-contact-details').attr('maxLength'));

                if (parseInt(transGuardianContactDetails.length) === 0 || transGuardianContactDetails == 'None' || parseInt(transGuardianContactDetails.length) > parseInt(maximumLength))
                {
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

            if (appointedTimeOfContact === '') 
            {
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
    $('#btn-add-contact-dt').click(function (event)
    {
        event.preventDefault();
        $('#btn-add-contact-modal').removeClass('read-only');
        $('#send-code').addClass('d-none');
        $('#resend').addClass('d-none');
        SetModalTitle('contact', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-contact-dt').click(function ()
    {
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
    $('#btn-add-contact-modal').click(function (event)
    {
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

    /// @@@@@@@@@@@@@@@@@@@@@@   Person  Address Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-person-address-dt').click(function (event)
    {
        event.preventDefault();
        editedAddressTypeId = '';
        SetAddressTypeUniqueDropdownList();
        SetModalTitle('person-address', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-person-address-dt').click(function ()
    {
        SetModalTitle('person-address', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
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
            $('#is-verified-address', myModal).prop('checked', columnValues[17].toString().toLowerCase() === 'true' ? true : false);
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
    $('#btn-add-person-address-modal').click(function (event)
    {
        if (IsValidAddressDataTableModal())
        {
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
    $('#btn-update-person-address-modal').click(function (event)
    {
        $('#select-all-person-address').prop('checked', false);
        if (IsValidAddressDataTableModal())
        {
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
    function IsValidAddressDataTableModal()
    {
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
        if (note == '')
            note = 'None';

        if (transNote == '')
            transNote = 'None';

        if (hasDivClass == true) {
            reasonForModification = 'None';
        }
        else {
            if (reasonForModification == '')
                reasonForModification = 'None';
        }


        //Validation Address Type
        if (addressType == '') {
            result = false;
            $('#address-type-id-error').removeClass('d-none')
        } else
            $('#address-type-id-error').addClass('d-none')

        //Validation FlatDoor No Min Length - 3 And Max Length = 50
        if (flatDoorNo == '' || parseInt(flatDoorNo.length) < 3 || parseInt(flatDoorNo.length) > 50) {
            result = false;
            $('#flat-door-no-error').removeClass('d-none');
        }
        else
            $('#flat-door-no-error').addClass('d-none');

        //Validation Trans FlatDoor No
        if (transFlatDoorNo == '') {
            result = false;
            $('#trans-flat-door-no-error').removeClass('d-none')
        }
        else
            $('#trans-flat-door-no-error').addClass('d-none')


        //Validation Building Name
        if (buildingName == '' || parseInt(buildingName.length) < 3 || parseInt(buildingName.length) > 100) {
            result = false;
            $('#building-name-error').removeClass('d-none');
        } else
            $('#building-name-error').addClass('d-none');


        //Validation Trans Building Name
        if (transBuildingName == '') {
            result = false;
            $('#trans-building-name-error').removeClass('d-none')
        } else
            $('#trans-building-name-error').addClass('d-none')

        //Validation Road Name
        if (roadName == '' || parseInt(roadName.length) < 3 || parseInt(roadName.length) > 100) {
            result = false;
            $('#road-name-error').removeClass('d-none');
        } else
            $('#road-name-error').addClass('d-none');

        //Validation Road Name
        if (transRoadName == '') {
            result = false;
            $('#trans-road-name-error').removeClass('d-none')
        } else
            $('#trans-road-name-error').addClass('d-none')


        //Validation Area Name
        if (areaName == '' || parseInt(areaName.length) < 3 || parseInt(areaName.length) > 100) {
            result = false;
            $('#area-name-error').removeClass('d-none');
        } else
            $('#area-name-error').addClass('d-none');

        //Validation Trans Area Name
        if (transAreaName == '') {
            result = false;
            $('#trans-area-name-error').removeClass('d-none')
        } else
            $('#trans-area-name-error').addClass('d-none')

        //Validation City
        if (city == '') {
            result = false;
            $('#city-id-error').removeClass('d-none');
        } else
            $('#city-id-error').addClass('d-none');

        //Validation Residence Type
        if (residenceType == '') {
            result = false;
            $('#residence-type-id-error').removeClass('d-none');
        } else
            $('#residence-type-id-error').addClass('d-none');

        //Validation Residence Ownership
        if (residenceOwnership == '') {
            result = false;
            $('#residence-ownership-id-error').removeClass('d-none')
        } else
            $('#residence-ownership-id-error').addClass('d-none')

        return result;

    }

    // Hide Unnecessary Columns
    function HideAddressDataTableColumns()
    {
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

    /// @@@@@@@@@@@@@@@@@@@@@@   Scheme Turn Over Limit  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

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

            activationDate = GetOnlyDate(columnValues[6]);
            expiryDate = GetOnlyDate(columnValues[7]);

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

    // @@@@@@@@@@@@@@@@@  End Of Income Detail Datatable Code
    //// Add down arrow icon for collapse element which is open by default
    $(".collapse.show").each(function () {
        $(this).prev(".card-header").find(".fa").addClass("fa-angle-down").removeClass("fa-angle-up");
    });

    // Toggle right and down arrow icon on show hide of collapse element
    $(".collapse").on('show.bs.collapse', function () {
        $(this).prev(".card-header").find(".fa").removeClass("fa-angle-up").addClass("fa-angle-down");
    }).on('hide.bs.collapse', function () {
        $(this).prev(".card-header").find(".fa").removeClass("fa-angle-down").addClass("fa-angle-up");
    });

    // Set All Required Default Values On Page Loading
    function SetPageLoadingDefaultValues(event)
    {
        let selectedSchemeId = $('#scheme-id').val();

        // Disalbe Events On Verify View
        if ($('#verify-view').length > 0)
            isVerifyView = true;

        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();

        // Select Default Record, If Dropdown Has Only One Record
        listItemCount = $('#business-office-id > option').not(':first').length;

        // Select Default First Record, If Dropdown Has Only One Record
        if (listItemCount == 1)
        {
            $('#business-office-id').prop('selectedIndex', 1);
            $('#business-office-id').change();

            SetGeneralLedgerDropdownList();
        }

        // Enable All Services Of SMS   
        if ($('#enable-credit-transaction-sms').is(':checked') && $('#enable-debit-transaction-sms').is(':checked') && $('#enable-insufficient-balance-sms').is(':checked'))
            $('#enable-all-service').prop('checked', true);
        else
            $('#enable-all-service').prop('checked', false);

        // Enable All Services Of EMAIL
        if ($('#enable-credit-transaction-email').is(':checked') && $('#enable-debit-transaction-email').is(':checked') && $('#enable-insufficient-balance-email').is(':checked') && $('#enable-statement-email').is(':checked'))
            $('#enable-all-email-service').prop('checked', true);
        else
            $('#enable-all-email-service').prop('checked', false);

        // Get Main Customer Person Dropdown
        // Get Person Dropdown
        $.get('/DynamicDropdownList/GetPersonDropdownListForSharesAccountOpening', { _schemeId: selectedSchemeId, async: false }, function (data, textStatus, jqXHR)
        {
            personDropdownListData = data;
        });

        // Get Person Dropdown For Joint Account
        $.get('/DynamicDropdownList/GetNonMemberPersonDropdownList', function (data)
        {
            personDropdownListDataForJointAccount = data;
        });

        // Get Person Dropdown For Nominee
        $.get('/DynamicDropdownList/GetPersonDropdownListForNominee', function (data)
        {
            personDropdownListDataForNominee = data;
        });

        // Get Person Dropdown For Guardian
        $.get('/DynamicDropdownList/GetPersonDropdownListForGuardian', function (data)
        {
            personDropdownListDataForGuardian = data;
        });

        // Check Whether Element Exist OR Not ***** Applicable For Only Amend
        if($('#person-id-value').length)
        {
            let personIdValueOnAmend = $('#person-id-value').attr('class').toString().replace('d-none', '');

            selectedPersonId = $('#person-id1').val();

            $('#person-id').val(personIdValueOnAmend);
        }
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function (event)
    {
        debugger;

        let isValidAllInputs = true;
         schemeId = $('#scheme-id option:selected').val();

        // Validate Inputs Of Full Page 
        if ($('form').valid())
        {
            // not add event.preventDefault
            $('.lastrow').remove();

            // Return List Object, Hence Create Array
            let jointAccountArray = new Array();
            let nomineeDetailArray = new Array();
            let contactDetailArray = new Array();
            let addressDetailArray = new Array();
            let turnOverLimitArray = new Array();
            let noticeScheduleArray = new Array();

            // To Get All Records From Data Table
            jointAccountDataTable.page.len(-1).draw();
            nomineeDataTable.page.len(-1).draw();
            contactDataTable.page.len(-1).draw();
            addressDataTable.page.len(-1).draw();
            turnOverLimitDataTable.page.len(-1).draw();
            noticeScheduleDataTable.page.len(-1).draw();

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

            // 5. Turn Over Limit - Create Array For Turn Over Limit Data Table To Pass Data
            if ($('#enable-turn-over-limit').is(':checked')) {
                if (turnOverLimitDataTable.data().any()) {
                    $('#turn-over-limit-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {
                        // Get Data Table Values In Turn Over Limit Array
                        $('#tbl-turn-over-limit > TBODY > TR').each(function () {
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

            // 6. SMS - Normal Accordion
            if (!IsValidSMSServiceDetailAccordionInputs())
                isValidAllInputs = false;

            // 7. Email - Normal Accordion
            if (!IsValidEmailServiceDetailAccordionInputs())
                isValidAllInputs = false;

            // Call Cantroller Save Data Table Method 
            if (isValidAllInputs) {
                $.ajax(
                    {
                        url: customerAccountDataTable,
                        type: 'POST',
                        dataType: 'json',
                        async: false,
                        data: { '_customerJointAccountHolder': jointAccountArray, '_customerAccountNominee': nomineeDetailArray, '_personContactDetail': contactDetailArray, '_personAddress': addressDetailArray, '_customerAccountTurnOverLimit': turnOverLimitArray, '_customerAccountNoticeSchedule': noticeScheduleArray },
                        ContentType: 'application/json;',

                        success: function (data) {

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
        }
        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });
});
