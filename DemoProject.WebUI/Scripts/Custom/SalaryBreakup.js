
//Unique Name Of Center
$("#name-of-salary-breakup").on('change', function (event) {
    debugger;
    var NameOfSalaryBreakup = $("#name-of-salary-breakup").val();
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: ({ NameOfSalaryBreakup: NameOfSalaryBreakup }),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-salary-breakup").next("div.validation").remove();
            }
            else {

                $("#name-of-salary-breakup").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Salary Breakup is already exist</div>");
            }
        },
        error: function (xhr) {
            alert("An error has occured!!!");
        }
    });
    $("#name-of-salary-breakup").next("div.validation").remove();
});
