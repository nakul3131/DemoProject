
$(document).ready(function () {
    ClearCenterTradingEntityDetailValues()
    debugger
    // Initialising & Configuring DataTables 
    var centerTradingEntityDetailTable = $('#center-trading-entity-detail-table').DataTable({

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
            { orderable: false, targets: 3 }
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
        dom: '<"float-left"B><"float-right"f>rt<"row"<"col-3"l><"col-5"i><"col-4"p>>',

        buttons: [
            {
                // Add new record in datatable
                text: 'New',
                attr: {
                    id: 'btn-addNew-trading-entity-detail'
                },
                action: function (e, dt, node, config, type) {
                    debugger
                    event.preventDefault();
                    var id = $("#trading-entity-detail-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Edit', 'Add'));
                    $("#btn-update-trading-entity").hide();
                    $("#btn-add-trading-entity").show();
                    ClearCenterTradingEntityDetailValues();
                    ClearCenterTradingEntityDetailDivErrors();

                    $('#add-center-trading-entity-detail').modal('show');

                    var rowNum = 0;

                    $('#btn-add-trading-entity').on('click', function (event) {

                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var tradingEntityId = $("#name-of-trading-entity option:selected").val();
                        var tradingEntityIdText = $("#name-of-trading-entity option:selected").text();
                        var volume = $("#volume-text").val().trim();

                        if (tradingEntityIdText == "" || (volume < 1)) {
                            ClearCenterTradingEntityDetailDivErrors();

                            if (tradingEntityIdText == "")
                                $('#name-of-trading-entity').after('<div class="error" style="color:red">Please Select Name Of Trading Entity</div>');

                            if (volume.trim().length < 1)
                                $('#volume-text').after('<div class="error" style="color:red">Please Enter Volume</div>');

                            return false;
                        }
                        else {
                            var row = centerTradingEntityDetailTable.row.add([
                                tag,
                                tradingEntityId,
                                tradingEntityIdText,
                                volume,
                            ]).draw();

                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            centerTradingEntityDetailTable.column(1).visible(false);
                            centerTradingEntityDetailTable.columns.adjust().draw();
                            ClearCenterTradingEntityDetailValues();
                            ClearCenterTradingEntityDetailDivErrors();

                            $('#add-center-trading-entity-detail').modal('hide');
                        }
                    });
                    return false;
                },
            },
            {
                // update existing record in datatable
                text: 'Edit',
                attr: {
                    id: 'btn-edit-trading-entity'
                },
                // className: 'btn btn-Edit disabled',
                action: function (e, dt, node, config, String, indexes) {
                    debugger;
                    var arr = new Array();
                    $("#center-trading-entity-detail-table  input[type='checkbox']").each(function (index) {
                        debugger
                        var row = $(this).closest("tr");
                        selectedRow = centerTradingEntityDetailTable.row(row).index();
                        var tdrow = (centerTradingEntityDetailTable.row(selectedRow).data());
                        arr.push({
                            td0: tdrow[1]
                        });
                    });

                    var id = $("#trading-entity-detail-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Add', 'Edit'));
                    $("#btn-add-trading-entity").hide();
                    $("#btn-update-trading-entity").show();
                    ClearCenterTradingEntityDetailDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit').data('rowindex');
                        var id = $("#add-center-trading-entity-detail").attr("id");
                        var myModal = $('#' + id).modal();

                        $('#name-of-trading-entity', myModal).val(columnValues[1]);
                        $('#volume-text', myModal).val(columnValues[3]);
                        myModal.modal({ show: true });
                    }
                    else {
                        $('.btn-Edit').addClass('disabled');
                        $("#add-center-trading-entity-detail").modal("hide");
                    }

                    arr.map(function (obj) {
                        debugger;
                        $('#name-of-trading-entity').find("option[value='" + obj.td0 + "']").hide();
                    });

                    $('#btn-update-trading-entity').data('rowindex', columnValues);

                    $(document).on('click', "#btn-update-trading-entity", function (event) {
                        debugger;
                        $('#selectAll').prop('checked', false);
                        colorchange();

                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var tradingEntityId = $("#name-of-trading-entity option:selected").val();
                        var tradingEntityIdText = $("#name-of-trading-entity option:selected").text();
                        var volume = $("#volume-text").val().trim();

                        if (tradingEntityIdText == "" || (volume < 1)) {
                            ClearCenterTradingEntityDetailDivErrors();

                            if (tradingEntityIdText == "")
                                $('#name-of-trading-entity').after('<div class="error" style="color:red">Please Select Name Of Trading Entity</div>');

                            if (volume.trim().length < 1)
                                $('#volume-text').after('<div class="error" style="color:red">Please Enter Volume</div>');

                            return false;
                        }
                        else {
                            centerTradingEntityDetailTable.row($(this).attr('rowindex')).data([

                                '<input type="checkbox" name="check_all" class="checks"/>',
                                $('#name-of-trading-entity option:selected').val(),
                                $('#name-of-trading-entity option:selected').text(),
                                $('#volume-text').val(),

                            ]).draw();

                            var columnValues = $('#btn-update-trading-entity').data('rowindex');
                            FromShowTradingEntityValues(columnValues);

                            centerTradingEntityDetailTable.column(1).visible(false);
                            centerTradingEntityDetailTable.columns.adjust().draw();
                            $("#add-center-trading-entity-detail").modal('hide');
                            $('.btn-add-trading-entity').removeClass('disabled');
                            $('.btn-Delete').addClass('disabled');
                            $('.btn-Edit').addClass('disabled');
                        }
                    })
                },
            },

            {
                // delete existing record in datatable
                text: 'Delete',
                attr: {
                    id: 'btn-delete-trading-entity'
                },

                action: function (e, dt, node, config) {
                    debugger;
                    var isChecked = $("input[type='checkbox']").is(":checked");
                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {
                            debugger;
                            if ($("input[type='checkbox']:checked").each(function () {
                                debugger;
                                centerTradingEntityDetailTable.row($("input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                                var arr = $('.btn-Delete').data('rowindex');
                                arr.map(function (obj) {
                                    debugger;
                                    $('#name-of-trading-entity').find("option[value='" + obj.td0 + "']").show();
                                    $("#name-of-trading-entity").prop("selectedIndex", 0);
                                });
                                $('.btn-add-trading-entity').removeClass('disabled');
                                $('.btn-Delete').addClass('disabled');
                                $('.btn-Edit').addClass('disabled');
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
        $('.btn-Delete').addClass('disabled');
        $('.btn-Edit').addClass('disabled');
        $('.checks').prop('checked', false);
        $('#selectAll').prop('checked', false);
        debugger;
        var tradingEntities = $("#name-of-trading-entity option:selected").val();
        $('#name-of-trading-entity').find("option[value='" + tradingEntities + "']").show();
        $("#name-of-trading-entity").prop("selectedIndex", 0);
    });

    // To dropdown added values by edit
    function FromShowTradingEntityValues(columnValues) {
        debugger;
        $('#name-of-trading-entity').find("option[value='" + columnValues[1] + "']").show();
        return false;
    }

    // Color of buttons changes according to opeation
    function colorchange() {
        debugger;
        var myid = $("input[type='checkbox']:checked").closest('tr').attr("id");

        $("#" + myid).animate({
            backgroundColor: "#B0C4DE"
        }, 500).animate({
            backgroundColor: "#F5F5F5"
        }, 500);
    }

    var btns = $('#btn-addNew-trading-entity-detail');
    btns.addClass('btn btn-success  btn-add-trading-entity').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-trading-entity');
    btns.addClass('btn btn-primary btn-Edit disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-delete-trading-entity');
    btns.addClass('btn btn-danger btn-Delete DeleteAll disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $('#selectAll').on('click', function () {
        debugger;
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);
            var arr = new Array();
            $('#center-trading-entity-detail-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                var row = $(this).closest("tr");
                selectedRow = centerTradingEntityDetailTable.row(row).index();
                var tdrow = (centerTradingEntityDetailTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-Delete').data('rowindex', arr);
                $('.btn-add-trading-entity').addClass('disabled');
                $('.btn-Edit').addClass('disabled');
                $('.btn-Delete').removeClass('disabled');
            });
        }
        else {
            $('#center-trading-entity-detail-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-trading-entity').removeClass('disabled');
                $('.btn-Delete').addClass('disabled');
            });
        }
    });

    // binding the change event-handler to the tbody:
    $('#center-trading-entity-detail-table tbody').on('change', function () {
        debugger;
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
            $('.btn-add-trading-entity').removeClass('disabled');
        }

        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#selectAll').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    $('#center-trading-entity-detail-table tbody').on('click', "input[type=checkbox]", function () {
        $('#center-trading-entity-detail-table input[type="checkbox"]:checked').each(function () {
            var isChecked = $(this).prop("checked");

            if (isChecked) {
                var arr = new Array();
                $("input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    selectedRow = centerTradingEntityDetailTable.row(row).index();
                    var tdrow = (centerTradingEntityDetailTable.row(selectedRow).data());
                    arr.push({
                        td0: tdrow[1]
                    });

                    $('.btn-add-trading-entity').addClass('disabled');
                    $('.btn-Edit').removeClass('disabled');
                    $('.btn-Delete').removeClass('disabled');

                    $('#btn-update-trading-entity').attr('rowindex', selectedRow);
                    $('.btn-Edit').data('rowindex', tdrow);
                    $('.btn-Delete').data('rowindex', arr);
                    $('#selectAll').data('rowindex', arr);
                });
            }
        });
    });

    // To page load table each row get value & dropdown value Hide 
    $('#center-trading-entity-detail-table > tbody > tr').each(function () {
        debugger;
        var currentRow = $(this).closest("tr");
        var columnvalue = (centerTradingEntityDetailTable.row(currentRow).data());
        if (typeof columnvalue != 'undefined' && columnvalue != null) {
            $('#name-of-trading-entity').find("option[value='" + columnvalue[1] + "']").hide();
        }
        else {
            return true;
        }
    });

    // To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    function ClearCenterTradingEntityDetailValues() {
        debugger;
        var tradingEntities = $("#name-of-trading-entity option:selected").val();
        $('#name-of-trading-entity').find("option[value='" + tradingEntities + "']").hide();
        $("#name-of-trading-entity").prop("selectedIndex", 0);
        $("#name-of-trading-entity").val('');
        $("#volume-text").val('0');
    }

    // Clear DataTable Inputs
    function ClearCenterTradingEntityDetailDivErrors() {
        debugger;
        $('#name-of-trading-entity').next("div.error").remove();
        $('#volume-text').next("div.error").remove();
    }

    //Code to save changes
    $('#btnsave').on('click', function () {
        debugger;
        var CenterTradingEntityDetail = new Array();

        $("#center-trading-entity-detail-table TBODY TR").each(function () {
            debugger;
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (centerTradingEntityDetailTable.row(currentRow).data());

            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                debugger;
                return false;
            }

            var tradingEntityDetailId = columnvalue[1];
            var volumes = columnvalue[3];

            CenterTradingEntityDetail.push({
                "TradingEntityId": tradingEntityDetailId,
                "Volume": volumes,
            });
        });

        // Call Controller Save Method 
        $.ajax({
            url: url,
            type: 'POST',
            data: { '_centerTradingEntityDetail': CenterTradingEntityDetail },
            ContentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (data) {
            },

            error: function (xhr, status, error) {
                alert("An Error Has Occured In CenterTradingEntityDetails DataTable!!! Error Message - " + error.toString());
            }
        })
    });

});

