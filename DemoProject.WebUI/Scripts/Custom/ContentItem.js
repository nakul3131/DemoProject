
//Unique Name Of Center
$("#name-of-content-item").on('change', function (event) {
    debugger;
    var NameOfContentItem = $("#name-of-content-item").val();
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: ({ NameOfContentItem: NameOfContentItem }),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-content-item").next("div.validation").remove();
            }
            else {

                $("#name-of-content-item").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of ContentItem is already exist</div>");
            }
        },
        error: function (xhr) {
            alert("An error has occured!!!");
        }
    });
    $("#name-of-content-item").next("div.validation").remove();
});
