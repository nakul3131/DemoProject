
//Unique Name Of Center
$("#name-of-schedule").on('change', function (event) {
    debugger;
    var NameOfSchedule = $("#name-of-schedule").val();
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: ({ NameOfSchedule: NameOfSchedule }),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-schedule").next("div.validation").remove();
            }
            else {

                $("#name-of-schedule").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Working schedule is already exist</div>");
            }
        },
        error: function (xhr) {
            alert("An error has occured!!!");
        }
    });
    $("#name-of-schedule").next("div.validation").remove();
});
