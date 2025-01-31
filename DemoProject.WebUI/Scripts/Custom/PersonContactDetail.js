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

'use strict';
$(document).ready(function () {

    // @@@@@@@@@@ Data Table Related letible Declaration
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
    let arr = new Array();
    let result = true;
    let isVerified = false;
    let filteredData;

    //PersonContact
    let contactType;
    let contactTypeId;
    let lastSelectedValue = '';
    let contactTypeText = '';
    let fieldValue;
    let verificationCode = '';
    let isDuplicateContact = false;
    let isMobile = false;
    let isEmail = false;
    let sysNameOfContactType = '';
    let note = '';
    let personAddressPrmKey = 0;
    let contactDetailPrmKey = 0;
    let reasonForModification = '';
    let editedContactNumber = 0;
    // All Values Get From ContactType And Account Class (Both Table Has Same Values)
    const WHATS_APP_NUMBER = 'WhatsAppNumber';
    const WORK_EMAIL = 'WorkEmail';
    const HOME_MAIL = 'HomeMail';
    const OTHER_MAIL = 'OtherMail';
    const MOBILE = 'Mobile';

    // Create DataTables
    let contactDataTable = CreateDataTable('contact');

    SetPageLoadingDefaultValues();

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   Focusout  Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Verification Code Visibility & Input Type Visibility Based On Contact Type
    $('#contact-type').focusout(function (event) {
        debugger;

        let currentValue = $(this).val();

        // If the value has changed from the initial value, clear the related fields
        if (currentValue !== lastSelectedValue) {
            // Clear input fields and reset verification state
            $('#field-value').val('');
            $('#is-verified').prop('checked', false);
            $('#note-contact-detail').val('');
            $('.modal-input-error').addClass('d-none');
        }
        lastSelectedValue = currentValue;

        // Get The Selected Contact Type
        contactTypeId = $('#contact-type option:selected').val();

        // Contact Type Wise Show Hide Inputs
        $.get('/PersonChildAction/GetSysNameOfContactTypeById', { _contactTypeId: contactTypeId, async: false }, function (data, textStatus, jqXHR) {
            debugger;
            sysNameOfContactType = data;
        });

        // Add Field Value Attribute Type As Number For Mobile Contact Type
        contactType = $('#contact-type option:selected').text();
        isMobile = contactType.includes('Mobile');
        isEmail = contactType.includes('Email');

        // Add Field Value Attribute Type As Number For Mobile Contact Type
        if (isMobile) {
            $('#field-value').attr('type', 'number');
            $('.is-verified-field').addClass('d-none');
            $('#send-code').removeClass('d-none');
            $('.verification-code').removeClass('d-none');
            $('#verification-token-error').addClass('d-none');
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

    // Sms Send - Display SMS Delilety Status
    $('#send-code').click(function (event) {
        debugger;
        let _mobileNumber = $('#field-value').val();
        $('#send-code').addClass('d-none');

        $.get('/SMS/SendVerificationToken', { MobileNumber: _mobileNumber, async: false }, function (data, textStatus, jqXHR) {
            if (data === 'success') {
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
            if (data === 'success') {
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

    //Field Value Validation Based On Contact Types
    $('#field-value').focusout(function (event) {
        debugger;
        let inputValue = $(this).val();
        let contactTypes = $('#contact-type').val();
        let errorMessage = '';
        // Clear any previous error message
        $('#field-value-error').addClass('d-none');

        // Fetch sysNameOfContactType based on selected contact type
        $.get('/PersonChildAction/GetSysNameOfContactTypeById', { _contactTypeId: contactTypes, async: false }, function (sysNameOfContactType) {

            let validations = {
                [WHATS_APP_NUMBER]: {
                    regex: /^\d{10}$/,
                    message: 'Please Enter Valid 10 Digit Mobile Number For WhatsApp.'
                },
                [MOBILE]: {
                    regex: /^\d{10}$/,
                    message: 'Please Enter Valid 10 Digit Mobile Number.'
                },
                [WORK_EMAIL]: {
                    regex: /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/,
                    message: 'Please Enter Valid Email.'
                },
                [HOME_MAIL]: {
                    regex: /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/,
                    message: 'Please Enter Valid Email.'
                },
                [OTHER_MAIL]: {
                    regex: /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/,
                    message: 'Please Enter Valid Email.'
                }
            };

            // Check the validation rules
            let validation = validations[sysNameOfContactType];
            if (validation && !validation.regex.test(inputValue)) {
                errorMessage = validation.message;
                $('#field-value-error').removeClass('d-none').text(errorMessage);
            } else {
                $('#field-value-error').addClass('d-none');
            }
        });
        // If Contact Type Is Mobile
        if (isMobile) {
            $('#verification-code').val('');

            if ($('#field-value').val().length === 10) {
                // Check Whether Enter Mobile Number Is Existed Or Not
                filteredData = contactDataTable.rows().indexes().filter(function (value, index) {
                    return contactDataTable.row(value).data()[3] == $('#field-value').val();

                });

                if (contactDataTable.rows(filteredData).count() > 0 && editedContactNumber !== $('#field-value').val()) {
                    isDuplicateContact = true;
                    $('#field-value-duplicate-error').removeClass('d-none');
                }
                else {
                    $('#field-value-duplicate-error').addClass('d-none');
                    isDuplicateContact = false;
                }

                if (isDuplicateContact === false) {
                    $('#send-code').removeClass('d-none');
                    $('#resend').addClass('d-none');
                    $('#field-value-error').addClass('d-none');
                }
            }
            else {
                $('#send-code').addClass('d-none');
                $('#field-value-error').addClass('d-none');
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

    $('#verification-code').focusout(function () {

        if ($(this).val() > 0)
            $('#verification-token-error').addClass('d-none');
    });

    function ResendSMS() {
        let mobileNumber = $('#field-value').val()
        debugger;
        $.get('/SMS/ReSendTeleVerificataionToken', { MobileNumber: mobileNumber, async: false }, function (data, textStatus, jqXHR) {
            debugger;
            if (data === 'success') {
                $(".link").fadeOut('slow').delay(30000).fadeIn("slow");
                $("#myToast").toast('show').css({ "z-index": "100", 'margin-top': "1%" });
            }
        });

    }

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // @@@@@@@@@@@@@@@@@@@@@@   Contact Details - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-contact-dt').click(function () {
        event.preventDefault();
        editedContactNumber = 0;
        isMobile = false;
        isEmail = false;
        sysNameOfContactType = '';
        $('#field-value-error').text('');
        $('#btn-add-contact-modal').removeClass('read-only');
        $('#send-code').addClass('d-none');
        $('#resend').addClass('d-none');
        $('.verification-code').addClass('d-none');
        SetModalTitle('contact', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-contact-dt').click(function () {
        debugger
        SetModalTitle('contact', 'Edit');
        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#contact-modal').modal();

            columnValues = $('#btn-edit-contact-dt').data('rowindex');

            //// Display Value In Modal Inputs
            isMobile = columnValues[2].includes('Mobile');
            isEmail = columnValues[2].includes('Email');

            $('#resend').addClass('d-none');

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
            lastSelectedValue = columnValues[1]
            editedContactNumber = columnValues[3];

            // Set Maximum Attributes
            $.get('/PersonChildAction/GetSysNameOfContactTypeById', { _contactTypeId: columnValues[1], async: false }, function (data) {
                debugger
                sysNameOfContactType = data;

                // Set Maximum Attributes
                if (WHATS_APP_NUMBER.includes(sysNameOfContactType)) { inputField.attr({ 'type': 'number', 'maxlength': 10 }); }
            });

            $('#is-verified').prop('checked', columnValues[4].toString().toLowerCase() === 'true' ? true : false);

            $('#verification-code', myModal).val('');
            $('#note-contact-detail', myModal).val(columnValues[6]);
            $('#reason-for-modification-contact', myModal).val(columnValues[7]);

            // Show Modals
            $('#contact-modal').modal('show');
        }
        else {
            $('#btn-edit-contact-dt').addClass('read-only');
            $('#contact-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-contact-modal').click(function (event) {
        $('#verification-token-error').addClass('d-none');

        if (IsValidContactDataTableModal()) {
            $('#btn-add-contact-modal').prop('disabled', true);
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

                    $('#btn-add-contact-modal').prop('disabled', false);
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
                personAddressPrmKey,
                ]).draw();

                HideContactDataTableColumns();

                contactDataTable.columns.adjust().draw();

                // Hide Table Required Data Error Message
                $('#contact-accordion-error').addClass('d-none');

                $('#contact-modal').modal('hide');

                EnableNewOperation('contact');

                $('#btn-add-contact-modal').prop('disabled', false);
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
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-contact tbody input[type="checkbox"]:checked').each(function () {
                    contactDataTable.row($('#tbl-contact tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-contact-dt').data('rowindex');
                    EnableNewOperation('contact');

                    $('#select-all-contact').prop('checked', false);

                    // Display Error, If Table Has Not Any Record
                    if (contactDataTable.data().any() === false)
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
        if (checked.length === 0)
            EnableNewOperation('contact');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1) {

            //[ Modify by  SS :05/09/2024 Amend operation Edit Button Does not work resolve this problem ]
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
        debugger;
        result = true;
        $('#field-value-error').text('');

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        contactType = $('#contact-type option:selected').val();
        contactTypeText = $('#contact-type option:selected').text();
        fieldValue = $('#field-value').val();

        isVerified = $('#is-verified').is(':checked') ? true : false;
        note = $('#note-contact-detail').val();
        verificationCode = $('#verification-code').val();
        personAddressPrmKey = 0;
        reasonForModification = $('#reason-for-modification-contact').val();

        // Set Default Value, If Empty
        if (note === '') {
            note = 'None';
        }

        if (reasonForModification == '') {
            reasonForModification = 'None';
        }

        //vehicle model Id
        if ($('#contact-type').prop('selectedIndex') < 1) {
            result = false;
            $('#contact-type-error').removeClass('d-none');
        }

        // Validate If Contact Type Is Mobile
        if (isMobile) {
            // Define a regular expression pattern for a 10-digit mobile number
            let regex = /^\d{10}$/;
            let verificationCode = $('#verification-code').val();


            let filteredData = contactDataTable.rows().indexes().filter(function (value, index) {
                return contactDataTable.row(value).data()[3] == $('#field-value').val();
            });

            if (contactDataTable.rows(filteredData).count() > 0 && editedContactNumber !== $('#field-value').val()) {
                isDuplicateContact = true;
                result = false;
                $('#field-value-duplicate-error').removeClass('d-none');
            }
            else {
                isDuplicateContact = false;
                $('#field-value-duplicate-error').addClass('d-none');
            }


            // mobileNumber
            if (!regex.test(fieldValue)) {
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
            if (fieldValue === '' || parseInt(fieldValue.length) > 320) {
                result = false;
                $('#verification-token-error').addClass('d-none');
                $('#field-value-error').removeClass('d-none').text('Please Enter Field Value.');

            } else {
                verificationCode = '0';
                $('#verification-code').val(verificationCode);
            }

        }

        let inputValue = $('#field-value').val();
        let contactTypes = $('#contact-type').val();

        $.get('/PersonChildAction/GetSysNameOfContactTypeById', { _contactTypeId: contactTypes, async: false }, function (data) {

            sysNameOfContactType = data;
        });


        // Check if inputValue is empty
        if (inputValue === "") {
            result = false;
            $('#field-value-error').removeClass('d-none').text('Please Enter Field Value.');
        } else {
            // WhatsApp Number Validation
            if (sysNameOfContactType === WHATS_APP_NUMBER) {
                if (!/^\d{10}$/.test(inputValue)) {
                    result = false;
                    $('#field-value-error').removeClass('d-none').text('Please Enter Valid 10 Digit Mobile Number For WhatsApp.');
                } else {
                    $('#field-value-error').addClass('d-none');
                }
            }

                // Mobile Number Validation
            else if (sysNameOfContactType === MOBILE) {
                if (!/^\d{10}$/.test(inputValue)) {
                    result = false;
                    $('#field-value-error').removeClass('d-none').text('Please Enter Valid 10 Digit Mobile Number.');
                } else {
                    $('#field-value-error').addClass('d-none');
                }
            }

                // Home Email
            else if (sysNameOfContactType === HOME_MAIL) {
                let emailRegex = /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/;

                if (emailRegex.test(inputValue)) {
                    $('#field-value-error').addClass('d-none'); // Clear error if valid email is entered
                } else {
                    result = false;
                    $('#field-value-error').removeClass('d-none').text('Please Enter Valid Email.');
                }
            }

                // Other Email Types
            else if (sysNameOfContactType === OTHER_MAIL || sysNameOfContactType === WORK_EMAIL) {
                let emailRegex = /^(?!.*[._\-]{2})[a-z0-9](?:[a-z0-9._\-]{0,62}[a-z0-9])?@[a-z0-9](?:[a-z0-9\-]{0,190}[a-z0-9])?\.[a-z]{1,63}$/;

                if (emailRegex.test(inputValue)) {
                    $('#field-value-error').addClass('d-none'); // Clear error if valid email is entered
                } else {
                    result = false;
                    $('#field-value-error').removeClass('d-none').text('Please Enter Valid Email.');
                }
            }
        }


        return result;
    }

    // Hide Unnecessary Columns
    function HideContactDataTableColumns() {
        contactDataTable.column(1).visible(false);
        contactDataTable.column(8).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    function SetPageLoadingDefaultValues() {

        ResendSMS();
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        let isValidAllInputs = true;

        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let contactDetailArray = new Array();

            // 2. Contact Detail - Create Array For contact Data Table To Pass Data
            if (contactDataTable.data().any()) {

                if (isValidAllInputs) {
                    // Get Data Table Values In Turn Over Limit Array
                    $('#tbl-contact > TBODY > TR').each(function () {
                        currentRow = $(this).closest('tr');

                        columnValues = (contactDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues !== 'undefined' && columnValues !== null) {
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
                isValidAllInputs = false;
            }


            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: saveContactDetailDataTable,
                    type: 'POST',
                    async: false,
                    data: {
                        '_contactDetail': contactDetailArray,
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person Contact Detail DataTable!!! Error Message - ' + error.toString());
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