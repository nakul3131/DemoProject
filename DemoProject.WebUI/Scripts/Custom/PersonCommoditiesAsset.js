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

    //Commodities Asset
    let result = true;
    let minimum;
    let maximum;

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   focusout Event  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Enable Commodities Asset Accordion Input Validation
    $('.commodities-asset-input').focusout(function () {
        if (IsValidCommoditiesAssetAccordionInputs())
            $('#commodities-asset-accordion-error').addClass('d-none');
    });

    // 5.Person Commodities Asset Accordion Input Validation
    function IsValidCommoditiesAssetAccordionInputs() {
        let goldOrnament = parseFloat($('#gold-ornaments').val());
        let silverOrnament = parseFloat($('#silver-ornaments').val());
        let platinumOrnament = parseFloat($('#platinum-ornaments').val());
        let diamondInGoldOrnament = parseInt($('#number-of-diamonds-in-gold-ornaments').val());
        let reasonformodification = $('#reason-for-modification-commodities').val();
        result = true;

        if (reasonformodification === '') {
            $('#reason-for-modification-commodities').val('None');
            reasonformodification = 'None';
        }

        // Gold Ornament
        if (isNaN(goldOrnament) === false) {

            minimum = parseFloat($('#gold-ornaments').attr('min'));
            maximum = parseFloat($('#gold-ornaments').attr('max'));

            if (parseFloat(goldOrnament) < parseFloat(minimum) || parseFloat(goldOrnament) > parseFloat(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Silver Ornament
        if (isNaN(silverOrnament) === false) {

            minimum = parseFloat($('#silver-ornaments').attr('min'));
            maximum = parseFloat($('#silver-ornaments').attr('max'));

            if (parseFloat(silverOrnament) < parseFloat(minimum) || parseFloat(silverOrnament) > parseFloat(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Platinum Ornament
        if (isNaN(platinumOrnament) === false) {

            minimum = parseFloat($('#platinum-ornaments').attr('min'));
            maximum = parseFloat($('#platinum-ornaments').attr('max'));

            if (parseFloat(platinumOrnament) < parseFloat(minimum) || parseFloat(platinumOrnament) > parseFloat(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }

        // Diamond Gold Ornament
        if (isNaN(diamondInGoldOrnament) === false) {

            minimum = parseInt($('#number-of-diamonds-in-gold-ornaments').attr('min'));
            maximum = parseInt($('#number-of-diamonds-in-gold-ornaments').attr('max'));

            if (parseInt(diamondInGoldOrnament) < parseInt(minimum) || parseInt(diamondInGoldOrnament) > parseInt(maximum)) {
                result = false;
            }
        }
        else {
            result = false;
        }


        if (result) {
            $('#commodities-asset-accordion-error').addClass('d-none');
        }
        else {
            $('#commodities-asset-accordion-error').removeClass('d-none');
        }

        return result;
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {
        let isValidAllInputs = true;

        //if ($('form').valid()
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // Accordion - Person Commodities Asset Validation, If Enable
            if (IsValidCommoditiesAssetAccordionInputs() === false) {
                isValidAllInputs = false;
            }
        }
        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });
});