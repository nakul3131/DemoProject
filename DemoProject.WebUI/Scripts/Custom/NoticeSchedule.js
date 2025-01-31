
$("body").on("click", ".btn-updateWeek", function () {

    debugger;

    var WeekDayId = $("#WeekDayId1 option:selected").val();
    var WeekDayId1 = $("#WeekDayId1").parents("tr").find("#WeekDayId1 option:selected").text();
    var WeekScheduleTime = $(this).parents("tr").find("input[id='WeekScheduleTime']").val();


    if (WeekDayId == "" || WeekScheduleTime == "") {
        // Changing HTML to draw attention
        $("#divErrorWeekDayId").html("Please Select WeekDayId");
        $("#divErrorWeekScheduleTime").html("Please Enter WeekScheduleTime");
        return false;
    }
    else {
        $("#divErrordivErrorWeekDayId").html("");
        $("#divErrorWeekScheduleTime").html("");
    }
    $(this).parents("tr").find("td:eq(0)").text(WeekDayId)
    $(this).parents("tr").find("td:eq(1)").text(WeekDayId1)
    $(this).parents("tr").find("td:eq(2)").text(WeekScheduleTime);

    $(this).parents("tr").attr('id', WeekDayId);
    $(this).parents("tr").attr('id', WeekScheduleTime);

    $(this).parents("tr").find(".btn-edit").show();
    $(this).parents("tr").find(".btn-cancel").remove();
    $(this).parents("tr").find(".btn-update").remove();

    debugger;


    var WeekSchedule = new Array();

    $("#tblWeek TBODY TR").each(function () {

        debugger;


        var row = $(this);

        var WeekDayId = row.find("TD").eq(0).text();
        var WeekScheduleTime = row.find("TD").eq(2).html();

        WeekSchedule.push({
            "WeekDayId": WeekDayId,
            "WeekScheduleTime": WeekScheduleTime,
        });

    });

    //Send the JSON array to Controller using AJAX.
    $.ajax({

        url: url,
        type: "POST",
        data: { '_weekSchedule': WeekSchedule },
        ContentType: "application/json; charset=utf-8",
        dataType: "JSON",
        success: function () {
            debugger;
            //alert( "record(s) inserted.");
        },

        error: function (xhr, status, error) {
            alert("An error has occured!!!");
        }

    });

});

$("body").on("click", "#add", function () {
    debugger;

    var WeekScheduleTime = $("input[id='WeekScheduleTime']").val();
    var html =
        '<tr>' +
        '<td>' + WeekScheduleTime +
        '</td>'
    '</tr>'
    $(html).appendTo($("#addtd"));

    // Clear the TextBoxes.
    $("input[id='WeekScheduleTime']").val('');

});

$("body").on("click", "#btnAddWeek", function () {
    debugger;

    var WeekDayId = $("#WeekDayId option:selected").val();
    var WeekDayId1 = $("#WeekDayId option:selected").text();
    var WeekScheduleTime = $("input[id='WeekScheduleTime']").val();

    // Validation for all Field
    if (WeekDayId == "") {
        $("#divErrorWeekDayId").html("Please Select WeekDayId");
        return false;
    }
    else {
        $("#divErrorWeekDayId").html("");
    }
    if (WeekScheduleTime == "") {
        $("#divErrorWeekScheduleTime").html("Please Enter WeekScheduleTime");
        return false;
    }
    else {
        $("#divErrorWeekScheduleTime").html("");
    }


    var html =
        '<tr>' +
        '<td style="display:none">' + WeekDayId +
        '<td>' + WeekDayId1 +
        '<td>' + WeekScheduleTime +
        '<td><button type="button" title="Edit" class="btn btn-info btn-sm btn-editWeek "><i class="fa fa-pencil-square-o"></i></button>&nbsp;<button type="button" title="Delete" class="btn btn-danger btn-sm btn-delete" id="delete"> <i class="fa fa-trash"></i></button ></td>' +
        '</tr>'
    $(html).appendTo($("#tblWeek")).css('width', '200');

    // Clear the TextBoxes.
    $("select[name='WeekDayId']").val('');
    $("input[id='WeekScheduleTime']").val('');

});

$("body").on("click", ".btn-delete", function () {
    debugger;
    $(this).parents("tr").remove();
});

$("body").on("click", ".btn-editWeek", function () {
    debugger;


    var WeekDayId = $("#WeekDayId option:selected").val();
    var DayOfWeekText = $(this).parents("tr").find('td').eq(1).text();
    var WeekScheduleTime = $(this).parents("tr").find('td').eq(2).text();


    $(this).parents("tr").find("td:eq(1)").html('<select class="form-control" style="width:90%" id="WeekDayId1"<option value = "' + WeekDayId + '">' + DayOfWeekText + '</option><option value="0">---Please Select---</option>' + WeekDayList + '</select ><span class="table-field-Required divError1"></span><span id="divError7" style="color:red;margin-left:5%;"></span>').attr("selected", "selected");
    $(this).parents("tr").find("td:eq(2)").html('<input class="form-control" type="time" id="WeekScheduleTime" value="' + WeekScheduleTime + '"><span id="divError8" style="color:red;margin-left:5%;"></span>');
    $(this).parents("tr").find("td:eq(3)").prepend('<button type="button" title="Add" class="btn btn-success btn-sm btn-updateWeek "><i class="fa fa-plus"></i></button>')
    $(this).hide();
    // Clear the TextBoxes.
    $("select[name='WeekDayId1']").val('');
    $("input[id='WeekScheduleTime']").val('');
});

var url1 = url;

// Month

$("body").on("click", "#btnAddMonth", function () {
    debugger;

    var MonthId = $("#MonthId option:selected").val();
    var MonthId1 = $("#MonthId option:selected").text();
    var MonthDayId = $("#MonthDayId option:selected").val();
    var MonthDayId1 = $("#MonthDayId option:selected").text();
    var MonthScheduleTime = $("input[id='MonthScheduleTime']").val();

    // Validation for all Field
    if (MonthId == "") {
        $("#divErrorMonthId").html("Please Select Month");
        return false;
    }
    else {
        $("#divErrorMonthId").html("");
    }
    if (MonthDayId == "") {
        $("#divErrorMonthDayId").html("Please Select Month Day");
        return false;
    }
    else {
        $("#divErrorMonthDayId").html("");
    }
    if (MonthScheduleTime == "") {
        $("#divErrorMonthScheduleTime").html("Please Enter MonthScheduleTime");
        return false;
    }
    else {
        $("#divErrorMonthScheduleTime").html("");
    }


    var html =
        '<tr>' +
        '<td style="display:none">' + MonthId +
        '<td>' + MonthId1 +
        '<td style="display:none">' + MonthDayId +
        '<td>' + MonthDayId1 +
        '<td>' + MonthScheduleTime +
        '<td ><button type="button" title="Edit" class="btn btn-info btn-sm btn-editMonth "><i class="fa fa-pencil-square-o"></i></button>&nbsp;<button type="button" title="Delete" class="btn btn-danger btn-sm btn-delete" id="delete"> <i class="fa fa-trash"></i></button ></td>' +
        '</tr>'

    $(html).appendTo($("#tblMonth")).css('width', '200');

    // Clear the TextBoxes.
    $("select[name='MonthId']").val('');
    $("select[name='MonthDayId']").val('');
    $("input[id='WeekScheduleTime']").val('');

});

$("body").on("click", ".btn-updateMonth", function () {

    debugger;

    var MonthId = $("#MonthId1 option:selected").val();
    var MonthId1 = $("#MonthId1").parents("tr").find("#MonthId1 option:selected").text();
    var MonthDayId = $("#MonthDayId1 option:selected").val();
    var MonthDayId1 = $("#MonthDayId1").parents("tr").find("#MonthDayId1 option:selected").text();
    var MonthScheduleTime = $("input[id='MonthScheduleTime']").val();

    if (MonthId == "" ||MonthDayId == "" || MonthScheduleTime == "") {
        // Changing HTML to draw attention
        $("#divErrorMonthId").html("Please Select MonthId");
        $("#divErrorMonthDayId").html("Please Select MonthDayId");
        $("#divErrorMonthScheduleTime").html("Please Enter MonthScheduleTime");
        return false;
    }
    else {
        $("#divErrordivErrorMonthId").html("");
        $("#divErrordivErrorMonthDayId").html("");
        $("#divErrorMonthScheduleTime").html("");
    }
    $(this).parents("tr").find("td:eq(0)").text(MonthId)
    $(this).parents("tr").find("td:eq(1)").text(MonthId1)
    $(this).parents("tr").find("td:eq(2)").text(MonthDayId)
    $(this).parents("tr").find("td:eq(3)").text(MonthDayId1)
    $(this).parents("tr").find("td:eq(4)").text(MonthScheduleTime);

    $(this).parents("tr").attr('id', MonthId);
    $(this).parents("tr").attr('id', MonthDayId);
    $(this).parents("tr").attr('id', MonthScheduleTime);

    $(this).parents("tr").find(".btn-edit").show();
    $(this).parents("tr").find(".btn-cancel").remove();
    $(this).parents("tr").find(".btn-update").remove();

    debugger;


    var MonthSchedule = new Array();

    $("#tblMonth TBODY TR").each(function () {

        debugger;


        var row = $(this);

        var MonthId = row.find("TD").eq(0).text();
        var MonthDayId = row.find("TD").eq(2).text();
        var MonthScheduleTime = row.find("TD").eq(4).html();

        MonthSchedule.push({
            "MonthId": MonthId,
            "MonthDayId": MonthDayId,
            "MonthScheduleTime": MonthScheduleTime,
        });

    });

    //Send the JSON array to Controller using AJAX.
    $.ajax({

        url: url,
        type: "POST",
        data: { '_monthSchedule': MonthSchedule },
        ContentType: "application/json; charset=utf-8",
        dataType: "JSON",
        success: function () {
            debugger;
            //alert( "record(s) inserted.");
        },

        error: function (xhr, status, error) {
            alert("An error has occured!!!");
        }

    });

});

$("body").on("click", ".btn-editMonth", function () {
    debugger;


    var MonthId = $("#MonthId option:selected").val();
    var MonthText = $(this).parents("tr").find('td').eq(1).text();
    var MonthDayId = $("#MonthDayId option:selected").val();
    var MonthDayText = $(this).parents("tr").find('td').eq(2).text();
    var MonthScheduleTime = $(this).parents("tr").find('td').eq(3).text();


    $(this).parents("tr").find("td:eq(1)").html('<select class="form-control" style="width:90%" id="MonthId1"<option value = "' + MonthId + '">' + MonthText + '</option><option value="0">---Please Select---</option>' + MonthList + '</select ><span class="table-field-Required divError1"></span><span id="divError7" style="color:red;margin-left:5%;"></span>').attr("selected", "selected");
    $(this).parents("tr").find("td:eq(3)").html('<select class="form-control" style="width:90%" id="MonthDayId1"<option value = "' + MonthDayId + '">' + MonthDayText + '</option><option value="0">---Please Select---</option>' + MonthDayList + '</select ><span class="table-field-Required divError1"></span><span id="divError7" style="color:red;margin-left:5%;"></span>').attr("selected", "selected");
    $(this).parents("tr").find("td:eq(4)").html('<input class="form-control" type="time" id="MonthScheduleTime" value="' + MonthScheduleTime + '"><span id="divError8" style="color:red;margin-left:5%;"></span>');
    $(this).parents("tr").find("td:eq(5)").prepend('<button type="button" title="Add" class="btn btn-success btn-sm btn-updateMonth "><i class="fa fa-plus"></i></button>')
    $(this).hide();
    // Clear the TextBoxes.
    $("select[name='MonthId1']").val('');
    $("select[name='MonthDayId1']").val('');
    $("input[id='MonthScheduleTime']").val('');
});


// Day

$("body").on("click", "#btnAddDay", function () {
    debugger;

    var DateScheduleTime = $("input[id='DateScheduleTime']").val();

    // Validation for all Field

    if (DateScheduleTime == "") {
        $("#divErrorDateScheduleTime").html("Please Enter DateScheduleTime");
        return false;
    }
    else {
        $("#divErrorDateScheduleTime").html("");
    }


    var html =
        '<tr>' +
        '<td>' + DateScheduleTime +
        '<td ><button type="button" title="Edit" class="btn btn-info btn-sm btn-editDay "><i class="fa fa-pencil-square-o"></i></button>&nbsp;<button type="button" title="Delete" class="btn btn-danger btn-sm btn-delete" id="delete"> <i class="fa fa-trash"></i></button ></td>' +
        '</tr>'

    $(html).appendTo($("#tblDay")).css('width', '200');

    // Clear the TextBoxes.
    $("input[id='DateScheduleTime']").val('');

});

$("body").on("click", ".btn-updateDay", function () {

    debugger;

    var DateScheduleTime = $(this).parents("tr").find("input[id='DateScheduleTime']").val();


    if (DateScheduleTime == "") {
        // Changing HTML to draw attention
        $("#divErrorDateScheduleTime").html("Please Enter DateScheduleTime");
        return false;
    }
    else {
        $("#divErrorDateScheduleTime").html("");
    }
    $(this).parents("tr").find("td:eq(0)").text(DateScheduleTime);

    $(this).parents("tr").attr('id', DateScheduleTime);

    $(this).parents("tr").find(".btn-edit").show();
    $(this).parents("tr").find(".btn-cancel").remove();
    $(this).parents("tr").find(".btn-update").remove();

    debugger;


    var DaySchedule = new Array();

    $("#tblDay TBODY TR").each(function () {

        debugger;


        var row = $(this);

        var DateScheduleTime = row.find("TD").eq(0).html();

        DaySchedule.push({
            "DateScheduleTime": DateScheduleTime,
        });

    });

    //Send the JSON array to Controller using AJAX.
    $.ajax({

        url: url,
        type: "POST",
        data: { '_daySchedule': DaySchedule },
        ContentType: "application/json; charset=utf-8",
        dataType: "JSON",
        success: function () {
            debugger;
            //alert( "record(s) inserted.");
        },

        error: function (xhr, status, error) {
            alert("An error has occured!!!");
        }

    });

});

$("body").on("click", ".btn-editDay", function () {
    debugger;


    var DateScheduleTime = $(this).parents("tr").find('td').eq(0).text();


    $(this).parents("tr").find("td:eq(0)").html('<input class="form-control" type="time" id="DateScheduleTime" value="' + DateScheduleTime + '"><span id="divError8" style="color:red;margin-left:5%;"></span>');
    $(this).parents("tr").find("td:eq(1)").prepend('<button type="button" title="Add" class="btn btn-success btn-sm btn-updateDay "><i class="fa fa-plus"></i></button>')
    $(this).hide();
    // Clear the TextBoxes.
    $("input[id='DateScheduleTime']").val('');
});

$("body").on("click", "#btnsave", function () {

    $(".lastrow").remove();

    debugger;
    var url = $(this).attr('request-url');

    var WeekSchedule = new Array();
    var MonthSchedule = new Array();
    var DaySchedule = new Array();

    $("#tblWeek TBODY TR").each(function () {

        debugger;

        var row = $(this);

        var WeekDayId1 = row.find("TD").eq(0).text();
        var WeekDayId = row.find("TD").eq(1).text();
        var WeekScheduleTime = row.find("TD").eq(2).html();

        WeekSchedule.push({
            "WeekDayId": WeekDayId1,
            "WeekScheduleTime": WeekScheduleTime,

        });

    });

    $("#tblMonth TBODY TR").each(function () {

        debugger;

        var row = $(this);

        var MonthId1 = row.find("TD").eq(0).text();
        var MonthId = row.find("TD").eq(1).text();
        var MonthDayId1 = row.find("TD").eq(2).text();
        var MonthDayId = row.find("TD").eq(3).text();
        var MonthScheduleTime = row.find("TD").eq(4).html();

        MonthSchedule.push({
            "MonthId": MonthId1,
            "MonthDayId": MonthDayId1,
            "MonthScheduleTime": MonthScheduleTime,

        });

    });

    $("#tblDay TBODY TR").each(function () {

        debugger;

        var row = $(this);

        var DateScheduleTime = row.find("TD").eq(0).html();

        DaySchedule.push({
            "DateScheduleTime": DateScheduleTime,

        });

    });

    //Send the JSON array to Controller using AJAX.
    $.ajax({

        url: url,
        type: "POST",
        data: { '_weekSchedule': WeekSchedule, '_monthSchedule': MonthSchedule, '_daySchedule': DaySchedule },
        ContentType: "application/json; charset=utf-8",
        dataType: "JSON",
        success: function () {
            debugger;
            //alert( "record(s) inserted.");
        },

        error: function (xhr, status, error) {
            alert("An error has occured!!!");
        }

    });

});
