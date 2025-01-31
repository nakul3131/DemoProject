
$(".center-category").on('change', function (event) {
    debugger
    $("#name-of-center").val("");
    $("#trans-name-of-center").val("");
    $("#name-of-center").next("div.validation").remove();

    // SubContinent 
    if ($(this).val() == '11') {
        $('#center-name').text('Name Of SubContinent');
        $('#continentDropDown').show();
    }
        //Continent
    else if ($(this).val() == '12') {
        $('#textboxes').show();
        $('#center-name').text('Name Of Continent');
        $('#continentDropDown').hide();
    }

        // Taluka
    else if ($(this).val() == '4') {
        $('#center-name-taluka').text('Name Of Taluka');
        $("#name-of-center-taluka").attr("placeholder", "Enter Name Of Taluka");
        $('#trans-center-name-taluka').text('तालुक्याचे नाव');
        $("#trans-name-of-center-taluka").attr("placeholder", "तालुक्याचे नाव प्रविष्ट करा");
        $('#sub-division-dropdown').show();
        $('#district-dropdown').hide();

    }
        //SubDivision
    else if ($(this).val() == '5') {
        $('#textboxes').show();
        $('#center-name-taluka').text('Name Of SubDivision');
        $("#name-of-center-taluka").attr("placeholder", "Enter Name Of SubDivision");
        $('#trans-center-name-taluka').text('उपविभागाचे नाव');
        $("#trans-name-of-center-taluka").attr("placeholder", "उपविभागाचे नाव प्रविष्ट करा");
        $('#district-dropdown').show();
        $('#sub-division-dropdown').hide();
    }
        //State
    else if ($(this).val() == '8') {
        $('#center-name').text('Name Of State');
    }
        //UnionTerritories
    else {
        $('#center-name').text('Name Of UnionTerritories');
    }
});

//Unique Name Of Center
$("#name-of-center").on('change', function (event) {
    debugger;
    var CenterCategoryPrmKey = $(".center-category:checked").val();
    var NameOfCenter = $("#name-of-center").val();
    if (CenterCategoryPrmKey == undefined) {
        CenterCategoryPrmKey = 0;
    }
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: ({ NameOfCenter: NameOfCenter, CenterCategoryPrmKey: CenterCategoryPrmKey }),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-center").next("div.validation").remove();
            }
            else {

                $("#name-of-center").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Center is already exist</div>");
            }
        },
        error: function (xhr) {
            alert("An error has occured!!!");
        }
    });
    $("#name-of-center").next("div.validation").remove();
});

$(document).ready(function () {

    debugger;
    //SubContinent
    if ($(".center-category:checked").val() == '11') {
        $('#center-name').text('Name Of SubContinent');
        $('#continentDropDown').show();
    }
        //Continent
    else if ($(".center-category:checked").val() == '12') {
        $('#textboxes').show();
        $('#center-name').text('Name Of Continent');
        $('#continentDropDown').hide();
    }

        //Taluka
    else if ($(".center-category:checked").val() == '4') {
        $('#sub-division-dropdown').show();
        $('#district-dropdown').hide();
        $('#center-name').text('Name Of Taluka');
    }
        //Subdivision
    else if ($(".center-category:checked").val() == '5') {
        $('#sub-division-dropdown').hide();
        $('district-dropdown').show();
        $('#center-name').text('Name Of SubDivision');
    }
        //State
    else if ($(".center-category:checked").val() == '8') {
        $('#center-name').text('Name Of State');
    }
        //UnionTerritories
    else if ($(".center-category:checked").val() == '9') {
        $('#center-name').text('Name Of UnionTerritories');
    }
    else {
        $('#center-name').text('Name Of Center');
    }
});
