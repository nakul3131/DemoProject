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

    const BOARD_OF_DROPDOWN_LIST = $('#board-of-director-id').html();

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
    let note = '';
    let reasonForModification = '';
    let relation;
    let relationText = '';

    //Board Of Director Relation
    let boardofdirector = '';
    let boardofdirectorText = '';
    let editedBoardOfDirectorId = '';

    // Create DataTables
    let boardOfDirectorDataTable = CreateDataTable('relation');

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Board of Director Relation - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@
    
    // DataTable Add Button 
    $('#btn-add-relation-dt').click(function () {
        debugger;
        event.preventDefault();
        editedBoardOfDirectorId = '';
        SetBoardOfDirectorUniqueDropdownList();

        let count = $('#board-of-director-id option').length;
        if (count === 1) {
            alert('Oops! It looks Like There Are No Records Available For Entry At This Time');
        }
        else {
            SetModalTitle('relation', 'Add');
        }
        
    });

    // DataTable Edit Button 
    $('#btn-edit-relation-dt').click(function () {
        SetModalTitle('relation', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#relation-modal').modal();

            columnValues = $('#btn-edit-relation-dt').data('rowindex');
            editedBoardOfDirectorId = columnValues[1];

            SetBoardOfDirectorUniqueDropdownList();

            $('#board-of-director-id', myModal).val(columnValues[1]);
            $('#relation-id', myModal).val(columnValues[3]);
            $('#note-board-of-director', myModal).val(columnValues[5]);
            $('#reason-for-modification-board-of-director', myModal).val(columnValues[6]);

            // Show Modals
            $('#relation-modal').modal('show');
        }
        else {
            $('#btn-edit-relation-edit-dt').addClass('read-only');
            $('#relation-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-relation-modal').click(function (event) {
        if (IsValidRelationModal()) {
            row = boardOfDirectorDataTable.row.add([
                        tag,
                        boardofdirector,
                        boardofdirectorText,
                        relation,
                        relationText,
                        note,
                        reasonForModification
            ]).draw();
            $('#relation-accordion-error').addClass('d-none');

            HideColumnsRelationDataTable();

            boardOfDirectorDataTable.columns.adjust().draw();

            $('#relation-modal').modal('hide');

            EnableNewOperation('relation');
        }
    });

    // Modal update Button Event
    $('#btn-update-relation-modal').click(function (event) {
        $('#select-all-relation').prop('checked', false);
        if (IsValidRelationModal()) {
            boardOfDirectorDataTable.row(selectedRowIndex).data([
                        tag,
                        boardofdirector,
                        boardofdirectorText,
                        relation,
                        relationText,
                        note,
                        reasonForModification
            ]).draw();

            HideColumnsRelationDataTable();

            boardOfDirectorDataTable.columns.adjust().draw();

            $('#relation-modal').modal('hide');

            EnableNewOperation('relation');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-relation-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-relation tbody input[type="checkbox"]:checked').each(function () {
                 boardOfDirectorDataTable.row($('#tbl-relation tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-relation-dt').data('rowindex');
                  EnableNewOperation('relation');
                    SetBoardOfDirectorUniqueDropdownList();

                  $('#select-all-relation').prop('checked', false);
                    // Add Required Error Message, If Table Has Not Any Record

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-relation').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-relation tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = boardOfDirectorDataTable.row(row).index();

                rowData = (boardOfDirectorDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-relation-dt').data('rowindex', arr);
                EnableDeleteOperation('relation');
            });
        }
        else {
            EnableNewOperation('relation');

            $('#tbl-relation tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-relation tbody').click("input[type=checkbox]", function () {
        $('#tbl-relation input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = boardOfDirectorDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (boardOfDirectorDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('relation');

                    $('#btn-update-relation-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-relation-dt').data('rowindex', rowData);
                    $('#btn-delete-relation-dt').data('rowindex', arr);
                    $('#select-all-relation').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-relation tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('relation');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('relation');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('relation');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-relation').prop('checked', true);
        else
            $('#select-all-relation').prop('checked', false);
    });

    // Validate relation Module
    function IsValidRelationModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        boardofdirector = $('#board-of-director-id option:selected').val();
        boardofdirectorText = $('#board-of-director-id option:selected').text();
        relation = $('#relation-id option:selected').val();
        relationText = $('#relation-id option:selected').text();
        note = $('#note-board-of-director').val();
        reasonForModification = $('#reason-for-modification-board-of-director').val();
        
        //Set Default Value if Empty
        if (note === '') {
            $('#note-board-of-director').val('None');
            note = 'None';
        }
            
        if (reasonForModification === '') {
            $('#reason-for-modification-board-of-director').val('None');
            reasonForModification = 'None';
        }
       
        if (($('#board-of-director-id').prop('selectedIndex')) < 1) {
            result = false;
            $('#board-of-director-id-error').removeClass('d-none');
        }

        if (($('#relation-id').prop('selectedIndex')) < 1) {
            result = false;
            $('#relation-id-error').removeClass('d-none');
        }
        
        return result;
    }

    function HideColumnsRelationDataTable() {
        boardOfDirectorDataTable.column(1).visible(false);
        boardOfDirectorDataTable.column(3).visible(false);
    }

    function SetBoardOfDirectorUniqueDropdownList() {
        // Show All List Items
        $('#board-of-director-id').html('');
        $('#board-of-director-id').append(BOARD_OF_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-relation > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (boardOfDirectorDataTable.row(currentRow).data());

            if (typeof myColumnValues !== 'undefined' && myColumnValues !== null) {
                if (myColumnValues[1] !== editedBoardOfDirectorId)
                    $('#board-of-director-id').find('option[value="' + myColumnValues[1] + '"]').remove();
            }
        });
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {

        let isValidAllInputs = true;

        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.

            let personBoardOfDirectorRelationArray = new Array();

            // Create Array For person board of director relation Table To Pass Data
            if (boardOfDirectorDataTable.data().any()) {

                if (isValidAllInputs) {

                    $('#tbl-relation > tbody > tr').each(function () {
                        currentRow = $(this).closest('tr');

                        columnValues = (boardOfDirectorDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues !== 'undefined' && columnValues !== null) {
                            personBoardOfDirectorRelationArray.push(
                            {
                                'BoardOfDirectorId': columnValues[1],
                                'RelationId': columnValues[3],
                                'Note': columnValues[5],
                                'ReasonForModification': columnValues[6]
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
                    data: {

                        '_boardOfDirectorRelation': personBoardOfDirectorRelationArray,

                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person Board Of Director Relation DataTable!!! Error Message - ' + error.toString());
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