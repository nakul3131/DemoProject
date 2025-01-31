$(document).ready(function () {

    debugger
    var model = '<option value="">-- Select Meeting Type --</option>';
    var Variants = $("#meeting-type");
    Variants.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    debugger
    $.ajax({
        type: "post",
        url: urlGetModel,
        data: { AgendaId: $('#agenda').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            debugger
            $('#meeting-type').empty();
            for (var i = 0; i < data.length; i++) {
                model += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#meeting-type').append(model);
        }
    });

    ClearAgendaMeetingTypeValues();

    // Initialising & Configuring DataTables
    var agendameetingtypeTable = $('#agenda-meeting-type-table').DataTable({

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
            { orderable: false, targets: [5] }
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
                    id: 'btn-addnew-agenda-meeting-type'
                },
                action: function (e, dt, node, config, type) {
                    debugger
                    event.preventDefault();
                    debugger;
                    $("#btn-update-agenda-meeting-type").hide();
                    $("#btn-add-agenda-meeting-type").show();
                    $('#title').html($('#title').html().replace('Edit', 'Add'));
                    ClearAgendaMeetingTypeValues();
                    ClearAgendaMeetingTypeDivErrors();

                    $('#Add-new-agenda-meeting-type').modal('show');

                    var rowNum = 0;

                    $('#btn-add-agenda-meeting-type').on('click', function (event) {

                        debugger
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var MeetingTypeId = $("#meeting-type option:selected").val();
                        var MeetingTypeText = $("#meeting-type option:selected").text();
                        var ActivationDate = $("#activation-date").val().trim();
                        var ExpiryDate = $("#expiry-date").val().trim();

                        if ((MeetingTypeId.trim().length < 36) || (ActivationDate == "") || (ExpiryDate == "")) {
                            ClearAgendaMeetingTypeDivErrors();

                            if (MeetingTypeId.trim().length < 36)
                                $('#meeting-type').after('<div class="error" style="color:red">Please Select Meeting Type </div>');

                            if (ActivationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter ActivationDate</div>');

                            if (ExpiryDate == "")
                                $('#expiry-date').after('<div class="error" style="color:red">Please Enter ExpiryDate</div>');
                            return false;
                        }
                        else {
                            var row = agendameetingtypeTable.row.add([
                                tag,
                                MeetingTypeId,
                                MeetingTypeText,
                                ActivationDate,
                                ExpiryDate,

                            ]).draw();

                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            agendameetingtypeTable.column(1).visible(false);
                            agendameetingtypeTable.columns.adjust().draw();
                            ClearAgendaMeetingTypeValues();
                            ClearAgendaMeetingTypeDivErrors();

                            $('#meeting-type').next("div.Error").remove();
                            $('#activation-date').next("div.Error").remove();
                            $('#expiry-date').next("div.Error").remove();

                            $('#Add-new-agenda-meeting-type').modal('hide');

                        }
                    });
                    return false;
                },
            },
            {

                text: 'Edit',
                attr: {
                    id: 'btn-edit-agenda-meeting-type'
                },
                //className: 'btn btn-Edit disabled',
                action: function (e, dt, node, config, String, indexes) {
                    debugger

                    $("#btn-update-agenda-meeting-type").show();
                    $("#btn-add-agenda-meeting-type").hide();
                    $('#title').html($('#title').html().replace('Add', 'Edit'));
                    ClearAgendaMeetingTypeDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit-agenda-meeting-type').data('rowindex');
                        var id = $("#Add-new-agenda-meeting-type").attr("id");
                        var myModal = $('#' + id).modal();

                        var d1 = new Date(columnValues[2]),
                            month = '' + (d1.getMonth() + 1),
                            day = '' + d1.getDate(),
                            year = d1.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var activationDate = [year, month, day].join('-');

                        var d2 = new Date(columnValues[3]),
                            month = '' + (d2.getMonth() + 1),
                            day = '' + d2.getDate(),
                            year = d2.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var expiryDate = [year, month, day].join('-');

                        $('#meeting-type', myModal).val(columnValues[1]);
                        $('#activation-date', myModal).val(activationDate);
                        $('#expiry-date', myModal).val(expiryDate);

                        myModal.modal({ show: true });
                    }

                    else {
                        $('.btn-Edit-agenda-meeting-type').addClass('disabled');
                        $("#model-Edit-agenda-meeting-type").modal("hide");
                    }


                    $(document).on('click', "#btn-update-agenda-meeting-type", function (event) {
                        debugger;
                        $('#selectAll').prop('checked', false);
                        colorchange();

                        debugger
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var MeetingTypeId = $("#meeting-type option:selected").val();
                        var MeetingTypeText = $("#meeting-type option:selected").text();
                        var ActivationDate = $("#activation-date").val().trim();
                        var ExpiryDate = $("#expiry-date").val().trim();

                        if ((MeetingTypeId.trim().length < 36) || (ActivationDate == "") || (ExpiryDate == "")) {
                            ClearAgendaMeetingTypeDivErrors();

                            if (MeetingTypeId.trim().length < 36)
                                $('#meeting-type').after('<div class="error" style="color:red">Please Select Meeting Type </div>');

                            if (ActivationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter ActivationDate</div>');

                            if (ExpiryDate == "")
                                $('#expiry-date').after('<div class="error" style="color:red">Please Enter ExpiryDate</div>');

                            return false;
                        }
                        else {
                            agendameetingtypeTable.row($(this).attr('rowindex')).data([
                                tag,
                                MeetingTypeId,
                                MeetingTypeText,
                                ActivationDate,
                                ExpiryDate,

                            ]).draw();

                            agendameetingtypeTable.column(1).visible(false);

                            agendameetingtypeTable.columns.adjust().draw();
                            $('#Add-new-agenda-meeting-type').modal('hide');
                            $('.btn-add-agenda-meeting-type').removeClass('disabled');
                            $('.btn-Delete-agenda-meeting-type').addClass('disabled');
                            $('.btn-Edit-agenda-meeting-type').addClass('disabled');
                        }
                    })
                },
            },

            {
                text: 'Delete',
                attr: {
                    id: 'btn-Delete-agenda-meeting-type'
                },

                action: function (e, dt, node, config) {
                    debugger;
                    var isChecked = $("input[type='checkbox']").is(":checked");
                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {
                            debugger;
                            if ($("#agenda-meeting-type-table input[type='checkbox']:checked").each(function () {
                                debugger;
                                agendameetingtypeTable.row($("input[type='checkbox']:checked").parents('tr')).remove().draw();
                                $('.btn-add-agenda-meeting-type').removeClass('disabled');
                                $('.btn-Delete-agenda-meeting-type').addClass('disabled');
                                $('.btn-Edit-agenda-meeting-type').addClass('disabled');
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
        ClearAgendaMeetingTypeValues();
        ClearAgendaMeetingTypeDivErrors();
        $('.btn-Delete-agenda-meeting-type').addClass('disabled');
        $('.btn-Edit-agenda-meeting-type').addClass('disabled');
        $('.btn-add-agenda-meeting-type').removeClass('disabled');
        $('.checks').prop('checked', false);
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

    var btns = $('#btn-addnew-agenda-meeting-type');
    btns.addClass('btn btn-success  btn-add-agenda-meeting-type').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-agenda-meeting-type');
    btns.addClass('btn btn-primary btn-Edit-agenda-meeting-type disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-Delete-agenda-meeting-type');
    btns.addClass('btn btn-danger btn-Delete-agenda-meeting-type DeleteAll disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $("#selectAll").on('click', function () {
        debugger;
        if ($(this).prop('checked')) {
            var arr = new Array();
            $('#agenda-meeting-type-table tbody input[type="checkbox"]').each(function () {
                debugger
                $(this).prop('checked', true);
                debugger
                var row = $(this).closest("tr");
                selectedRow = agendameetingtypeTable.row(row).index();
                var tdrow = (agendameetingtypeTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-Delete-agenda-meeting-type').data('rowindex', arr);
                $('.btn-add-agenda-meeting-type').addClass('disabled');
                $('.btn-Edit-agenda-meeting-type').addClass('disabled');
                $('.btn-Delete-agenda-meeting-type').removeClass('disabled');

            });
        } else {
            $('#agenda-meeting-type-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-agenda-meeting-type').removeClass('disabled');
                $('.btn-Delete-agenda-meeting-type').addClass('disabled');
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
        $('.btn-add-agenda-meeting-type').attr('disabled', true);
    }

    $('#agenda-meeting-type-table  tbody').on('click', 'input[type="checkbox"]', function () {
        debugger;
        var all = $('tbody input[type="checkbox"]');
        // getting only the checked checkboxes from that collection:
        checked = all.filter(':checked');
        if (name === "Closed") {
            debugger;
            if (all.length > 0 == checked.length) {
                debugger;
                $('.btn-Delete-agenda-meeting-type').addClass('disabled');

                $('.btn-Edit-agenda-meeting-type').removeClass('disabled');
            }
            else {
                $('.btn-Edit-agenda-meeting-type').addClass('disabled');

            }
            if (checked.length == 0) {
                $('.btn-Delete-agenda-meeting-type').addClass('disabled');
                $('.btn-add-agenda-meeting-type').removeClass('disabled');
            }
        }
        else {

        }
        if (all.length > 0 == checked.length) {

            $('.btn-add-agenda-meeting-type').addClass('disabled');
            $('.btn-Edit-agenda-meeting-type').removeClass('disabled');
            $('.btn-Delete-agenda-meeting-type').removeClass('disabled');
        }
        else {
            $('.btn-add-agenda-meeting-type').addClass('disabled');
            $('.btn-Edit-agenda-meeting-type').addClass('disabled');
            $('.btn-Delete-agenda-meeting-type').removeClass('disabled');
        }
        if (all.length === checked.length || !this.checked) {
            $('#selectAll').prop('checked', this.checked);
        }

        if (checked.length === 0) {
            $('.btn-add-agenda-meeting-type').removeClass('disabled');
            $('.btn-Delete-agenda-meeting-type').addClass('disabled');
            $('#selectAll').prop('checked', this.checked);
        }

    });

    $('#agenda-meeting-type-table tbody').on('click', "input[type=checkbox]", function () {
        $('#agenda-meeting-type-table input[type="checkbox"]:checked').each(function () {


            var isChecked = $(this).prop("checked");

            if (isChecked) {
                var arr = new Array();
                $("input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    selectedRow = agendameetingtypeTable.row(row).index();
                    var tdrow = (agendameetingtypeTable.row(selectedRow).data());

                    arr.push({
                        td0: tdrow[1]
                    });

                    $('.btn-Delete-agenda-meeting-type').removeClass('disabled');

                    $('#btn-update-agenda-meeting-type').attr('rowindex', selectedRow);
                    $('.btn-Edit-agenda-meeting-type').data('rowindex', tdrow);
                    $('.btn-Delete-agenda-meeting-type').data('rowindex', arr);
                })
            }
        });
    });
    
    //To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    function ClearAgendaMeetingTypeValues() {
        $("#meeting-type").val('');
        $("#activation-date").val('');
        $("#expiry-date").val('');
    }

    // Clear Div Errors 
    function ClearAgendaMeetingTypeDivErrors() {
        $('#meeting-type').next("div.error").remove();
        $('#activation-date').next("div.error").remove();
        $('#expiry-date').next("div.error").remove();
    }

    //Code to save changes
    $('#btnsave').on('click', function (e) {
        debugger;

        // Return List Object, Hence Create Array
        var _AgendaMeetingType = new Array();

        $("#agenda-meeting-type-table > TBODY > TR").each(function () {

            debugger;
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (agendameetingtypeTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                return false;
            }

            if (!($(this).find("input[type='checkbox']").hasClass("disabled"))) {
                var td0 = columnvalue[1];
                var td1 = columnvalue[3];
                var td2 = columnvalue[4];

                _AgendaMeetingType.push({
                    "MeetingTypeId": td0,
                    "ActivationDate": td1,
                    "ExpiryDate": td2

                });
            }
        });

        // Call Cantroller Save Method 
        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            data: { '_AgendaMeetingType': _AgendaMeetingType },
            success: function (data) {
            }
        })

    });
});
