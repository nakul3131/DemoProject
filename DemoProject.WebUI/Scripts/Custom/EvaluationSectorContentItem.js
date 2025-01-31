$(document).ready(function () {
    debugger
    var model = '<option value="">-- Select Content Item --</option>';
    var Evaluations = $("#evaluation-section-id");
    Evaluations.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    debugger
    $.ajax({
        type: "post",
        url: evaluationDropdownListForCreateUrl,
        data: { EvaluationSectionId: $('#evaluation-section-id').val() },
        datatype: "json",
        traditional: true,
        success: function (data) {
            debugger
            $('#content-item-id').empty();
            for (var i = 0; i < data.length; i++) {
                model += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
            }
            $('#content-item-id').append(model);
        }
    });

    ClearEvaluationSectorContentItemValues()

    // Initialising & Configuring DataTables 
    var evaluationContentTable = $('#evaluation-content-item-table').DataTable({

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

                    var id = $("#evaluation-content-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Edit', 'Add'));
                    $("#btn-update-evaluation-content-item").hide();
                    $("#btn-add-evaluation-content-item").show();
                    ClearEvaluationSectorContentItemDivErrors();
                    ClearEvaluationSectorContentItemValues();

                    $('#add-evaluation-sector-content-item').modal('show');

                    var rowNum = 0;

                    $('#btn-add-evaluation-content-item').on('click', function (event) {
                        debugger
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var ContentItemId = $("#content-item-id option:selected").val();
                        var ContentItemIdText = $("#content-item-id option:selected").text();
                        var sequenceNumber = $("#sequence-number").val().trim();
                        var activationDate = $("#activation-date").val().trim();
                        var expiryDate = $("#expiry-date").val().trim();


                        var filteredData = evaluationContentTable
                            .rows()
                            .indexes()
                            .filter(function (value, index) {
                                return evaluationContentTable.row(value).data()[3] == $('#sequence-number').val();
                            });

                        if ((ContentItemId.trim().length < 36) || (evaluationContentTable.rows(filteredData).count() > 0) || (sequenceNumber == "")  || (activationDate == "")) {

                            ClearEvaluationSectorContentItemDivErrors();

                            if (ContentItemId.trim().length < 36)
                                $('#content-item-id').after('<div class="error" style="color:red">Please Select Content Item </div>');

                            if (evaluationContentTable.rows(filteredData).count() > 0)
                                $('#sequence-number').after('<div class="error" style="color:red">Please Enter Unique Sequence Number</div>');

                            if (sequenceNumber == "")
                                $('#sequence-number').after('<div class="error" style="color:red">Please Enter Sequence Number </div>');

                            if (activationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            var row = evaluationContentTable.row.add([
                                tag,
                                ContentItemId,
                                ContentItemIdText,
                                sequenceNumber,
                                activationDate,
                                expiryDate,

                            ]).draw();

                            //$('#select-all-content-item').removeClass('disabled');
                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            evaluationContentTable.column(1).visible(false);
                            evaluationContentTable.columns.adjust().draw();
                            ClearEvaluationSectorContentItemValues();
                            ClearEvaluationSectorContentItemDivErrors();
                            $('#add-evaluation-sector-content-item').modal('hide');
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
                    $("#evaluation-content-item-table  input[type='checkbox']").each(function (index) {
                        debugger
                        var row = $(this).closest("tr");
                        selectedRow = evaluationContentTable.row(row).index();
                        var tdrow = (evaluationContentTable.row(selectedRow).data());
                        arr.push({
                            td0: tdrow[1]
                        });
                    });

                    var id = $("#evaluation-content-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Add', 'Edit'));
                    $("#btn-add-evaluation-content-item").hide();
                    $("#btn-update-evaluation-content-item").show();
                    ClearEvaluationSectorContentItemDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit').data('rowindex');
                        var id = $("#add-evaluation-sector-content-item").attr("id");
                        var myModal = $('#' + id).modal();
                        var d = new Date(columnValues[4]),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var activationDate = [year, month, day].join('-');

                        var d = new Date(columnValues[5]),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var expiryDate = [year, month, day].join('-');

                        $('#content-item-id', myModal).val(columnValues[1]);
                        $('#sequence-number', myModal).val(columnValues[3]);
                        $('#activation-date', myModal).val(activationDate);
                        $('#expiry-date', myModal).val(expiryDate);
                        myModal.modal({ show: true });
                    }

                    else {
                        $('.btn-Edit').addClass('disabled');
                        $("#add-evaluation-sector-content-item").modal("hide");
                    }

                    arr.map(function (obj) {
                        debugger;
                        $('#content-item-id').find("option[value='" + obj.td0 + "']").hide();
                    });
                    $('#btn-update-evaluation-content-item').data('rowindex', columnValues);

                    $(document).on('click', "#btn-update-evaluation-content-item", function (event) {
                        debugger;
                        $('#select-all-content-item').prop('checked', false);
                        colorchange();
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var ContentItemId = $('#content-item-id option:selected').val();
                        var ContentItemIdText = $('#content-item-id option:selected').text();
                        var sequenceNumber = $("#sequence-number").val().trim();
                        var activationDate = $("#activation-date").val().trim();
                        var expiryDate = $("#expiry-date").val().trim();

                        var columnValues = $('#btn-update-evaluation-content-item').data('rowindex');

                        var filteredData = evaluationContentTable
                            .rows()
                            .indexes()
                            .filter(function (value, index) {
                                return evaluationContentTable.row(value).data()[3] == $('#sequence-number').val();
                            });

                        if ((ContentItemId.trim().length < 36) || (evaluationContentTable.rows(filteredData).count() > 0 && columnValues[3] != sequenceNumber) || (sequenceNumber == "") || (activationDate == "")) {

                            ClearEvaluationSectorContentItemDivErrors();

                            if (ContentItemId.trim().length < 36)
                                $('#content-item-id').after('<div class="error" style="color:red">Please Select Content Item </div>');

                            if (evaluationContentTable.rows(filteredData).count() > 0)
                                $('#sequence-number').after('<div class="error" style="color:red">Please Enter Unique Sequence Number</div>');

                            if (sequenceNumber == "")
                                $('#sequence-number').after('<div class="error" style="color:red">Please Enter Sequence Number </div>');

                            if (activationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            evaluationContentTable.row($(this).attr('rowindex')).data([
                                tag,
                                ContentItemId,
                                ContentItemIdText,
                                sequenceNumber,
                                activationDate,
                                expiryDate,
                            ]).draw();

                            evaluationContentTable.column(1).visible(false);
                            evaluationContentTable.columns.adjust().draw();

                            var columnValues = $('#btn-update-evaluation-content-item').data('rowindex');
                            FromShowEvaluationContentItemValues(columnValues);

                            $('#add-evaluation-sector-content-item').modal('hide');
                            $('.btn-add-evaluation-content-item').removeClass('disabled');
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
                                evaluationContentTable.row($("input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                                var arr = $('.btn-Delete').data('rowindex');
                                arr.map(function (obj) {
                                    debugger;
                                    $('#content-item-id').find("option[value='" + obj.td0 + "']").show();
                                    $("#content-item-id").prop("selectedIndex", 0);
                                });
                                $('.btn-add-evaluation-content-item').removeClass('disabled');
                                $('.btn-Delete').addClass('disabled');
                                $('.btn-Edit').addClass('disabled');
                                $('#select-all-content-item').prop('checked', false);
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
        ClearEvaluationSectorContentItemDivErrors();
        ClearEvaluationSectorContentItemValues();
        $('.btn-Delete').addClass('disabled');
        $('.btn-Edit').addClass('disabled');
        $('.checks').prop('checked', false);
        $('.btn-add-evaluation-content-item').removeClass('disabled');
        $('#select-all-content-item').prop('checked', false);
    });

    //To dropdown added values by edit
    function FromShowEvaluationContentItemValues(columnValues) {
        debugger;
        $('#content-item-id').find("option[value='" + columnValues[1] + "']").show();
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
    btns.addClass('btn btn-success  btn-add-evaluation-content-item').append('<i class="fas fa-plus icon"></i>');
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

    $('#select-all-content-item').on('click', function () {
        debugger;
        debugger;
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);
            var arr = new Array();
            $('#evaluation-content-item-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                var row = $(this).closest("tr");
                selectedRow = evaluationContentTable.row(row).index();
                var tdrow = (evaluationContentTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-Delete').data('rowindex', arr);
                $('.btn-add-evaluation-content-item').addClass('disabled');
                $('.btn-Edit').addClass('disabled');
                $('.btn-Delete').removeClass('disabled');
            });
        }
        else {
            $('#evaluation-content-item-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-evaluation-content-item').removeClass('disabled');
                $('.btn-Delete').addClass('disabled');
            });
        }
    });

    // binding the change event-handler to the tbody:
    $('#evaluation-content-item-table tbody').on('change', function () {

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
            $('.btn-add-evaluation-content-item').removeClass('disabled');
        }
        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#select-all-content-item').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    $('#evaluation-content-item-table tbody').on('click', "input[type=checkbox]", function () {
        $('#evaluation-content-item-table input[type="checkbox"]:checked').each(function () {

            var isChecked = $(this).prop("checked");

            if (isChecked) {

                var arr = new Array();
                $("input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    selectedRow = evaluationContentTable.row(row).index();
                    var tdrow = (evaluationContentTable.row(selectedRow).data());
                    arr.push({
                        td0: tdrow[1]
                    });

                    //Variant Dropdown List For Edit
                    var model = '<option value="">--Select Content Item ---</option>';
                    var Evaluations = $("#evaluation-section-id");
                    Evaluations.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
                    debugger
                    $.ajax({
                        type: "post",
                        url: evaluationDropdownListForEditUrl,
                        data: { EvaluationSectionId: $('#evaluation-section-id').val(), ContentItemId: tdrow[1] },
                        datatype: "json",
                        traditional: true,
                        success: function (data) {
                            debugger
                            $('#content-item-id').empty();
                            for (var i = 0; i < data.length; i++) {
                                model += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
                            }
                            $('#content-item-id').append(model);
                        }
                    });

                    $('.btn-add-evaluation-content-item').addClass('disabled');
                    $('.btn-Edit').removeClass('disabled');
                    $('.btn-Delete').removeClass('disabled');


                    $('#btn-update-evaluation-content-item').attr('rowindex', selectedRow);
                    $('.btn-Edit').data('rowindex', tdrow);
                    $('.btn-Delete').data('rowindex', arr);
                    $('#select-all-content-item').data('rowindex', arr);
                });
            }
        });
    });

    //To page load table each row get value & dropdown value Hide 
    $('#evaluation-content-item-table > tbody > tr').each(function () {
        debugger;
        var currentRow = $(this).closest("tr");
        var columnvalue = (evaluationContentTable.row(currentRow).data());

        if (typeof columnvalue != 'undefined' && columnvalue != null) {

            $('#content-item-id').find("option[value='" + columnvalue[1] + "']").hide();
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
    function ClearEvaluationSectorContentItemValues() {
        debugger
        var ContentItemId = $("#content-item-id option:selected").val();
        $('#content-item-id').find("option[value='" + ContentItemId + "']").hide();
        $("#content-item-id").prop("selectedIndex", 0);

        $("#content-item-id").val('');
        $("#sequence-number").val('');
        $("#activation-date").val('');
        $("#expiry-date").val('');

    }

    //Clear Div Errors
    function ClearEvaluationSectorContentItemDivErrors() {
        debugger
        $('#content-item-id').next("div.error").remove();
        $('#sequence-number').next("div.error").remove();
        $('#activation-date').next("div.error").remove();
        $('#expiry-date').next("div.error").remove();

    }

    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger
        //not add event.preventDefault
        $(".lastrow").remove();

        // Return List Object, Hence Create Array
        var EvaluationSectorContentItem = new Array();

        // Recursive Loop By Row
        $("#evaluation-content-item-table TBODY TR").each(function () {
            debugger
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (evaluationContentTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                return false;
            }

            if (!($(this).find("input[type='checkbox']").hasClass("disabled"))) {
                var ContentItemId = columnvalue[1];
                var SequenceNumber = columnvalue[3];
                var ActivationDate = columnvalue[4];
                var ExpiryDate = columnvalue[5];

                EvaluationSectorContentItem.push({
                    "ContentItemId": ContentItemId,
                    "SequenceNumber": SequenceNumber,
                    "ActivationDate": ActivationDate,
                    "ExpiryDate": ExpiryDate,
                });
            }

        });

        // Call Cantroller Save Method 
        $.ajax({
            url: SaveEvaluationSectorContentItemDataTableUrl,
            type: 'POST',
            data: { '_EvaluationSectorContentItem': EvaluationSectorContentItem },
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