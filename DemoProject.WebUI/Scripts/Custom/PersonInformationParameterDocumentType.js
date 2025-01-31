$(document).ready(function () {
    debugger

    ClearDocumentTypeValues()

    // Initialising & Configuring DataTables 
    var documentTypeTable = $('#person-information-parameter-document-type').DataTable({

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
            { orderable: false, targets: 5 }
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
                    id: 'btn-addNew-document'
                },
                action: function (e, dt, node, config, type) {
                    debugger
                    event.preventDefault();

                    var id = $("#document-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Edit', 'Add'));
                    $("#btn-update-document").hide();
                    $("#btn-add-document").show();
                    ClearDocumentTypeValues();
                    ClearDocumentTypeDivErrors();

                    $('#add-document').modal('show');

                    var rowNum = 0;

                    $('#btn-add-document').on('click', function (event) {
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var documentId = $("#document-type-id option:selected").val();
                        var documentIdText = $("#document-type-id option:selected").text();
                        var isMandatory = $('input[name="IsMandatory"]').is(':checked') ? "True" : "False";
                        var activationDate = $("#activation-date").val();
                        var note = $("#note").val();

                        if ((documentId.trim().length < 36) || (activationDate == "")) {

                            ClearDocumentTypeDivErrors();

                            if (documentId.trim().length < 36)
                                $('#document-type-id').after('<div class="error" style="color:red">Please Select Document Type </div>');

                            if (activationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            var row = documentTypeTable.row.add([
                                tag,
                                documentId,
                                documentIdText,
                                isMandatory,
                                activationDate,
                                note,

                            ]).draw();

                            rowNum++;
                            row.nodes().to$().attr('id', 'tr' + rowNum);
                            documentTypeTable.column(1).visible(false);
                            documentTypeTable.columns.adjust().draw();
                            ClearDocumentTypeValues();
                            ClearDocumentTypeDivErrors();
                            $('#add-document').modal('hide');
                        }
                    });
                    return false;
                },
            },
            {
                text: 'Edit',
                attr: {
                    id: 'btn-edit-document'
                },

                // className: 'btn btn-Edit disabled',
                action: function (e, dt, node, config, String, indexes) {
                    debugger

                    var arr = new Array();
                    $("#person-information-parameter-document-type  input[type='checkbox']").each(function (index) {
                        debugger
                        var row = $(this).closest("tr");
                        selectedRow = documentTypeTable.row(row).index();
                        var tdrow = (documentTypeTable.row(selectedRow).data());
                        arr.push({
                            td0: tdrow[1]
                        });
                    });

                    var id = $("#document-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Add', 'Edit'));
                    $("#btn-add-document").hide();
                    $("#btn-update-document").show();
                    ClearDocumentTypeDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {

                        var columnValues = $('.btn-Edit').data('rowindex');
                        var id = $("#add-document").attr("id");
                        var myModal = $('#' + id).modal();
                        var d = new Date(columnValues[4]),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();
                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;
                        var newDate = [year, month, day].join('-');

                        $('#document-type-id', myModal).val(columnValues[1]);
                        if (columnValues[3] === "True") {
                            $("#is-mandatory").prop("checked", true);
                        }
                        else {
                            $("#is-mandatory").prop("checked", false);
                        }
                        $('#activation-date', myModal).val(newDate);
                        $('#note', myModal).val(columnValues[5]);

                        myModal.modal({ show: true });
                    }

                    else {
                        $('.btn-Edit').addClass('disabled');
                        $("#add-document").modal("hide");
                    }

                    arr.map(function (obj) {
                        debugger;
                        $('#document-type-id').find("option[value='" + obj.td0 + "']").hide();
                    });
                    $('#btn-update-document').data('rowindex', columnValues);

                    $(document).on('click', "#btn-update-document", function (event) {
                        debugger;
                        $('#select-all-document').prop('checked', false);
                        colorchange();
                        var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                        var documentId = $('#document-type-id option:selected').val();
                        var documentIdText = $('#document-type-id option:selected').text();
                        var isMandatory = $('input[name="IsMandatory"]').is(':checked') ? "True" : "False";
                        var activationDate = $('#activation-date').val();
                        var note = $("#note").val();

                        var columnValues = $('#btn-update-document').data('rowindex');

                        if ((documentId.trim().length < 36) || (activationDate == "")) {

                            ClearDocumentTypeDivErrors();

                            if (documentId.trim().length < 36)
                                $('#document-type-id').after('<div class="error" style="color:red">Please Select Document Type </div>');

                            if (activationDate == "")
                                $('#activation-date').after('<div class="error" style="color:red">Please Enter Activation Date</div>');

                            return false;
                        }
                        else {
                            documentTypeTable.row($(this).attr('rowindex')).data([
                                tag,
                                documentId,
                                documentIdText,
                                isMandatory,
                                activationDate,
                                note,

                            ]).draw();

                            documentTypeTable.column(1).visible(false);
                            documentTypeTable.columns.adjust().draw();

                            var columnValues = $('#btn-update-document').data('rowindex');
                            FromShowDocumentValues(columnValues);

                            $('#add-document').modal('hide');
                            $('.btn-add-document').removeClass('disabled');
                            $('.btn-Delete').addClass('disabled');
                            $('.btn-Edit').addClass('disabled');
                        }
                    })
                },
            },

            {
                text: 'Delete',
                attr: {
                    id: 'btn-Delete-document'
                },

                action: function (e, dt, node, config) {
                    debugger;
                    var isChecked = $("input[type='checkbox']").is(":checked");
                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {
                            debugger;
                            if ($("#person-information-parameter-document-type tbody input[type='checkbox']:checked").each(function () {
                                debugger;
                                documentTypeTable.row($("input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                                var arr = $('.btn-Delete').data('rowindex');
                                arr.map(function (obj) {
                                    debugger;
                                    $('#document-type-id').find("option[value='" + obj.td0 + "']").show();
                                    $("#document-type-id").prop("selectedIndex", 0);
                                });
                                $('.btn-add-document').removeClass('disabled');
                                $('.btn-Delete').addClass('disabled');
                                $('.btn-Edit').addClass('disabled');
                                $('#select-all-document').prop('checked', false);
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
        ClearDocumentTypeDivErrors();
        //ClearDocumentTypeValues();
        $('.btn-Delete').addClass('disabled');
        $('.btn-Edit').addClass('disabled');
        $('.checks').prop('checked', false);
        $('.btn-add-document').removeClass('disabled');
        $('#select-all-document').prop('checked', false);

        var documentTypes = $("#document-type-id option:selected").val();
        $('#document-type-id').find("option[value='" + documentTypes + "']").show();
        $("#document-type-id").prop("selectedIndex", 0);
    });

    // To dropdown added values by edit
    function FromShowDocumentValues(columnValues) {
        debugger;
        $('#document-type-id').find("option[value='" + columnValues[1] + "']").show();
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

    var btns = $('#btn-addNew-document');
    btns.addClass('btn btn-success  btn-add-document').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-document');
    btns.addClass('btn btn-primary btn-Edit disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-Delete-document');
    btns.addClass('btn btn-danger btn-Delete DeleteAll disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $('#select-all-document').on('click', function () {
        debugger;
        debugger;
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);
            var arr = new Array();
            $('#person-information-parameter-document-type tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                var row = $(this).closest("tr");
                selectedRow = documentTypeTable.row(row).index();
                var tdrow = (documentTypeTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-Delete').data('rowindex', arr);
                $('.btn-add-document').addClass('disabled');
                $('.btn-Edit').addClass('disabled');
                $('.btn-Delete').removeClass('disabled');
            });
        }
        else {
            $('#person-information-parameter-document-type tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-document').removeClass('disabled');
                $('.btn-Delete').addClass('disabled');
            });
        }
    });

    // binding the change event-handler to the tbody:
    $('#person-information-parameter-document-type tbody').on('change', function () {

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
            $('.btn-add-document').removeClass('disabled');
        }
        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#select-all-document').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    $('#person-information-parameter-document-type tbody').on('click', "input[type=checkbox]", function () {
        $('#person-information-parameter-document-type input[type="checkbox"]:checked').each(function () {

            var isChecked = $(this).prop("checked");

            if (isChecked) {

                var arr = new Array();
                $("#person-information-parameter-document-type tbody input[type='checkbox']:checked").each(function (index) {
                    debugger
                    var row = $(this).closest("tr");
                    var selectedRow = documentTypeTable.row(row).index();
                    var tdrow = documentTypeTable.row(selectedRow).data();
                    arr.push({
                        td0: tdrow[1]
                    });

                    $('.btn-add-document').addClass('disabled');
                    $('.btn-Edit').removeClass('disabled');
                    $('.btn-Delete').removeClass('disabled');

                    $('#btn-update-document').attr('rowindex', selectedRow);
                    $('.btn-Edit').data('rowindex', tdrow);
                    $('.btn-Delete').data('rowindex', arr);
                    $('#select-all-document').data('rowindex', arr);
                });
            }
        });
    });

    // To page load table each row get value & dropdown value Hide 
    $('#person-information-parameter-document-type > tbody > tr').each(function () {
        debugger;

        var currentRow = $(this).closest("tr");
        var columnvalue = (documentTypeTable.row(currentRow).data());
        if (typeof columnvalue != 'undefined' && columnvalue != null) {

            $('#document-type-id').find("option[value='" + columnvalue[1] + "']").hide();
        }
        else {
            return true;
        }
    });

    // To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    function ClearDocumentTypeValues() {
        debugger
        var documentTypes = $("#document-type-id option:selected").val();
        $('#document-type-id').find("option[value='" + documentTypes + "']").hide();
        $("#document-type-id").prop("selectedIndex", 0);

        $("#document-type-id").val('');
        $('input[name="IsMandatory"]').prop('checked', false);
        $("#activation-date").val('');
        $("#note").val('');
    }

    // Clear Div Errors
    function ClearDocumentTypeDivErrors() {
        debugger
        $('#document-type-id').next("div.error").remove();
        $('#activation-date').next("div.error").remove();
    }

    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger
        //not add event.preventDefault
        $(".lastrow").remove();

        // Return List Object, Hence Create Array
        var PersonInformationParameterDocumentType = new Array();

        // Recursive Loop By Row
        $("#person-information-parameter-document-type TBODY TR").each(function () {
            debugger
            var currentRow = $(this).closest("tr");

            var columnvalue = columnvalue = (documentTypeTable.row(currentRow).data());

            // Handling Code If Row Is Undefined Or Null
            if (typeof columnvalue == 'undefined' && columnvalue == null) {
                return false;
            }

            var DocumentTypeId = columnvalue[1];
            var IsMandatory = columnvalue[3];
            var ActivationDate = columnvalue[4];
            var Note = columnvalue[5];

            PersonInformationParameterDocumentType.push({
                "DocumentTypeId": DocumentTypeId,
                "IsMandatory": IsMandatory,
                "ActivationDate": ActivationDate,
                "Note": Note,
            });
        });

        // Call Controller Save Method 
        $.ajax({
            url: url,
            type: 'POST',
            data: { '_personInformationParameterDocumentType': PersonInformationParameterDocumentType },
            ContentType: "application/json; charset=utf-8",
            dataType: "JSON",

            success: function (data) {
            },

            error: function (xhr, status, error) {
                alert("An Error Has Occured In Document Details DataTable!!! Error Message - " + error.toString());
            }
        })
    });
});