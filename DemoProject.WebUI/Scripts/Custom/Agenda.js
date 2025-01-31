
//Unique Name Of Business Office 
$("#name-of-agenda").on('change', function (event) {
    debugger;
    var nameOfAgenda = $("#name-of-agenda").val();
    $.ajax({
        url: uniqueNameOfAgenda,
        dataType: "json",
        type: "POST",
        data: ({ nameOfAgenda: nameOfAgenda }),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-agenda").next("div.validation").remove();
            }
            else {

                $("#name-of-agenda").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Agenda is already exist</div>");
            }
        },
        error: function (xhr) {
            alert("An error has occured!!!");
        }
    });
    $("#name-of-agenda").next("div.validation").remove();
});
