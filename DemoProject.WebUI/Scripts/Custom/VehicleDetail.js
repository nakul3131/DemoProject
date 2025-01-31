$(document).ready(function () {
    debugger
    debugger
    var model = '<option value="">-- Select Vehicle Model --</option>';
    var Variants = $("#vehicle-model");
    Variants.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    debugger
    $.ajax({
        type: "post",
        url: urlGetModel,
        data: { VehicleMakeId: $('#vehicle-make').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            debugger
            $('#vehicle-model').empty();
            for (var i = 0; i < data.length; i++) {
                model += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#vehicle-model').append(model);
        }
    });
    
    ClearVehicleDetailValues();

    // Initialising & Configuring DataTables
    var vehicledetailTable = $('#vehicle-detail-table').DataTable({

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
            { orderable: false, targets: [7] }
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
                    id: 'btn-addnew-vehicle-detail'
                },
                action: function (e, dt, node, config, type) {
                    debugger
                    event.preventDefault();
                    debugger;
                    $("#btn-update-vehicle-detail").hide();
                    $("#btn-add-vehicle-detail").show();
                    $('#title').html($('#title').html().replace('Edit', 'Add'));
                    ClearVehicleDetailValues();
                    ClearVehicleDetailDivErrors();

                    $('#Add-new-vehicle-detail').modal('show');

                    var rowNum = 0;

                    $('#btn-add-vehicle-detail').on('click', function (event) {

                        debugger
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var VehicleModelId = $("#vehicle-model option:selected").val();
                        var VehicleModelText = $("#vehicle-model option:selected").text();
                        var VehicleBodyTypeId = $("#vehicle-bodyType option:selected").val();
                        var VehicleBodyTypeText = $("#vehicle-bodyType option:selected").text();
                        var ActivationDate = $("#ActivationDate").val().trim();

                        if ((VehicleModelText == "") || (VehicleBodyTypeText == "") || (ActivationDate == "")) {
                            ClearVehicleDetailDivErrors();

                            if (VehicleModelText == "")
                                $('#vehicle-model').after('<div class="error" style="color:red">Please Select Vehicle Model </div>');

                            if (VehicleBodyTypeText == "")
                                $('#vehicle-bodyType').after('<div class="error" style="color:red">Please Select Vehicle BodyType </div>');

                            if (ActivationDate == "")
                                $('#ActivationDate').after('<div class="error" style="color:red">Please Enter ActivationDate</div>');
                            return false;
                        }
                        else {
                            var row = vehicledetailTable.row.add([
                                tag,
                                VehicleModelId,
                                VehicleModelText,
                                VehicleBodyTypeId,
                                VehicleBodyTypeText,
                                ActivationDate,

                            ]).draw();

                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            vehicledetailTable.column(1).visible(false);
                            vehicledetailTable.column(3).visible(false);
                            vehicledetailTable.columns.adjust().draw();
                            ClearVehicleDetailValues();
                            ClearVehicleDetailDivErrors();

                            $('#vehicle-model').next("div.Error").remove();
                            $('#vehicle-bodyType').next("div.Error").remove();
                            $('#ActivationDate').next("div.Error").remove();

                            $('#Add-new-vehicle-detail').modal('hide');

                        }
                    });
                    return false;
                },
            },
            {

                text: 'Edit',
                attr: {
                    id: 'btn-edit-vehicle-detail'
                },
                //className: 'btn btn-Edit disabled',
                action: function (e, dt, node, config, String, indexes) {
                    debugger

                    $("#btn-update-vehicle-detail").show();
                    $("#btn-add-vehicle-detail").hide();
                    $('#title').html($('#title').html().replace('Add', 'Edit'));
                    ClearVehicleDetailDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit-vehicle-detail').data('rowindex');
                        var id = $("#Add-new-vehicle-detail").attr("id");
                        var myModal = $('#' + id).modal();
                       
                        $('#vehicle-model', myModal).val(columnValues[1]);
                        $('#vehicle-bodyType', myModal).val(columnValues[3]);
                        $('#ActivationDate', myModal).val(columnValues[4]);
                        myModal.modal({ show: true });
                    }

                    else {
                        $('.btn-Edit-vehicle-detail').addClass('disabled');
                        $("#model-Edit-vehicle-detail").modal("hide");
                    }


                    $(document).on('click', "#btn-update-vehicle-detail", function (event) {
                        debugger;
                        $('#selectAll').prop('checked', false);
                        colorchange();

                        debugger
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var VehicleModelId = $("#vehicle-model option:selected").val();
                        var VehicleModelText = $("#vehicle-model option:selected").text();
                        var VehicleBodyTypeId = $("#vehicle-bodyType option:selected").val();
                        var VehicleBodyTypeText = $("#vehicle-bodyType option:selected").text();
                        var ActivationDate = $("#ActivationDate").val().trim();

                        if ((VehicleModelText == "") || (VehicleBodyTypeText == "") || (ActivationDate == "")) {
                            ClearVehicleDetailDivErrors();

                            if (VehicleModelText == "")
                                $('#vehicle-model').after('<div class="error" style="color:red">Please Select Vehicle Model </div>');

                            if (VehicleBodyTypeText == "")
                                $('#vehicle-bodyType').after('<div class="error" style="color:red">Please Select Vehicle BodyType </div>');

                            if (ActivationDate == "")
                                $('#ActivationDate').after('<div class="error" style="color:red">Please Enter ActivationDate</div>');
                            return false;
                        }
                        else {
                            vehicledetailTable.row($(this).attr('rowindex')).data([
                                tag,
                                VehicleModelId,
                                VehicleModelText,
                                VehicleBodyTypeId,
                                VehicleBodyTypeText,
                                ActivationDate,

                            ]).draw();

                            vehicledetailTable.column(1).visible(false);

                            vehicledetailTable.columns.adjust().draw();
                            $('#Add-new-vehicle-detail').modal('hide');
                            $('.btn-add-vehicle-detail').removeClass('disabled');
                            $('.btn-Delete-vehicle-detail').addClass('disabled');
                            $('.btn-Edit-vehicle-detail').addClass('disabled');
                        }
                    })
                },
            },

            {
                text: 'Delete',
                attr: {
                    id: 'btn-Delete-vehicle-detail'
                },

                action: function (e, dt, node, config) {
                    debugger;
                    var isChecked = $("input[type='checkbox']").is(":checked");
                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {
                            debugger;
                            if ($("#vehicle-detail-table input[type='checkbox']:checked").each(function () {
                                debugger;
                                vehicledetailTable.row($("input[type='checkbox']:checked").parents('tr')).remove().draw();
                                $('.btn-add-vehicle-detail').removeClass('disabled');
                                $('.btn-Delete-vehicle-detail').addClass('disabled');
                                $('.btn-Edit-vehicle-detail').addClass('disabled');
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
        ClearVehicleDetailValues();
        ClearVehicleDetailDivErrors();
        $('.btn-Delete-vehicle-detail').addClass('disabled');
        $('.btn-Edit-vehicle-detail').addClass('disabled');
        $('.checks').prop('checked', false);
        $('.btn-add-vehicle-detail').removeClass('disabled');
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

    var btns = $('#btn-addnew-vehicle-detail');
    btns.addClass('btn btn-success  btn-add-vehicle-detail').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-vehicle-detail');
    btns.addClass('btn btn-primary btn-Edit-vehicle-detail disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-Delete-vehicle-detail');
    btns.addClass('btn btn-danger btn-Delete-vehicle-detail DeleteAll disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $("#selectAll").on('click', function () {
        debugger;
        if ($(this).prop('checked')) {
            var arr = new Array();
            $('#vehicle-detail-table tbody input[type="checkbox"]').each(function () {
                debugger
                $(this).prop('checked', true);
                debugger
                var row = $(this).closest("tr");
                selectedRow = vehicledetailTable.row(row).index();
                var tdrow = (vehicledetailTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-Delete-vehicle-detail').data('rowindex', arr);
                $('.btn-add-vehicle-detail').addClass('disabled');
                $('.btn-Edit-vehicle-detail').addClass('disabled');
                $('.btn-Delete-vehicle-detail').removeClass('disabled');

            });
        } else {
            $('#vehicle-detail-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-vehicle-detail').removeClass('disabled');
                $('.btn-Delete-vehicle-detail').addClass('disabled');
            });
        }
    });

    $('#vehicle-detail-table  tbody').on('click', 'input[type="checkbox"]', function () {
        debugger;
        var all = $('tbody input[type="checkbox"]');
        // getting only the checked checkboxes from that collection:
        checked = all.filter(':checked');

        if (all.length > 0 == checked.length) {

            $('.btn-add-vehicle-detail').addClass('disabled');
            $('.btn-Edit-vehicle-detail').removeClass('disabled');
            $('.btn-Delete-vehicle-detail').removeClass('disabled');
        }
        else {
            $('.btn-add-vehicle-detail').addClass('disabled');
            $('.btn-Edit-vehicle-detail').addClass('disabled');
            $('.btn-Delete-vehicle-detail').removeClass('disabled');
        }
        if (all.length === checked.length || !this.checked) {
            $('#selectAll').prop('checked', this.checked);
        }

        if (checked.length === 0) {
            $('.btn-add-vehicle-detail').removeClass('disabled');
            $('.btn-Delete-vehicle-detail').addClass('disabled');
            $('#selectAll').prop('checked', this.checked);
        }

    });

    $('#vehicle-detail-table tbody').on('click', "input[type=checkbox]", function () {
        $('#vehicle-detail-table input[type="checkbox"]:checked').each(function () {


            var isChecked = $(this).prop("checked");

            if (isChecked) {
                var arr = new Array();
                $("input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    selectedRow = vehicledetailTable.row(row).index();
                    var tdrow = (vehicledetailTable.row(selectedRow).data());

                    arr.push({
                        td0: tdrow[1]
                    });

                    $('#btn-update-vehicle-detail').attr('rowindex', selectedRow);
                    $('.btn-Edit-vehicle-detail').data('rowindex', tdrow);
                    $('.btn-Delete-vehicle-detail').data('rowindex', arr);
                })
            }
        });
    });


    //To page load table each row get value & dropdown value Hide 
    $('#vehicle-detail-table > tbody > tr').each(function () {
        debugger;

        var currentRow = $(this).closest("tr");
        var columnvalue = (vehicledetailTable.row(currentRow).data());
        if (typeof columnvalue != 'undefined' && columnvalue != null) {

            $('#vehicle-model').find("option[value='" + columnvalue[1] + "']").hide();

        }
        else {
            return true;
        }

        //To get Already Assign Vehicle make Record For Only Create Page
        var name = window.location.pathname
            .split("/")
            .filter(function (c) { return c.length; })
            .pop();
        if (name == "Create") {
            $(this).find("input[type='checkbox']").addClass('disabled').attr('disabled', 'disabled');
        }

    });

    //To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    function ClearVehicleDetailValues() {
        debugger

        $("#vehicle-model").prop("selectedIndex", 0);
        
        $("#vehicle-model").val('');
        $("#vehicle-bodyType").val('');
        $("#ActivationDate").val('');
      
    }

    // Clear Div Errors 
    function ClearVehicleDetailDivErrors() {
        debugger
        $('#vehicle-model').next("div.error").remove();
        $('#vehicle-bodyType').next("div.error").remove();
        $('#ActivationDate').next("div.error").remove();
    }

    //Code to save changes
    $('#btnsave').on('click', function (e) {
        debugger;

        // Return List Object, Hence Create Array
        var _VehicleDetail = new Array();

        $("#vehicle-detail-table > TBODY > TR").each(function () {

            debugger;
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (vehicledetailTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                debugger;
                return false;
            }

            if (!($(this).find("input[type='checkbox']").hasClass("disabled"))) {
                var td0 = columnvalue[1];
                var td1 = columnvalue[3];
                var td2 = columnvalue[5];

                _VehicleDetail.push({
                    "VehicleModelId": td0,
                    "VehicleBodyTypeId": td1,
                    "ActivationDate": td2
                });
            }
        });

        // Call Cantroller Save Method 
        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            data: { '_VehicleDetail': _VehicleDetail },
            success: function (data) {
            }
        })

    });
});
