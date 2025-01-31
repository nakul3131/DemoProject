
//Unique Name Of Qualification
$("#name-of-qualification").on('change', function (event) {
    debugger;
    var NameOfQualification = $("#name-of-qualification").val();
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: ({ NameOfQualification: NameOfQualification }),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-qualification").next("div.validation").remove();
            }
            else {

                $("#name-of-qualification").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Education Qualification is already exist</div>");
            }
        },
        error: function (xhr) {
            alert("An error has occured!!!");
        }
    });
    $("#name-of-qualification").next("div.validation").remove();
});
