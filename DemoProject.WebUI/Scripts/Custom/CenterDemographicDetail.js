$(document).ready(function () {

    let minimumLength = 0;
    let maximumLength = 0;

    let minimum;
    let maximum;

    $('.center-demographic-detail-input').focusout(function () {
        debugger;
        IsValidCenterDemographicDetailAccordionInputs();
    });

    function IsValidCenterDemographicDetailAccordionInputs() {
        debugger;
        let result = true;
       
        let pincode = $('#pincode').val();
        let population = $('#population').val();
        let perCapitaIncome = $('#per-capita-income').val();
        let numberOfResidentsOwningHomes = $('#number-of-residents-owning-homes').val();

        if ($('#local-government').prop('selectedIndex') < 1) {
            result = false;
        }

        if ($('#diretion').prop('selectedIndex') < 1) {
            result = false;
        }

        if ($('#area-type').prop('selectedIndex') < 1) {
            result = false;
        }

        if ($('#educational-level').prop('selectedIndex') < 1) {
            result = false;
        }

        if ($('#family-system').prop('selectedIndex') < 1) {
            result = false;
        }

        if (isNaN(pincode.length) === false) {
            minimumLength = parseInt($('pincode').attr('minlength'));
            maximumLength = parseInt($('pincode').attr('maxlength'));

            if (parseInt(pincode.length) < parseInt(minimumLength) || parseInt(pincode.length) > parseInt(maximumLength)) {
                result = false;
            }
        }


        if (isNaN(population) === false) {
            minimum = $('#population').attr('min');
            maximum = $('#population').attr('max');

            if (parseInt(population) < parseInt(minimum) || parseInt(population) > parseInt(maximum)) {
                result = false;
            }
        }

        if (isNaN(perCapitaIncome) === false) {
            minimum = $('#per-capita-income').attr('min');
            maximum = $('#per-capita-income').attr('max');

            if (parseFloat(perCapitaIncome) < parseFloat(minimum) || parseFloat(perCapitaIncome) > parseFloat(maximum)) {
                result = false;
            }
        }

        if (isNaN(numberOfResidentsOwningHomes) === false) {
            minimum = $('#number-of-residents-owning-homes').attr('min');
            maximum = $('#number-of-residents-owning-homes').attr('max');

            if (parseInt(numberOfResidentsOwningHomes) < parseInt(minimum) || parseInt(numberOfResidentsOwningHomes) > parseInt(maximum)) {
                result = false;
            }
        }

        return result;
    }

    $('#btnsave').click(function () {
        debugger;
        let isValidAllInputs = true;

        //if ($('form').valid() 
        if ($('form').valid()) {
            // not add event.preventDefault
            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.
          
            if (IsValidCenterDemographicDetailAccordionInputs() === false) {
                isValidAllInputs = false;
            }
        }
        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });

});