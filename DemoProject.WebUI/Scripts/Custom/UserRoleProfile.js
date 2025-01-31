'use strict'
// Document Ready Function
$(document).ready(function () {
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

    // validateEmail();

    let arr = new Array();

    let result = true;

    let userProfilePrmKey = 0;

    

    //Role Profile
    let businessOfficeId = '';
    let businessOfficeText = '';
    let roleProfileId = '';
    let roleProfileText = '';
    let reasonForModification = '';

    // Create DataTables
   
    let roleProfileDataTable = CreateDataTable('role-profile');

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Role-profile - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@
    var roleprofile = roleProfileDataTable.data().any()
    if (roleprofile == true)
    {
        $('#tbl-role-profile tbody input[type="checkbox"]').each(function () {
            $(this).prop('disabled', true);
            $('#select-all-role-profile').prop('disabled', true);
        })

    }
    // DataTable Add Button 
    $('#btn-add-role-profile-dt').click(function () {
        userProfilePrmKey = 0;
        event.preventDefault();
        SetModalTitle('role-profile', 'Add');
    });
        
    // DataTable Edit Button 
    $('#btn-edit-role-profile-dt').click(function () {
        debugger;
        SetModalTitle('role-profile', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-role-profile-dt').data('rowindex');
            id = $('#role-profile-modal').attr('id');
            myModal = $('#' + id).modal();
            activationDate = new Date(columnValues[5]);
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
            userProfilePrmKey = columnValues[10];
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
        debugger;
        let roleProfileId = [];
        let roleProfileText = [];

        $('#user-role-profile-id option:selected').each(function () {
            roleProfileId.push($(this).val());
            roleProfileText.push($(this).text());
        });

        if (IsValidRoleProfileDataTableModal()) {
            
            for (let i = 0, j = 0; i < roleProfileId.length, j < roleProfileText.length; i++ , j++) {
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
                    reasonForModification,
                    userProfilePrmKey
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
            roleProfileDataTable.row(selectedRowIndex).data([
                tag,
                businessOfficeId,
                businessOfficeText,
                roleProfileId,
                roleProfileText,
                activationDate,
                expiryDate,
                closeDate,
                note,
                reasonForModification,
                userProfilePrmKey
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
            $(this).prop('checked', true);
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

        debugger;
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

        if (roleProfileId == '') {
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




    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Handling Save/Submit Click Event
    debugger
    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;

        if ($('form').valid()) {
            $('.lastrow').remove();

            // Return List Object, Hence Create Array
            
            let roleProfileArray = new Array();

            roleProfileDataTable.page.len(-1).draw();

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
                                debugger;
                                roleProfileArray.push(
                                    {
                                        'BusinessOfficeID': columnValues[1],
                                        'RoleProfileId': columnValues[3],
                                        'ActivationDate': columnValues[5],
                                        'ExpiryDate': columnValues[6],
                                        'CloseDate': columnValues[7],
                                        'Note': columnValues[8],
                                        'ReasonForModification': columnValues[9],
                                        'UserProfilePrmKey': columnValues[10],

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
                         '_userRoleProfile': roleProfileArray
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