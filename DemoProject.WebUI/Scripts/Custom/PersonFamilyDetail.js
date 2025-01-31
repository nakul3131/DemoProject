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
    debugger;
    let result = true;

    let tag = '';
    let id = '';
    let myModal;
    let selectedRowIndex = 0;
    let row = 0;
    let rowData = 0;
    let checked;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let note;
    let personDropdownListDataForFamily;
    let minimum = 0;
    let maximum = 0;
    let maximumLength = 0;
    let arr = new Array();
    // Array
    let finalDropdownListArray = [];
    let columnValues = 0;
    // Count
    let dropDownListItemCount = 0;



    //Person Family Detail
    let editedJointAccountPersonId = '';
    let personInformationNumber = 0;
    let personInformationNumberText = '';
    let fullNameOfFamilyMember = '';
    let transFullNameOfFamilyMember = '';
    let relation;
    let relationText = '';
    let birthDate = '';
    let occupation = '';
    let occupationText = '';
    let income = 0;
    let familyDetailsBirthDate = '';
    let transNote = '';
    let reasonForModification = '';
    // Create DataTables
    let personFamilyDataTable = CreateDataTable('family-detail');

    SetPageLoadingDefaultValues();


    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@


    // Joint Account Person Dropdown List FocusOut Event
    $('#person-information-numbers').focusout(function (event) {
        $(this).val($(this).val().trim());
    });

    // While Adding Nominee Hide Selected Customer Name In Nominee Dropdown List.
    $('#person-information-numbers').autocomplete(
    {
        minLength: 0,
        appendTo: '#person-information',
        scroll: true,
        autoFocus: true,
        source: function (request, response) {
            debugger;
            let responseDropdownListArray = [];
            dropDownListItemCount = finalDropdownListArray.length;

            if (parseInt(dropDownListItemCount) > 0) {
                responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                    if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1)
                        return { label: key.Text, valueId: key.Value }
                    else
                        return null;
                });

                // The slice() method returns selected elements in an array, as a new array. 
                // The slice() method selects from a given start, up to a (not inclusive) given end.
                response(responseDropdownListArray.slice(0, 10));
            }
            else
                response([{ label: 'No Records Found', value: -1 }]);
        },
        select: function (event, ui) {
            event.preventDefault();
            $('#person-information-numbers').val(ui.item.label);
            personInformationNumber = ui.item.valueId;
            personInformationNumberText = ui.item.label;
            if (personInformationNumber !== '') {
                $('#family-member-name').addClass('d-none');
                $('#person-information').removeClass('d-none');
            }

        },
    }).focus(function (event, ui) {
        debugger;
        personInformationNumber = '';
        personInformationNumberText = '';
        let dataTablePersonIdArray = [];

        // Assign Array Without Reference  *** Use Slice Method
        finalDropdownListArray = personDropdownListDataForFamily.slice();
         
       dropDownListItemCount = finalDropdownListArray.length;

        // Get Added Person Id For Remove From List
        $('#tbl-family-detail > tbody > tr').each(function () {
            let currentRow = $(this).closest("tr");
            let columnValues = (personFamilyDataTable.row(currentRow).data());

            if (typeof columnValues !== 'undefined' && columnValues != null)
                dataTablePersonIdArray.push({ 'Value': columnValues[1], 'Text': columnValues[2] })
        });

        if (parseInt(dropDownListItemCount) > 0 && parseInt(dataTablePersonIdArray.length) > 0) {
            while (dropDownListItemCount--) {
                // Remove Added Joint Account Person Id From List
                for (let personId of dataTablePersonIdArray)
                {
                    if (finalDropdownListArray[dropDownListItemCount].Value === personId.Value)
                        // splice - remove item from array at a choosen index
                        finalDropdownListArray.splice(dropDownListItemCount, 1);
                }
            }
        }


        $(this).autocomplete('search');
    });

    // Validation Family Member
    $('#full-name-of-family-member').focusout(function () {
        debugger;
        let fullNameOfFamily = $('#full-name-of-family-member').val();

        if ((fullNameOfFamily !== 'None') && (fullNameOfFamily.length > 3)) {
            $('#family-person-information-number').prop('selectedIndex', 0);
            $('#person-information').addClass('d-none');
            $('#person-information').val('None');
        } else {
            $('#person-information').removeClass('d-none');
        }
    });

    $('#person-information-numbers').focusout(function () {
        debugger;
        let familyPersonInformationNumber = $('#person-information-numbers').val();

        // Check if the value is not from the autocomplete list
        let isInAutocomplete = false;
        $('#person-information-numbers').autocomplete("widget").children("li").each(function () {
            if ($(this).text() === familyPersonInformationNumber) {
                isInAutocomplete = true;
                return false;
            }
        });

        if (!isInAutocomplete) {
            // Clear the input if the typed value is not in the autocomplete list
            $('#person-information-numbers').val('');
            $('#family-pin').val('None');
            $('#family-member-name').removeClass('d-none');
        } else if ((familyPersonInformationNumber !== 'None') && (familyPersonInformationNumber.length > 3)) {
            $('#family-member-name').addClass('d-none');
            $('#family-member-name').val('None')
        }
    });

    let familyPersonInfo = $('#family-pin').val();
    if (familyPersonInfo !== '') {
        $('#person-information-numbers').val(familyPersonInfo);
    }


    /// @@@@@@@@@@@@@@@@@@@@@@  Person Family Detail - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-family-detail-dt').click(function () {

        //Modify By --- Sagar Kare 
        $('#person-information').removeClass('d-none');
        $('#family-member-name').removeClass('d-none');
        $('#person-information-numbers-error').addClass('d-none');

        // Display Alert Message Only When Modal Present On Page (i.e. Create And Amend) Hide On Verify
        if ($('#family-detail-modal').length) {
            personInformationNumber = ''
            personInformationNumberText = '';
        }
        event.preventDefault();
        SetModalTitle('family-detail', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-family-detail-dt').click(function () {
        debugger
        SetModalTitle('family-detail', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#family-detail-modal').modal();

            columnValues = $('#btn-edit-family-detail-dt').data('rowindex');

            familyDetailsBirthDate = new Date(columnValues[7]);
            editedJointAccountPersonId = columnValues[1];

            personInformationNumber = columnValues[1];
            personInformationNumberText = columnValues[2];
            $('#person-information-numbers', myModal).val(columnValues[2]);
            $('#full-name-of-family-member', myModal).val(columnValues[3]);
            $('#trans-full-name-of-family-member', myModal).val(columnValues[4]);
            $('#relations-id', myModal).val(columnValues[5]);
            $('#birth-date-family-member', myModal).val(GetInputDateFormat(familyDetailsBirthDate));
            $('#occupation-id', myModal).val(columnValues[8]);
            $('#income', myModal).val(columnValues[10]);
            $('#note-family-detail', myModal).val(columnValues[11]);
            $('#trans-note-family-detail', myModal).val(columnValues[12]);
            $('#reason-for-modification-family-detail', myModal).val(columnValues[13]);
            debugger
            // If Nominee Is Existing Customer i.e. Has Valid Person Information Number
            if ((columnValues[3] == 'None')) {
                $('#family-member-name').addClass('d-none');
                $('#person-information').removeClass('d-none');
            }
            else   // User Enter Manullay Nominee Details 
            {
                $('#family-member-name').removeClass('d-none');
                $('#person-information').addClass('d-none');
            }

            // Show Modals
            $('#family-detail-modal').modal('show');
        }
        else {
            $('#btn-edit-family-detail-edit-dt').addClass('read-only');
            $('#family-detail-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-family-detail-modal').click(function (event) {

        if (IsValidFamilyDetailsModal()) {
            row = personFamilyDataTable.row.add([
                          tag,
                          personInformationNumber,
                          personInformationNumberText,
                          fullNameOfFamilyMember,
                          transFullNameOfFamilyMember,
                          relation,
                          relationText,
                          birthDate,
                          occupation,
                          occupationText,
                          income,
                          note,
                          transNote,
                          reasonForModification
            ]).draw();

            // Error Message In Span
            $('#family-detail-data-table-error').addClass('d-none');

            HideColumnsFamilyDetailsDataTable();

            personFamilyDataTable.columns.adjust().draw();

            $('#family-detail-modal').modal('hide');

            EnableNewOperation('family-detail');
        }
    });

    // Modal update Button Event
    $('#btn-update-family-detail-modal').click(function (event) {
        $('#select-all-family-detail').prop('checked', false);
        if (IsValidFamilyDetailsModal()) {
            personFamilyDataTable.row(selectedRowIndex).data([
                                tag,
                                personInformationNumber,
                                personInformationNumberText,
                                fullNameOfFamilyMember,
                                transFullNameOfFamilyMember,
                                relation,
                                relationText,
                                birthDate,
                                occupation,
                                occupationText,
                                income,
                                note,
                                transNote,
                                reasonForModification
            ]).draw();
            // Error Message In Span
            $('#family-detail-validation span').html('');

            HideColumnsFamilyDetailsDataTable();

            personFamilyDataTable.columns.adjust().draw();

            $('#family-detail-modal').modal('hide');

            EnableNewOperation('family-detail');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-family-detail-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-family-detail tbody input[type="checkbox"]:checked').each(function () {
                 personFamilyDataTable.row($('#tbl-family-detail tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-family-detail-dt').data('rowindex');
                  EnableNewOperation('family-detail');

                  $('#select-all-family-detail').prop('checked', false);
                    // Add Required Error Message, If Table Has Not Any Record
                    //if (!personFamilyDataTable.data().any())
                    //$('#family-detail-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-family-detail').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-family-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = personFamilyDataTable.row(row).index();

                rowData = (personFamilyDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-family-detail-dt').data('rowindex', arr);
                EnableDeleteOperation('family-detail');
            });
        }
        else {
            EnableNewOperation('family-detail');

            $('#tbl-family-detail tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-family-detail tbody').click('input[type="checkbox"]', function () {
        $('#tbl-family-detail input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = personFamilyDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (personFamilyDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('family-detail');

                    $('#btn-update-family-detail-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-family-detail-dt').data('rowindex', rowData);
                    $('#btn-delete-family-detail-dt').data('rowindex', arr);
                    $('#select-all-family-detail').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-family-detail tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('family-detail');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('family-detail');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('family-detail');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-family-detail').prop('checked', true);
        else
            $('#select-all-family-detail').prop('checked', false);
    });

    // Validate Family Detail Module
    function IsValidFamilyDetailsModal() {
        debugger;
        result = true;
        let isSelectedPersonInformationNumber = false;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        fullNameOfFamilyMember = $('#full-name-of-family-member').val();
        transFullNameOfFamilyMember = $('#trans-full-name-of-family-member').val();
        relation = $('#relations-id option:selected').val();
        relationText = $('#relations-id option:selected').text();
        birthDate = $('#birth-date-family-member').val();
        occupation = $('#occupation-id option:selected').val();
        occupationText = $('#occupation-id option:selected').text();
        income = parseFloat($('#income').val());
        note = $('#note-family-detail').val();
        transNote = $('#trans-note-family-detail').val();
        reasonForModification = $('#reason-for-modification-family-detail').val();

        //Set Default Value if Empty
        if (note === '')
            note = 'None';

        if (transNote === '')
            transNote = 'None';


        if (reasonForModification === '')
            reasonForModification = 'None';


        //Modify By --- Sagar Kare 
        //Validatio For  Person Information Number
        if (personInformationNumber === '' || personInformationNumber === 'None') {
            fullNameOfFamilyMember = $('#full-name-of-family-member').val();
            transFullNameOfFamilyMember = $('#trans-full-name-of-family-member').val();
        }
        else {
            fullNameOfFamilyMember = 'None';
            transFullNameOfFamilyMember = 'None';
        }
        if (personInformationNumber === '' || typeof personInformationNumber === 'undefined') {
            personInformationNumber = 'None';
        }

        // Check Whether Person Information Number Selected For Nominee Or Not?
        if (personInformationNumber === 'None' || typeof personInformationNumber === 'undefined') {
            isSelectedPersonInformationNumber = false;
        } else {
            isSelectedPersonInformationNumber = true;
        }

        if ($('#person-information').hasClass('d-none') === false) {
            if (isSelectedPersonInformationNumber === false) {
                result = false;
                $('#person-information-numbers-error').removeClass('d-none');
            } else {
                $('#person-information-numbers-error').addClass('d-none');
            }
        }
        else {
            personInformationNumberText = 'None';
        }

        if (isSelectedPersonInformationNumber === false) {

            //reference Number
            maximumLength = parseInt($('#full-name-of-family-member').attr('maxlength'));

            if (parseInt(fullNameOfFamilyMember.length) === 0 || parseInt(fullNameOfFamilyMember.length) > parseInt(maximumLength)) {
                result = false;
                $('#full-name-of-family-member-error').removeClass('d-none');
            }

            //reference Number
            maximumLength = parseInt($('#trans-full-name-of-family-member').attr('maxlength'));

            if (parseInt(transFullNameOfFamilyMember.length) === 0 || parseInt(transFullNameOfFamilyMember.length) > parseInt(maximumLength)) {
                result = false;
                $('#trans-full-name-of-family-member-error').removeClass('d-none');
            }
        }

        // DocumentTypeId
        if ($('#relations-id').prop('selectedIndex') < 1) {
            result = false;
            $('#relations-id-error').removeClass('d-none');
        }

        //date of Birth
        if (IsValidInputDate('#birth-date-family-member') === false) {
            result = false;
            $('#birth-date-family-member-error').removeClass('d-none');
        }

        // DocumentTypeId
        if ($('#occupation-id').prop('selectedIndex') < 1) {
            result = false;
            $('#occupation-id-error').removeClass('d-none');
        }

        // Sanction Loan Amount
        if (isNaN(income) === false) {
            minimum = parseFloat($('#income').attr('min'));
            maximum = parseFloat($('#income').attr('max'));

            if (parseFloat(income) < parseFloat(minimum) || parseFloat(income) > parseFloat(maximum)) {
                result = false;
                $('#income-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#income-error').removeClass('d-none');
        }

        return result;

    }

    function HideColumnsFamilyDetailsDataTable() {
        personFamilyDataTable.column(1).visible(false);
        personFamilyDataTable.column(5).visible(false);
        personFamilyDataTable.column(8).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    function SetPageLoadingDefaultValues() {
        debugger;
        // Get Person Dropdown For Nominee
        $.get('/DynamicDropdownList/GetPersonDropdownListForNominee', function (data) {
            debugger;
            personDropdownListDataForFamily = data;
        });

    }


    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@


    $('#btnsave').click(function () {

        debugger;

        let isValidAllInputs = true;
        debugger;
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personFamilyDetailArray = new Array();

            // Create Array For person family detail Table To Pass Data
            if (!$('#heading-family-detail').hasClass('d-none')) {

                if (personFamilyDataTable.data().any()) {

                    $('#family-detail-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-family-detail > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (personFamilyDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                personFamilyDetailArray.push(
                                {
                                    'PersonInformationNumber': columnValues[1],
                                    'FullNameOfFamilyMember': columnValues[3],
                                    'TransFullNameOfFamilyMember': columnValues[4],
                                    'RelationId': columnValues[5],
                                    'BirthDate': columnValues[7],
                                    'OccupationId': columnValues[8],
                                    'Income': columnValues[10],
                                    'Note': columnValues[11],
                                    'TransNote': columnValues[12],
                                    'ReasonForModification': columnValues[13],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    //$('#family-detail-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }


            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: SaveFamilyDetailDataTable,
                    type: 'POST',
                    async: false,
                    data: {
                        '_familyDetail': personFamilyDetailArray,
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person DataTable!!! Error Message - ' + error.toString());
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