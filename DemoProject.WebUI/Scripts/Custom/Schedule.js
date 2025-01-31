$(document).ready(function () {

    //Unique Name Of Business Office 
    $("#name-of-schedule").on('change', function (event) {
        debugger;
        var nameOfSchedule = $("#name-of-schedule").val();
        $.ajax({
            url: uniqueNameOfSchedule,
            dataType: "json",
            type: "POST",
            data: ({ nameOfSchedule: nameOfSchedule }),
            success: function (data) {
                debugger;
                if (data) {
                    $("#name-of-schedule").next("div.validation").remove();
                }
                else {

                    $("#name-of-schedule").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Schedule is already exist</div>");
                }
            },
            error: function (xhr) {
                alert("An error has occured!!!");
            }
        });
        $("#name-of-schedule").next("div.validation").remove();
    });

    var x = 0; //Initial field counter
    var list_maxField = 10; //Input fields increment limitation

    $("#schedule-time").change(function () {
        debugger;
        $('#schedule-time').next("div.error").remove();

    })
    //Once add button is clicked
    $('.list_add_button').click(function () {
        debugger;
        var arr = new Array();
        time = $('input[name="ScheduleTime"]').val();
        var inputvalue = $("input[name='ScheduleTime[]']").map(function () {
            return $(this).val();
        }).get();

        $('input[name="ScheduleTime"]').each(function () {

            arr.push($(this).val());
        });


        arr.forEach(function (time) {
            debugger
            if (inputvalue.indexOf(time) != -1) {
                debugger
                $('#schedule-time').next("div.error").remove();
                $('#schedule-time').after('<div class="error" style="color:red">Please another schedule time </div>');
                $("#schedule-time").val('');
            }
            else {
                if ((time != "")) {
                    //Check maximum number of input fields
                    if (x < list_maxField) {
                        x++;
                        //Increment field counter
                        var list_fieldHTML = '<div class="row myclass"><div class="col-xs-11 col-sm-11 col-md-11"><div class="form-group"><input name="ScheduleTime[]" value=' + time + ' type = "time" class="form-control schedule-time" /></div ></div > <div class="col-xs-1 col-sm-1 col-md-1" style="margin-left:-5%;"><a href="javascript:void(0);" class="list_remove_button btn btn-danger"><i class="fas fa-minus"></i></a></div></div >'; //New input field html 
                        $('.list_wrapper').prepend(list_fieldHTML); //Add field html
                    }
                    $("#schedule-time").val('');
                }
            }
        });

    });

    //Once remove button is clicked
    $('.list_wrapper').on('click', '.list_remove_button', function () {
        $(this).closest('div.row').remove(); //Remove field html
        x--; //Decrement field counter

    })

    //Hide Textbox by ScheduleType dropdown Selection
    debugger;
    var Text = $("#schedule-type option:selected").val();

    if (Text == '') {
        $(".SpecifiedDate").hide();
        $(".DaysOfWeek").hide();
        $(".DaysOfMonth").hide();
        $(".NumberOfDays").hide();
    }

    //ScheduleType dropdown Selection change event
    $("#schedule-type").on("change", function () {
        debugger;
        ClearScheduleFrequencyDivErrors();

        var scheduleTypeId = $("#schedule-type option:selected").val();
        $.ajax({
            url: ScheduleModelurl,
            dataType: "json",
            type: "POST",
            data: ({ scheduleTypeId: scheduleTypeId }),
            success: function (data) {
                debugger;
                if (data == 2) {
                    $(".DaysOfWeek").hide();
                    $(".DaysOfMonth").hide();
                    $(".SpecifiedDate").show();
                    $(".NumberOfDays").hide();
                    $(".Recur").hide();
                    $(".IsEvery").hide();
                }
                else if (data == 5) {
                    $(".DaysOfWeek").show();
                    $(".DaysOfMonth").hide();
                    $(".SpecifiedDate").hide();
                    $(".NumberOfDays").hide();
                    $(".Recur").show();
                    $(".IsEvery").show();
                }
                else if (data == 6) {
                    $(".DaysOfWeek").hide();
                    $(".DaysOfMonth").show();
                    $(".SpecifiedDate").hide();
                    $(".NumberOfDays").hide();
                }
                else if (data == 0) {
                    $(".DaysOfWeek").hide();
                    $(".DaysOfMonth").hide();
                    $(".SpecifiedDate").hide();
                    $(".NumberOfDays").hide();
                    $(".Recur").hide();
                    $(".IsEvery").hide();
                }
                else if (data == 1) {
                    $(".DaysOfWeek").hide();
                    $(".DaysOfMonth").hide();
                    $(".SpecifiedDate").show();
                    $(".NumberOfDays").hide();
                    $(".Recur").hide();
                    $(".IsEvery").hide();
                }
                else if (data == 3) {
                    $(".DaysOfWeek").hide();
                    $(".DaysOfMonth").hide();
                    $(".SpecifiedDate").hide();
                    $(".NumberOfDays").show();
                    $(".Recur").hide();
                    $(".IsEvery").hide();
                }
                else if (data == 4) {
                    $(".DaysOfWeek").hide();
                    $(".DaysOfMonth").hide();
                    $(".SpecifiedDate").hide();
                    $(".NumberOfDays").show();
                    $(".Recur").hide();
                    $(".IsEvery").hide();
                }
                else if (data == 7) {
                    $(".DaysOfWeek").hide();
                    $(".DaysOfMonth").hide();
                    $(".SpecifiedDate").hide();
                    $(".NumberOfDays").hide();
                    $(".Recur").hide();
                    $(".IsEvery").hide();
                }
                else {
                    $(".Recur").show();
                    $(".IsEvery").show();
                }
            },
            error: function (xhr) {
                alert("An error has occured!!!");
            }
        });
    });

    ClearScheduleFrequencyValues();

    // Initialising & Configuring DataTables
    var scheduleFrequencyTable = $('#schedule-frequency-table').DataTable({

        "paging": true,
        "fixedHeader": true,
        "searching": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "orderMulti": false,
        "bserverSide": true,
        "Processing": true,
        "sScrollY": "40vh",
        "sScrollX": "100%",
        "sScrollXInner": "110%",
        "scrollX": true,
        "scrollCollapse": true,
        "scroller": true,
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "aaSorting": [],
        "columnDefs": [{
            "targets": [1],
            "visible": false
        }],

        columnDefs: [
            { orderable: false, targets: [13] }
        ],
        columnDefs: [{
            orderable: false,
            targets: 0

        }],

        "order": [2, 'desc'],
        dom: 'Bfrtip',
        dom: '<"float-left"B><"float-right"f>rt<"row"<"col-sm-2"l><"col-sm-6"i><"col-sm-4"p>>',

        buttons: [

            {
                text: 'New',
                attr: {
                    id: 'btn-addnew-schedule-frequency'
                },
                action: function (e, dt, node, config, type) {
                    debugger

                    event.preventDefault();
                    debugger;
                    $("#btn-update-schedule-frequency").hide();
                    $("#btn-add-schedule-frequency").show();
                    $('#title').html($('#title').html().replace('Edit', 'Add'));
                    ClearScheduleFrequencyValues();
                    ClearScheduleFrequencyDivErrors();

                    $('#Add-new-schedule-frequency').modal('show');

                    //Hide Textbox 
                    $(".SpecifiedDate").hide();
                    $(".DaysOfWeek").hide();
                    $(".DaysOfMonth").hide();

                    var rowNum = 0;
                    $('#btn-add-schedule-frequency').on('click', function (event) {

                        debugger
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var ScheduleTypeId = $("#schedule-type option:selected").val();
                        var ScheduleTypeText = $("#schedule-type option:selected").text();
                        var numberOfDays = $("#number-of-days").val().trim();
                        var DaysOfWeekId = $("#days-of-week option:selected").val();
                        var DaysOfWeekText = $("#days-of-week option:selected").text();
                        var DaysOfMonthId = $("#days-of-month option:selected").val();
                        var DaysOfMonthText = $("#days-of-month option:selected").text();
                        var scheduletime = new Array();
                        $('input.schedule-time').each(function () {
                            debugger;
                            var tt = $(this).val().split(':');

                            if (tt != "") {
                                [h, m] = tt;
                                var h = parseInt(tt[0]);
                                var m = parseInt(tt[1]);
                                var ampm = h >= 12 ? 'PM' : 'AM';
                                h = h % 12;
                                h = h ? +h : 12; // 0 should be 12
                                h = h < 10 ? '0' + h : h;
                                m = m < 10 ? '0' + m : m; // if minutes less than 10,    add a 0 in front of it ie: 6:6 -> 6:06
                                var strTime = h + ':' + m + ' ' + ampm;
                                scheduletime.push(strTime);
                            }

                        })

                        var SpecifiedDate = $("#specified-date").val().trim();
                        var Recur = $("#recur").val().trim();
                        var isEvery = $('input[name="IsEvery"]').is(':checked') ? "True" : "False";

                        //Pass Default if dropdown value not selected 
                        if ((DaysOfWeekId.trim().length < 36) || (DaysOfMonthId.trim().length < 36) || (SpecifiedDate == "")) {

                            if (DaysOfWeekId.trim().length < 36) {
                                DaysOfWeekId = "f2d67af4-07c8-467b-891e-660d4c140574";
                                DaysOfWeekText = "None";
                            }

                            if (DaysOfMonthId.trim().length < 36) {
                                DaysOfMonthId = "7be526e3-2066-49d3-abd9-63a3d60b0a7c";
                                DaysOfMonthText = "None";
                            }

                            if (SpecifiedDate == "")
                                SpecifiedDate = "2000-01-01";
                        }

                        //Validation
                        if ((ScheduleTypeId.trim().length < 36) || (numberOfDays == "") || (DaysOfWeekId.trim().length < 36) || (DaysOfMonthId.trim().length < 36) || (SpecifiedDate == "") || (Recur == "") || (scheduletime == "")) {
                            ClearScheduleFrequencyDivErrors();

                            if (ScheduleTypeId.trim().length < 36)
                                $('#schedule-type').after('<div class="error" style="color:red">Please Select Schedule Type </div>');

                            if (numberOfDays == "")
                                $('#recur').after('<div class="error" style="color:red">Please Enter Number Of Days</div>');

                            if (DaysOfWeekId.trim().length < 36)
                                $('#days-of-week').after('<div class="error" style="color:red">Please Select Days Of Week </div>');

                            if (DaysOfMonthId.trim().length < 36)
                                $('#days-of-month').after('<div class="error" style="color:red">Please Select Day Of Month </div>');

                            if (scheduletime == "")
                                $('#schedule-time').after('<div class="error" style="color:red">Please Select schedule time </div>');

                            if (SpecifiedDate == "")
                                $('#specified-date').after('<div class="error" style="color:red">Please Enter Specified Date</div>');

                            if (Recur == "")
                                $('#recur').after('<div class="error" style="color:red">Please Enter Recur</div>');

                            return false;
                        }
                        else {
                            for (var i = 0; i < scheduletime.length; i++) {

                                scheduleFrequencyTable.row.add([
                                    tag,
                                    ScheduleTypeId,
                                    ScheduleTypeText,
                                    numberOfDays,
                                    DaysOfWeekId,
                                    DaysOfWeekText,
                                    DaysOfMonthId,
                                    DaysOfMonthText,
                                    scheduletime[i],
                                    SpecifiedDate,
                                    Recur,
                                    isEvery,

                                ]).draw();
                            }

                            scheduleFrequencyTable.column(1).visible(false);
                            scheduleFrequencyTable.column(4).visible(false);
                            scheduleFrequencyTable.column(6).visible(false);
                            scheduleFrequencyTable.columns.adjust().draw();

                            ClearScheduleFrequencyValues();
                            ClearScheduleFrequencyDivErrors();

                            $(".schedule-time").val('');
                            $(".myclass").remove();
                            $(".list_remove_button").remove();
                            $("#schedule-time").show();
                            $(".list_add_button").show();
                            $('#Add-new-schedule-frequency').modal('hide');

                        }
                    });

                    return false;
                },
            },
            {


                text: 'Edit',
                attr: {
                    id: 'btn-edit-schedule-frequency'
                },
                //className: 'btn btn-Edit disabled',
                action: function (e, dt, node, config, String, indexes) {
                    debugger

                    $(".list_add_button").hide();
                    $("#btn-update-schedule-frequency").show();
                    $("#btn-add-schedule-frequency").hide();
                    $('#title').html($('#title').html().replace('Add', 'Edit'));
                    ClearScheduleFrequencyDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit-schedule-frequency').data('rowindex');
                        var id = $("#Add-new-schedule-frequency").attr("id");
                        var myModal = $('#' + id).modal();

                        [time, meridian] = columnValues[8].split(' ');
                        [hours, minutes] = time.split(':');
                        if (hours === '12') {
                            hours = '00';
                        }
                        if (meridian === 'PM') {
                            hours = parseInt(hours, 10) + 12;
                        }
                        var scheduletime = hours + ":" + minutes;

                        $("#mydiv").addClass("col-xs-12 col-sm-12 col-md-12").removeClass('col-xs-11 col-sm-11 col-md-11');

                        var d = new Date(columnValues[9]),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var specifiedDate = [year, month, day].join('-');

                        $('#schedule-type', myModal).val(columnValues[1]);
                        $('#number-of-days', myModal).val(columnValues[3]);
                        if (columnValues[3] != "0") {
                            $(".SpecifiedDate").hide();
                            $(".DaysOfWeek").hide();
                            $(".DaysOfMonth").hide();
                            $(".NumberOfDays").show();
                        }
                        $('#days-of-week', myModal).val(columnValues[4]);
                        if (columnValues[4] != "f2d67af4-07c8-467b-891e-660d4c140574") {
                            $(".SpecifiedDate").hide();
                            $(".DaysOfWeek").show();
                            $(".DaysOfMonth").hide();
                            $(".NumberOfDays").hide();
                        }
                        $('#days-of-month', myModal).val(columnValues[6]);
                        if (columnValues[6] != "7be526e3-2066-49d3-abd9-63a3d60b0a7c") {
                            $(".SpecifiedDate").hide();
                            $(".DaysOfWeek").hide();
                            $(".DaysOfMonth").show();
                            $(".NumberOfDays").hide();
                        }
                        $('#schedule-time', myModal).val(scheduletime);
                        $('#specified-date', myModal).val(specifiedDate);
                        if (specifiedDate != "2000-01-01") {
                            $(".SpecifiedDate").show();
                            $(".DaysOfWeek").hide();
                            $(".DaysOfMonth").hide();
                            $(".NumberOfDays").hide();
                        }
                        $('#recur', myModal).val(columnValues[10]);
                        $("#is-every", myModal).val(columnValues[11]);
                        if (columnValues[10] === "True") {
                            $("#is-every").prop("checked", true);
                        }
                        else {
                            $("#is-every").prop("checked", false);
                        }
                        myModal.modal({ show: true });
                    }

                    else {
                        $('.btn-Edit-schedule-frequency').addClass('disabled');
                        $("#model-Edit-schedule-frequency").modal("hide");
                    }


                    $(document).on('click', "#btn-update-schedule-frequency", function (event) {
                        debugger;
                        $('#selectAll').prop('checked', false);
                        colorchange();

                        debugger
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var ScheduleTypeId = $("#schedule-type option:selected").val();
                        var ScheduleTypeText = $("#schedule-type option:selected").text();
                        var numberOfDays = $("#number-of-days").val().trim();
                        var DaysOfWeekId = $("#days-of-week option:selected").val();
                        var DaysOfWeekText = $("#days-of-week option:selected").text();
                        var DaysOfMonthId = $("#days-of-month option:selected").val();
                        var DaysOfMonthText = $("#days-of-month option:selected").text();

                        var ScheduleTime = $("#schedule-time").val().trim();
                        var tt = ScheduleTime.split(':');
                        [h, m] = tt;
                        var h = parseInt(tt[0]);
                        var m = parseInt(tt[1]);
                        var ampm = h >= 12 ? 'PM' : 'AM';
                        h = h % 12;
                        h = h ? +h : 12; // 0 should be 12
                        h = h < 10 ? '0' + h : h;
                        m = m < 10 ? '0' + m : m; // if minutes less than 10,    add a 0 in front of it ie: 6:6 -> 6:06
                        var strTime = h + ':' + m + ' ' + ampm;

                        var SpecifiedDate = $("#specified-date").val().trim();
                        var Recur = $("#recur").val().trim();
                        var isEvery = $('input[name="IsEvery"]').is(':checked') ? "True" : "False";

                        //Pass Default if dropdown value not selected 
                        if ((DaysOfWeekId.trim().length < 36) || (DaysOfMonthId.trim().length < 36) || (SpecifiedDate == "")) {

                            if (DaysOfWeekId.trim().length < 36) {
                                DaysOfWeekId = "f2d67af4-07c8-467b-891e-660d4c140574";
                                DaysOfWeekText = "None";
                            }

                            if (DaysOfMonthId.trim().length < 36) {
                                DaysOfMonthId = "7be526e3-2066-49d3-abd9-63a3d60b0a7c";
                                DaysOfMonthText = "None";
                            }

                            if (SpecifiedDate == "")
                                SpecifiedDate = "2000-01-01";
                        }

                        //Validation
                        if ((ScheduleTypeId.trim().length < 36) || (numberOfDays == "") || (DaysOfWeekId.trim().length < 36) || (DaysOfMonthId.trim().length < 36) || (SpecifiedDate == "") || (Recur == "") || (scheduletime == "")) {
                            ClearScheduleFrequencyDivErrors();

                            if (ScheduleTypeId.trim().length < 36)
                                $('#schedule-type').after('<div class="error" style="color:red">Please Select Schedule Type </div>');

                            if (numberOfDays == "")
                                $('#schedule-type').after('<div class="error" style="color:red">Please Enter Number Of Days </div>');

                            if (DaysOfWeekId.trim().length < 36)
                                $('#days-of-week').after('<div class="error" style="color:red">Please Select Days Of Week </div>');

                            if (DaysOfMonthId.trim().length < 36)
                                $('#days-of-month').after('<div class="error" style="color:red">Please Select Day Of Month </div>');

                            if (scheduletime == "")
                                $('#schedule-time').after('<div class="error" style="color:red">Please Select schedule time </div>');

                            if (SpecifiedDate == "")
                                $('#specified-date').after('<div class="error" style="color:red">Please Enter Specified Date</div>');

                            if (Recur == "")
                                $('#recur').after('<div class="error" style="color:red">Please Enter Recur</div>');

                            return false;
                        }
                        else {
                            scheduleFrequencyTable.row($(this).attr('rowindex')).data([
                                tag,
                                ScheduleTypeId,
                                ScheduleTypeText,
                                numberOfDays,
                                DaysOfWeekId,
                                DaysOfWeekText,
                                DaysOfMonthId,
                                DaysOfMonthText,
                                strTime,
                                SpecifiedDate,
                                Recur,
                                isEvery,

                            ]).draw();

                            scheduleFrequencyTable.column(1).visible(false);
                            scheduleFrequencyTable.column(4).visible(false);
                            scheduleFrequencyTable.column(6).visible(false);
                            scheduleFrequencyTable.columns.adjust().draw();

                            $("#mydiv").addClass("col-xs-11 col-sm-11 col-md-11").removeClass('col-xs-12 col-sm-12 col-md-12');
                            $(".list_add_button").show();
                            $('#Add-new-schedule-frequency').modal('hide');
                            $('.btn-add-schedule-frequency').removeClass('disabled');
                            $('.btn-Delete-schedule-frequency').addClass('disabled');
                            $('.btn-Edit-schedule-frequency').addClass('disabled');
                        }
                    })
                },
            },

            {
                text: 'Delete',
                attr: {
                    id: 'btn-Delete-schedule-frequency'
                },

                action: function (e, dt, node, config) {
                    debugger;
                    var isChecked = $("input[type='checkbox']").is(":checked");
                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {
                            debugger;
                            if ($("#schedule-frequency-table input[type='checkbox']:checked").each(function () {
                                debugger;
                                scheduleFrequencyTable.row($("input[type='checkbox']:checked").parents('tr')).remove().draw();
                                $('.btn-add-schedule-frequency').removeClass('disabled');
                                $('.btn-Delete-schedule-frequency').addClass('disabled');
                                $('.btn-Edit-schedule-frequency').addClass('disabled');
                                $('#selectAll').prop('checked', false);

                            }));
                        }

                    }
                    else {
                        alert("Please select a checkbox");
                    }
                }
            },
        ]
    })

    $('.close').click(function () {
        ClearScheduleFrequencyValues();
        ClearScheduleFrequencyDivErrors();
        $('.btn-Delete-schedule-frequency').addClass('disabled');
        $('.btn-Edit-schedule-frequency').addClass('disabled');
        $('.checks').prop('checked', false);
        $('.btn-add-schedule-frequency').removeClass('disabled');
        $('#selectAll').prop('checked', false);
    });

    function colorchange() {
        debugger;
        var myid = $("input[type='checkbox']:checked").closest('tr').attr("id");

        $("#" + myid).animate({
            backgroundColor: "#B0C4DE"
        }, 500).animate({
            backgroundColor: "#F5F5F5"
        }, 500);
    }

    var btns = $('#btn-addnew-schedule-frequency');
    btns.addClass('btn btn-success  btn-add-schedule-frequency').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-schedule-frequency');
    btns.addClass('btn btn-primary btn-Edit-schedule-frequency disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-Delete-schedule-frequency');
    btns.addClass('btn btn-danger btn-Delete-schedule-frequency DeleteAll disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $("#selectAll").on('click', function () {
        debugger;
        if ($(this).prop('checked')) {
            var arr = new Array();
            $('#schedule-frequency-table tbody input[type="checkbox"]').each(function () {
                debugger
                $(this).prop('checked', true);
                debugger
                var row = $(this).closest("tr");
                selectedRow = scheduleFrequencyTable.row(row).index();
                var tdrow = (scheduleFrequencyTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-Delete-schedule-frequency').data('rowindex', arr);
                $('.btn-add-schedule-frequency').addClass('disabled');
                $('.btn-Edit-schedule-frequency').addClass('disabled');
                $('.btn-Delete-schedule-frequency').removeClass('disabled');

            });
        } else {
            $('#schedule-frequency-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-schedule-frequency').removeClass('disabled');
                $('.btn-Delete-schedule-frequency').addClass('disabled');
            });
        }
    });

    var name = window.location.pathname
        .split("/")
        .filter(function (c) { return c.length; })
        .pop();
    if (name === "Closed") {
        debugger;
        $('.btn-Delete').addClass('disabled');
        $('.btn-add-schedule-frequency').attr('disabled', true);
    }

    $('#schedule-frequency-table  tbody').on('click', 'input[type="checkbox"]', function () {
        debugger;
        var all = $('tbody input[type="checkbox"]');
        // getting only the checked checkboxes from that collection:
        checked = all.filter(':checked');
        if (name === "Closed") {
            debugger;
            if (all.length > 0 == checked.length) {
                debugger;
                $('.btn-Delete-schedule-frequency').addClass('disabled');

                $('.btn-Edit-schedule-frequency').removeClass('disabled');
            }
            else {
                $('.btn-Edit-schedule-frequency').addClass('disabled');

            }
            if (checked.length == 0) {
                $('.btn-Delete-schedule-frequency').addClass('disabled');
                $('.btn-add-schedule-frequency').removeClass('disabled');
            }
        }
        else {

        }
        if (all.length > 0 == checked.length) {

            $('.btn-add-schedule-frequency').addClass('disabled');
            $('.btn-Edit-schedule-frequency').removeClass('disabled');
            $('.btn-Delete-schedule-frequency').removeClass('disabled');
        }
        else {
            $('.btn-add-schedule-frequency').addClass('disabled');
            $('.btn-Edit-schedule-frequency').addClass('disabled');
            $('.btn-Delete-schedule-frequency').removeClass('disabled');
        }
        if (all.length === checked.length || !this.checked) {
            $('#selectAll').prop('checked', this.checked);
        }

        if (checked.length === 0) {
            $('.btn-add-schedule-frequency').removeClass('disabled');
            $('.btn-Delete-schedule-frequency').addClass('disabled');
            $('#selectAll').prop('checked', this.checked);
        }

    });

    $('#schedule-frequency-table tbody').on('click', "input[type=checkbox]", function () {
        $('#schedule-frequency-table input[type="checkbox"]:checked').each(function () {

            var isChecked = $(this).prop("checked");

            if (isChecked) {
                var arr = new Array();
                $("input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    selectedRow = scheduleFrequencyTable.row(row).index();
                    var tdrow = (scheduleFrequencyTable.row(selectedRow).data());

                    arr.push({
                        td0: tdrow[1]
                    });

                    if (name === "Closed") {
                        $('.btn-Delete-schedule-frequency').addClass('disabled');
                    }
                    else {
                        $('.btn-Delete-schedule-frequency').removeClass('disabled');
                    }
                    $('#btn-update-schedule-frequency').attr('rowindex', selectedRow);
                    $('.btn-Edit-schedule-frequency').data('rowindex', tdrow);
                    $('.btn-Delete-schedule-frequency').data('rowindex', arr);
                })
            }
        });
    });

    //To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    function ClearScheduleFrequencyValues() {
        debugger

        $("#schedule-type").val('');
        $("#number-of-days").val(0);
        $("#days-of-week").val('');
        $("#days-of-month").val('');
        $("#schedule-time").val('');
        $("#specified-date").val('');
        $("#recur").val(0);
        $('input[name="IsEvery"]').prop('checked', false);
    }

    // Clear Div Errors 
    function ClearScheduleFrequencyDivErrors() {
        debugger
        $('#schedule-type').next("div.error").remove();
        $('#number-of-days').next("div.error").remove();
        $('#days-of-week').next("div.error").remove();
        $('#days-of-month').next("div.error").remove();
        $('#schedule-time').next("div.error").remove();
        $('#specified-date').next("div.error").remove();
        $('#recur').next("div.error").remove();
    }

    //Code to save changes
    $('#btnsave').on('click', function (e) {
        debugger;

        // Return List Object, Hence Create Array
        var _ScheduleFrequency = new Array();

        $("#schedule-frequency-table > TBODY > TR").each(function () {

            debugger;
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (scheduleFrequencyTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                debugger;
                return false;
            }

            [time, meridian] = columnvalue[8].split(' ');

            [hours, minutes] = time.split(':');

            var scheduletime = hours + ":" + minutes;

            var scheduleType = columnvalue[1];
            var numberOfDays = columnvalue[3];
            var daysOfWeek = columnvalue[4];
            var daysOfMonth = columnvalue[6];
            var scheduleTime = scheduletime;
            var specifiedDate = columnvalue[9];
            var recur = columnvalue[10];
            var IsEvery = columnvalue[11];

            _ScheduleFrequency.push({

                "ScheduleTypeId": scheduleType,
                "NumberOfDays": numberOfDays,
                "DaysOfWeekId": daysOfWeek,
                "DaysOfMonthId": daysOfMonth,
                "ScheduleTime": scheduleTime,
                "SpecifiedDate": specifiedDate,
                "Recur": recur,
                "IsEvery": IsEvery,

            });
        });

        // Call Cantroller Save Method 
        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            data: { '_ScheduleFrequency': _ScheduleFrequency },
            success: function (data) {
            }
        })

    });
});
