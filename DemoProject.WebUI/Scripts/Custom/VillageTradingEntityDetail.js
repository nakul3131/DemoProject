// State Dropdown By Country
$("#country").on('change', function (event) {
    debugger
    var stateList = '<option value="">Select Name Of State</option>';
    var state = $("#state");
    state.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    debugger
    $.ajax({
        type: "post",
        url: stateDropdownListUrl,
        data: { _countryId: $('#country').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            debugger
            $('#state').empty();
            for (var i = 0; i < data.length; i++) {
                stateList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#state').append(stateList);
        }
    });
});

// Division Dropdown By State
$("#state").on('change', function (event) {
    debugger
    var divisionList = '<option value="">Select Name Of Division</option>';
    var division = $("#division");
    division.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    debugger
    $.ajax({
        type: "post",
        url: divisionDropdownListUrl,
        data: { _stateId: $('#state').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            debugger
            $('#division').empty();
            for (var i = 0; i < data.length; i++) {
                divisionList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#division').append(divisionList);
        }
    });
});

// District Dropdown By Division
$("#division").on('change', function (event) {
    debugger
    var districtList = '<option value="">Select Name Of District</option>';
    var district = $("#district");
    district.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    debugger
    $.ajax({
        type: "post",
        url: districtDropdownListUrl,
        data: { _divisionId: $('#division').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            debugger
            $('#district').empty();
            for (var i = 0; i < data.length; i++) {
                districtList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#district').append(districtList);
        }
    });
});

// SubDivisionOffice Dropdown By District
$("#district").on('change', function (event) {
    debugger
    var subDivList = '<option value="">Select Name Of District</option>';
    var subDiv = $("#sub-division-office");
    subDiv.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    debugger
    $.ajax({
        type: "post",
        url: subDivisionOfficeDropdownListUrl,
        data: { _districtId: $('#district').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            debugger
            $('#sub-division-office').empty();
            for (var i = 0; i < data.length; i++) {
                subDivList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#sub-division-office').append(subDivList);
        }
    });
});

// Taluka Dropdown By SubDivisionOffice
$("#sub-division-office").on('change', function (event) {
    debugger
    var talukaList = '<option value="">Select Name Of Taluka</option>';
    var taluka = $("#taluka");
    taluka.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    debugger
    $.ajax({
        type: "post",
        url: talukaDropdownListUrl,
        data: { _subDivisionOfficeId: $('#sub-division-office').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            debugger
            $('#taluka').empty();
            for (var i = 0; i < data.length; i++) {
                talukaList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#taluka').append(talukaList);
        }
    });
});

// Postal Office Dropdown By Taluka
$("#taluka").on('change', function (event) {
    debugger
    var postList = '<option value="">Select Name Of Postal Office</option>';
    var post = $("#postal-office");
    post.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    debugger
    $.ajax({
        type: "post",
        url: postDropdownListUrl,
        data: { _talukaId: $('#taluka').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            debugger
            $('#postal-office').empty();
            for (var i = 0; i < data.length; i++) {
                postList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#postal-office').append(postList);
        }
    });
});

// Center Category Change with Village, Town, City
$(".center-category").on('change', function (event) {
    debugger
    $("#name-of-center").val("");
    $("#trans-name-of-center").val("");
    $("#name-of-center").next("div.validation").remove();

    if ($(this).val() == '2') {
        $('#pincode').removeClass('d-none');
        $('#center-name').text('Name Of Town');
        $('#postal-drop-down').addClass('d-none');
        $('#postal-office').addClass('d-none');
        $("#name-of-center").attr("placeholder", "Enter Name Of Town");
        $('#trans-center-name').text('गावाचे नाव');
        $("#trans-name-of-center").attr("placeholder", "गावाचे नाव प्रविष्ट करा");
    }
    else if ($(this).val() == '3') {
        $('#pincode').removeClass('d-none');
        $('#center-name').text('Name Of City');
        $('#postal-drop-down').addClass('d-none');
        $('#postal-office').addClass('d-none');
        $("#name-of-center").attr("placeholder", "Enter Name Of City");
        $('#trans-center-name').text('शहराचे नाव');
        $("#trans-name-of-center").attr("placeholder", "शहराचे नाव प्रविष्ट करा");
    }
    else {
        $('#pincode').addClass('d-none');
        $('#center-name').text('Name Of Village');
        $('#postal-drop-down').removeClass('d-none');
        $('#postal-office').removeClass('d-none');
        $("#name-of-center").attr("placeholder", "Enter Name Of Village");
        $('#trans-center-name').text('खेडेगावाचे नाव');
        $("#trans-name-of-center").attr("placeholder", "खेडेगावाचे नाव प्रविष्ट करा");
    }
});

// Unique Name Of Center
$("#name-of-center").on('change', function (event) {
    debugger;
    var CenterCategoryPrmKey = $(".center-category:checked").val();
    var NameOfCenter = $("#name-of-center").val();

    $.ajax({
        url: uniqueCenterNameUrl,
        dataType: "json",
        type: "POST",
        data: ({ NameOfCenter: NameOfCenter, CenterCategoryPrmKey: CenterCategoryPrmKey }),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-center").next("div.validation").remove();
            }
            else {

                $("#name-of-center").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Center is already exist</div>");
            }
        },
        //error: function (xhr) {
        //    alert("An error has occured!!!");
        //}
    });
    $("#name-of-center").next("div.validation").remove();
});

// Validation For Pincode
$("#pin-code").change(function () {
    debugger
    var inputvalues = $("#pin-code").val();
    var regex = /^\d{6}$/;
    $('#pin-code').next("div.error").remove();
    if ($("#pin-code").val() != 123456) {
        if (!regex.test(inputvalues)) {
            $('#pin-code').after('<div class="error" style="color:red">Please Enter Valid PIN Number</div>');
            return regex.test(inputvalues);
        }
        $('#pin-code').next("div.error").remove();
    }
});

// Called When CenterCategory is already checked
$(document).ready(function () {
    debugger;
    if ($(".center-category:checked").val() == '2') {
        $('#pincode').removeClass('d-none');
        $('#center-name').text('Name Of Town');
        $('#postal-drop-down').addClass('d-none');
        $('#postal-office').addClass('d-none');
        $("#name-of-center").attr("placeholder", "Enter Name Of Town");
        $('#trans-center-name').text('गावाचे नाव');
        $("#trans-name-of-center").attr("placeholder", "गावाचे नाव प्रविष्ट करा");
    }
    else if ($(".center-category:checked").val() == '3') {
        $('#pincode').removeClass('d-none');
        $('#center-name').text('Name Of City');
        $('#postal-drop-down').addClass('d-none');
        $('#postal-office').addClass('d-none');
        $("#name-of-center").attr("placeholder", "Enter Name Of City");
        $('#trans-center-name').text('शहराचे नाव');
        $("#trans-name-of-center").attr("placeholder", "शहराचे नाव प्रविष्ट करा");
    }
    else {
        $('#pincode').addClass('d-none');
        $('#center-name').text('Name Of Village');
        $('#postal-drop-down').removeClass('d-none');
        $('#postal-office').removeClass('d-none');
        $("#name-of-center").attr("placeholder", "Enter Name Of Village");
        $('#trans-center-name').text('खेडेगावाचे नाव');
        $("#trans-name-of-center").attr("placeholder", "खेडेगावाचे नाव प्रविष्ट करा");
    }
});

$(".js-example-basic-multiple").select2();
$(".js-example-basic-multiple").select2({ width: '100%' });

// ************** CenterTradingEntityDetail **************

$(document).ready(function () {
    ClearCenterTradingEntityDetailValues()

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

                        if (tradingEntityId.trim().length < 36 || (volume < 1)) {
                            ClearCenterTradingEntityDetailDivErrors();

                            if (tradingEntityId.trim().length < 36)
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

                        // Validation
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var tradingEntityId = $("#name-of-trading-entity option:selected").val();
                        var tradingEntityIdText = $("#name-of-trading-entity option:selected").text();
                        var volume = $("#volume-text").val().trim();

                        if (tradingEntityId.trim().length < 36 || (volume < 1)) {
                            ClearCenterTradingEntityDetailDivErrors();

                            if (tradingEntityId.trim().length < 36)
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
        $('.btn-add-trading-entity').removeClass('disabled');
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
        $("#volume-text").val('');
    }

    // Clear DataTable Inputs
    function ClearCenterTradingEntityDetailDivErrors() {
        debugger;
        $('#name-of-trading-entity').next("div.error").remove();
        $('#volume-text').next("div.error").remove();
    }

    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger
        // not add event.preventDefault
        $("#lastrow").remove();

        // Return List Object, Hence Create Array
        var CenterTradingEntityDetail = new Array();

        // Recursive Loop By Row
        $("#center-trading-entity-detail-table TBODY TR").each(function () {
            debugger
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (centerTradingEntityDetailTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
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
        })
    });
});