// Document Ready Function
'use strict';
$(document).ready(function () {

    // @@@@@@@@@@ Data Table Related Varible Declaration
    let tag = '';
    let id = '';
    let myModal;
    let rowNum = 0;
    let selectedRowIndex;
    let row;
    let note;
    let rowData;
    let checked;
    let columnValues;
    let columnValuesForSalaryStructure;

    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let result;
    let minimum = 0;
    let maximum = 0;

    let arr = new Array();
    let counter = 0;

    // Agenda
    let agendaId = 0;
    let agendaIdText = '';
    let sequenceNumber = 0;
    let suggestiveMemberNumber = '';
    let permissiveMemberNumber = '';
    
    // Board Of Director
    let boardOfDirectorId = 0;
    let boardOfDirectorText = '';
    let noticeReferenceNumber = 0;
    let noticeStatus = '';
    let attendanceStatus = '';
    
    // Invitee Member
    let customerSharesCapitalAccountId = 0;
    let customerSharesCapitalAccountText = '';
    
    // Notice
    let noticeMediaId = 0;
    let noticeMediaIdText = '';
    let scheduleId = 0;
    let scheduleIdText = '';
    let noticeFormatId = 0;
    let noticeFormatIdText = '';

    // CreateDataTable
    let agendaDataTable = CreateDataTable('agenda');
    let boardOfDirectorDataTable = CreateDataTable('board-of-director');
    let inviteeMemberDataTable = CreateDataTable('invitee-member');
    let noticeDataTable = CreateDataTable('notice');
 
    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************

    SetPageLoadingDefaultValues();

    //Set All Required Default Values On Page Loading
    function SetPageLoadingDefaultValues() {

        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();

    }



    // @@@@@@@@@@@@@@@@@@@@@@@@@@   E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   FOCUSOUT EVENT  @@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#next-meeting-date').focusout(function (event) {
        debugger;
        $.get('/Meeting/GetSharesApplicationPending', function (data) {
            debugger;
            rowNum = 0;
            if (data) {
                debugger;
                $.get('/Meeting/GetAgendaList', function (agendaList) {
                    debugger;
                    $('#meeting-agenda-data-table > tbody > tr').each(function () {
                        debugger;
                        let currentRow = $(this).closest('tr');

                        columnvalue = (meetingAgendaDataTable.row(currentRow).data());

                        if (typeof columnvalue == 'undefined' && columnvalue == null) {
                            debugger;
                            let tag = '<input type="checkbox" name="check_all" class="checks"/>';
                            let row = meetingAgendaDataTable.row.add([
                                       tag,
                                       agendaList[0].Value,
                                       agendaList[0].Text,
                                       0,
                                       true,
                                       0,
                                       0,
                                       'note'
                            ]).draw();

                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            meetingAgendaDataTable.column(1).visible(false);

                            meetingAgendaDataTable.columns.adjust().draw();
                            $('#agenda-id').find("option[value='" + agendaList[0].Value + "']").hide();
                        }
                    });
                })
            }
        });
    });


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Agenda - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-agenda-dt').click(function () {
        event.preventDefault();
        SetModalTitle('agenda', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-agenda-dt').click(function () {
        SetModalTitle('agenda', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-agenda-dt').data('rowindex');
            id = $('#agenda-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#agenda-id', myModal).val(columnValues[1]);
            $('#sequence-number', myModal).val(columnValues[3]);
            $('#suggestive-member-number', myModal).val(columnValues[4]);
            $('#permissive-member-number', myModal).val(columnValues[5]);
            $('#note-meeting-agenda', myModal).val(columnValues[6]);

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-agenda-dt').addClass('read-only');
            $('#agenda-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-agenda-modal').click(function (event) {
        if (IsValidAgendaDataTableModal()) {
            row = agendaDataTable.row.add([
                tag,
                agendaId,
                agendaIdText,
                sequenceNumber,
                suggestiveMemberNumber,
                permissiveMemberNumber,
                note,
            ]).draw();
            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideAgendaDataTableColumns()

            agendaDataTable.columns.adjust().draw();

            $('#agenda-data-table-error').addClass('d-none');

            EnableNewOperation('agenda');

            $('#agenda-modal').modal('hide');
        }
    });

    // Modal Update Button Event
    $('#btn-update-agenda-modal').click(function (event) {
        $('#select-all-agenda').prop('checked', false);
        if (IsValidAgendaDataTableModal()) {
            agendaDataTable.row(selectedRowIndex).data([
                tag,
                agendaId,
                agendaIdText,
                sequenceNumber,
                suggestiveMemberNumber,
                permissiveMemberNumber,
                note,
            ]).draw();

            HideAgendaDataTableColumns()

            agendaDataTable.columns.adjust().draw();

            EnableNewOperation('agenda');

            $('#agenda-modal').modal('hide');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-agenda-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-agenda tbody input[type="checkbox"]:checked').each(function () {
                    agendaDataTable.row($('#tbl-agenda tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    // Display Required Error Message, If Table Has Not Any Record
                    if (!agendaDataTable.data().any())
                        $('#agenda-data-table-error').removeClass('d-none');

                    EnableNewOperation('agenda');

                    $('#select-all-agenda').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Other Charges Datatable
    $('#select-all-agenda').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-agenda tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = agendaDataTable.row(row).index();
                rowData = (agendaDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-agenda-dt').data('rowindex', arr);

                EnableDeleteOperation('agenda');
            });
        }
        else {
            EnableNewOperation('agenda');

            $('#tbl-agenda tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-agenda tbody').click('input[type=checkbox]', function () {
        $('#tbl-agenda input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = agendaDataTable.row(row).index();
                rowData = (agendaDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('agenda');

                $('#btn-update-agenda-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-agenda-dt').data('rowindex', rowData);
                $('#btn-delete-agenda-dt').data('rowindex', arr);
                $('#select-all-agenda').data('rowindex', arr);
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        isCheckedAll = $('#tbl-agenda tbody input[type="checkbox"]');
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('agenda');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('agenda');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('agenda');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-agenda').prop('checked', true);
        else
            $('#select-all-agenda').prop('checked', false);
    });

    // Validate Agenda Module
    function IsValidAgendaDataTableModal() {
        debugger;
        result = true;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        agendaId = $('#agenda-id option:selected').val();
        agendaIdText = $('#agenda-id option:selected').text();
        sequenceNumber = $('#sequence-number').val();
        suggestiveMemberNumber = $('#suggestive-member-number').val();
        permissiveMemberNumber = $('#permissive-member-number').val();
        note = $('#note-meeting-agenda').val();
        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        // Validate General Ledger
        if (agendaId == '') {
            result = false;
            $('#agenda-id-error').removeClass('d-none');
        }
        else
            $('#agenda-id-error').addClass('d-none');

        // Validate Charges
        if (sequenceNumber == '' || parseInt(sequenceNumber) < 0 || parseInt(sequenceNumber) >65 ) {
            result = false;
            $('#sequence-number-error').removeClass('d-none');
        }
        else
            $('#sequence-number-error').addClass('d-none');

        //Validation Road Name
        if (suggestiveMemberNumber == '' || parseInt(suggestiveMemberNumber.length) < 0 || parseInt(suggestiveMemberNumber.length) > 50) {
            result = false;
            $('#suggestive-member-number-error').removeClass('d-none');
        } else
            $('#suggestive-member-number-error').addClass('d-none');

        //Validation Road Name
        if (permissiveMemberNumber == '' || parseInt(permissiveMemberNumber.length) < 0 || parseInt(permissiveMemberNumber.length) > 50) {
            result = false;
            $('#permissive-member-number-error').removeClass('d-none');
        } else
            $('#permissive-member-number-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideAgendaDataTableColumns() {
        agendaDataTable.column(1).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Board Of Director- DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-board-of-director-dt').click(function () {
        debugger;
        event.preventDefault();

        SetModalTitle('board-of-director', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-board-of-director-dt').click(function () {

        SetModalTitle('board-of-director', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-board-of-director-dt').data('rowindex');
            id = $('#board-of-director-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#board-of-director-id', myModal).val(columnValues[1]);
            $('#notice-reference-number-board-of-director', myModal).val(columnValues[3]);
            $('#notice-status-board-of-director', myModal).val(columnValues[4]);
            $('#attendance-status-board-of-director', myModal).val(columnValues[5]);
            $('#note-board-of-director', myModal).val(columnValues[6]);

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-board-of-director-dt').addClass('read-only');
            $('#board-of-director-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-board-of-director-modal').click(function (event) {
        if (IsValidBoardOfDirectorDataTableModal()) {
            row = boardOfDirectorDataTable.row.add([
                tag,
                boardOfDirectorId,
                boardOfDirectorText,
                noticeReferenceNumber,
                noticeStatus,
                attendanceStatus,
                note,
            ]).draw();
            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideBoardOfDirectorDataTableColumns()

            boardOfDirectorDataTable.columns.adjust().draw();

            $('#board-of-director-data-table-error').addClass('d-none');

            EnableNewOperation('board-of-director');

            $('#board-of-director-modal').modal('hide');
        }
    });

    // Modal Update Button Event
    $('#btn-update-board-of-director-modal').click(function (event) {
        $('#select-all-board-of-director').prop('checked', false);
        if (IsValidBoardOfDirectorDataTableModal()) {
            boardOfDirectorDataTable.row(selectedRowIndex).data([
                tag,
                boardOfDirectorId,
                boardOfDirectorText,
                noticeReferenceNumber,
                noticeStatus,
                attendanceStatus,
                note,
            ]).draw();

            HideBoardOfDirectorDataTableColumns()

            boardOfDirectorDataTable.columns.adjust().draw();

            EnableNewOperation('board-of-director');

            $('#board-of-director-modal').modal('hide');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-board-of-director-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-board-of-director tbody input[type="checkbox"]:checked').each(function () {
                    boardOfDirectorDataTable.row($('#tbl-board-of-director tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    // Display Required Error Message, If Table Has Not Any Record
                    if (!boardOfDirectorDataTable.data().any())
                        $('#board-of-director-data-table-error').removeClass('d-none');

                    EnableNewOperation('board-of-director');

                    $('#select-all-board-of-director').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Board Of Director Datatable
    $('#select-all-board-of-director').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-board-of-director tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = boardOfDirectorDataTable.row(row).index();
                rowData = (boardOfDirectorDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-board-of-director-dt').data('rowindex', arr);

                EnableDeleteOperation('board-of-director');
            });
        }
        else {
            EnableNewOperation('board-of-director');

            $('#tbl-board-of-director tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-board-of-director tbody').click('input[type=checkbox]', function () {
        $('#tbl-board-of-director input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = boardOfDirectorDataTable.row(row).index();
                rowData = (boardOfDirectorDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('board-of-director');

                $('#btn-update-board-of-director-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-board-of-director-dt').data('rowindex', rowData);
                $('#btn-delete-board-of-director-dt').data('rowindex', arr);
                $('#select-all-board-of-director').data('rowindex', arr);
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        isCheckedAll = $('#tbl-board-of-director tbody input[type="checkbox"]');
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('board-of-director');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('board-of-director');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('board-of-director');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-board-of-director').prop('checked', true);
        else
            $('#select-all-board-of-director').prop('checked', false);
    });

    // Validate Board Of Director Module
    function IsValidBoardOfDirectorDataTableModal() {
        result = true;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        boardOfDirectorId = $('#board-of-director-id option:selected').val();
        boardOfDirectorText = $('#board-of-director-id option:selected').text();
        noticeReferenceNumber = $('#notice-reference-number-board-of-director').val();
        noticeStatus = $('#notice-status-board-of-director').val();
        attendanceStatus = $('#attendance-status-board-of-director').val();
        note = $('#note-board-of-director').val();
        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        // Validate General Ledger
        if (boardOfDirectorId == '') {
            result = false;
            $('#board-of-director-id-error').removeClass('d-none');
        }
        else
            $('#board-of-director-id-error').addClass('d-none');

        //Validation Notice Reference Number
        if (noticeReferenceNumber == '' || parseInt(noticeReferenceNumber.length) < 0 || parseInt(noticeReferenceNumber.length) > 100) {
            result = false;
            $('#notice-reference-number-board-of-director-error').removeClass('d-none');
        } else
            $('#notice-reference-number-board-of-director-error').addClass('d-none');

        //Validation Notice Status
        if (noticeStatus == '' || parseInt(noticeStatus.length) < 0 || parseInt(noticeStatus.length) > 3) {
            result = false;
            $('#notice-status-board-of-director-error').removeClass('d-none');
        } else
            $('#notice-status-board-of-director-error').addClass('d-none');

        //Validation Road Attendance Status
        if (attendanceStatus == '' || parseInt(attendanceStatus.length) < 0 || parseInt(attendanceStatus.length) > 3) {
            result = false;
            $('#attendance-status-board-of-director-error').removeClass('d-none');
        } else
            $('#attendance-status-board-of-director-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideBoardOfDirectorDataTableColumns() {
        boardOfDirectorDataTable.column(1).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Invitee Member - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-invitee-member-dt').click(function () {
        event.preventDefault();
        SetModalTitle('invitee-member', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-invitee-member-dt').click(function () {

        SetModalTitle('invitee-member', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-invitee-member-dt').data('rowindex');
            id = $('#invitee-member-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#customer-shares-capital-account-id', myModal).val(columnValues[1]);
            $('#notice-reference-number-invitee-member', myModal).val(columnValues[3]);
            $('#notice-status-invitee-member', myModal).val(columnValues[4]);
            $('#attendance-status-invitee-member', myModal).val(columnValues[5]);
            $('#note-invitee-member', myModal).val(columnValues[6]);

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-invitee-member-dt').addClass('read-only');
            $('#invitee-member-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-invitee-member-modal').click(function (event) {
        if (IsValidInviteeMemberDataTableModal()) {
            row = inviteeMemberDataTable.row.add([
                tag,
                customerSharesCapitalAccountId,
                customerSharesCapitalAccountText,
                noticeReferenceNumber,
                noticeStatus,
                attendanceStatus,
                note,
            ]).draw();
            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideInviteeMemberDataTableColumns()

            inviteeMemberDataTable.columns.adjust().draw();

            $('#invitee-member-data-table-error').addClass('d-none');

            EnableNewOperation('invitee-member');

            $('#invitee-member-modal').modal('hide');
        }
    });

    // Modal Update Button Event
    $('#btn-update-invitee-member-modal').click(function (event) {
        $('#select-all-invitee-member').prop('checked', false);
        if (IsValidInviteeMemberDataTableModal()) {
            inviteeMemberDataTable.row(selectedRowIndex).data([
                tag,
                customerSharesCapitalAccountId,
                customerSharesCapitalAccountText,
                noticeReferenceNumber,
                noticeStatus,
                attendanceStatus,
                note,
            ]).draw();

            HideInviteeMemberDataTableColumns()

            inviteeMemberDataTable.columns.adjust().draw();

            EnableNewOperation('invitee-member');

            $('#invitee-member-modal').modal('hide');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-invitee-member-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-invitee-member tbody input[type="checkbox"]:checked').each(function () {
                    inviteeMemberDataTable.row($('#tbl-invitee-member tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    // Display Required Error Message, If Table Has Not Any Record
                    if (!inviteeMemberDataTable.data().any())
                        $('#invitee-member-data-table-error').removeClass('d-none');

                    EnableNewOperation('invitee-member');

                    $('#select-all-invitee-member').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Invitee Member Datatable
    $('#select-all-invitee-member').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-invitee-member tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = inviteeMemberDataTable.row(row).index();
                rowData = (inviteeMemberDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-invitee-member-dt').data('rowindex', arr);

                EnableDeleteOperation('invitee-member');
            });
        }
        else {
            EnableNewOperation('invitee-member');

            $('#tbl-invitee-member tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-invitee-member tbody').click('input[type=checkbox]', function () {
        $('#tbl-invitee-member input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = inviteeMemberDataTable.row(row).index();
                rowData = (inviteeMemberDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('invitee-member');

                $('#btn-update-invitee-member-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-invitee-member-dt').data('rowindex', rowData);
                $('#btn-delete-invitee-member-dt').data('rowindex', arr);
                $('#select-all-invitee-member').data('rowindex', arr);
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        isCheckedAll = $('#tbl-invitee-member tbody input[type="checkbox"]');
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('invitee-member');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('invitee-member');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('invitee-member');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-invitee-member').prop('checked', true);
        else
            $('#select-all-invitee-member').prop('checked', false);
    });

    // Validate Invitee Member Module
    function IsValidInviteeMemberDataTableModal() {
        result = true;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        customerSharesCapitalAccountId = $('#customer-shares-capital-account-id option:selected').val();
        customerSharesCapitalAccountText = $('#customer-shares-capital-account-id option:selected').text();
        noticeReferenceNumber = $('#notice-reference-number-invitee-member').val();
        noticeStatus = $('#notice-status-invitee-member').val();
        attendanceStatus = $('#attendance-status-invitee-member').val();
        note = $('#note-invitee-member').val();
        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        // Validate General Ledger
        if (customerSharesCapitalAccountId == '') {
            result = false;
            $('#customer-shares-capital-account-id-error').removeClass('d-none');
        }
        else
            $('#customer-shares-capital-account-id-error').addClass('d-none');

        //Validation Notice Reference Number
        if (noticeReferenceNumber == '' || parseInt(noticeReferenceNumber.length) < 0 || parseInt(noticeReferenceNumber.length) > 100) {
            result = false;
            $('#notice-reference-number-invitee-member-error').removeClass('d-none');
        } else
            $('#notice-reference-number-invitee-member-error').addClass('d-none');

        //Validation Notice Status
        if (noticeStatus == '' || parseInt(noticeStatus.length) < 0 || parseInt(noticeStatus.length) > 3) {
            result = false;
            $('#notice-status-invitee-member-error').removeClass('d-none');
        } else
            $('#notice-status-invitee-member-error').addClass('d-none');

        //Validation Road Attendance Status
        if (attendanceStatus == '' || parseInt(attendanceStatus.length) < 0 || parseInt(attendanceStatus.length) > 3) {
            result = false;
            $('#attendance-status-invitee-member-error').removeClass('d-none');
        } else
            $('#attendance-status-invitee-member-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideInviteeMemberDataTableColumns() {
        inviteeMemberDataTable.column(1).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Notice - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-notice-dt').click(function () {
        event.preventDefault();
        SetModalTitle('notice', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-notice-dt').click(function () {

        SetModalTitle('notice', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked) {
            columnValues = $('#btn-edit-notice-dt').data('rowindex');
            id = $('#notice-modal').attr('id');
            myModal = $('#' + id).modal();

            // Display Value In Modal Inputs
            $('#notice-media-id', myModal).val(columnValues[1]);
            $('#schedule-id', myModal).val(columnValues[3]);
            $('#notice-format-id', myModal).val(columnValues[5]);
            $('#note-meeting-notice', myModal).val(columnValues[7]);

            // Show Modal
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-notice-dt').addClass('read-only');
            $('#notice-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-notice-modal').click(function (event) {
        if (IsValidNoticeDataTableModal()) {
            row = noticeDataTable.row.add([
                tag,
                noticeMediaId,
                noticeMediaIdText,
                scheduleId,
                scheduleIdText,
                noticeFormatId,
                noticeFormatIdText,
                note,
            ]).draw();
            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideNoticeDataTableColumns()

            noticeDataTable.columns.adjust().draw();

            $('#notice-data-table-error').addClass('d-none');

            EnableNewOperation('notice');

            $('#notice-modal').modal('hide');
        }
    });

    // Modal Update Button Event
    $('#btn-update-notice-modal').click(function (event) {
        $('#select-all-notice').prop('checked', false);
        if (IsValidNoticeDataTableModal()) {
            noticeDataTable.row(selectedRowIndex).data([
                tag,
                noticeMediaId,
                noticeMediaIdText,
                scheduleId,
                scheduleIdText,
                noticeFormatId,
                noticeFormatIdText,
                note,
            ]).draw();

            HideNoticeDataTableColumns()

            noticeDataTable.columns.adjust().draw();

            EnableNewOperation('notice');

            $('#notice-modal').modal('hide');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-notice-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-notice tbody input[type="checkbox"]:checked').each(function () {
                    noticeDataTable.row($('#tbl-notice tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    // Display Required Error Message, If Table Has Not Any Record
                    if (!noticeDataTable.data().any())
                        $('#notice-data-table-error').removeClass('d-none');

                    EnableNewOperation('notice');

                    $('#select-all-notice').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Notice Datatable
    $('#select-all-notice').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-notice tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = noticeDataTable.row(row).index();
                rowData = (noticeDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-notice-dt').data('rowindex', arr);

                EnableDeleteOperation('notice');
            });
        }
        else {
            EnableNewOperation('notice');

            $('#tbl-notice tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-notice tbody').click('input[type=checkbox]', function () {
        $('#tbl-notice input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = noticeDataTable.row(row).index();
                rowData = (noticeDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('notice');

                $('#btn-update-notice-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-notice-dt').data('rowindex', rowData);
                $('#btn-delete-notice-dt').data('rowindex', arr);
                $('#select-all-notice').data('rowindex', arr);
            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        isCheckedAll = $('#tbl-notice tbody input[type="checkbox"]');
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('notice');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('notice');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('notice');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-notice').prop('checked', true);
        else
            $('#select-all-notice').prop('checked', false);
    });

    // Validate Notice Module
    function IsValidNoticeDataTableModal() {
        result = true;

        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        noticeMediaId = $('#notice-media-id option:selected').val();
        noticeMediaIdText = $('#notice-media-id option:selected').text();
        scheduleId = $('#schedule-id option:selected').val();
        scheduleIdText = $('#schedule-id option:selected').text();
        noticeFormatId = $('#notice-format-id option:selected').val();
        noticeFormatIdText = $('#notice-format-id option:selected').text();
        note = $('#note-meeting-notice').val();
        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        // Validate General Ledger
        if (noticeMediaId == '') {
            result = false;
            $('#notice-media-id-error').removeClass('d-none');
        }
        else
            $('#notice-media-id-error').addClass('d-none');

        // Validate General Ledger
        if (scheduleId == '') {
            result = false;
            $('#schedule-id-error').removeClass('d-none');
        }
        else
            $('#schedule-id-error').addClass('d-none');

        // Validate General Ledger
        if (noticeFormatId == '') {
            result = false;
            $('#notice-format-id-error').removeClass('d-none');
        }
        else
            $('#notice-format-id-error').addClass('d-none');

        return result;
    }

    // Hide Unnecessary Columns
    function HideNoticeDataTableColumns() {
        noticeDataTable.column(1).visible(false);
        noticeDataTable.column(3).visible(false);
        noticeDataTable.column(5).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger;
        let isValidAllInputs = true;

        if ($('form').valid()) {

            // not add event.preventDefault
            $(".lastrow").remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let agendaArray = new Array();
            let boardOfDirectorArray = new Array();
            let inviteeMemberArray = new Array();
            let noticeArray = new Array();

            //CreateDataTable
            agendaDataTable.page.len(-1).draw();
            boardOfDirectorDataTable.page.len(-1).draw();
            inviteeMemberDataTable.page.len(-1).draw();
            noticeDataTable.page.len(-1).draw();

            // Create Array For Agenda Data Table To Pass Data
            if (!$('#heading-meeting-agenda').hasClass('d-none')) {
                if (agendaDataTable.data().any()) {

                    $('#agenda-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        // Get Data Table Values In Business Office Array
                        $('#tbl-agenda > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (agendaDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                agendaArray.push(
                                    {
                                        'AgendaId': columnValues[1],
                                        'SequenceNumber': columnValues[3],
                                        'SuggestiveMemberNumber': columnValues[4],
                                        'PermissiveMemberNumber': columnValues[5],
                                        'Note': columnValues[6],
                                    });
                            } else {
                                return false;
                            }
                        });
                        // isValidAllInputs = true;
                    }
                } else {
                    $('#agenda-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For Board Of Director Data Table To Pass Data
            if (!$('#heading-board-of-director').hasClass('d-none')) {
                if (boardOfDirectorDataTable.data().any()) {

                    $('#board-of-director-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        // Get Data Table Values In Business Office Array
                        $('#tbl-board-of-director > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (boardOfDirectorDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                boardOfDirectorArray.push({
                                    'BoardOfDirectorId': columnValues[1],
                                    'NoticeReferenceNumber': columnValues[3],
                                    'NoticeStatus': columnValues[4],
                                    'AttendanceStatus': columnValues[5],
                                    'Note': columnValues[6],
                                });
                            } else {
                                return false;
                            }
                        });
                     }
                } else {
                    $('#board-of-director-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }
         
            // Create Array For InviteeMember Data Table To Pass Data
            if (!$('#heading-invitee-member').hasClass('d-none')) {
                if (inviteeMemberDataTable.data().any()) {

                    $('#invitee-member-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        // Get Data Table Values In Business Office Array
                        $('#tbl-invitee-member > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (inviteeMemberDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                
                                inviteeMemberArray.push({
                                    'SharesCapitalCustomerAccountId': columnValues[1],
                                    'NoticeReferenceNumber': columnValues[3],
                                    'NoticeStatus': columnValues[4],
                                    'AttendanceStatus': columnValues[5],
                                    'Note': columnValues[6],
                                });
                            } else {
                                return false;
                            }
                        });
                        // isValidAllInputs = true;
                    }
                } else {
                    $('#invitee-member-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Create Array For Notice Data Table To Pass Data
            if (!$('#heading-meeting-notice').hasClass('d-none')) {
                if (noticeDataTable.data().any()) {

                    $('#notice-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        // Get Data Table Values In Business Office Array
                        $('#tbl-notice > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (noticeDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                noticeArray.push({
                                    'NoticeMediaId': columnValues[1],
                                    'ScheduleId': columnValues[3],
                                    'MenuId': columnValues[5],
                                    'Note': columnValues[7],
                                     
                                });
                            } else {
                                return false;
                            }
                        });
                     }
                } else {
                    $('#notice-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

       
            // Call Cantroller Save Data Table Method
            if (isValidAllInputs) {
                $.ajax({
                    url: saveDataTableUrl,
                    type: 'POST',
                    data: { '_meetingAgenda': agendaArray, '_meetingInviteeBoardOfDirector': boardOfDirectorArray, '_meetingInviteeMember': inviteeMemberArray, '_meetingNotice': noticeArray },
                    ContentType: "application/json; charset=utf-8",
                    dataType: "JSON",

                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert("An Error Has Occured While Save Data In Contact Details DataTable!!! Error Message - " + error.toString());
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
});
