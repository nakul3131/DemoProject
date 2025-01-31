//Unique Name Of Designation
$("#name-of-designation").change(function (event) {
    debugger;
    let NameOfDesignation = $("#name-of-designation").val();
    $.ajax({
        url: url,
        dataType: "json",
        type: "POST",
        data: ({ NameOfDesignation: NameOfDesignation}),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-designation").next("div.validation").remove();
            }
            else {

                $("#name-of-designation").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Designation is already exist</div>");
            }
        },
        error: function (xhr) {
            alert("An error has occured!!!");
        }
    });
    $("#name-of-designation").next("div.validation").remove();
});