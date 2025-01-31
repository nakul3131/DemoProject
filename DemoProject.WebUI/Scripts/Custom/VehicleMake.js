'use strict';
$(document).ready(function () {
    debugger;
    // @@@@@@@@@@ Data Table Related Varible Declaration
     
    let isValidNameOfVehicleMake = true;

    //Validate Unique Name of Employee Code
    $('#name-of-vehicle-make').focusout(function (event) {
        debugger;
        let nameOfVehicleMake = $('#name-of-vehicle-make').val();

        $.get('/AccountChildAction/IsUniqueNameOfVehicleMake', { _nameOfVehicleMake: nameOfVehicleMake, async: false }, function (data, textStatus, jqXHR) {
            if (data && nameOfVehicleMake != '') {
                isValidNameOfVehicleMake = true;
                $('#name-of-vehicle-make-error').addClass('d-none');
            }
            else {
                isValidNameOfVehicleMake = false;
                $('#name-of-vehicle-make-error').removeClass('d-none');
            }
        });
    });

   
    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function () {

        debugger;
 
        if ($('form').valid()) {

            // Validate Employee Code
            if (!isValidNameOfVehicleMake) {
                event.preventDefault();
                isValidNameOfVehicleMake = false;
                $('#name-of-vehicle-make-error').removeClass('d-none');
            }
            else
                $('#name-of-vehicle-make-error').addClass('d-none');
        }
        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });
});