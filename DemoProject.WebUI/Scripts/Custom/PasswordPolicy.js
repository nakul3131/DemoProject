$(document).ready(function () {
    debugger;
    // Set Max Limit Based On Related Fields And Max Password Length
    function setMaxLimit(elementId, relatedFields) {
        // get max password length from input
        let maxLength = parseInt($('#maximum-number-password-length').val());

        // calculate the total of the related fields' values (defaults to 0 if invalid)
        let total = relatedFields.reduce((sum, field) => sum + (parseInt($(field).val()) || 0), 0);

        // calculate max limit: maxLength - total of related fields
        let maxLimit = Math.max(0, maxLength - total);

        // set the min and max attributes for the target element
        $(elementId).attr('min', 0).attr('max', isNaN(maxLength) ? '' : maxLimit);

        // if current value exceeds max limit, adjust it to the max limit
        if (parseInt($(elementId).val()) > maxLimit) {
            $(elementId).val(maxLimit);
        }
    }

    // All Ids For Calculating Max Limit
    let fields = [
        '#minimum-number-of-lower-case-characters',
        '#minimum-number-of-upper-case-characters',
        '#minimum-number-of-numeric-case-characters',
        '#minimum-number-of-special-case-characters'
       
    ];

    //Set Max Limit When Field Is Focused
    fields.forEach(field => {
        $(field).focus(() => {
            setMaxLimit(field, fields.filter(f => f !== field));  
        });
    });

    // Clear All Fields And Remove Max Attributes And Set Max Repetitive Character
    $('#maximum-number-password-length').focusout(() => {
        let passwordLength = parseInt($('#maximum-number-password-length').val());

        if (!isNaN(passwordLength)) {
            let maximum = Math.floor(passwordLength / 2);
            $('#minimum-number-of-repetitive-characters').attr('max', maximum);
        }
        else {
            $('#minimum-number-of-repetitive-characters').val('').removeAttr('max');
        }
        fields.forEach(field => {
            $(field).val('').removeAttr('max');  
        });
    });



});



