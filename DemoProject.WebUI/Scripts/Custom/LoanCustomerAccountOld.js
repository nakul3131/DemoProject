'use strict'
$(document).ready(function ()
{
    const DISABLE_VALUE = 'D';
    const FLAT_AMOUNT = 'F';
    const NO_ROUNDING = 'NOR';
    const LOAN_AGAINTS_PROPERTY = 'LAP';
    const LOAN_AGAINST_INSURENCE = 'LAI';
    const HOME_LOAN = 'HOME';
    const GOLD_LOAN = 'GOLD';
    const LOAN_AGAINST_MUTUAL_FUND = 'LAMF';
    const LOAN_AGAINST_FIXED_DEPOSITE = 'LAFD';
    const FLEXIBLE_LOAN = 'FLEXI';
    const VEHICLE_LOAN = 'VEHICLE';
    const PERSONAL_LOAN = 'PERSONAL';
    const SMALL_BUSINESS_LOAN = 'SBUSINESS';
    const EDUCATION_LOAN = 'EDUCATION';
    const CASH_CREDIT_LOAN = 'CC';
    const OVER_DRAFT_LOAN = 'OD';
    const CONSUMER_DURABLE_LOAN = 'CONS';

    const ADDRESS_TYPE_DROPDOWN_LIST = $('#address-type-id').html();

    // DropdownList Variables
    let personDropdownListData = '';
    let personDropdownListDataForJointAccount = '';
    let personDropdownListDataForNominee = '';
    let personDropdownListDataForGuardian = '';

    let documentDropdownList = '';

    // Array
    let finalDropdownListArray = [];

    // Count
    let dropDownListItemCount = 0;

    // Global Variable
    let result = true;
    let loan_Type = '';
    let minMaxResult = true;
    let requiredDocumentObj;
    let requiredDocumentArray = new Array();
    let listItemCount = 0;
    let dropdownListItems = '';
    let dataTableRecordCount = 0;
    let schemeData;
    let isUpdate = false;
    let prevPersonId = '';
    let prevLoanTypeId = '';
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

    // @@@@@@@@@@ Data Table Related Varible Declaration
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
    let document_Information;
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
    let personContactDetailPrmkey;
    let hasDivClass;
    let entryStatus;
    let isDuplicateContact = false;
    let isDuplicateSequenceNumber = false;
    let isDuplicateNomineeNumber = false;

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
    let guaranteeAmount = 0;

    //// Turn Over Limit
    //let frequency = '';
    //let frequencyText = '';
    //let transactionType = '';
    //let transactionTypeText = '';
    //let amount = 0;

    // NoticeSchedule
    let noticeTypeId = '';
    let noticeScheduleText = '';
    let communicationMediaId = '';
    let communicationMediaText = '';
    let scheduleId = '';
    let scheduleText = '';
    let filteredData = '';

    //Collateral Detail
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
    let marketValue = '';

    let isLimitedGuarantee;


    //Documen
    let storagePathId = '';
    let photoPath = '';
    let files;
    let photoinput;
    let photoDocumentId = '';
    let photoSrc;
    let isRequired;
    let storagePathInput = '';
    let dt;
    let j;
    let f;
    let documentId;
    let documentText = '';
    let photo;
    let photoPathDocument = '';
    let dochtml;
    let dochtml1;
    let fileCaption;
    let PrmKey = 0;
    let path;
    let i;
    let newID = '';
    let photoID = '';
    let editedDocumentId = '';

    let url = window.location.href;
    let index = url.lastIndexOf('/') + 1;
    let filenameWithExtension = url.substr(index);
    let filename = filenameWithExtension.split('?')[0]  // <-- added this line

    //  ************** Create Data Table  **************
    let addressDataTable = CreateDataTable('person-address');
    let contactDataTable = CreateDataTable('contact');
    let jointAccountDataTable = CreateDataTable('joint-account');
    let nomineeDataTable = CreateDataTable('account-nominee');
    //let turnOverLimitDataTable = CreateDataTable('turn-over-limit');
    let noticeScheduleDataTable = CreateDataTable('notice-schedule');
    let guarantorDetailDataTable = CreateDataTable('guarantor-detail');
    let goldLoanCollateralDetailDataTable = CreateDataTable('gold-collateral-detail');
    let preOwnedVehicleLoanPhotoDataTable = CreateDataTable('preOwned-vehicle-loan-photo');
    let goldLoanPhotoDataTable = CreateDataTable('gold-loan-photo');
    let documentDataTable = CreateDataTable('document');

    // Page Loading Default Values (Usually For Amend)
    SetPageLoadingDefaultValues();

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@


    // Sms Send - Display SMS Delilety Status
    $('#send-code').click(function (event) {
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
    $('#enable-all-service').click(function (event) {
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
    $('.enable-service').click(function (event) {
        if ($('#enable-credit-transaction-sms').is(':checked') && $('#enable-debit-transaction-sms').is(':checked') && $('#enable-insufficient-balance-sms').is(':checked'))
            $('#enable-all-service').prop('checked', true);
        else
            $('#enable-all-service').prop('checked', false);
    });

    // Enable All Services Of EMAIL 
    $('#enable-all-email-service').click(function (event) {
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
    $('.enable-email-service').click(function (event) {
        if ($('#enable-credit-transaction-email').is(':checked') && $('#enable-debit-transaction-email').is(':checked') && $('#enable-insufficient-balance-email').is(':checked') && $('#enable-statement-email').is(':checked'))
            $('#enable-all-email-service').prop('checked', true);
        else
            $('#enable-all-email-service').prop('checked', false);
    });

    //Business Office Id
    $('#business-office-id').focusout(function (event) {
        let businessOfficeId = $('#business-office-id option:selected').val();

        if (prevBusinessOfficeId != businessOfficeId) {
            if (prevBusinessOfficeId != 0)
                $('#business-office-id-error').addClass('d-none')

            prevBusinessOfficeId = businessOfficeId;

            // Clear Dependent Data
            $('#loan-type-id').prop('selectedIndex', -1);
            $('#scheme-id').prop("selectedIndex", -1);
            $('#loan-type-id').prop("selectedIndex", -1);
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
    });

    // On Changing Business Office, All Dependent Setting (Loan Type , Genreral Ledger, Scheme) Required To Be Clear.
    $('#loan-type-id').focusout(function (event) {
        debugger;
        SetGeneralLedgerDropdownList();
    });

    // On Changing General Ledger, All Dependent Setting (Scheme) Required To Be Clear.
    $('#general-ledger-id').focusout(function (event) {
        debugger
        SetSchemeDropdownList();
    });

    // On Changing Scheme, All Dependent Setting Required To Be Clear Or Change.
    $('#scheme-id').focusout(function (event) {
        debugger;
        SetPersonDropdownList();
    });

    // Person Autocomplete FocusOut Event
    $('#person-id').focusout(function (event) {
        debugger;
        $(this).val($(this).val().trim());
        SetPersonData();
    });

    // Joint Account Person Dropdown List FocusOut Event
    $('#person-id-joint-account-holder').focusout(function (event) {
        $(this).val($(this).val().trim());
    });

    // Clear Depended Inputs
    $('#account-opening-date').focusout(function (event) {
        $('#tenure').val('');
        $('#maturity-date').val('');
    });

    // Hide Guardian Details If User Input Nominee Birtdate As Adults
    $('#nominee-birth-date').focusout(function (event) {

        $.get('/PersonChildAction/GetAgeFromBirthDate', { _birthDate: $('#nominee-birth-date').val(), async: false }, function (data, textStatus, jqXHR) {
            if (data < 18)
                $('#heading-guardian').removeClass('d-none');
            else
                $('#heading-guardian').addClass('d-none');
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
    $('#nominee-person-id').focusout(function (event) {
        debugger;
        if ($('#nominee-person-id').val() == 0 || typeof $('#nominee-person-id').val() == null)
            $('#nomineedetails').removeClass('d-none');
        else {
            $('#nomineedetails').addClass('d-none');

            //let personInformationNumbers = $(this).val();
            $.get('/PersonChildAction/GetPersonCurrentAge', { _personInformationNumber: nomineePersonInformationNumber, async: false }, function (data, textStatus, jqXHR) {
                if (data <= 18)
                    $('#heading-guardian').removeClass('d-none');
                else
                    $('#heading-guardian').addClass('d-none');
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
    $('#field-value').focusout(function (event) {
        $(this).val($(this).val().trim());

        // If Contact Type Is Mobile
        if (isMobile) {
            $('#verification-code').val('');

            if ($('#field-value').val().length == 10) {
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
            $('.is-verified-field').addClass('d-none');
            $('#send-code').removeClass('d-none');
            $('.verification-code').removeClass('d-none');
        }
        else {
            $('#field-value').removeAttr('type');
            $('#send-code').addClass('d-none');
            $('.is-verified-field').removeClass('d-none');
            $('#resend').addClass('d-none');
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
            $('#person-id1').val(ui.item.valueId);
            $('#person-id').val(ui.item.label);
            selectedPersonId = ui.item.valueId;
            selectedPersonText = ui.item.label;
        }
    }).focus(function (event, ui) {
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
                    if (finalDropdownListArray[dropDownListItemCount].Text === jointAccountPersonId.Text)
                        // splice - remove item from array at a choosen index
                        finalDropdownListArray.splice(dropDownListItemCount, 1);
                }
            }
        }

        $(this).autocomplete('search');
    })

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
        guardianNomineePersonInformationNumber = '';
        guardianNomineePersonInformationNumberText = '';

        // Assign Array Without Reference  *** Use Slice Method
        finalDropdownListArray = personDropdownListDataForGuardian.slice();

        $(this).autocomplete('search');
    });


    //For Enable Document
    $('#enable-document-upload-in-db').change(function () {
        //$('#collapse-gold-loans span').html('');
        if ($(this).is(':checked')) {
            $('#document-allowed-file-format-for-db').prop('disabled', false);
            $('#maximum-file-size-document-in-db').addClass('mandatory-mark');
            $('#maximum-file-size-document-in-db').removeClass('read-only');
            $('#maximum-file-size-document-in-db').prop('min', 1);
            $('#maximum-file-size-document-in-db').val('');
            $('#maximum-file-size-document-in-local-storage').attr('min', 0);
            $('#maximum-file-size-document-in-local-storage').val('0');
            $('#maximum-file-size-document-in-local-storage').addClass('read-only');
            $('#maximum-file-size-document-in-local-storage').removeClass('mandatory-mark');
            $('#document-allowed-file-format-for-local-storage').prop('disabled', true);
            $('#document-allowed-file-format-for-local-storage > option').prop('selected', false);
            documentFileFormatLocal.trigger('change');
            $('#document-local-storage-path').val('None');
            $('#document-local-storage-path').addClass('read-only');
            $('.document-upload-ls').addClass('read-only');
        }
        else {
            $('#document-allowed-file-format-for-db > option').prop('selected', false);
            documentFileFormatDb.trigger('change');
            $('#document-allowed-file-format-for-db').prop('disabled', true);
            $('#maximum-file-size-document-in-db').removeClass('mandatory-mark');
            $('#maximum-file-size-document-in-db').addClass('read-only');
            $('#maximum-file-size-document-in-db').prop('min', 0);
            $('#maximum-file-size-document-in-db').val('0');
            $('#maximum-file-size-document-in-local-storage').attr('min', 0);
            $('#maximum-file-size-document-in-local-storage').val('0');
            $('#maximum-file-size-document-in-local-storage').addClass('read-only');
            $('#document-allowed-file-format-for-local-storage').prop('disabled', true);
            $('#document-allowed-file-format-for-local-storage > option').prop('selected', false);
            documentFileFormatLocal.trigger('change');
            $('#document-local-storage-path').val('None');
            $('#document-local-storage-path').addClass('read-only');
            $('.document-upload-db').removeClass('read-only');
            $('.document-upload-ls').removeClass('read-only');
        }
    });

    //For Enable Document
    $('#enable-document-upload-in-ls').change(function () {
        //$('#collapse-gold-loans span').html('');
        if ($(this).is(':checked')) {
            $('#maximum-file-size-document-in-local-storage').attr('min', 1);
            $('#maximum-file-size-document-in-local-storage').val('');
            $('#maximum-file-size-document-in-local-storage').removeClass('read-only');
            $('#maximum-file-size-document-in-local-storage').addClass('mandatory-mark');
            $('#document-allowed-file-format-for-local-storage').prop('disabled', false);
            $('#document-local-storage-path').removeClass('read-only');
            $('#document-local-storage-path').val('');
            $('#document-allowed-file-format-for-db > option').prop('selected', false);
            documentFileFormatDb.trigger('change');
            $('#document-allowed-file-format-for-db').prop('disabled', true);
            $('#maximum-file-size-document-in-db').removeClass('mandatory-mark');
            $('#maximum-file-size-document-in-db').addClass('read-only');
            $('#maximum-file-size-document-in-db').prop('min', 0);
            $('#maximum-file-size-document-in-db').val('0');
            $('.document-upload-db').addClass('read-only');
        }
        else {
            $('#document-allowed-file-format-for-db').prop('disabled', true);
            $('#document-allowed-file-format-for-db').prop('selected', false);
            documentFileFormatDb.trigger('change');
            $('#maximum-file-size-photo-in-db').removeClass('mandatory-mark');
            $('#maximum-file-size-photo-in-db').addClass('read-only');
            $('#maximum-file-size-photo-in-db').prop('min', 0);
            $('#maximum-file-size-photo-in-db').val('0');
            $('#maximum-file-size-document-in-local-storage').attr('min', 0);
            $('#maximum-file-size-document-in-local-storage').val('0');
            $('#maximum-file-size-document-in-local-storage').addClass('read-only');
            $('#document-allowed-file-format-for-local-storage').prop('disabled', true);
            $('#document-allowed-file-format-for-local-storage > option').prop('selected', false);
            documentFileFormatLocal.trigger('change');
            $('#document-local-storage-path').val('None');
            $('#document-local-storage-path').addClass('read-only');
            $('.document-upload-db').removeClass('read-only');
            $('.document-upload-ls').removeClass('read-only');
        }
    });

    //  ************** Accordion Input Validation  **************

    // SMS Service Detail Input Validation
    $('.sms-service-input').focusout(function () {
        if (IsValidSMSServiceDetailAccordionInputs())
            $('#sms-service-accordion-error').addClass('d-none');
    });

    // Email Service Detail Input Validation
    $('.email-service-input').focusout(function () {
        if (IsValidEmailServiceDetailAccordionInputs())
            $('#email-service-accordion-error').addClass('d-none');
    });

    $('.preOwned-vehicle-loan-inspection-input').focusout(function () {
        debugger;
        IsValidPreOwnedVehicleLoanInspectionAccordionInputs()
    });

    $('.preOwned-vehicle-loan-inspection-input').focusout(function () {
        debugger;
        IsValidPreOwnedVehicleLoanInspectionAccordionInputs()
    });

    $('.loan-collateral-detail').focusout(function () {
        debugger;
        IsValidVehicleLoanCollateralDetailAccordionInputs();
    });

    $('.loan-economics-detail-input').focusout(function () {
        debugger;
        IsValidCustomerVehicleLoanEconomicDetailAccordionInputs();

    });

    $('.vehicle-insurance-detail-input').focusout(function () {
        debugger;
        IsValidVehicleInsuranceDetailAccordionInputs();

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

            // Adjust the date if it exceeds the last day of the month
            if (maturityDate.getDate() !== (startDate.getDate() + days) % 31)
                maturityDate.setDate(0);

            // Set Maximum Mature Date
            startDate.setDate(parseInt(startDate.getDate()) + parseInt(maximumTenure));

            $('#maturity-date').attr('max', GetInputDateFormat(startDate));

            // Format the new maturity date to YYYY-MM-DD and set it in the input field
            $('#maturity-date').val(GetInputDateFormat(maturityDate));
        }
    }

    // Focusout in  SetTenure  Function
    $('#account-opening-date, #maturity-date').focusout(SetTenure);

    //Focusout in  SetMaturityDate
    $('.tenure').focusout(SetMaturityDate);

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

    // Variable to keep track of previously missing required documents
    let previouslyMissingDocs = [];

    // Initialize a variable to store the previous valid file state
    let previousFile =
        {
        name: '',
        src: ''
    };

    // Event listener for change event on file input element
    $('#photo-path-document').change(function () {
        debugger;
        const fileType = 'PhotoPathDocument';
        let document_Name = $('#document-id option:selected').text();
        const previewId = '#photo-path-document-image-preview';
        const imgPreview = $(previewId);
        validateFile(this, imgPreview, fileType, document_Name);
    });

    // Function to validate the uploaded file
    function validateFile(input, imgPreview, fileType, document_Name) {
        debugger;
        let result = true;
        let storagePath = input.value;

        if (storagePath === '') {
            alert("Please upload an image");
            $('#photo-sign-accordion-error').removeClass('d-none');
            return false;
        }

        let schemeId = $('#scheme-id').val();
        if (schemeId <= 0) {
            alert("Please select Deposit Type");
            $('#document-error').removeClass('d-none');
            return false;
        }

        let maxFileSize, validFileFormats;

        if (schemeData) {
            debugger;
            if (fileType === "PhotoPathDocument") {
                let documentInformation = schemeData.SchemeDocumentViewModel;
                let documentFound = false;
                documentInformation.forEach(doc => {
                    debugger;
                    if (document_Name === doc.NameOfDocument) {
                        documentFound = true;
                        maxFileSize = doc.EnableDocumentUploadInLocalStorage
                            ? doc.MaximumFileSizeForDocumentUploadInLocalStorage
                            : doc.MaximumFileSizeForDocumentUploadInDb;
                        validFileFormats = doc.EnableDocumentUploadInLocalStorage
                            ? doc.DocumentAllowedFileFormatsForLocalStorage
                            : doc.DocumentAllowedFileFormatsForDb;
                    }
                });
                if (!documentFound) {
                    alert("Selected Document Not Found In Scheme Data.");
                    $('#document-error').removeClass('d-none');
                    return false;
                }
            }
        } else {
            alert("Scheme data not loaded properly.");
            $('#document-error').removeClass('d-none');
            return false;
        }

        if (fileType && input.files.length === 0) {
            alert("Please select a file");
            $('#photo-sign-accordion-error').removeClass('d-none');
            return false;
        }

        let file = input.files[0];
        let fileExtension = file.name.split('.').pop().toLowerCase();
        let validExtensions = validFileFormats.split(',').map(ext => ext.trim().toLowerCase());
        let errorMessage = "";

        if (fileType && !validExtensions.includes(fileExtension)) {
            errorMessage = "Invalid file format. Allowed formats are: " + validFileFormats;
        } else if (fileType && (file.size / 1024) >= maxFileSize) {
            errorMessage = "File size exceeds the maximum allowed size of " + maxFileSize + " KB";
        }

        if (errorMessage) {
            $('#photo-path-document-error').removeClass('d-none').text(errorMessage);
            $('#document-error').removeClass('d-none');

            if (previousFile && previousFile.src && previousFile.name) {
                imgPreview.attr('src', previousFile.src);
            } else {
                console.error('previousFile is not valid');
            }
            return false;
        } else {
            $('#photo-path-document-error').addClass('d-none').text("");
            $('#document-error').addClass('d-none').text("");

            let reader = new FileReader();
            reader.onload = function (e) {
                imgPreview.attr('src', e.target.result);
            };
            reader.readAsDataURL(file);

            // Update previous valid file details only if the new file is valid
            previousFile.name = file.name;
            previousFile.src = imgPreview.attr('src');

            // Display the selected file name
            $('#photo-path-document').text(file.name);

            return true;
        }
    }

    // Get Selected Scheme For Other Than Create Operation
    let selectedSchemeId = $('#scheme-id option:selected').val();

    // Set Page Data According To Selected Scheme
    if (selectedSchemeId != '') {
        $.get('/AccountChildAction/GetLoanSchemeDetailBySchemeId', { _schemeId: selectedSchemeId, async: false }, function (data, textStatus, jqXHR) {
            if (data) {
                debugger;
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
    function SetSchemeSetting(data) 
    {
        // Tenure
        if (data.SchemeAccountParameterViewModel.EnableTenure)
        {
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
                }
            });
        }

        if (data.SchemeAccountParameterViewModel.EnableTenureList)
            tenure = parseInt(data.SchemeTenureListViewModel.Tenure);

        // Auto Account Number
        if (data.SchemeAccountParameterViewModel.EnableAutoAccountNumber)
            $('#acc-number-grp').addClass('d-none');
        else
            $('#acc-number-grp').removeClass('d-none');

        // Account Number2
        if (data.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 == true) 
            $('#account-number2').removeClass('d-none');      
        else
        {
            $('#account-number2').addClass('d-none');
            $('#account-number2').val('');
        }

        // $.grep() - Nothing But Work As Filter; It Return Data Only Meet A Condition
        requiredDocumentObj = $.grep(data.SchemeDocumentViewModel, function (element) { return element.IsRequired });

        // map() - creates a new array from calling a function for every array element.
        //          - does not execute the function for empty elements.
        //          - does not change the original array.
        requiredDocumentArray = requiredDocumentObj.map(function (id) { return id.DocumentId; });

        //AccountNumber3
        if (data.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 == true) {
            $('#account-number3').removeClass('d-none');
        }
        else
        {
            $('#account-number3').addClass('d-none');
            $('#account-number3').val('');
        }

        // Document Upload
        if (data.SchemeAccountParameterViewModel.EnableDocumentUpload)
            $('.document').removeClass('d-none');
        else
            $('.document').addClass('d-none');

        // Passbook
        if (data.SchemeAccountParameterViewModel.EnablePassbookDetail)
            $('.passbook-number-field').addClass('d-none');
        else
            $('.passbook-number-field').removeClass('d-none');

        // Enable Collateral
        if (data.SchemeLoanAccountParameterViewModel.EnableCollateral == false)
            $('.gold-loan-collateral-detail').addClass('d-none');
        else
            $('.gold-loan-collateral-detail').removeClass('d-none');

        // Enable Field Investigation
        if (data.SchemeLoanAccountParameterViewModel.EnableFieldInvestigation == false)
            $('.field-investigation').addClass('d-none');
        else
            $('.field-investigation').removeClass('d-none');

        // Enable Guarantor Detail
        if (data.SchemeLoanAccountParameterViewModel.EnableGuarantorDetail == false)
            $('.guarantor-detail').addClass('d-none');
        else
            $('.guarantor-detail').removeClass('d-none');

        // Nominee
        if (data.SchemeAccountParameterViewModel.MaximumNominee == 0) 
            $('.account-nominee').addClass('d-none');
        else
        {
            $('.account-nominee').removeClass('d-none');
            minimumNominee = data.SchemeAccountParameterViewModel.MinimumNominee;
            maximumNominee = data.SchemeAccountParameterViewModel.MaximumNominee;
        }

        // Joint Account
        if (data.SchemeAccountParameterViewModel.MaximumJointAccountHolder == 0)
            $('.joint-account').addClass('d-none');
        else
        {
            $('.joint-account').removeClass('d-none');
            minimumJointAccountHolder = data.SchemeAccountParameterViewModel.MinimumJointAccountHolder;
            maximumJointAccountHolder = data.SchemeAccountParameterViewModel.MaximumJointAccountHolder;
        }

        // Guarantors
        if (data.SchemeLoanAccountParameterViewModel.MaximumNumberOfGuarantors == 0)
            $('.guarantor-detail').addClass('d-none');
        else
        {
            $('.guarantor-detail').removeClass('d-none');
            minimumNumberOfGuarantor = data.SchemeLoanAccountParameterViewModel.MinimumNumberOfGuarantors;
            maximumNumberOfGuarantor = data.SchemeLoanAccountParameterViewModel.MaximumNumberOfGuarantors;
        }

        let sanctionAmountMin = data.SchemeLoanAccountParameterViewModel.MinimumLoanAmountForIndividual;
        let sanctionAmountMax = data.SchemeLoanAccountParameterViewModel.MaximumLoanAmountForIndividual;

        $('#sanction-amount').attr({ 'min': sanctionAmountMin, 'max': sanctionAmountMax }).val(sanctionAmountMin);

         let rateOfInterestMin = data.SchemeLoanInterestParameterViewModel.MinimumInterestRate;
         let rateOfInterestMax = data.SchemeLoanInterestParameterViewModel.MaximumInterestRate;

          $('#rate-of-interest').attr({ 'min': rateOfInterestMin, 'max': rateOfInterestMax });

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
            $('#heading-notice-schedule').addClass('d-none');
        else
            $('#heading-notice-schedule').removeClass('d-none');

        // Notice Schedule
        if (data.SchemeAccountParameterViewModel.EnableSmsService == false && data.SchemeAccountParameterViewModel.EnableEmailService == false)
            $('#heading-notice-schedule').addClass('d-none');
        else
            $('#heading-notice-schedule').removeClass('d-none');

        // Call Document Dropdown List
        SetDocumentDropdownList();
    }

    function SetPersonData()
    {
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
                        ]).draw();

                        rowNum++;

                        row.nodes().to$().attr('id', 'tr' + rowNum);

                        contactDataTable.column(1).visible(false);
                        contactDataTable.column(7).visible(false);
                        contactDataTable.column(8).visible(false);

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
                    ]).draw();

                    rowNum++;
                    row.nodes().to$().attr('id', 'tr' + rowNum);

                    addressDataTable.column(1).visible(false);
                    addressDataTable.column(11).visible(false);
                    addressDataTable.column(13).visible(false);
                    addressDataTable.column(15).visible(false);
                    addressDataTable.column(20).visible(false);
                    addressDataTable.column(21).visible(false);

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

    //Set Deposite Type Setting
    function SetLoanTypeSetting()
    {
        // Mark Out Select All Check Box Of All Datatables.
        $('input[name="select-all"]').prop('checked', false);

        // Clear Accordion Title Error Messages
        $('.accordion-title-error').addClass('d-none');

        // Make False All Toggle Switch
        $('.switch-input').prop('checked', false);


        $('.sms-service-input').val('');
        $('.email-service-input').val('');

        let loanTypeId = $('#loan-type-id option:selected').val();
        // Assuming nameOfScheme is defined somewhere
        $.get('/AccountChildAction/GetLoanTypeSysNameByLoanTypeId', { _loanTypeId: loanTypeId, async: false }, function (data, textStatus, jqXHR) {
            debugger;
            let loan_Type = data
            debugger;
            if (loan_Type == GOLD_LOAN || loan_Type == LOAN_AGAINST_INSURENCE || loan_Type == LOAN_AGAINST_MUTUAL_FUND || loan_Type == LOAN_AGAINST_FIXED_DEPOSITE) {
                $('.gold-insurance-mutual-deposit-loan').addClass('d-none');
            }

            if (loan_Type == GOLD_LOAN) {
                $('.gold-loan-accordian').removeClass('d-none');
            } else {
                $('.gold-loan-accordian').addClass('d-none');
            }

            if (loan_Type == GOLD_LOAN || loan_Type == LOAN_AGAINST_INSURENCE)
                $('.account-guarantor-detail').addClass('d-none');
            else
                $('.account-guarantor-detail').removeClass('d-none');


            if (loan_Type == VEHICLE_LOAN) {
                $('.vehicle-loan').removeClass('d-none');
            } else {
                $('.vehicle-loan').addClass('d-none');
            }

            if (loan_Type == CASH_CREDIT_LOAN) {
                debugger;
                $('.cash-credit-loan').removeClass('d-none');
                $('.cash-credit-loan-hide').addClass('d-none');
            } else {
                $('.cash-credit-loan').addClass('d-none');
                $('.cash-credit-loan-hide').removeClass('d-none');
            }

            if (loan_Type == HOME_LOAN || loan_Type == LOAN_AGAINTS_PROPERTY || loan_Type == VEHICLE_LOAN || loan_Type == PERSONAL_LOAN || loan_Type == SMALL_BUSINESS_LOAN || loan_Type == FLEXIBLE_LOAN || loan_Type == EDUCATION_LOAN) {
                $('.guarantor-detail').addClass('d-none');
                $('.collateral').addClass('d-none');
                $('.home-property-vehicle-personal-business-flexi-education-loan').addClass('d-none');
            }
            if (loan_Type == GOLD_LOAN || loan_Type == LOAN_AGAINST_INSURENCE || loan_Type == LOAN_AGAINST_MUTUAL_FUND || loan_Type == LOAN_AGAINST_FIXED_DEPOSITE) {
                $('.guarantor-detail').addClass('d-none');
                $('.collateral').addClass('d-none');
                $('.gold-insurance-mutual-deposite-loan').addClass('d-none');
            }
        });
    }

    function SetGeneralLedgerDropdownList()
    {
        let loanTypeId = $('#loan-type-id option:selected').val();
        let businessOfficeId = $('#business-office-id option:selected').val();

        if (prevLoanTypeId != loanTypeId)
        {
            $.get('/DynamicDropdownList/GetLoanGeneralLedgerDropdownListByBusinessOfficeId', { _businessOfficeId: businessOfficeId, _loanTypeId: loanTypeId, async: false }, function (data)
            {
                dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000">Select General Ledger</option>';

                $.each(data, function (index, selectListItemObj)
                {
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

                    prevLoanTypeId = $('#loan-type-id option:selected').val();
                }
            });

            if (prevLoanTypeId != '')
                $('#loan-type-id-error').addClass('d-none')

            prevLoanTypeId = loanTypeId;

            SetLoanTypeSetting();

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
            $('#loan-type-id-error').removeClass('d-none');
            prevLoanTypeId = $('#loan-type-id option:selected').val();
        }
    }

    function SetSchemeDropdownList()
    {
        debugger
        let selectedGeneralLedgerId = $('#general-ledger-id option:selected').val();

        if (prevGeneralLedgerId != selectedGeneralLedgerId) {
            // Clear
            if (prevGeneralLedgerId != '0')
                $('#general-ledger-change-info').removeClass('d-none');

            // Set Scheme Dropdown List Based On Selected General Ledger
            $.get('/DynamicDropdownList/GetSchemeDropdownListByGeneralLedger', { _generalLedgerId: selectedGeneralLedgerId, async: false }, function (data, textStatus, jqXHR) {
                debugger;
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

    function SetPersonDropdownList() {
        debugger;
        schemeId = $('#scheme-id').val();

        if (prevSchemeId != schemeId) {
            $('#person-id').val('');

            // Input Visiblity Base On Selected Scheme
            $.get('/AccountChildAction/GetLoanSchemeDetailBySchemeId', { _schemeId: schemeId, async: false }, function (data) {
                if (data) {
                    debugger;
                    SetSchemeSetting(data);
                    schemeData = data;

                    $('#scheme-id-error').addClass('d-none');
                }
                else
                    $('#scheme-id-error').removeClass('d-none');
            });

            // Set PersonDropdownList Based On Scheme
            $.get('/DynamicDropdownList/GetPersonDropdownListBySchemeId', { _schemeId: schemeId, async: false }, function (data) {
                personDropdownListData = data;
            });



            if (prevSchemeId != 0)
                $('#scheme-id-error').addClass('d-none');

            prevSchemeId = schemeId;
        }
        else {
            $('#scheme-id-error').removeClass('d-none');
            prevSchemeId = $("#scheme-id option:selected").val();
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

    function SetDocumentDropdownList()
    {
        let schemeId = $('#scheme-id option:selected').val();

        let text = '';
        let classText = '';

        debugger;
        $.get('/DynamicDropdownList/GetDocumentDropdownListBySchemeId', { _schemeId: schemeId, async: false }, function (data)
        {
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000">Select Valid Document</option>';
            debugger;

            $.each(data, function (index, selectListItemObj)
            {
                debugger;

                // jquery's inArray operator used to get keys in an object.
                // Use '|' Mandatory Marks To Indicate Required
                if ($.inArray(selectListItemObj.Value, requiredDocumentArray) > -1)
                {
                    text = '| ' + selectListItemObj.Text;
                    classText = 'text-danger';
                }
                else
                {
                    text = selectListItemObj.Text;
                    classText = '';
                }

                dropdownListItems += '<option class="' + classText + ' ' + '" value="' + selectListItemObj.Value + '">' + text + '</option>';
            });

            $('#document-id').html(dropdownListItems);

            documentDropdownList = $('#document-id').html();

            listItemCount = $('#document-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount == 1)
            {
                $('#document-id').prop('selectedIndex', 1);
                $('#document-id').change();
            }
        });
    }

    // ###############   FUNCTIONS FOR NORMAL ACCORDION VALIDITY  

    // Sms Service Detail
    function IsValidSMSServiceDetailAccordionInputs()
    {
        result = true;

        if (!$('#heading-customer-account-sms-service').hasClass('d-none'))
        {
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

        if (!$('#heading-customer-account-email-service').hasClass('d-none')) {
            if (IsValidInputDate('#activation-date-email') == false || IsValidInputDate('#expiry-date-email') == false || $('.statement-frequency:checked').next('label').text() == '')
                result = false;
        }

        if (result)
            $('#email-service-accordion-error').addClass('d-none');
        else
            $('#email-service-accordion-error').removeClass('d-none');

        return result;
    }

    //Vehicle Loan Collateral Detail
    function IsValidVehicleLoanCollateralDetailAccordionInputs() {
        debugger;

        let loanPurpose = $('.loan-purpose:checked').next('label').text();
        let manufactureYear = $('#manufacture-year').val();
        let registrationDate = $('#registration-date').val();
        let registrationNumber = $('#registration-number').val();
        let exShowroomPrice = parseFloat($('#ex-showroom-price').val());
        let onroadPrice = parseFloat($('#onroad-price').val());
        let additionalAccessoriesAmount = parseFloat($('#additional-accessories-amount').val());
        let engineNumber = $('#engine-number').val();
        let chasisNumber = $('#chasis-number').val();
        let colourOfBody = $('#colour-of-body').val();
        let numberOfTyresAxel = parseInt($('#number-of-tyres-axel').val());
        let roadTaxDueDate = $('#road-tax-due-date').val();
        let fitnessCertificateValidUpto = $('#fitness-certificate-valid-upto').val();

        let permitType = $('.permit-type:checked').next('label').text()
        let permitDetails = $('#permit-details').val();
        let registeredLadenWeight = $('#registered-laden-weight').val();
        let transportationBusinessExperience = $('#transportation-business-experience').val();
        let goodsCarriedIndustry = $('#goods-carried-industry').val();
        let contractingCompanyAddressDetails = $('#contracting-company-address-details').val();
        let seatingCapacity = $('#seating-capacity').val();
        let loanAmount = $('#loan-amount').val();

        result = true;
        if (!$('#heading-loan-collateral-detail').hasClass('d-none')) {

            if ($(loanPurpose != '' && $('#vehicle-supplier-id').val() && $('#vehicle-variant-id').val() && manufactureYear != '' && $('#registration-date').val() != ''
                && registrationNumber != '' && exShowroomPrice != '' && onroadPrice != '' && additionalAccessoriesAmount != '' && engineNumber != '' && chasisNumber != ''
                && colourOfBody != '' && numberOfTyresAxel != '' && $('#road-tax-due-date').val() != '' && $('#fitness-certificate-valid-upto').val() != ''
                && permitType != '' && permitDetails != '' && $('#permit-expiry-date').val() != '' && registeredLadenWeight != '' && transportationBusinessExperience != '' && goodsCarriedIndustry != ''
                && contractingCompanyAddressDetails != '' && seatingCapacity != '' && loanAmount != '')) {

                // Get the current year
                let currentYear = new Date().getFullYear();

                // Calculate the minimum allowed year
                let minAllowedYear = currentYear - 30;

                let allowedYears = [];
                for (let i = minAllowedYear; i <= currentYear; i++) {
                    allowedYears.push(i.toString());
                }

                if (manufactureYear == '' || !allowedYears.includes(manufactureYear)) {
                    result = false;
                }

                // Regular expression for validation
                let regex = /^[A-Z]{2}\d{1,2}[A-Z]{1,2}\d{4}$/;

                // Check if registrationNumber is empty or doesn't match the regex
                if (registrationNumber === '' || !regex.test(registrationNumber)) {
                    result = false;
                    $('#registration-number-error').removeClass('d-none');
                } else
                    $('#registration-number-error').addClass('d-none');

                //Ex Showroom Price
                minimum = parseFloat($('#ex-showroom-price').attr('min'));
                maximum = parseFloat($('#ex-showroom-price').attr('max'));

                if (parseFloat(exShowroomPrice) < parseFloat(minimum) || parseFloat(exShowroomPrice) > parseFloat(maximum))
                    result = false;

                //Onroad Price
                minimum = parseFloat($('#onroad-price').attr('min'));
                maximum = parseFloat($('#onroad-price').attr('max'));

                if (parseFloat(onroadPrice) < parseFloat(minimum) || parseFloat(onroadPrice) > parseFloat(maximum))
                    result = false;

                //Additional Accessories Amount
                minimum = parseFloat($('#additional-accessories-amount').attr('min'));
                maximum = parseFloat($('#additional-accessories-amount').attr('max'));

                if (parseFloat(additionalAccessoriesAmount) < parseFloat(minimum) || parseFloat(additionalAccessoriesAmount) > parseFloat(maximum))
                    result = false;

                // Engine Number
                if (engineNumber == '' || engineNumber.length > 50)
                    result = false;

                // chasis Number
                if (chasisNumber == '' || chasisNumber.length > 50)
                    result = false;

                // colourOfBody
                if (colourOfBody == '' || colourOfBody.length > 50)
                    result = false;


                // number Of Tyres Axel
                minimum = parseInt($('#number-of-tyres-axel').attr('min'));
                maximum = parseInt($('#number-of-tyres-axel').attr('max'));

                if (parseInt(numberOfTyresAxel) < parseInt(minimum) || parseInt(numberOfTyresAxel) > parseInt(maximum))
                    result = false;

                if (roadTaxDueDate < registrationDate) {
                    result = false;
                    $('#road-tax-due-date-error').removeClass('d-none');
                } else {
                    $('#road-tax-due-date-error').addClass('d-none');
                }

                if (fitnessCertificateValidUpto < roadTaxDueDate) {
                    result = false;
                    $('#fitness-certificate-valid-upto-error').removeClass('d-none');
                } else {
                    $('#fitness-certificate-valid-upto-error').addClass('d-none');
                }

                // permit Details
                if (permitDetails == '' || permitDetails.length > 1500)
                    result = false;

                // contracting Company Address Details
                if (contractingCompanyAddressDetails == '' || contractingCompanyAddressDetails.length > 500)
                    result = false;

            } else {
                result = false;
            }
        } if (result)
            $('#loan-collateral-detail-error').addClass('d-none');
        else
            $('#loan-collateral-detail-error').removeClass('d-none');

        return result;
    }

    // VehicleInsuranceDetail
    function IsValidVehicleInsuranceDetailAccordionInputs() {

        debugger;
        let policyNumber = parseInt($('#policy-number').val());
        let typeOfCoverage = $('.type-of-coverage:checked').next('label').text();
        let sumInsured = parseInt($('#sum-insured').val());

        result = true;
        if (!$('#heading-vehicle-insurance-detail').hasClass('d-none')) {
            if ($('#insurance-company-id').val() != '' && $('#commencement-date').val() != '' && isNaN(policyNumber) == false && typeOfCoverage != '' && isNaN(sumInsured) == false) {

                minimum = parseInt($('#policy-number').attr('min'));
                maximum = parseInt($('#policy-number').attr('max'));

                if (parseInt(policyNumber) < parseInt(minimum) || parseInt(policyNumber) > parseInt(maximum))
                    result = false;


                minimum = parseInt($('#sum-insured').attr('min'));
                maximum = parseInt($('#sum-insured').attr('max'));

                if (parseInt(sumInsured) < parseInt(minimum) || parseInt(sumInsured) > parseInt(maximum))
                    result = false;

            } else {
                result = false;
            }
        }
        if (result)
            $('#vehicle-insurance-detail-error').addClass('d-none');
        else
            $('#vehicle-insurance-detail-error').removeClass('d-none');

        return result;
    }

    // CustomerVehicleLoanEconomicDetail
    function IsValidCustomerVehicleLoanEconomicDetailAccordionInputs() {

        let costOfVehicle = parseFloat($('#cost-of-vehicle').val());
        let downPayment = parseFloat($('#down-payment').val());
        let bodyFabricationCostForChassis = parseFloat($('#body-fabrication-cost-for-chassis').val());
        let registrationCharges = parseFloat($('#registration-charges').val());
        let roadTaxAmount = parseFloat($('#road-tax-amount').val());
        let permitAmount = parseFloat($('#permit-amount').val());
        let insuranceAmount = parseFloat($('#insurance-amount').val());
        let miscellaneousAmount = parseFloat($('#miscellaneous-amount').val());
        let loanEMI = parseFloat($('#loan-EMI').val());
        let earningPerMonth = parseFloat($('#earning-per-month').val());
        let taxPerMonth = parseFloat($('#tax-per-month').val());
        let permitAmountPerMonth = parseFloat($('#permit-amount-per-month').val());
        let insuranceAmountPerMonth = parseFloat($('#insurance-amount-per-month').val());
        let driverSalaryPerMonth = parseFloat($('#driver-salary-per-month').val());
        let helperSalaryPerMonth = parseFloat($('#helper-salary-per-month').val());
        let other1ExpensesPerMonth = parseFloat($('#other1-expenses-per-month').val());
        let other2ExpensesPerMonth = parseFloat($('#other2-expenses-per-month').val());
        let other3ExpensesPerMonth = parseFloat($('#other3-expenses-per-month').val());
        let fuelExpensesPerMonth = parseFloat($('#fuel-expenses-per-month').val());
        let tyreExpensesPerMonth = parseFloat($('#tyre-expenses-per-month').val());
        let maintenanceExpensesPerMonth = parseFloat($('#maintenance-expenses-per-month').val());
        let miscellaneousExpensesPerMonth = parseFloat($('#miscellaneous-expenses-per-month').val());

        result = true;
        if (!$('#heading-vehicle-loan-economics-detail').hasClass('d-none')) {

            if (isNaN(costOfVehicle) == false && isNaN(downPayment) == false && isNaN(bodyFabricationCostForChassis) == false && isNaN(registrationCharges) == false
                && isNaN(roadTaxAmount) == false && isNaN(permitAmount) == false && isNaN(insuranceAmount) == false && isNaN(miscellaneousAmount) == false && isNaN(loanEMI) == false
                && isNaN(earningPerMonth) == false && isNaN(taxPerMonth) == false && isNaN(permitAmountPerMonth) == false && isNaN(insuranceAmountPerMonth) == false
                && isNaN(driverSalaryPerMonth) == false && isNaN(helperSalaryPerMonth) == false && isNaN(other1ExpensesPerMonth) == false && isNaN(other2ExpensesPerMonth) == false
                && isNaN(other3ExpensesPerMonth) == false && isNaN(fuelExpensesPerMonth) == false && isNaN(tyreExpensesPerMonth) == false && isNaN(maintenanceExpensesPerMonth) == false && isNaN(miscellaneousExpensesPerMonth) == false) {

                // Cost Of Vehicle
                minimum = parseFloat($('#cost-of-vehicle').attr('min'));
                maximum = parseFloat($('#cost-of-vehicle').attr('max'));

                if (parseFloat(costOfVehicle) < parseFloat(minimum) || parseFloat(costOfVehicle) > parseFloat(maximum))
                    result = false;

                // down Payment
                minimum = parseFloat($('#down-payment').attr('min'));
                maximum = parseFloat($('#down-payment').attr('max'));

                if (parseFloat(downPayment) < parseFloat(minimum) || parseFloat(downPayment) > parseFloat(maximum))
                    result = false;


                // body Fabrication Cost For Chassis
                minimum = parseFloat($('#body-fabrication-cost-for-chassis').attr('min'));
                maximum = parseFloat($('#body-fabrication-cost-for-chassis').attr('max'));

                if (parseFloat(bodyFabricationCostForChassis) < parseFloat(minimum) || parseFloat(bodyFabricationCostForChassis) > parseFloat(maximum))
                    result = false;

                // registration Charges
                minimum = parseFloat($('#registration-charges').attr('min'));
                maximum = parseFloat($('#registration-charges').attr('max'));

                if (parseFloat(registrationCharges) < parseFloat(minimum) || parseFloat(registrationCharges) > parseFloat(maximum))
                    result = false;


                // road Tax Amount
                minimum = parseFloat($('#road-tax-amount').attr('min'));
                maximum = parseFloat($('#road-tax-amount').attr('max'));

                if (parseFloat(roadTaxAmount) < parseFloat(minimum) || parseFloat(roadTaxAmount) > parseFloat(maximum))
                    result = false;

                // permit Amount
                minimum = parseFloat($('#permit-amount').attr('min'));
                maximum = parseFloat($('#permit-amount').attr('max'));

                if (parseFloat(permitAmount) < parseFloat(minimum) || parseFloat(permitAmount) > parseFloat(maximum))
                    result = false;


                // Insurance Amount
                minimum = parseFloat($('#insurance-amount').attr('min'));
                maximum = parseFloat($('#insurance-amount').attr('max'));

                if (parseFloat(insuranceAmount) < parseFloat(minimum) || parseFloat(insuranceAmount) > parseFloat(maximum))
                    result = false;

                // Miscellaneous Amount
                minimum = parseFloat($('#miscellaneous-amount').attr('min'));
                maximum = parseFloat($('#miscellaneous-amount').attr('max'));

                if (parseFloat(miscellaneousAmount) < parseFloat(minimum) || parseFloat(miscellaneousAmount) > parseFloat(maximum))
                    result = false;

                // loan EMI
                minimum = parseFloat($('#loan-EMI').attr('min'));
                maximum = parseFloat($('#loan-EMI').attr('max'));

                if (parseFloat(loanEMI) < parseFloat(minimum) || parseFloat(loanEMI) > parseFloat(maximum))
                    result = false;

                // earning Per Month
                minimum = parseFloat($('#earning-per-month').attr('min'));
                maximum = parseFloat($('#earning-per-month').attr('max'));

                if (parseFloat(earningPerMonth) < parseFloat(minimum) || parseFloat(earningPerMonth) > parseFloat(maximum))
                    result = false;

                // tax Per Month
                minimum = parseFloat($('#tax-per-month').attr('min'));
                maximum = parseFloat($('#tax-per-month').attr('max'));

                if (parseFloat(taxPerMonth) < parseFloat(minimum) || parseFloat(taxPerMonth) > parseFloat(maximum))
                    result = false;


                // permit Amount Per Month
                minimum = parseFloat($('#permit-amount-per-month').attr('min'));
                maximum = parseFloat($('#permit-amount-per-month').attr('max'));

                if (parseFloat(permitAmountPerMonth) < parseFloat(minimum) || parseFloat(permitAmountPerMonth) > parseFloat(maximum))
                    result = false;

                // insurance Amount Per Month
                minimum = parseFloat($('#insurance-amount-per-month').attr('min'));
                maximum = parseFloat($('#insurance-amount-per-month').attr('max'));

                if (parseFloat(insuranceAmountPerMonth) < parseFloat(minimum) || parseFloat(insuranceAmountPerMonth) > parseFloat(maximum))
                    result = false;

                // driver Salary Per Month
                minimum = parseFloat($('#driver-salary-per-month').attr('min'));
                maximum = parseFloat($('#driver-salary-per-month').attr('max'));

                if (parseFloat(driverSalaryPerMonth) < parseFloat(minimum) || parseFloat(driverSalaryPerMonth) > parseFloat(maximum))
                    result = false;

                // helper Salary Per Month
                minimum = parseFloat($('#helper-salary-per-month').attr('min'));
                maximum = parseFloat($('#helper-salary-per-month').attr('max'));

                if (parseFloat(helperSalaryPerMonth) < parseFloat(minimum) || parseFloat(helperSalaryPerMonth) > parseFloat(maximum))
                    result = false;

                // other1 Expenses Per Month
                minimum = parseFloat($('#other1-expenses-per-month').attr('min'));
                maximum = parseFloat($('#other1-expenses-per-month').attr('max'));

                if (parseFloat(other1ExpensesPerMonth) < parseFloat(minimum) || parseFloat(other1ExpensesPerMonth) > parseFloat(maximum))
                    result = false;

                // other2 Expenses Per Month
                minimum = parseFloat($('#other2-expenses-per-month').attr('min'));
                maximum = parseFloat($('#other2-expenses-per-month').attr('max'));

                if (parseFloat(other2ExpensesPerMonth) < parseFloat(minimum) || parseFloat(other2ExpensesPerMonth) > parseFloat(maximum))
                    result = false;

                // other3 Expenses Per Month
                minimum = parseFloat($('#other3-expenses-per-month').attr('min'));
                maximum = parseFloat($('#other3-expenses-per-month').attr('max'));

                if (parseFloat(other3ExpensesPerMonth) < parseFloat(minimum) || parseFloat(other3ExpensesPerMonth) > parseFloat(maximum))
                    result = false;


                // fuel Expenses Per Month
                minimum = parseFloat($('#fuel-expenses-per-month').attr('min'));
                maximum = parseFloat($('#fuel-expenses-per-month').attr('max'));

                if (parseFloat(fuelExpensesPerMonth) < parseFloat(minimum) || parseFloat(fuelExpensesPerMonth) > parseFloat(maximum))
                    result = false;

                // tyre Expenses Per Month
                minimum = parseFloat($('#tyre-expenses-per-month').attr('min'));
                maximum = parseFloat($('#tyre-expenses-per-month').attr('max'));

                if (parseFloat(tyreExpensesPerMonth) < parseFloat(minimum) || parseFloat(tyreExpensesPerMonth) > parseFloat(maximum))
                    result = false;

                // maintenance Expenses Per Month
                minimum = parseFloat($('#maintenance-expenses-per-month').attr('min'));
                maximum = parseFloat($('#maintenance-expenses-per-month').attr('max'));

                if (parseFloat(maintenanceExpensesPerMonth) < parseFloat(minimum) || parseFloat(maintenanceExpensesPerMonth) > parseFloat(maximum))
                    result = false;

                // miscellaneou Expenses Per Month
                minimum = parseFloat($('#miscellaneous-expenses-per-month').attr('min'));
                maximum = parseFloat($('#miscellaneous-expenses-per-month').attr('max'));

                if (parseFloat(miscellaneousExpensesPerMonth) < parseFloat(minimum) || parseFloat(miscellaneousExpensesPerMonth) > parseFloat(maximum))
                    result = false;

            } else
                result = false;

        }
        if (result)
            $('#vehicle-loan-economics-detail-error').addClass('d-none');
        else
            $('#vehicle-loan-economics-detail-error').removeClass('d-none');

        return result;
    }

    // PreOwnedVehicleLoanInspection
    function IsValidPreOwnedVehicleLoanInspectionAccordionInputs() {
        result = true;
        debugger;

        let numberOfOwner = parseInt($('#number-of-owner').val());
        let speedoMeterReading = parseFloat($('#speedo-meter-reading').val());
        let engineCondition = $('#engine-condition').val();
        let gearBoxCondition = $('#gear-box-condition').val();
        let rearAxleCondition = $('#rear-axle-condition').val();
        let frontAxleCondition = $('#front-axle-condition').val();
        let bodyCabinCondition = $('#body-cabin-condition').val();
        let tyresCondition = $('#tyres-condition').val();
        let expectedPriceOfSameModelSameCondition = parseFloat($('#expected-price-of-same-loanCustomerAccountDetailViewModel-same-condition').val());
        let expectedPriceOfSameModelExcellentCondition = parseFloat($('#expected-price-of-same-loanCustomerAccountDetailViewModel-excellent-condition').val());
        let hypothecationFullDetails = $('#hypothecation-full-details').val();
        let remarkOfValuer = $('#remark-of-valuer').val();

        if (!$('#heading-preOwned-vehicle-loan-inspection').hasClass('d-none')) {
            if (isNaN(numberOfOwner) == false && isNaN(speedoMeterReading) == false && isNaN(engineCondition) == false && isNaN(gearBoxCondition) == false
                && isNaN(rearAxleCondition) == false && isNaN(frontAxleCondition) == false && isNaN(bodyCabinCondition) == false && isNaN(tyresCondition) == false
                && isNaN(expectedPriceOfSameModelSameCondition) == false && isNaN(expectedPriceOfSameModelExcellentCondition) == false && isNaN(hypothecationFullDetails) == false && isNaN(remarkOfValuer) == false) {

                // Number Of Owner
                minimum = parseInt($('#number-of-owner').attr('min'));
                maximum = parseInt($('#number-of-owner').attr('max'));

                if (parseInt(numberOfOwner) < parseInt(minimum) || parseInt(numberOfOwner) > parseInt(maximum))
                    result = false;

                // speedo Meter Reading
                minimum = parseFloat($('#speedo-meter-reading').attr('min'));
                maximum = parseFloat($('#speedo-meter-reading').attr('max'));

                if (parseFloat(speedoMeterReading) < parseFloat(minimum) || parseFloat(speedoMeterReading) > parseFloat(maximum))
                    result = false;


                // Expected Price Of Same Model Same Condition
                minimum = parseFloat($('#expected-price-of-same-loanCustomerAccountDetailViewModel-same-condition').attr('min'));
                maximum = parseFloat($('#expected-price-of-same-loanCustomerAccountDetailViewModel-same-condition').attr('max'));

                if (parseFloat(expectedPriceOfSameModelSameCondition) < parseFloat(minimum) || parseFloat(expectedPriceOfSameModelSameCondition) > parseFloat(maximum))
                    result = false;


                // Expected Price Of Same Model Same Condition
                minimum = parseFloat($('#expected-price-of-same-loanCustomerAccountDetailViewModel-excellent-condition').attr('min'));
                maximum = parseFloat($('#expected-price-of-same-loanCustomerAccountDetailViewModel-excellent-condition').attr('max'));

                if (parseFloat(expectedPriceOfSameModelExcellentCondition) < parseFloat(minimum) || parseFloat(expectedPriceOfSameModelExcellentCondition) > parseFloat(maximum))
                    result = false;

            } else
                result = false;

        }
        if (result)
            $('#loan-inspection-error').addClass('d-none');
        else
            $('#loan-inspection-error').removeClass('d-none');

        return result;
    }

    // FieldInvestigation
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
        if (!$('#heading-field-investigation').hasClass('d-none')) {

            if ($($('#date-of-investigation').val() != '' && $('#investigation-officer-id').val() != '' && nameOfContactedPerson !== '' && $('.relation-with-applicant:checked').next('label').text() != '' && !isNaN(otherRelationTitle) && !isNaN(localityRemark) && !isNaN(firstReferenceName)
                && !isNaN(firstReferenceAddress) && !isNaN(secondReferenceName) && !isNaN(secondReferenceAddress) && !isNaN(thirdReferenceName) && !isNaN(thirdReferenceAddress) && !isNaN(positiveObservations) && !isNaN(negativeObservations) && !isNaN(nonRecommendationReason))) {


                if (nameOfContactedPerson == '' || nameOfContactedPerson.length > 100)
                    result = false;

                if (otherRelationTitle == '' || otherRelationTitle.length > 100)
                    result = false;

                if (localityRemark == '' || localityRemark.length > 1500)
                    result = false;

                if (firstReferenceName == '' || firstReferenceName.length > 100)
                    result = false;

                if (firstReferenceAddress == '' || firstReferenceAddress.length > 500)
                    result = false;

                if (secondReferenceName == '' || secondReferenceName.length > 100)
                    result = false;

                if (secondReferenceAddress == '' || secondReferenceAddress.length > 500)
                    result = false;

                if (thirdReferenceName == '' || thirdReferenceName.length > 100)
                    result = false;


                if (thirdReferenceAddress == '' || thirdReferenceAddress.length > 500)
                    result = false;

                if (positiveObservations == '' || positiveObservations.length > 2500)
                    result = false;

                if (negativeObservations == '' || negativeObservations.length > 2500)
                    result = false;

                if (nonRecommendationReason == '' || nonRecommendationReason.length > 2500)
                    result = false;

            } else
                result = false;

        }
        if (result)
            $('#field-investigation-error').addClass('d-none');
        else
            $('#field-investigation-error').removeClass('d-none');

        return result;

    }

    // Document Validation - Check Whether All Required Record (Documents) Added Or Not
    function IsAddedAllRequiredDocument()
    {
        debugger;
        let result = true;
        let documentTableValueArray = new Array();
        let i = 0;

        dataTableRecordCount = documentDataTable.rows().count();

        if (parseInt(dataTableRecordCount) >= requiredDocumentObj.length)
        {
            // Hide Added Joint Account DropdownList Items
            $('#tbl-document > tbody > tr').each(function ()
            {
                currentRow = $(this).closest('tr');

                let myColumnValues = (documentDataTable.row(currentRow).data());

                documentTableValueArray.push(myColumnValues[1]);

                // Check All Entered 
                //if ($.inArray(selectListItemObj.Value, requiredDocumentObj.DocumentId) > -1)
                //    if (myColumnValues[1] != editedAddressTypeId)
                //        $('#address-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            });

            for (i = 0; i < requiredDocumentArray.length; i++)
            {
                debugger;
                if ($.inArray(requiredDocumentArray[i], documentTableValueArray) == -1)
                {
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

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // #################  Join Account - DataTable Code 

    // DataTable Add Button 
    $('#btn-add-joint-account-dt').click(function ()
    {
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

            // Display Value In Modal Inputs

            $('#person-id-joint-account-holder', myModal).val(columnValues[2]);
            $('#joint-account-holder-id', myModal).val(columnValues[3]);
            $('#joint-account-sequence-number', myModal).val(columnValues[5]);
            $('#activation-date-joint-account-holder', myModal).val(jointAccountHolderActivationDate);
            $('#expiry-date-joint-account-holder', myModal).val(jointAccountHolderExpiryDate);
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
            if (confirm('Are You Sure To Delete This Row?')) {
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
        debugger;
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
        if (note == '')
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
        if (selectedjointPersonId == '') {
            result = false;
            $('#person-id-joint-account-holder-error').removeClass('d-none');
        }
        else
            $('#person-id-joint-account-holder-error').addClass('d-none');

        //Validation Joint Account Holder Id
        if (jointAccountHolderId == '') {
            result = false;
            $('#joint-account-holder-id-error').removeClass('d-none');
        }
        else
            $('#joint-account-holder-id-error').addClass('d-none');

        //Validation Sequence Number
        if (sequenceNumber == '' || isDuplicateSequenceNumber == true || parseInt(sequenceNumber) < 1 || parseInt(sequenceNumber) > 199) {
            result = false;
            $('#joint-account-sequence-number-error').removeClass('d-none');
        }
        else
            $('#joint-account-sequence-number-error').addClass('d-none');

        // Validation Activation Date
        if (!IsValidInputDate('#activation-date-joint-account-holder')) {
            result = false;
            $('#activation-date-joint-account-holder-error').removeClass('d-none');
        }
        else
            $('#activation-date-joint-account-holder-error').addClass('d-none');

        // Validation Expiry Date
        if (!IsValidInputDate('#expiry-date-joint-account-holder')) {
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
    $('#btn-add-account-nominee-dt').click(function () {
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

            options = '<option value="0">--- Select Person ---</option>';

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

            // Get Adding Nominee Limit By SchemeId
            if (schemeId != '') {
                $.get('/AccountChildAction/GetSharesCapitalSchemeDetailBySchemeId', { _schemeId: schemeId, async: false }, function (data, textStatus, jqXHR) {
                    minimumNominee = data.SchemeAccountParameterViewModel.MinimumNominee;
                    maximumNominee = data.SchemeAccountParameterViewModel.MaximumNominee;

                    let nomineeDataTableCount = nomineeDataTable.rows().count();

                    // Raise Error If Add Nominee Out Of Range
                    if (parseInt(nomineeDataTableCount) >= parseInt(maximumNominee)) {
                        $('#account-nominee-modal').modal('hide');
                        alert('Number Of Nominee Allowed Between' + minimumNominee + ' And ' + maximumNominee);
                    }
                    else
                        SetModalTitle('account-nominee', 'Add');
                });
            }
            else
                alert('Select Valid Scheme');
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
                    if (data <= 18)
                        $('#heading-guardian').removeClass('d-none');
                    else
                        $('#heading-guardian').addClass('d-none');
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
                    $('#heading-guardian').removeClass('d-none');
                else
                    $('#heading-guardian').addClass('d-none');
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
            debugger;


            // Display Value In Modal Inputs
            $('#customer-person-id', myModal).val(columnValues[1]);
            $('#nomination-number', myModal).val(columnValues[3]);
            $('#nomination-date', myModal).val(nominationDate);
            $('#sequence-number', myModal).val(columnValues[5]);
            $('#name-of-nominee', myModal).val(columnValues[6]);
            $('#trans-name-of-nominee', myModal).val(columnValues[7]);
            nomineePersonInformationNumber = columnValues[8];
            nomineePersonInformationNumberText = columnValues[9];
            $('#nominee-person-id', myModal).val(columnValues[9])
            $('#nominee-birth-date', myModal).val(birthDate);
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
            $('#guardian-nominee-birth-date', myModal).val(guardianBirthDate);
            $('#guardian-nominee-full-address-details', myModal).val(columnValues[32]);
            $('#trans-guardian-nominee-full-address-details', myModal).val(columnValues[33]);
            $('#guardian-nominee-contact-details', myModal).val(columnValues[34]);
            $('#trans-guardian-nominee-contact-details', myModal).val(columnValues[35]);

            if (columnValues[36] == '')
                $('input[name="CustomerAccountNomineeViewModel.CustomerAccountNomineeGuardianViewModel.AgeProofSubmissionStatusOfTheMinor"]').prop('checked', false);
            else
                $('input[name="CustomerAccountNomineeViewModel.CustomerAccountNomineeGuardianViewModel.AgeProofSubmissionStatusOfTheMinor"][value=' + columnValues[36] + ']').prop('checked', true);

            $('#appointed-date-of-contact', myModal).val(appointedDateOfContact);
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
                $('#heading-guardian').addClass('d-none');
            else
                $('#heading-guardian').removeClass('d-none');

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
            if (confirm('Are You Sure To Delete This Row?')) {
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

        if (nomineePersonInformationNumber == '' || nomineePersonInformationNumber == 'None') {
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
        if (birthDate == '')
            birthDate = '1900-01-01';

        if (nomineePersonInformationNumber == '' || typeof nomineePersonInformationNumber == 'undefined')
            nomineePersonInformationNumber = 'None';

        if (guardianNomineePersonInformationNumber == '' || typeof guardianNomineePersonInformationNumber == 'undefined')
            guardianNomineePersonInformationNumber = 'None';

        if (nameOfNominee == '')
            nameOfNominee = 'None';

        if (fullAddressDetails == '')
            fullAddressDetails = 'None';

        if (transnameOfNominee == '')
            transnameOfNominee = 'None';

        if (transContactDetails == '')
            transContactDetails = 'None';

        if (contactDetails == '')
            contactDetails = 'None';

        if (transFullAddress == '')
            transFullAddress = 'None';

        if (note == '')
            note = 'None';

        if (transNote == '')
            transNote = 'None';

        if (guardianNomineeFullAddress == '')
            guardianNomineeFullAddress = 'None';

        if (transGuardianNomineeFullAddress == '')
            transGuardianNomineeFullAddress = 'None';

        if (guardianContactDetails == '')
            guardianContactDetails = 'None';

        if (transGuardianContactDetails == '')
            transGuardianContactDetails = 'None';

        if (guardianNote == '')
            guardianNote = 'None';

        if (transGuardianNote == '')
            transGuardianNote = 'None';

        if (isNaN(holdingPercentage))
            holdingPercentage = 0;

        if (isNaN(proportionateAmountForEachNominee))
            proportionateAmountForEachNominee = 0;

        // Check Whether Nominee Is Adult Or Minor 
        let isAdult = $('#heading-guardian').hasClass('d-none');

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
                return parseInt(nomineeDataTable.row(value).data()[3]) === parseInt($('#nomination-number').val());
            });

        if (nomineeDataTable.rows(filteredDataForNomineeNumber).count() > 0 && editedNomineeNumber != $('#nomination-number').val()) {
            isDuplicateNomineeNumber = true;
            result = false;
            $('#joint-account-sequence-number-error').removeClass('d-none');
        }
        else {
            isDuplicateNomineeNumber = false;
            $('#joint-account-sequence-number-error').addClass('d-none');
        }

        if (nominationNumber == '' || isDuplicateNomineeNumber == true || parseInt(nominationNumber.length) > 50) {
            result = false;
            $('#nomination-number-error').removeClass('d-none');
        }
        else
            $('#nomination-number-error').addClass('d-none');

        if (nominationDate == '') {
            result = false;
            $('#nomination-date-error').removeClass('d-none');
        }
        else
            $('#nomination-date-error').addClass('d-none');

        if (typeof sequenceNumber == 'undefined' || $('#customer-person-id option:selected').text().trim() == 'Select Person') {
            result = false;
            $('#customer-person-id-error').removeClass('d-none');
        }
        else
            $('#customer-person-id-error').addClass('d-none');

        // Check Whether Nominee Is Added Or Not?
        if (nomineeDataTable.rows(filteredDataForNomineeNumber).count() > nominationNumberCount)
            $('#nomination-number-error').removeClass('d-none')

        // Check Whether Person Information Number Selected For Nominee Or Not?
        if (nomineePersonInformationNumber == 'None' || typeof nomineePersonInformationNumber == 'undefined')
            isSelectedPersonInformationNumberForNominee = false;
        else
            isSelectedPersonInformationNumberForNominee = true;

        if ((isSelectedPersonInformationNumberForNominee == false && nameOfNominee == 'None') || parseInt(nameOfNominee.length) < 3 || parseInt(nameOfNominee.length) > 150) {
            result = false;

            $('#nominee-person-id-error').removeClass('d-none');
            $('#name-of-nominee-error').removeClass('d-none');
        }
        else {
            $('#nominee-person-id-error').addClass('d-none');
            $('#name-of-nominee-error').addClass('d-none');
        }

        if (isSelectedPersonInformationNumberForNominee == false) {
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
        if (relationId == '') {
            result = false;
            $('#relation-id-error').removeClass('d-none');
        }
        else
            $('#relation-id-error').addClass('d-none');

        // Validate Holding Percentage, If Visible
        if (!$('#holding-percentage-input').hasClass('d-none')) {
            if (isNaN(holdingPercentage) || parseFloat(holdingPercentage) < 0.1 || parseFloat(holdingPercentage) > 100) {
                result = false;
                $('#holding-percentage-error').removeClass('d-none');
            }
            else
                $('#holding-percentage-error').addClass('d-none');
        }

        // Validate Holding Percentage, If Visible
        if (!$('#proportionate-amount-for-each-nominee-input').hasClass('d-none')) {
            if (isNaN(proportionateAmountForEachNominee) || parseFloat(proportionateAmountForEachNominee) < 1 || parseFloat(proportionateAmountForEachNominee) > 9999999999) {
                result = false;
                $('#proportionate-amount-for-each-nominee-error').removeClass('d-none');
            }
            else
                $('#proportionate-amount-for-each-nominee-error').addClass('d-none');
        }

        // Validate Date
        if (!IsValidInputDate('#activation-date-nominee')) {
            result = false;
            $('#nominee-activation-date-error').removeClass('d-none');
        }
        else
            $('#activation-date-nominee-error').addClass('d-none');

        if (!IsValidInputDate('#expiry-date-nominee')) {
            result = false;
            $('#nominee-expiry-date-error').removeClass('d-none');
        }
        else
            $('#expiry-date-nominee-error').addClass('d-none');


        // If Nominee Is Minor (i.e. Adult)
        if (!(isAdult)) {
            $('#customer-nominee-guardian-accordion-error').removeClass('d-none');

            if (guardianTypeId == '') {
                result = false;
                $('#guardian-type-id-error').removeClass('d-none');
            }

            // Check Whether Person Information Number Selected For Nominee Or Not?
            if (guardianNomineePersonInformationNumber == 'None' || typeof guardianNomineePersonInformationNumber == 'undefined')
                isSelectedPersonInformationNumberForGuardian = false;
            else
                isSelectedPersonInformationNumberForGuardian = true;

            if ((isSelectedPersonInformationNumberForGuardian == false && guardianFullName == 'None') || parseInt(guardianFullName.length) < 3 || parseInt(guardianFullName.length) > 150) {
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

            if (isSelectedPersonInformationNumberForGuardian == false) {
                if (transGuardianFullName == '' || transGuardianFullName == 'None' || parseInt(transGuardianFullName.length) > 150) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#trans-guardian-full-name-error').removeClass('d-none');
                }

                if (!IsValidInputDate('#guardian-nominee-birth-date')) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#guardian-nominee-birth-date-error').removeClass('d-none');
                }

                if (guardianNomineeFullAddress == 'None' || parseInt(guardianNomineeFullAddress.length) > 500) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#guardian-nominee-full-address-details-error').removeClass('d-none');
                }

                if (transGuardianNomineeFullAddress == 'None' || parseInt(transGuardianNomineeFullAddress.length) > 500) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#trans-guardian-nominee-full-address-details-error').removeClass('d-none');
                }

                if (guardianContactDetails == 'None' || parseInt(guardianContactDetails.length) > 150) {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#guardian-nominee-contact-details-error').removeClass('d-none');
                }

                if (transGuardianContactDetails == 'None') {
                    result = false;
                    isValidGuardianDetails = false;
                    $('#trans-guardian-nominee-contact-details-error').removeClass('d-none');
                }
            }

            if (ageProofSubmissionStatusOfTheMinorText == '') {
                result = false;
                isValidGuardianDetails = false;
                $('#age-proof-sub-status-minor-error').removeClass('d-none');
            }

            if (appointedDateOfContact == '') {
                result = false;
                isValidGuardianDetails = false;
                $('#appointed-date-of-contact-error').removeClass('d-none');
            }

            if (appointedTimeOfContact == '') {
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
        nomineeDataTable.column(24).visible(false);
        nomineeDataTable.column(27).visible(false);
        nomineeDataTable.column(29).visible(false);
        nomineeDataTable.column(36).visible(false);
        nomineeDataTable.column(42).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@   Contact Details - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-contact-dt').click(function () {
        event.preventDefault();
        $('#send-code').addClass('d-none');
        $('#resend').addClass('d-none');
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
                            personAddressPrmKey,
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
                            personAddressPrmKey,
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
                    personAddressPrmKey,
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
            if (confirm('Are You Sure To Delete This Row?')) {
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
        if (!isDuplicateContact) {
            // Get Modal Inputs In Local letiable
            tag = '<input type="checkbox" name="select-all" class="checks"/>';
            contactType = $('#contact-type option:selected').val();
            contactTypeText = $('#contact-type option:selected').text();
            fieldValue = $('#field-value').val();
            isVerified = $('input[name="PersonContactDetailViewModel.IsVerified"]').is(':checked') ? 'True' : "False";
            note = $('#note-contact-detail').val();
            verificationCode = $('#verification-code').val();
            personAddressPrmKey = 0;
            reasonForModification = $('#reason-for-modification-contact').val();
            hasDivClass = $('#contact-div').hasClass('d-none');

            // Set Default Value, If Empty
            if (note == '')
                note = 'None';

            if (contactType == '') {
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
                if (!regex.test(fieldValue)) {
                    result = false;
                    $('#field-value-error').removeClass('d-none');
                }
                else
                    $('#field-value-error').addClass('d-none');

                // Mobile OTP Validation
                if (verificationCode == '' || verificationCode == '0') {
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
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   Person  Address Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-person-address-dt').click(function () {
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
            $('#reason-for-modification-address', myModal).val(columnValues[2]);

            if (columnValues[17] === 'True')

                $('#is-verified-address').prop('checked', true);
            else
                $('#is-verified-address').prop('checked', false);

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
                personAddressPrmKey
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
                personAddressPrmKey
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
            if (confirm('Are You Sure To Delete This Row?')) {
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
    function HideAddressDataTableColumns() {
        addressDataTable.column(1).visible(false);
        addressDataTable.column(11).visible(false);
        addressDataTable.column(13).visible(false);
        addressDataTable.column(15).visible(false);
        addressDataTable.column(20).visible(false);
        addressDataTable.column(21).visible(false);
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

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
                if (myColumnValues[1] != editedAddressTypeId)
                    $('#address-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    // Document Unique Dropdown
    // Address Type Unique Dropdown
    function SetDocumentUniqueDropdownList()
    {
        // Show All List Items
        $('#document-id').html('');
        $('#document-id').append(documentDropdownList);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-document > tbody > tr').each(function ()
        {
            currentRow = $(this).closest('tr');

            let myColumnValues = (documentDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
            {
                if (myColumnValues[1] != editedDocumentId)
                    $('#document-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
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

        // Set Default Value, If Empty
        if (note == '')
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
    $('#btn-add-guarantor-detail-dt').click(function () {
        debugger;
        if ($('#guarantor-detail-modal').length) {
            editedSequenceNumber = 0;
            personId = '';
            personIdText = '';

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

            // Display Value In Modal Inputs
            $('#person-id-guarantor-detail', myModal).val(columnValues[1]);
            $('#guarantee-amount', myModal).val(columnValues[4]);
            $('#is-limited-guarantee', myModal).val(columnValues[5]);
            editedSequenceNumber = columnValues[3];
            if (columnValues[5] === 'True')
                $('#is-limited-guarantee').prop('checked', true);
            else
                $('#is-limited-guarantee').prop('checked', false);

            $('#note-account-guarantor-detail', myModal).val(columnValues[6]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-guarantor-detail-dt').addClass('read-only');
            $('#guarantor-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-guarantor-detail-modal').click(function (event) {
        debugger;
        if (IsValidGuarantorDetailDataTableModal()) {
            row = guarantorDetailDataTable.row.add([
            tag,
            personId,
            personIdText,
            sequenceNumber,
            guaranteeAmount,
            isLimitedGuarantee,
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
    $('#btn-update-guarantor-detail-modal').click(function (event) {
        debugger;
        $('#select-all-guarantor-detail').prop('checked', false);
        if (IsValidGuarantorDetailDataTableModal()) {
            guarantorDetailDataTable.row(selectedRowIndex).data([
                             tag,
                             personId,
                             personIdText,
                             sequenceNumber,
                             guaranteeAmount,
                             isLimitedGuarantee,
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
    $('#btn-delete-guarantor-detail-dt').click(function (event) {
        debugger;
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
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

                    $('#guarantor-detail-error').addClass('d-none');
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
        $('#tbl-guarantor-detail input[type="checkbox"]:checked').each(function (index) {

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
            $('#person-id-guarantor-detail').find("option[value='" + columnValues[1] + "']").hide();
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
        personId = $('#person-id-guarantor-detail option:selected').val();
        personIdText = $('#person-id-guarantor-detail option:selected').text();
        sequenceNumber = $('#sequence-number-guarantor-detail').val();
        guaranteeAmount = $('#guarantee-amount').val();
        isLimitedGuarantee = $('input[name="CustomerLoanAccountGuarantorDetailViewModel.IsLimitedGuarantee"]').is(':checked') ? 'True' : "False";
        note = $('#note-account-guarantor-detail').val();


        // Check Whether Enter Mobile Number Is Existed Or Not
        let filteredData = guarantorDetailDataTable
            .rows()
            .indexes()
            .filter(function (value, index) {
                return guarantorDetailDataTable.row(value).data()[3] == $('#sequence-number-guarantor-detail').val();
            });

        if (guarantorDetailDataTable.rows(filteredData).count() > 0 && editedSequenceNumber != $('#sequence-number-guarantor-detail').val()) {
            isDuplicateSequenceNumber = true;
            result = false;
            $('#sequence-number-guarantor-detail-error').removeClass('d-none');
        }
        else {
            isDuplicateSequenceNumber = false;
            $('#sequence-number-guarantor-detail-error').addClass('d-none');
        }


        // Set Default Value, If Empty
        if (reasonForModification == '')
            reasonForModification = 'None';



        if (note == '')
            note = 'None';

        if (personId == '') {
            result = false;
            $('#person-id-guarantor-detail-error').removeClass('d-none')
        } else
            $('#person-id-guarantor-detail-error').addClass('d-none')


        //Validation Sequence Number
        if (sequenceNumber == '' || isDuplicateSequenceNumber == true || parseInt(sequenceNumber) < 1 || parseInt(sequenceNumber) > 199) {
            result = false;
            $('#sequence-number-guarantor-detail-error').removeClass('d-none');
        }
        else
            $('#sequence-number-guarantor-detail-error').addClass('d-none');

        if (guaranteeAmount == '') {
            result = false;
            $('#guarantee-amount-error').removeClass('d-none')
        } else {
            $('#guarantee-amount-error').addClass('d-none')
        }



        // Add + 1 (i.e. Current Row Count)
        if (editedSequenceNumber == 0)
            dataTableRecordCount = dataTableRecordCount + 1;

        if (parseInt(dataTableRecordCount) < parseInt(minimumJointAccountHolder)) {
            minMaxResult = false;
            $('#guarantor-detail-min-max-error').html('Number Of Guarantor Detail Are Allowed Between ' + minimumJointAccountHolder + ' And ' + maximumJointAccountHolder);
        }

        if (result) {
            if (minMaxResult == false) {
                $('#guarantor-detail-error').addClass('d-none');
                $('#guarantor-detail-min-max-error').removeClass('d-none');
            }
            else {
                $('#guarantor-detail-error').addClass('d-none');
                $('#guarantor-detail-min-max-error').addClass('d-none');
            }
        }


        return result;

    }

    // Hide Unnecessary Columns
    function HideGuarantorDetailDataTableColumns() {
        guarantorDetailDataTable.column(1).visible(false);
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
    function SetPageLoadingDefaultValues(event) {
        let selectedSchemeId = $('#scheme-id').val();

        debugger;
        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();
        SetLoanTypeSetting();
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
        $.get('/DynamicDropdownList/GetPersonDropdownListForSharesAccountOpening', { _schemeId: selectedSchemeId, async: false }, function (data, textStatus, jqXHR) {
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

        // Get Person Dropdown For Guardian
        $.get('/DynamicDropdownList/GetPersonDropdownListForGuardian', function (data) {
            personDropdownListDataForGuardian = data;
        });

        // Check Whether Element Exist OR Not ***** Applicable For Only Amend
        if ($('#person-id-value').length) {
            let personIdValueOnAmend = $('#person-id-value').attr('class').toString().replace('d-none', '');

            $('#person-id').val(personIdValueOnAmend);

            SetupTimePeriodUnit()
        }

    }

    // @@@@@@@@@@@@@@@@@@@@@@@@ collateral Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-gold-collateral-detail-dt').click(function () {
        debugger;
        event.preventDefault();
        SetModalTitle('gold-collateral-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-gold-collateral-detail-dt').click(function () {
        debugger;
        SetModalTitle('gold-collateral-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-gold-collateral-detail-dt').data('rowindex');
            id = $('#gold-collateral-detail-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#jewel-assayer-id', myModal).val(columnValues[1]);
            $('#gold-ornament-id', myModal).val(columnValues[3]);
            $('#sequence-number-gold-collateral-detail', myModal).val(columnValues[5]);
            $('input[name="CustomerGoldLoanCollateralDetailViewModel.MetalPurity"][value=' + columnValues[6] + ']').prop('checked', true);
            $('#huid', myModal).val(columnValues[8]);
            $('#qty', myModal).val(columnValues[9]);
            $('#metal-gross-weight', myModal).val(columnValues[16]);
            $('#enable-any-damage', myModal).val(columnValues[10]);

            if (columnValues[10] === 'True')
                $('#enable-any-damage').prop('checked', true);
            else
                $('#enable-any-damage').prop('checked', false);

            $('#damage-description', myModal).val(columnValues[11]);
            $('#damage-weight', myModal).val(columnValues[12]);


            if (columnValues[13] === 'True')
                $('#enable-any-westage').prop('checked', true);
            else
                $('#enable-any-westage').prop('checked', false);

            $('#westage-description', myModal).val(columnValues[14]);
            $('#westage-weight', myModal).val(columnValues[15]);

            $('#enable-diamond', myModal).val(columnValues[18]);

            if (columnValues[18] === 'True')
                $('#enable-diamond').prop('checked', true);
            else
                $('#enable-diamond').prop('checked', false);

            $('#enable-diamond-deductable', myModal).val(columnValues[18]);

            if (columnValues[17] === 'True')
                $('#enable-diamond-deductable').prop('checked', true);
            else
                $('#enable-diamond-deductable').prop('checked', false);

            $('#number-of-diamond', myModal).val(columnValues[19]);
            $('#diamond-carat', myModal).val(columnValues[20]);
            $('#clarity-colour', myModal).val(columnValues[21]);
            $('#diamond-weight', myModal).val(columnValues[22]);
            $('#diamond-price', myModal).val(columnValues[23]);
            $('#diamond-valuation', myModal).val(columnValues[24]);
            $('#metal-net-weight', myModal).val(columnValues[25]);
            $('input[name="CustomerGoldLoanCollateralDetailViewModel.CustodyStatus"][value=' + columnValues[26] + ']').prop('checked', true);
            $('#jewel-assayer-remark', myModal).val(columnValues[28]);
            $('#note-gold-loan-collateral-detail', myModal).val(columnValues[29]);
            $('#reason-for-modification-gold-loan', myModal).val(columnValues[30]);


            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-gold-collateral-detail-dt').addClass('read-only');
            $('#gold-collateral-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-gold-collateral-detail-modal').click(function (event) {
        debugger;
        if (IsValidgoldLoanCollateralDetailModal()) {
            row = goldLoanCollateralDetailDataTable.row.add([

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
            hasAnyDamage,
            damageDescription,
            damageWeight,
            hasAnyWestage,
            westageDescription,
            westageWeight,
            metalGrossWeight,
            isDiamondDeductable,
            hasDiamond,
            numberOfDiamond,
            diamondCarat,
            clarityColour,
            diamondWeight,
            diamondPrice,
            diamondValuation,
            metalNetWeight,
            custodyStatus,
            custodyStatusText,
            jewelAssayerRemark,
            note,
            reasonForModification,
            //valuationAmount,
            //marketValue,
            ]).draw();

            HidegoldLoanCollateralDetail();

            goldLoanCollateralDetailDataTable.columns.adjust().draw();

            // Hide Table Required Data Error Message
            $('#gold-collateral-detail-error').addClass('d-none');

            $('#gold-collateral-detail-modal').modal('hide');

            EnableNewOperation('gold-collateral-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-gold-collateral-detail-modal').click(function (event) {
        debugger;
        $('#select-all-gold-collateral-detail').prop('checked', false);
        if (IsValidgoldLoanCollateralDetailModal()) {
            goldLoanCollateralDetailDataTable.row(selectedRowIndex).data([

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
                                 hasAnyDamage,
                                 damageDescription,
                                 damageWeight,
                                 hasAnyWestage,
                                 westageDescription,
                                 westageWeight,
                                 metalGrossWeight,
                                 isDiamondDeductable,
                                 hasDiamond,
                                 numberOfDiamond,
                                 diamondCarat,
                                 clarityColour,
                                 diamondWeight,
                                 diamondPrice,
                                 diamondValuation,
                                 metalNetWeight,
                                 custodyStatus,
                                 custodyStatusText,
                                 jewelAssayerRemark,
                                 note,
                                 reasonForModification,
                                //valuationAmount,
                                //marketValue,


            ]).draw();

            HidegoldLoanCollateralDetail();

            goldLoanCollateralDetailDataTable.columns.adjust().draw();

            $('#gold-collateral-detail-modal').modal('hide');

            EnableNewOperation('gold-collateral-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-gold-collateral-detail-dt').click(function (event) {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-gold-collateral-detail tbody input[type="checkbox"]:checked').each(function () {
                    goldLoanCollateralDetailDataTable.row($('#tbl-gold-collateral-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-gold-collateral-detail-dt').data('rowindex');
                    EnableNewOperation('gold-collateral-detail');
                    $('#select-all-gold-collateral-detail').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!goldLoanCollateralDetailDataTable.data().any())
                    $('#gold-collateral-detail-error').removeClass('d-none');


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
            $('#tbl-gold-collateral-detail tbody input[type="checkbox"]').each(function (index) {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (goldLoanCollateralDetailDataTable.row(selectedRowIndex).data());

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
        $('#tbl-gold-collateral-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = goldLoanCollateralDetailDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (goldLoanCollateralDetailDataTable.row(selectedRowIndex).data());

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

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-gold-collateral-detail > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (goldLoanCollateralDetailDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#jewel-assayer-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate Fund Module
    function IsValidgoldLoanCollateralDetailModal() {
        debugger;

        // Get Modal Inputs In Local letiable    
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        jewelAssayerId = $('#jewel-assayer-id option:selected').val();
        jewelAssayerText = $('#jewel-assayer-id option:selected').text();
        goldOrnamentId = $('#gold-ornament-id option:selected').val();
        goldOrnamentText = $('#gold-ornament-id option:selected').text();
        sequenceNumber = $('#sequence-number-gold-collateral-detail').val();
        metalPurity = $('.metal-purity-carat:checked').val();
        metalPurityText = $('.metal-purity-carat:checked').next('label').text();
        huid = $('#huid').val();
        qty = $('#qty').val();
        hasAnyDamage = $('#enable-any-damage').is(':checked') ? 'True' : 'False';
        damageDescription = $('#damage-description').val();
        damageWeight = $('#damage-weight').val();
        hasAnyWestage = $('#enable-any-westage').is(':checked') ? 'True' : 'False';
        westageDescription = $('#westage-description').val();
        westageWeight = $('#westage-weight').val();
        metalGrossWeight = $('#metal-gross-weight').val();
        isDiamondDeductable = $('#enable-diamond-deductable').is(':checked') ? 'True' : 'False';
        hasDiamond = $('#enable-diamond').is(':checked') ? 'True' : 'False';
        numberOfDiamond = parseInt($('#number-of-diamond').val());
        diamondCarat = parseInt($('#diamond-carat').val());
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
        //valuationAmount;
        // marketValue;

        result = true;

        // Set Default Value To Note, If Empty
        if (note == '')
            note = 'None';

        if (reasonForModification == '')
            reasonForModification = 'None';

        if (jewelAssayerId == '') {
            result = false;
            $('#jewel-assayer-id-error').removeClass('d-none');
        } else
            $('#jewel-assayer-id-error').addClass('d-none');

        if (goldOrnamentId == '') {
            result = false;
            $('#gold-ornament-id-error').removeClass('d-none');
        } else
            $('#gold-ornament-id-error').addClass('d-none');

        if (sequenceNumber == '' || isDuplicateSequenceNumber == true || parseInt(sequenceNumber) < 1 || parseInt(sequenceNumber) > 199) {
            result = false;
            $('#sequence-number-gold-collateral-detail-error').removeClass('d-none');
        }
        else
            $('#sequence-number-gold-collateral-detail-error').addClass('d-none');

        if (metalPurityText == '') {
            result = false;
            $('#metal-purity-carat-error').removeClass('d-none');
        } else
            $('#metal-purity-carat-error').addClass('d-none');

        if (huid == '') {
            result = false;
            $('#huid-error').removeClass('d-none');
        } else
            $('#huid-error').addClass('d-none');

        if (qty == '') {
            result = false;
            $('#qty-error').removeClass('d-none');
        } else
            $('#qty-error').addClass('d-none');

        if ((metalGrossWeight == '') || (parseFloat(metalGrossWeight) < parseFloat(metalNetWeight))) {
            result = false;
            $('#metal-gross-weight-error').removeClass('d-none');
        } else
            $('#metal-gross-weight-error').addClass('d-none');

        if (hasAnyDamage == "True") {

            if (damageWeight == '') {
                $('#damage-weight-error').removeClass('d-none');
            }

        } else {
            $('#damage-weight-error').addClass('d-none');
        }

        if (hasAnyWestage == "True") {
            if (westageWeight == '') {

                $('#westage-weight-error').removeClass('d-none');
            }
        } else {
            $('#westage-weight-error').addClass('d-none');
        }

        if (hasDiamond == "True") {

            //if (!isNaN(numberOfDiamond) && !isNaN(diamondCarat) && !isNaN(clarityColour) && !isNaN(diamondWeight) && !isNaN(diamondPrice) && !isNaN(diamondValuation) && !isNaN(jewelAssayerRemark)) {

            //    //number Of Diamond
            //    minimum = parseInt($('#number-of-diamond').attr('min'));
            //    maximum = parseInt($('#number-of-diamond').attr('max'));

            //    if (parseInt(numberOfDiamond) < parseInt(minimum) || parseInt(numberOfDiamond) > parseInt(maximum))
            //        $('#number-of-diamond-error').removeClass('d-none');
            //    else
            //        $('#number-of-diamond-error').addClass('d-none');

            //    //diamond Carat
            //    minimum = parseInt($('#diamond-carat').attr('min'));
            //    maximum = parseInt($('#diamond-carat').attr('max'));

            //    if (parseInt(diamondCarat) < parseInt(minimum) || parseInt(diamondCarat) > parseInt(maximum))
            //        result = false;

            //    //clarity Colour
            //    if (clarityColour == '' || clarityColour.length > 150)
            //        result = false;


            //    //diamond Weight
            //    minimum = parseFloat($('#diamond-weight').attr('min'));
            //    maximum = parseFloat($('#diamond-weight').attr('max'));

            //    if (parseFloat(diamondWeight) < parseFloat(minimum) || parseFloat(diamondWeight) > parseFloat(maximum))
            //        result = false;

            //    //diamondPrice
            //    minimum = parseFloat($('#diamond-price').attr('min'));
            //    maximum = parseFloat($('#diamond-price').attr('max'));

            //    if (parseFloat(diamondPrice) < parseFloat(minimum) || parseFloat(diamondPrice) > parseFloat(maximum))
            //        result = false;

            //    //diamond Valuation
            //    minimum = parseFloat($('#diamond-valuation').attr('min'));
            //    maximum = parseFloat($('#diamond-valuation').attr('max'));

            //    if (parseFloat(diamondValuation) < parseFloat(minimum) || parseFloat(diamondValuation) > parseFloat(maximum))
            //        result = false;

            //    //clarity Colour
            //    if (jewelAssayerRemark == '' || jewelAssayerRemark.length > 1500)
            //        result = false;

            if (numberOfDiamond == '') {
                result = false;
                $('#number-of-diamond-error').removeClass('d-none');
            } else
                $('#number-of-diamond-error').addClass('d-none');

            if (diamondCarat == '') {
                result = false;
                $('#diamond-carat-error').removeClass('d-none');
            } else
                $('#diamond-carat-error').addClass('d-none');

            if (clarityColour == '') {
                result = false;
                $('#clarity-colour-error').removeClass('d-none');
            } else
                $('#clarity-colour-error').addClass('d-none');


            if (diamondWeight == '') {
                result = false;
                $('#diamond-weight-error').removeClass('d-none');
            } else
                $('#diamond-weight-error').addClass('d-none');


            if (diamondPrice == '') {
                result = false;
                $('#diamond-price-error').removeClass('d-none');
            } else
                $('#diamond-price-error').addClass('d-none');

            if (diamondValuation == '') {
                result = false;
                $('#diamond-valuation-error').removeClass('d-none');
            } else
                $('#diamond-valuation-error').addClass('d-none');

            if ((metalNetWeight == '') || (parseFloat(metalNetWeight) < 1)) {
                result = false;
                $('#metal-net-weight-error').removeClass('d-none');
            } else
                $('#metal-net-weight-error').addClass('d-none');


            if (custodyStatusText == '') {
                result = false;
                $('#custody-status-error').removeClass('d-none');
            } else
                $('#custody-status-error').addClass('d-none');

        }
        else
            result = false;

        if (result)
            $('#gold-collateral-detail-error').addClass('d-none');
        else
            $('#gold-collateral-detail-error').removeClass('d-none');





        return result;

    }

    // Hide Unnecessary Columns
    function HidegoldLoanCollateralDetail() {
        goldLoanCollateralDetailDataTable.column(1).visible(false);
        goldLoanCollateralDetailDataTable.column(3).visible(false);
        goldLoanCollateralDetailDataTable.column(6).visible(false);
        goldLoanCollateralDetailDataTable.column(26).visible(false);
        goldLoanCollateralDetailDataTable.column(30).visible(false);
    }


    // Close Button Click Event - On Close Button Click Clear All Modal Inputs
    $('.close').click(function () {
        ClearPreOwnedVehicleLoanPhotoDivErrors();

        $('.btn-delete').addClass('disabled');
        $('.btn-edit').addClass('disabled');
        $('.checks').prop('checked', false);
        $('.btn-add-preOwned-vehicle-loan-photo').removeClass('disabled');
        $('#preOwned-vehicle-loan-photo-select-all-chkbox').prop('checked', false);
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#preOwned-vehicle-loan-photo-select-all-chkbox').on('click', function () {
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);

            let arr = new Array();

            $('#preOwned-vehicle-loan-photo-data-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                let row = $(this).closest('tr');

                selectedRow = preOwnedVehicleLoanPhotoDataTable.row(row).index();
                let tdrow = (preOwnedVehicleLoanPhotoDataTable.row(selectedRow).data());
                arr.push({ td0: tdrow[1] });

                $('.btn-delete').data('rowindex', arr);
                $('.btn-add-preOwned-vehicle-loan-photo').addClass('disabled');
                $('.btn-edit').addClass('disabled');
                $('.btn-delete').removeClass('disabled');
            });
        }
        else {
            $('#preOwned-vehicle-loan-photo-data-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-preOwned-vehicle-loan-photo').removeClass('disabled');
                $('.btn-delete').addClass('disabled');
            });
        }
    });

    // binding the change event-handler to the tbody:
    $('#preOwned-vehicle-loan-photo-data-table tbody').on('change', function () {
        // getting all the checkboxes within the tbody:
        let all = $('tbody input[type="checkbox"]');
        let checked = all.filter(':checked');

        if (all.length > 0 == checked.length) {
            $('.btn-delete').removeClass('disabled');
            $('.btn-edit').removeClass('disabled');
        }
        else {
            $('.btn-edit').addClass('disabled');
        }

        if (checked.length == 0) {
            $('.btn-delete').addClass('disabled');
            $('.btn-add-preOwned-vehicle-loan-photo').removeClass('disabled');
        }
        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#preOwned-vehicle-loan-photo-select-all-chkbox').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    $('#preOwned-vehicle-loan-photo-data-table tbody').on('click', 'input[type=checkbox]', function () {
        $('#preOwned-vehicle-loan-photo-data-table input[type="checkbox"]:checked').each(function () {
            let isChecked = $(this).prop('checked');

            if (isChecked) {
                let arr = new Array();

                $('input[type="checkbox"]:checked').each(function (index) {
                    let row = $(this).closest('tr');

                    selectedRow = preOwnedVehicleLoanPhotoDataTable.row(row).index();

                    if (selectedRow >= 0) {
                        let tdrow = (preOwnedVehicleLoanPhotoDataTable.row(selectedRow).data());

                        arr.push({ td0: tdrow[1] });

                        $('.btn-add-preOwned-vehicle-loan-photo').addClass('disabled');
                        $('.btn-edit').removeClass('disabled');
                        $('.btn-delete').removeClass('disabled');


                        $('#btn-update-preOwned-vehicle-loan-photo').attr('rowindex', selectedRow);
                        $('.btn-edit').data('rowindex', tdrow);
                        $('.btn-delete').data('rowindex', arr);
                        $('#preOwned-vehicle-loan-photo-select-all-chkbox').data('rowindex', arr);
                    }
                });
            }
        });
    });

    //To page load table each row get value & dropdown value Hide 
    $('#preOwned-vehicle-loan-photo-data-table > tbody > tr').each(function () {
        let currentRow = $(this).closest('tr');

        //To get Already Assign Vehicle  letiant Record For Only Create Page 
        let name = window.location.pathname
            .split('/')
            .filter(function (c) { return c.length; })
            .pop();
        if (name == 'Verify') {
            $('.btn-add-preOwned-vehicle-loan-photo').addClass('disabled');
        }
    });

    //To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    function ClearPreOwnedVehicleLoanPhotoValues() {
        $('#preOwned-vehicle-loan-photo-note').val('None');
        $('#photo-path').val('');
        $('#image-pre-view-commodity').attr('src', '');
        $('#photo-caption').val('None');
    }

    //Clear Div Errors
    function ClearPreOwnedVehicleLoanPhotoDivErrors() {
        $('#preOwned-vehicle-loan-photo-note').next('div.error').remove();
        $('#photo-path').next('div.error').remove();
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Gold Loan Collateral Photo  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-gold-loan-photo-dt').click(function () {
        event.preventDefault();

        loanSanctionAmount = $('#sanction-amount').val();
        goldcollateralValuationAmount = 0;

        let goldOrnamentId = new Array();

        // Validate Gold Loan Valuation Amount 
        $('#tbl-gold-loan-photo > TBODY > TR').each(function () {
            let currentRow = $(this).closest('tr');

            columnvalue = columnvalue = (goldLoanCollateralDetailDataTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                return false;
            }
            else {
                goldcollateralValuationAmount = +columnvalue[29];

                let td0 = columnvalue[1];
                let td1 = columnvalue[5];

                goldOrnamentId.push({ td0, td1 });

                // Hide Photo Type Dropdown According Inputs
                if (columnvalue[11] == 'True')
                    $('#photo-type-id').find('option[value="DMG"]').removeClass('d-none');
                else
                    $('#photo-type-id').find('option[value="DMG"]').addClass('d-none');

                // Westage
                if (columnvalue[14] == 'True')
                    $('#photo-type-id').find('option[value="WST"]').removeClass('d-none');
                else
                    $('#photo-type-id').find('option[value="WST"]').addClass('d-none');

                // Diamond
                if (columnvalue[17] == 'True')
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
        let $mySelect = $('#gold-ornament-id-photo');
        $mySelect.html('');

        let options = '<option value="0"> Select Gold Ornament </option>';

        $.each(goldOrnamentId, function (key, value) {
            options += '<option value="' + value.td0 + '">' + value.td1 + '</option>';
        });

        $mySelect.append(options);
        $mySelect.prop('selectedIndex', 0);

        let schemeId = $('#scheme-id option:selected').val();

        // Get Phot Upload Range i.e. Minimum Or Maximum Normal Photo
        if (schemeId != '') {
            $.get('/AccountChildAction/GetLoanSchemeDetailBySchemeId', { _schemeId: schemeId }, function (data, textStatus, jqXHR) {
                MinmumGoldPhoto = data.SchemeGoldLoanParameterViewModel.MinmumGoldPhoto,
                MaximumGoldPhoto = data.SchemeGoldLoanParameterViewModel.MaximumGoldPhoto

                let a = goldLoanCollateralPhotoDataTable.rows().count();

                if (a > 0 && a >= MinmumGoldPhoto && a == MaximumGoldPhoto) {
                    $('#gold-loan-photo-detail-modal').modal('hide');

                    alert('Upload Gold Collateral Photo Between' + MinmumGoldPhoto + ' And ' + MaximumGoldPhoto);
                }
                else {
                    if (MaximumGoldPhoto == 0)
                        $('#nominee-modal').modal('hide');
                    else
                        $('#gold-loan-photo-detail-modal').modal('show');
                }
            });
        }

        SetModalTitle('gold-loan-photo', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-gold-loan-photo-dt').click(function () {
        SetModalTitle('gold-loan-photo', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            debugger;
            columnValues = $('#btn-edit-gold-loan-photo-dt').data('rowindex');
            id = $('#gold-loan-photo-modal').attr('id');
            myModal = $('#' + id).modal();

            $('#gold-ornament-id-photo', myModal).val(columnValues[1]);
            $('#photo-type-id', myModal).val(columnValues[3]);
            // Display Value In Modal Inputs
            storagePathInput = columnValues[5];
            storagePathId = $(storagePathInput).attr('id');
            docPath = $('#' + storagePathId).get(0);
            files = docPath.files;

            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathTax = $('#photo-path-gold').get(0);
            photoPathTax.files = dt.files;

            photoinput = columnValues[6];
            photoPathTaxId = $(photoinput).attr('id');
            photoSrc = $('#' + photoPathTaxId).attr('src');

            $('#image-preview-gold').attr('src', photoSrc);

            $('#photo-caption-gold', myModal).val(columnValues[7]);
            $('#note-gold-loan-photo-detail', myModal).val(columnValues[8]);


            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-gold-loan-photo-dt').addClass('read-only');
            $('#gold-loan-photo-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-gold-loan-photo-modal').click(function (event) {
        if (IsValidGoldLoanPhotoDataTableModal()) {
            row = goldLoanPhotoDataTable.row.add([
                           tag,
                           sequenceNumber,
                           goldOrnamentText,
                           photoTypeId,
                           photoTypeText,
                           dochtml,
                           dochtml1,
                           photoCaption,
                           note,
                           customerGoldLoanCollateralPhotoPrmKey,
            ]).draw();

            files = photoPathTax.files;

            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];

                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathTax.files.length !== 0) {
                docPath.files = dt.files;
            }

            HideGoldLoanPhotoDataTableColumns();

            goldLoanPhotoDataTable.columns.adjust().draw();

            // Remove Required Error Message
            $('#gold-loan-photo-data-table-error').addClass('d-none');

            ClearModal('gold-loan-photo');

            $('#gold-loan-photo-modal').modal('hide');

            EnableNewOperation('gold-loan-photo');
        }
    });

    // Modal update Button Event
    $('#btn-update-gold-loan-photo-modal').click(function (event) {
        $('#select-all-gold-loan-photo').prop('checked', false);

        if (IsValidGoldLoanPhotoDataTableModal()) {
            goldLoanPhotoDataTable.row(selectedRowIndex).data([
                           tag,
                           sequenceNumber,
                           goldOrnamentText,
                           photoTypeId,
                           photoTypeText,
                           dochtml,
                           dochtml1,
                           photoCaption,
                           note,
                           customerGoldLoanCollateralPhotoPrmKey,
            ]).draw();

            files = photoPathTax.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            docPath = $('#' + newID).get(0);

            if (photoPathTax.files.length !== 0) {
                docPath.files = dt.files;
            }

            HideGoldLoanPhotoDataTableColumns();

            goldLoanPhotoDataTable.columns.adjust().draw();

            $('#gold-loan-photo-modal').modal('hide');

            EnableNewOperation('gold-loan-photo');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-gold-loan-photo-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-gold-loan-photo tbody input[type="checkbox"]:checked').each(function () {
                    goldLoanPhotoDataTable.row($('#tbl-fund tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    // ******* Check Usage Otherwise Remove It
                    // rowData = $('#btn-delete-fund-dt').data('rowindex');

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!goldLoanPhotoDataTable.data().any())
                        $('#gold-loan-photo-data-table-error').removeClass('d-none');

                    EnableNewOperation('gold-loan-photo');

                    $('#select-all-gold-loan-photo').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-gold-loan-photo').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-gold-loan-photo tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                // ******* Check Usage Otherwise Remove It
                row = $(this).closest('tr');

                selectedRowIndex = goldLoanPhotoDataTable.row(row).index();

                rowData = (goldLoanPhotoDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[5] });

                $('#btn-delete-gold-loan-photo-dt').data('rowindex', arr);

                // ***********************

                EnableDeleteOperation('gold-loan-photo')
            });
        }
        else {
            EnableNewOperation('gold-loan-photo')

            $('#tbl-gold-loan-photo tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-gold-loan-photo-loan-photo tbody').click('input[type=checkbox]', function () {
        $('#tbl-gold-loan-photo input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');

                selectedRowIndex = goldLoanPhotoDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (goldLoanPhotoDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[5] });

                    EnableEditDeleteOperation('gold-loan-photo');

                    $('#btn-update-gold-loan-photo-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-gold-loan-photo-dt').data('rowindex', rowData);
                    $('#btn-delete-gold-loan-photo-dt').data('rowindex', arr);
                    $('#select-all-gold-loan-photo').data('rowindex', arr);
                }
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-gold-loan-photo tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('gold-loan-photo');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('gold-loan-photo');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('gold-loan-photo');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-gold-loan-photo').prop('checked', true);
        else
            $('#select-all-gold-loan-photo').prop('checked', false);
    });

    // Validate  Fund Module
    function IsValidGoldLoanPhotoDataTableModal() {
        debugger;
        result = true;
        counter++;
        i = counter;
        newID = 'Path' + i;
        photoID = 'Photo' + i;
        columnValues = $('#btn-update-gold-loan-photo-detail').data('rowindex');

        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        sequenceNumber = $('#gold-ornament-id-photo option:selected').val();
        goldOrnamentText = $('#gold-ornament-id-photo option:selected').text();
        photoTypeId = $('#photo-type-id option:selected').val();
        photoTypeText = $('#photo-type-id option:selected').text();
        photo = $('#image-pre-view-gold').attr('src');
        photoPathTax = $('#photo-path-gold').get(0);
        dochtml = '<input type="file", id="' + newID + '", name = "DocPath", disabled="true"/>'
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />'

        photoCaption = $('#photo-caption-gold').val();
        note = $('#note-gold-loan-photo-detail').val();

        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        path = $('#photo-path-gold').val().trim();

        customerGoldLoanCollateralPhotoPrmKey = columnValues[3];

        if (sequenceNumber == '') {
            result = false;
            $('#gold-ornament-id-photo').after('<div class="error" style="color:red">Please Select Gold Ornament</div>');
        }
        if (photoTypeId == '') {
            result = false;
            $('#photo-type-id').after('<div class="error" style="color:red">Please Select Photo Type</div>');
        }
        return result;
    }

    // Hide Unnecessary Columns
    function HideGoldLoanPhotoDataTableColumns() {
        goldLoanPhotoDataTable.column(1).visible(false);
        goldLoanPhotoDataTable.column(3).visible(false);
        goldLoanPhotoDataTable.column(5).visible(false);
        goldLoanPhotoDataTable.column(8).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme Document - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-document-dt').click(function ()
    {    
        event.preventDefault();
        editedDocumentId = '';

        SetBusinessOfficeUniquePhotorecord();

        SetDocumentUniqueDropdownList();

        SetModalTitle('document', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-document-dt').click(function ()
    {
        debugger;
        SetModalTitle('document', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
            columnValues = $('#btn-edit-document-dt').data('rowindex');

            id = $('#document-modal').attr('id');
            myModal = $('#' + id).modal();

            editedDocumentId = columnValues[1];
            debugger;

            $('#sequence-number', myModal).val(columnValues[3]);
            $('#document-id', myModal).val(columnValues[1]);

            storagePathInput = columnValues[4];
            storagePathId = $(storagePathInput).attr('id');
            photoPath = $('#' + storagePathId).get(0);
            files = photoPath.files;

            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPathDocument = $('#photo-path-document').get(0);
            photoPathDocument.files = dt.files;

            photoinput = columnValues[5];
            photoDocumentId = $(photoinput).attr('id');
            photoSrc = $('#' + photoDocumentId).attr('src');

            $('#photo-path-document-image-preview').attr('src', photoSrc);

            $('#file-caption-document', myModal).val(columnValues[6]);
            $('#note-document', myModal).val(columnValues[7]);

            SetBusinessOfficeUniquePhotorecord(photoinput, storagePathInput);

            SetDocumentUniqueDropdownList();

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-document-edit-dt').addClass('read-only');
            $('#document-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-document-modal').click(function (event)
    {
        debugger;

        if (IsValidDocumentDataTableModal())
        {
            row = DocumentDataTable.row.add([
                        tag,
                        documentId,
                        documentText,
                        sequenceNumber,
                        dochtml,
                        dochtml1,
                        fileCaption,
                        note,
            ]).draw();

            files = photoPathDocument.files;

            // Check if files are present
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        )
                    );
                }

                // Handle the case where the photoPath document is empty
                photoPath = $('#' + newID).get(0);
                if (photoPath) {
                    // Set files only if photoPathDocument has files
                    if (files.length !== 0) {
                        photoPath.files = dt.files;
                    }
                }
            } else {
                // Handle the case where no files are uploaded
                photoPath = $('#' + newID).get(0);
                if (photoPath) {
                    // Reset files to empty if no files are uploaded
                    photoPath.files = new DataTransfer().files;
                }
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
    $('#btn-update-document-modal').click(function (event)
    {
        $('#select-all-document').prop('checked', false);

        if (IsValidDocumentDataTableModal())
        {
            documentDataTable.row(selectedRowIndex).data([
                          tag,
                          document,
                          documentText,
                          sequenceNumber,
                          dochtml,
                          dochtml1,
                          fileCaption,
                          note,
            ]).draw();

            files = photoPathDocument.files;
            if (files.length !== 0) {
                dt = new DataTransfer();
                for (j = 0; j < files.length; j++) {
                    f = files[j];
                    dt.items.add(
                        new File(
                            [f.slice(0, f.size, f.type)],
                            f.name
                        ));
                }
            }

            photoPath = $('#' + newID).get(0);

            if (photoPathDocument.files.length !== 0) {
                photoPath.files = dt.files;
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
    $('#btn-delete-document-dt').click(function (event)
    {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked)
        {
            if (confirm('Are You Sure To Delete This Row?'))
            {
                if ($('#tbl-document tbody input[type="checkbox"]:checked').each(function ()
                {
                    documentDataTable.row($('#tbl-document tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-document-dt').data('rowindex');

                    // Check Whether All Required Documents Added Or Not
                    IsAddedAllRequiredDocument();

                    EnableNewOperation('document');

                    SetDocumentUniqueDropdownList();

                   $('#select-all-document').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!documentDataTable.data().any())
                    {
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
    $('#select-all-document').click(function ()
    {
        if ($(this).prop('checked'))
        {
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
        $('#tbl-document input[type="checkbox"]:checked').each(function (index) {

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

    // Validate Kyc Module
    function IsValidDocumentDataTableModal() {
        debugger;
        result = true;
        counter++;
        i = counter;
        newID = "photoPathDocument" + i;
        photoID = "PhotoId" + i;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        // Capture the currently selected value and text
        documentId;
        documentText = NameOfDocument; // Get the text of the selected option


        sequenceNumber = $('#sequence-number').val();
        photo = $('#photo-path-document-image-preview').prop('src');
        photoPathDocument = $('#photo-path-document').get(0);
        dochtml = '<input type="file", id="' + newID + '", class="new-record", name = "PhotoPath", disabled="true"/>';
        dochtml1 = '<img src="' + photo + '", width="50", height="50", id= "' + photoID + '" />';
        fileCaption = $('#file-caption-document').val();
        note = $('#note-document').val();

        filename = $('input[type=file]').val().split('\\').pop();

        //Set Default Value if Empty
        if (fileCaption == '')
            fileCaption = 'None';

        if (note == '')
            note = 'None';


        // Validate Contact Type
        if (documentId == '') {
            result = false;
            $('#document-id-error').removeClass('d-none');
        }
        else
            $('#document-id-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideDocumentDataTableColumns() {
        documentDataTable.column(1).visible(false);
    }

    function SetBusinessOfficeUniquePhotorecord(photoinput) {
        // Iterate over each row in the document table
        $('#tbl-document > tbody > tr').each(function () {
            var currentRow = $(this);
            var columnValues = documentDataTable.row(currentRow).data();

            if (columnValues && columnValues[4]) {
                // Hide the option in the dropdown that matches the value in columnValues[4]
                $(photoinput).find('option[value="' + columnValues[5] + '"]').hide();
            }
        });
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@
    $('#btnsave').click(function () {
        debugger;

        let isValidAllInputs = true;
        let schemeId = $('#scheme-id option:selected').val();

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
            //let turnOverLimitArray = new Array();
            let noticeScheduleArray = new Array();
            let guarantorDetail = new Array();
            let collateralDetailArray = new Array();
            let goldLoanCollateralPhotoArray = new Array();


            // To Get All Records From Data Table
            jointAccountDataTable.page.len(-1).draw();
            nomineeDataTable.page.len(-1).draw();
            contactDataTable.page.len(-1).draw();
            addressDataTable.page.len(-1).draw();
            //turnOverLimitDataTable.page.len(-1).draw();
            noticeScheduleDataTable.page.len(-1).draw();

            // 1. Address Detail - Create Array For Person Address Detail Data Table To Pass Data
            if (!$('#heading-address-details').hasClass('d-none')) {
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
            if (!$('#heading-contact-details').hasClass('d-none')) {
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
            if (!$('#heading-joint-account').hasClass('d-none')) {
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
            if (!$('#heading-account-nominee').hasClass('d-none')) {
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
            //if ($('#enable-turn-over-limit').is(':checked')) {
            //    if (turnOverLimitDataTable.data().any()) {
            //        $('#turn-over-limit-accordion-error').addClass('d-none');

            //        if (isValidAllInputs) {
            //            // Get Data Table Values In Turn Over Limit Array
            //            $('#tbl-turn-over-limit > TBODY > TR').each(function () {
            //                currentRow = $(this).closest('tr');

            //                columnValues = (turnOverLimitDataTable.row(currentRow).data());

            //                // Handling Code If Row Is Undefined Or Null
            //                if (typeof columnValues != 'undefined' && columnValues != null) {
            //                    turnOverLimitArray.push({
            //                        'FrequencyId': columnValues[1],
            //                        'TransactionTypeId': columnValues[3],
            //                        'Amount': columnValues[5],
            //                        'ActivationDate': columnValues[6],
            //                        'ExpiryDate': columnValues[7],
            //                        'Note': columnValues[8],
            //                        'ReasonForModification': columnValues[9],
            //                    });
            //                }
            //                else
            //                    return false;
            //            });
            //        }
            //    }
            //    else {
            //        $('#turn-over-limit-accordion-error').removeClass('d-none');
            //        isValidAllInputs = false;
            //    }
            //}

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

            if (!IsValidVehicleLoanCollateralDetailAccordionInputs())
                isValidAllInputs = false;

            if (!IsValidCustomerVehicleLoanEconomicDetailAccordionInputs())
                isValidAllInputs = false;

            if (!IsValidVehicleInsuranceDetailAccordionInputs())
                isValidAllInputs = false;

            if (!IsValidFieldInvestigationAccordionInputs())
                isValidAllInputs = false;

            if (!IsValidPreOwnedVehicleLoanInspectionAccordionInputs())
                isValidAllInputs = false;

            // Validate Gold Loan Collateral Data Table        
            //if ($('.gold-collateral-detail').hasClass('d-none')) {
            //    isGoldLoanCollateralDataTableValid = true;
            //}
            //else {
            //    loanSanctionAmount = $('#sanction-amount').val();
            //    goldcollateralValuationAmount = 0;

            //    // Get Gold Loan Valuation Amount 
            //    $('#tbl-gold-collateral-detail> TBODY> TR').each(function () {
            //        currentRow = $(this).closest('tr');

            //        columnvalue = (goldLoanCollateralDetailDataTable.row(currentRow).data());

            //        // Handling Code If Row Is Undefined Or Null
            //        if (typeof columnvalue == 'undefined' && columnvalue == null)
            //            return false;
            //        else
            //            goldcollateralValuationAmount = +columnvalue[29];
            //    });

            //    // Validate Whether Gold Valuation Amount Is Larger Than Gold Sanction Loan Amount
            //    if (parseFloat(loanSanctionAmount) < parseFloat(goldcollateralValuationAmount)) {
            //        isGoldLoanCollateralDataTableValid = false;
            //        isValidGoldLoanValuation = false;
            //    }
            //    else {
            //        isGoldLoanCollateralDataTableValid = true;
            //        isValidGoldLoanValuation = true;
            //    }
            //}

            // Call Cantroller Save Data Table Method 
            if (isValidAllInputs) {
                debugger;
                $.ajax(
                    {   
                        url: customerAccountDataTableUrl,
                        type: 'POST',
                        dataType: 'json',
                        async: false,
                        data: { '_customerJointAccountHolder': jointAccountArray, '_customerAccountNominee': nomineeDetailArray, '_personContactDetail': contactDetailArray, '_personAddress': addressDetailArray, '_customerAccountNoticeSchedule': noticeScheduleArray, '_customerAccountGuarantorDetail': guarantorDetail, '_goldLoanCollateralDetail': collateralDetailArray },
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
