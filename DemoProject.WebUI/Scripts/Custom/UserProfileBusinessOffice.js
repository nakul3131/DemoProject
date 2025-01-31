'use strict'
// Document Ready Function
$(document).ready(function () {
    debugger;
    // DECLARATION - OF PAGE GLOBAL VARIABLE
    let note = '';
    let closeDate;
    let activationDate;
    let expiryDate;

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
    let selectedRow;
    // validateEmail();

    let arr = new Array();

    let result = true;

    let userProfilePrmKey = 0;

    //business office                           
    let businessOfficeIdText = '';
    let businessOfficeId = '';
    let reasonForModification = '';

    // Create DataTables
    let businessOfficeDataTable = CreateDataTable('business-office');
    
    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Scheme  Business Office - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    var UserBusinessOffice = businessOfficeDataTable.data().any();
    if (UserBusinessOffice == true)
    {
        $('#tbl-business-office tbody input[type="checkbox"]').each(function () {
            $(this).prop('disabled', true);
        })

    }
    // DataTable Add Button 
    $('#btn-add-business-office-dt').click(function () {
        userProfilePrmKey = 0;
        event.preventDefault();
        SetModalTitle('business-office', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-business-office-dt').click(function () {
        debugger
        SetModalTitle('business-office', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-business-office-dt').data('rowindex');

            id = $('#business-office-modal').attr('id');
            myModal = $('#' + id).modal();

            activationDate = new Date(columnValues[3]);
            expiryDate = new Date(columnValues[4]);
            closeDate = new Date(columnValues[5]);

            $('#business-office-id').removeAttr('style');
            $('#business-office-id').removeAttr('multiple');
            $('.ms-options-wrap').hide();
            $('#business-office-id', myModal).val(columnValues[1]);
            $('#activation-date-business-office', myModal).val(GetInputDateFormat(activationDate));
            $('#expiry-date-business-office', myModal).val(GetInputDateFormat(expiryDate));
            $('#close-date-business-office', myModal).val(GetInputDateFormat(closeDate));
            $('#note-business-office', myModal).val(columnValues[6]);
            $('#reason-for-modification-business-office', myModal).val(columnValues[7]);
            userProfilePrmKey = columnValues[8];

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
        debugger
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
                    reasonForModification,
                    userProfilePrmKey
                ]).draw();

                //rowNum++;

                //row.nodes().to$().attr('id', 'tr' + rowNum);

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
        debugger;
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
                reasonForModification,
                userProfilePrmKey
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
                //selectedRow = businessOfficeDataTable.row(row).index();

                selectedRowIndex = businessOfficeDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (businessOfficeDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('business-office');

                $('#btn-update-business-office-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-business-office-dt').data('rowindex', rowData);
                $('#btn-delete-business-office-dt').data('rowindex', arr);
                $('#select-all-business-office').data('rowindex', arr);
            }
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

    // On Page Load - Hide All Dropdown Items Which Are Added In Data Table.
    $('#tbl-business-office > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (businessOfficeDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null) {
            $('#business-office-id').find("option[value='" + columnValues[1] + "']").hide();
            
        } else
            return true;
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

        if (businessOfficeId == '') {
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

   

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Page Loading Default Values
    function SetPageLoadingDefaultValues() {
        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Handling Save/Submit Click Event
    $('#btnsave').click(function ()
    {
       
        let isValidAllInputs = true;
        debugger;
        if ($('form').valid()) {
            $('.lastrow').remove();

            // Return List Object, Hence Create Array
            let businessOfficeArray = new Array();
            

            businessOfficeDataTable.page.len(-1).draw();
            
            
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
                                        'ReasonForModification': columnValues[7],
                                        'UserProfilePrmKey': columnValues[8],
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

            
            if (isValidAllInputs) {
                // Call Cantroller Save Method 
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: {
                        '_userProfileBusinessOffice': businessOfficeArray
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