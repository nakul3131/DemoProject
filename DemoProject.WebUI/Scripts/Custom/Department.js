//Unique Name Of Department
$("#name-of-department").on('change', function (event) {
    debugger;
    var NameOfDepartment = $("#name-of-department").val();
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: ({ NameOfDepartment: NameOfDepartment }),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-department").next("div.validation").remove();
            }
            else {

                $("#name-of-department").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Department is already exist</div>");
            }
        },
        error: function (xhr) {
            alert("An error has occured!!!");
        }
    });
    $("#name-of-department").next("div.validation").remove();
});