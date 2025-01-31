$(document).ready(function () {
    debugger

    ClearNoticeTypeValues()

    // Initialising & Configuring DataTables 
    var noticeTypeTable = $('#person-information-parameter-notice-type').DataTable({

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
            "targets": [1, 3],
            "visible": false
        }],

        columnDefs: [
            { orderable: false, targets: 6 }
        ],
        columnDefs: [{
            orderable: false,

            'checkboxes': {
                'selectRow': true
            },
            targets: 0

        }],
        select: {
            style: 'multi'
        },

        "order": [2, 'desc'],
        dom: 'Bfrtip',
        dom: '<"float-left"B><"float-right"f>rt<"row"<"col-sm-2"l><"col-sm-6"i><"col-sm-4"p>>',

        buttons: [

            {
                text: 'New',
                attr: {
                    id: 'btn-addNew-notice-type'
                },
                action: function (e, dt, node, config, type) {
                    debugger
                    event.preventDefault();

                    var id = $("#notice-type-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Edit', 'Add'));
                    $("#btn-update-notice-type").hide();
                    $("#btn-add-notice-type").show();
                    ClearNoticeTypeValues();
                    ClearNoticeTypeDivErrors();

                    $('#add-notice-type').modal('show');

                    var rowNum = 0;

                    $('#btn-add-notice-type').on('click', function (event) {
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var noticeTypeId = $("#notice-type-id option:selected").val();
                        var noticeTypeIdText = $("#notice-type-id option:selected").text();
                        var enableNoticeInRegionalLanguage = $('input[name="EnableNoticeInRegionalLanguage"]').is(':checked') ? "True" : "False";
                        var maximumResendsOnFailure = $("#maximum-resends-on-failure").val();
                        var activationDate = $("#activation-date").val();
                        var note = $("#note").val();

                        if ((noticeTypeId.trim().length < 36) || (activationDate == "") || (maximumResendsOnFailure == "")) {

                            ClearNoticeTypeDivErrors();

                            if (noticeTypeId.trim().length < 36)
                                $('#notice-type-id').after('<div class="error" style="color:red">Please Select Notice Type</div>');

                            if (maximumResendsOnFailure == "")
                                $('#maximum-resends-on-failure').after('<div class="error" style="color:red">Please Enter Maximum Resends On Failure</div>');

                            if (activationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            var row = noticeTypeTable.row.add([
                                tag,
                                noticeTypeId,
                                noticeTypeIdText,
                                enableNoticeInRegionalLanguage,
                                maximumResendsOnFailure,
                                activationDate,
                                note,

                            ]).draw();

                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            noticeTypeTable.column(1).visible(false);
                            noticeTypeTable.columns.adjust().draw();
                            ClearNoticeTypeValues();
                            ClearNoticeTypeDivErrors();
                            $('#add-notice-type').modal('hide');
                        }
                    });
                    return false;
                },
            },
            {
                text: 'Edit',
                attr: {
                    id: 'btn-edit-notice-type'
                },

                // className: 'btn btn-Edit disabled',
                action: function (e, dt, node, config, String, indexes) {
                    debugger

                    var arr = new Array();
                    $("#person-information-parameter-notice-type  input[type='checkbox']").each(function (index) {
                        debugger
                        var row = $(this).closest("tr");
                        selectedRow = noticeTypeTable.row(row).index();
                        var tdrow = (noticeTypeTable.row(selectedRow).data());
                        arr.push({
                            td0: tdrow[1]
                        });
                    });

                    var id = $("#notice-type-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Add', 'Edit'));
                    $("#btn-add-notice-type").hide();
                    $("#btn-update-notice-type").show();
                    ClearNoticeTypeDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit').data('rowindex');
                        var id = $("#add-notice-type").attr("id");
                        var myModal = $('#' + id).modal();

                        var d = new Date(columnValues[5]),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var newDate = [year, month, day].join('-');

                        $('#notice-type-id', myModal).val(columnValues[1]);
                        if (columnValues[3] === "True") {
                            $("#enable-notice-in-regional-language").prop("checked", true);
                        }
                        else {
                            $("#enable-notice-in-regional-language").prop("checked", false);
                        }

                        $('#maximum-resends-on-failure', myModal).val(columnValues[4]);
                        $('#activation-date', myModal).val(newDate);
                        $('#note', myModal).val(columnValues[6]);

                        myModal.modal({ show: true });
                    }

                    else {
                        $('.btn-Edit').addClass('disabled');
                        $("#add-notice-type").modal("hide");
                    }

                    arr.map(function (obj) {
                        debugger;
                        $('#notice-type-id').find("option[value='" + obj.td0 + "']").hide();
                    });
                    $('#btn-update-notice-type').data('rowindex', columnValues);

                    $(document).on('click', "#btn-update-notice-type", function (event) {
                        debugger;
                        $('#select-all-notice-type').prop('checked', false);
                        colorchange();
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var noticeTypeId = $('#notice-type-id option:selected').val();
                        var noticeTypeIdText = $('#notice-type-id option:selected').text();
                        var enableNoticeInRegionalLanguage = $('input[name="EnableNoticeInRegionalLanguage"]').is(':checked') ? "True" : "False";
                        var maximumResendsOnFailure = $("#maximum-resends-on-failure").val();
                        var activationDate = $("#activation-date").val();
                        var note = $("#note").val();

                        if ((noticeTypeId.trim().length < 36) || (activationDate == "") || (maximumResendsOnFailure == "")) {

                            ClearNoticeTypeDivErrors();

                            if (noticeTypeId.trim().length < 36)
                                $('#notice-type-id').after('<div class="error" style="color:red">Please Select Notice Type</div>');

                            if (maximumResendsOnFailure == "")
                                $('#maximum-resends-on-failure').after('<div class="error" style="color:red">Please Enter Maximum Resends On Failure</div>');

                            if (activationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            noticeTypeTable.row($(this).attr('rowindex')).data([
                                tag,
                                noticeTypeId,
                                noticeTypeIdText,
                                enableNoticeInRegionalLanguage,
                                maximumResendsOnFailure,
                                activationDate,
                                note,

                            ]).draw();

                            noticeTypeTable.column(1).visible(false);
                            noticeTypeTable.columns.adjust().draw();

                            var columnValues = $('#btn-update-notice-type').data('rowindex');
                            FromShowNoticeValues(columnValues);

                            $('#add-notice-type').modal('hide');
                            $('.btn-Add').removeClass('disabled');
                            $('.btn-Delete').addClass('disabled');
                            $('.btn-Edit').addClass('disabled');
                        }
                    })
                },
            },

            {
                text: 'Delete',
                attr: {
                    id: 'btn-Delete-notice-type'
                },

                action: function (e, dt, node, config) {
                    debugger;
                    var isChecked = $("input[type='checkbox']").is(":checked");
                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {
                            debugger;
                            if ($("#person-information-parameter-notice-type tbody input[type='checkbox']:checked").each(function () {
                                debugger;
                                noticeTypeTable.row($("input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                                var arr = $('.btn-Delete').data('rowindex');
                                arr.map(function (obj) {
                                    debugger;
                                    $('#notice-type-id').find("option[value='" + obj.td0 + "']").show();
                                    $("#notice-type-id").prop("selectedIndex", 0);
                                });
                                $('.btn-Add').removeClass('disabled');
                                $('.btn-Delete').addClass('disabled');
                                $('.btn-Edit').addClass('disabled');
                                $('#select-all-notice-type').prop('checked', false);
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
        debugger
        ClearNoticeTypeDivErrors();
        $('.btn-Delete').addClass('disabled');
        $('.btn-Edit').addClass('disabled');
        $('.checks').prop('checked', false);
        $('.btn-Add').removeClass('disabled');
        $('#select-all-notice-type').prop('checked', false);

        var noticeTypes = $("#notice-type-id option:selected").val();
        $('#notice-type-id').find("option[value='" + noticeTypes + "']").show();
        $("#notice-type-id").prop("selectedIndex", 0);
    });

    // To dropdown added values by edit
    function FromShowNoticeValues(columnValues) {
        debugger;
        $('#notice-type-id').find("option[value='" + columnValues[1] + "']").show();
        return false;
    }

    function colorchange() {
        debugger;
        var myid = $("input[type='checkbox']:checked").closest('tr').attr("id");

        $("#" + myid).animate({
            backgroundColor: "#B0C4DE"
        }, 500).animate({
            backgroundColor: "#F5F5F5"
        }, 500);
    }

    var btns = $('#btn-addNew-notice-type');
    btns.addClass('btn btn-success  btn-add-notice-type').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-notice-type');
    btns.addClass('btn btn-primary btn-Edit disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-Delete-notice-type');
    btns.addClass('btn btn-danger btn-Delete DeleteAll disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $('#select-all-notice-type').on('click', function () {
        debugger;
        debugger;
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);
            var arr = new Array();
            $('#person-information-parameter-notice-type tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                var row = $(this).closest("tr");
                selectedRow = noticeTypeTable.row(row).index();
                var tdrow = (noticeTypeTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-Delete').data('rowindex', arr);
                $('.btn-Add').addClass('disabled');
                $('.btn-Edit').addClass('disabled');
                $('.btn-Delete').removeClass('disabled');
            });
        }
        else {
            $('#person-information-parameter-notice-type tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-Add').removeClass('disabled');
                $('.btn-Delete').addClass('disabled');
            });
        }
    });

    // binding the change event-handler to the tbody:
    $('#person-information-parameter-notice-type tbody').on('change', function () {

        // getting all the checkboxes within the tbody:
        var all = $('tbody input[type="checkbox"]'),
            // getting only the checked checkboxes from that collection:
            checked = all.filter(':checked');

        if (all.length > 0 == checked.length) {
            debugger;
            $('.btn-Delete').removeClass('disabled');

            $('.btn-Edit').removeClass('disabled');
        }
        else {
            $('.btn-Edit').addClass('disabled');

        }
        if (checked.length == 0) {
            $('.btn-Delete').addClass('disabled');
            $('.btn-Add').removeClass('disabled');
        }
        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#select-all-notice-type').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    $('#person-information-parameter-notice-type tbody').on('click', "input[type=checkbox]", function () {
        $('#person-information-parameter-notice-type input[type="checkbox"]:checked').each(function () {

            var isChecked = $(this).prop("checked");

            if (isChecked) {

                var arr = new Array();
                $("#person-information-parameter-notice-type tbody input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    var selectedRow = noticeTypeTable.row(row).index();
                    var tdrow = noticeTypeTable.row(selectedRow).data();
                    arr.push({
                        td0: tdrow[1]
                    });

                    $('.btn-Add').addClass('disabled');
                    $('.btn-Edit').removeClass('disabled');
                    $('.btn-Delete').removeClass('disabled');

                    $('#btn-update-notice-type').attr('rowindex', selectedRow);
                    $('.btn-Edit').data('rowindex', tdrow);
                    $('.btn-Delete').data('rowindex', arr);
                    $('#select-all-notice-type').data('rowindex', arr);
                });
            }
        });
    });

    // To page load table each row get value & dropdown value Hide 
    $('#person-information-parameter-notice-type > tbody > tr').each(function () {
        debugger;

        var currentRow = $(this).closest("tr");
        var columnvalue = (noticeTypeTable.row(currentRow).data());
        if (typeof columnvalue != 'undefined' && columnvalue != null) {

            $('#notice-type-id').find("option[value='" + columnvalue[1] + "']").hide();
        }
        else {
            return true;
        }
    });

    // To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    function ClearNoticeTypeValues() {
        debugger
        var noticeTypes = $("#notice-type-id option:selected").val();
        $('#notice-type-id').find("option[value='" + noticeTypes + "']").hide();
        $("#notice-type-id").prop("selectedIndex", 0);

        $("#notice-type-id").val('');
        $('input[name="EnableNoticeInRegionalLanguage"]').prop('checked', false);
        $("#maximum-resends-on-failure").val('0');
        $("#activation-date").val('');
        $("#note").val('None');
    }

    // Clear Div Errors
    function ClearNoticeTypeDivErrors() {
        debugger
        $('#notice-type-id').next("div.error").remove();
        $('#maximum-resends-on-failure').next("div.error").remove();
        $('#activation-date').next("div.error").remove();
    }

    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger
        //not add event.preventDefault
        $(".lastrow").remove();

        // Return List Object, Hence Create Array
        var PersonInformationParameterNoticeType = new Array();

        // Recursive Loop By Row
        $("#person-information-parameter-notice-type TBODY TR").each(function () {
            debugger
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (noticeTypeTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                return false;
            }

            var NoticeTypeId = columnvalue[1];
            var EnableNoticeInRegionalLanguage = columnvalue[3];
            var MaximumResendsOnFailure = columnvalue[4];
            var ActivationDate = columnvalue[5];
            var Note = columnvalue[6];

            PersonInformationParameterNoticeType.push({
                "NoticeTypeId": NoticeTypeId,
                "EnableNoticeInRegionalLanguage": EnableNoticeInRegionalLanguage,
                "MaximumResendsOnFailure": MaximumResendsOnFailure,
                "ActivationDate": ActivationDate,
                "Note": Note,
            });
        });

        // Call Controller Save Method 
        $.ajax({
            url: url,
            type: 'POST',
            data: { '_personInformationParameterNoticeType': PersonInformationParameterNoticeType },
            ContentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (data) {
            },

            error: function (xhr, status, error) {
                alert("An Error Has Occured In Notice Details DataTable!!! Error Message - " + error.toString());
            }
        })
    });
});