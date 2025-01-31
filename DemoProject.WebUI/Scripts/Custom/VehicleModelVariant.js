$(document).ready(function () {
    debugger
        var model = '<option value="">-- Select Vehicle Variant --</option>';
        var Variants = $("#vehicle-variant");
        Variants.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
        debugger
        $.ajax({
            type: "post",
            url: variantDropdownListForCreateUrl,
            data: { VehicleModelId: $('#vehicle-model-id').val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                debugger
                $('#vehicle-variant-id').empty();
                for (var i = 0; i < data.length; i++) {
                    model += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
                }
                $('#vehicle-variant-id').append(model);
            }
        });

    ClearVehicleValues()

    // Initialising & Configuring DataTables 
    var vehicleTable = $('#vehicle-variant-table').DataTable({

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
                    id: 'btn-addNew-vehicle'
                },
                action: function (e, dt, node, config, type) {
                    debugger
                    event.preventDefault();

                    var id = $("#vehicle-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Edit', 'Add'));
                    $("#btn-update-vehicle-model-variant").hide();
                    $("#btn-add-vehicle-model-variant").show();
                    ClearVehicleDivErrors();
                    ClearVehicleValues();

                    $('#add-vehicle-model-variant').modal('show');

                    var rowNum = 0;

                    $('#btn-add-vehicle-model-variant').on('click', function (event) {
                        debugger
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var VehicleVariantId = $("#vehicle-variant-id option:selected").val();
                        var VehicleVariantIdText = $("#vehicle-variant-id option:selected").text();
                        var activationDate = $("#activation-date").val().trim();

                        if ((VehicleVariantId.trim().length < 36) || (activationDate == "")) {

                            ClearVehicleDivErrors();

                            if (VehicleVariantId.trim().length < 36)
                                $('#vehicle-variant-id').after('<div class="error" style="color:red">Please Select Vehicle Variant </div>');

                            if (activationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            var row = vehicleTable.row.add([
                                tag,
                                VehicleVariantId,
                                VehicleVariantIdText,
                                activationDate,

                            ]).draw();

                            //$('#select-all-vehicle').removeClass('disabled');
                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            vehicleTable.column(1).visible(false);
                            vehicleTable.columns.adjust().draw();
                            ClearVehicleValues();
                            ClearVehicleDivErrors();
                            $('#add-vehicle-model-variant').modal('hide');
                        }

                    });
                    return false;
                },
            },
            {

                text: 'Edit',
                attr: {
                    id: 'btn-edit-vehicle'
                },
                //className: 'btn btn-Edit disabled',
                action: function (e, dt, node, config, String, indexes) {
                    debugger

                    var arr = new Array();
                    $("#vehicle-variant-table  input[type='checkbox']").each(function (index) {
                        debugger
                        var row = $(this).closest("tr");
                        selectedRow = vehicleTable.row(row).index();
                        var tdrow = (vehicleTable.row(selectedRow).data());
                        arr.push({
                            td0: tdrow[1]
                        });
                    });

                    var id = $("#vehicle-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Add', 'Edit'));
                    $("#btn-add-vehicle-model-variant").hide();
                    $("#btn-update-vehicle-model-variant").show();
                    ClearVehicleDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit').data('rowindex');
                        var id = $("#add-vehicle-model-variant").attr("id");
                        var myModal = $('#' + id).modal();
                        var d = new Date(columnValues[3]),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var newDate = [year, month, day].join('-');

                        $('#vehicle-variant-id', myModal).val(columnValues[1]);
                        $('#activation-date', myModal).val(newDate);
                        myModal.modal({ show: true });
                    }

                    else {
                        $('.btn-Edit').addClass('disabled');
                        $("#add-vehicle-model-variant").modal("hide");
                    }

                    arr.map(function (obj) {
                        debugger;
                        $('#vehicle-variant-id').find("option[value='" + obj.td0 + "']").hide();
                    });
                    $('#btn-update-vehicle-model-variant').data('rowindex', columnValues);

                    $(document).on('click', "#btn-update-vehicle-model-variant", function (event) {
                        debugger;
                        $('#select-all-vehicle').prop('checked', false);
                        colorchange();
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var VehicleVariantId = $('#vehicle-variant-id option:selected').val();
                        var VehicleVariantIdText = $('#vehicle-variant-id option:selected').text();
                        var activationDate = $('#activation-date').val();

                        if ((VehicleVariantId == "")  || (activationDate == "")) {

                            ClearVehicleDivErrors();

                            if (VehicleVariantIdText == "")
                                $('#vehicle-variant-id').after('<div class="error" style="color:red">Please Select Contact Type </div>');

                            if (activationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            vehicleTable.row($(this).attr('rowindex')).data([
                                tag,
                                VehicleVariantId,
                                VehicleVariantIdText,
                                activationDate,
                            ]).draw();

                            vehicleTable.column(1).visible(false);
                            vehicleTable.columns.adjust().draw();

                            var columnValues = $('#btn-update-vehicle-model-variant').data('rowindex');
                            FromShowVehicleValues(columnValues);

                            $('#add-vehicle-model-variant').modal('hide');
                            $('.btn-add-vehicle-model-variant').removeClass('disabled');
                            $('.btn-Delete').addClass('disabled');
                            $('.btn-Edit').addClass('disabled');
                        }
                    })
                },
            },

            {
                text: 'Delete',
                attr: {
                    id: 'btn-Delete-vehicle'
                },

                action: function (e, dt, node, config) {
                    debugger;
                    var isChecked = $("input[type='checkbox']").is(":checked");
                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {
                            debugger;
                            if ($("input[type='checkbox']:checked").each(function () {
                                debugger;
                                vehicleTable.row($("input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                                var arr = $('.btn-Delete').data('rowindex');
                                arr.map(function (obj) {
                                    debugger;
                                    $('#vehicle-variant-id').find("option[value='" + obj.td0 + "']").show();
                                    $("#vehicle-variant-id").prop("selectedIndex", 0);
                                });
                                $('.btn-add-vehicle-model-variant').removeClass('disabled');
                                $('.btn-Delete').addClass('disabled');
                                $('.btn-Edit').addClass('disabled');
                                $('#select-all-vehicle').prop('checked', false);
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
        ClearVehicleDivErrors();
        ClearVehicleValues();
        $('.btn-Delete').addClass('disabled');
        $('.btn-Edit').addClass('disabled');
        $('.checks').prop('checked', false);
        $('.btn-add-vehicle-model-variant').removeClass('disabled');
        $('#select-all-vehicle').prop('checked', false);
    });

    //To dropdown added values by edit
    function FromShowVehicleValues(columnValues) {
        debugger;
        $('#vehicle-variant-id').find("option[value='" + columnValues[1] + "']").show();
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

    var btns = $('#btn-addNew-vehicle');
    btns.addClass('btn btn-success  btn-add-vehicle-model-variant').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-vehicle');
    btns.addClass('btn btn-primary btn-Edit disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-Delete-vehicle');
    btns.addClass('btn btn-danger btn-Delete DeleteAll disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $('#select-all-vehicle').on('click', function () {
        debugger;
        debugger;
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);
            var arr = new Array();
            $('#vehicle-variant-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                var row = $(this).closest("tr");
                selectedRow = vehicleTable.row(row).index();
                var tdrow = (vehicleTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-Delete').data('rowindex', arr);
                $('.btn-add-vehicle-model-variant').addClass('disabled');
                $('.btn-Edit').addClass('disabled');
                $('.btn-Delete').removeClass('disabled');
            });
        }
        else {
            $('#vehicle-variant-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-vehicle-model-variant').removeClass('disabled');
                $('.btn-Delete').addClass('disabled');
            });
        }
    });

    // binding the change event-handler to the tbody:
    $('#vehicle-variant-table tbody').on('change', function () {

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
            $('.btn-add-vehicle-model-variant').removeClass('disabled');
        }
        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#select-all-vehicle').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    $('#vehicle-variant-table tbody').on('click', "input[type=checkbox]", function () {
        $('#vehicle-variant-table input[type="checkbox"]:checked').each(function () {

            var isChecked = $(this).prop("checked");

            if (isChecked) {

                var arr = new Array();
                $("input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    selectedRow = vehicleTable.row(row).index();
                    var tdrow = (vehicleTable.row(selectedRow).data());
                    arr.push({
                        td0: tdrow[1]
                    });

                    //Variant Dropdown List For Edit
                    var model = '<option value="">--Select Variant Name---</option>';
                    var Variants = $("#vehicle-variant");
                    Variants.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
                    debugger
                    $.ajax({
                        type: "post",
                        url: variantDropdownListForEditUrl,
                        data: { VehicleModelId: $('#vehicle-model-id').val(), VehicleVariantId: tdrow[1] },
                        datatype: "json",
                        traditional: true,
                        success: function (data) {
                            debugger
                            $('#vehicle-variant-id').empty();
                            for (var i = 0; i < data.length; i++) {
                                model += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
                            }
                            $('#vehicle-variant-id').append(model);
                        }
                    });

                    $('.btn-add-vehicle-model-variant').addClass('disabled');
                    $('.btn-Edit').removeClass('disabled');
                    $('.btn-Delete').removeClass('disabled');


                    $('#btn-update-vehicle-model-variant').attr('rowindex', selectedRow);
                    $('.btn-Edit').data('rowindex', tdrow);
                    $('.btn-Delete').data('rowindex', arr);
                    $('#select-all-vehicle').data('rowindex', arr);
                });
            }
        });
    });

    //To page load table each row get value & dropdown value Hide 
    $('#vehicle-variant-table > tbody > tr').each(function () {
        debugger;
        var currentRow = $(this).closest("tr");
        var columnvalue = (vehicleTable.row(currentRow).data());

        if (typeof columnvalue != 'undefined' && columnvalue != null) {

            $('#vehicle-variant-id').find("option[value='" + columnvalue[1] + "']").hide();
        }
        else {
            return true;
        }

        //To get Already Assign Vehicle  Variant Record For Only Create Page 
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
    function ClearVehicleValues() {
        debugger
        var VehicleVariantId = $("#vehicle-variant-id option:selected").val();
        $('#vehicle-variant-id').find("option[value='" + VehicleVariantId + "']").hide();
        $("#vehicle-variant-id").prop("selectedIndex", 0);

        $("#vehicle-variant-id").val('');;
        $("#activation-date").val('');

    }

    //Clear Div Errors
    function ClearVehicleDivErrors() {
        debugger
        $('#vehicle-variant-id').next("div.error").remove();;
        $('#activation-date').next("div.error").remove();

    }

    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger
        //not add event.preventDefault
        $(".lastrow").remove();

        // Return List Object, Hence Create Array
        var VehicleModelVariant = new Array();

        // Recursive Loop By Row
        $("#vehicle-variant-table TBODY TR").each(function () {
            debugger
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (vehicleTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                return false;
            }

            if (!($(this).find("input[type='checkbox']").hasClass("disabled")))
            {
                var VehicleVariantId = columnvalue[1];
                var ActivationDate = columnvalue[3];

                VehicleModelVariant.push({
                    "VehicleVariantId": VehicleVariantId,
                    "ActivationDate": ActivationDate,
                });
            }
           
        });

        // Call Cantroller Save Method 
        $.ajax({
            url: SaveVehicleModelVariantDataTableUrl,
            type: 'POST',
            data: { '_VehicleModelVariant': VehicleModelVariant },
            ContentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (data) {
            },

            error: function (xhr, status, error) {
                alert("An Error Has Occured In Contact Details DataTable!!! Error Message - " + error.toString());
            }
        })
    });


});