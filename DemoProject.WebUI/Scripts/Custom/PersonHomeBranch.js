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
    //PersonAddress
    let result = true;

    //Home Branch Dropdown Focusout
    let lastSelectedValue = $('#home-branch-id').val();

    $('#home-branch-id').focusout(function ()
    {
        debugger;
        let currentValue = $(this).val();

        // If the value has changed from the initial value, clear the related fields
        if (currentValue !== lastSelectedValue)
        {
            $('#language-id').val('');
            $('#activation-date-home-branch').val('');
            $('#note-home-branch').val('');
        }

        // Update lastSelectedValue to the current value for subsequent changes
        lastSelectedValue = currentValue;
    });


    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FOCUSOUT FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Enable Home Branch Accordion Input Validation
    $('.home-branch-input').focusout(function () {
        debugger;
        if (IsValidHomeBranchAccordionInputs())
            $('#home-branch-accordion-error').addClass('d-none');
    });


    // @@@@@@@@@@@@@@@@@@@@@@@@@@   VALIDATION FUNCTIONS FOR NORMAL ACCORDION VALIDITY  @@@@@@@@@@@@@@@@@@@@@@@@@@

   
    // 1. Home Branch Accordion Input Validation
    function IsValidHomeBranchAccordionInputs() {
        // Validate Date
        let isValidActivationDate = IsValidInputDate('#activation-date-home-branch');
        let note = $('#note-home-branch').val();
        let reasonForModification = $('reason-for-modification-home').val();
        result = true;

        //note
        if (note === '') {
            $('#note-home-branch').val('None');
            note = 'None';
        }

        //reason For Modification
        if (reasonForModification === '') {
            $('reason-for-modification-home').val('None');
            reasonForModification == 'None';
        }

        //Activation Date
        if (isValidActivationDate === false) {
            result = false;
        }

        //Home Branch Id
        if ($('#home-branch-id').prop('selectedIndex') < 1) {
            result = false;
        }

        //Language Id
        if ($('#language-id').prop('selectedIndex') < 1) {
            result = false;
        }

        // Show or hide error message based on validation result
        if (result) {
            $('#home-branch-accordion-error').addClass('d-none');
        }
        else {
            $('#home-branch-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;
        
        //if ($('form').valid() 
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // Accordion 3 - Home Branch Validation, If Enable
            if (IsValidHomeBranchAccordionInputs() === false) {
                isValidAllInputs = false;
            }
        }
        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });
});