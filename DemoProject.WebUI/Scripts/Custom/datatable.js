'use strict'

// Parameter : 
//_dataTableName - It Is Name Of Data Table / Accordion
function CreateDataTable(_dataTableName)
{
    let tblObj = $('#tbl-' + _dataTableName).DataTable({
        'paging': true,
        'fixedHeader': true,
        'searching': true,
        'ordering': true,
        'filter': true,
        'destroy': true,
        'orderMulti': false,
        'bserverSide': true,
        'Processing': true,
        'sScrollY': '40vh',
        'sScrollX': '100%',
        'sScrollXInner': '110%',
        'scrollX': true,
        'scrollCollapse': true,
        'scroller': true,
        'lengthMenu': [[10, 25, 50, -1], [10, 25, 50, 'All']],
        'aaSorting': [],
        'order': [2, 'desc'],
        'dom': 'Bfrtip',
        'columnDefs': [{
            'targets': '_all',
            'defaultContent': "",
        }, ],
        'dom': '<"float-left"B><"float-right"f>rt<"row"<"col-sm-2"l><"col-sm-6"i><"col-sm-4"p>>',
        'buttons':
        [
            {
                text: 'Add',
                attr: { id: 'btn-add-' + _dataTableName + '-dt' },
                action: function (e, dt, node, config, type) { },
            },
            {
                text: 'Edit',
                attr: { id: 'btn-edit-' + _dataTableName + '-dt' },
                action: function (e, dt, node, config, String, indexes) { },
            },

            {
                text: 'Delete',
                attr: { id: 'btn-delete-' + _dataTableName + '-dt' },
                action: function (e, dt, node, config) { }
            },

        ],
    })

    // Css For Create/Add/New Data Table Buttons
    var btns = $('#btn-add' + '-' + _dataTableName + '-dt');
    btns.addClass(`btn btn-success btn-sm btn-add-${_dataTableName} `).append('<i class="fas fa-plus icon"></i>');
    $('.icon').css({ 'float': 'left', 'margin-right': '6px', 'margin-top': '5px' });
    btns.removeClass('dt-button');

    // Css For Edit Button
    var btns = $('#btn-edit' + '-' + _dataTableName + '-dt');
    btns.addClass(`btn btn-primary btn-sm btn-edit-${_dataTableName} read-only`).append('<i class="far fa-edit ml-2 icon"></i>');
    $('.icon').css({ 'float': 'left', 'margin-right': '6px', 'margin-top': '5px' });
    btns.removeClass('dt-button');

    // Css For Delete Button
    var btns = $('#btn-delete' + '-' + _dataTableName + '-dt');
    btns.addClass(`btn btn-danger btn-sm delete-all btn-delete-${_dataTableName} read-only`).append('<i class="fas fa-trash ml-2 icon"></i>');
    $('.icon').css({ 'float': 'left', 'margin-right': '6px', 'margin-top': '5px' });
    btns.removeClass('dt-button');

    $('.dt-buttons').next("label.btn-file").remove();
    var items = `<label class="btn btn-default-${_dataTableName}  btn-file mt-4" title="Import" pull-right><i class="fas fa-regular fa-file-excel fa-2x text-success "></i><input type="file" class="upload" style="display: none;"></label>`;
    $('.import').html(items);

    return tblObj;
}

// After Edit And Delete Opetaion Button Setting (i.e. Enable Create Operation)
function EnableNewOperation(_dataTableName) {
    ClearModal(_dataTableName);
    $('#btn-add-' + _dataTableName + '-dt').removeClass('read-only');
    $('#btn-delete-' + _dataTableName + '-dt').addClass('read-only');
    $('#btn-edit-' + _dataTableName + '-dt').addClass('read-only');
}

// On Clicking Checkbox Setting (i.e. Enable Create Operation)
function EnableEditDeleteOperation(_dataTableName) {
    $('#btn-add-' + _dataTableName + '-dt').addClass('read-only');
    $('#btn-delete-' + _dataTableName + '-dt').removeClass('read-only');
    $('#btn-edit-' + _dataTableName + '-dt').removeClass('read-only');
}

function EnableDeleteOperation(_dataTableName) {
    $('#btn-add-' + _dataTableName + '-dt').addClass('read-only');
    $('#btn-delete-' + _dataTableName + '-dt').removeClass('read-only');
    $('#btn-edit-' + _dataTableName + '-dt').addClass('read-only');
}

// Set Modal Title While Performing Operation i.e - Add And Edit
function SetModalTitle(_dataTableName, _title) {
    let modalId = _dataTableName + '-modal';

    if (_title == 'Add') {
        $('#btn-update-' + modalId).hide();
        $('#btn-add-' + modalId).show();

        $('h4.modal-title').text('Add');
    }
    else {
        $('#btn-update-' + modalId).show();
        $('#btn-add-' + modalId).hide();

        $('h4.modal-title').text('Edit');
    }

    ClearModal(modalId);

    $('#' + modalId).modal('show');
}

// Clear All Modals Error And Input Values Parameters :  _id Is Id Of Modal
function ClearModal(_dataTableName) {
    const today = new Date();

    //$('.modal-input').next('span.error').addClass('d-none');
    $('.modal-input-error').addClass('d-none');

    // Handle Radio Button Error
    $('.modal-input').val('');

    $('.modal-input-img-preview').attr('src', '');

    $('.modal-input-radio').prop('checked', false);

    $('.modal-switch-input').prop('checked', false);

    // Clear Select2 - Multiselect Dropdown Expect
    if (typeof modalObjSelect2 !== 'undefined')
        modalObjSelect2.val(null).trigger('change');

    // Jquery MultiSelect Clear
    if ($('[multiple = "multiple"]').hasClass('modal-multi-check-select')) {
        $('.ms-options-wrap > button').text('Please Select Proper Values');
        $('.ms-options-wrap > .ms-options > ul input[type="checkbox"]').prop('checked', false);
        $('.ms-options-wrap > .ms-options > .ms-search input').val('');
        $('li').removeClass('selected');

        if ($('.ms-options-wrap > .ms-options > .ms-selectall').hasClass('global'))
            $('.ms-options-wrap > .ms-options > .ms-selectall').text('Select all');
    }

    // Set Default Current Date
    $('.set-current-date').val(GetInputDateFormat(today));
}

// Show Hide Accordions Based On Enable Toggle Switch
function SetToggleSwitchBasedAccordions() {
    // Hide All Accordion Or Div Blocks Based On Toggle Switch
    $('[id$=block]').each(function (idx, input) {
        let blockInputId = $(input).attr('id');
        let enableInputId = '#enable-' + blockInputId.replace('-block', '');
        let isHideBlock = $(enableInputId).hasClass('hide-block');

        if ($(enableInputId).is(':checked') == false) {
            if (isHideBlock)
                $('#' + blockInputId).removeClass('d-none');
            else
                $('#' + blockInputId).addClass('d-none');
        }
        else {
            if (isHideBlock)
                $('#' + blockInputId).addClass('d-none');
            else
                $('#' + blockInputId).removeClass('d-none');
        }
    });
}

// Validate Bank Account Number By RBI
// 
function ValidateBankAccountNumber(accountNumber, bankPrefix)
{
    let result = true;
    let regExp = '^\d{9,18}$'; 
    // ^ :- Beginning of the string. 
    // [0-9] :- Match any character in the set.
    // {9,18} :- Match Between 9 to 18 of the preceding token.
    // $ :- End of the string.

    // Check if the account number is exactly 15 digits
    if (accountNumber.length < 10 || accountNumber.length > 18)
    {
        $('#bank-account-number-error').html('Account Number Length From 9 Digit To 18 Digit.')
        result = false;
    }

    // Check if the account number contains only numeric digits
    if (regExp.test(accountNumber))
    {
        $('#bank-account-number-error').html('Account Number Must Only Contain Digit.')
        result = false;
    }
}

$(document).ready(function () {
    var t = $('#example').DataTable(
    {
        //Search Lable text change
        'language':
        {
            sLengthMenu: 'Show _MENU_',
            search: '',
            searchPlaceholder: 'Search records'
        },

        'order': [[1, 'asc']],
        'paging': true,
        'responsive': true,
        'fixedHeader': true,
        'scrollY': '400px',
        'scrollX': true,
        autoWidth: false,

        'scrollCollapse': true,
        'iDisplayLength': 10,
    });

    //SrNO AutoIncrese
    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    // Set Default None On Page Load
    $('.default-none').each(function () {
        if ($(this).val().trim().length === 0) {
            $(this).val('None');
        }
    })
    // Clear Data Table  Modal Inputs
    $('.modal-input').focusout(function () {
        let inputId = $(this).attr('id') + '-error';

        $('#' + inputId).addClass('d-none');

    });

    // Clear Data Table  Modal Radio Button Inputs
    $('.modal-input-radio').focusout(function () {
        // Handle Radio Button Error (*** Don't Remove Space Before modal-input-radio)
        let inputId = $(this).attr('class').replace('modal-input-radio', '');
        inputId = inputId.trim() + '-error'

        $('#' + inputId).addClass('d-none');
    });

    // Hide Div Block Based On Enable Toggle Switch Input 
    $('.hide-block').change(function () {
        // Get Input Id
        let inputId = $(this).attr('id');

        // Get Div Id
        let divCodeBlockId = inputId.replace('enable-', '') + '-block';

        if ($(this).is(':checked'))
            $('#' + divCodeBlockId).addClass('d-none');
        else
            $('#' + divCodeBlockId).removeClass('d-none');

        // Hide Accordion Title Error 
        $('#' + divCodeBlockId).find('.accordion-title-error').addClass('d-none');

        // Clear Div Element // Space Is Necessary To Select All Input
        $.each($('#' + divCodeBlockId + ' :input'), function (idx, input) {
            let inputType = $(input).attr('type');

            if (inputType == 'checkbox' || inputType == 'radio') {
                inputId = $(input).attr('id');

                inputId = '#' + inputId;

                $(inputId).prop('checked', false);
            }
            else if (inputType == 'search') {
                inputId = $('#' + divCodeBlockId).find("select").map(function () { return this.id; }).get();

                let childCount = $(input).children().length;

                if (childCount > 0) {
                    inputId.forEach(function (id) {
                        $('#' + id + ' > option').prop('selected', false);
                    });
                }

                if (typeof objSelect2 !== 'undefined')
                    objSelect2.trigger('change');
            }
            else if (inputType !== 'undefined')
                $(input).val('');
        });
    });

    // Show Div Block Based On Enable Toggle Switch Input
    $('.show-block').change(function ()
    {
        // Get Input Id
        let inputId = $(this).attr('id');

        // Get Div Id
        let divCodeBlockId = inputId.replace('enable-', '') + '-block';

        if ($(this).is(':checked'))
            $('#' + divCodeBlockId).removeClass('d-none');
        else
            $('#' + divCodeBlockId).addClass('d-none');

        // Hide Accordion Title Error 
        $('#' + divCodeBlockId).find('.accordion-title-error').addClass('d-none');

        // Clear Div Element // Space Is Necessary To Select All Input
        $.each($('#' + divCodeBlockId + ' :input'), function (idx, input) {
            let inputType = $(input).attr('type');

            if (inputType == 'checkbox' || inputType == 'radio') {
                inputId = $(input).attr('id');

                inputId = '#' + inputId;

                $(inputId).prop('checked', false);
            }
            else if (inputType == 'search')
            {
                inputId = $('#' + divCodeBlockId).find("select").map(function () { return this.id; }).get();

                let childCount = $(input).children().length;

                if (childCount > 0) {
                    inputId.forEach(function (id) {
                        $('#' + id + ' > option').prop('selected', false);

                    });
                }

                if (typeof objSelect2 !== 'undefined')
                    objSelect2.trigger('change');
            }
            else if (inputType !== 'undefined')
                $(input).val('');

            // Set Default Value None
            if ($(input).hasClass('default-none'))
                $(input).val('None');
        });
    });

    // Minimum Number Validation
    // Clear Maximum Number And Increment By Inputs
    $('.min-number').focusout(function () {
        debugger;
        let minValueId = $(this).attr('id');
        let maxValueId = '';
        let midValueId = '';    // It May Be Increment By Or Default Or Multiple Of Value
        let defaultId = '';
        let multipleOfId = minValueId.replace('minimum-', '') + '-multiple-of';

        let myVal = $(this).val();
        let myMinVal = $(this).attr('min');
        let maxValue = 0;

        if ($('#' + minValueId).hasClass('real-number'))
            myVal = parseInt(myVal);

        if (parseFloat(myVal) < parseFloat(myMinVal)) {
            $(this).val(myMinVal);
            myVal = myMinVal;
        }

        // Get Maximum Value Of This Field
        maxValue = parseFloat($('#' + minValueId).attr('max'));

        // Set Maximmum To This Input If It Has Not Set Maximum Value.
        if (isNaN(maxValue) || maxValue == '')
            maxValue = $(maxValueId).attr('max');

        // Reset Value To Zero If It Excedded Maximum Limit
        if (parseFloat(myVal) > parseFloat(maxValue)) {
            $(this).val(0);
            myVal = 0;
        }

        if (minValueId.indexOf('start') == 0) {
            maxValueId = minValueId.replace('start', '#end');

            midValueId = '#' + minValueId.replace('start-', '') + '-increment-by';

            let a = $(midValueId).val();

            // Multiple Of
            if (typeof midValueId == 'undefined')
                midValueId = '#' + minValueId.replace('start-', '') + '-multiple-of';

            // Default
            if (typeof midValueId == 'undefined')
                midValueId = '#' + minValueId.replace('start', 'default');

            defaultId = '#' + minValueId.replace('start', 'default');

            // Assign This (Minimum) Value + 100 To Minimum Attribute Of Maximum Value  
            // To Maintain Difference Between At Least 100 Numbers
            $(maxValueId).prop('min', parseFloat(myVal) + parseFloat(100));
        }
        else {
            maxValueId = minValueId.replace('minimum', '#maximum');

            midValueId = '#' + minValueId.replace('minimum-', '') + '-increment-by';

            let midValue = $(midValueId).val();

            // Multiple Of Tenure
            if (typeof midValue == 'undefined')
                midValueId = '#' + minValueId.replace('minimum-', '') + '-multiple-of';

            // Assign Value Of Updated Above Id
            midValue = $(midValueId).val();

            // Default Value
            if (typeof midValue == 'undefined')
                midValueId = '#' + minValueId.replace('minimum', 'default');

            defaultId = '#' + minValueId.replace('minimum', 'default');

            // Assign This (Minimum) Value To Minimum Attribute Of Maximum Value  
            $(maxValueId).prop('min', parseFloat(myVal));
        }

        // Assign Maximum Value To Minimum - Max Attribute
        $(midValueId).prop('min', 1);

        $('#' + minValueId).prop('max', maxValue);

        // Multiple Of
        $('#' + multipleOfId).prop('min', 1);
        $('#' + multipleOfId).prop('max', myVal <= 0 ? 1 : myVal);

        // Clear Mid And Max Value
        if (parseFloat($(midValueId).val()) < parseFloat($(this).val()) || parseFloat($(this).val()) == parseFloat(myMinVal))
            $(midValueId).val('');

        // Clear Default Value If Exists
        if ($(defaultId).length > 0) {
            $(defaultId).val('');
            $(defaultId).prop('min', myVal);
        }

        // Clear Max Value
        if (parseFloat($(maxValueId).val()) < parseFloat($(this).val()))
            $(maxValueId).val('');
    });

    // Maximum Number Validation
    $('.max-number').focusout(function () {
        debugger;
        let minValueId = '';
        let maxValueId = $(this).attr('id');
        let midValueId = '';       // It May Be Increment By Or Default Value

        let minValue = 0;
        let maxValue = $(this).val();
        let midValue = 0;
        let myMaxVal = $(this).attr('max');

        // Set Max Value If Greater Than Max Value
        if (parseFloat(maxValue) > parseFloat(myMaxVal)) {
            $(this).val(myMaxVal);
            maxValue = myMaxVal;
        }

        // Require To Handle Only Minimum & Maximum Number
        if (maxValueId.indexOf('end') == 0) {
            minValueId = maxValueId.replace('end', '#start');

            midValueId = '#' + maxValueId.replace('end-', '') + '-increment-by';

            if (typeof midValueId == 'undefined')
                midValueId = '#' + maxValueId.replace('end-', '') + '-multiple-of';

            if (typeof midValueId == 'undefined')
                midValueId = '#' + maxValueId.replace('end', 'default');

            minValue = $(minValueId).val();

            // Set Multiple Or Increment By Maximum Value Upto 9999999 (Valid For Int Data Type)
            let tmpMaxValue = parseFloat((maxValue - minValue) / 100);

            $(midValueId).prop('max', tmpMaxValue > 9999999 ? 9999999 : tmpMaxValue);
        }
        else {
            minValueId = maxValueId.replace('maximum', '#minimum');

            midValueId = '#' + maxValueId.replace('maximum-', '') + '-increment-by';
            let midValue = $(midValueId).val();

            if (typeof midValue == 'undefined')
                midValueId = '#' + maxValueId.replace('maximum-', '') + '-multiple-of';
            else
                midValue = $(midValueId).val();
            if (typeof midValue == 'undefined')

                midValueId = '#' + maxValueId.replace('maximum', 'default');

            minValue = $(minValueId).val();

            // Set Minimum And Maximum Value To Default  
            $(midValueId).prop('min', minValue);
            $(midValueId).prop('max', $(this).val());
        }

        // Clear Default, Increment By, Multiple Of Value
        if (parseFloat($(midValueId).val()) < parseFloat($(this).val()))
            $(midValueId).val('');
    });

    // Real Number - Deny Decimal Point i.e. For Integer Numbe
    $('.real-number').focusout(function () {
        let inputVal = $(this).val();
        $(this).val(Math.floor(inputVal));
    });

    // Alpha Numeric Validation
    $('.alpha-numeric').focusout(function (e) {
        let $this = $(this);
        let inputValue = $this.val().trim(); // Trim leading/trailing spaces

        // Check if the input is valid (alphanumeric with spaces)
        if (inputValue.match(/^[a-zA-Z0-9 ]+$/)) {
            // Replace multiple spaces with a single space
            let sanitizedText = inputValue.replace(/\s+/g, ' ');

            // Convert to Title Case (capitalize first letter of each word)
            sanitizedText = sanitizedText.split(' ').map(word => {
                return word.charAt(0).toUpperCase() + word.slice(1).toLowerCase();
            }).join(' ');

            // Get the maximum length of the input field
            let maxLength = $this.attr('maxlength');

            // Truncate sanitizedText to the maxLength if necessary
            if (maxLength && sanitizedText.length > maxLength) {
                sanitizedText = sanitizedText.substring(0, maxLength);
            }

            // Set the sanitized value to the input
            $this.val(sanitizedText);
        } else {
            // If invalid input, clear the field
            $this.val('');
        }
    });

    // Numeric value upto 2 decimal places
    $('.allow-upto-two-decimal-place').focusout(function () {
        var value = $(this).val();

        // Allow only numbers and one decimal point with up to two decimals
        value = value.replace(/[^0-9.]/g, ''); // Remove any non-numeric or non-decimal characters
        var parts = value.split('.');

        // Limit to one decimal point and only two digits after it
        if (parts.length > 1) {
            parts[1] = parts[1].substring(0, 2); // Limit decimal to two digits
            value = parts.join('.');
        }

        // Update the input field with the corrected value
        $(this).val(value);

    });

    // Alpha validation on focusout
    $('.alpha').focusout(function () {
        let $this = $(this);
        let inputValue = $this.val().trim();

        // Sanitize the input: remove non-alphabetic characters, replace multiple spaces with a single space
        inputValue = inputValue.replace(/[^a-zA-Z ]/g, '').replace(/\s+/g, ' ');

        // Convert to Title Case
        inputValue = inputValue.toLowerCase().replace(/\b\w/g, function (char) {
            return char.toUpperCase();
        });

        // Get the maximum length of the input field
        let maxLength = $this.attr('maxlength');
        if (maxLength && inputValue.length > maxLength) {
            inputValue = inputValue.substring(0, maxLength);
        }

        $this.val(inputValue);  // Set the formatted value
    });

    //Title Case focusout
    $('.title-case').focusout(function (e)
    {
        let trimValue = $(this).val()

        // Replace multiple spaces with a single space
        trimValue = trimValue.replace(/\s+/g, ' ').trim();

        // Convert to Title Case
        let titleCaseValue = trimValue.toLowerCase().replace(/\b\w/g, function (char) {
            return char.toUpperCase();
        });

        $(this).val(titleCaseValue);
    });

    // Upper Case
    $('.upper-case').focusout(function ()
    {
        let input = $(this);
        let maxLength = input.attr('maxlength');

        // Convert the current value to uppercase and enforce maxlength
        let uppercaseValue = input.val().toUpperCase().slice(0, maxLength);

        // Set the uppercase value back to the input field
        input.val(uppercaseValue);
    });

    // Alpha With Single Space focusout
    $('.alpha-with-single-space').focusout(function (e) {
        let $this = $(this);
        let value = $this.val().trim();  // Get the trimmed value
        let alphaWithSpace = /^[a-zA-Z\s]*$/;

        // Check if the value contains only alphabetic characters and spaces
        if (!alphaWithSpace.test(value)) {
            $this.val('');  // Clear the input if invalid characters are found
            return;
        }

        // Remove multiple spaces between words and leading/trailing spaces
        value = value.replace(/\s+/g, ' ').replace(/^\s+/g, '');

        // Split into words and limit to two words
        let words = value.split(' ');
        if (words.length > 2) {
            value = words[0] + ' ' + words[1];  // Limit to two words
        }

        // Convert to Title Case
        let titleCaseValue = value.toLowerCase().replace(/\b\w/g, function (char) {
            return char.toUpperCase();
        });

        // Get the max length of the field and truncate if necessary
        let maxLength = $this.attr('maxlength');
        if (maxLength && titleCaseValue.length > maxLength) {
            titleCaseValue = titleCaseValue.substring(0, maxLength);
        }

        $this.val(titleCaseValue);  // Set the sanitized and formatted value
    });

    //Alpha Numeric With Dash Slash focusout
    $('.alpha-numeric-with-dash-slash').focusout(function ()
    {
        let $this = $(this);
        let inputValue = $this.val().trim();
        let regex_alphanumericwithdashslashspace = /^[a-z0-9\-\/ ]*$/i;

        // Check if the input contains only alphanumeric characters, dashes, slashes, and spaces
        if (regex_alphanumericwithdashslashspace.test(inputValue)) {
            // Replace multiple consecutive spaces with a single space
            inputValue = inputValue.replace(/\s{2,}/g, ' ');

            // Get the maximum length of the input field
            let maxLength = $this.attr('maxlength');

            // Truncate the inputValue to the maxLength if necessary
            if (maxLength && inputValue.length > maxLength) {
                inputValue = inputValue.substring(0, maxLength);
            }

            $this.val(inputValue); // Set the cleaned-up value
        } else {
            $this.val(''); // Clear the input if it doesn't match the regex
        }
    });

    // Alphabet With Underscore And Hypen
    $('.alpha-numeric-with-hyphen-underscore').focusout(function ()
    {
        let regex_policyNumber = /^[a-zA-Z0-9-_]*$/;

        let inputValue = $(this).val().trim();

        // Check if the input contains only alphanumeric characters, dashes, slashes, and spaces
        if (regex_policyNumber.test(inputValue)) {
            // Replace multiple consecutive spaces with a single space
            inputValue = inputValue.replace(/\s{2,}/g, ' ');

            // Get the maximum length of the input field
            let maxLength = $(this).attr('maxlength');

            // Truncate the inputValue to the maxLength if necessary
            if (maxLength && inputValue.length > maxLength) {
                inputValue = inputValue.substring(0, maxLength);
            }

            $(this).val(inputValue);
        }
        else {
            $(this).val('');
        }
    });

    //Alpha With Hyphen Apostrophge focusout
    $('.alpha-with-hyphen-apostrophe').focusout(function () {
         
        let inputValue = $(this).val().trim();
        let regex_alphanumericwithdashslashspace = /^[a-zA-Z\-\' ]*$/i;

        // Check if the input contains only alphanumeric characters, dashes, slashes, and spaces
        if (regex_alphanumericwithdashslashspace.test(inputValue)) {
            // Replace multiple consecutive spaces with a single space
            inputValue = inputValue.replace(/\s{2,}/g, ' ');

            // Get the maximum length of the input field
            let maxLength = $(this).attr('maxlength');

            // Truncate the inputValue to the maxLength if necessary
            if ( parseInt(inputValue.length) > parseInt(maxLength)) {
                inputValue = inputValue.substring(0, maxLength);
            }

            $(this).val(inputValue); // Set the cleaned-up value
        } else {
            $(this).val(''); // Clear the input if it doesn't match the regex
        }
    });

    $('.deny-multiple-space').focusout(function (e) {
        // Remove Multiple Space Between Words
        $(this).val($(this).val().replace(/\s+/g, ' '));

        // Work As Ltrim - Remove Left Side Space
        $(this).val($(this).val().replace(/^\s+/g, ''));
    });

    $('.auto-complete').keydown(function (event) {
        // Get the value of the input field
        var userInput = $(this).val();

        // Count the number of spaces in the input
        var spaceCount = (userInput.match(/ /g) || []).length;

        // Check if the key pressed is a space and if there is any text entered
        if (event.which === 32 && userInput.length === 0) {
            event.preventDefault();
        }

        // Check if there are already three single spaces
        if (spaceCount >= 3) {
            // Check if the key pressed is the backspace key And Input String Length Is Greater Than 20
            if (event.which === 8 && userInput.length > 20)
                // Clear the input value
                $(this).val('');

            // Prevent further input Except Arrow Key
            if (event.which < 35 || event.which > 40) {
                if (event.which !== 8)
                    event.preventDefault();
            }
            return;
        }

        // Check if the key pressed is a space
        if (event.which === 32) {
            // If the last character is also a space and there are already three spaces, prevent input
            if (userInput.charAt(userInput.length - 1) === ' ' && userInput.split(' ').length >= 3) {
                event.preventDefault();
            }
            // If the last character is a space and the second-to-last character is also a space, prevent input
            if (userInput.charAt(userInput.length - 1) === ' ' && userInput.charAt(userInput.length - 1) === ' ') {
                event.preventDefault();
            }
        }

        // Check if there are at least three single spaces and if the text after the second space is at least 30 characters long
        if (spaceCount === 1 && userInput.substring(userInput.lastIndexOf(' ') + 1).length >= 30) {
            var keycode = event.keyCode ? event.keyCode : event.which;

            if (keycode != 8)
                event.preventDefault();

            //if (keycode != 8  && keycode != 0 && keycode != 48 && keycode != 57) {
            //    event.preventDefault();
            //}
        }

    });

    $('.read-only').keydown(function (e) {
        // Skip Only Tab (9 - Keycode)
        if ($(this).hasClass('read-only') && e.which != 9)
            e.preventDefault();
    })

    // Default None 
    $('.default-none').focusout(function () {
        if ($(this).val().trim().length === 0)
            $(this).val('None');
    });

    // Default Zero
    $('.default-zero').focusout(function () {
        let myVal = $(this).val();

        if (isNaN(myVal))
            myVal = 0

        if (myVal < 1)
            $(this).val('0');
    });

    // Disable Resize Option Of Text Area Input
    $('textarea').css('resize', 'none');

    // Added by Rahul Kharat date 28.09.2024 //

    $('[data-toggle="collapse"]').click(function ()
    {
        var icon = $(this).find('.toggle-icon');

        // Check if this icon is currently down
        if (icon.hasClass('fa-angle-down'))
        {
            // Change all icons to down
            $('.toggle-icon').removeClass('fa-angle-up').addClass('fa-angle-down');

            // Then, switch the clicked one to up
            icon.removeClass('fa-angle-down').addClass('fa-angle-up');
        }
        else
        {
            // If already up, you can toggle it back to down
            icon.removeClass('fa-angle-up').addClass('fa-angle-down');
        }
    });
});