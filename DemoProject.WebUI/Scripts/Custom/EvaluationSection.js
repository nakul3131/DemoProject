
//Unique Name Of Center
$("#name-of-evaluation-section").on('change', function (event) {
    debugger;
    var NameOfEvaluationSection = $("#name-of-evaluation-section").val();
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: ({ NameOfEvaluationSection: NameOfEvaluationSection }),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-evaluation-section").next("div.validation").remove();
            }
            else {

                $("#name-of-evaluation-section").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Evaluation Section is already exist</div>");
            }
        },
        error: function (xhr) {
            alert("An error has occured!!!");
        }
    });
    $("#name-of-evaluation-section").next("div.validation").remove();
});
