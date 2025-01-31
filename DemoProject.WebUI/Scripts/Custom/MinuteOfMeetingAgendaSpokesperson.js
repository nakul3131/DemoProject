// Document Ready Function
'use strict';
$(document).ready(function () {
    debugger;
    // @@@@@@@@@@ Data Table Related letible Declaration
    debugger;
    let tag = '';
    let id = '';
    let myModal;
    let rowNum = 0;
    let selectedRowIndex;
    let row;
    let today;
    let age;
    let rowData;
    let checked;
    let columnValues;
    let maritalStatusId;
    let occupationId;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let meridian;
    let hours;
    let minutes;
    let minimum;
    let maximum;
    let arr = new Array();
    let result = true;

    //Spokes person
    let boardOfDirectorId;
    let boardOfDirectorIdText;
    let speakingStartTime = [];
    let speakingEndTime = [];
    let endTime;
    let startTime;
    let speech;
    let transspeech;
    let note;
    let transnote;

    // CreateDataTable
    let spokesPersonDataTable = CreateDataTable('spokes-person');

    /// @@@@@@@@@@@@@@@@@@@@@@   Scheme Spokes Person Alert  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('spokes-person');

    // DataTable Add Button 
    $('#btn-add-spokes-person-dt').click(function () {

        event.preventDefault();
        $('.field_wrapper1').html('');
        SetModalTitle('spokes-person', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-spokes-person-dt').click(function () {
        debugger;
        SetModalTitle('spokes-person', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-spokes-person-dt').data('rowindex');
            id = $('#spokes-person-modal').attr('id');
            myModal = $('#' + id).modal();
            // Display Value In Modal Inputs
            $('#board-of-director-id', myModal).val(columnValues[1]);
            $('#speaking-start-time', myModal).val(columnValues[3]);
            $('#speaking-end-time', myModal).val(columnValues[4]);
            $('#speech', myModal).val(columnValues[5]);
            $('#trans-speech', myModal).val(columnValues[6]);
            $('#note', myModal).val(columnValues[7]);
            $('#trans-note', myModal).val(columnValues[8]);
            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-spokes-person-edit-dt').addClass('read-only');
            $('#spokes-person-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-spokes-person-modal').click(function (event) {
        if (IsValidSpokesPersonModal()) {
            debugger
            row = spokesPersonDataTable.row.add([
                tag,
                boardOfDirectorId,
                boardOfDirectorIdText,
                speakingStartTime,
                speakingEndTime,
                speech,
                transspeech,
                note,
                transnote
            ]).draw();

            // Error Message In Span
            $('#spokes-person-data-table-error').addClass('d-none');

            HideColumnsSpokesPersonDataTable();

            spokesPersonDataTable.columns.adjust().draw();

            ClearModal('spokes-person');

            $('#spokes-person-modal').modal('hide');

            EnableNewOperation('spokes-person');
        }
    });

    // Modal update Button Event
    $('#btn-update-spokes-person-modal').click(function (event) {
        debugger
        $('#select-all-spokes-person').prop('checked', false);
        if (IsValidSpokesPersonModal()) {
            spokesPersonDataTable.row(selectedRowIndex).data([
                tag,
                boardOfDirectorId,
                boardOfDirectorIdText,
                speakingStartTime,
                speakingEndTime,
                speech,
                transspeech,
                note,
                transnote
            ]).draw();

            HideColumnsSpokesPersonDataTable();

            spokesPersonDataTable.columns.adjust().draw();

            $('#spokes-person-modal').modal('hide');

            EnableNewOperation('spokes-person');
        }

    });

    // Modal Delete Button Event
    $('#btn-delete-spokes-person-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-spokes-person tbody input[type="checkbox"]:checked').each(function () {
                    spokesPersonDataTable.row($('#tbl-spokes-person tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-spokes-person-dt').data('rowindex');
                    EnableNewOperation('spokes-person');

                    $('#select-all-spokes-person').prop('checked', false);
                    if (!spokesPersonDataTable.data().any())
                    $('#spokes-person-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-spokes-person').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-spokes-person tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = spokesPersonDataTable.row(row).index();

                rowData = (spokesPersonDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-spokes-person-dt').data('rowindex', arr);
                EnableDeleteOperation('spokes-person');
            });
        }
        else {
            EnableNewOperation('spokes-person');

            $('#tbl-spokes-person tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-spokes-person tbody').click('input[type=checkbox]', function () {
        $('#tbl-spokes-person input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = spokesPersonDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (spokesPersonDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('spokes-person');

                    $('#btn-update-spokes-person-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-spokes-person-dt').data('rowindex', rowData);
                    $('#btn-delete-spokes-person-dt').data('rowindex', arr);
                    $('#select-all-spokes-person').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-spokes-person tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('spokes-person');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('spokes-person');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('spokes-person');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-spokes-person').prop('checked', true);
        else
            $('#select-all-spokes-person').prop('checked', false);
    });

    // Validate Spokes Person Module
    function IsValidSpokesPersonModal() {
        debugger;
        result = true;
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        boardOfDirectorId = $('#board-of-director-id option:selected').val();
        boardOfDirectorIdText = $('#board-of-director-id option:selected').text();
        speakingStartTime = $('#speaking-start-time').val();
        speakingEndTime = $('#speaking-end-time').val();
        speech = $('#speech').val();
        transspeech = $('#trans-speech').val();
        note = $('#note').val();
        transnote = $('#trans-note').val();

        if (note == '')
            note = 'None';


        // Validate Limit
        if (boardOfDirectorId == '') {
            result = false;
            $('#board-of-director-id-error').removeClass('d-none');
        }
        else {
            $('#board-of-director-id-error').addClass('d-none');
        }


        //Validation Notice Status
        if (speech == '' || parseInt(speech.length) < 0 || parseInt(speech.length) > 3500) {
            result = false;
            $('#speech-error').removeClass('d-none');
        } else
            $('#speech-error').addClass('d-none');

        //Validation Notice Status
        if (transspeech == '' || parseInt(transspeech.length) < 0 || parseInt(transspeech.length) > 3500) {
            result = false;
            $('#trans-speech-error').removeClass('d-none');
        } else
            $('#trans-speech-error').addClass('d-none');

        if (speakingStartTime == '') {
            result = false;

            $('#speaking-start-time-error').removeClass('d-none');
        }
        else
            $('#speaking-start-time-error').addClass('d-none');

        if (speakingEndTime == '') {
            result = false;

            $('#speaking-end-time-error').removeClass('d-none');
        }
        else
            $('#speaking-end-time-error').addClass('d-none');


        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsSpokesPersonDataTable() {
        spokesPersonDataTable.column(1).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Handling Save/Submit Click Event
    $('#btnsave').click(function () {
        debugger;

        let isValidAllInputs = true;

        if ($('form').valid()) {

            // not add event.preventDefault
            $('.lastrow').remove();
            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let spokesPersonArray = new Array();

            //CreateDataTable
            spokesPersonDataTable.page.len(-1).draw();

            // Create Array For spokesPerson Data Table To Pass Data
            if (!$('#heading-spokes-person').hasClass('d-none')) {
                if (spokesPersonDataTable.data().any()) {
                    $('#spokes-person-data-table-error').addClass('d-none');
                    if (isValidAllInputs) {
                        // Get Data Table Values In Notice Schedule Array
                        $('#tbl-spokes-person > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (spokesPersonDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                debugger;
                                spokesPersonArray.push({
                                    'BoardOfDirectorId': columnValues[1],
                                    'SpeakingStartTime': columnValues[3],
                                    'SpeakingEndTime': columnValues[4],
                                    'Speech': columnValues[5],
                                    'TransSpeech': columnValues[6],
                                    'Note': columnValues[7],
                                    'TransNote': columnValues[8]
                                });
                            } else {
                                return false;
                            }
                        });
                    }
                } else {
                    $('#spokes-person-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            // Call Cantroller Save Data Table Method
            if (isValidAllInputs) {
                $.ajax({
                    url: saveDataTableUrl,
                    type: 'POST',
                    data: { '_minuteOfMeetingAgendaSpokesperson': spokesPersonArray },
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