'use strict'
// Document Ready Function
$(document).ready(function () {
    // DECLARATION - OF PAGE GLOBAL VARIABLE
    let note = '';
    let closeDate;
    let activationDate;
    let expiryDate;
    let isValidUserProfileName = true;
    let isValidEmailId = true;
    let isValidAlternateEmailId = true;
    
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

    // validateEmail();

    let arr = new Array();

    let result = true;

    //Menu
    let menuId = '';
    let menuText = '';
    let mainMenuId = '';
    let mainMenuText = '';
    let menuPrmKeyid = [];
    let menuPrmKeyText = [];

    //Special Permission
    let specialPermissionId = [];
    let specialPermissionIdText = [];
    let reasonForModification = '';

    //GeneralLedger
    let userProfilegeneralLedgerId = '';
    let userProfilegeneralLedgerText = '';

    //Transaction Limit
    let generalLedgerId = '';
    let generalLedgerIdText = '';
    let transactionTypeId = '';
    let transactionTypeText = '';
    let currencyId = '';
    let currencyIdText = '';
    let transactionCurrencyId = '';
    let transactionCurrencyIdText = '';
    let minimumAmountLimitForTransaction = 0;
    let maximumAmountLimitForTransaction = 0;
    let maximumNumberOfBackDaysForTransaction = 0;
    let minimumAmountLimitForVerification = 0;
    let maximumAmountLimitForVerification = 0;
    let maximumNumberOfBackDaysForVerification = 0;
    let minimumAmountLimitForAutoVerification = 0;
    let maximumAmountLimitForAutoVerification = 0;
    let maximumNumberOfBackDaysForAutoVerification = 0;

    //Role Profile
    let businessOfficeId = '';
    let businessOfficeText = '';
    let roleProfileId = '';
    let roleProfileText = '';

    //business office                           
    let businessOfficeIdText = '';

    // Create DataTables
    let businessOfficeDataTable = CreateDataTable('business-office');
    let currencyDataTable = CreateDataTable('currency');
    let generalLedgerDataTable = CreateDataTable('general-ledger');
    let menuDataTable = CreateDataTable('menu');
    let specialPermissionDataTable = CreateDataTable('special-permission');
    let transactionLimitDataTable = CreateDataTable('transaction-limit');
    let roleProfileDataTable = CreateDataTable('role-profile');
    
    // Load Default Values Of Pages (On Amend, Modify, Verify Operation)
    SetPageDefaultValues();
    SetPageLoadingDefaultValues();

    
    // Mobile Number Validation
     $('#mobile-number').change(function () {
        var value = $(this).val();
        // Check if the value is exactly 10 digits
        if (value.length !== 10) {
            $('#mobile-number-error').removeClass('d-none').text('Mobile Number must be exactly 10 digits.');
        } else {
            $('#mobile-number-error').addClass('d-none');
        }
    });

    // Alternate Mobile Number Validation
    $('#alternate-mobile-number').keypress(function (e) {
        let value = $(this).val();

        // Remove all whitespace characters
        value = value.replace(/\s+/g, '');

        // Allow only numbers and limit length to 10
        if (isNaN(e.key) || value.length >= 10) {
            e.preventDefault();
            return false;
        }
    });

    $('#mobile-number').focusout(function () {
        let alternatemobile = $('#alternate-mobile-number').val();
        let mobile = $(this).val()
        if (alternatemobile === mobile && alternatemobile != '' && mobile != '') {
            $('#alternate-mobile-number-error').removeClass('d-none');
        }
        else {
            $('#alternate-mobile-number-error').addClass('d-none');
        }
    });
    //Alternate Mobile Number Validation
        $('#alternate-mobile-number').focusout(function () {
            let mobile = $('#mobile-number').val();
            let mobilemail = $(this).val()
            if (mobile === mobilemail && mobile !='' && mobilemail !='') {
                $('#alternate-mobile-number-error').removeClass('d-none');
            }
            else {
                $('#alternate-mobile-number-error').addClass('d-none');
            }
        });

        $('#user-type-id').focusout(function () {
            debugger;
        $.ajax({
            type: 'post',
            url: getPersonDropdownByUserType,
            data: { _userTypeId: $('#user-type-id').val() },
            datatype: 'json',
            traditional: true,
            success: function (data) {
                $('#person-id').empty();
                let personList = "<option value='0'>--Select Person--</option>";
                for (let i = 0; i < data.length; i++) {
                    personList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
                }
                $('#person-id').append(personList);
            }
        });
    })

    // Page Loading Default Values
    function SetPageDefaultValues() {
        debugger;
        HideAccordionByRoleProfileId();
    }
    $('#role-profile-id').focusout(function (event) {
        debugger;
        HideAccordionByRoleProfileId();
    });

    function HideAccordionByRoleProfileId() {
        debugger;
        let selectedroleProfileId = $('#role-profile-id option:selected').val();

        $.get('/RoleProfile/GetRoleProfileAllowAllAccess', { _roleProfileId: selectedroleProfileId, async: false }, function (data, textStatus, jqXHR) {
            if (data.IsAllowAllAccessForBusinessOffice) {
                debugger;
                $('#heading-business-office-parameter').addClass('d-none');
            }
            else {
                $('#heading-business-office-parameter').removeClass('d-none');
            }
            if (data.IsAllowAllAccessForGeneralLedger) {
                debugger;
                $('#heading-user-profile-general-ledger').addClass('d-none');
            }
            else {
                $('#heading-user-profile-general-ledger').removeClass('d-none');
            }
            if (data.IsAllowAllAccessForMenu) {
                debugger;
                $('#heading-menu').addClass('d-none');
            }
            else {
                $('#heading-menu').removeClass('d-none');
            }
            if (data.IsAllowAllAccessForSpecialPermission) {
                debugger;
                $('#heading-special-permission').addClass('d-none');
            }
            else {
                $('#heading-special-permission').removeClass('d-none');
            }
            if (data.IsAllowAllTransactions) {
                debugger;
                $('#heading-transaction-limit').addClass('d-none');
            }
            else {
                $('#heading-transaction-limit').removeClass('d-none');
            }
            if (data.IsAllowAllAccessForBusinessOffice && data.IsAllowAllAccessForGeneralLedger && data.IsAllowAllAccessForMenu && data.IsAllowAllTransactions) {
                debugger;
                $('#heading-user-profile-role-profile').addClass('d-none');
            }
            else {
                $('#heading-user-profile-role-profile').removeClass('d-none');
            }
        });
    };


    $('#email-id').keydown(function (e) {
        if (e.keyCode === 32) {
            e.preventDefault();
            return false; // Stop event propagation
        }
    });

    $('#email-id').focusout(function () {
        debugger;
        let result = true;
        let errorContainer = $('#email-id-error');
        let alternateemail = $('#alternate-email-id').val();

        let email = $(this).val();

        // Convert input to lowercase 
        email = email.toLowerCase();
        $(this).val($(this).val().toLowerCase());

       
        if (email == "") {
            result = false;
            isValidEmailId = false;
            errorContainer.text('This is required field.');
            $('#email-id-error').removeClass('d-none');
        }


        let parts = email.split("@");
        let xyz = email.split("..");
        let localPart = parts[0];
        let domain = parts[1];
       
        
        // Check Email has @ 
        if (result && domain == undefined) {
            result = false;
            isValidEmailId = false;
            errorContainer.text('Email should have @.');
            $('#email-id-error').removeClass('d-none');
        }

        // Check Email has  Double @ Symbol
        if (result && (parts.length > 2)) {
            result = false;
            isValidEmailId = false;
            errorContainer.text('Email Should Not Contain More Than One @ Character.');
            $('#email-id-error').removeClass('d-none');
        }

        if (result && xyz != email)
        {
            result = false;
            isValidEmailId = false;
            errorContainer.text('Email should Not Have Consecutive Dots');
            $('#email-id-error').removeClass('d-none');
        }

        //Local Part Start With Character //@@check and replace code@@//
        if (/^[^a-zA-Z0-9_]/.test(localPart)) {
            result = false;
            isValidEmailId = false;
            errorContainer.text('Local Part Should Not Start With Symbols.');
            $('#email-id-error').removeClass('d-none');
        }

        //Local Part End With Character //@@check and replace code@@//
        if (/[^a-zA-Z0-9_]$/.test(localPart)) {
            result = false;
            isValidEmailId = false;
            errorContainer.text('Local Part Should Not End With Symbols.');
            $('#email-id-error').removeClass('d-none');
        }
        // Check Domain is empty or not
        if (result && domain =="") {
            result = false;
            isValidEmailId = false;
            errorContainer.text('Domain should not empty.');
            $('#email-id-error').removeClass('d-none');
        }

        let domainPart = domain.split(".");
        let mainLabel = domainPart[0];
        let dnsLabel = domainPart[1];
        
        if (result && email === alternateemail && email != '' && alternateemail != '') {
            $('#alternate-email-id-error').removeClass('d-none');
        } else {
            $('#alternate-email-id-error').addClass('d-none');
        }

        if (result && email.length > 320) {
            errorContainer.text('Email should be maximum 320 characters long.');
            result = false;
        }

        if (result && localPart.length > 64) {
            errorContainer.text('Local part should be maximum 64 characters long.');
            result = false;
        }

        if (result && localPart.length === 0) {
            errorContainer.text('Local part should not be empty.');
            result = false;
        }

        if (result && !/^[a-zA-Z0-9_.+-]+$/.test(localPart)) {
            errorContainer.text('Local part should only contain dot ".", underscore "_", dash "-", alphabets, and numbers.');
            result = false;
        }

       
        if (result && domain.length === 0) {
            errorContainer.text('Domain part should not be empty.');
            result = false;
        }

        if (result && domainPart.length > 192) {
            errorContainer.text('Domain part should be maximum 192 characters long.');
            result = false;
        }
       
        if (result) {

            if (dnsLabel != undefined) {

                if (result && dnsLabel === "") {
                    errorContainer.text('DNS label should not empty.');
                    result = false;
                }

                if (result && dnsLabel.length > 63) {
                    errorContainer.text('DNS label should be maximum 63 characters long.');
                    result = false;
                }

                if (result && dnsLabel.length === 0) {
                    errorContainer.text('DNS label should not be empty.');
                    result = false;
                }

                if (result && result && dnsLabel.length == undefined) {
                    errorContainer.text('Domain part should not be undefined.');
                    result = false;
                }
                //if (!dnsLabel.startsWith('.')) {
                //    errorContainer.text('DNS label should start with a dot ".".');
                //    result = false;
                //}

                if (result && !/^[a-zA-Z0-9_.+-]+$/.test(dnsLabel)) {
                    errorContainer.text('DNS label should only contain dot ".", underscore "_", dash "-", alphabets, and numbers.');
                    result = false;
                }
                

            }
            else {
                errorContainer.text('DNS label should not empty');
                result = false;

            }
        }

        if (result) {
            isValidEmailId = true;
            $('#email-id-error').addClass('d-none');
        }
        else {
            isValidEmailId = false;
            $('#email-id-error').removeClass('d-none');
        }

        //errorContainer.text('Email is valid!');
    });


    $('#alternate-email-id').keydown(function (e) {
        if (e.keyCode === 32) {
            e.preventDefault();
            return false; // Stop event propagation
        }
    });
    $('#alternate-email-id').focusout(function () {
        debugger;
        let result = true;
        let errorContainer = $('#alternate-email-id-error');
        let email = $('#email-id').val();

        let alternateemail = $(this).val();

        // Convert input to lowercase 
        alternateemail = alternateemail.toLowerCase();
        $(this).val($(this).val().toLowerCase());


        if (alternateemail == "") {
            result = false;
            isValidAlternateEmailId = false;
         }


        let parts = alternateemail.split("@");
        let xyz = alternateemail.split("..");
        let localPart = parts[0];
        let domain = parts[1];

        // Check Email has @ 
        if (result && domain == undefined) {
            result = false;
            isValidAlternateEmailId = false;
            errorContainer.text('Email should have @.');
            $('#alternate-email-id-error').removeClass('d-none');
        }

        // Check Email has  Double @ Symbol
        if (result && (parts.length > 2)) {
            result = false;
            isValidAlternateEmailId = false;
            errorContainer.text('Email Should Not Contain More Than One @ Character.');
            $('#alternate-email-id-error').removeClass('d-none');
        }

        if (result && xyz != alternateemail) {
            result = false;
            isValidAlternateEmailId = false;
            errorContainer.text('Email should Not Have Consecutive Dots');
            $('#alternate-email-id-error').removeClass('d-none');
        }

        //Local Part Start With Character //@@check and replace code@@//
        if (/^[^a-zA-Z0-9_]/.test(localPart)) {
            result = false;
            isValidAlternateEmailId = false;
            errorContainer.text('Local Part Should Not Start With Symbols.');
            $('#alternate-email-id-error').removeClass('d-none');
        }

        //Local Part End With Character //@@check and replace code@@//
        if (/[^a-zA-Z0-9_]$/.test(localPart)) {
            result = false;
            isValidAlternateEmailId = false;
            errorContainer.text('Local Part Should Not End With Symbols.');
            $('#alternate-email-id-error').removeClass('d-none');
        }



        // Check Domain is empty or not
        if (result && domain == "") {
            result = false;
            isValidAlternateEmailId = false;
            errorContainer.text('Domain should not empty.');
            $('#alternate-email-id-error').removeClass('d-none');
        }

        let domainPart = domain.split(".");
        let mainLabel = domainPart[0];
        let dnsLabel = domainPart[1];

        if (result && alternateemail === email && alternateemail != '' && email != '') {
            $('#alternate-email-id-error').removeClass('d-none');
            result = false;
            isValidAlternateEmailId = false;
        } else {
            $('#alternate-email-id-error').addClass('d-none');
        }

        if (result && alternateemail.length > 320) {
            errorContainer.text('Email should be maximum 320 characters long.');
            result = false;
        }

        if (result && localPart.length > 64) {
            errorContainer.text('Local part should be maximum 64 characters long.');
            result = false;
        }

        if (result && localPart.length === 0) {
            errorContainer.text('Local part should not be empty.');
            result = false;
        }

        if (result && !/^[a-zA-Z0-9_.+-]+$/.test(localPart)) {
            errorContainer.text('Local part should only contain dot ".", underscore "_", dash "-", alphabets, and numbers.');
            result = false;
        }

       
        if (result && domain.length === 0) {
            errorContainer.text('Domain part should not be empty.');
            result = false;
        }

        if (result && domainPart.length > 192) {
            errorContainer.text('Domain part should be maximum 192 characters long.');
            result = false;
        }

        if (result) {

            if (dnsLabel != undefined) {

                if (result && dnsLabel === "") {
                    errorContainer.text('DNS label should not empty.');
                    result = false;
                }

                if (result && dnsLabel.length > 63) {
                    errorContainer.text('DNS label should be maximum 63 characters long.');
                    result = false;
                }

                if (result && dnsLabel.length === 0) {
                    errorContainer.text('DNS label should not be empty.');
                    result = false;
                }

                if (result && result && dnsLabel.length == undefined) {
                    errorContainer.text('Domain part should not be undefined.');
                    result = false;
                }
                //if (!dnsLabel.startsWith('.')) {
                //    errorContainer.text('DNS label should start with a dot ".".');
                //    result = false;
                //}

                if (result && !/^[a-zA-Z0-9_.+-]+$/.test(dnsLabel)) {
                    errorContainer.text('DNS label should only contain dot ".", underscore "_", dash "-", alphabets, and numbers.');
                    result = false;
                }
                

            }
            else {
                errorContainer.text('DNS label should not empty');
                result = false;

            }
        }

        if (result) {
            isValidAlternateEmailId = true;
            $('#alternate-email-id-error').addClass('d-none');
        }
        else {
            isValidAlternateEmailId = false;
            $('#alternate-email-id-error').removeClass('d-none');
        }
    });


    //Validate Unique Name of User Profile
    $('#name-of-user-profile').focusout(function (event) {
        debugger;
        let nameOfUserProfile = $('#name-of-user-profile').val();

        $.get('/AccountChildAction/IsUniqueUserProfileName', { _nameOfUserProfile: nameOfUserProfile, async: false }, function (data, textStatus, jqXHR) {
            if (data && nameOfUserProfile != '') {
                isValidUserProfileName = true;
                $('#name-of-user-profile-error').addClass('d-none');
            }
            else {
                isValidUserProfileName = false;
                $('#name-of-user-profile-error').removeClass('d-none');
            }
        });
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FOCUSOUT FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // HomeBusinessOffice Input Validation
    $('.home-business-input').focusout(function () {
        IsValidHomeBusinessOfficeAccordionInputs();
    });

    // Trigger Validation When The Home Business Office Section Is Opened
    $('#collapse-home-business-office').on('shown.bs.collapse', () => {
        IsValidHomeBusinessOfficeAccordionInputs();
    });

    // Trigger Validation When The Home Business Office Section Is Opened
    $('#collapse-password-policy').on('shown.bs.collapse', () => {
        IsValidPasswordPolicyAccordionInputs();
    });

    // PasswordPolicy Accordion Input Validation
    $('.password-policy-input').focusout(function () {
        IsValidPasswordPolicyAccordionInputs();
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // 1. HomeBusinessOffice Accordion Input Validation
    function IsValidHomeBusinessOfficeAccordionInputs() {
        debugger;
        let activationDate = $('#activation-date').val();
        let expiryDate = $('#expiry-date').val();
        let homeBranch = $('#home-branch').val();

        let isValidActivationDate = IsValidInputDate('#activation-date');
        let isValidExpiryDate = IsValidInputDate('#expiry-date');

        let result = true;

       
        // Check if isValidActivationDate is a valid date string
        if (isValidActivationDate === false|| isValidActivationDate === '') {
            result = false;
        }
        
        // Check If Values Is Not Empty
        if (isValidExpiryDate === '')
            result = false;

        // Check If Values Is Not Empty
        if ($('#home-branch').val() === '')
            result = false;

         
        if (result) {
            $('#home-business-office-accordion-error').addClass('d-none');
        } else {
            $('#home-business-office-accordion-error').removeClass('d-none');
        }
        return result;
    }

    // 2. PasswordPolicy Accordion Input Validation
    function IsValidPasswordPolicyAccordionInputs() {

        let activationDate = $('#activation-date-password-policy').val();
        let expiryDate = $('#expiry-date-password-policy').val();
        let result = true;

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-password-policy');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-password-policy');

        // Check if isValidActivationDate is a valid date string
        if (isValidActivationDate === false || isValidActivationDate === '') {
            result = false;
        }

        // Check If Values Is Not Empty
        if (isValidExpiryDate === '')
            result = false;

        // Check If Values Is Not Empty
        if ($('#password-policy-id').val() === '')
            result = false;


        if (result)
            $('#password-policy-accordion-error').addClass('d-none');
        else
            $('#password-policy-accordion-error').removeClass('d-none');

        return result;
    }

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Menu - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-menu-dt').click(function () {
        event.preventDefault();
        SetModalTitle('menu', 'Add');

        $.ajax({
            type: 'post',
            url: homeBranchUrl,
            data: { homeBranchId: $('#home-branch').val() },
            datatype: 'json',
            traditional: true,
            success: function (data) {
                $('#model-menu-id').empty();
                //var MainMenuList = '<option value="All">--Select All Menu--</option>';
                let MainMenuList;
                for (let i = 0; i < data.length; i++) {
                    MainMenuList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
                }
                $('#model-menu-id').append(MainMenuList);
            }
        });

    });

    // DataTable Edit Button 
    $('#btn-edit-menu-dt').click(function () {
        SetModalTitle('menu', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-menu-dt').data('rowindex');
            id = $('#menu-modal').attr('id');
            myModal = $('#' + id).modal();

            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);
            closeDate = new Date(columnValues[5]);

            // Display Value In Modal Inputs
            $('#menu-id', myModal).val(columnValues[1]);
            $('#activation-date-menu', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-menu', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-menu', myModal).val(GetInputDateFormat(closeDate));
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
            for (let k = 0, l = 0; k < menuPrmKeyid.length, l < menuPrmKeyText.length; k++, l++) {
                row = menuDataTable.row.add([
                    tag,
                    menuPrmKeyid[k],
                    menuPrmKeyText[l],
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                    reasonForModification
                ]).draw();
            }

            $('#menu-data-table-error').addClass('d-none');

            HideMenuDataTableColumns();

            menuDataTable.columns.adjust().draw();


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

            HideMenuDataTableColumns();

            menuDataTable.columns.adjust().draw();

            $('#menu-modal').modal('hide');

            EnableNewOperation('menu');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-menu-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-menu tbody input[type="checkbox"]:checked').each(function () {
                    menuDataTable.row($('#tbl-menu tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

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
            alert('Please Select A CheckBox');
    });

    // Select All Check Box Click Event -  
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
    $('#tbl-menu tbody').click('input[type="checkbox"]', function () {
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
        if (checked.length == 1)
            EnableEditDeleteOperation('menu');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('menu');

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
        menuText = $('#menu-id option:selected').text();
        mainMenuId = $('#main-menu-id option:selected').val();
        mainMenuText = $('#main-menu-id option:selected').text();
        //submenuId = $('#sub-menu-id option:selected').val();
        //submenuText = $('#sub-menu-id option:selected').text();
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

        if ((menuId.trim().length < 36) || (activationDate == '')) {
            if (menuId.trim().length < 36)
                $('#model-menu-id').after('<div class="error" style="color:red">Please Select Name Of Menu.</div>');

            if (activationDate == '')
                $('#activation-date-menu').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

            result = false;

            $('#menu-id-error').removeClass('d-none');
        }
        else {
            menuPrmKeyid = [];
            menuPrmKeyText = [];
            if (menuId != undefined) {
                if (!menuPrmKeyid.includes(menuId)) {
                    menuPrmKeyid.push(menuId)
                    menuPrmKeyText.push(menuText)
                }
            }

            if (mainMenuId != undefined) {
                if (!menuPrmKeyid.includes(mainMenuId)) {
                    menuPrmKeyid.push(mainMenuId)
                    menuPrmKeyText.push(mainMenuText)
                }
            }

            $('#sub-menu-id option:selected').each(function () {
                menuPrmKeyid.push($(this).val());
                menuPrmKeyText.push($(this).text());
            });
            //$('#menu-id-error').addClass('d-none');
        }

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
        menuDataTable.column(7).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Special Permission - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-special-permission-dt').click(function () {
        event.preventDefault();

        SetModalTitle('special-permission', 'Add');

        $('#special-permission-id').hide();
        $('#special-permission-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-special-permission-dt').click(function () {
        SetModalTitle('special-permission', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-special-permission-dt').data('rowindex');
            id = $('#special-permission-modal').attr('id');
            myModal = $('#' + id).modal();

            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);
            closeDate = new Date(columnValues[5]);

            // Display Value In Modal Inputs
            $('#special-permission-id').removeAttr('style');
            $('#special-permission-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');
            $('#special-permission-id', myModal).val(columnValues[1]);
            $('#activation-date-special-permission', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-special-permission', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-special-permission', myModal).val(GetInputDateFormat(closeDate));
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
        let specialPermissionId = [];
        let specialPermissionIdText = [];

        $('#special-permission-id option:selected').each(function () {
            specialPermissionId.push($(this).val());
            specialPermissionIdText.push($(this).text());
        });

        if (IsValidSpecialPermissionDataTableModal()) {
            for (let i = 0, j = 0; i < specialPermissionId.length, j < specialPermissionIdText.length; i++, j++) {

                row = specialPermissionDataTable.row.add([
                    tag,
                    specialPermissionId[i],
                    specialPermissionIdText[j],
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                    reasonForModification,
                ]).draw();
                $('#special-permission-data-table-error').addClass('d-none');

                HideSpecialPermissionDataTableColumns();

                specialPermissionDataTable.columns.adjust().draw();

                $('#special-permission-modal').modal('hide');

                EnableNewOperation('special-permission');
            }
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

            // Hide the element with id 'business-office-id'
            $('#special-permission-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
            $('#special-permission-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();


            HideSpecialPermissionDataTableColumns();

            specialPermissionDataTable.columns.adjust().draw();

            $('#special-permission-modal').modal('hide');

            EnableNewOperation('special-permission');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-special-permission-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-special-permission tbody input[type="checkbox"]:checked').each(function () {
                    specialPermissionDataTable.row($('#tbl-special-permission tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-special-permission-dt').data('rowindex');
                    EnableNewOperation('special-permission');

                    $('#select-all-special-permission').prop('checked', false);

                    // Hide the element with id 'business-office-id'
                    $('#special-permission-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
                    $('#special-permission-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();


                    // Display Error, If Table Has Not Any Record
                    if (!specialPermissionDataTable.data().any())
                        $('#special-permission-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A CheckBox');
    });

    // Select All Check Box Click Event -  
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
    $('#tbl-special-permission tbody').click('input[type="checkbox"]', function () {
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

        if (specialPermissionIdText == '') {
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

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme  Business Office - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-business-office-dt').click(function () {
        event.preventDefault();
        SetModalTitle('business-office', 'Add');
        $('#business-office-id').hide();
        $('#business-office-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();

    });

    // DataTable Edit Button 
    $('#btn-edit-business-office-dt').click(function () {
        debugger;
        SetModalTitle('business-office', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            debugger;
            columnValues = $('#btn-edit-business-office-dt').data('rowindex');

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
            $('#reason-for-modification-business-office', myModal).val(columnValues[7]);

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-business-office-dt').addClass('read-only');
            $('#business-office-modal').modal('hide');
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
            for (let i = 0, j = 0; i < businessOfficeId.length, j < businessOfficeIdText.length; i++, j++) {
                row = businessOfficeDataTable.row.add([
                    tag,
                    businessOfficeId[i],
                    businessOfficeIdText[j],
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                    reasonForModification
                ]).draw();

                rowNum++;

                row.nodes().to$().attr('id', 'tr' + rowNum);

                $('#business-office-data-table-error').addClass('d-none');

                HideBusinessOfficeDataTableColumns()

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
                note,
                reasonForModification
            ]).draw();

            // Hide the element with id 'business-office-id'
            $('#business-office-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
            $('#business-office-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();


            HideBusinessOfficeDataTableColumns()

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

                    // Uncheck the checkbox with id 'select-all-business-office'
                    $('#select-all-business-office').prop('checked', false);

                    // Hide the element with id 'business-office-id'
                    $('#business-office-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
                    $('#business-office-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();



                    // Display Error, If Table Has Not Any Record
                    if (!businessOfficeDataTable.data().any())
                        $('#business-office-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select Any One CheckBox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Business Office Datatable
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

    // Validate Agent Incentive Module
    function IsValidBusinessOfficeDataTableModal() {
        debugger;
        result = true;
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        businessOfficeId = $('#business-office-id option:selected').val();
        businessOfficeIdText = $('#business-office-id option:selected').text();
        activationDate = $('#activation-date-business-office').val();
        expiryDate = $('#expiry-date-business-office').val();
        closeDate = $('#close-date-business-office').val();
        note = $('#note-business-office').val();
        reasonForModification = $('#reason-for-modification-business-office').val();

        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        if ((reasonForModification == '') || (reasonForModification == undefined))
            reasonForModification = 'None';

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-business-office');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-business-office');

        if (businessOfficeIdText == '') {
            result = false;
            $('#business-office-id-error').removeClass('d-none');
        } else
            $('#business-office-id-error').addClass('d-none');

        if (!isValidActivationDate) {
            result = false;
            $('#activation-date-business-office-error').removeClass('d-none');
        } else
            $('#activation-date-business-office-error').addClass('d-none');

        if (!isValidExpiryDate) {
            result = false;
            $('#expiry-date-business-office-error').removeClass('d-none');
        } else
            $('#expiry-date-business-office-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideBusinessOfficeDataTableColumns() {
        businessOfficeDataTable.column(1).visible(false);
        businessOfficeDataTable.column(5).visible(false);
        businessOfficeDataTable.column(7).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Currency - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-currency-dt').click(function () {
        event.preventDefault();
        debugger;
        SetModalTitle('currency', 'Add');
        $('#special-permission-id').hide();
        $('#special-permission-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-currency-dt').click(function () {
        SetModalTitle('currency', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-currency-dt').data('rowindex');
            id = $('#currency-modal').attr('id');
            myModal = $('#' + id).modal();

            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);
            closeDate = new Date(columnValues[5]);

            // Display Value In Modal Inputs
            $('#currency-id').removeAttr('style');
            $('#currency-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');
            $('#currency-id', myModal).val(columnValues[1]);
            $('#activation-date-currency', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-currency', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-currency', myModal).val(GetInputDateFormat(closeDate));
            $('#note-currency', myModal).val(columnValues[6]);



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
        let currencyId = [];
        let currencyIdText = [];

        $('#currency-id option:selected').each(function () {
            currencyId.push($(this).val());
            currencyIdText.push($(this).text());
        });

        if (IsValidCurrencyDataTableModal()) {
            for (let i = 0, j = 0; i < currencyId.length, j < currencyIdText.length; i++, j++) {

                row = currencyDataTable.row.add([
                    tag,
                    currencyId[i],
                    currencyIdText[j],
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                ]).draw();

                HideCurrencyDataTableColumns();

                currencyDataTable.columns.adjust().draw();

                $('#currency-modal').modal('hide');

                EnableNewOperation('currency');
            }
        }
    });

    // Modal update Button Event
    $('#btn-update-currency-modal').click(function (event) {

        $('#select-all-currency').prop('checked', false);
        if (IsValidCurrencyDataTableModal()) {
            currencyDataTable.row(selectedRowIndex).data([
                tag,
                currencyId,
                currencyIdText,
                activationDate,
                expiryDate,
                closeDate,
                note
            ]).draw();

            // Hide the element with id 'business-office-id'
            $('#currency-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
            $('#currency-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();


            HideCurrencyDataTableColumns();

            currencyDataTable.columns.adjust().draw();

            $('#currency-modal').modal('hide');

            EnableNewOperation('currency');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-currency-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-currency tbody input[type="checkbox"]:checked').each(function () {
                    currencyDataTable.row($('#tbl-currency tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-currency-dt').data('rowindex');
                    EnableNewOperation('currency');

                    $('#select-all-currency').prop('checked', false);

                    // Hide the element with id 'business-office-id'
                    $('#currency-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
                    $('#currency-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();


                    // Display Error, If Table Has Not Any Record
                    if (!businessOfficeDataTable.data().any())
                        $('#currency-data-table-error').removeClass('d-none');


                }));
            }
        }
        else
            alert('Please Select A CheckBox');
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
    $('#tbl-currency tbody').click('input[type="checkbox"]', function () {
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
        currencyId = $('#currency-id option:selected').val();
        currencyIdText = $('#currency-id option:selected').text();
        activationDate = $('#activation-date-currency').val();
        expiryDate = $('#expiry-date-currency').val();
        closeDate = $('#close-date-currency').val();
        note = $('#note-currency').val();
        if (note == '')
            note = 'None';

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-currency');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-currency');

        if (currencyId == '') {
            result = false;
            $('#currency-id-error').removeClass('d-none');
        } else
            $('#currency-id-error').addClass('d-none');

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

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Currency - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-general-ledger-dt').click(function () {
        event.preventDefault();
        SetModalTitle('general-ledger', 'Add');

        $('#user-profile-general-ledger-id').hide();
        $('#user-profile-general-ledger-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-general-ledger-dt').click(function () {
        SetModalTitle('general-ledger', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-general-ledger-dt').data('rowindex');
            id = $('#general-ledger-modal').attr('id');
            myModal = $('#' + id).modal();

            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);
            closeDate = new Date(columnValues[5]);

            // Display Value In Modal Inputs
            $('#user-profile-general-ledger-id').removeAttr('style');
            $('#user-profile-general-ledger-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');
            $('#user-profile-general-ledger-id', myModal).val(columnValues[1]);
            $('#activation-date-general-ledger', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-general-ledger', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-general-ledger', myModal).val(GetInputDateFormat(closeDate));
            $('#note-general-ledger', myModal).val(columnValues[6]);
            $('#reason-for-modification-general-ledger', myModal).val(columnValues[7]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-general-ledger-dt').addClass('read-only');
            $('#general-ledger-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-general-ledger-modal').click(function (event) {
        let userProfilegeneralLedgerId = [];
        let userProfilegeneralLedgerText = [];

        $('#user-profile-general-ledger-id option:selected').each(function () {
            userProfilegeneralLedgerId.push($(this).val());
            userProfilegeneralLedgerText.push($(this).text());
        });

        if (IsValidGeneralLedgerDataTableModal()) {
            for (let i = 0, j = 0; i < userProfilegeneralLedgerId.length, j < userProfilegeneralLedgerText.length; i++, j++) {
                row = generalLedgerDataTable.row.add([
                    tag,
                    userProfilegeneralLedgerId[i],
                    userProfilegeneralLedgerText[j],
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                    reasonForModification
                ]).draw();

                $('#general-ledger-data-table-error').addClass('d-none');

                HideGeneralLedgerDataTableColumns();

                generalLedgerDataTable.columns.adjust().draw();

                $('#general-ledger-modal').modal('hide');

                EnableNewOperation('general-ledger');
            }
        }
    });

    // Modal update Button Event
    $('#btn-update-general-ledger-modal').click(function (event) {

        $('#select-all-general-ledger').prop('checked', false);
        if (IsValidGeneralLedgerDataTableModal()) {
            generalLedgerDataTable.row(selectedRowIndex).data([
                tag,
                userProfilegeneralLedgerId,
                userProfilegeneralLedgerText,
                activationDate,
                expiryDate,
                closeDate,
                note,
                reasonForModification
            ]).draw();

            // Hide the element with id 'business-office-id'
            $('#user-profile-general-ledger-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
            $('#user-profile-general-ledger-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();


            HideGeneralLedgerDataTableColumns();

            generalLedgerDataTable.columns.adjust().draw();

            $('#general-ledger-modal').modal('hide');

            EnableNewOperation('general-ledger');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-general-ledger-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-general-ledger tbody input[type="checkbox"]:checked').each(function () {
                    generalLedgerDataTable.row($('#tbl-general-ledger tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-general-ledger-dt').data('rowindex');
                    EnableNewOperation('general-ledger');

                    $('#select-all-general-ledger').prop('checked', false);

                    // Hide the element with id 'business-office-id'
                    $('#user-profile-general-ledger-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
                    $('#user-profile-general-ledger-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();


                    // Display Error, If Table Has Not Any Record
                    if (!generalLedgerDataTable.data().any())
                        $('#general-ledger-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A CheckBox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Menu Datatable
    $('#select-all-general-ledger').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-general-ledger tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = generalLedgerDataTable.row(row).index();

                rowData = (generalLedgerDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-general-ledger-dt').data('rowindex', arr);
                EnableDeleteOperation('general-ledger')
            });
        }
        else {
            EnableNewOperation('general-ledger')

            $('#tbl-general-ledger tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-general-ledger tbody').click('input[type="checkbox"]', function () {
        $('#tbl-general-ledger input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = generalLedgerDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (generalLedgerDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('general-ledger');

                    $('#btn-update-general-ledger-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-general-ledger-dt').data('rowindex', rowData);
                    $('#btn-delete-general-ledger-dt').data('rowindex', arr);
                    $('#select-all-general-ledger').data('rowindex', arr);

                }

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

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-general-ledger > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (generalLedgerDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#user-profile-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });


    // Validate  Fund Module
    function IsValidGeneralLedgerDataTableModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        userProfilegeneralLedgerId = $('#user-profile-general-ledger-id option:selected').val();
        userProfilegeneralLedgerText = $('#user-profile-general-ledger-id option:selected').text();
        activationDate = $('#activation-date-general-ledger').val();
        expiryDate = $('#expiry-date-general-ledger').val();
        closeDate = $('#close-date-general-ledger').val();
        note = $('#note-general-ledger').val();
        reasonForModification = $('#reason-for-modification-general-ledger').val();
        if (note == '')
            note = 'None';

        if (reasonForModification == '')
            reasonForModification = 'None';

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-general-ledger');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-general-ledger');

        if (userProfilegeneralLedgerId == '' || userProfilegeneralLedgerId ==undefined) {
            result = false;
            $('#user-profile-general-ledger-id-error').removeClass('d-none');
        } else
            $('#user-profile-general-ledger-id-error').addClass('d-none');

            if (!isValidActivationDate) {
            result = false;
            $('#activation-date-general-ledger-error').removeClass('d-none');
        } else
            $('#activation-date-general-ledger-error').addClass('d-none');

        if (!isValidExpiryDate) {
            result = false;
            $('#expiry-date-general-ledger-error').removeClass('d-none');
        } else
            $('#expiry-date-general-ledger-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideGeneralLedgerDataTableColumns() {
        generalLedgerDataTable.column(1).visible(false);
        generalLedgerDataTable.column(5).visible(false);
        generalLedgerDataTable.column(7).visible(false);
    }


    //objSelect2.on('select2:close', function (e) {
    //    $('#user-profile-general-ledger-id-error').addClass('d-none');

    //});
    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Transaction Limit - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-transaction-limit-dt').click(function () {
        event.preventDefault();
        SetModalTitle('transaction-limit', 'Add');

        $('#transaction-type-id').hide();
        $('#transaction-type-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
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

            activationDate = new Date(columnValues[16]);
            expiryDate = new Date(columnValues[17]);
            closeDate = new Date(columnValues[18]);

            // Display Value In Modal Inputs
            $('#transaction-type-id').removeAttr('style');
            $('#transaction-type-id').removeAttr('multiple');
            $('.ms-options-wrap').css('display', 'none');
            $('#general-ledger-transaction-limit-id', myModal).val(columnValues[1]);
            $('#transaction-type-id', myModal).val(columnValues[3]);
            $('#currency-id-transaction-limit', myModal).val(columnValues[5]);
            $('#minimum-amount-transaction', myModal).val(columnValues[7]);
            $('#maximum-amount-transaction', myModal).val(columnValues[8]);
            $('#maximum-number-transaction', myModal).val(columnValues[9]);
            $('#minimum-amount-verification', myModal).val(columnValues[10]);
            $('#maximum-amount-verification', myModal).val(columnValues[11]);
            $('#maximum-number-verification', myModal).val(columnValues[12]);
            $('#minimum-amount-auto-verification', myModal).val(columnValues[13]);
            $('#maximum-amount-auto-verification', myModal).val(columnValues[14]);
            $('#maximum-number-auto-verification', myModal).val(columnValues[15]);
            $('#activation-date-transaction-limit', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-transaction-limit', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-transaction-limit', myModal).val(GetInputDateFormat(closeDate));
            $('#note-transaction-limit', myModal).val(columnValues[19]);
            $('#reason-for-modification-transaction-limit', myModal).val(columnValues[20]);

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
        let transactionTypeId = [];
        let transactionTypeText = [];

        $('#transaction-type-id option:selected').each(function () {
            transactionTypeId.push($(this).val());
            transactionTypeText.push($(this).text());
        });

        if (IsValidTransactionLimitDataTableModal()) {
            debugger;
            for (let i = 0, j = 0; i < transactionTypeId.length, j < transactionTypeText.length; i++, j++) {

                row = transactionLimitDataTable.row.add([
                    tag,
                    generalLedgerId,
                    generalLedgerIdText,
                    transactionTypeId[i],
                    transactionTypeText[j],
                    transactionCurrencyId,
                    transactionCurrencyIdText,
                    minimumAmountLimitForTransaction,
                    maximumAmountLimitForTransaction,
                    maximumNumberOfBackDaysForTransaction,
                    minimumAmountLimitForVerification,
                    maximumAmountLimitForVerification,
                    maximumNumberOfBackDaysForVerification,
                    minimumAmountLimitForAutoVerification,
                    maximumAmountLimitForAutoVerification,
                    maximumNumberOfBackDaysForAutoVerification,
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                    reasonForModification
                ]).draw();
                $('#transaction-limit-data-table-error').addClass('d-none');
                HideTransactionLimitDataTableColumns();

                transactionLimitDataTable.columns.adjust().draw();

                $('#transaction-limit-modal').modal('hide');

                EnableNewOperation('transaction-limit');
            }
        }
    });

    // Modal update Button Event
    $('#btn-update-transaction-limit-modal').click(function (event) {
        debugger;
        $('#select-all-transaction-limit').prop('checked', false);
        if (IsValidTransactionLimitDataTableModal()) {
            transactionLimitDataTable.row(selectedRowIndex).data([
                tag,
                generalLedgerId,
                generalLedgerIdText,
                transactionTypeId,
                transactionTypeText,
                transactionCurrencyId,
                transactionCurrencyIdText,
                minimumAmountLimitForTransaction,
                maximumAmountLimitForTransaction,
                maximumNumberOfBackDaysForTransaction,
                minimumAmountLimitForVerification,
                maximumAmountLimitForVerification,
                maximumNumberOfBackDaysForVerification,
                minimumAmountLimitForAutoVerification,
                maximumAmountLimitForAutoVerification,
                maximumNumberOfBackDaysForAutoVerification,
                activationDate,
                expiryDate,
                closeDate,
                note,
                reasonForModification,
            ]).draw();

            // Hide the element with id 'business-office-id'
            $('#transaction-type-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
            $('#transaction-type-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();

            HideTransactionLimitDataTableColumns();

            transactionLimitDataTable.columns.adjust().draw();

            $('#transaction-limit-modal').modal('hide');

            EnableNewOperation('transaction-limit');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-transaction-limit-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-transaction-limit tbody input[type="checkbox"]:checked').each(function () {
                    transactionLimitDataTable.row($('#tbl-transaction-limit tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-transaction-limit-dt').data('rowindex');
                    EnableNewOperation('transaction-limit');

                    $('#select-all-transaction-limit').prop('checked', false);

                    // Hide the element with id 'business-office-id'
                    $('#transaction-type-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
                    $('#transaction-type-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();

                    // Display Error, If Table Has Not Any Record
                    if (!transactionLimitDataTable.data().any())
                        $('#transaction-limit-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A CheckBox');
    });

    // Select All Check Box Click Event -  
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
    $('#tbl-transaction-limit tbody').click('input[type="checkbox"]', function () {
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
        columnValues = (transactionLimitDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#general-ledger-transaction-limit-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate  Fund Module
    function IsValidTransactionLimitDataTableModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        generalLedgerId = $('#general-ledger-transaction-limit-id option:selected').val();
        generalLedgerIdText = $('#general-ledger-transaction-limit-id option:selected').text();
        transactionTypeId = $('#transaction-type-id option:selected').val();
        transactionTypeText = $('#transaction-type-id option:selected').text();
        transactionCurrencyId = $('#currency-id-transaction-limit option:selected').val();
        transactionCurrencyIdText = $('#currency-id-transaction-limit option:selected').text();
        minimumAmountLimitForTransaction = $('#minimum-amount-transaction').val();
        maximumAmountLimitForTransaction = $('#maximum-amount-transaction').val();
        maximumNumberOfBackDaysForTransaction = $('#maximum-number-transaction').val();
        minimumAmountLimitForVerification = $('#minimum-amount-verification').val();
        maximumAmountLimitForVerification = $('#maximum-amount-verification').val();
        maximumNumberOfBackDaysForVerification = $('#maximum-number-verification').val();
        minimumAmountLimitForAutoVerification = $('#minimum-amount-auto-verification').val();
        maximumAmountLimitForAutoVerification = $('#maximum-amount-auto-verification').val();
        maximumNumberOfBackDaysForAutoVerification = $('#maximum-number-auto-verification').val();

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
            $('#general-ledger-transaction-limit-id-error').removeClass('d-none');
        } else
            $('#general-ledger-transaction-limit-id-error').addClass('d-none');

        //Transaction Type Id
        if (transactionTypeId == '' || transactionTypeId === undefined) {
            result = false;
            $('#transaction-type-id-error').removeClass('d-none');
        } else
            $('#transaction-type-id-error').addClass('d-none');

        //General Ledger Id
        if (transactionCurrencyId == '') {
            result = false;
            $('#currency-id-transaction-limit-error').removeClass('d-none');
        } else
            $('#currency-id-transaction-limit-error').addClass('d-none');


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
        if (maximumNumberOfBackDaysForTransaction == '' || maximumNumberOfBackDaysForTransaction < 1 || maximumNumberOfBackDaysForTransaction > 365) {
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
        if (maximumNumberOfBackDaysForVerification == '' || maximumNumberOfBackDaysForVerification < 1 || maximumNumberOfBackDaysForVerification > 365) {
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
        if (maximumNumberOfBackDaysForAutoVerification == '' || maximumNumberOfBackDaysForAutoVerification < 1 || maximumNumberOfBackDaysForAutoVerification > 365) {
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
        transactionLimitDataTable.column(18).visible(false);
        transactionLimitDataTable.column(20).visible(false);
    }

    $('#transaction-type-id').change(function () {
        debugger;
        var selectedValue = $(this).val();
        if (selectedValue && selectedValue.length > 0) {
            $('#transaction-type-id-error').addClass('d-none');
        } else {
            $('#transaction-type-id-error').removeClass('d-none');
        }
    });

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Role-profile - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-role-profile-dt').click(function () {
        event.preventDefault();
        SetModalTitle('role-profile', 'Add');

        $('#user-role-profile-id').hide();
        $('#user-role-profile-id').attr('multiple', 'multiple');
        $('.ms-options-wrap').show();
    });

    // DataTable Edit Button 
    $('#btn-edit-role-profile-dt').click(function () {
        SetModalTitle('role-profile', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-role-profile-dt').data('rowindex');
            id = $('#role-profile-modal').attr('id');
            myModal = $('#' + id).modal();

            expiryDate = new Date(columnValues[6]);
            closeDate = new Date(columnValues[7]);

            $('#user-role-profile-id').removeAttr('style');
            $('#user-role-profile-id').removeAttr('multiple');
            $('.ms-options-wrap').hide();
            $('#business-office-role-profile-id', myModal).val(columnValues[1]);
            $('#user-role-profile-id', myModal).val(columnValues[3]);
            $('#activation-date-role-profile', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-role-profile', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-role-profile', myModal).val(GetInputDateFormat(closeDate));
            $('#note-role-profile', myModal).val(columnValues[8]);
            $('#reason-for-modification-role-profile', myModal).val(columnValues[9]);

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-role-profile-dt').addClass('read-only');
            $('#role-profile-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-role-profile-modal').click(function (event) {
        let roleProfileId = [];
        let roleProfileText = [];

        $('#user-role-profile-id option:selected').each(function () {
            roleProfileId.push($(this).val());
            roleProfileText.push($(this).text());
        });

        if (IsValidRoleProfileDataTableModal()) {
            for (let i = 0, j = 0; i < roleProfileId.length, j < roleProfileText.length; i++, j++) {
                row = roleProfileDataTable.row.add([
                    tag,
                    businessOfficeId,
                    businessOfficeText,
                    roleProfileId[i],
                    roleProfileText[j],
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                    reasonForModification
                ]).draw();
                $('#role-profile-data-table-error').addClass('d-none');

                HideRoleProfileDataTableColumns();

                roleProfileDataTable.columns.adjust().draw();

                $('#role-profile-modal').modal('hide');

                EnableNewOperation('role-profile');
            }
        }
    });

    // Modal update Button Event
    $('#btn-update-role-profile-modal').click(function (event) {

        $('#select-all-role-profile').prop('checked', false);
        if (IsValidRoleProfileDataTableModal()) {
            roleProfileDataTable.row($(this).attr('rowindex')).data([
                tag,
                businessOfficeId,
                businessOfficeText,
                roleProfileId,
                roleProfileText,
                activationDate,
                expiryDate,
                closeDate,
                note,
                reasonForModification
            ]).draw();

            // Hide the element with id 'role-profile-id'
            $('#user-role-profile-id').hide();
            // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
            $('#user-role-profile-id').attr('multiple', 'multiple');
            // Show elements with class 'ms-options-wrap'
            $('.ms-options-wrap').show();


            HideRoleProfileDataTableColumns();

            roleProfileDataTable.columns.adjust().draw();

            $('#role-profile-modal').modal('hide');

            EnableNewOperation('role-profile');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-role-profile-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-role-profile tbody input[type="checkbox"]:checked').each(function () {
                    roleProfileDataTable.row($('#tbl-role-profile tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-role-profile-dt').data('rowindex');
                    EnableNewOperation('role-profile');

                    $('#select-all-role-profile').prop('checked', false);

                    // Hide the element with id 'role-profile-id'
                    $('#user-role-profile-id').hide();
                    // Set the 'multiple' attribute to 'multiple' for the element with id 'business-office-id'
                    $('#user-role-profile-id').attr('multiple', 'multiple');
                    // Show elements with class 'ms-options-wrap'
                    $('.ms-options-wrap').show();

                    // Display Error, If Table Has Not Any Record
                    if (!roleProfileDataTable.data().any())
                        $('#role-profile-data-table-error').removeClass('d-none');


                }));
            }
        }
        else
            alert('Please Select A CheckBox');
    });

    // Select All Check Box Click Event  
    $('#select-all-role-profile').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-role-profile tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = roleProfileDataTable.row(row).index();

                rowData = (roleProfileDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-role-profile-dt').data('rowindex', arr);
                EnableDeleteOperation('role-profile')
            });
        }
        else {
            EnableNewOperation('role-profile')

            $('#tbl-role-profile tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-role-profile tbody').click('input[type="checkbox"]', function () {
        $('#tbl-role-profile input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {

                row = $(this).closest('tr');

                selectedRowIndex = roleProfileDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (roleProfileDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('role-profile');

                    $('#btn-update-role-profile-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-role-profile-dt').data('rowindex', rowData);
                    $('#btn-delete-role-profile-dt').data('rowindex', arr);
                    $('#select-all-role-profile').data('rowindex', arr);
                }

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-role-profile tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('role-profile');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('role-profile');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('role-profile');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-role-profile').prop('checked', true);
        else
            $('#select-all-role-profile').prop('checked', false);
    });

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-role-profile > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (roleProfileDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null) {
            $('#business-office-role-profile-id').find("option[value='" + columnValues[1] + "']").hide();
            $('#user-role-profile-id').find("option[value='" + columnValues[3] + "']").hide();
        } else
            return true;
    });

    // Validate  Fund Module
    function IsValidRoleProfileDataTableModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        businessOfficeId = $('#business-office-role-profile-id option:selected').val();
        businessOfficeText = $('#business-office-role-profile-id option:selected').text();
        roleProfileId = $('#user-role-profile-id option:selected').val();
        roleProfileText = $('#user-role-profile-id option:selected').text();
        activationDate = $('#activation-date-role-profile').val();
        expiryDate = $('#expiry-date-role-profile').val();
        closeDate = $('#close-date-role-profile').val();
        note = $('#note-role-profile').val();
        reasonForModification = $('#reason-for-modification-role-profile').val();

        if (note == '')
            note = 'None';

        if ((reasonForModification == '') || (reasonForModification == undefined))
            reasonForModification = 'None';

        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-role-profile');
        let isValidExpiryDate = IsValidInputDate('#expiry-date-role-profile');

        if (businessOfficeId == '') {
            result = false;
            $('#business-office-role-profile-id-error').removeClass('d-none');
        } else
            $('#business-office-role-profile-id-error').addClass('d-none');

        if (roleProfileId=='' || roleProfileId== undefined) {
            result = false;
            $('#user-role-profile-id-error').removeClass('d-none');
        } else
            $('#user-role-profile-id-error').addClass('d-none');

        if (!isValidActivationDate) {
            result = false;
            $('#activation-date-role-profile-error').removeClass('d-none');
        } else
            $('#activation-date-role-profile-error').addClass('d-none');

        if (!isValidExpiryDate) {
            result = false;
            $('#expiry-date-role-profile-error').removeClass('d-none');
        } else
            $('#expiry-date-role-profile-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideRoleProfileDataTableColumns() {
        roleProfileDataTable.column(1).visible(false);
        roleProfileDataTable.column(3).visible(false);
        roleProfileDataTable.column(7).visible(false);
        roleProfileDataTable.column(9).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Page Loading Default Values
    function SetPageLoadingDefaultValues() {
        debugger;
        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Handling Save/Submit Click Event
    $('#btnsave').click(function () {
        let isValidAllInputs = true;

        if ($('form').valid()) {
            $('.lastrow').remove();

            // Return List Object, Hence Create Array
            let businessOfficeArray = new Array();
            let currencyArray = new Array();
            let generalLedgerArray = new Array();
            let menuArray = new Array();
            let specialPermissionArray = new Array();
            let transactionLimitArray = new Array();
            let roleProfileArray = new Array();

            businessOfficeDataTable.page.len(-1).draw();
            currencyDataTable.page.len(-1).draw();
            generalLedgerDataTable.page.len(-1).draw();
            menuDataTable.page.len(-1).draw();
            specialPermissionDataTable.page.len(-1).draw();
            transactionLimitDataTable.page.len(-1).draw();
            roleProfileDataTable.page.len(-1).draw();

            
            // Validate User Profile Name
            if (!isValidUserProfileName) {
                isValidUserProfileName = false;
                $('#name-of-user-profile-error').removeClass('d-none');
            }
            else
                $('#name-of-user-profile-error').addClass('d-none');
           
            
            // Validate Email
            if (!isValidEmailId) {
                isValidEmailId = false;
                $('#email-id-error').removeClass('d-none');
            }
            else
                $('#email-id-error').addClass('d-none');

            // Validate Alternate Email
            if (!isValidAlternateEmailId) {
                isValidAlternateEmailId = false;
                $('#alternate-email-id-error').removeClass('d-none');
            }
            else
                $('#alternate-email-id-error').addClass('d-none');

            // Accordion 1 - HomeBusinessOffice Validation, If Enable
            if (!IsValidHomeBusinessOfficeAccordionInputs())
                isValidAllInputs = false;

            // Accordion 2 - PasswordPolicy Validation, If Enable
            if (!IsValidPasswordPolicyAccordionInputs())
                isValidAllInputs = false;

            // Get Data Table Values In Business Office Password Policy  Array
            if (!$('#heading-business-office-parameter').hasClass('d-none')) {
                if (businessOfficeDataTable.data().any()) {
                    $('#business-office-data-table-error').addClass('d-none');
                    if (isValidAllInputs) {
                        $('#tbl-business-office TBODY TR').each(function () {
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
                                        'ReasonForModification': columnValues[7]
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

            // Get Data Table Values In Business Office Currency Array
            if (!$('#show-user-profile-currency-block').hasClass('d-none')) {
                if ($('#enable-show-user-profile-currency').is(':checked')) {
                    if (currencyDataTable.data().any()) {
                        $('#currency-data-table-error').addClass('d-none');
                        if (isValidAllInputs) {
                            $('#tbl-currency  TBODY TR').each(function () {
                                currentRow = $(this).closest('tr');

                                columnValues = (currencyDataTable.row(currentRow).data());

                                // Handling Code If Row Is Undefined Or Null
                                if (typeof columnValues != 'undefined' && columnValues != null) {


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
            }

            // Get Data Table Values In Business Office Menu Array
            if (!$('#heading-menu').hasClass('d-none')) {
                if (menuDataTable.data().any()) {
                    $('#menu-data-table-error').addClass('d-none');
                    if (isValidAllInputs) {

                        $('#tbl-menu TBODY TR').each(function () {

                            currentRow = $(this).closest('tr');

                            columnValues = (menuDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                menuArray.push(
                                    {
                                        'ModelMenuId': columnValues[1],
                                        'ActivationDate': columnValues[3],
                                        'ExpiryDate': columnValues[4],
                                        'CloseDate': columnValues[5],
                                        'Note': columnValues[6],
                                        'ReasonForModification': columnValues[7],
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
                        $('#tbl-special-permission TBODY TR').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (specialPermissionDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                specialPermissionArray.push(
                                    {
                                        'SpecialPermissionId': columnValues[1],
                                        'ActivationDate': columnValues[3],
                                        'ExpiryDate': columnValues[4],
                                        'CloseDate': columnValues[5],
                                        'Note': columnValues[6],
                                        'ReasonForModification': columnValues[7],
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
                            currentRow = $(this).closest('tr');

                            columnValues = (transactionLimitDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

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
                                        'ActivationDate': columnValues[16],
                                        'ExpiryDate': columnValues[17],
                                        'CloseDate': columnValues[18],
                                        'Note': columnValues[19],
                                        'ReasonForModification': columnValues[20]
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
            if (!$('#heading-user-profile-general-ledger').hasClass('d-none')) {
                if (generalLedgerDataTable.data().any()) {
                    $('#general-ledger-data-table-error').addClass('d-none');
                    if (isValidAllInputs) {

                        $('#tbl-general-ledger TBODY TR').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (generalLedgerDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                generalLedgerArray.push(
                                    {
                                        'GeneralLedgerID': columnValues[1],
                                        'ActivationDate': columnValues[3],
                                        'ExpiryDate': columnValues[4],
                                        'CloseDate': columnValues[5],
                                        'Note': columnValues[6],
                                        'ReasonForModification': columnValues[7],
                                    });
                            }
                            else
                                return false;

                        });
                    }
                }
                else {
                    $('#general-ledger-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Get Data Table Values In Business Office Application Number  Array
            if (!$('#heading-user-profile-role-profile').hasClass('d-none')) {
                if (roleProfileDataTable.data().any()) {
                    $('#heading-user-profile-role-profile').addClass('d-none');
                    if (isValidAllInputs) {

                        $('#tbl-role-profile TBODY TR').each(function () {

                            currentRow = $(this).closest('tr');

                            columnValues = (roleProfileDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {

                                roleProfileArray.push(
                                    {
                                        'BusinessOfficeID': columnValues[1],
                                        'RoleProfileId': columnValues[3],
                                        'ActivationDate': columnValues[5],
                                        'ExpiryDate': columnValues[6],
                                        'CloseDate': columnValues[7],
                                        'Note': columnValues[8],
                                        'ReasonForModification': columnValues[9]
                                    });
                            }
                            else
                                return false;

                        });
                    }
                }
                else {
                    $('#heading-user-profile-role-profile').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            if (isValidAllInputs) {
                // Call Cantroller Save Method 
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: {
                        '_userProfileBusinessOffice': businessOfficeArray, '_userProfileCurrency': currencyArray, '_userProfileGeneralLedger': generalLedgerArray,
                        '_userProfileMenu': menuArray,
                        '_userProfileSpecialPermission': specialPermissionArray, '_userProfileTransactionLimit': transactionLimitArray, '_userRoleProfile': roleProfileArray
                    },

                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',

                    success: function (data) {
                    },

                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Contact Details DataTable!!! Error Message - ' + error.toString());
                    }
                })
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
})