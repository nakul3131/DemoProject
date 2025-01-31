'use strict'
$(document).ready(function () {

    let datepart;
    let dd;
    let mm;
    let yyyy;

    // @@@@@@@@@@ Data Table Related Varible Declaration
    let tag = '';
    let id;
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
    let arr = new Array();


    //Validation Data Table

    let isValidActivationDate;
    let isValidExpiryDate;

    //BusinessOffice
    let businessOfficeId;
    let businessOfficeText;
    let activationDate;
    let expiryDate;
    let closeDate;
    let note;

    let generalLedgerPrmKey = 0;


    // M A I N     P A G E     I N P U T     V A L I D A T I O N 
    let url = window.location.href;
    let index = url.lastIndexOf('/') + 1;
    let filenameWithExtension = url.substr(index);
    let filename = filenameWithExtension.split('?')[0]; // <-- added this line
    let winname = filename;

    //Data Table Declaration
    let businessOfficeDataTable = CreateDataTable('business-office');


    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@ Business Office - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    var businessOffice = businessOfficeDataTable.data().any()
    if (businessOffice = true) {
        $('#tbl-business-office tbody input[type="checkbox"]').each(function () {
            $(this).prop('disabled', true);
            $('#select-all-business-office').prop('disabled', true);
        })


    }

    ClearModal('business-office');

    // DataTable Add Button 
    $('#btn-add-business-office-dt').click(function () {
        event.preventDefault();
        debugger;
        SetModalTitle('business-office', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-business-office-dt').click(function () {
        debugger;
        SetModalTitle('business-office', 'Edit');
        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-business-office-dt').data('rowindex');
            id = $('#business-office-modal').attr('id');
            myModal = $('#' + id).modal();
            //// Display Value In Modal Inputs
            // Get Only Activation Date
            datepart = columnValues[3].split(' ')[0];

            if (datepart.length === 0) {
                datepart = columnValues[3]
            }

            const t = new Date(datepart);

            today = t.toLocaleDateString("en-US");

            const date = ('0' + t.getDate()).slice(-2);
            const month = ('0' + (t.getMonth() + 1)).slice(-2);
            const year = t.getFullYear();

            if (isNaN(year) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                activationDate = [yyyy, mm, dd].join('-');
            }
            else {
                activationDate = [year, month, date].join('-');
            }

            // Get Only Expiry Date
            datepart = columnValues[4].split(' ')[0];

            if (datepart.length = 0) {
                datepart = columnValues[4]
            }

            const t1 = new Date(datepart);

            today1 = t1.toLocaleDateString("en-US");

            const date1 = ('0' + t1.getDate()).slice(-2);
            const month1 = ('0' + (t1.getMonth() + 1)).slice(-2);
            const year1 = t1.getFullYear();

            if (isNaN(year1) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                expiryDate = [yyyy, mm, dd].join('-');
            }
            else {
                expiryDate = [year1, month1, date1].join('-');
            }

            // Get Only Close Date
            datepart = columnValues[5].split(' ')[0];

            if (datepart.length = 0) {
                datepart = columnValues[5]
            }

            const t2 = new Date(datepart);

            today2 = t2.toLocaleDateString("en-US");

            const date2 = ('0' + t2.getDate()).slice(-2);
            const month2 = ('0' + (t2.getMonth() + 1)).slice(-2);
            const year2 = t2.getFullYear();

            if (isNaN(year2) == true) {
                // Split Date In Arry
                arr = datepart.split('-');

                dd = arr[0];
                mm = arr[1];
                yyyy = arr[2];

                closeDate = [yyyy, mm, dd].join('-');
            }
            else {
                closeDate = [year2, month2, date2].join('-');
            }

            // Display Value In Modal Inputs
            $('#business-office-id', myModal).val(columnValues[1]);
            $('#activation-date-business-office', myModal).val(activationDate);
            $('#expiry-date-business-office', myModal).val(expiryDate);
            $('#close-date-business-office', myModal).val(closeDate);
            $('#note-business-office', myModal).val(columnValues[6]);
            generalLedgerPrmKey = columnValues[7];

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('.btn-edit-business-office-edit-dt').addClass('read-only');
            $('#business-office-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-business-office-modal').click(function (event) {
        debugger;
        if (IsValidBusinessOfficeDataTableModal()) {
            row = businessOfficeDataTable.row.add([
                tag,
                businessOfficeId,
                businessOfficeText,
                activationDate,
                expiryDate,
                closeDate,
                note,
                generalLedgerPrmKey
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            // Error Message In Span
            $('#business-office-validation span').html('');

            HideBusinessOfficeDataTableColumns();

            businessOfficeDataTable.columns.adjust().draw();

            ClearModal('business-office');

            $('#business-office-modal').modal('hide');

            EnableNewOperation('business-office');
        }
    });

    // Modal update Button Event
    $('#btn-update-business-office-modal').click(function (event) {

        $('#select-all-business-office').prop('checked', false);
        if (IsValidBusinessOfficeDataTableModal()) {
            businessOfficeDataTable.row(selectedRowIndex).data([
                tag,
                businessOfficeId,
                businessOfficeText,
                activationDate,
                expiryDate,
                closeDate,
                note,
                generalLedgerPrmKey
            ]).draw();
            // Error Message In Span
            $('#business-office-validation span').html('');

            HideBusinessOfficeDataTableColumns();

            businessOfficeDataTable.columns.adjust().draw();

            $('#business-office-modal').modal('hide');

            EnableNewOperation('business-office');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-business-office-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($("#tbl-business-office tbody input[type='checkbox']:checked").each(function () {
                    businessOfficeDataTable.row($("#tbl-business-office tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-business-office-dt').data('rowindex');
                    EnableNewOperation('business-office');

                    $('#select-all-business-office').prop('checked', false);
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-business-office').click(function () {
        if ($(this).prop('checked')) {
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
        isCheckedAll = $('tbody input[type="checkbox"]');

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
    $('#tbl-business-office> tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (businessOfficeDataTable.row(currentRow).data());

        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#business-office-general-ledger-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    // Validate Agent Incentive Module
    function IsValidBusinessOfficeDataTableModal() {
        debugger;

        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        businessOfficeId = $('#business-office-id option:selected').val();
        businessOfficeText = $('#business-office-id option:selected').text();
        activationDate = $('#activation-date-business-office').val();
        expiryDate = $('#expiry-date-business-office').val();
        closeDate = $('#close-date-business-office').val();
        note = $('#note-business-office').val();

        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        isValidActivationDate = IsValidInputDate('#activation-date-business-office');
        isValidExpiryDate = IsValidInputDate('#expiry-date-business-office');

        // Validate Modal Inputs
        if ((businessOfficeId == '') || !isValidActivationDate || !isValidExpiryDate) {
            if (businessOfficeId == '')
                $('#business-office-id-error').removeClass('d-none');

            if (!isValidActivationDate)
                $('#activation-date-business-office-error').removeClass('d-none');

            if (!isValidExpiryDate)
                $('#expiry-date-business-office-error').removeClass('d-none');

            return false;
        }
        else
            return true;
    }

    // Hide Unnecessary Columns
    function HideBusinessOfficeDataTableColumns() {

        businessOfficeDataTable.column(1).visible(false);
        businessOfficeDataTable.column(5).visible(false);
    }



    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger
        let isValidAllInputs = true;

        if ($('form').valid()) {
            debugger;

            // not add event.preventDefault
            $('.lastrow').remove();

            let isBusinessOfficeVisible = $('#heading-business-office').hasClass('d-none') ? false : true;

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let businessOffice = new Array();

            businessOfficeDataTable.page.len(-1).draw();
            // Validation For DataTable (i.e. Check Whether Data Exists Or Not)
            //let isbusinessOfficeDataTableValid = businessOfficeDataTable.data().any();

            // Create Array For Business Office Data Table To Pass Data
            if (isBusinessOfficeVisible) {
                if (businessOfficeDataTable.data().any()) {
                    $('#business-office-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In Business Office Array

                        $('#tbl-business-office tbody tr').each(function () {
                            debugger;
                            currentRow = $(this).closest('tr');

                            columnValues = (businessOfficeDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                debugger;
                                businessOffice.push(
                                    {
                                        'BusinessOfficeId': columnValues[1],
                                        'ActivationDate': columnValues[3],
                                        'ExpiryDate': columnValues[4],
                                        'CloseDate': columnValues[5],
                                        'Note': columnValues[6],
                                        'GeneralLedgerPrmKey': columnValues[7]
                                    });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#business-office-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            debugger;

            // Call Cantroller Save Data Table Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: urlForDataTable,
                    type: 'POST',
                    data: { '_businessOffice': businessOffice },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'json',

                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured While Save Data In SaveDataTable Method!!! Error Message - ' + error.toString());
                    }

                });

            }
            else {
                event.preventDefault();
            }
        }
        else {
            isValidAllInputs = false;
        }
    });
});