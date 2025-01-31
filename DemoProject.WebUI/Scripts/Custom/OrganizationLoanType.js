
$(document).ready(function () {
    debugger

    ClearLoanTypeValues()
    // Initialising & Configuring DataTables 
    var loanTypeTable = $('#organization-loan-type-table').DataTable({

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
            { orderable: false, targets: 8 }
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
                    id: 'btn-addNew-loan-type'
                },
                action: function (e, dt, node, config, type) {
                    debugger
                    event.preventDefault();
                    var id = $("#loan-type-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Edit', 'Add'));
                    $("#btn-update-loan-type").hide();
                    $("#btn-add-loan-type").show();
                    ClearLoanTypeValues();
                    ClearLoanTypeDivErrors();

                    $('#add-loan-type').modal('show');

                    var rowNum = 0;

                    $('#btn-add-loan-type').on('click', function (event) {
                        debugger
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var loanTypeId = $("#account-class-id option:selected").val();
                        var loanTypeIdText = $("#account-class-id option:selected").text();
                        var maximumLoanTenure = $("#maximum-loan-tenure").val().trim();
                        var minimumDownPaymentPercentage = $("#minimum-down-payment-percentage").val().trim();
                        var sequenceNumber = $("#sequence-number-loan-type").val().trim();
                        var sequenceNumberText = $("#sequence-number-text-loan-type").val().trim();
                        var transSequenceNumber = $("#trans-sequence-number-text-loan-type").val().trim();
                        var activationDate = $("#activation-date-loan-type").val().trim();

                        var filteredData = loanTypeTable
                            .rows()
                            .indexes()
                            .filter(function (value, index) {
                                return loanTypeTable.row(value).data()[5] == $('#sequence-number-loan-type').val();
                            });

                        if ((loanTypeIdText == "") || (maximumLoanTenure == "") || (minimumDownPaymentPercentage == "") || (loanTypeTable.rows(filteredData).count() > 0) || (sequenceNumber == "") || (sequenceNumberText == "") || (transSequenceNumber == "") || (activationDate == "")) {

                            ClearLoanTypeDivErrors();

                            if (loanTypeIdText == "")
                                $('#account-class-id').after('<div class="error" style="color:red">Please Select Contact Type </div>');

                            if (maximumLoanTenure == "")
                                $('#maximum-loan-tenure').after('<div class="error" style="color:red">Please Enter Maximum Loan Tenure </div>');

                            if (minimumDownPaymentPercentage == "")
                                $('#minimum-down-payment-percentage').after('<div class="error" style="color:red">Please Enter Minimum Down Payment Percentage </div>');

                            if (loanTypeTable.rows(filteredData).count() > 0)
                                $('#sequence-number-loan-type').after('<div class="error" style="color:red">Please Enter Unique Sequence Number</div>');

                            if (sequenceNumber == "")
                                $('#sequence-number-loan-type').after('<div class="error" style="color:red">Please Enter Sequence Number </div>');

                            if (sequenceNumberText == "")
                                $('#sequence-number-text-loan-type').after('<div class="error" style="color:red">Please Enter Sequence Number Text</div>');

                            if (transSequenceNumber == "")
                                $('#trans-sequence-number-text-loan-type').after('<div class="error" style="color:red">Please Enter Trans Sequence Number </div>');

                            if (activationDate == "")
                                $('#activation-date-loan-type').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            var row = loanTypeTable.row.add([
                                tag,
                                loanTypeId,
                                loanTypeIdText,
                                maximumLoanTenure,
                                minimumDownPaymentPercentage,
                                sequenceNumber,
                                sequenceNumberText,
                                transSequenceNumber,
                                activationDate,

                            ]).draw();

                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            loanTypeTable.column(1).visible(false);
                            loanTypeTable.columns.adjust().draw();
                            ClearLoanTypeValues();
                            ClearLoanTypeDivErrors();

                            $('#add-loan-type').modal('hide');
                        }

                    });
                    return false;
                },


            },
            {

                text: 'Edit',
                attr: {
                    id: 'btn-edit-loan-type'
                },
                //className: 'btn btn-Edit disabled',
                action: function (e, dt, node, config, String, indexes) {
                    debugger

                    var arr = new Array();
                    $("#organization-loan-type-table  input[type='checkbox']").each(function (index) {
                        debugger
                        var row = $(this).closest("tr");
                        selectedRow = loanTypeTable.row(row).index();
                        var tdrow = (loanTypeTable.row(selectedRow).data());
                        arr.push({
                            td0: tdrow[1]
                        });
                    });

                    var id = $("#loan-type-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Add', 'Edit'));
                    $("#btn-add-loan-type").hide();
                    $("#btn-update-loan-type").show();
                    ClearLoanTypeDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit').data('rowindex');
                        var id = $("#add-loan-type").attr("id");
                        var myModal = $('#' + id).modal();

                        var d = new Date(columnValues[8]),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var newDate = [year, month, day].join('-');

                        $('#account-class-id', myModal).val(columnValues[1]);
                        $('#maximum-loan-tenure', myModal).val(columnValues[3]);
                        $('#minimum-down-payment-percentage', myModal).val(columnValues[4]);
                        $('#sequence-number-loan-type', myModal).val(columnValues[5]);
                        $('#sequence-number-text-loan-type', myModal).val(columnValues[6]);
                        $('#trans-sequence-number-text-loan-type', myModal).val(columnValues[7]);
                        $('#activation-date-loan-type', myModal).val(newDate);
                        myModal.modal({ show: true });
                    }
                    else {
                        $('.btn-Edit').addClass('disabled');
                        $("#add-loan-type").modal("hide");
                    }

                    arr.map(function (obj) {
                        debugger;
                        $('#fund-id').find("option[value='" + obj.td0 + "']").hide();
                    });
                    $('#btn-update-loan-type').data('rowindex', columnValues);

                    $(document).on('click', "#btn-update-loan-type", function (event) {
                        debugger;
                        $('#select-all-loan-Type').prop('checked', false);
                        colorchange();

                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var accountId = $('#account-class-id option:selected').val();
                        var accountIdText = $('#account-class-id option:selected').text();
                        var maximumLoanTenure = $('#maximum-loan-tenure').val();
                        var minimumDownPaymentPercentage = $('#minimum-down-payment-percentage').val();
                        var sequenceNumber = $('#sequence-number-loan-type').val();
                        var sequenceNumberText = $('#sequence-number-text-loan-type').val();
                        var transSequenceNumberText = $('#trans-sequence-number-text-loan-type').val();
                        var activationDate = $('#activation-date-loan-type').val();

                        var columnValues = $('#btn-update-loan-type').data('rowindex');

                        var filteredData = loanTypeTable
                            .rows()
                            .indexes()
                            .filter(function (value, index) {
                                return loanTypeTable.row(value).data()[5] == $('#sequence-number-loan-type').val();
                            });

                        if ((accountIdText == "") || (maximumLoanTenure == "") || (minimumDownPaymentPercentage == "") || (loanTypeTable.rows(filteredData).count() > 0 && columnValues[5] != sequenceNumber) || (sequenceNumber == "") || (sequenceNumberText == "") || (transSequenceNumberText == "") || (activationDate == "")) {

                            ClearLoanTypeDivErrors();

                            if (accountIdText == "")
                                $('#account-class-id').after('<div class="error" style="color:red">Please Select Contact Type </div>');

                            if (maximumLoanTenure == "")
                                $('#maximum-loan-tenure').after('<div class="error" style="color:red">Please Enter Maximum Loan Tenure </div>');

                            if (minimumDownPaymentPercentage == "")
                                $('#minimum-down-payment-percentage').after('<div class="error" style="color:red">Please Enter Minimum Down Payment Percentage </div>');

                            if (loanTypeTable.rows(filteredData).count() > 0)
                                $('#sequence-number-loan-type').after('<div class="error" style="color:red">Please Enter Unique Sequence Number</div>');

                            if (sequenceNumber == "")
                                $('#sequence-number-loan-type').after('<div class="error" style="color:red">Please Enter Sequence Number </div>');

                            if (sequenceNumberText == "")
                                $('#sequence-number-text-loan-type').after('<div class="error" style="color:red">Please Enter Sequence Number Text</div>');

                            if (transSequenceNumberText == "")
                                $('#trans-sequence-number-text-loan-type').after('<div class="error" style="color:red">Please Enter Trans Sequence Number </div>');

                            if (activationDate == "")
                                $('#activation-date-loan-type').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            loanTypeTable.row($(this).attr('rowindex')).data([
                                tag,
                                accountId,
                                accountIdText,
                                maximumLoanTenure,
                                minimumDownPaymentPercentage,
                                sequenceNumber,
                                sequenceNumberText,
                                transSequenceNumberText,
                                activationDate,

                            ]).draw();

                            loanTypeTable.column(1).visible(false);
                            loanTypeTable.columns.adjust().draw();

                            var columnValues = $('#btn-update-loan-type').data('rowindex');
                            FromShowLoanTypeValues(columnValues);

                            $('#add-loan-type').modal('hide');
                            $('.btn-add-loan-type').removeClass('disabled');
                            $('.btn-Delete').addClass('disabled');
                            $('.btn-Edit').addClass('disabled');
                        }
                    })
                },
            },

            {
                text: 'Delete',
                attr: {
                    id: 'btn-Delete-loan-type'
                },

                action: function (e, dt, node, config) {
                    debugger;
                    var isChecked = $("input[type='checkbox']").is(":checked");
                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {
                            debugger;
                            if ($("input[type='checkbox']:checked").each(function () {
                                debugger;
                                loanTypeTable.row($("input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                                var arr = $('.btn-Delete').data('rowindex');
                                arr.map(function (obj) {
                                    debugger;
                                    $('#account-class-id').find("option[value='" + obj.td0 + "']").show();
                                    $("#account-class-id").prop("selectedIndex", 0);
                                });
                                $('.btn-add-loan-type').removeClass('disabled');
                                $('.btn-Delete').addClass('disabled');
                                $('.btn-Edit').addClass('disabled');
                                $('#select-all-loan-Type').prop('checked', false);
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

        ClearLoanTypeDivErrors();
        ClearLoanTypeValues();
        $('.btn-Delete').addClass('disabled');
        $('.btn-Edit').addClass('disabled');
        $('.checks').prop('checked', false);
        $('.btn-add-loan-type').removeClass('disabled');
        $('#select-all-loan-Type').prop('checked', false);
    });

    //To dropdown added values by edit
    function FromShowLoanTypeValues(columnValues) {
        debugger;
        $('#account-class-id').find("option[value='" + columnValues[1] + "']").show();
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


    var btns = $('#btn-addNew-loan-type');
    btns.addClass('btn btn-success  btn-add-loan-type').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-loan-type');
    btns.addClass('btn btn-primary btn-Edit disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-Delete-loan-type');
    btns.addClass('btn btn-danger btn-Delete DeleteAll disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $('#select-all-loan-Type').on('click', function () {
        debugger;
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);
            var arr = new Array();
            $('#organization-loan-type-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                var row = $(this).closest("tr");
                selectedRow = loanTypeTable.row(row).index();
                var tdrow = (loanTypeTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-Delete').data('rowindex', arr);
                $('.btn-add-loan-type').addClass('disabled');
                $('.btn-Edit').addClass('disabled');
                $('.btn-Delete').removeClass('disabled');
            });
        }
        else {
            $('#organization-loan-type-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-loan-type').removeClass('disabled');
                $('.btn-Delete').addClass('disabled');
            });
        }
    });


    // binding the change event-handler to the tbody:
    $('#organization-loan-type-table tbody').on('change', function () {

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
            $('.btn-add-loan-type').removeClass('disabled');
        }

        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#select-all-loan-Type').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    $('#organization-loan-type-table tbody').on('click', "input[type=checkbox]", function () {
        $('#organization-loan-type-table input[type="checkbox"]:checked').each(function () {

            var isChecked = $(this).prop("checked");

            if (isChecked) {
                //var row = $(this).closest("tr")[0];
                //selectedRow = centerTradingEntityDetailTable.row(row).index();
                //var tdrow = (centerTradingEntityDetailTable.row(selectedRow).data());

                var arr = new Array();
                $("input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    selectedRow = loanTypeTable.row(row).index();
                    var tdrow = (loanTypeTable.row(selectedRow).data());
                    arr.push({
                        td0: tdrow[1]
                    });

                    $('.btn-add-loan-type').addClass('disabled');
                    $('.btn-Edit').removeClass('disabled');
                    $('.btn-Delete').removeClass('disabled');


                    $('#btn-update-loan-type').attr('rowindex', selectedRow);
                    $('.btn-Edit').data('rowindex', tdrow);
                    $('.btn-Delete').data('rowindex', arr);
                    $('#select-all-loan-Type').data('rowindex', arr);
                });
            }
        });
    });

    //To page load table each row get value & dropdown value Hide 
    $('#organization-loan-type-table > tbody > tr').each(function () {
        debugger;
        var currentRow = $(this).closest("tr");
        var columnvalue = (loanTypeTable.row(currentRow).data());
        if (typeof columnvalue != 'undefined' && columnvalue != null) {

            $('#account-class-id').find("option[value='" + columnvalue[1] + "']").hide();
        }
        else {
            return true;
        }
    });

    //To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    function ClearLoanTypeValues() {
        debugger
        var accountClassId = $("#account-class-id option:selected").val();
        $('#account-class-id').find("option[value='" + accountClassId + "']").hide();
        $("#account-class-id").prop("selectedIndex", 0);

        $("#account-class-id").val('');
        $("#maximum-loan-tenure").val('');
        $("#minimum-down-payment-percentage").val('');
        $("#sequence-number-loan-type").val('');
        $("#sequence-number-text-loan-type").val('');
        $("#trans-sequence-number-text-loan-type").val('');
        $("#activation-date-loan-type").val('');
    }

    // Clear DataTable Inputs
    function ClearLoanTypeDivErrors() {
        debugger
        $('#account-class-id').next("div.error").remove();
        $('#maximum-loan-tenure').next("div.error").remove();
        $('#minimum-down-payment-percentage').next("div.error").remove();
        $('#sequence-number-loan-type').next("div.error").remove();
        $('#sequence-number-text-loan-type').next("div.error").remove();
        $('#trans-sequence-number-text-loan-type').next("div.error").remove();
        $('#activation-date-loan-type').next("div.error").remove();
    }

    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger
        //not add event.preventDefault
        $(".lastrow").remove();

        // Return List Object, Hence Create Array
        var OrganizationLoanType = new Array();

        // Recursive Loop By Row
        $("#organization-loan-type-table TBODY TR").each(function () {
            debugger
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (loanTypeTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                return false;
            }

            var AccountClassId = columnvalue[1];
            var MaximumLoanTenure = columnvalue[3];
            var MinimumDownPaymentPercentage = columnvalue[4];
            var SequenceNumber = columnvalue[5];
            var SequenceNumberText = columnvalue[6];
            var TransSequenceNumberText = columnvalue[7];
            var ActivationDate = columnvalue[8];

            OrganizationLoanType.push({
                "AccountClassId": AccountClassId,
                "MaximumLoanTenure": MaximumLoanTenure,
                "MinimumDownPaymentPercentage": MinimumDownPaymentPercentage,
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
            data: { '_organizationLoanType': OrganizationLoanType },
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
