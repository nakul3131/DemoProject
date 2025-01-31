
$(document).ready(function () {
    debugger

    ClearFundValues()

    // Initialising & Configuring DataTables 
    var fundTable = $('#organization-fund-table').DataTable({

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
                    id: 'btn-addNew-fund'
                },
                action: function (e, dt, node, config, type) {
                    debugger
                    event.preventDefault();

                    var id = $("#fund-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Edit', 'Add'));
                    $("#btn-update-fund").hide();
                    $("#btn-add-fund").show();
                    ClearFundValues();
                    ClearFundDivErrors();

                    $('#add-fund').modal('show');

                    var rowNum = 0;

                    $('#btn-add-fund').on('click', function (event) {
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var fundId = $("#fund-id option:selected").val();
                        var fundIdText = $("#fund-id option:selected").text();
                        var sequenceNumber = $("#sequence-number").val().trim();
                        var sequenceNumberText = $("#sequence-number-text").val().trim();
                        var transSequenceNumber = $("#trans-sequence-number-text").val().trim();
                        var activationDate = $("#activation-date").val().trim();

                        var filteredData = fundTable
                            .rows()
                            .indexes()
                            .filter(function (value, index) {
                                return fundTable.row(value).data()[3] == $('#sequence-number').val();
                            });

                        if ((fundId.trim().length < 36) || (fundTable.rows(filteredData).count() > 0) || (sequenceNumber == "") || (sequenceNumberText == "") || (transSequenceNumber == "") || (activationDate == "")) {

                            ClearFundDivErrors();

                            if (fundId.trim().length < 36)
                                $('#fund-id').after('<div class="error" style="color:red">Please Select Contact Type </div>');

                            if (fundTable.rows(filteredData).count() > 0)
                                $('#sequence-number').after('<div class="error" style="color:red">Please Enter Unique Sequence Number</div>');

                            if (sequenceNumber == "")
                                $('#sequence-number').after('<div class="error" style="color:red">Please Enter Sequence Number </div>');

                            if (sequenceNumberText == "")
                                $('#sequence-number-text').after('<div class="error" style="color:red">Please Enter Sequence Number Text</div>');

                            if (transSequenceNumber == "")
                                $('#trans-sequence-number-text').after('<div class="error" style="color:red">Please Enter Trans Sequence Number </div>');

                            if (activationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            var row = fundTable.row.add([
                                tag,
                                fundId,
                                fundIdText,
                                sequenceNumber,
                                sequenceNumberText,
                                transSequenceNumber,
                                activationDate,

                            ]).draw();

                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            fundTable.column(1).visible(false);
                            fundTable.columns.adjust().draw();
                            ClearFundValues();
                            ClearFundDivErrors();
                            $('#add-fund').modal('hide');
                        }

                    });
                    return false;
                },
            },
            {

                text: 'Edit',
                attr: {
                    id: 'btn-edit-fund'
                },
                //className: 'btn btn-Edit disabled',
                action: function (e, dt, node, config, String, indexes) {
                    debugger

                    var arr = new Array();
                    $("#organization-fund-table  input[type='checkbox']").each(function (index) {
                        debugger
                        var row = $(this).closest("tr");
                        selectedRow = fundTable.row(row).index();
                        var tdrow = (fundTable.row(selectedRow).data());
                        arr.push({
                            td0: tdrow[1]
                        });
                    });

                    var id = $("#fund-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Add', 'Edit'));
                    $("#btn-add-fund").hide();
                    $("#btn-update-fund").show();
                    ClearFundDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit').data('rowindex');
                        var id = $("#add-fund").attr("id");
                        var myModal = $('#' + id).modal();
                        var d = new Date(columnValues[6]),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var newDate = [year, month, day].join('-');

                        $('#fund-id', myModal).val(columnValues[1]);
                        $('#sequence-number', myModal).val(columnValues[3]);
                        $('#sequence-number-text', myModal).val(columnValues[4]);
                        $('#trans-sequence-number-text', myModal).val(columnValues[5]);
                        $('#activation-date', myModal).val(newDate);
                        myModal.modal({ show: true });
                    }

                    else {
                        $('.btn-Edit').addClass('disabled');
                        $("#add-fund").modal("hide");
                    }

                    arr.map(function (obj) {
                        debugger;
                        $('#fund-id').find("option[value='" + obj.td0 + "']").hide();
                    });
                    $('#btn-update-fund').data('rowindex', columnValues);

                    $(document).on('click', "#btn-update-fund", function (event) {
                        debugger;
                        $('#select-all-fund').prop('checked', false);
                        colorchange();
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var fundId = $('#fund-id option:selected').val();
                        var fundIdText = $('#fund-id option:selected').text();
                        var sequenceNumber = $('#sequence-number').val();
                        var sequenceNumberText = $('#sequence-number-text').val();
                        var transSequenceNumberText = $('#trans-sequence-number-text').val();
                        var activationDate = $('#activation-date').val();

                        var columnValues = $('#btn-update-fund').data('rowindex');

                        var filteredData = fundTable
                            .rows()
                            .indexes()
                            .filter(function (value, index) {
                                return fundTable.row(value).data()[3] == $('#sequence-number').val();
                            });

                        if ((fundId.trim().length < 36) || (fundTable.rows(filteredData).count() > 0 && columnValues[3] != sequenceNumber) || (sequenceNumber == "") || (sequenceNumberText == "") || (transSequenceNumberText == "") || (activationDate == "")) {

                            ClearFundDivErrors();

                            if (fundId.trim().length < 36)
                                $('#fund-id').after('<div class="error" style="color:red">Please Select Contact Type </div>');

                            if ((fundTable.rows(filteredData).count() > 0))
                                $('#sequence-number').after('<div class="error" style="color:red">Please Enter Unique Sequence Number</div>');

                            if (sequenceNumber == "")
                                $('#sequence-number').after('<div class="error" style="color:red">Please Enter Sequence Number </div>');

                            if (sequenceNumberText == "")
                                $('#sequence-number-text').after('<div class="error" style="color:red">Please Enter Sequence Number Text</div>');

                            if (transSequenceNumberText == "")
                                $('#trans-sequence-number-text').after('<div class="error" style="color:red">Please Enter Trans Sequence Number </div>');

                            if (activationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            fundTable.row($(this).attr('rowindex')).data([
                                tag,
                                fundId,
                                fundIdText,
                                sequenceNumber,
                                sequenceNumberText,
                                transSequenceNumberText,
                                activationDate,
                            ]).draw();

                            fundTable.column(1).visible(false);
                            fundTable.columns.adjust().draw();

                            var columnValues = $('#btn-update-fund').data('rowindex');
                            FromShowFundValues(columnValues);

                            $('#add-fund').modal('hide');
                            $('.btn-add-fund').removeClass('disabled');
                            $('.btn-Delete').addClass('disabled');
                            $('.btn-Edit').addClass('disabled');
                        }
                    })
                },
            },

            {
                text: 'Delete',
                attr: {
                    id: 'btn-Delete-fund'
                },

                action: function (e, dt, node, config) {
                    debugger;
                    var isChecked = $("input[type='checkbox']").is(":checked");
                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {
                            debugger;
                            if ($("input[type='checkbox']:checked").each(function () {
                                debugger;
                                fundTable.row($("input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                                var arr = $('.btn-Delete').data('rowindex');
                                arr.map(function (obj) {
                                    debugger;
                                    $('#fund-id').find("option[value='" + obj.td0 + "']").show();
                                    $("#fund-id").prop("selectedIndex", 0);
                                });
                                $('.btn-add-fund').removeClass('disabled');
                                $('.btn-Delete').addClass('disabled');
                                $('.btn-Edit').addClass('disabled');
                                $('#select-all-fund').prop('checked', false);
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
        ClearFundDivErrors();
        ClearFundValues();
        $('.btn-Delete').addClass('disabled');
        $('.btn-Edit').addClass('disabled');
        $('.checks').prop('checked', false);
        $('.btn-add-fund').removeClass('disabled');
        $('#select-all-fund').prop('checked', false);
    });

    //To dropdown added values by edit
    function FromShowFundValues(columnValues) {
        debugger;
        $('#fund-id').find("option[value='" + columnValues[1] + "']").show();
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

    var btns = $('#btn-addNew-fund');
    btns.addClass('btn btn-success  btn-add-fund').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-fund');
    btns.addClass('btn btn-primary btn-Edit disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-Delete-fund');
    btns.addClass('btn btn-danger btn-Delete DeleteAll disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $('#select-all-fund').on('click', function () {
        debugger;
        debugger;
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);
            var arr = new Array();
            $('#organization-fund-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                var row = $(this).closest("tr");
                selectedRow = fundTable.row(row).index();
                var tdrow = (fundTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-Delete').data('rowindex', arr);
                $('.btn-add-fund').addClass('disabled');
                $('.btn-Edit').addClass('disabled');
                $('.btn-Delete').removeClass('disabled');
            });
        }
        else {
            $('#organization-fund-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-fund').removeClass('disabled');
                $('.btn-Delete').addClass('disabled');
            });
        }
    });

    // binding the change event-handler to the tbody:
    $('#organization-fund-table tbody').on('change', function () {

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
            $('.btn-add-fund').removeClass('disabled');
        }
        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#select-all-fund').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    $('#organization-fund-table tbody').on('click', "input[type=checkbox]", function () {
        $('#organization-fund-table input[type="checkbox"]:checked').each(function () {

            var isChecked = $(this).prop("checked");

            if (isChecked) {

                var arr = new Array();
                $("input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    selectedRow = fundTable.row(row).index();
                    var tdrow = (fundTable.row(selectedRow).data());
                    arr.push({
                        td0: tdrow[1]
                    });

                    $('.btn-add-fund').addClass('disabled');
                    $('.btn-Edit').removeClass('disabled');
                    $('.btn-Delete').removeClass('disabled');


                    $('#btn-update-fund').attr('rowindex', selectedRow);
                    $('.btn-Edit').data('rowindex', tdrow);
                    $('.btn-Delete').data('rowindex', arr);
                    $('#select-all-fund').data('rowindex', arr);
                });
            }
        });
    });

    //To page load table each row get value & dropdown value Hide 
    $('#organization-fund-table > tbody > tr').each(function () {
        debugger;

        var currentRow = $(this).closest("tr");
        var columnvalue = (fundTable.row(currentRow).data());
        if (typeof columnvalue != 'undefined' && columnvalue != null) {

            $('#fund-id').find("option[value='" + columnvalue[1] + "']").hide();
        }
        else {
            return true;
        }

    });

    //To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    function ClearFundValues() {
        debugger
        var fundId = $("#fund-id option:selected").val();
        $('#fund-id').find("option[value='" + fundId + "']").hide();
        $("#fund-id").prop("selectedIndex", 0);

        $("#fund-id").val('');
        $("#sequence-number").val('');
        $("#sequence-number-text").val('');
        $("#trans-sequence-number-text").val('');
        $("#activation-date").val('');

    }

    //Clear Div Errors
    function ClearFundDivErrors() {
        debugger
        $('#fund-id').next("div.error").remove();
        $('#sequence-number').next("div.error").remove();
        $('#sequence-number-text').next("div.error").remove();
        $('#trans-sequence-number-text').next("div.error").remove();
        $('#activation-date').next("div.error").remove();

    }

    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger
        //not add event.preventDefault
        $(".lastrow").remove();

        // Return List Object, Hence Create Array
        var OrganizationFund = new Array();

        // Recursive Loop By Row
        $("#organization-fund-table TBODY TR").each(function () {
            debugger
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (fundTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                return false;
            }

            var row = $(this);

            var OrganizationFundId = columnvalue[1];
            var SequenceNumber = columnvalue[3];
            var SequenceNumberText = columnvalue[4];
            var TransSequenceNumberText = columnvalue[5];
            var ActivationDate = columnvalue[6];

            OrganizationFund.push({
                "FundId": OrganizationFundId,
                "SequenceNumber": SequenceNumber,
                "SequenceNumberText": SequenceNumberText,
                "TransSequenceNumberText": TransSequenceNumberText,
                "ActivationDate": ActivationDate,
            });
        });

        // Call Cantroller Save Method 
        $.ajax({
            url: url,
            type: 'POST',
            data: { '_organizationFund': OrganizationFund },
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

