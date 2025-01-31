
//Unique Name Of Business Office 
$("#name-of-business-office").on('change', function (event) {
    debugger;
    var NameOfBusinessOffice = $("#name-of-business-office").val();
    $.ajax({
        url: uniqueNameOfBusinessOffice,
        dataType: "json",
        type: "POST",
        data: ({ NameOfBusinessOffice: NameOfBusinessOffice }),
        success: function (data) {
            debugger;
            if (data) {
                $("#name-of-business-office").next("div.validation").remove();
            }
            else {

                $("#name-of-business-office").after("<div class='validation' style='color:red;font-weight:bold;margin-bottom: 20px;'>Name Of Business Office is already exist</div>");
            }
        },
        error: function (xhr) {
            alert("An error has occured!!!");
        }
    });
    $("#name-of-business-office").next("div.validation").remove();
});

// ************** BusinessOfficePasswordPolicy **************

$(document).ready(function () {
    ClearBusinessOfficePasswordPolicyValues()
    debugger;
    // Initialising & Configuring DataTables 
    var businessOfficePasswordPolicyTable = $('#business-office-password-policy-table').DataTable({

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
        dom: '<"float-left"B><"float-right"f>rt<"row"<"col-3"l><"col-5"i><"col-3"p>>',

        buttons: [
            {
                // Add new record in datatable
                text: 'New',
                attr: {
                    id: 'btn-addNew-business-office-password-policy'
                },
                action: function (e, dt, node, config, type) {
                    debugger
                    event.preventDefault();
                    var id = $("#business-office-password-policy-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Edit', 'Add'));
                    $("#btn-update-business-office-password-policy").hide();
                    $("#btn-add-business-office-password-policy").show();
                    ClearBusinessOfficePasswordPolicyValues();
                    ClearBusinessOfficePasswordPolicyDivErrors();

                    $('#add-business-office-password-policy').modal('show');

                    var rowNum = 0;

                    $('#btn-add-business-office-password-policy').on('click', function (event) {

                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var passwordPolicyId = $("#name-of-password-policy option:selected").val();
                        var passwordPolicyIdText = $("#name-of-password-policy option:selected").text();
                        var activationDate = $("#activation-date-password-policy").val().trim();
                        var closeDate = $("#close-date-password-policy").val().trim();

                        if (passwordPolicyId.trim().length < 36 || activationDate == "") {
                            ClearBusinessOfficePasswordPolicyDivErrors();

                            if (passwordPolicyId.trim().length < 36)
                                $('#name-of-password-policy').after('<div class="error" style="color:red">Please Select Name Of Password Policy</div>');

                            if (activationDate == "")
                                $('#activation-date-password-policy').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            var row = businessOfficePasswordPolicyTable.row.add([
                                tag,
                                passwordPolicyId,
                                passwordPolicyIdText,
                                activationDate,
                                closeDate,

                            ]).draw();

                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            businessOfficePasswordPolicyTable.column(1).visible(false);
                            businessOfficePasswordPolicyTable.columns.adjust().draw();
                            ClearBusinessOfficePasswordPolicyValues();
                            ClearBusinessOfficePasswordPolicyDivErrors();

                            $('#add-business-office-password-policy').modal('hide');
                        }
                    });
                    return false;
                },
            },
            {
                // update existing record in datatable
                text: 'Edit',
                attr: {
                    id: 'btn-edit-business-office-password-policy'
                },
                // className: 'btn btn-Edit disabled',
                action: function (e, dt, node, config, String, indexes) {
                    debugger;
                    var arr = new Array();
                    $("#business-office-password-policy-table  input[type='checkbox']").each(function (index) {
                        debugger
                        var row = $(this).closest("tr");
                        selectedRow = businessOfficePasswordPolicyTable.row(row).index();
                        var tdrow = (businessOfficePasswordPolicyTable.row(selectedRow).data());
                        arr.push({
                            td0: tdrow[1]
                        });
                    });

                    var id = $("#business-office-password-policy-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Add', 'Edit'));
                    $("#btn-add-business-office-password-policy").hide();
                    $("#btn-update-business-office-password-policy").show();
                    ClearBusinessOfficePasswordPolicyDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit').data('rowindex');
                        var id = $("#add-business-office-password-policy").attr("id");
                        var myModal = $('#' + id).modal();

                        var d1 = new Date(columnValues[3]),
                            month = '' + (d1.getMonth() + 1),
                            day = '' + d1.getDate(),
                            year = d1.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var newDate = [year, month, day].join('-');

                        var d2 = new Date(columnValues[4]),
                            month = '' + (d2.getMonth() + 1),
                            day = '' + d2.getDate(),
                            year = d2.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var newDate2 = [year, month, day].join('-');

                        if (name === "Closed") {
                            $('#name-of-password-policy', myModal).val(columnValues[1]).attr('disabled', 'disabled');
                            $('#activation-date-password-policy', myModal).val(newDate).attr('disabled', 'disabled');
                            $('#close-date-password-policy', myModal).val(newDate2);
                        }

                        else if ((name === "Create") || (name === "Edit")) {
                            $('#name-of-password-policy', myModal).val(columnValues[1]);
                            $('#activation-date-password-policy', myModal).val(newDate);
                            $('#close-date-password-policy', myModal).val(newDate2);
                        }
                        myModal.modal({ show: true });
                    }
                    else {
                        $('.btn-Edit').addClass('disabled');
                        $("#add-business-office-password-policy").modal("hide");
                    }

                    arr.map(function (obj) {
                        debugger;
                        $('#name-of-password-policy').find("option[value='" + obj.td0 + "']").hide();
                    });

                    $('#btn-update-business-office-password-policy').data('rowindex', columnValues);

                    $(document).on('click', "#btn-update-business-office-password-policy", function (event) {
                        debugger;
                        $('#selectAll').prop('checked', false);
                        colorchange();

                        // Validation
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var passwordPolicyId = $("#name-of-password-policy option:selected").val();
                        var passwordPolicyIdText = $("#name-of-password-policy option:selected").text();
                        var activationDate = $("#activation-date-password-policy").val().trim();
                        var closeDate = $("#close-date-password-policy").val().trim();

                        if (passwordPolicyId.trim().length < 36 || activationDate == "") {
                            ClearBusinessOfficePasswordPolicyDivErrors();

                            if (passwordPolicyId.trim().length < 36)
                                $('#name-of-password-policy').after('<div class="error" style="color:red">Please Select Name Of Password Policy</div>');

                            if (activationDate == "")
                                $('#activation-date-password-policy').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            businessOfficePasswordPolicyTable.row($(this).attr('rowindex')).data([

                                '<input type="checkbox" name="check_all" class="checks"/>',
                                $('#name-of-password-policy option:selected').val(),
                                $('#name-of-password-policy option:selected').text(),
                                $('#activation-date-password-policy').val(),
                                $('#close-date-password-policy').val(),

                            ]).draw();

                            var columnValues = $('#btn-update-business-office-password-policy').data('rowindex');
                            FromShowPasswordPolicyValues(columnValues);

                            businessOfficePasswordPolicyTable.column(1).visible(false);
                            businessOfficePasswordPolicyTable.columns.adjust().draw();
                            $("#add-business-office-password-policy").modal('hide');
                            $('.btn-add-business-office-password-policy').removeClass('disabled');
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
                    id: 'btn-delete-business-office-password-policy'
                },

                action: function (e, dt, node, config) {
                    debugger;
                    var isChecked = $("input[type='checkbox']").is(":checked");
                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {
                            debugger;
                            if ($("input[type='checkbox']:checked").each(function () {
                                debugger;
                                businessOfficePasswordPolicyTable.row($("input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                                var arr = $('.btn-Delete').data('rowindex');
                                arr.map(function (obj) {
                                    debugger;
                                    $('#name-of-password-policy').find("option[value='" + obj.td0 + "']").show();
                                    $("#name-of-password-policy").prop("selectedIndex", 0);
                                });
                                $('.btn-add-business-office-password-policy').removeClass('disabled');
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
        var passwordPolicies = $("#name-of-password-policy option:selected").val();
        $('#name-of-password-policy').find("option[value='" + passwordPolicies + "']").show();
        $("#name-of-password-policy").prop("selectedIndex", 0);
    });

    // To dropdown added values by edit
    function FromShowPasswordPolicyValues(columnValues) {
        debugger;
        $('#name-of-password-policy').find("option[value='" + columnValues[1] + "']").show();
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

    var btns = $('#btn-addNew-business-office-password-policy');
    btns.addClass('btn btn-success  btn-add-business-office-password-policy').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-business-office-password-policy');
    btns.addClass('btn btn-primary btn-Edit disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-delete-business-office-password-policy');
    btns.addClass('btn btn-danger btn-Delete DeleteAll disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $('#selectAll').on('click', function () {
        debugger;
        if ($(this).prop('checked')) {
            if (name === "Create") {
                $(this).prop('checked', true);
                var arr = new Array();
                $('#business-office-password-policy-table tbody input[type="checkbox"]').each(function () {
                    $(this).prop('checked', true);
                    var row = $(this).closest("tr");
                    selectedRow = businessOfficePasswordPolicyTable.row(row).index();
                    var tdrow = (businessOfficePasswordPolicyTable.row(selectedRow).data());
                    arr.push({
                        td0: tdrow[1]
                    });

                    $('.btn-Delete').data('rowindex', arr);
                    $('.btn-add-business-office-password-policy').addClass('disabled');
                    $('.btn-Edit').addClass('disabled');
                    $('.btn-Delete').removeClass('disabled');
                });
            }
            // if name=Closed
            else {
                $('.btn-Edit').addClass('disabled');
                $('.btn-Delete').addClass('disabled');
                $('.btn-add-business-office-password-policy').addClass('disabled');
            }

        }
        else {
            $('#business-office-password-policy-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-business-office-password-policy').removeClass('disabled');
                $('.btn-Delete').addClass('disabled');
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
        $('.btn-add-business-office-password-policy').attr('disabled', true);
    }

    // binding the change event-handler to the tbody:
    $('#business-office-password-policy-table tbody').on('change', function () {
        debugger;

        // getting all the checkboxes within the tbody:
        var all = $('tbody input[type="checkbox"]'),
            // getting only the checked checkboxes from that collection:
            checked = all.filter(':checked');
        if (name === "Closed") {
            debugger;
            if (all.length > 0 == checked.length) {
                debugger;
                $('.btn-Delete').addClass('disabled');

                $('.btn-Edit').removeClass('disabled');
            }
            else {
                $('.btn-Edit').addClass('disabled');

            }
            if (checked.length == 0) {
                $('.btn-Delete').addClass('disabled');
                $('.btn-add-business-office-password-policy').removeClass('disabled');
            }
        }

        //  if (name === "Create") 
        else {
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
                $('.btn-add-business-office-password-policy').removeClass('disabled');
            }
        }

        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#selectAll').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    $('#business-office-password-policy-table tbody').on('click', "input[type=checkbox]", function () {
        $('#business-office-password-policy-table input[type="checkbox"]:checked').each(function () {
            var isChecked = $(this).prop("checked");

            if (isChecked) {
                var arr = new Array();
                $("input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    selectedRow = businessOfficePasswordPolicyTable.row(row).index();
                    var tdrow = (businessOfficePasswordPolicyTable.row(selectedRow).data());
                    arr.push({
                        td0: tdrow[1]
                    });

                    $('.btn-add-business-office-password-policy').addClass('disabled');
                    $('.btn-Edit').removeClass('disabled');
                    if (name === "Closed") {
                        $('.btn-Delete').addClass('disabled');
                    }
                    else {
                        $('.btn-Delete').removeClass('disabled');
                    }
                    $('#btn-update-business-office-password-policy').attr('rowindex', selectedRow);
                    $('.btn-Edit').data('rowindex', tdrow);
                    $('.btn-Delete').data('rowindex', arr);
                    $('#selectAll').data('rowindex', arr);
                });
            }
        });
    });

    // To page load table each row get value & dropdown value Hide 
    $('#business-office-password-policy-table > tbody > tr').each(function () {
        debugger;
        var currentRow = $(this).closest("tr");
        var columnvalue = (businessOfficePasswordPolicyTable.row(currentRow).data());
        if (typeof columnvalue != 'undefined' && columnvalue != null) {
            $('#name-of-password-policy').find("option[value='" + columnvalue[1] + "']").hide();
        }
        else {
            return true;
        }

    });

    // To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    function ClearBusinessOfficePasswordPolicyValues() {
        debugger;

        var passwordPolicies = $("#name-of-password-policy option:selected").val();
        $('#name-of-password-policy').find("option[value='" + passwordPolicies + "']").hide();
        $("#name-of-password-policy").prop("selectedIndex", 0);
        $("#name-of-password-policy").val('');
        $("#activation-date-password-policy").val('');
        $("#close-date-password-policy").val('');
    }

    // Clear DataTable Inputs
    function ClearBusinessOfficePasswordPolicyDivErrors() {
        debugger;
        $('#name-of-password-policy').next("div.error").remove();
        $("#activation-date-password-policy").next("div.error").remove();
        $("#close-date-password-policy").next("div.error").remove();
    }

    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger
        // not add event.preventDefault
        $("#lastrow").remove();

        // Return List Object, Hence Create Array
        var BusinessOfficePasswordPolicy = new Array();

        // Recursive Loop By Row
        $("#business-office-password-policy-table TBODY TR").each(function () {
            debugger
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (businessOfficePasswordPolicyTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                return false;
            }

            var passwordPoliciesId = columnvalue[1];
            var activationDates = columnvalue[3];
            var closeDates = columnvalue[4];

            BusinessOfficePasswordPolicy.push({
                "PasswordPolicyId": passwordPoliciesId,
                "ActivationDate": activationDates,
                "CloseDate": closeDates,
            });
        });

        // Call Controller Save Method 
        $.ajax({
            url: url,
            type: 'POST',
            data: { '_businessOfficePasswordPolicy': BusinessOfficePasswordPolicy },
            ContentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (data) {
            },

            error: function (xhr, status, error) {
                alert("An Error Has Occured In BusinessOfficePasswordPolicies DataTable!!! Error Message - " + error.toString());
            }
        })
    });
});