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
    let row = 0;
    let rowData = 0;
    let checked;
    let columnValues;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow = 0;
    let meridian;
    let hours;
    let minutes;
    let arr = new Array();

    //PersonAddress
    let result = true;
    let note = '';
    let reasonForModification = '';
    let time = 0;

    //sms alert
    let personInformationParameterNoticeTypeId = '';
    let personInformationParameterNoticeTypeIdText = '';
    let appLanguageId = '';
    let appLanguageIdText = '';
    let ss;
    let sendingTime = 0;
    let scheduletime = [];

    // M A I N     P A G E     I N P U T     V A L I D A T I O N
    let url = window.location.href;
    let index = url.lastIndexOf('/') + 1;

    // Create DataTables
    let smsAlertDataTable = CreateDataTable('sms-alert');

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Function to add a new sending-time textbox
    let maxField = 10; // Input fields increment limitation
    let addButton = $('.add_button'); // Add button selector
    let wrapper = $('.field_wrapper'); // Input field wrapper
    let fieldHTML = '<div class="field_wrapper1"><div class="row"><div class="col-xs-11 col-sm-11 col-md-11" id="mydiv"><div class="form-group"><input type="time" id="virtual" class="form-control schedule-time mandatory-mark" name="field_name[]" value="" placeholder = "Enter Sending Time" required/> <div class="col-xs-1 col-sm-1 col-md-1" id="removebtn" style="margin-right:-6%; margin-top:-8.5%;float:right"><a href="javascript:void(0);" class="remove_button btn btn-danger"><i class="fas fa-minus"></i></a></div><span class="error-time-input-message-new modal-input-error">Please Select Schedule Time & Then Press Add Button Again</span></div></div></div></div></div>'; // New input field html 
    let x = 1; // Initial field counter is 1

    // Once add button is clicked
    $(addButton).click(function () {
        event.preventDefault();
        let time = $('#sending-time').val().trim(); // Get the value of the date input
        let fieldHTMLVal = $('.field_wrapper1 .schedule-time');
        let lastFieldVal = fieldHTMLVal.last().val();
        let lastField = fieldHTMLVal.last();

        // Check if date input is not empty
        if (time !== '') {
            // Check maximum number of input fields
            if (x < maxField) {
                x++; // Increase field counter
                if (lastFieldVal !== '') {
                    lastField.removeClass('time-input-error-new');
                    lastField.siblings('.error-time-input-message-new').hide();
                    $(wrapper).append(fieldHTML);
                }
                else {
                    lastField.addClass('time-input-error-new'); // Add error styling
                    lastField.siblings('.error-time-input-message-new').show();
                    x--;
                    event.preventDefault();
                }
            } else {
                alert('A maximum of ' + maxField + ' fields are allowed to be added.');
            }
        } else {
            $('#sending-time-error').removeClass('d-none').text('Please enter a date before adding a new Schedule Time');
        }
    });

    // Once remove button is clicked
    $(wrapper).on('click', '.remove_button', function (e) {
        e.preventDefault();
        $(this).closest('div.row').remove(); // Remove field html
        x--; // Decrease field counter
    });


    /// @@@@@@@@@@@@@@@@@@@@@@   Scheme SMS Alert  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-sms-alert-dt').click(function ()
    {
        event.preventDefault();

        $('#removebtn').removeClass('read-only');
        $('.field_wrapper1').html('');
        SetModalTitle('sms-alert', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-sms-alert-dt').click(function ()
    {
        SetModalTitle('sms-alert', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#sms-alert-modal').modal();

            columnValues = $('#btn-edit-sms-alert-dt').data('rowindex');

            [time, meridian] = columnValues[5].split(' ');
            [hours, minutes] = time.split(':');
            if (hours === '12') {
                hours = '00';
            }
            if (meridian === 'PM')
                hours = parseInt(hours, 10) + 12;
            sendingTime = hours + ':' + minutes;
            $('#removebtn').addClass('read-only');

            $('#alert-type-id', myModal).val(columnValues[1]);
            $('#notice-language-id', myModal).val(columnValues[3]);
            $('#sending-time', myModal).val(sendingTime);
            $('#note-sms-alert', myModal).val(columnValues[6]);
            $('#reason-for-modification-sms-alert', myModal).val(columnValues[7]);
            $('.field_wrapper1').html('');
            // Show Modals
            $('#sms-alert-modal').modal('show');
        }
        else {
            $('#btn-edit-sms-alert-edit-dt').addClass('read-only');
            $('#sms-alert-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-sms-alert-modal').click(function (event) {
        if (IsValidSmsAlertModal()) {
            for (var i = 0; i < scheduletime.length; i++) {
                row = smsAlertDataTable.row.add([
                    tag,
                    personInformationParameterNoticeTypeId,
                    personInformationParameterNoticeTypeIdText,
                    appLanguageId,
                    appLanguageIdText,
                    scheduletime[i],
                    note,
                    reasonForModification
                ]).draw();
            }
            // Error Message In Span
            $('#sms-alert-data-table-error').addClass('d-none');

            scheduletime = [];

            HideColumnsSmsAlertDataTable();

            smsAlertDataTable.columns.adjust().draw();

            $('#sms-alert-modal').modal('hide');

            EnableNewOperation('sms-alert');
        }
    });

    // Modal update Button Event
    $('#btn-update-sms-alert-modal').click(function (event) {
        debugger
        $('#select-all-sms-alert').prop('checked', false);
        if (IsValidSmsAlertModal()) {
            for (var i = 0; i < scheduletime.length; i++) {
                smsAlertDataTable.row(selectedRowIndex).data([
                    tag,
                    personInformationParameterNoticeTypeId,
                    personInformationParameterNoticeTypeIdText,
                    appLanguageId,
                    appLanguageIdText,
                    scheduletime[i],
                    note,
                    reasonForModification
                ]).draw();
            }
            HideColumnsSmsAlertDataTable();

            smsAlertDataTable.columns.adjust().draw();

            $('#sms-alert-modal').modal('hide');

            EnableNewOperation('sms-alert');

            scheduletime = [];
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-sms-alert-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-sms-alert tbody input[type="checkbox"]:checked').each(function () {
                    smsAlertDataTable.row($('#tbl-sms-alert tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-sms-alert-dt').data('rowindex');
                    EnableNewOperation('sms-alert');

                    $('#select-all-sms-alert').prop('checked', false);
                    if (!smsAlertDataTable.data().any())
                    $('#sms-alert-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-sms-alert').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-sms-alert tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = smsAlertDataTable.row(row).index();

                rowData = (smsAlertDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-sms-alert-dt').data('rowindex', arr);
                EnableDeleteOperation('sms-alert');
            });
        }
        else {
            EnableNewOperation('sms-alert');

            $('#tbl-sms-alert tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-sms-alert tbody').click('input[type=checkbox]', function () {
        $('#tbl-sms-alert input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = smsAlertDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (smsAlertDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('sms-alert');

                    $('#btn-update-sms-alert-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-sms-alert-dt').data('rowindex', rowData);
                    $('#btn-delete-sms-alert-dt').data('rowindex', arr);
                    $('#select-all-sms-alert').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-sms-alert tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('sms-alert');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('sms-alert');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('sms-alert');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-sms-alert').prop('checked', true);
        else
            $('#select-all-sms-alert').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidSmsAlertModal() {
        result = true;
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        personInformationParameterNoticeTypeId = $('#alert-type-id option:selected').val();
        personInformationParameterNoticeTypeIdText = $('#alert-type-id option:selected').text();
        appLanguageId = $('#notice-language-id option:selected').val();
        appLanguageIdText = $('#notice-language-id option:selected').text();
        sendingTime = $('#sending-time').val();
        note = $('#note-sms-alert').val();
        reasonForModification = $('#reason-for-modification-sms-alert').val();
        ss = $('#sms-div').hasClass('d-none');
        scheduletime = [];

        $('input.schedule-time').each(function () {

            let tt = $(this).val().split(':');

            if (tt !== "") {
                let h = parseInt(tt[0]);
                let m = parseInt(tt[1]);
                let array = [h, m];
                array = tt;
                let ampm = h >= 12 ? 'PM' : 'AM';
                h = h % 12;
                h = h ? +h : 12; // 0 should be 12
                h = h < 10 ? '0' + h : h;
                m = m < 10 ? '0' + m : m; // if minutes less than 10,    add a 0 in front of it ie: 6:6 -> 6:06
                let strTime = h + ':' + m + ' ' + ampm;
                scheduletime.push(strTime);
            }
        });

        let fieldHTMLVal = $('.field_wrapper1 .schedule-time');
        let lastFieldVal = fieldHTMLVal.last().val();
        let lastField = fieldHTMLVal.last();

        if (lastFieldVal === '') {
            lastField.addClass('time-input-error-new'); // Add error styling
            lastField.siblings('.error-time-input-message-new').show();
            result = false;
        }

        // Set Default Value, If Empty
        if (note === '') {
            $('#note-sms-alert').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-sms-alert').val('None');
            reasonForModification = 'None';
        }

        if (ss === true) {
            reasonForModification = 'None';
        }

        //alert type dropdown
        if ($('#alert-type-id').prop('selectedIndex') < 1) {
            result = false;
            $('#alert-type-id-error').removeClass('d-none');
        }

        //alert type dropdown
        if ($('#notice-language-id').prop('selectedIndex') < 1) {
            result = false;
            $('#notice-language-id-error').removeClass('d-none');
        }

        //sending Time
        if (sendingTime === '') {
            result = false;
            $('#sending-time-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsSmsAlertDataTable() {
        smsAlertDataTable.column(1).visible(false);
        smsAlertDataTable.column(3).visible(false);
    }


    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {

        let isValidAllInputs = true;

        //if ($('form').valid()
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personSMSAlertArray = new Array();

            // Create Array For person sms alert case Table To Pass Data
            if (smsAlertDataTable.data().any()) {
                if (isValidAllInputs) {

                    // Checkbox Click Event - On Checkbox Click For Edit And Delete
                    $('#tbl-sms-alert > tbody > tr').each(function () {
                        currentRow = $(this).closest('tr');

                        columnValues = (smsAlertDataTable.row(currentRow).data());

                        [time, meridian] = columnValues[5].split(' ');
                        [hours, minutes] = time.split(':');

                        if (hours === '12') {
                            hours = '00';
                        }

                        if (meridian === 'PM')
                            hours = parseInt(hours, 10) + 12;

                        sendingTime = hours + ':' + minutes;

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues !== 'undefined' && columnValues !== null) {
                            debugger;
                            personSMSAlertArray.push(
                                {
                                    'PersonInformationParameterNoticeTypeId': columnValues[1],
                                    'AppLanguageId': columnValues[3],
                                    'SendingTime': sendingTime,
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
                isValidAllInputs = false;
            }

            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: saveDataTableURL,
                    type: 'POST',
                    async: false,
                    data: {'_sMSAlert': personSMSAlertArray},
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person SMS Alert DataTable!!! Error Message - ' + error.toString());
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