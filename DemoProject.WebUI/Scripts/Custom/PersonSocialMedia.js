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
    let selectedRowIndex = 0;
    let row = 0;
    let rowData = 0;
    let checked;
    let columnValues;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let arr = new Array();

    //Social Media
    let socialMediaId = '';
    let socialMediaIdText = '';
    let socialMediaLink = '';
    let otherDetails = '';
    let result = true;
    let note = '';
    let reasonForModification = '';
    let maximumLength = 0;
    let lastSelectedValue = '';

    // M A I N     P A G E     I N P U T     V A L I D A T I O N 

    // Create DataTables
    let socialMediaDataTable = CreateDataTable('social-media');

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    //clear vlaue
    $('#social-media-id').focusout(function () {
        debugger;
        let currentValue = $(this).val();

        if (currentValue !== lastSelectedValue) {
            $('#social-media-link').val('');
            $('#other-details-social-media').val('');
            $('#note-social-media').val('');
        }
        lastSelectedValue = currentValue;
    });

    // @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    /// @@@@@@@@@@@@@@@@@@@@@@   Social Media  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // DataTable Add Button 
    $('#btn-add-social-media-dt').click(function () {
        event.preventDefault();

        lastSelectedValue = '';

        SetModalTitle('social-media', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-social-media-dt').click(function () {
        SetModalTitle('social-media', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            // restricting the scope to a subset of the DOM Use myModal
            myModal = $('#social-media-modal').modal();

            columnValues = $('#btn-edit-social-media-dt').data('rowindex');

            lastSelectedValue = columnValues[1];

            $('#social-media-id', myModal).val(columnValues[1]);
            $('#social-media-link', myModal).val(columnValues[3]);
            $('#other-details-social-media', myModal).val(columnValues[4]);
            $('#note-social-media', myModal).val(columnValues[5]);
            $('#reason-for-modification-social-media', myModal).val(columnValues[6]);
            // Show Modals
            $('#social-media-modal').modal('show');
        }
        else {
            $('#btn-edit-social-media-edit-dt').addClass('read-only');
            $('#social-media-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-social-media-modal').click(function (event) {
        if (IsValidSocialMediaModal()) {
            row = socialMediaDataTable.row.add([
                tag,
                socialMediaId,
                socialMediaIdText,
                socialMediaLink,
                otherDetails,
                note,
                reasonForModification

            ]).draw();

            // Error Message In Span
            $('#social-media-data-table-error').addClass('d-none');

            HideColumnsSocialMediaDataTable();

            socialMediaDataTable.columns.adjust().draw();

            $('#social-media-modal').modal('hide');

            EnableNewOperation('social-media');
        }
    });

    // Modal update Button Event
    $('#btn-update-social-media-modal').click(function (event) {
        $('#select-all-social-media').prop('checked', false);
        if (IsValidSocialMediaModal()) {
            socialMediaDataTable.row(selectedRowIndex).data([
                tag,
                socialMediaId,
                socialMediaIdText,
                socialMediaLink,
                otherDetails,
                note,
                reasonForModification
            ]).draw();

            HideColumnsSocialMediaDataTable();

            socialMediaDataTable.columns.adjust().draw();

            $('#social-media-modal').modal('hide');

            EnableNewOperation('social-media');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-social-media-dt').click(function (event) {
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete Selected Record?')) {
                if ($('#tbl-social-media tbody input[type="checkbox"]:checked').each(function () {
                    socialMediaDataTable.row($('#tbl-social-media tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-social-media-dt').data('rowindex');
                    EnableNewOperation('social-media');

                    $('#select-all-social-media').prop('checked', false);

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-social-media').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-social-media tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = socialMediaDataTable.row(row).index();

                rowData = (socialMediaDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-social-media-dt').data('rowindex', arr);
                EnableDeleteOperation('social-media');
            });
        }
        else {
            EnableNewOperation('social-media');

            $('#tbl-social-media tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-social-media tbody').click('input[type=checkbox]', function () {
        $('#tbl-social-media input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = socialMediaDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (socialMediaDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('social-media');

                    $('#btn-update-social-media-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-social-media-dt').data('rowindex', rowData);
                    $('#btn-delete-social-media-dt').data('rowindex', arr);
                    $('#select-all-social-media').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-social-media tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length === 0)
            EnableNewOperation('social-media');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length === 1)
            EnableEditDeleteOperation('social-media');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('social-media');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-social-media').prop('checked', true);
        else
            $('#select-all-social-media').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidSocialMediaModal() {
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        socialMediaId = $('#social-media-id option:selected').val();
        socialMediaIdText = $('#social-media-id option:selected').text();
        socialMediaLink = $('#social-media-link').val();
        otherDetails = $('#other-details-social-media').val();
        note = $('#note-social-media').val();
        reasonForModification = $('#reason-for-modification-social-media').val();

        // Set Default Value, If Empty
        if (note === '') {
            $('#note-social-media').val('None');
            note = 'None';
        }

        if (reasonForModification === '') {
            $('#reason-for-modification-social-media').val('None');
            reasonForModification = 'None';
        }

        if (socialMediaLink === '') {
            socialMediaLink = 'None';
        }

        if (otherDetails === '') {
            otherDetails = 'None';
        }

        //Social media 
        if ($('#social-media-id').prop('selectedIndex') < 1) {
            result = false;
            $('#social-media-id-error').removeClass('d-none');
        }

        //social Media Link
        if (isNaN(socialMediaLink.length) === false) {
            maximumLength = parseInt($('#social-media-link').attr('maxlength'));

            if (parseInt(socialMediaLink.length) > parseInt(maximumLength)) {
                result = false;
                $('#social-media-link-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#social-media-link-error').removeClass('d-none');
        }

        //other Details
        if (isNaN(otherDetails.length) === false) {
            maximumLength = parseInt($('#other-details-social-media').attr('maxlength'));

            if (parseInt(otherDetails.length) > parseInt(maximumLength)) {
                result = false;
                $('#other-details-social-media-error').removeClass('d-none');
            }
        }
        else {
            result = false;
            $('#other-details-social-media-error').removeClass('d-none');
        }

        return result;
    }

    // Hide Unnecessary Columns
    function HideColumnsSocialMediaDataTable() {
        socialMediaDataTable.column(1).visible(false);
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {

        let isValidAllInputs = true;

        //if ($('form').valid() 
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
            let personSocialMediaArray = new Array();

            // Create Array For person social media case Table To Pass Data
            if (socialMediaDataTable.data().any()) {

                if (isValidAllInputs) {

                    $('#tbl-social-media > tbody > tr').each(function () {
                        currentRow = $(this).closest('tr');

                        columnValues = (socialMediaDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues !== 'undefined' && columnValues !== null) {
                            personSocialMediaArray.push(
                            {
                                'SocialMediaId': columnValues[1],
                                'SocialMediaLink': columnValues[3],
                                'OtherDetails': columnValues[4],
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
                        '_socialMedia': personSocialMediaArray
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Person Social Media DataTable!!! Error Message - ' + error.toString());
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