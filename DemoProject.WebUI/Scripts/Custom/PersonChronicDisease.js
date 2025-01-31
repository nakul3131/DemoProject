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
$(document).ready(function ()
{
    // @@@@@@@@@@ Data Table Related letible Declaration

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
    let minimumLength = 0;
    let maximumLength = 0;
    let arr = new Array();
    let result = true;
    let note = '';
    let reasonForModification = '';
    let lastSelectedValue = '';
    //Chronic Disease
    let disease = '';
    let diseaseText = '';
    let otherDetails = '';

    // Create DataTables
    let chronicDataTable = CreateDataTable('chronic-disease');

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    //Chronic disease Dropdown focusout values get clear
    $('#disease-id').focusout(function ()
    {
        debugger;
        let currentValue = $(this).val();

        // If the value has changed from the initial value, clear the related fields
        if (currentValue !== lastSelectedValue)
        {
            $('#other-detail').val('');
            $('#note-chronic-disease').val('');
        }

        lastSelectedValue = currentValue;
    });

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@  Person Chronic Disease - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-chronic-disease-dt').click(function () {
        event.preventDefault();
        lastSelectedValue = '';
        SetModalTitle('chronic-disease', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-chronic-disease-dt').click(function ()
    {
        SetModalTitle('chronic-disease', 'Edit');

        isChecked = $('.checks').is(':checked');

        if (isChecked)
        {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#chronic-disease-modal').modal();

            columnValues = $('#btn-edit-chronic-disease-dt').data('rowindex');

            lastSelectedValue = columnValues[1];

            $('#disease-id', myModal).val(columnValues[1]);
            $('#other-detail', myModal).val(columnValues[3]);
            $('#note-chronic-disease', myModal).val(columnValues[4]);
            $('#reason-for-modification-chronic-disease', myModal).val(columnValues[5]);

            // Show Modals
            $('#chronic-disease-modal').modal('show');
        }
        else {
            $('#btn-edit-chronic-disease-edit-dt').addClass('read-only');
            $('#chronic-disease-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-chronic-disease-modal').click(function (event) {
        debugger;
        if (IsValidChronicDiseaseModal()) {
            row = chronicDataTable.row.add([
                        tag,
                        disease,
                        diseaseText,
                        otherDetails,
                        note,
                        reasonForModification
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            $('#chronic-disease-data-table-error').addClass('d-none');

            HideColumnsChronicDiseaseDataTable();

            chronicDataTable.columns.adjust().draw();

            $('#chronic-disease-modal').modal('hide');

            EnableNewOperation('chronic-disease');
        }
    });

    // Modal update Button Event
    $('#btn-update-chronic-disease-modal').click(function (event) {
        let b = $('#btn-edit-chronic-disease-dt').attr('rowindex');
        $('#select-all-chronic-disease').prop('checked', false);
        if (IsValidChronicDiseaseModal()) {
            chronicDataTable.row(selectedRowIndex).data([
                        tag,
                        disease,
                        diseaseText,
                        otherDetails,
                        note,
                        reasonForModification
            ]).draw();

            HideColumnsChronicDiseaseDataTable();

            chronicDataTable.columns.adjust().draw();

            $('#chronic-disease-modal').modal('hide');

            EnableNewOperation('chronic-disease');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-chronic-disease-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-chronic-disease tbody input[type="checkbox"]:checked').each(function () {
                 chronicDataTable.row($('#tbl-chronic-disease tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-chronic-disease-dt').data('rowindex');
                  EnableNewOperation('chronic-disease');

                  $('#select-all-chronic-disease').prop('checked', false);

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-chronic-disease').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-chronic-disease tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = chronicDataTable.row(row).index();

                rowData = (chronicDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-chronic-disease-dt').data('rowindex', arr);
                EnableDeleteOperation('chronic-disease');
            });
        }
        else {
            EnableNewOperation('chronic-disease');

            $('#tbl-chronic-disease tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-chronic-disease tbody').click('input[type=checkbox]', function () {
        $('#tbl-chronic-disease input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = chronicDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (chronicDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('chronic-disease');

                    $('#btn-update-chronic-disease-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-chronic-disease-dt').data('rowindex', rowData);
                    $('#btn-delete-chronic-disease-dt').data('rowindex', arr);
                    $('#select-all-chronic-disease').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-chronic-disease tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('chronic-disease');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('chronic-disease');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('chronic-disease');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-chronic-disease').prop('checked', true);
        else
            $('#select-all-chronic-disease').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidChronicDiseaseModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        disease = $('#disease-id option:selected').val();
        diseaseText = $('#disease-id option:selected').text();
        otherDetails = $('#other-detail').val();
        note = $('#note-chronic-disease').val();
        reasonForModification = $('#reason-for-modification-chronic-disease').val();

        //Set Default Value if Empty
        if (note === '') {
            $('#note-chronic-disease').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-chronic-disease').val('None');
            reasonForModification = 'None';
        }

        if (otherDetails === '') {
            $('#other-detail').val('None');
            otherDetails = 'None';
        }

        if ($('#disease-id').prop('selectedIndex') < 1) {
            result = false;
            $('#disease-id-error').removeClass('d-none');
        }

        //other Details
        if (isNaN(otherDetails.length) === false) {
            maximumLength = parseInt($('#other-detail').attr('maxlength'));

            if (parseInt(otherDetails.length) === 0 || parseInt(otherDetails.length) > parseInt(maximumLength)) {
                result = false;
                $('#other-detail-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#other-detail-error').removeClass('d-none');
        }

        return result;
    }

    function HideColumnsChronicDiseaseDataTable() {
        chronicDataTable.column(1).visible(false);
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;
        //let personId = $('#person-id option:selected').val();
        //if ($('form').valid() && isValidPancardNumber && isValidAadharNumber)
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.

            let personChronicDiseaseArray = new Array();

            // Create Array For person chronic disease Table To Pass Data

            if (chronicDataTable.data().any()) {

                if (isValidAllInputs) {

                    $('#tbl-chronic-disease > tbody > tr').each(function () {
                        currentRow = $(this).closest('tr');

                        columnValues = (chronicDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues !== 'undefined' && columnValues !== null) {
                            personChronicDiseaseArray.push(
                            {
                                'DiseaseId': columnValues[1],
                                'OtherDetails': columnValues[3],
                                'Note': columnValues[4],
                                'ReasonForModification': columnValues[5],

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
                        '_chronicDisease': personChronicDiseaseArray,
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {

                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person Chronic Disease DataTable!!! Error Message - ' + error.toString());
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