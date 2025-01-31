//DataTable
var creditDataTable = null;
var debitDataTable = null;
var denominationDataTable = null;
$(function (e) {
    jQuery("#credit-amount").attr('data-id', '0');
    jQuery("#debit-amount").attr('data-id', '0');
    jQuery("#total-amount").attr('data-id', '0');
    jQuery("#badge-amounts").attr('data-id', '100000');
    // 1  - C  R  E  D  I  T 
    // Clear Credit Modal Input Values
    ClearCreditModalInputs();
    ClearCreditDivErrors();
    // Initialising & Configuring DataTables 
    creditDataTable = $('#credit-data-table').DataTable({
        "deferRender": true,
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
        "aoColumnDefs": [{ "sClass": "d-none", "aTargets": [15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32] }],
        "language": {
            "lengthMenu": "_MENU_"
        },
        //"rowsGroup": [2],

        "columnDefs": [
           { orderable: false, targets: 0 }
        ],
        "columnDefs": [{
            targets: 17,
            className: 'text-nowrap'
        }],
        // "recordsTotal": 100,
        //"displayStart": (3 - 1) * 10,
        // to limit records
        //pageLength: 5,
        //recordsdisplay:2,
        //recordsTotal: 50,
        //columnDefs:
        //        [{
        //            targets: 7,
        //            "render": function (data, type, full, meta, row, indexes) {
        //                ;
        //                var total = ((full[7]));
        //                full[7] = ("" + total + "");
        //                //for (i = 0; i < full.length; i++) {
        //                
        //                //var items = [];
        //                ////var newItems = new Array(1000000);
        //                //for (var i = 0; i < full.length; i++) {
        //                //    items.push(full[i]);
        //                //}

        //                alert("ok")
        //                //debitDataTable.cell(0, 7).data(data).draw();

        //                return total
        //                ///}
        //                // return total;
        //            }
        //        }],
        //"drawCallback": function (settings, start, end, max, total, pre) {
        //    ;
        //    //console.log(this.fnSettings().json); /* for json response you can use it also*/
        //    alert(this.fnSettings().fnRecordsTotal()); // total number of rows
        //},
        //"initComplete": function (settings, json) {
        //    ;
        //    if (this.api().page.info().pages === 1) {
        //        $('#credit-data-table_paginate').hide();
        //    }
        //    else {
        //        $('#credit-data-table_paginate').show();
        //    }
        //    var info = this.api().page.info();
        //    var tt = info.recordsTotal = 4;
        //    var rr = info.recordsDisplay = 2;
        //    //var info = this.api().page.info();
        //    //var tt = info.recordsTotal = 4;

        //    //console.log('Total records', info.recordsTotal);
        //},

        //columnDefs:
        //      [{
        //          targets:[9],
        //          "render": function (data, type, full, meta, row, indexes) {
        //              debugger;
        //             // var transactionamount = parseFloat($("#credit-amount").attr("data-id"));
        //              var transactiontype = $("#transaction-type-id option:selected").val();
        //              total = (full[15]) * (full[16]);
        //              //total = (full[15]) * (full[16]);
        //             // if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {

        //                  full[9] = ("" + total + "");
        //                  return total;
        //             // }
        //              //else {

        //              //    full[4] = ("" + total + "");
        //              //    return total;
        //              //}
        //          }
        //      }],
        columnDefs: [{
            orderable: false,
            className: 'select-checkbox',
            targets: 0
        }],
        
        order: [[2, 'desc']],
        dom: 'Bfrtip',
        dom: '<"float-left"B><"float-right"f>rt<"row"<"col-sm-4"l><"col-sm-4"i><"col-sm-4"p>>',
        //buttons
        buttons: [
             //NEW OPERATION
            {

                attr: {
                    id: 'btn-add-credit-dt'
                },
                action: function (e, dt, node, config, type) {
                    event.preventDefault();
                    $('#credit-modal').find('#credit-fd').html('');
                    var id = $("#credit-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Edit', 'Add'));
                    $("#btn-update-credit-modal").hide();
                    $("#btn-add-credit-modal").show();
                    ClearCreditModalInputs();
                    ClearCreditDivErrors();
                    //$('input[name="amount[]"]').on('input', function () {
                    //    //$("#input[name='amount[]']").change(function (idx, ele) {
                    //    debugger;
                    //    alert("OK");

                    //})
                    var txt = "";
                    //var array = $('input[id^=credit-]').map(function (i, ele) {
                    //    return $(ele).val();
                    //}).get()
                    //txt += "Being " + array[2] + " Shares Sold @ " + array[1] + " Rupee To Mr.Ramesh and Receiving " + array[4] + " Admission Fee And " + array[7] + " Building Fund.";
                    //$("#credit-modal").find($('#credit-narration').val(txt));
                    //for (var i = 0; i < array2.length; i++) {
                    //$.each(array2, function (index, item) {
             
                    $(document).on("change", "input[id^=credit-]", function () {
                        var  arr = 0;
                        txt = "";
                        var text="";
                        
                        ///var personText = $("#credit-person-id").data("ui-autocomplete").selectedItem.label;

                        var nameofGL = $('label[id^=credit-]').map(function (i, ele) {
                            return $(ele).html();
                        }).get();

                        array = $('input[id^=credit-]').map(function (i, ele) {
                            return $(ele).val();
                        }).get();

                        arr = $("input[name='gl[]']").map(function (idx, ele) {
                            return $(ele).val().trim().length == 0 ? 0 : parseFloat($(ele).val().trim());
                        }).get();
                        
                        var personText = array[0].split("-->");
                       
                        //var BuildingFund = "";
                        var admissionfee = "";
                        var stationaryfee = "";
                        var buildingfund = "";
                        var reservefund = "";

                        // $.each(array, function (index, item) {
                        //debugger
                        //     if(parseInt(item) > 0)
                        
                        //     admissionfee += '' + array[2] + ' Admission Fee And ';
                        //     stationaryfee += '' + array[3] + 'Stationary Fee And ';
                        //     buildingfund += '' + array[4] + ' Building Fund And ';
                        //     reservefund += '' + array[5] + ' Reserve Fund ';

                        //     //var index1 = array.findIndex(x => x.age === tt);
                               
                        // })
                        //var tt1 = array.indexOf(array);

                        if (arr[0] > 0 || nameofGL[0] > 0) {
                            debugger;
                            admissionfee += '' + arr[0] + ' ' + nameofGL[0] + ' And Rs';
                        }
                        if (arr[1] > 0) {
                            debugger;
                            stationaryfee += '' + arr[1] + ' ' + nameofGL[1] + ' And Rs';
                        }
                        if (arr[2] > 0) {
                            debugger;
                            buildingfund += '' + arr[2] + ' ' + nameofGL[2] + ' And Rs';
                        }
                        if (arr[3] > 0) {
                            debugger;
                            reservefund += '' + arr[3] + ' ' + nameofGL[3] + ' And Rs';
                        }
                          
                        if (arr.length === 0 || arr[0] === 0) {

                            txt += "Being " + array[2] + " Shares Sold @ Face Value RS " + array[1].trim() + " To Mr. " + personText[0].trim() + " And Receiving Rs " + admissionfee.trim() + " " + stationaryfee.trim() + " " + buildingfund.trim() + " " + reservefund.trim() + "";

                            text += txt.slice(0, -20);
                        }
                        else {
                            txt += "Being " + array[2] + " Shares Sold @ Face Value RS " + array[1].trim() + " To Mr. " + personText[0].trim() + " And Receiving Rs " + admissionfee.trim() + " " + stationaryfee.trim() + " " + buildingfund.trim() + " " + reservefund.trim() + "";

                            text += txt.slice(0, -9);
                        }
                      
                        $("#credit-modal").find($('#credit-narration').val(text));
                    })
                
                    //var BuildingFund = "";
                    //var admissionfee = "";
                    //var stationaryfee = "";
                    //var buildingfund = "";
                    //var reservefund = "";

                    // $.each(array, function (index, item) {
                    //debugger
                    //     if(parseInt(item) > 0)

                    //     admissionfee += '' + array[2] + ' Admission Fee And ';
                    //     stationaryfee += '' + array[3] + 'Stationary Fee And ';
                    //     buildingfund += '' + array[4] + ' Building Fund And ';
                    //     reservefund += '' + array[5] + ' Reserve Fund ';

                    //     //var index1 = array.findIndex(x => x.age === tt);

                    // })
                    //var tt1 = array.indexOf(array);
                       
                    //    if (array[3] > 0 || nameofGL[0] > 0) {
                    //        debugger;
                    //        admissionfee += '' + array[3] + ' ' + nameofGL[0] + ' And Rs';
                    //    }
                    //    if (array[4] > 0 || nameofGL[1] > 0) {
                    //        debugger;
                    //        stationaryfee += '' + array[4] + ' ' + nameofGL[1] + ' And Rs';
                    //    }
                    //    if (array[5] > 0 || nameofGL[2] > 0) {
                    //        debugger;
                    //        buildingfund += '' + array[5] + ' ' + nameofGL[2] + ' And Rs';
                    //    }
                    //    //if (array[6] > 0 || nameofGL[3] > 0) {
                    //    //    debugger;
                    //    //    reservefund += '' + array[6] + ' Reserve Fund.';
                    //    //}

                    //    txt += "Being " + array[2] + " Shares Sold @ Face Value Rs " + array[1].trim() + " To Mr. " + personText[0].trim() + " And Receiving Rs " + admissionfee.trim() + " " + stationaryfee.trim() + " " + buildingfund.trim() + " " + reservefund.trim() + "";


                    //    $("#credit-modal").find($('#credit-narration').val(txt));
                    //}
                        
                        
                    //})
                    

                    //$('input[id^=credit-]').change(function (i, v) {
                    //    debugger;
                    //    //array = [];
                    //    var txt1 = "";
                    //    var val = $(this).val();
                    //    array.push(val);
                    //    txt1 += "Being " + val + " Shares Sold To Mr.Ramesh By Receiving " + array[4] + " Admission Fee And " + array[7] + " Building Fund.";
                    //    $("#credit-modal").find($('#credit-narration').val(txt1));
                    //})

                    //var array2 = $('input[id^=credit-]').map(function (i, v) {
                    //    return v.id;
                    //}).get()
                    //$("#" + array2).on("change", function () {
                    //    debugger;
                    //})


                    //alert(array[1]);
                    //var rr = new Array();
                    //var arr = ["credit-number-of-shares-error", "credit-entry-fees", "credit-stationary", "credit-building-fund", "credit-reserve-fund"].values;

                    ///$.each(arr, function (index, item) {
                   
                    //rr.push("#" + item);


                    $('#credit-data-table > tbody > tr').each(function () {
                        var currentRow = $(this).closest("tr");
                        var columnvalue = (creditDataTable.row(currentRow).data());
                        if (typeof columnvalue != 'undefined' && columnvalue != null) {

                            $('#person-id').find("option[value='" + columnvalue[1] + "']").hide();
                        }
                        else {
                            return true;
                        }

                    });

                    $('#credit-modal').modal('show');

                    $('#btn-add-credit-modal').on('click', function (event) {
                        debugger;
                        var balance = $("#badge-amount").data("amount");

                        var count = parseFloat($("#badge-text").html());
                        //var generalledgerprmkey = $("#general-Ledger-PrmKey").data("generalledgerprmkey");
                        //var transactionAmount = $("#credit-modal input[name='amount[]']").map(function (idx, ele) {
                        //    return $(ele).val().trim().length == 0 ? 0 : parseFloat($(ele).val().trim());
                        //}).get();
                        var nameofGLprmkey = $('label[id^=general-]').map(function (i, ele) {
                            return $(ele).html();
                        }).get();

                        var nameofGL = $("#credit-modal input[name='gl[]']").map(function (idx, ele) {

                            return $(ele).val();
                        }).get();
                       
                        var nameofGLprmkey1 = "";
                        var nameofGLprmkey2 = "";
                        var nameofGLprmkey3 = "";
                        var nameofGLprmkey4 = "";
                        var nameofGLprmkey5 = "";

                        if (nameofGLprmkey[0] > 0) {
                            nameofGLprmkey1 += '' + nameofGLprmkey[0];
                        }
                        if (nameofGLprmkey[1] > 0) {
                            nameofGLprmkey2 += '' + nameofGLprmkey[1];
                        }
                        if (nameofGLprmkey[2] > 0) {
                            nameofGLprmkey3 += '' + nameofGLprmkey[2];
                        }
                        if (nameofGLprmkey[3] > 0) {
                            nameofGLprmkey4 += '' + nameofGLprmkey[3];
                        }
                        if (nameofGLprmkey[4] > 0) {
                            nameofGLprmkey5 += '' + nameofGLprmkey[4];
                        }
                        var admissionfee =  "";
                        var stationaryfee = "";
                        var buildingfund = "";
                        var reservefund = "";
                        var admissionfee2 = "";
                        var admissionfee3 = "";
                        if (nameofGL[0] > 0)
                        {
                            admissionfee += '' + nameofGL[0];
                        }
                        if (nameofGL[1] > 0) {
                            stationaryfee += '' + nameofGL[1];
                        }

                        if (nameofGL[2] > 0) {
                            buildingfund += '' + nameofGL[2];
                        }
                        if (nameofGL[3] > 0) {
                            reservefund += '' + nameofGL[3];
                        }
                        if (nameofGL[4] > 0) {
                            admissionfee2 += '' + nameofGL[4];
                        }
                       
                        var checkbox = '<input type="checkbox" name="check_all" class="checks"/>';
                        var personId = $("#credit-person-id").data("ui-autocomplete").selectedItem.value;
                        var personText = $("#credit-person-id").data("ui-autocomplete").selectedItem.label;
                        var businessOfficeId = $("#credit-businessOffice-id option:selected").val();
                        var businessOfficeText = $("#credit-businessOffice-id option:selected").text();
                        var generalLedgerId = $("#credit-general-ledger-id option:selected").val();
                        var generalLedgerText = $("#credit-general-ledger-id option:selected").text();
                        var customerAccountId = $("#credit-customer-account-id option:selected").val();
                        var customerAccountIdText = $("#credit-customer-account-id option:selected").text();
                        var sharesfacevalue = $("#credit-shares-face-value").val();
                        var numberofshares = $("#credit-number-of-shares").val();
                        var transactionAmount = parseFloat($("#credit-transaction-amount").val());
                        var startCertificateNumber = $("#credit-start-certificate-number").val() ? $("#credit-start-certificate-number").val() : 0;
                        var endCertificateNumber = $("#credit-end-certificate-number").val() ? $("#credit-end-certificate-number").val() : 0;
                        var note = $("#credit-note").val().trim();
                        var narration = $("#credit-narration").val().trim();
                        var min = parseFloat($('#credit-transaction-amount').attr('min'));
                        var max = parseFloat($('#credit-transaction-amount').attr('max'));
                        
                        if (transactionAmount >= min && transactionAmount <= max) {

                        } else {
                            ClearCreditDivErrors();
                            $('#credit-transaction-amount').after('<div class="error" style="color:red">Please enter a value greater than or less than or equal to ' + min + ' and  ' + max + ' </div>');
                            return false
                        }
                        if (personId.length < 36 || (generalLedgerId.length < 36) || (customerAccountId.length < 36) || (startCertificateNumber == 0) || (endCertificateNumber == 0) || (narration == "")) {
                            ClearCreditDivErrors();

                            if (personId.trim().length < 36)
                                $('#person-id').after('<div class="error" style="color:red">Please Select Customer Name </div>');

                            if (generalLedgerId.trim().length < 36)
                                $('#general-ledger-id').after('<div class="error" style="color:red">Please Select General Ledger </div>');

                            if (customerAccountId.trim().length < 36)
                                $('#customer-account-id').after('<div class="error" style="color:red">Please Select Customer Aaccount </div>');

                            if (transactionAmount == "")
                                $('#transaction-amount').after('<div class="error" style="color:red">Please Select Transaction Amount </div>');

                            if (startCertificateNumber == "")
                                $('#credit-start-certificate-number').after('<div class="error" style="color:red">Please Enter Start Number</div>');

                            if (endCertificateNumber == "")
                                $('#credit-end-certificate-number').after('<div class="error" style="color:red">Please Enter End Number</div>');
                            
                            return false;
                        }
                        else {
                            //var tt1 = creditDataTable.rows().data().toArray().length = 2;
                            //var data = creditDataTable.rows().data().toArray();
                            var dataCount = creditDataTable.rows(':contains("' + personText + '")').data().length;

                            //var tableData = $('table tbody td').map(function () {
                            // var input = $('input', this);
                            //  return input.length > 0 ? input.val() : $(this).text();
                            //}).get();
                            //var index = creditDataTable.rows().length = 3;
                            //creditDataTable.page(1).draw();
                            //creditDataTable.page.len(1).draw();
                            //if (index < 2) {
                            //if (2 < 4) {
                            // if (tt1 < tt) {
                            //for (var count of 20) {
                            //if (transactionAmount < balance) {
                            //for (var i = 0; i < transactionAmount.length; i++) {
                            //if (dataCount < count) {
                                var row = creditDataTable.row.add([
                                     checkbox,
                                     personId,
                                     personText,
                                     businessOfficeId,
                                     businessOfficeText,
                                     generalLedgerId,
                                     generalLedgerText,
                                     customerAccountId,
                                     customerAccountIdText,
                                     "",
                                     "",
                                     "0",
                                     "0",
                                     "0",
                                     "0",
                                     "0",
                                     sharesfacevalue,
                                     numberofshares,
                                     //transactionAmount[i],
                                     transactionAmount,
                                     //fee[i],
                                     //generalledgerprmkey,
                                     startCertificateNumber,
                                     endCertificateNumber,
                                     note,
                                     narration,
                                     nameofGLprmkey1,
                                     nameofGLprmkey2,
                                     nameofGLprmkey3,
                                     nameofGLprmkey4,
                                     nameofGLprmkey5,
                                     admissionfee,
                                     stationaryfee,
                                     buildingfund,
                                     reservefund,
                                     admissionfee2
                                ]).draw();
                            //}
                            creditDataTable.column(1).visible(false);
                            creditDataTable.column(3).visible(false);
                            creditDataTable.column(5).visible(false);
                            creditDataTable.column(7).visible(false);
                            //creditDataTable.column(14).visible(false);
                            creditDataTable.column(16).visible(false);
                            creditDataTable.column(15).visible(false);
                            creditDataTable.column(17).visible(false);
                            creditDataTable.column(18).visible(false);
                            creditDataTable.column(19).visible(false);
                            creditDataTable.column(20).visible(false);
                            creditDataTable.column(21).visible(false);
                            creditDataTable.column(22).visible(false);
                            creditDataTable.column(23).visible(false);
                            creditDataTable.column(24).visible(false);
                            creditDataTable.column(25).visible(false);
                            creditDataTable.column(26).visible(false);
                            creditDataTable.column(27).visible(false);
                            creditDataTable.column(28).visible(false);
                            creditDataTable.column(29).visible(false);
                            creditDataTable.column(30).visible(false);
                            creditDataTable.column(31).visible(false);
                            creditDataTable.column(32).visible(false);


                            var transactiontype = $("#transaction-type-id option:selected").val();

                            if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {
                                $('#debit-data-table > tbody > tr').each(function (index) {
                                    debugger;
                                    var currentRow = $(this).closest('tr');
                                    var columnvalue = (debitDataTable.row(currentRow).data());
                                    if (typeof columnvalue != 'undefined' && columnvalue != null) {

                                        debitDataTable.cell(0, 9).data(total);
                                        $('td', currentRow).eq(0).find(".checks").attr("disabled", true);
                                        $('th', currentRow).find("#select-all-debit").attr("disabled", true);
                                        $('#debit-amount').attr("data-id", total.toFixed(2));
                                        $('#debit-amount').html(total.toFixed(2));
                                        number4text(total);
                                    }
                                    else {


                                        debitDataTable.row.add(JSON.parse(JSON.stringify(row.data()))).draw();
                                        debitDataTable.cell(0, 2).data("none");
                                        debitDataTable.cell(0, 4).data("none");
                                        debitDataTable.cell(0, 6).data("Cash");
                                        //debitDataTable.cell(0, 7).data("none");
                                        debitDataTable.cell(0, 8).data("none");
                                        //debitDataTable.cell(0, 9).data("none");
                                        //debitDataTable.cell(0, 8).data("none");
                                        debitDataTable.cell(0, 10).data("none");
                                        debitDataTable.cell(0, 11).data("none");
                                        debitDataTable.cell(0, 12).data("none");
                                        debitDataTable.cell(0, 13).data("none");
                                        debitDataTable.column(1).visible(false);
                                        debitDataTable.column(3).visible(false);
                                        debitDataTable.column(5).visible(false);
                                        debitDataTable.column(7).visible(false);
                                        $('#select-all-debit').prop('disabled', true);
                                        $('table#debit-data-table input[type=checkbox]').attr('disabled', 'true');
                                        $('.denomination-btn-add').removeClass('disabled');


                                    }
                                })
                            }

                            creditDataTable.columns.adjust().draw();
                            ClearCreditModalInputs();
                            ClearCreditDivErrors();
                            $('#credit-modal').modal('hide');
                        }


                    });

                    return false;
                },
            },

            //EDIT OPERATION
            {

                text: '',
                attr: {
                    id: 'btn-edit-credit-dt'
                },
                action: function (e, dt, node, config, String, indexes) {

                    debugger;
                    var arr = new Array();
                    $('#credit-modal').find('#credit-fd').html('');
                    var id = $("#credit-text").attr("id");
                    $('#' + id).html($('#' + id).html().replace('Add', 'Edit'));
                    $("#btn-add-credit-modal").hide();
                    $("#btn-update-credit-modal").show();
                    ClearCreditDivErrors();

                    var isChecked = $(".checks").is(":checked");
                    if (isChecked) {


                        var columnValues = $('.btn-edit-credit').data('rowindex');

                        //var nameofGL = $("#credit-modal input[name='gl[]']").map(function (idx, ele) {
                        //    "0" = columnValues[28]
                        //    "1" = columnValues[28]
                        //    "2" = columnValues[28]
                        //    return $(ele).val();
                        //}).get();

                        var id = $("#credit-modal").attr("id");
                        var myModal = $('#' + id).modal();
                        //$('#person-id-credit', myModal).val(columnValues[]);
                        //$("#person-id-credit").data("ui-autocomplete")._trigger("change");
                        //$("#credit-person-id").data('ui-autocomplete')._trigger('select', 'autocompleteselect', { item: { value: columnValues[1] } })
                        $('#credit-person-id', myModal).val(columnValues[2]);
                        $("#credit-BusinessOffice-id", myModal).val(columnValues[3]).trigger("change");
                        $("#credit-general-ledger-id").append("<option value='" + columnValues[5] + "'>" + columnValues[6] + "</option>");
                        $('#credit-general-ledger-id option[value="' + columnValues[5] + '"]', myModal).prop("selected", true);
                        $("#credit-customer-account-id").append("<option value='" + columnValues[7] + "'>" + columnValues[8] + "</option>");
                        $('#credit-customer-account-id option[value="' + columnValues[7] + '"]', myModal).prop("selected", true);
                        //$//('#credit-customer-account-id', myModal).val(columnValues[7]);
                        $('#credit-customer-account-id').val(columnValues[7]).trigger('change');
                        //document.getElementById('credit1-01').value = columnValues[13];
                        //$('#fund-amount').find($("#credit1-01").val(columnValues[13]));
                        //$("input[id^=credit1-]").val(columnValues[13]);
                        //$('#fund-amount').find('#credit1-01').val(columnValues[13]);
                        //var parentHtmlTag = $('#fund-amount');
                        $('#credit-1').val(columnValues[28]);
                        $('#credit-3').val(columnValues[29]);
                        $('#credit-5').val(columnValues[30]);
                        //parentHtmlTag.find('#credit-02').val(columnValues[13]);
                        //parentHtmlTag.find('#credit-03').val(columnValues[13]);
                        $("#credit-shares-face-value", myModal).val(columnValues[16]);
                        $("#credit-number-of-shares", myModal).val(columnValues[17]);
                        $('#credit-transaction-amount', myModal).val(columnValues[18]);
                        $('#credit-start-certificate-number', myModal).val(columnValues[19]);
                        $('#credit-end-certificate-number', myModal).val(columnValues[20]);
                        $('#credit-note', myModal).val(columnValues[21]);
                        $('#credit-narration', myModal).val(columnValues[22]);
                        myModal.modal({ show: true });
                    }
                    else {
                        $('.btn-edit-delete').addClass('disabled');
                        $("#credit-modal").modal("hide");
                    }

                    // Hide Selected Dropdown Id Column
                    arr.map(function (obj) {
                        $('#person-id').find("option[value='" + obj.td0 + "']").hide();
                    });
                    $('#btn-update-credit-modal').data('rowindex', columnValues);
                    $("#credit-person-id").data("ui-autocomplete").selectedItem.value = "";
                    // Modal Update Buttons Click Event - Call Event On Update Button Click
                    $(document).on('click', "#btn-update-credit-modal", function (event) {
                        debugger;
                        $('#select-all-credit').prop('checked', false);

                        // Get Modal Inputs In Local Variable
                       
                        var nameofGLprmkey = $('label[id^=general-]').map(function (i, ele) {
                            return $(ele).html();
                        }).get();

                        var nameofGL = $("#credit-modal input[name='gl[]']").map(function (idx, ele) {

                            return $(ele).val();
                        }).get();

                        var nameofGLprmkey1 = "";
                        var nameofGLprmkey2 = "";
                        var nameofGLprmkey3 = "";
                        var nameofGLprmkey4 = "";
                        var nameofGLprmkey5 = "";

                        if (nameofGLprmkey[0] > 0) {
                            nameofGLprmkey1 += '' + nameofGLprmkey[0];
                        }
                        if (nameofGLprmkey[1] > 0) {
                            nameofGLprmkey2 += '' + nameofGLprmkey[1];
                        }
                        if (nameofGLprmkey[2] > 0) {
                            nameofGLprmkey3 += '' + nameofGLprmkey[2];
                        }
                        if (nameofGLprmkey[3] > 0) {
                            nameofGLprmkey4 += '' + nameofGLprmkey[3];
                        }
                        if (nameofGLprmkey[4] > 0) {
                            nameofGLprmkey5 += '' + nameofGLprmkey[4];
                        }
                        var admissionfee = "";
                        var stationaryfee = "";
                        var buildingfund = "";
                        var reservefund = "";
                        var admissionfee2 = "";
                        var admissionfee3 = "";
                        if (nameofGL[0] > 0) {
                            admissionfee += '' + nameofGL[0];
                        }
                        if (nameofGL[1] > 0) {
                            stationaryfee += '' + nameofGL[1];
                        }

                        if (nameofGL[2] > 0) {
                            buildingfund += '' + nameofGL[2];
                        }
                        if (nameofGL[3] > 0) {
                            reservefund += '' + nameofGL[3];
                        }
                        if (nameofGL[4] > 0) {
                            admissionfee2 += '' + nameofGL[4];
                        }

                        var checkbox = '<input type="checkbox" name="check_all" class="checks"/>';
                        var personId = "";
                        var personText = "";
                        if ($("#credit-person-id").data("ui-autocomplete").selectedItem.value != "") {
                            personId = $("#credit-person-id").data("ui-autocomplete").selectedItem.value;
                            personText = $("#credit-person-id").data("ui-autocomplete").selectedItem.label;
                        }
                        else {
                            var getdata = $('#btn-update-credit-modal').data('rowindex');
                            personId = $("#credit-person-id").data("ui-autocomplete").selectedItem.value = getdata[1];
                            personText = $("#credit-person-id").data("ui-autocomplete").selectedItem.label = getdata[2];
                        }
                        var generalLedgerId = $("#credit-general-ledger-id option:selected").val();
                        var generalLedgerText = $("#credit-general-ledger-id option:selected").text();
                        var businessOfficeId = $("#credit-businessOffice-id option:selected").val();
                        var businessOfficeText = $("#credit-businessOffice-id option:selected").text();
                        var customerAccountId = $("#credit-customer-account-id option:selected").val();
                        var customerAccountIdText = $("#credit-customer-account-id option:selected").text();
                        var sharesfacevalue = $("#credit-shares-face-value").val();
                        var numberofshares = $("#credit-number-of-shares").val();
                        var transactionAmount = parseFloat($("#credit-transaction-amount").val());
                        var startCertificateNumber = $("#credit-start-certificate-number").val() ? $("#credit-start-certificate-number").val() : 0;
                        var endCertificateNumber = $("#credit-end-certificate-number").val() ? $("#credit-end-certificate-number").val() : 0;
                        var note = $("#credit-note").val().trim();
                        var narration = $("#credit-narration").val().trim();
                        if (personId.trim().length < 36 || (generalLedgerId.trim().length < 36) || (customerAccountId.trim().length < 36) || (transactionAmount == "") || (startCertificateNumber == "") || (endCertificateNumber == "") || (narration == "")) {
                            ClearDebitDivErrors();

                            if (personId.trim().length < 36)
                                $('#person-id').after('<div class="error" style="color:red">Please Select Customer Name </div>');

                            if (generalLedgerId.trim().length < 36)
                                $('#general-ledger-id').after('<div class="error" style="color:red">Please Select General Ledger </div>');

                            if (customerAccountId.trim().length < 36)
                                $('#customer-account-id').after('<div class="error" style="color:red">Please Select Customer Aaccount </div>');

                            if (transactionAmount == "")
                                $('#transaction-amount').after('<div class="error" style="color:red">Please Select Transaction Amount </div>');

                            if (startCertificateNumber == "")
                                $('#start-number').after('<div class="error" style="color:red">Please Enter Start Number</div>');

                            if (endCertificateNumber == "")
                                $('#end-number').after('<div class="error" style="color:red">Please Enter End Number</div>');

                            return false;
                        }
                        else {

                            var row = creditDataTable.row($(this).attr('rowindex')).data([
                                     checkbox,
                                     personId,
                                     personText,
                                     businessOfficeId,
                                     businessOfficeText,
                                     generalLedgerId,
                                     generalLedgerText,
                                     customerAccountId,
                                     customerAccountIdText,
                                     "",
                                     "",
                                     "0",
                                     "0",
                                     "0",
                                     "0",
                                     "0",
                                     sharesfacevalue,
                                     numberofshares,
                                     //transactionAmount[i],
                                     transactionAmount,
                                     //fee[i],
                                     //generalledgerprmkey,
                                     startCertificateNumber,
                                     endCertificateNumber,
                                     note,
                                     narration,
                                     nameofGLprmkey1,
                                     nameofGLprmkey2,
                                     nameofGLprmkey3,
                                     nameofGLprmkey4,
                                     nameofGLprmkey5,
                                     admissionfee,
                                     stationaryfee,
                                     buildingfund,
                                     reservefund,
                                     admissionfee2,
                            ]).draw();

                        }
                        var transactiontype = $("#transaction-type-id option:selected").val();

                        if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {
                            $('#debit-data-table > tbody > tr').each(function (index) {

                                var currentRow = $(this).closest('tr');
                                var columnvalue = (debitDataTable.row(currentRow).data());
                                if (typeof columnvalue != 'undefined' && columnvalue != null) {

                                    debitDataTable.cell(0, 9).data(total);
                                    $('#debit-amount').attr("data-id", total.toFixed(2));
                                    $('#debit-amount').html(total.toFixed(2));
                                    number4text(total);


                                }
                            })
                        }

                        // Hide Id Column Of Datatable
                        creditDataTable.column(1).visible(false);
                        creditDataTable.column(3).visible(false);
                        creditDataTable.column(5).visible(false);

                        
                        // Update Datatable Row
                        creditDataTable.columns.adjust().draw();

                        $("#credit-modal").modal('hide');
                        $('.btn-add-credit-modal').removeClass('disabled');
                        $('.btn-delete-credit').addClass('disabled');
                        $('.btn-edit-credit').addClass('disabled');
                        $('.btn-add-credit').removeClass('disabled');
                        $('#select-all-credit').prop('checked', false);

                    })
                },
            },

            //DELETE OPERATION
            {
                text: '',
                attr: {
                    id: 'btn-Delete-credit-dt'
                },

                action: function (e, dt, node, config) {

                    var isChecked = $("input[type='checkbox']").is(":checked");

                    if (isChecked) {
                        if (confirm("Are you sure to delete this row?")) {

                            if ($("input[type='checkbox']:checked").each(function () {

                               var row = creditDataTable.row($("input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                                var arr = $('.btn-delete-credit').data('rowindex');
                                  arr.map(function (obj) {

                                var transactiontype = $("#transaction-type-id option:selected").val();

                               if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {
                                $('#debit-data-table > tbody > tr').each(function (index) {


                                    var currentRow = $(this).closest('tr');
                                    var columnvalue = (debitDataTable.row(currentRow).data());
                                    if (typeof columnvalue != 'undefined' && columnvalue != null) {

                                       if (total > 0) {

                                        debitDataTable.cell(0, 9).data(total);
                                        $('#debit-amount').attr("data-id", total.toFixed(2));
                                        $('#debit-amount').html(total.toFixed(2));
                                        number4text(total);
                            }
                            else {
                                           debitDataTable.row($(this).closest("tr").get(0)).remove().draw();
                            }
                            }
                                    $('#person-id').find("option[value='" + obj.td0 + "']").show();
                                    $("#person-id").prop("selectedIndex", 0);
                            })
                            }
                            });

                                $('.btn-add-credit-modal').removeClass('disabled');
                                $('.btn-delete-credit').addClass('disabled');
                                $('.btn-edit-credit').addClass('disabled');
                                $('.btn-add-credit').removeClass('disabled');
                                $('#select-all-credit').prop('checked', false);


                                //  var total = $("#total-amount").data('id');

                                //if (total === "0.00") {

                                //    $(".amountcard").hide();
                                //    }
                                //alert(total);
                            }));
                        }

                    }
                    else {
                        alert("Please Select Any Record For Delete Operation.");

                    }
                }
            },
        ],
        //"rowCallback": function( row, data, index ) {
        //    //alert(data.file_name);

        //    ;
        //    creditDataTable.row.add(data).draw();
        //    //$('td:eq(4)', row).html( '<a class="text-primary" href="http://localhost/ci/uploaded/'+name+'/sharedFiles/'+data.file_name+'" target="_blank">Download</a>');
        //},
        //"createdRow": function (row, data, full, index) {
        //
        //var row_index = $('tr', row).index();
        //var row_count = $('#credit-data-table').find('tr').length;

        // 
        //for (var row of data) {

        //    creditDataTable.row.add(row);

        //}
        //creditDataTable.draw();
        //if (creditDataTable.length > 0) {
        //    
        //    creditDataTable.row.add(data).draw();
        //}
        //},
        //            //for (var i = 0; i < data.length; i++) {
        //            //    $('td', row).eq(i).addClass(data[i]);
        //            //    //The above line assumes that you want to add a CSS class named "red" to a 
        //            //    //field that has the text "red" in it, if not, you can change the logic
        //            //}
        //////            //var data = JSON.parse(JSON.stringify(row.data()));
        //////            //alert(data);

        //////            //let row1 = creditDataTable.row('#row-' + msg.id);
        //////            //let rowindex = row1.index();    
        //////            // instead of getting the row, I get the row data.
        //////            // the json stuff is done just to make a copy of the data
        //////            // to ensure it is disconnected from the source.
        //////            ///var data = JSON.parse(JSON.stringify(row.data()));

        //////            // this actually destroys the row so you can't add it to the other table.
        //////            //row.remove().draw();
        ////////            DT_CellIndex
        //////            //: 
        //////            //var data = creditDataTable.row($(this).parents('tr')).data();
        //////            //debitDataTable.row.add(data).draw();
        ////////                {row: 0, column: 7}
        //////            ///ar arr = {  "0":data()[7] };
        ////            //            // then add and draw.
        ////           var api = this.api();


        ////            $('#debit-data-table > tbody > tr ').each(function (index) {

        ////                //var row = creditDataTable.row($(this).attr('rowindex'));



        ////                var currentRow = $(this).closest('tr');
        ////                var columnvalue = (debitDataTable.row(currentRow).data());
        ////                if (typeof columnvalue != 'undefined' && columnvalue != null) {
        ////                    //Remove the formatting to get integer data for summation
        ////                    var intVal = function (i) {
        ////                        return typeof i === 'string' ?
        ////                            i.replace(/[\$,]/g, '') * 1 :
        ////                            typeof i === 'number' ?
        ////                            i : 0;
        ////                    };
        ////                    total = api
        ////                            .column(7)
        ////                            .data()
        ////                            .reduce(function (a, b) {
        ////                                return intVal(a) + intVal(b);
        ////                            }, 0);


        ////                    var data1 = JSON.parse(JSON.stringify(total));
        ////                    debitDataTable.cell(currentRow, 7).data(data1).draw();
        ////                    debitDataTable.cell(currentRow, 2).data("none").draw();
        ////                    debitDataTable.cell(currentRow, 3).data("none").draw();
        ////                    debitDataTable.cell(currentRow, 4).data("none").draw();
        ////                    //creditDataTable.row.add(data).draw();
        ////                    //creditDataTable.column(1).visible(false);
        ////                    //creditDataTable.column(3).visible(false);
        ////                    //creditDataTable.column(5).visible(false);

        ////                }
        ////                else {
        ////                    var data1 = JSON.parse(JSON.stringify(data[7]));

        ////                   // debitDataTable.cell(0, 7).data(data1).draw();
        ////                    debitDataTable.row.add(data).draw();
        ////                    //debitDataTable.cell(currentRow, 1).data("none").draw();
        ////                    //debitDataTable.cell(currentRow, 2).data("none").draw();
        ////                    //debitDataTable.cell(currentRow, 4).data("none").draw();
        //////                    //debitDataTable.column(2).data("none").draw();
        ////                    debitDataTable.column(1).visible(false);
        ////                    debitDataTable.column(3).visible(false);
        ////                    debitDataTable.column(5).visible(false);
        //////                    //debitDataTable.column(1).visible(false);
        //////                                //debitDataTable.column(2).visible(false);
        //////                                //debitDataTable.column(3).visible(false);
        //////                                //debitDataTable.column(4).visible(false);
        //////                                //debitDataTable.column(6).visible(false);
        //////                                //debitDataTable.column(5).visible(false);
        //////                                //debitDataTable.column(8).visible(false);
        ////               }

        //////              //var row_index = debitDataTable.closest("tr").index();
        //////               //var col_index = debitDataTable.index();
        //////               //var row_index = $(this).parent('table').index();
        //////               //var data1 = JSON.parse(JSON.stringify(data[7]));
        //////               //tt = ($(row)).eq(7).add(data1);
        //////               //var tt1 = creditDataTable.cell(row, 7).data('Helloo').draw();
        //////               //var rr = tt1[0];
        //////               //debitDataTable.row.add.data('Helloo').draw();
        //////               //var TT = debitDataTable.cell(rr[0].row,rr[0].column).data('Helloo').draw();
        ////         })
        ////            //debitDataTable.cell(, column: 7 }).data('Helloo').draw();
        //////            //var row1 = debitDataTable.row(this);
        //////            //if (debitDataTable.length >0) {
        //////            //    
        //////            //    $('#debit-data-table > tbody > tr').each(function () {
        //////            //        ;
        //////            //        var currentRow = $(this).closest("tr");

        //////            //        debitDataTable.cell(currentRow, 7).data(data1).draw();
        //////            //        //var columnvalue = (creditDataTable.row(currentRow).data());
        //////            //        //if (typeof columnvalue != 'undefined' && columnvalue != null) {

        //////            //        //    $('#person-id').find("option[value='" + columnvalue[1] + "']").hide();
        //////            //        //}
        //////            //        //else {
        //////            //        //    return true;
        //////            //        //}

        //////            //    });
        //////            //}
        //////            //var row = $(this).closest('tr');
        //////            //var row1 = debitDataTable.row(this)[0];
        //////            //if (creditDataTable.length > 0) {

        //////               debitDataTable.row.add(data).draw();
        //////            //}

        //////           // debitDataTable.cell({ row: index[7], column: 7 }).data('Helloo').draw();

        //////           /// var tt1=tt.DT_CellIndex;
        //////              //debitDataTable.cell(0,7).data(data1).draw();
        //////            //

        //////            //debitDataTable.cell(this).data("new");
        //////            //

        //////            //var
        //////            //var data2 = tt[1];

        //////            //debitDataTable.row.add(data2).draw();
        //////            // debitDataTable.row[0].column(7).add(data);
        //////            //debitDataTable.rows.add(data1).draw();
        //////            //row.remove().draw();
        //////            debitDataTable.column(1).visible(false);
        //////            debitDataTable.column(2).visible(false);
        //////            debitDataTable.column(3).visible(false);
        //////            debitDataTable.column(4).visible(false);
        //////            debitDataTable.column(6).visible(false);
        //////            debitDataTable.column(5).visible(false);
        //////            debitDataTable.column(8).visible(false);

        //////           // 

        //////        //    var api = this.api(), data;
        //////        //    //// Remove the formatting to get integer data for summation
        //////        //    var intVal = function (i) {
        //////        //        return typeof i === 'string' ?
        //////        //            i.replace(/[\$,]/g, '') * 1 :
        //////        //            typeof i === 'number' ?
        //////        //            i : 0;
        //////        //    };
        //////        //    total = api
        //////        //      .column(4)
        //////        //      .data()
        //////        //      .reduce(function (a, b) {
        //////        //          return intVal(a) + intVal(b);
        //////        //      }, 0);


        //////        //    var denomination = api.column(2).data().toArray();
        //////        //    var pices = api.column(3).data().toArray();
        //////        //    var denominationtotal = 0;

        //////        //    for (i = 0; i < denomination.length; i++) {
        //////        //        denominationtotal += denomination[i] * pices[i];
        //////        //    }

        //////        //    var transactionamount = parseFloat($("#credit-amount").attr("data-id"));
        //////        //    var debittransactionamount = parseFloat($("#debit-amount").attr("data-id"));
        //////        //    var diffamount = parseFloat($("#total-amount").data("id"));

        //////        //    var transactiontype = $("#transaction-type-id option:selected").val();

        //////        //    if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {

        //////        //        if ((denominationtotal <= transactionamount) && (diffamount == 0)) {


        //////        //        }
        //////        //        else {

        //////        //            denominationDataTable.rows($(row)).remove().draw();
        //////        //        }

        //////        //    }
        //////        //    else {
        //////        //        if ((denominationtotal <= debittransactionamount) && (diffamount == 0)) {

        //////        //        } else {


        //////        //            denominationDataTable.rows($(row)).remove().draw();
        //////        //        }
        ////            //        //    
        //       }     ,
        //"fnInfoCallback": function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
        //    ;
        //    var info = iTotal + (iTotal > 1 ? ' Accounts' : ' Account'),
        //    infoFiltered = ' (filtered from ' + iMax + ' total accounts)',
        //    infoEmpty = '0 Accounts';

        //    oSettings.oLanguage.sInfo = info;
        //    oSettings.oLanguage.sInfoFiltered = infoFiltered;
        //    oSettings.oLanguage.sInfoEmpty = infoEmpty;
        //},

        rowCallback: function (row, data, index) {
            debugger;
            //if (data.length > 0) {
            //    //$('#hfId').val('');
            //    //$(row).animate({ backgroundColor: "#B0C4DE" }, 700).animate({ backgroundColor: "#F5F5F5" }, 700);
            //    //$(row).css('background-color', '#00FF00');
            //    $(row).animate({ backgroundColor: "#B0C4DE" }, 500).animate({ backgroundColor: "#F5F5F5" }, 500);

            //}
            var transactiontype = $("#transaction-type-id option:selected").val();
            if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {

                // If name is "Ashton Cox"
                var fee = $("#credit-modal input[name='fee[]']").map(function (idx, ele) {

                    return $(ele).val();
                }).get();

                //creditDataTable.row.add(JSON.parse(JSON.stringify(data))).draw();
                // for (var i = 0; i < fee.length; i++) {
                //$('td', row).eq(5).addClass('highlight');
               
                // Add COLSPAN attribute
                //$('td:eq(2)', row).attr('colspan', 4);
                // Math.round(mySum * 2) / 2
                var arr = [];
                var principleamount = Math.round(data[16] * data[17]);

                arr.push(data[28], data[29], data[30], data[31], data[32])
                var other = 0;
                $.each(arr, function (i, val) {
                    if (!isNaN(parseFloat(val))) {
                        other += parseInt(val);
                    }

                })

                // Update cell data
                //creditDataTable.cell(0, 9).data(principleamount);
                //creditDataTable.cell(0, 10).data("0");
                //creditDataTable.cell(0, 11).data("0");
                //creditDataTable.cell(0, 12).data("0");
                //creditDataTable.cell(0, 13).data(other);
                //creditDataTable.cell(0, 14).data(data[18]);
                //creditDataTable.cell(0, 26).data("0");
                //creditDataTable.cell(0, 27).data("0");
                //creditDataTable.cell(0, 31).data("0");
                //creditDataTable.cell(0, 32).data("0");
                creditDataTable.cell(row, 9).data(principleamount);
                creditDataTable.cell(row, 13).data(other);
                creditDataTable.cell(row, 14).data(data[18]);

            }
           
        },
        createdRow: function(row, data, dataIndex){
            debugger;
            //if ($($.parseHTML(data[0])).is(":checked")) {
            //    debugger;
            //    $('td', row).css('background-color', 'Red');

            //}
            //if (data.length > 0) {
            //    //$('#hfId').val('');
            //    //$(row).animate({ backgroundColor: "#B0C4DE" }, 700).animate({ backgroundColor: "#F5F5F5" }, 700);
            //    //$(row).css('background-color', '#00FF00');
            //    $(row).animate({ backgroundColor: "#B0C4DE" }, 500).css({ backgroundColor: "" });
            
            //}
            var transactiontype = $("#transaction-type-id option:selected").val();
            if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {

                // If name is "Ashton Cox"
                var fee = $("#credit-modal input[name='fee[]']").map(function (idx, ele) {

                    return $(ele).val();
                }).get();

                //creditDataTable.row.add(JSON.parse(JSON.stringify(data))).draw();
                // for (var i = 0; i < fee.length; i++) {
                //$('td', row).eq(5).addClass('highlight');
                debugger;
                // Add COLSPAN attribute
                //$('td:eq(2)', row).attr('colspan', 4);
                // Math.round(mySum * 2) / 2
                var arr = [];
                var principleamount = Math.round(data[16] * data[17]);

                arr.push(data[28], data[29], data[30], data[31], data[32])
                var other = 0;
                $.each(arr, function (i, val) {
                    if (!isNaN(parseFloat(val))) {
                        other += parseInt(val);
                    }

                })

                // Update cell data
                //creditDataTable.cell(0, 9).data(principleamount);
                //creditDataTable.cell(0, 10).data("0");
                //creditDataTable.cell(0, 11).data("0");
                //creditDataTable.cell(0, 12).data("0");
                //creditDataTable.cell(0, 13).data(other);
                //creditDataTable.cell(0, 14).data(data[18]);
                //creditDataTable.cell(0, 26).data("0");
                //creditDataTable.cell(0, 27).data("0");
                //creditDataTable.cell(0, 31).data("0");
                //creditDataTable.cell(0, 32).data("0");
                creditDataTable.cell(row, 9).data(principleamount);
                creditDataTable.cell(row, 13).data(other);
                creditDataTable.cell(row, 14).data(data[18]);

            }
            else {
                creditDataTable.cell(row, 6).data("Cash");
                creditDataTable.cell(row, 13).data("none");
                creditDataTable.cell(row, 14).data(data[9]);

            }
                //creditDataTable.column(9).visible(false);
                //creditDataTable.column(14).visible(false);
                //this.api().cell($('td:eq(9)', row)).data(principleamount);
                //this.api().cell($('td:eq(13)', row)).data(other);
                //this.api().cell($('td:eq(14)', row)).data(data[18]);
               
               
                //this.api().cell($(18, row)).data(pin);
                //this.api().cell($(19, row)).data(0);
                //this.api().cell($(20, row)).data(0);
                // this.api().cell($(12, row)).data(0);
                //this.api().cell($(13, row)).data(data[12]);
           // }
        },

        "footerCallback": function (row, data, start, end, display, setting) {
            debugger
            //if (this.api().page.info().pages === 1) {
            //    $('#example_paginate').hide();
            //}
            //if (this.api().page.info().pages === 1) {
            //    $('#credit-data-table_paginate').hide();
            //}
            // var info = this.api().page.info();
            //var tt = info.recordsTotal = 4;
            //var rr = info.recordsDisplay = 2;
            //if (row < 5)
            //{

            //}
            //var data1 = JSON.parse(JSON.stringify(data[7]));
            //alert(data1);

            //var table = $(this).closest('table').DataTable();
            //if (data > 0) {
            //    ;
            //    var data1 = creditDataTable.row($(this).parents('tr')).data();
            //}
            //ebitDataTable.cell(this).data("new");
            //if (data > 0) {
            //    
            //    var data = creditDataTable.row($(this).parents('tr')).data();
            //    debitDataTable.row.add(data).draw();
            //}
            //if (debitDataTable!=null)
            //{
            //    debitDataTable.cell(row, 7).data('Denied');
            //}

            var transactiontype = $("#transaction-type-id option:selected").val();
            if (transactiontype === "408d891d-244a-4b8f-b590-0e1b2a6d5afb") {
                debugger
                let total = debitDataTable.column(9).data();
                credittotal = total[0];
                if (typeof credittotal === 'undefined') {
                    $("#total-amount").attr('data-id', '0')
                    jQuery("#credit-amount").attr('data-id', '0');
                    $('#credit-amount').html("0.00");
                    $(".amountcard").hide();
                    Amountdifference();
                    number3text(credittotal);
                }
                else {

                    creditDataTable.cell(0, 9).data(credittotal.toFixed(2));
                    $("#total-amount").attr('data-id', '0')
                    jQuery("#credit-amount").attr('data-id', credittotal.toFixed(2));
                    $('#credit-amount').html(credittotal.toFixed(2));
                    $(".amountcard").hide();
                    Amountdifference();
                    number3text(credittotal);
                    //debitDataTable.column(8).data();
                }
            }
            else {
               

                total = this.api()
                  .column(14)
                  .data()
                  .reduce(function (a, b) {
                      return parseInt(a) + parseInt(b);
                  }, 0);
                var getcreditamount = $('#credit-amount').data("id");
                var credittotal = parseInt(total) + parseInt(getcreditamount);
                var transactiontype = $("#transaction-type-id option:selected").val();

                if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {

                    if (total > 0) {

                        $("#total-amount").attr('data-id', '0')
                        jQuery("#credit-amount").attr('data-id', credittotal.toFixed(2));
                        $('#credit-amount').html(credittotal.toFixed(2));
                        $(".amountcard").hide();
                        Amountdifference();
                        number3text(credittotal);

                    }
                    else {
                        jQuery("#credit-amount").attr('data-id', credittotal.toFixed(2));
                        $('#credit-amount').html(credittotal.toFixed(2));
                        $(".amountcard").hide();
                        number3text(credittotal);
                    }

                }
                else {
                    if (total > 0) {

                        jQuery("#credit-amount").attr('data-id', credittotal.toFixed(2));
                        $('#credit-amount').html(credittotal.toFixed(2));
                        $(".amountcard").show();
                        Amountdifference();
                        number3text(credittotal);


                    }
                    else {

                        jQuery("#credit-amount").attr('data-id', credittotal.toFixed(2));
                        $('#credit-amount').html(getcreditamount.toFixed(2));
                        Amountdifference();
                        number3text(credittotal);

                    }
                }
            }
        },
    })
   
    //}).on('mouseenter', 'td', function () {
    //    var colIdx = creditDataTable.cell(this).index().column;
    //                  $(creditDataTable.cells().nodes()).removeClass('highlight');
    //                  $(creditDataTable.column(colIdx).nodes()).addClass('highlight');
    //              });
    $('.close').click(function () {
        ClearCreditDivErrors();
        ClearCreditModalInputs();


        $('.btn-delete-credit').addClass('disabled');
        $('.btn-edit-credit').addClass('disabled');
        $('.checks').prop('checked', false);
        $('#select-all-checks').prop('checked', false);

    });

    var btns = $('#btn-add-credit-dt');
    btns.addClass('btn btn-success btn-sm btn-add-credit disabled').append('<i class="fas fa-plus icon"></i>');
    $("#btn-add-credit-dt").attr('title', 'Add');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');
    btns.Enabled = true;

    var btns = $('#btn-edit-credit-dt');
    btns.addClass('btn btn-primary btn-sm btn-edit-credit disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $("#btn-edit-credit-dt").attr('title', 'Edit');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-Delete-credit-dt');
    btns.addClass('btn btn-danger btn-sm btn-delete-credit disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $("#btn-Delete-credit-dt").attr('title', 'Delete');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $('#select-all-credit').on('click', function () {
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);

            var arr = new Array();

            $('#credit-data-table tbody input[type="checkbox"]').each(function () {

                $(this).prop('checked', true);

                var row = $(this).closest("tr");

                selectedRow = creditDataTable.row(row).index();

                var tdrow = (creditDataTable.row(selectedRow).data());

                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-delete-credit').data('rowindex', arr);
                $('.btn-add-credit').addClass('disabled');
                $('.btn-edit-credit').addClass('disabled');
                $('.btn-delete-credit').removeClass('disabled');
            });
        }
        else {
            $('#credit-data-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-credit-modal').removeClass('disabled');
                $('.btn-delete-credit').addClass('disabled');
                $('.btn-add-credit').removeClass('disabled');
            });
        }
    });

    $("#credit-data-table tbody").on("click", ":checkbox", function () {

        if ($(this).is(':checked')) {
            //$(this).parents("tr:first").data('prevColor', $(this).parents("tr:first").css('background-color'));
            $(this).parents("tr:first").animate({ backgroundColor: "#B0C4DE" }, 500).css('background-color');
        }
        else {
            //$(this).parents("tr:first").css('background-color', $(this).parents("tr:first").data('prevColor'));
            $(this).parents("tr:first").css('background-color', "");
        }
    });
    // binding the change event-handler to the tbody:
    $('#credit-data-table tbody').on('change', function () {
        debugger;
       
            // getting all the checkboxes within the tbody:
        var all = $('#credit-data-table tbody input[type="checkbox"]');
       
           
                // getting only the checked checkboxes from that collection:
                checked = all.filter(':checked');

            if (all.length > 0 == checked.length) {
                $('.btn-delete-credit').removeClass('disabled');

                $('.btn-edit-credit').removeClass('disabled');
            }
            else {
                //jQuery(this).parents("tr").css("background-color", "");
                $('.btn-edit-credit').addClass('disabled');

            }
            if (checked.length == 0) {
                $('.btn-delete-credit').addClass('disabled');
                $('.btn-add-credit').removeClass('disabled');

            }
            // setting the checked property of toggleCheckbox to true, or false
            // according to whether the number of checkboxes is greater than 0;
            // if it is, we use the assessment to determine true/false,
            // otherwise we set it to false (if there are no checkboxes):
            $('#select-all-credit').prop('checked', all.length > 0 ? all.length === checked.length : false);
        });
    
    $('#credit-data-table tbody').on('click', "input[type=checkbox]", function () {
        $('#credit-data-table input[type="checkbox"]:checked').each(function () {
            debugger;
            var isChecked = $(this).prop("checked");

            if (isChecked) {
                debugger;
                //jQuery(this).parents("tr").css('background-color', 'yellow');
               // $(this).closest('tr').toggleClass("highlight", isChecked);

                var arr = new Array();

                $("#credit-data-table tbody input[type='checkbox']:checked").each(function (index) {

                    var row = $(this).closest("tr");
                    selectedRow = creditDataTable.row(row).index();
                    if (selectedRow >= 0) {
                        var tdrow = (creditDataTable.row(selectedRow).data());
                        arr.push({
                            td0: tdrow[1],
                            td1: tdrow[7]
                        });

                        $('.btn-add-credit').addClass('disabled');
                        $('.btn-edit-credit').removeClass('disabled');
                        $('.btn-delete-credit').removeClass('disabled');

                        $('#btn-update-credit-modal').attr('rowindex', selectedRow);
                        $('.btn-edit-credit').data('rowindex', tdrow);
                        $('.btn-delete-credit').data('rowindex', arr);
                        $('#select-all-credit').data('rowindex', arr);
                    }
                });
            }
            else {
                //jQuery(this).closest("tr").css('background-color', 'pink !important');
            }
        });
    });

    //To page load table each row get value & dropdown value Hide 
    $('#credit-data-table > tbody > tr').each(function () {


        var currentRow = $(this).closest("tr");
        var columnvalue = (creditDataTable.row(currentRow).data());
        if (typeof columnvalue != 'undefined' && columnvalue != null) {

            $('#person-id').find("option[value='" + columnvalue[1] + "']").hide();
        }
        else {
            return true;
        }

    });

    function ClearCreditModalInputs() {
        debugger;
        $("#credit-person-id").val('');
       // $("#credit-BusinessOffice-id").val('');
        //alert($("#credit-BusinessOffice-id").find("option:first").val());
        $("select[name='transactionCustomerAccountViewModel.BusinessOfficeId']option:first").text("Select Address");
        //$("#credit-BusinessOffice-id").prop('selectedIndex', -1)
        //$("#credit-BusinessOffice-id").prepend("<option value='0'>None</option>");
      //  $('#credit-BusinessOffice-id')
      //.append($("<option></option>")
      //  .attr("value", "0")
      //  .text("rer"));
        //var option = '<option value="0">l;l;l</option>';
        //$('#credit-BusinessOffice-id').append(option);
        ///var tt = $("#credit-BusinessOffice-id").find('option:first').val();
        //$("#credit-BusinessOffice-id")[0].options[0].selected = true;
        //$("#credit-BusinessOffice-id").prepend("<option value='0'>None</option>");
        //$("#credit-BusinessOffice-id").append(new Option("rtt", 0))
        //$("#credit-BusinessOffice-id").append("<option value='0'>ui</option>");
        //$("#credit-BusinessOffice-id").val('');
        //$('#credit-BusinessOffice-id').empty().append('<option selected="selected" value="0">--Please Select--</option>');
       /// $('#credit-BusinessOffice-id').empty().append($('<option>').text('Please Select').attr('value', '0'));
        //var selectList1 = $("#credit-businessOffice-id");
        //selectList1.find('option').not(':first').remove();
        //$('#credit-BusinessOffice-id option:first').prop('selected', true);
        //$('#credit-BusinessOffice-id  option[value]').attr('selected', 'selected');
        //$("#credit-BusinessOffice-id option:first").attr('selected', 'selected').trigger('change');
        ///$("#credit-BusinessOffice-id option:first").attr('selected', 'selected');
        var selectList = $("#credit-general-ledger-id");
        selectList.find('option').not(':first').remove();
        var selectList = $("#credit-customer-account-id");
        selectList.find('option').not(':first').remove();
        $('#demo').html('');
        $("#fund-amount").html('');
        $("#credit-note").val('');
        $("#credit-narration").val('');
    }

    // Clear Div Errors 
    function ClearCreditDivErrors() {

        $('#person-id-credit').next("div.error").remove();
        $('#general-ledger-id-credit').next("div.error").remove();
        $('#customer-account-id-credit').next("div.error").remove();
        $('#credit-transaction-amount').next("div.error").remove();
        $('#credit-start-certificate-number').next("div.error").remove();
        $('#credit-end-certificate-number').next("div.error").remove();
        $('#narration').next("div.error").remove();
    }

    // Clear Customer Account Turn Over Limit Modal Input Values

    ClearDebitModalInputs();

    // Initialising & Configuring DataTables 
    debitDataTable = $('#debit-data-table').DataTable({

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
            "targets": [2],
            "visible": false
        }],
        "columnDefs": [
           { orderable: false, targets: 0 }
        ],
        "language": {
            "lengthMenu": "_MENU_",
        },
        "bAutoWidth": false,
        "order": ['2', 'desc'],
        "dom": 'Bfrtip',
        "dom": '<"float-left"B><"float-right"f>rt<"row"<"col-sm-4"l><"col-sm-4"i><"col-sm-4"p>>',
        "buttons": [
                //  NEW    OPERATION
                {
                    text: '',
                    attr: {
                        id: 'btn-add-debit-dt'
                    },
                    action: function (e, dt, node, config, type) {

                        event.preventDefault();
                        $('#debit-modal').find('#debit-fd').html('');
                        var id = $("#debit-text").attr("id");
                        $('#' + id).html($('#' + id).html().replace('Edit', 'Add'));
                        $("#btn-update-debit-modal").hide();
                        $("#btn-add-debit-modal").show();
                        ClearDebitModalInputs();
                        ClearDebitDivErrors();

                        $('#debit-data-table > tbody > tr').each(function () {
                            var currentRow = $(this).closest("tr");
                            var columnvalue = (debitDataTable.row(currentRow).data());
                            if (typeof columnvalue != 'undefined' && columnvalue != null) {

                                $('#person-id1').find("option[value='" + columnvalue[1] + "']").hide();
                            }
                            else {
                                return true;
                            }

                        });

                        $('#debit-modal').modal('show');


                        $('#btn-add-debit-modal').on('click', function (event) {
                            debugger;
                            //var transactionAmount = $("#debit-modal input[name='amount[]']").map(function (idx, ele) {
                            //    return $(ele).val().trim().length == 0 ? 0 : parseFloat($(ele).val().trim());
                            //}).get();
                            var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                            var person = $("#person-id-debit").data("ui-autocomplete").selectedItem.value;
                            var personText = $("#person-id-debit").data("ui-autocomplete").selectedItem.label;
                            var BusinessOfficeId = $("#debit-BusinessOffice-id option:selected").val();
                            var BusinessOfficeText = $("#debit-BusinessOffice-id option:selected").text();
                            var generalLedger = $("#debit-general-ledger-id option:selected").val();
                            var generalLedgerText = $("#debit-general-ledger-id option:selected").text();
                            var customerAccountId = $("#debit-customer-account-id option:selected").val();
                            var customerAccountIdText = $("#debit-customer-account-id option:selected").text();
                            var transactionAmount = parseFloat($("#debit-transaction-amount").val());
                            var startCertificateNumber = $("#debit-start-certificate-number").val() ? $("#debit-start-certificate-number").val() : 0;
                            var endCertificateNumber = $("#debit-end-certificate-number").val() ? $("#debit-end-certificate-number").val() : 0;
                            var note = $("#debit-note").val();
                            var narration = $("#debit-narration").val();
                            if (person.length < 36 || (generalLedger.length < 36) || (customerAccountId.length < 36) || (narration == "")) {
                                ClearDebitDivErrors();

                                if (person.trim().length < 36)
                                    $('#person-id-debit').after('<div class="error" style="color:red">Please Select Customer Name </div>');

                                if (generalLedger.trim().length < 36)
                                    $('#general-ledger-id-debit').after('<div class="error" style="color:red">Please Select General Ledger </div>');

                                if (customerAccountId.trim().length < 36)
                                    $('#customer-account-id-debit').after('<div class="error" style="color:red">Please Select Customer Aaccount </div>');

                                if (transactionAmount == "")
                                    $('#customer-account-transaction-amount').after('<div class="error" style="color:red">Please Select Transaction Amount </div>');

                                return false;
                            }

                            else {
                                //for (var i = 0; i < transactionAmount.length; i++) {

                                var row = debitDataTable.row.add([
                                 tag,
                                 person,
                                 personText,
                                 BusinessOfficeId,
                                 BusinessOfficeText,
                                 generalLedger,
                                 generalLedgerText,
                                 customerAccountId,
                                 customerAccountIdText,
                                 //transactionAmount[i],
                                 transactionAmount,
                                 startCertificateNumber,
                                 endCertificateNumber,
                                 note,
                                 narration,
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none",
                                 "none"
                                ]).draw();

                                debitDataTable.column(1).visible(false);
                                debitDataTable.column(3).visible(false);
                                debitDataTable.column(5).visible(false);
                                debitDataTable.column(7).visible(false);
                                debitDataTable.column(10).visible(false);
                                debitDataTable.column(11).visible(false);
                                debitDataTable.columns.adjust().draw();

                                var transactiontype = $("#transaction-type-id option:selected").val();
                                if (transactiontype === "408d891d-244a-4b8f-b590-0e1b2a6d5afb") {
                                    debugger;
                                    $('#credit-data-table > tbody > tr').each(function (index) {
                                        debugger;
                                        var currentRow = $(this).closest('tr');
                                        var columnvalue = (creditDataTable.row(currentRow).data());
                                        if (typeof columnvalue != 'undefined' && columnvalue != null) {

                                            creditDataTable.cell(0, 14).data(total);
                                            $('td', currentRow).eq(0).find(".checks").attr("disabled", true);
                                            //$('th', currentRow).eq(0).find("#select-all-debit").attr("disabled", true);
                                            $('#credit-amount').attr("data-id", total.toFixed(2));
                                            $('#credit-amount').html(total.toFixed(2));
                                            number3text(total);
                                        }
                                        else {

                                            creditDataTable.row.add(JSON.parse(JSON.stringify(row.data()))).draw();
                                            creditDataTable.cell(0, 2).data("none");
                                            creditDataTable.cell(0, 4).data("none");
                                            //creditDataTable.cell(0, 6).data("Cash");
                                            creditDataTable.cell(0, 7).data("none");
                                            creditDataTable.cell(0, 8).data("none");
                                            creditDataTable.cell(0, 9).data("none");
                                            creditDataTable.cell(0, 10).data("none");
                                            creditDataTable.cell(0, 11).data("none");
                                            creditDataTable.cell(0, 12).data("nane");
                                            creditDataTable.cell(0, 13).data("nane");
                                            //creditDataTable.cell(0, 14).data("nane");
                                            creditDataTable.cell(0, 15).data("nane");
                                            creditDataTable.cell(0, 16).data("nane");
                                            creditDataTable.cell(0, 15).data("none");
                                            creditDataTable.cell(0, 17).data("none");
                                            creditDataTable.cell(0, 18).data("none");
                                            creditDataTable.cell(0, 19).data("none");
                                            creditDataTable.cell(0, 20).data("none");
                                            creditDataTable.cell(0, 21).data("none");
                                            creditDataTable.cell(0, 22).data("none");
                                            creditDataTable.cell(0, 23).data("none");
                                            creditDataTable.cell(0, 24).data("none");
                                            creditDataTable.cell(0, 25).data("none");
                                            creditDataTable.cell(0, 26).data("none");
                                            creditDataTable.cell(0, 27).data("none");
                                            creditDataTable.cell(0, 28).data("none");
                                            creditDataTable.cell(0, 29).data("none");
                                            creditDataTable.cell(0, 30).data("none");
                                            creditDataTable.cell(0, 31).data("none");
                                            creditDataTable.cell(0, 32).data("none");


                                            creditDataTable.column(1).visible(false);
                                            creditDataTable.column(3).visible(false);
                                            creditDataTable.column(5).visible(false);
                                            creditDataTable.column(7).visible(false);
                                            //creditDataTable.column(14).visible(false);
                                            creditDataTable.column(16).visible(false);
                                            creditDataTable.column(15).visible(false);
                                            creditDataTable.column(17).visible(false);
                                            creditDataTable.column(18).visible(false);
                                            creditDataTable.column(19).visible(false);
                                            creditDataTable.column(20).visible(false);
                                            creditDataTable.column(21).visible(false);
                                            creditDataTable.column(22).visible(false);
                                            creditDataTable.column(23).visible(false);
                                            creditDataTable.column(24).visible(false);
                                            creditDataTable.column(25).visible(false);
                                            creditDataTable.column(26).visible(false);
                                            creditDataTable.column(27).visible(false);
                                            creditDataTable.column(28).visible(false);
                                            creditDataTable.column(29).visible(false);
                                            creditDataTable.column(30).visible(false);
                                            creditDataTable.column(31).visible(false);
                                            creditDataTable.column(32).visible(false);

                                            $('#select-all-credit').prop('disabled', true);
                                            $('table#credit-data-table input[type=checkbox]').attr('disabled', 'true');
                                            $('.denomination-btn-add').removeClass('disabled');
                                            //creditDataTable.column(1).visible(false);
                                            //creditDataTable.column(3).visible(false);
                                            //creditDataTable.column(5).visible(false);
                                            //creditDataTable.column(8).visible(false);
                                            //creditDataTable.column(9).visible(false);
                                            //creditDataTable.column(15).visible(false);
                                            //creditDataTable.column(17).visible(false);
                                            //creditDataTable.column(18).visible(false);
                                            //creditDataTable.column(19).visible(false);
                                            //creditDataTable.column(20).visible(false);
                                            //creditDataTable.column(21).visible(false);
                                            //creditDataTable.column(22).visible(false);
                                            //creditDataTable.column(23).visible(false);
                                            //creditDataTable.column(24).visible(false);
                                            //creditDataTable.column(25).visible(false);
                                            //creditDataTable.column(26).visible(false);
                                            //creditDataTable.column(27).visible(false);
                                            //creditDataTable.column(28).visible(false);
                                            //creditDataTable.column(29).visible(false);
                                            //creditDataTable.column(30).visible(false);
                                            //creditDataTable.column(31).visible(false);
                                            //creditDataTable.column(32).visible(false);

                                        }
                                    })

                                }
                                ClearDebitModalInputs();
                                ClearDebitDivErrors();
                                $('#debit-modal').modal('hide');
                            }

                        });
                        return false;

                    },

                },

                //   EDIT   OPERATION
                {

                    text: '',
                    attr: {
                        id: 'btn-edit-debit-dt'
                    },
                    action: function (e, dt, node, config, String, indexes) {
                        debugger;
                        $('#debit-modal').find('#debit-fd').html('');
                        var arr = new Array();
                        $("#debit-data-table  input[type='checkbox']").each(function (index) {

                            var row = $(this).closest("tr");
                            selectedRow = debitDataTable.row(row).index();
                            var tdrow = (debitDataTable.row(selectedRow).data());
                            arr.push({
                                td0: tdrow[1]
                            });
                        });
                        var id = $("#debit-text").attr("id");
                        $('#' + id).html($('#' + id).html().replace('Add', 'Edit'));
                        $("#btn-add-debit-modal").hide();
                        $("#btn-update-debit-modal").show();
                        ClearDebitDivErrors();

                        var isChecked = $(".checks").is(":checked");

                        if (isChecked) {
                            debugger;
                            var columnValues = $('.btn-edit-debit').data('rowindex');
                            var id = $("#debit-modal").attr("id");
                            var myModal = $('#' + id).modal();
                            $("#person-id-debit").data('ui-autocomplete')._trigger('select', 'autocompleteselect', { item: { value: columnValues[1] } })
                            //$('#person-id-debit').data('Label', columnValues[2]);
                            $('#person-id-debit', myModal).val(columnValues[2]);
                            $("#debit-BusinessOffice-id", myModal).val(columnValues[3]).trigger("change");
                            $("#debit-general-ledger-id").append("<option value='" + columnValues[5] + "'>" + columnValues[6] + "</option>");
                            $('#debit-general-ledger-id option[value="' + columnValues[5] + '"]', myModal).prop("selected", true);
                            $("#debit-customer-account-id").append("<option value='" + columnValues[7] + "'>" + columnValues[8] + "</option>");
                            $('#debit-customer-account-id option[value="' + columnValues[7] + '"]', myModal).prop("selected", true);
                            $("#debit-transaction-amount").val(columnValues[9]);
                            $("#debit-start-number").val(columnValues[10]);
                            $("#debit-end-number").val(columnValues[11]);
                            $('#debit-note', myModal).val(columnValues[12]);
                            $('#debit-narration', myModal).val(columnValues[13]);
                            myModal.modal({ show: true });
                        }

                        else {
                            $('.btn-edit-debit').addClass('disabled');
                            $("#debit-modal").modal("hide");
                        }

                        // Hide Selected Dropdown Id Column
                        arr.map(function (obj) {

                            $('#person-id1').find("option[value='" + obj.td0 + "']").hide();

                        });

                        $('#btn-update-debit-modal').data('rowindex', columnValues);
                        $("#person-id-debit").data("ui-autocomplete").selectedItem.value = "";

                        // Modal Update Buttons Click Event - Call Event On Update Button Click
                        $(document).on('click', "#btn-update-debit-modal", function (event) {
                            debugger;
                            $('#select-all-debit').prop('checked', false);
                            // Get Modal Inputs In Local Variable
                            var person = "";
                            var personText = "";
                            var tag = '<input type="checkbox" name="check_all" class="checks"/>';
                            if ($("#person-id-debit").data("ui-autocomplete").selectedItem.value != "") {
                                person = $("#person-id-debit").data("ui-autocomplete").selectedItem.value;
                                personText = $("#person-id-debit").data("ui-autocomplete").selectedItem.label;
                            }
                            else {
                                var getdata = $('#btn-update-debit-modal').data('rowindex');
                                person = $("#person-id-debit").data("ui-autocomplete").selectedItem.value = getdata[1];
                                personText = $("#person-id-debit").data("ui-autocomplete").selectedItem.label = getdata[2];
                            }
                            var BusinessOffice = $("#debit-BusinessOffice-id option:selected").val();
                            var BusinessOfficeText = $("#debit-BusinessOffice-id option:selected").text();
                            var generalLedger = $("#debit-general-ledger-id option:selected").val();
                            var generalLedgerText = $("#debit-general-ledger-id option:selected").text();
                            var customerAccountId = $("#debit-customer-account-id option:selected").val();
                            var customerAccountIdText = $("#debit-customer-account-id option:selected").text();
                            var transactionAmount = $("#debit-transaction-amount").val().trim();
                            var startNumber = $("#debit-start-certificate-number").val();
                            var endNumber = $("#debit-end-certificate-number").val();
                            var note = $("#debit-note").val();
                            var narration = $("#debit-narration").val();

                            if (person.trim().length < 36 || (generalLedger.trim().length < 36) || (customerAccountId.trim().length < 36) || (transactionAmount == "") || (narration == "")) {
                                ClearDebitDivErrors();

                                if (person.trim().length < 36)
                                    $('#person-id-debit').after('<div class="error" style="color:red">Please Select Customer Name </div>');

                                if (generalLedger.trim().length < 36)
                                    $('#debit-general-ledger-id').after('<div class="error" style="color:red">Please Select General Ledger </div>');

                                if (customerAccountId.trim().length < 36)
                                    $('#debit-customer-account-id').after('<div class="error" style="color:red">Please Select Customer Aaccount </div>');

                                if (transactionAmount == "")
                                    $('#debit-transaction-amount').after('<div class="error" style="color:red">Please Select Transaction Amount </div>');

                                return false;
                            }
                            else {
                                debitDataTable.row($(this).attr('rowindex')).data([
                                    tag,
                                    person,
                                    personText,
                                    BusinessOffice,
                                    BusinessOfficeText,
                                    generalLedger,
                                    generalLedgerText,
                                    customerAccountId,
                                    customerAccountIdText,
                                    transactionAmount,
                                    startNumber,
                                    endNumber,
                                    note,
                                    narration,
                                    "None",
                                    "None",
                                    "None",
                                    "None",
                                    "None",
                                    "None",
                                ]).draw();

                                // Hide Id Column Of Datatable
                                debitDataTable.column(1).visible(false);
                                debitDataTable.column(3).visible(false);
                                debitDataTable.column(5).visible(false);
                                debitDataTable.column(7).visible(false);

                               
                                var transactiontype = $("#transaction-type-id option:selected").val();
                                if (transactiontype === "408d891d-244a-4b8f-b590-0e1b2a6d5afb") {
                                    $('#credit-data-table > tbody > tr').each(function (index) {
                                        var currentRow = $(this).closest('tr');
                                        var columnvalue = (creditDataTable.row(currentRow).data());
                                        if (typeof columnvalue != 'undefined' && columnvalue != null) {

                                            creditDataTable.cell(0, 14).data(total);
                                            $('#credit-amount').attr("data-id", total.toFixed(2));
                                            $('#credit-amount').html(total.toFixed(2));
                                            number3text(total);
                                        }
                                    })
                                }

                                // Update Datatable Row
                                debitDataTable.columns.adjust().draw();

                                $("#debit-modal").modal('hide');
                                $('.btn-add-debit-modal').removeClass('disabled');
                                $('.btn-add-debit').removeClass('disabled');
                                $('.btn-delete-debit').addClass('disabled');
                                $('.btn-edit-debit').addClass('disabled');
                                var columnValues = $('#btn-update-debit-modal').data('rowindex');
                                FromShowTradingEntityValues(columnValues)
                            }
                        })
                    },
                },

                //     DELETE   OPERATION
                {
                    text: '',
                    attr: {
                        id: 'btn-Delete-debit-dt'
                    },

                    action: function (e, dt, node, config) {

                        var isChecked = $("#debit-data-table tbody input[type='checkbox']").is(":checked");

                        if (isChecked) {
                            if (confirm("Are you sure to delete this row?")) {

                                if ($("#debit-data-table tbody input[type='checkbox']:checked").each(function () {

                                    debitDataTable.row($("#debit-data-table tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                                    var arr = $('.btn-delete-debit').data('rowindex');

                                    arr.map(function (obj) {


                                        $('#person-id1').find("option[value='" + obj.td0 + "']").show();
                                        $("#person-id1").prop("selectedIndex", 0);

                                    var transactiontype = $("#transaction-type-id option:selected").val();
                                    if (transactiontype === "408d891d-244a-4b8f-b590-0e1b2a6d5afb") {
                                    $('#credit-data-table > tbody > tr').each(function (index) {

                                        var currentRow = $(this).closest('tr');
                                        var columnvalue = (creditDataTable.row(currentRow).data());
                                        if (typeof columnvalue != 'undefined' && columnvalue != null) {

                                        if (total > 0) {

                                        creditDataTable.cell(0, 14).data(total);
                                        $('#credit-amount').attr("data-id", total.toFixed(2));
                                        $('#credit-amount').html(total.toFixed(2));
                                        number3text(total);
                                }
                                else {
                                           creditDataTable.row($(this).closest("tr").get(0)).remove().draw();
                                }
                                }
                                })
                                }
                                });
                                    $('.btn-add-debit-modal').removeClass('disabled');
                                    $('.btn-add-debit').removeClass('disabled');
                                    $('.btn-delete-debit  ').addClass('disabled');
                                    $('.btn-edit-debit').addClass('disabled');
                                    $('#select-all-debit').prop('checked', false);
                                }));
                            }
                        }


                        else {
                            alert("Please Select Any Record For Delete Operation.");

                        }
                    }
                },
        ],
        //"createdRow": function (row, data, index) {
        //    //var text = data[0];
        //    //alert(text);

        //   /// var data1 = JSON.parse(JSON.stringify(data));
        //    //debitDataTable.row.add(data1).draw();

        //    //$("table").closest('tbody tr td').find('input:checkbox').prop('disabled', true);
        //    // for (var i = 0; i < data.length; i++) {
        //    //$('th', row).eq(0).find("#select-all-debit").attr("disabled", true);

        //            //The above line assumes that you want to add a CSS class named "red" to a 
        //            //field that has the text "red" in it, if not, you can change the logic
        //        //}


        ////    //;
        ////    //debitDataTable.cell(row, 7).data(data).draw();
        ////    //$('td:eq(4)', row).html('<a class="text-primary" href="http://localhost/ci/uploaded/' + name + '/sharedFiles/' + data.file_name + '" target="_blank">Download</a>');
        ////    //var rowLength = debitDataTable.rows.length;
        ////    //alert(rowLength)

        ////    //var row = creditDataTable.row($(this).attr('rowindex'));
        ////    $('table#debit-data-table > tbody  > tr').each(function (index, tr) {
        ////        ;
        ////        var currentRow = $(this).closest('tr');
        ////        var columnvalue = (debitDataTable.row(currentRow).data());
        ////                  if (typeof columnvalue != 'undefined' && columnvalue != null) {
        ////                            //Remove the formatting to get integer data for summation
        ////                            //var intVal = function (i) {
        ////                            //    return typeof i === 'string' ?
        ////                            //        i.replace(/[\$,]/g, '') * 1 :
        ////                            //        typeof i === 'number' ?
        ////                            //        i : 0;
        ////                            //};
        ////                            //total = api
        ////                            //        .column(7)
        ////                            //        .data()
        ////                            //        .reduce(function (a, b) {
        ////                            //            return intVal(a) + intVal(b);
        ////                            //        }, 0);


        ////                            //var data1 = JSON.parse(JSON.stringify(total));
        ////                            debitDataTable.cell(currentRow, 7).data(data[7]).draw();
        ////                            //debitDataTable.cell(currentRow, 2).data("none").draw();
        ////                            //debitDataTable.cell(currentRow, 3).data("none").draw();
        ////                            //debitDataTable.cell(currentRow, 4).data("none").draw();
        ////                            //creditDataTable.row.add(data).draw();
        ////                            //creditDataTable.column(1).visible(false);
        ////                            //creditDataTable.column(3).visible(false);
        ////                      //creditDataTable.column(5).visible(false);
        ////                            return false;
        ////                        }
        ////                        else {
        ////                            $('td:eq(2)', row).html('none');
        ////                            $('td:eq(3)', row).html('none');
        ////                            $('td:eq(4)', row).html('none');
        ////                            $('td:eq(7)', row).html(data[7]);
        ////                        }
        ////    })


        ////    //var currentRow = $(this).closest('tr');
        ////    //var columnvalue = (debitDataTable.row(currentRow).data());
        ////    //if (typeof columnvalue != 'undefined' && columnvalue != null) {
        ////    //Remove the formatting to get integer data for summation
        ////    //var intVal = function (i) {
        ////    //    return typeof i === 'string' ?
        ////    //        i.replace(/[\$,]/g, '') * 1 :
        ////    //        typeof i === 'number' ?
        ////    //        i : 0;
        ////    //};
        ////    //total = api
        ////    //        .column(7)
        ////    //        .data()
        ////    //        .reduce(function (a, b) {
        ////    //            return intVal(a) + intVal(b);
        ////    //        }, 0);


        ////    //var data1 = JSON.parse(JSON.stringify(data[7]));
        ////    //if (row != 'undefined')
        ////    //    {
        ////    //    debitDataTable.cell('td:eq(7)', row).data(data[7]).draw();
        ////    //    }
        ////          //$('td:eq(7)', row).html(data[7]);
        ////          //debitDataTable.cell(currentRow, 2).data("none").draw();
        ////          //debitDataTable.cell(currentRow, 3).data("none").draw();
        ////          //debitDataTable.cell(currentRow, 4).data("none").draw();
        ////          //creditDataTable.row.add(data).draw();
        ////          //creditDataTable.column(1).visible(false);
        ////          //creditDataTable.column(3).visible(false);
        ////          //creditDataTable.column(5).visible(false);
        "footerCallback": function (row, data, start, end, display) {
            debugger;
            //debitDataTable.column(8).data("ewe");
            var transactiontype = $("#transaction-type-id option:selected").val();
            if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {
                debugger
                let total = creditDataTable.column(14).data();
                debittotal = total[0];
                if (typeof debittotal === 'undefined') {
                    $("#total-amount").attr('data-id', '0')
                    jQuery("#debit-amount").attr('data-id', '0');
                    $('#debit-amount').html("0.00");
                    $(".amountcard").hide();
                    Amountdifference();
                    number4text(debittotal);
                }
                else {

                    debitDataTable.cell(0, 9).data(debittotal);
                    $("#total-amount").attr('data-id', '0')
                    jQuery("#debit-amount").attr('data-id', debittotal);
                    $('#debit-amount').html(debittotal.toFixed(2));
                    $(".amountcard").hide();
                    Amountdifference();
                    number4text(debittotal);
                    //debitDataTable.column(8).data();
                }
            }
            else {
                total = this.api()
                  .column(9)
                  .data()
                  .reduce(function (a, b) {
                      return parseInt(a) + parseInt(b);
                  }, 0);
                var getdebittamount = $('#debit-amount').data("id");
                var debittotal = parseInt(total) + parseInt(getdebittamount);

                var transactiontype = $("#transaction-type-id option:selected").val();

                if (transactiontype === "408d891d-244a-4b8f-b590-0e1b2a6d5afb") {
                    if (total > 0) {
                        $("#total-amount").attr('data-id', '0')
                        jQuery("#debit-amount").attr('data-id', debittotal.toFixed(2));
                        $('#debit-amount').html(debittotal.toFixed(2));
                        $(".amountcard").hide();
                        number4text(debittotal);

                    }
                    else {
                        $("#total-amount").attr('data-id', '0')
                        jQuery("#debit-amount").attr('data-id', debittotal.toFixed(2));
                        $('#debit-amount').html(debittotal.toFixed(2));
                        $(".amountcard").hide();
                        number4text(debittotal);
                    }

                }
                else {
                    if (total > 0) {

                        jQuery("#debit-amount").attr('data-id', debittotal.toFixed(2));
                        $('#debit-amount').html(debittotal.toFixed(2));
                        $(".amountcard").show();
                        Amountdifference();
                        number4text(debittotal);
                    }
                    else {
                        jQuery("#debit-amount").attr('data-id', debittotal.toFixed(2));
                        $('#debit-amount').html(getdebittamount.toFixed(2));
                        Amountdifference();
                        number4text(debittotal);
                    }
                }
            }

        }

    })

    $('.close').click(function () {
        ClearDebitDivErrors();
        ClearDebitModalInputs();

        $('.btn-delete-debit').addClass('disabled');
        $('.btn-edit-debit').addClass('disabled');
        $('.checks').prop('checked', false);
        $('#select-all-debit').prop('checked', false);
        $('#select-all-credit').prop('checked', false);

    });


    var btns = $('#btn-add-debit-dt');
    btns.addClass('btn btn-success btn-add-debit btn-sm disabled').append('<i class="fas fa-plus icon"></i>');
    $("#btn-add-debit-dt").attr('title', 'Add');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-edit-debit-dt');
    btns.addClass('btn btn-primary btn-edit-debit disabled btn-sm').append('<i class="far fa-edit ml-2 icon"></i>');
    $("#btn-edit-debit-dt").attr('title', 'Edit');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    var btns = $('#btn-Delete-debit-dt');
    btns.addClass('btn btn-danger btn-delete-debit disabled btn-sm').append('<i class="fas fa-trash ml-2 icon"></i>');
    $("#btn-Delete-debit-dt").attr('title', 'Delete');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    $('#select-all-debit').on('click', function () {
        ;
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);
            var arr = new Array();
            $('#debit-data-table tbody input[type="checkbox"]').each(function () {
                ;
                $(this).prop('checked', true);
                var row = $(this).closest("tr");
                selectedRow = debitDataTable.row(row).index();
                var tdrow = (debitDataTable.row(selectedRow).data());
                arr.push({
                    td0: tdrow[1]
                });

                $('.btn-delete-debit').data('rowindex', arr);
                $('.btn-add-debit-modal').addClass('disabled');
                $('.btn-add-debit').addClass('disabled');
                $('.btn-edit-debit').addClass('disabled');
                $('.btn-delete-debit').removeClass('disabled');
            });
        }
        else {
            $('#debit-data-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
                $('.btn-add-debit-modal').removeClass('disabled');
                $('.btn-delete-debit').addClass('disabled');
                $('.btn-add-debit').removeClass('disabled');

            });
        }
    });

    // binding the change event-handler to the tbody:
    $('#debit-data-table tbody').on('change', function () {
        ;
        // getting all the checkboxes within the tbody:
        var all = $('#debit-data-table tbody input[type="checkbox"]'),
            // getting only the checked checkboxes from that collection:
            checked = all.filter(':checked');

        if (all.length > 0 == checked.length) {
            ;
            $('.btn-delete-debit').removeClass('disabled');

            $('.btn-edit-debit').removeClass('disabled');
        }
        else {
            $('.btn-edit-debit').addClass('disabled');


        }
        if (checked.length == 0) {
            $('.btn-delete-debit').addClass('disabled');
            $('.btn-add-debit').removeClass('disabled');

        }
        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#select-all-debit').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    $('#debit-data-table tbody').on('click', "input[type=checkbox]", function () {
        $('#debit-data-table tbody input[type="checkbox"]:checked').each(function () {

            var isChecked = $(this).prop("checked");

            if (isChecked) {
                var arr = new Array();

                $("#debit-data-table tbody input[type='checkbox']:checked").each(function (index) {

                    var row = $(this).closest("tr");
                    selectedRow = debitDataTable.row(row).index();
                    if (selectedRow >= 0) {
                        var tdrow = (debitDataTable.row(selectedRow).data());
                        arr.push({
                            td0: tdrow[1]
                        });

                        $('.btn-add-debit-modal').addClass('disabled');
                        $('.btn-add-debit').addClass('disabled');
                        $('.btn-edit-debit').removeClass('disabled');
                        $('.btn-delete-debit').removeClass('disabled');


                        $('#btn-update-debit-modal').attr('rowindex', selectedRow);
                        $('.btn-edit-debit').data('rowindex', tdrow);
                        $('.btn-delete-debit').data('rowindex', arr);
                        $('#select-all-debit').data('rowindex', arr);
                    }
                });
            }
        });
    });

    //To page load table each row get value & dropdown value Hide 
    $('#debit-data-table > tbody > tr').each(function () {
        ;

        var currentRow = $(this).closest("tr");
        var columnvalue = (debitDataTable.row(currentRow).data());
        if (typeof columnvalue != 'undefined' && columnvalue != null) {

            $('#frequency-id').find("option[value='" + columnvalue[0] + "']").hide();
        }
        else {
            return true;
        }

    });
    // let msg = { id: 1, customer_name: 'Fred' };  // source of updates (from backend)
    //let row = table.row('#row-' + msg.id);
    //let rowindex = row.index();
    //let columns = creditDataTable.settings().init().columns;
    //creditDataTable.columns().every(function (colindex) {
    //    ;
    //    let coldata = columns[colindex].data;  // 'data' as in the DT column definition
    //    if (coldata != 'id' && msg.hasOwnProperty(coldata)) {  // (don't update the id!)
    //        debitDataTable.cell({ row: rowindex, column: colindex }).data(msg[coldata])
    //    }
    //});
    //To clear input filed & selected dropdown value Hide 
    // Clear DataTable Inputs
    // To dropdown added values by edit
    function FromShowTradingEntityValues(columnValues) {
        $('#person-id1').find("option[value='" + columnValues[1] + "']").show();
        return false;
    }
    function Amountdifference1() {
        var creditamount = $("#credit-amount").attr('data-id');
        var debitamount = $("#debit-amount").attr('data-id');
        var denominationamount = $("#denomination-amount").attr('data-id');
        var diffamount = 0;
        if (Math.sign(creditamount) === Math.sign(denominationamount)) {
            if (creditamount > denominationamount) {

                diffamount = (Math.abs(creditamount - denominationamount));
                $('#total-amount').html(diffamount.toFixed(2));
                $('#total-amount').attr('data-id', diffamount.toFixed(2));

                //numberToWords(diffamount);
                //number2text(diffamount)

                //if (diffamount == 0) {

                //    $(".amountcard").hide();
                //}
            }

        } else {

            var diffamount = Math.abs(creditamount) + Math.abs(denominationamount);
            $('#total-amount').attr('data-id', diffamount.toFixed(2));
            $('#total-amount').html(diffamount.toFixed(2));
            // numberToWords(diffamount);
            //number2text(diffamount)
        };


    }
    function Amountdifference() {

        var creditamount = $("#credit-amount").attr('data-id');
        var debitamount = $("#debit-amount").attr('data-id');
        var diffamount = 0;

        if (parseFloat(creditamount) > parseFloat(debitamount)) {

            $('#deff-amount').find("h4,i,span").addClass("text-danger");
        }
        else {
            $('#deff-amount').find("h4,i,span").addClass("text-success").removeClass("text-danger");

        }
        if (Math.sign(creditamount) === Math.sign(debitamount)) {

            diffamount = (Math.abs(creditamount - debitamount));
            $('#total-amount').html(diffamount.toFixed(2));
            $('#total-amount').attr('data-id', diffamount.toFixed(2));

            //numberToWords(diffamount);
            number2text(diffamount)

            if (diffamount == 0) {

                $(".amountcard").hide();
            }

        } else {

            var diffamount = Math.abs(creditamount) + Math.abs(debitamount);
            $('#total-amount').attr('data-id', diffamount.toFixed(2));
            $('#total-amount').html(diffamount.toFixed(2));
            // numberToWords(diffamount);
            number2text(diffamount)
        };


    }
    function ClearDebitModalInputs() {

        $("#person-id-debit").val('');
        $("#debit-BusinessOffice-id").val('');
        $("#debit-general-ledger-id").prop("selectedIndex", 0);
        $("#debit-customer-account-id").prop("selectedIndex", 0);
        $("#debit-transaction-amount").val('');
        $("#debit-start-certificate-number").val('');
        $("#debit-end-certificate-number").val('');
        $("#narration-debit").val('');
    }

    // Clear Div Errors 
    function ClearDebitDivErrors() {

        $('#person-id-debit').next("div.error").remove();
        $('#general-ledger-id-debit').next("div.error").remove();
        $('#customer-account-id-debit').next("div.error").remove();
        $('#customer-account-transaction-amount').next("div.error").remove();
        $("#debit-start-number").next("div.error").remove();
        $("#debit-end-number").next("div.error").remove();
        $("#narration-debit").next("div.error").remove();

    }

    //*************************************

    // Clear Denomination Modal Input Values
    ClearDenominationModalInputs();

    // @@@@@@@@  BusinessOfficeCurrency Starting DataTable Code - Initialising & Configuring DataTables
    // Clear Denomination Modal Input Values
    ClearDenominationModalInputs();

    var array = new Array();
    // @@@@@@@@  BusinessOfficeCurrency Starting DataTable Code - Initialising & Configuring DataTables
    denominationDataTable = $('#denomination-data-table').DataTable(
       {
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
           "lengthMenu": [[10, 25, 50, -1],
                         [10, 25, 50, "All"]],
           "aaSorting": [],
           columnDefs: [{
               targets: 0,
               orderable: false,
           }],
           "columnDefs":
               [{
                   "targets": [1, 3],
                   "visible": false
               }],
           "order":
               [[
                   1, 'asc'
               ]],
           //"columnDefs": [{
           //    targets: 4,
           //    render: $.fn.dataTable.render.percentBar('round', '#fff', '#FF9CAB', '#FF0033', '#FF9CAB', 0, 'solid')
           //}],
           columnDefs:
               [{
                   targets: 4,
                   "render": function (data, type, full, meta, row, indexes) {

                       var transactionamount = parseFloat($("#credit-amount").attr("data-id"));
                       var transactiontype = $("#transaction-type-id option:selected").val();
                       total = (full[2]) * (full[3]);
                       if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {

                           full[4] = ("" + total + "");
                           return total;
                       }
                       else {

                           full[4] = ("" + total + "");
                           return total;
                       }
                   }
               }],
           //initComplete: function (settings, json) {

           //   denominationDataTable.columns.adjust();
           //   denominationDataTable.fixedHeader.adjust();
           //},

           //columnDefs: [{
           //    render: function (data, type, row) {
           //        ;
           //        if (type === 'display') {
           //            alert("ok");
           //            console.log(row.mws_name);
           //        }
           //    }
           //}],

           dom: 'Bfrtip',
           dom: '<"float-left"B><"float-right"f>rt<"row"<"col-sm-4"l><"col-sm-4"i><"col-sm-4"p>>',

           buttons:
               [
                   //  NEW    OPERATION
                   {
                       text: 'New',
                       attr: { id: 'btn-add-denomination-dt' },
                       action: function (e, dt, node, config, type) {
                           event.preventDefault();

                           // Getting Only Current Page Rows Issue - To Get All Rows From Data Table
                           denominationDataTable.page.len(-1).draw();

                           var denominationModalTitleId = $("#denomination-text").attr("id");

                           $('#' + denominationModalTitleId).html($('#' + denominationModalTitleId).html().replace('Edit', 'Add'));

                           $("#btn-update-denomination-modal").hide();

                           $("#btn-add-denomination-modal").show();

                           ClearDenominationModalInputs();
                           ClearDenominationDivErrors();

                           $('#denomination-modal').modal('show');

                           // On Click Add / New Button Of Modal
                           $('#btn-add-denomination-modal').on('click', function (event) {

                               // Get Modal Inputs In Local Variable
                               var transactionamount = parseFloat($("#credit-amount").attr("data-id"));
                               var denominationamount = parseFloat($("#denomination-amount").attr("data-id"));
                               var htmlTagForCheckboxWithSelectAll = '<input type="checkbox" name="select-all" class="checks" />';
                               var denomination = $("#denomination-id option:selected").val();
                               var denominationText = $("#denomination-id option:selected").text();
                               var pieces = $("#pieces-cash-denomination").val();
                               var denominationpieces = $("#pieces-denomination-maultiple").val();

                               var note = $("#note-cash-denomination").val();

                               // Validate All Modal Inputs
                               if (denomination.trim().length < 36 || (pieces == "")) {
                                   ClearDenominationDivErrors();

                                   if (denomination.trim().length < 36)
                                       $('#denomination-id').after('<div class="error" style="color:red">Please Select Denomination </div>');

                                   if (pieces == "")
                                       $('#pieces').after('<div class="error" style="color:red">Please Enter Pieces </div>');

                                   return false;
                               }
                               else {
                                   if (transactionamount <= denominationamount) {
                                       pieces = ("" + -pieces + "");
                                   }
                                   denominationDataTable.row.add(
                                    [
                                          htmlTagForCheckboxWithSelectAll,
                                          denomination,
                                          denominationText,
                                          pieces,
                                          "",
                                          note
                                    ]).draw();

                                   //denominationDataTable.rows(function (idx, data, node) {
                                   //    return data[0] === 4;
                                   //}).remove().draw();




                                   denominationDataTable.column(1).visible(false);
                                   //denominationDataTable.column(4).visible(false);
                                   denominationDataTable.columns.adjust().draw();

                                   ClearDenominationModalInputs();
                                   ClearDenominationDivErrors();

                                   $('#denomination-modal').modal('hide');

                                   //$(".checks").css({ "margin-left": "30px" });
                                   //$("#selectAll-denomination").css({ "margin-right": "30px" });
                               }
                           });

                           return false;
                       },
                   },

                   //    EDIT   OPERATION
                   {
                       text: 'Edit',
                       attr: { id: 'btn-edit-denomination-dt' },
                       action: function (e, dt, node, config, String, indexes) {
                           // Single Page Rows Issue - To Get All Rows From Data Table
                           denominationDataTable.page.len(-1).draw();
                           ;
                           var arr = new Array();

                           // Get All Datatable Rows In Array
                           $("#denomination-data-table input[type='checkbox']").each(function (index) {
                               var row = $(this).closest("tr");

                               selectedRow = denominationDataTable.row(row).index();

                               var tdrow = (denominationDataTable.row(selectedRow).data());

                               arr.push({ td0: tdrow[1] });
                           });

                           var denominationModalTitleId = $("#denomination-text").attr("id");

                           $('#' + denominationModalTitleId).html($('#' + denominationModalTitleId).html().replace('Add', 'Edit'));

                           $("#btn-add-denomination-modal").hide();
                           $("#btn-update-denomination-modal").show();

                           ClearDenominationDivErrors();
                           var transactionamount = parseFloat($("#credit-amount").attr("data-id"));
                           var denominationamount = parseFloat($("#denomination-amount").attr("data-id"));

                           // To Check Whether Current Row Is Selected For Edit Or Not?
                           var isSelected = $(".checks").is(":checked");

                           // If Selected
                           if (isSelected) {
                               var columnValues = $('.denomination-btn-edit').data('rowindex');
                               var denominationModalId = $("#denomination-modal").attr("id");
                               var denominationModal = $('#' + denominationModalId).modal();


                               // Display Value In Modal Inputs
                               $('#denomination-id', denominationModal).val(columnValues[1]);
                               var sign = columnValues[3] > 0 ? "+" : "-";
                               if (sign === "-") {
                                   var val = columnValues[3].split('-');
                                   $('#pieces-cash-denomination', denominationModal).val(val[1]);

                               }
                               else {

                                   $('#pieces-cash-denomination', denominationModal).val(columnValues[3]);
                               }
                               $('#note-cash-denomination', denominationModal).val(columnValues[5]);

                               // Show Modals
                               denominationModal.modal({ show: true });
                           }
                           else {
                               $('.denomination-btn-edit').addClass('disabled');
                               $("#denomination-modal").modal("hide");
                           }

                           // Hide Selected Dropdown Id Column
                           arr.map(function (obj) {
                               $('#denomination-id').find("option[value='" + obj.td0 + "']").hide();
                           });

                           // Assign Updated Data To Update Button
                           var row = $('#btn-update-denomination-modal').data('rowindex', columnValues);

                           // Call Event On Update Button Click Of Modal
                           $(document).on('click', "#btn-update-denomination-modal", function (event) {
                               ;


                               $('#denomination-select-all-chkbox').prop('checked', false);

                               // Get Modal Inputs In Local Variable
                               var htmlTagForCheckboxWithSelectAll = '<input type="checkbox" name="select-all" class="checks"/>';
                               var denomination = $('#denomination-id option:selected').val();
                               var denominationText = $('#denomination-id option:selected').text();
                               var pieces = $("#pieces-cash-denomination").val().trim();
                               var note = $("#note-cash-denomination").val();

                               var transactionamount = parseFloat($("#credit-amount").attr("data-id"));
                               var debittransactionamount = parseFloat($("#debit-amount").attr("data-id"));
                               var denominationamount = parseFloat($("#denomination-amount").attr("data-id"));

                               // Validate Modal Inputs
                               if (denomination.trim().length < 36 || (pieces == "")) {
                                   ClearDenominationDivErrors();

                                   if (denomination.trim().length < 36)
                                       $('#denomination-id').after('<div class="error" style="color:red">Please Select denomination </div>');

                                   if (pieces == "")
                                       $('#pieces').after('<div class="error" style="color:red">Please Enter Pieces </div>');

                                   return false;
                               }
                               else {
                                   if (transactionamount <= denominationamount) {
                                       pieces = ("" + -pieces + "");
                                   }

                                   ;
                                   var row1 = denominationDataTable.row($(this).attr('rowindex')).data(
                                        [
                                            htmlTagForCheckboxWithSelectAll,
                                            denomination,
                                            denominationText,
                                            pieces,
                                            "",
                                            note
                                        ]).draw();
                                   ;
                                   //  var data = JSON.parse(JSON.stringify(row1.data()));
                                   //var toReturn;
                                   // $.each(data, function (i, value) {
                                   //    ;
                                   //    toReturn = parseFloat(value[4]);
                                   // });
                                   //   $('#denomination-data-table > tbody > tr').each(function (index) {
                                   //       ;
                                   //       var currentRow = $(this).closest('tr');
                                   //       var columnvalue = (denominationDataTable.row(currentRow).data());
                                   //       if (typeof columnvalue != 'undefined' && columnvalue != null) {

                                   //           denominationDataTable.cell(currentRow, 4).data(columnvalue[4]);
                                   //       }
                                   //})
                                   //denominationDataTable.cell(row, 4).data(transactionamount);
                                   // Hide Id Column Of Datatable
                                   denominationDataTable.column(1).visible(false);

                                   // Update Datatable Row
                                   denominationDataTable.columns.adjust().draw();

                                   $('#denomination-modal').modal('hide');

                                   $('.denomination-btn-add').removeClass('disabled');
                                   $('.denomination-btn-delete').addClass('disabled');
                                   $('.denomination-btn-edit').addClass('disabled');
                               }
                           })
                       },
                   },

                   //     DELETE   OPERATION
                   {
                       text: 'Delete',
                       attr: { id: 'btn-delete-denomination-dt' },
                       action: function (e, dt, node, config) {
                           // Get Selected Row For Delete
                           var isSelected = $("#denomination-data-table input[type='checkbox']").is(":checked");

                           if (isSelected) {
                               if (confirm("Are You Sure To Delete This Row?")) {
                                   if ($("#denomination-data-table input[type='checkbox']:checked").each(function () {
                                       denominationDataTable.row($("#denomination-data-table input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                                       var arr = $('.denomination-btn-delete').data('rowindex');

                                       // Show Deleted Dropdown Id In Dropdown List
                                       arr.map(function (obj) {
                                           $('#denomination-id').find("option[value='" + obj.td0 + "']").show();
                                           $("#denomination-id").prop("selectedIndex", 0);
                                   });

                                       $('.denomination-btn-add').removeClass('disabled');

                                       $('.denomination-btn-edit').addClass('disabled');
                                       $('.denomination-btn-delete').addClass('disabled');

                                       $('#denomination-select-all').prop('checked', false);
                                   }));
                               }
                           }
                           else {
                               alert("Please Select Any Record For Delete Operation.");
                           }
                       }
                   },
               ],
           //"rowCallback": function( row, data, index ) {
           //    //alert(data.file_name);
           //    ;

           //    var total1 = ((data[2]) * (data[3]));
           //    return total1;
           //    //var api = this.api(), data;
           //    ////// Remove the formatting to get integer data for summation
           //    //var intVal = function (i) {
           //    //    return typeof i === 'string' ?
           //    //        i.replace(/[\$,]/g, '') * 1 :
           //    //        typeof i === 'number' ?
           //    //        i : 0;
           //    //};
           //    //total = api
           //    //  .column(4)
           //    //  .data()
           //    //  .reduce(function (a, b) {
           //    //      return intVal(a) + intVal(b);
           //    //  }, 0);

           //    //debitDataTable.cell(row, 7).data(data).draw();
           //    //$('td:eq(4)', row).html( '<a class="text-primary" href="http://localhost/ci/uploaded/'+name+'/sharedFiles/'+data.file_name+'" target="_blank">Download</a>');
           //},
           //"createdRow": function (row, data, full, index) {
           //    ;
           //    var api = this.api(), data;
           //    //// Remove the formatting to get integer data for summation
           //    var intVal = function (i) {
           //        return typeof i === 'string' ?
           //            i.replace(/[\$,]/g, '') * 1 :
           //            typeof i === 'number' ?
           //            i : 0;
           //    };
           //    total = api
           //      .column(4)
           //      .data()
           //      .reduce(function (a, b) {
           //          return intVal(a) + intVal(b);
           //      }, 0);


           //    var denomination = api.column(2).data().toArray();
           //    var pices = api.column(3).data().toArray();
           //    var denominationtotal = 0;

           //    for (i = 0; i < denomination.length; i++) {
           //        denominationtotal += denomination[i] * pices[i];
           //    }

           //    var transactionamount = parseFloat($("#credit-amount").attr("data-id"));
           //    var debittransactionamount = parseFloat($("#debit-amount").attr("data-id"));
           //    var diffamount = parseFloat($("#total-amount").data("id"));

           //        }
           //        //else {

           //        //denominationDataTable.rows($(row)).remove().draw();
           //        //}

           //    }
           //    else {
           //        if ((denominationtotal <= debittransactionamount) && (diffamount == 0)) {
           //        }
           //        //} else {


           //        //    denominationDataTable.rows($(row)).remove().draw();
           //        //}
           //    }
           //},
           "footerCallback": function (row, data, start, end, display, event, index, e) {

               var api = this.api(), data;

               //// Remove the formatting to get integer data for summation
               var intVal = function (i) {
                   return typeof i === 'string' ?
                       i.replace(/[\$,]/g, '') * 1 :
                       typeof i === 'number' ?
                       i : 0;
               };

               total = api
                 .column(4)
                 .data()
                 .reduce(function (a, b) {
                     return intVal(a) + intVal(b);
                 }, 0);

               var denomination = api.column(2).data().toArray();
               var pices = api.column(3).data().toArray();

               var denominationtotal = 0;

               for (i = 0; i < denomination.length; i++) {

                   denominationtotal += denomination[i] * pices[i];
               }
               var resulty = Math.abs(denominationtotal); //  12
               var transactionamount = parseFloat($("#credit-amount").attr("data-id"));
               var debittransactionamount = parseFloat($("#debit-amount").attr("data-id"));
               var diffamount = parseFloat($("#total-amount").data("id"));

               var transactiontype = $("#transaction-type-id option:selected").val();

               if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {


                   if ((resulty <= transactionamount) && (diffamount == 0)) {


                       if (resulty > 0) {

                           jQuery("#denomination-amount").attr('data-id', resulty.toFixed(2));
                           $('#denomination-amount').html(resulty.toFixed(2));
                           $(".amountcard").hide();
                           //Amountdifference();
                           number5text(resulty)
                           //Amountdifference1();

                       }
                       else {

                           jQuery("#denomination-amount").attr('data-id', resulty.toFixed(2));
                           $('#denomination-amount').html(resulty.toFixed(2));
                           //Amountdifference();
                           number5text(resulty);
                       }

                   }
                   else {
                       if (denominationtotal > 0) {

                           jQuery("#denomination-amount").attr('data-id', resulty.toFixed(2));
                           $('#denomination-amount').html(resulty.toFixed(2));
                           $(".amountcard").hide();
                           // Amountdifference();
                           number5text(resulty)

                       }
                       else {

                           jQuery("#denomination-amount").attr('data-id', resulty.toFixed(2));
                           $('#denomination-amount').html(resulty.toFixed(2));
                           // Amountdifference();
                           number5text(resulty)
                       }
                   }

               }
               else {
                   if ((resulty <= transactionamount) && (diffamount == 0)) {

                       var getcreditamount = $('#denomination-amount').data("id");
                       //var credittotal = parseInt(denominationtotal) + parseInt(getcreditamount);


                       if (resulty > 0) {

                           jQuery("#denomination-amount").attr('data-id', resulty.toFixed(2));
                           $('#denomination-amount').html(resulty.toFixed(2));
                           //$(".amountcard").show();
                           number5text(resulty)
                           Amountdifference();

                       }
                       else {

                           jQuery("#denomination-amount").attr('data-id', resulty.toFixed(2));
                           $('#denomination-amount').html(resulty.toFixed(2));
                           number5text(resulty)
                           Amountdifference();
                       }

                   }
                   else {
                       if (resulty > 0) {

                           jQuery("#denomination-amount").attr('data-id', resulty.toFixed(2));
                           $('#denomination-amount').html(resulty.toFixed(2));
                           $(".amountcard").hide();
                           // Amountdifference();
                           number5text(resulty)

                       }
                       else {

                           jQuery("#denomination-amount").attr('data-id', resulty.toFixed(2));
                           $('#denomination-amount').html(resulty.toFixed(2));
                           // Amountdifference();
                           number5text(resulty)
                       }
                   }

               }

           }

       })
    //var sum = denominationDataTable.cells(function (index, data, node) {
    //    return denominationDataTable.row(index).data()[4] === '2000000' ?
    //        true : false;
    //}, 0, { search: 'applied' })
    //         .data()
    //         .reduce();
    //$('#assignVender').on('click', 'tr', function () {
    //    $(this).toggleClass('selected');
    //});

    //$('#denomination-data-table tbody').on('click', 'tr', function () {
    //    $(this).toggleClass('selected');
    //});
    // Close Button Click Event - On Close Button Click Clear All Modal Inputs
    $('.close').click(function () {
        ClearDenominationDivErrors();
        ClearDenominationModalInputs();

        $('.denomination-btn-delete').addClass('disabled');
        $('.denomination-btn-edit').addClass('disabled');

        $('.checks').prop('checked', false);
        $('#denomination-select-all-chkbox').prop('checked', false);

        $('.denomination-btn-add').removeClass('disabled');
    });

    // Edit Operation - Display Hidden Record While Editing
    function DisplayFundGLIdWhileEditing(columnValues) {
        $('#denomination-id').find("option[value='" + columnValues[1] + "']").show();

        return false;
    }

    // After Update Operation - Flash Color Which One Updated
    function FlashColorOfUpdatedRecord() {
        var updatedRecordId = $("input[type='checkbox']:checked").closest('tr').attr("id");

        $("#" + updatedRecordId).animate(
            {
                backgroundColor: "#B0C4DE"
            }, 500).animate(
                {
                    backgroundColor: "#F5F5F5"
                }, 500);
    }

    // Css For Create/Add/New Button
    var btns = $('#btn-add-denomination-dt');
    btns.addClass('btn btn-success denomination-btn-add disabled').append('<i class="fas fa-plus icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    // Css For Edit Button
    var btns = $('#btn-edit-denomination-dt');
    btns.addClass('btn btn-primary denomination-btn-edit disabled').append('<i class="far fa-edit ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    // Css For Delete Button
    var btns = $('#btn-delete-denomination-dt');
    btns.addClass('btn btn-danger denomination-btn-delete disabled').append('<i class="fas fa-trash ml-2 icon"></i>');
    $(".icon").css({ 'float': 'left', "margin-right": "6px", "margin-top": "5px" });
    btns.removeClass('dt-button');

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#denomination-select-all').on('click', function () {
        if ($(this).prop('checked')) {
            $(this).prop('checked', true);

            var arr = new Array();

            $('#denomination-data-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                var row = $(this).closest("tr");

                selectedRow = denominationDataTable.row(row).index();

                var tdrow = (denominationDataTable.row(selectedRow).data());

                arr.push({ td0: tdrow[1] });

                // Assign Data To Delete Button
                $('.denomination-btn-delete').data('rowindex', arr);

                // Inactivate Add And Edit Button
                $('.denomination-btn-add').addClass('disabled');
                $('.denomination-btn-edit').addClass('disabled');

                // Activate Delete Button
                $('.denomination-btn-delete').removeClass('disabled');
            });
        }
        else {
            $('#denomination-data-table tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);

                $('.denomination-btn-add').removeClass('disabled');

                $('.denomination-btn-delete').addClass('disabled');
            });
        }
    });

    // Binding The Change Event-Handler To The Datatable Tbody:
    $('#denomination-data-table tbody').on('change', function () {
        // getting all the checkboxes within the tbody:
        var all = $('tbody input[type="checkbox"]'),
            // getting only the checked checkboxes from that collection:
            checked = all.filter(':checked');

        if (all.length > 0 == checked.length) {
            ;
            $('.denomination-btn-Delete').removeClass('disabled');
            $('.denomination-btn-Edit').removeClass('disabled');
        }
        else {
            $('.denomination-btn-Edit').addClass('disabled');

        }
        if (checked.length == 0) {
            $('.denomination-btn-Delete').addClass('disabled');
            $('.denomination-btn-add').removeClass('disabled');
        }

        // setting the checked property of toggleCheckbox to true, or false
        // according to whether the number of checkboxes is greater than 0;
        // if it is, we use the assessment to determine true/false,
        // otherwise we set it to false (if there are no checkboxes):
        $('#denomination-select-all-chkbox').prop('checked', all.length > 0 ? all.length === checked.length : false);
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#denomination-data-table tbody').on('click', "input[type=checkbox]", function () {
        $('#denomination-data-table input[type="checkbox"]:checked').each(function () {
            var isSelected = $(this).prop("checked");

            if (isSelected) {
                var arr = new Array();

                $("input[type='checkbox']:checked").each(function (index) {
                    var row = $(this).closest("tr");

                    selectedRow = denominationDataTable.row(row).index();

                    var tdrow = (denominationDataTable.row(selectedRow).data());

                    arr.push({ td0: tdrow[1] });

                    $('.denomination-btn-add').addClass('disabled');
                    $('.denomination-btn-edit').removeClass('disabled');
                    $('.denomination-btn-delete').removeClass('disabled');

                    $('#btn-update-denomination-modal').attr('rowindex', selectedRow);
                    $('.denomination-btn-edit').data('rowindex', tdrow);
                    $('.denomination-btn-delete').data('rowindex', arr);
                    $('#denomination-select-all-chkbox').data('rowindex', arr);
                });
            }
        });
    });


    //To clear input filed & selected dropdown value Hide 
    // Clear BusinessOfficeCurrency DataTable Inputs
    function ClearDenominationModalInputs() {
        $("#denomination-id").val('');
        $("#pieces-cash-denomination").val('');
        $("#note-cash-denomination").val('');
    }

    // Clear BusinessOfficeCurrency Div Errors
    function ClearDenominationDivErrors() {
        $('#denomination-id').next("div.error").remove();
        $('#pieces-cash-denomination').next("div.error").remove();
        $('#note-cash-denomination').next("div.error").remove();
    }

});
//autocomplete person-id-credit/person-id-debit
$(function () {
    
    //    if ($(this).val() === '') {
    //        ClearCreditModalInputs();
    //    }
    //});

    $("#credit-person-id").autocomplete(
    {

        source: function (request, response) {
            $.post(GetPersonAutoCompleteList, { _inputString: request.term, async: false }, function (data) {
                var result = [];
                var arry1 = [];
                if (data.length > 0) {
                    var resp =$.map(data, function (item, i) {
                           
                        var arry = item.split('-');
                        var NameOfCustomerType = arry[3];
                        //var TransctionNumber;
                        //arry.push(TransctionNumber);
                        //var TransctionNumber1 = arry[11];
                        //var NameOfJointAccountHolderType = arry[10];
                        //var fullname =arry[0] + " --> " + arry[1]+ '<li>'+ arry[10] +'</li>'
                        var fullname = arry[0] + " --> " + arry[1];
                        var PersonId = arry[4] + "-" + arry[5] + "-" + arry[6] + "-" + arry[7] + "-" + arry[8];

                        if (PersonId === "5293c8ec-64a3-44f1-9e5f-beb72aeab9e9") {
                            var TransctionNumber1 = 5;
                            arry1.push(TransctionNumber1);
                        }
                        if (PersonId === "3b7b055d-27db-42ec-9209-504222eea0fb") {
                            var TransctionNumber2 = 3;
                            arry1.push(TransctionNumber2);
                        }
                        if (PersonId === "c8b75020-9d9c-4f1b-b3f0-a0d38e28788a") {
                            var TransctionNumber3 = 2;
                            arry1.push(TransctionNumber3);
                        }
                        if (PersonId === "3a059746-d2c9-456f-86e7-5d71aa0e0028") {
                            var TransctionNumber4 = 1;
                            arry1.push(TransctionNumber4);
                        }
                        var trannumber1 = arry1[0];
                        var trannumber2 = arry1[1];
                        var trannumber3 = arry1[0];
                        var trannumber4 = arry1[1];

                        return { label: fullname, value: PersonId, NameOfCustomerType, trannumber1, trannumber2, trannumber3, trannumber4 }

                    });
                    response(resp.slice(0, 4));
                }
                else {
                    response([{ label: 'No Records Found', value: -1 }]);
                }

            })
        },

        focus: function (event, ui) {

            event.preventDefault();
            var tt = ui.item.label.split("<li>");
            //var array = tt.split(',');
            $(this).val(tt);
        },
        appendTo: "#credit-modal",
        minLength: 3,
        scroll: true,
        autoFocus: true,
        search: function(e,ui){
            $(this).data("ui-autocomplete").menu.bindings = $();
        },
        select: function (event, ui, item) {
            debugger;
            event.preventDefault();
            //$(this).val(ui.item.label);
            //.. this.value = (ui.item.label ? ui.item.label: '');
            // var arry = item.split('-');
            $("#credit-person-id").val(ui.item.label);
            //$("#badge-text").html(ui.item.TransctionNumber1);
            var PersonId = ui.item.value;
            $('input[id^=credit-]').trigger("change");
        
            //$.ajax({
            //    url: GetMemberType,
            //    //data: JSON.stringify({ _personId: _personId }),
            //    data: "{ '_personId': '" + PersonId + "'}",
            //    type: "post",
            //    async: true,
            //    cache: false,
            //    dataType: "json",
            //    contentType: "application/json; charset=utf-8",
            //    success: function (data) {
            //        debugger;
            //        var tt = JSON.stringify(data);
            //        alert(tt);
            //        if (data != null) {
            //            $("#badge-text").html(data);
            //        }
            //        else {
            //            $("#badge-text").html("None");
            //        }
                    
            //    },
            //})
            ////}).done(function(){
            //    $.ajax({
            //        url: GetIsVIPCustomer,
            //        data: "{ '_personId': '" + PersonId + "'}",
            //        type: "post",
            //        dataType: "json",
            //        async: true,
            //        cache: false,
            //        contentType: "application/json; charset=utf-8",
            //        success: function (data) {
            //            debugger;
            //            var text = "";
            //            if (data == true)
            //            {
            //                $(".amount-badge-text").html('');

            //                text += '<span class="badge badge-pill badge-success float-right text-white badge-text" id="badge-amount" data-amount="5000"><i class="fa fa-inr text-white fa-1x mr-1" aria-hidden="true"></i>5000</span>';
            //                $(".amount-badge-text").append(text);
            //            }
            //            else {
            //                $(".amount-badge-text").html('');
                            
            //                text += '<span class="badge badge-pill badge-success float-right text-white badge-text" id="badge-amount" data-amount="******">******</span>';
            //                $(".amount-badge-text").append(text);
            //            }
            //        },
            //    });
           // })

            //$.post("/CustomerSharesCapitalAccountJQ/GetPersonNameAutoComplete", { _inputString: request.term, async: false }, function (data) {
            $.post(GetMemberType, { "_personId": PersonId, async: false }, function (data) {
                debugger;

                if (data != "")
                    {

                    $("#badge-text").html(data);
                }
                else {
                    $("#badge-text").html("None");
                }

            });

            //$.post(GetIsVIPCustomer, { "_personId": PersonId, async: false }, function (data) {
            //    debugger;
            //    var text = "";
            //    if (data == true) {
            //        $(".amount-badge-text").html('');

            //        text += '<span class="badge badge-pill badge-success float-right text-white badge-text" id="badge-amount" data-amount="5000"><i class="fa fa-inr text-white fa-1x mr-1" aria-hidden="true"></i>5000</span>';
            //        $(".amount-badge-text").append(text);
            //    }
            //    else {
            //        $(".amount-badge-text").html('');

            //        text += '<span class="badge badge-pill badge-success float-right text-white badge-text" id="badge-amount" data-amount="******">******</span>';
            //        $(".amount-badge-text").append(text);
            //    }
            //});
            //$.ajax({

            //    url: GetIsVIPCustomer,
            //    dataType: "json",
            //    type: "POST",
            //    cache: true,
            //    async: true,
            //    data: ({ _personId: PersonId }),
            //    success: function (data) {
            //        debugger
            //        var srs = data.Result;
            //        $('input[name="IsVisibleBalance"][value="' + srs + '"]').prop("checked", true);
            //        }
            //      })

            //$.post(GetIsVIPCustomer, { "_personId": PersonId, async: false }, function (data) {
            //    debugger;
                
            //    var srs = data.Result;
            //    $('input[name="IsVisibleBalance"][value="' + srs + '"]').prop("checked", true);
            //    //$('#vehicleChkBox input[value="' + srs + '"]').prop('checked', 'checked');
            //    //$('input.type_checkbox[value="' + srs + '"]');
                

            //});            ////if (ui.item.value === "5293c8ec-64a3-44f1-9e5f-beb72aeab9e9") {
            ////   // $("#badge-text").html(ui.item.trannumber1);
            ////}
            ////if (ui.item.value === "3b7b055d-27db-42ec-9209-504222eea0fb") {
            ////    //$("#badge-text").html(ui.item.trannumber2);
            ////}
            ////if (ui.item.value === "c8b75020-9d9c-4f1b-b3f0-a0d38e28788a") {
            ////    //$("#badge-text").html(ui.item.trannumber3);
            ////}
            ////if (ui.item.value === "3a059746-d2c9-456f-86e7-5d71aa0e0028") {
            ////   // $("#badge-text").html(ui.item.trannumber4);
            ////}
            ////$('input[name="TransactionCustomerAccountViewModel.PersonId"]').val(ui.item.value);
           // call_1(PersonId);
            
            return false;
            //$.post(AuthorizedGeneralLedger, { _businessOfficePrmKey :_businessOfficePrmKey,personId: PersonId, }, function (data) {
            //    var items = '<option value="0">Select General Ledger</option>';
            //    $.each(data, function (i, generalLedger) {
            //        items += "<option value='" + generalLedger.Value + "'>" + generalLedger.Text + "</option>";
            //    });
            //    $('#credit-general-ledger-id').html(items);
            //    //$("#general-ledger-id-credit").prop("selectedIndex", 1);

            //});

            //$.post(GetGetAccountNumberUrl, { personId: PersonId }, function (data) {
            //    var items = '<option value="0">Select General Ledger</option>';
            //    $.each(data, function (i, generalLedger) {
            //        items += "<option value='" + generalLedger.Value + "'>" + generalLedger.Text + "</option>";
            //    });
            //    $('#credit-general-ledger-id').html(items);
            //    $("#general-ledger-id-credit").prop("selectedIndex", 1);

            //});

            //$.post(GetGetAccountNumberUrl, { personId: PersonId }, function (data) {
            //    var debitarray = [];
            //    $('#debit-data-table > tbody > tr').each(function () {
            //        var currentRow = $(this).closest("tr");
            //        var columnvalue = (debitDataTable.row(currentRow).data());

            //        if (typeof columnvalue != 'undefined' && columnvalue != null) {

            //            debitarray.push({ 'Value': columnvalue[5], 'Text': columnvalue[6] })
            //        }
            //    });

            //    for (var i = 0; i < data.length; i++) {
            //        for (var j = 0; j < debitarray.length; j++) {
            //            if (data[i].Value == debitarray[j].Value) {
            //                data.splice(i, 1);
            //                data.push({ 'Value': '1', 'Text': 'Account Number Not Found' })

            //            }
            //        }
            //    }

            //    var items = '<option value="0">Select General Ledger</option>';
            //    $.each(data, function (i, accountNumber) {

            //        items += "<option value='" + accountNumber.Value + "'>" + accountNumber.Text + "</option>";
            //    });
            //    $('#credit-customer-account-id').html(items);
            //    //$("#general-ledger-id-credit").prop("selectedIndex", 1);

            //});


            //$.ajax({

            //    url: GetGetAccountNumberUrl,
            //    dataType: "json",
            //    type: "POST",
            //    cache: true,
            //    async: true,
            //    data: ({ personId: PersonId }),
            //    success: function (data) {
            //        ;
            //        var debitarray = [];
            //        $('#debit-data-table > tbody > tr').each(function () {
            //            ;
            //            var currentRow = $(this).closest("tr");
            //            var columnvalue = (debitDataTable.row(currentRow).data());

            //            if (typeof columnvalue != 'undefined' && columnvalue != null) {
            //                ;
            //                debitarray.push({ 'Value': columnvalue[5], 'Text': columnvalue[6] })
            //            }
            //        });

            //        for (var i = 0; i < data.length; i++) {
            //            for (var j = 0; j < debitarray.length; j++) {
            //                if (data[i].Value == debitarray[j].Value) {
            //                    data.splice(i, 1);
            //                    data.push({ 'Value': '1', 'Text': 'Account Number Not Found' })

            //                }
            //            }
            //        }

            //        var items = '<option value="0">Select General Ledger</option>';
            //        $.each(data, function (i, accountNumber) {

            //            items += "<option value='" + accountNumber.Value + "'>" + accountNumber.Text + "</option>";
            //        });
            //        $('#credit-customer-account-id').html(items);
            //        //$("#customer-account-id-credit").prop("selectedIndex", 1);
            //    }
            //});
            ////var selectedObj = ui.item;
            ////var label = selectedObj.label;
            ////////window.location.href = "/Home/Menus?id=" + ui.item.value;
            //////var url = "/Home/Menus";
            //////localStorage.setItem('htmltest1', label);
            //////localStorage.setItem('htmltest', searchQueryId);
            //////window.location.href = url;
        },
        //close: function (event, ui) {
        //    // Close event fires when selection options closes

        //    $(this).data().autocomplete.term = null; // Clear the cached search term, make every search new
        //},
    }).data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li></li>")
            .data("ui-autocomplete-item", item)
            .append(" " + ("<li>" + item.label + "</li>") + "")
            //.append("<li>" + item.value + "</li>")
            .appendTo(ul);
    };


    //.each(function() {
    //    jQuery(this).data( "autocomplete" )._renderItem = function( ul, item ) {
    //        return jQuery( " " )
    //            .data( "item.autocomplete", item )
    //        .append(" " + (item.user ? item.user.name : item.pitch.name) + " " + (item.user ? item.user.investor_type : item.pitch.investor_type) + " - " + (item.user ? item.user.city : item.pitch.city) + " " )
    //                    .appendTo( ul );
    //    };
    //});
    //.data("ui-autocomplete")._renderItem = function (ul, item) {
    //    return $("<li></li>")
    //        .data("ui-autocomplete-item", item)
    //        .append(" " + ("<li>" + item.label + "</li>") + "")
    //        //.append("<li>" + item.value + "</li>")
    //        .appendTo(ul);
    //};

   
    //// AJAX call 1
    //function call_1(PersonId) {
    //    return new Promise(function (resolve, reject) {
    //        let xhttp = new XMLHttpRequest();
    //        xhttp.onreadystatechange = function () {
    //            if (this.readyState == 4) {
    //                if (this.status == 200)
    //                resolve(this.responseText);
    //                //$("#badge-text").html(this.responseText);
    //                alert(this.responseText);
                
    //            }
    //        };
    //        xhttp.open("post", GetMemberType,true);
    //        xhttp.send(PersonId);
    //    });
    //}

    // AJAX call 2
    //function call_2(PersonId) {
    //    return new Promise(function (resolve, reject) {
    //        let xhttp = new XMLHttpRequest();
    //        xhttp.onreadystatechange = function () {
    //            if (this.readyState == 4) {
    //                if (this.status == 200)
    //                    resolve(this.responseText);
    //                else
    //                    reject('Call 2 Failed');
    //            }
    //        };
    //        xhttp.open("post", GetIsVIPCustomer,true);
    //        xhttp.send(PersonId);
    //    });
    //}
    $("#person-id-debit").autocomplete(
     {

         minLength: 3,
         source: function (request, response) {
             $.ajax(
             {
                 url: GetGetAccountNumberUrl1,
                 data: "{ '_inputString': '" + request.term + "'}",
                 dataType: "json",
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 dataFilter: function (data) { return data; },

                 success: function (data) {
                     debugger;
                     var arry=[];
                     if (data.length > 0) {
                         response($.map(data, function (item, i) {
                             debugger;
                             var TransactionNumber="";
                             arry.push(TransactionNumber);
                             arry = item.split('-');
                             var name = arry[9];
                             var fullname = arry[0] + " --> " + arry[1]
                             var PersonId = arry[4] + "-" + arry[5] + "-" + arry[6] + "-" + arry[7] + "-" + arry[8];

                             if (PersonId === "5293c8ec-64a3-44f1-9e5f-beb72aeab9e9")
                             {
                                 TransactionNumber = 5;
                             }
                             if(PersonId === "98d75375-d6f2-4fd4-958d-64d5e34de6fc")
                             {
                                 TransactionNumber = 3;
                             }
                             
                             if (PersonId === "c8b75020-9d9c-4f1b-b3f0-a0d38e28788a")
                             {
                                 TransactionNumber = 2;
                             }
                             if (PersonId === "3a059746-d2c9-456f-86e7-5d71aa0e0028")
                             {
                                 TransactionNumber = 1;
                             }
                                        
                             return { label: fullname, value: PersonId, name, TransactionNumber }

                         }));
                     }
                     else {
                         response([{ label: 'No Records Found', value: -1 }]);
                     }

                 },

                 error: function (XMLHttpRequest, textStatus, errorThrown) {
                     alert(textStatus);
                 },
                 //error: function (response) {
                 //    alert(response.responseText);
                 //},

                 failure: function (response) {
                     alert(response.responseText);
                 }
             });
         },
         focus: function (event, ui) {
             event.preventDefault();
             $(this).val(ui.item.label);
         },
         appendTo: "#debit-modal",

         //change: function (event, ui) {
         //    ;
         //    if (ui.item) {
         //        $("#person-id-debit").val(ui.item.label);
         //    } else {
         //        $("#person-id-debit").val("");
         //    }
         //},
         select: function (event, ui, item) {
             debugger;
             event.preventDefault();
             //optionSelected = true;
             //$(this).val(ui.item.label);
            //this.value = (ui.item.label ? ui.item.label: '');
             // var arry = item.split('-');
           $("#person-id-debit").val(ui.item.label);
             //$('input[name="TransactionCustomerAccountViewModel.PersonId"]').val(ui.item.value);

           //$("#badge-text-debit").html(ui.item.TransctionNumber1);
             
             var PersonId = ui.item.value;
            

         },

     })
  .data("ui-autocomplete")._renderItem = function (ul, item) {
      ;
      return $("<li></li>")
          .data("ui-autocomplete-item", item)
          .append(" " + ("<li>" + item.label + "</li>") + "")
          //.append("<li>" + item.value + "</li>")
          .appendTo(ul);
  };
})
$("#credit-businessOffice-id").change(function () {
    debugger;
    var PersonId = $("#credit-person-id").data("ui-autocomplete").selectedItem.value;
    //if (PersonId == "") {
    //    PersonId = sessionStorage.getItem("dropselvalue");

    //}
    var businessOfficePrmKey = $("#credit-businessOffice-id option:selected").val();
    $.ajax({
        url: GetAuthorizedGeneralLedger,
        type: "POST",
        data: { _businessOfficeId: businessOfficePrmKey, personId: PersonId },
        async: true,
        //dataType: "json",
        cache:true,
        success: function (data) {

            $("#credit-general-ledger-id").empty();
            //if ($('#credit-general-ledger-id').val() == "0") {
            //    $('#credit-general-ledger-id').empty().append('<option selected="selected" value="0">Please select</option>');
            //    $('#credit-general-ledger-id').empty().append('<option selected="selected" value="0">Please select</option>');
            //}
            //else {
            $('#credit-general-ledger-id').empty().append('<option selected="selected" value="0">Please select</option>');
            //}
            //var items = '<option value="0">Loading...</option>';
            //$('#credit-general-ledger-id').prepend($('<option value="0"></option>').html('Loading...'));
            $.each(data, function (i, generalLedger) {
                $('#credit-general-ledger-id').append("<option value='" + generalLedger.Value + "'>" + generalLedger.Text + "</option>");
                // items += "<option value='" + generalLedger.Value + "'>" + generalLedger.Text + "</option>";
            });
            //$('#credit-general-ledger-id').html(items);
        },
        error: function (xhr, exception) {
            alert(exception);
            return false;
        }
    });

    //$.post(GetAuthorizedGeneralLedger, { _businessOfficeId: businessOfficePrmKey, personId: PersonId,async: false }, function (data) {
    //    var items = '<option value="0">Select General Ledger</option>';
    //    $.each(data, function (i, generalLedger) {
    //        items += "<option value='" + generalLedger.Value + "'>" + generalLedger.Text + "</option>";
    //    });
    //    $('#credit-general-ledger-id').html(items);
    //});
})
$("#credit-general-ledger-id").on("change", function () {
    debugger;
    var PersonId = $("#credit-person-id").data("ui-autocomplete").selectedItem.value;
    if (PersonId == "") {
        PersonId = sessionStorage.getItem("dropselvalue");

    }
    var generalLedgerPrmKey = $("#credit-general-ledger-id option:selected").val();
    $.ajax({
        url: GetCustomerWithJointAccount,
        type: "POST",
        data: ({ _generalLedgerId: generalLedgerPrmKey, _personId: PersonId }),
        async: false,
        cache:true,
        success: function (data) {
            var items = '<option value="0">Select General Ledger</option>';
            $.each(data, function (i, CustomerWithJointAccount) {
                items += "<option value='" + CustomerWithJointAccount.Value + "'>" + CustomerWithJointAccount.Text + "</option>";
            });
            $('#credit-customer-account-id').html(items);
        },
        error: function (xhr, exception) {
            alert(exception);
            return false;
        }
    });

    //$.post(GetCustomerWithJointAccount,  { _generalLedgerId: generalLedgerPrmKey, _personId: PersonId ,async: false}, function (data) {
    //    var items = '<option value="0">Select General Ledger</option>';
    //    $.each(data, function (i, CustomerWithJointAccount) {
    //        items += "<option value='" + CustomerWithJointAccount.Value + "'>" + CustomerWithJointAccount.Text + "</option>";
    //    });
    //    $('#credit-customer-account-id').html(items);
    //});
})
$("#debit-BusinessOffice-id").change(function () {
    debugger;
    var PersonId = $("#person-id-debit").data("ui-autocomplete").selectedItem.value;
    if (PersonId == "") {
        PersonId = sessionStorage.getItem("dropselvalue");

    }
    var businessOfficePrmKey = $("#debit-BusinessOffice-id option:selected").val();
    $.ajax({
        url: GetAuthorizedGeneralLedger,
        type: "POST",
        data: { _businessOfficeId: businessOfficePrmKey, personId: PersonId },
        //async: false,
        success: function (data) {

            $("#debit-general-ledger-id").empty();
            //if ($('#credit-general-ledger-id').val() == "0") {
            //    $('#credit-general-ledger-id').empty().append('<option selected="selected" value="0">Please select</option>');
            //    $('#credit-general-ledger-id').empty().append('<option selected="selected" value="0">Please select</option>');
            //}
            //else {
            $('#debit-general-ledger-id').empty().append('<option selected="selected" value="0">Please select</option>');
            //}
            //var items = '<option value="0">Loading...</option>';
            //$('#credit-general-ledger-id').prepend($('<option value="0"></option>').html('Loading...'));
            $.each(data, function (i, generalLedger) {
                $('#debit-general-ledger-id').append("<option value='" + generalLedger.Value + "'>" + generalLedger.Text + "</option>");
                // items += "<option value='" + generalLedger.Value + "'>" + generalLedger.Text + "</option>";
            });
            //$('#credit-general-ledger-id').html(items);
        },
        error: function (xhr, exception) {
            alert(exception);
            return false;
        }
    });

    //$.post(GetAuthorizedGeneralLedger, { _businessOfficeId: businessOfficePrmKey, personId: PersonId,async: false }, function (data) {
    //    var items = '<option value="0">Select General Ledger</option>';
    //    $.each(data, function (i, generalLedger) {
    //        items += "<option value='" + generalLedger.Value + "'>" + generalLedger.Text + "</option>";
    //    });
    //    $('#credit-general-ledger-id').html(items);
    //});
})
$("#debit-general-ledger-id").on("change", function () {
    debugger;
    var PersonId = $("#person-id-debit").data("ui-autocomplete").selectedItem.value;
    if (PersonId == "") {
        PersonId = sessionStorage.getItem("dropselvalue");

    }
    var generalLedgerPrmKey = $("#debit-general-ledger-id option:selected").val();
    $.ajax({
        url: GetCustomerWithJointAccount,
        type: "POST",
        data: ({ _generalLedgerId: generalLedgerPrmKey, _personId: PersonId }),
        async: false,
        success: function (data) {
            var items = '<option value="0">Select General Ledger</option>';
            $.each(data, function (i, CustomerWithJointAccount) {
                items += "<option value='" + CustomerWithJointAccount.Value + "'>" + CustomerWithJointAccount.Text + "</option>";
            });
            $('#debit-customer-account-id').html(items);
        },
        error: function (xhr, exception) {
            alert(exception);
            return false;
        }
    });

    //$.post(GetCustomerWithJointAccount,  { _generalLedgerId: generalLedgerPrmKey, _personId: PersonId ,async: false}, function (data) {
    //    var items = '<option value="0">Select General Ledger</option>';
    //    $.each(data, function (i, CustomerWithJointAccount) {
    //        items += "<option value='" + CustomerWithJointAccount.Value + "'>" + CustomerWithJointAccount.Text + "</option>";
    //    });
    //    $('#credit-customer-account-id').html(items);
    //});
})
$(document).on("change", "input[id^=credit-]", function () {
  
    var arr = $("input[name='gl[]']").map(function (idx, ele) {
        return $(ele).val().trim().length == 0 ? 0 : parseFloat($(ele).val().trim());
    }).get();

    var sharesfacevalue = parseFloat($("#credit-shares-face-value").val());
    var numberofshares = parseFloat($("#credit-number-of-shares").val());
    //var balance = parseFloat($("#badge-amount").data("amount"));
    var sharesholdinglimit = parseFloat($("#shares-holding-limit").attr("data-sharesLimit"));
    var previouscertificatenumber = parseFloat($("#previous-certificate-number").attr("data-previousCertificateNumber"));
    var totals = parseFloat(sharesfacevalue * numberofshares);
    var start = parseFloat(previouscertificatenumber + 1);
    var end = parseFloat(previouscertificatenumber + numberofshares);
    $("#credit-start-certificate-number").val(start);
    $("#credit-end-certificate-number").val(end);
    if (totals <= sharesholdinglimit) {
        var value = ({ totals });
        arr.push(totals);
        var total = 0;
        $.each(arr, function (i, val) {
            total += parseInt(val);
        })
        $("#credit-transaction-amount").val(total.toFixed(2));
        //var mx = parseInt($("#credit-transaction-amount").val());
        var min= $('#credit-transaction-amount').attr('min');
        var max= $('#credit-transaction-amount').attr('max');
        $("form").validate({
            rules: {
                number: { required: true, min: min, max: max }
            }
        });

        Credittransactionamountbadge(total);
    }
    else {
        alert("TransactionAmount Less than of Total Balance!")

    }
    //var array = $('input[id^=credit-]').map(function (i, ele) {
    //    return $(ele).val();
    //}).get();

    //var arr = $("input[name='fee[]']").map(function (idx, ele) {

    // return $(ele).val().trim().length == 0 ? 0 : parseFloat($(ele).val().trim());
    //}).get();
    //var str;
    //var totalPrice = 0;
    //$.each(arr1, function (idx2, val) {
    //    str = val;
    //    totalPrice += parseInt(str);
    //    // alert(str)
    //    //blkstr.push(str);
    //});
    //var str;
    //var totalPrice = 0;
    //$('input[id^=credit-]').each(function (i, val) {
    //    debugger;
    //    str = val;
    //     totalPrice += parseInt(str);
    //})

    //var total = arr.reduce(function (a, b) {
    //        return parseFloat(a) + parseFloat(b);
    //}, 0);

    //$("#credit-transaction-amount").val(totalPrice);


})

$(document).ready(function () {
    $("#credit-customer-account-id").change(function () {
        debugger;
        //var array = new array();
        var count = 0;
        var _customerAccountId = $(this).val();
        $.ajax({
            url: GetSchemeId,
            type: "POST",
            async: false,
            data: ({ _customerAccountId: _customerAccountId }),
            success: function (data) {
                debugger;
                var len = data.length;
                var items = '';
                $.each(data, function (i, item) {
                    debugger;
                    $('#credit-modal').find('#fund-amount').html('');

                    items += "<div class='form-group mt-2'><label class='font-weight-bold' id='credit-" + count++ + "'>" + item.NameOfGL + "</label><input type='text' name='gl[]' id='credit-" + count++ + "' class='form-control' placeholder ='Enter Entry Fees'/><label id='general-Ledger-PrmKey' hidden>" + item.GeneralLedgerPrmKey + "</label></div>";

                   $('#credit-modal').find('#fund-amount').append(items);
                });
                //for (i = 0; i < len; i++) {
                //    debugger;

                //    $('#credit-modal').find('#fund-amount').html('');

                //    //var item = "<div class='form-group mt-2'><label class='font-weight-bold'>" + items + "</label><input type='text' name='fee[]'class='form-control' placeholder ='Enter Entry Fees'/></label></div>";
                //    //var label = '<label id="shares-holding-limit" data-sharesLimit='+ +' hidden></label>';
                //    //array.push(label);
                //    //item=[
                //    // label
                //    //]
                //    $('#credit-modal').find('#fund-amount').append(items);

                //}
                //$('#myDiv').after(items);

            },
            error: function (xhr, exception) {
                alert(exception);
                return false;
            }
        });
        if (typeof _balanceDate != "undefined" && _customerAccountId != "") {

            var _balanceDate = $('#TransactionDate').val();

            $.post(GetClosingBalance, { "_balanceDate": _balanceDate, "_customerAccountId": _customerAccountId, async: false }, function (data) {
                debugger;

                var text = "";
                if (data > 0) {
                    $(".amount-badge-text").html('');

                    text += '<span class="badge badge-pill badge-success float-right text-white badge-text" id="badge-amount" data-amount="' + data + '"><i class="fa fa-inr text-white fa-1x mr-1" aria-hidden="true"></i>' + data + '</span>';
                    $(".amount-badge-text").append(text);
                }
                else {
                    $(".amount-badge-text").html('');

                    text += '<span class="badge badge-pill badge-success float-right text-white badge-text" id="badge-amount" data-amount="******">******</span>';
                    $(".amount-badge-text").append(text);
                }

            });
        }


    })
    $("#transaction-type-id").on('change', function () {

        var person = $("#transaction-type-id option:selected").val();
        if (person === "") {
            $('#btn-add-credit-dt').addClass("disabled");
            $('#btn-add-debit-dt').addClass("disabled");
            $('.cash-denomination').addClass("d-none");
            creditDataTable.clear().draw();
            debitDataTable.clear().draw();
            denominationDataTable.clear().draw();
            return false;
        }
        if (person === "42882154-c991-468f-a645-59eb12939b1d") {


            jQuery("#total-amount").attr('data-id', '0');
            $(".btn-add-credit").removeClass("disabled");
            $(".btn-add-debit").addClass("disabled");
            $(".denomination-btn-add").addClass("disabled");
            $('#select-all-credit').prop("disabled", false);
            $('#select-all-debit').prop("disabled", false);
            $(".amountcard").hide();
            $("#credit-amount").attr('data-id', '0');
            creditDataTable.clear().draw();
            debitDataTable.clear().draw();
            denominationDataTable.clear().draw();
            $('.cash-denomination').removeClass("d-none");
        }
        else if (person === "408d891d-244a-4b8f-b590-0e1b2a6d5afb") {
            jQuery("#total-amount").attr('data-id', '0');
            $(".btn-add-debit").removeClass("disabled");
            $(".btn-add-credit").addClass("disabled");
            $(".denomination-btn-add").addClass("disabled");
            $('#select-all-debit').prop("disabled", false);
            debitDataTable.clear().draw();
            creditDataTable.clear().draw();
            denominationDataTable.clear().draw();
            $('.cash-denomination').removeClass("d-none");
        }
        else {
            $(".btn-add-debit").addClass("disabled");
            $(".btn-add-credit").addClass("disabled");
            $('#btn-add-credit-dt').removeClass("disabled");
            $('#btn-add-debit-dt').removeClass("disabled");
            $('.cash-denomination').addClass("d-none");
            debitDataTable.clear().draw();
            creditDataTable.clear().draw();

        }
    })
    document.querySelector("#credit-transaction-amount").addEventListener("keypress", function (evt) {

        if (evt.which != 8 && evt.which != 0 && evt.which < 48 || evt.which > 57) {
            evt.preventDefault();
        }
    });
    document.querySelector("#debit-transaction-amount").addEventListener("keypress", function (evt) {
        if (evt.which != 8 && evt.which != 0 && evt.which < 48 || evt.which > 57) {
            evt.preventDefault();
        }
    });
})
$(function () {
    //var date = new Date().toISOString().slice(0, -14);
    //var today = new Date("2022-12-07");
    //transaction Date validation
    //var DaysAgo1 = new Date(new Date().setDate(new Date().getDate() - 4)).toISOString().split("T")[0];
    //alert(DaysAgo1);
    //$("#TransactionMasterViewModel_TransactionDate").prop("min", DaysAgo);
    //var today = new Date();
    //var parts = today.split(/[\/\-\.]/);
    //$("#TransactionMasterViewModel_TransactionDate").val("2022-12-07");
    //$('#TransactionMasterViewModel_TransactionDate').attr('min', date);
    //var nextWeekDate = new Date(new Date().getTime() - 4 * 24 * 60 * 60 * 1000).toISOString().split('T')[0];
    //var DaysAgo = new Date(new Date().setDate(new Date().getDate() - 4)).toISOString().split("T")[0];
    //var DaysAgo = new Date(today.setDate(today.getDate() - parseFloat(UserPastDaysPermissionForTransaction))).toISOString().split("T")[0];
    var DaysAgo = new Date(new Date().setDate(new Date().getDate() - parseFloat(UserPastDaysPermissionForTransaction))).toISOString().split("T")[0];
    var DaysInTheFuture = new Date(new Date(DaysAgo).setDate(new Date(DaysAgo).getDate() + parseFloat(UserPastDaysPermissionForTransaction)));
    $("#TransactionDate").prop("min", DaysAgo);
    $('#TransactionDate').attr('max', DaysInTheFuture.toISOString().split("T")[0]);

})
$(function () {

    $(".shares").addClass("read-only");
    //shares();
    //function shares() {
    //    debugger;

    //    if (JsonModel === "true") {
    //        debugger;
    //        //$(".txtCal").on("change", function () {

    //        var sharesfacevalue = parseFloat($("#credit-shares-face-value").val());
    //        var numberofshares = parseFloat($("#credit-number-of-shares").val());
    //        var entryfees = parseFloat($("#credit-entry-fees").val());
    //        var balance = parseFloat($("#badge-amount").data("amount"));
    //        var sharesholdinglimit = parseFloat($("#shares-holding-limit").attr("data-sharesLimit"));
    //        var totals = balance + (sharesfacevalue * numberofshares);
    //        if (totals <= sharesholdinglimit) {

    //            var value = ({ totals, entryfees })
    //            var total = 0;
    //            $.each(value, function (i, val) {
    //                total += parseInt(val);
    //            })
    //            $("#credit-transaction-amount").val(total);
    //        }
    //        else {
    //            alert("TransactionAmount Less than of Total Balance!")

    //        }
    //    }

    //}

    //var total=0;
    //$(".txtCal").each(function () {
    //    debugger;
    //   var tt = $(this).val();
    //   if (!isNaN(parseFloat(tt))) {
    //       total += parseInt(tt);
    //   }
    //});
    //$("#credit-transaction-amount").val(total);
    ///})
    //$("#credit-number-of-shares").on("change", function () {
    //   shares();
    //});

    //$("#credit-01").change(function (idx, ele) {
    //    shares();
    //})
    //$("#debit-number-of-shares").on("change", function () {
    //    shares();
    //});
})
//byhand
$(function () {
    //var data = [{ text: "Choice 1" },
    //          { text: "Choice 2" },
    //          { text: "Choice 3" }]

    var data11 = [
        "MongoDB",
        "ExpressJS",
        "Angular",
        "NodeJS",
        "JavaScript",
        "jQuery",
        "jQuery UI",
        "PHP",
        "Zend Framework",
        "JSON",
        "MySQL",
        "PostgreSQL",
        "SQL Server",
        "Oracle",
        "Informix",
        "Java",
        "Visual basic",
        "Yii",
        "Technology",
        "WilzonMB.com"
    ];


    //$("#by-hand").autocomplete(data,
    //{
    //    matchContains: true,
    //    minLength: 3,

    //    formatItem: function(item) 
    //    { return item.text; }
    //});


    //$.ajax({
    //    url: GetGetAccountNumberUrl2,
    //    dataType: "json",
    //    type: "GET",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (data) {
    //        ;
    //        alert(data);
    //        if (data.length > 0) {
    //            response($.map(data, function (item, i) {
    //                ;
    //                var arry = item.split('-');
    //                var NameOfCustomerType = arry[9];
    //                var fullname = arry[0] + " --> " + arry[1]
    //                var PersonId = arry[4] + "-" + arry[5] + "-" + arry[6] + "-" + arry[7] + "-" + arry[8];
    //                return { label: fullname, value: PersonId, NameOfCustomerType }

    //            }));
    //        }
    //        else {
    //            response([{ label: 'No Records Found', value: -1 }]);
    //        }
    //    },
    //})

    $.get(GetGetAccountNumberUrl2, function (data) {
        $('#by-hand').autocomplete({
            minLength: 2,
            appendTo: "#byhand",
            source: function (request, response) {

                response($.map(data, function (value, key) {

                    var name = value.ByHand.toUpperCase();
                    if (name.indexOf(request.term.toUpperCase()) != -1) {
                        return {
                            label: value.ByHand,
                            value: value.PersonId
                        }
                    } else {
                        return null;
                    }
                }));
            },
            focus: function (event, ui) {
                $('#by-hand').val(ui.item.label);
                return false;
            },
            // Once a value in the drop down list is selected, do the following:
            select: function (event, ui) {
                debugger;
                // place the person.given_name value into the textfield called 'select_origin'...
                $('#by-hand').val(ui.item.label);
                // and place the person.id into the hidden textfield called 'link_origin_id'. 
                //$('#link_origin_id').val(ui.item.id);
                return false;
            }
        }).data("ui-autocomplete")._renderItem = function (ul, item) {
            return $("<li></li>")
                .data("ui-autocomplete-item", item)
                .append(" " + ("<li>" + item.label + "</li>") + "")
                //.append("<li>" + item.value + "</li>")
                .appendTo(ul);
        };
    });
})
$(function () {
    debugger;

    //document.getElementById("credit-narration").value = "rewrwer";
    //$("#credit-narration").val("Being " + array[1] + " Shares Sold To Mr.Ramesh By Receiving " + array[4] + " Entry Fee And " + array[7] + " Building Fund.");

})
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});
function shares1() {
    debugger;
    ///if (isVisibleEntryFees === "true" || isVisibleStationary === "true" || isvisibleBuildingFund === "true" || isVisibleReserveFund === "true" || EnableAutoCertificateNumber === "false") {
    debugger
    //$(".txtCal").on("change", function () {
    var sharesfacevalue = parseFloat($("#credit-shares-face-value").val());
    var numberofshares = parseFloat($("#credit-number-of-shares").val());
    var allvalue = parseFloat($(this).val());
    //var numberofshares1 = parseFloat($("#credit-01").val());
    //var entryfees = parseFloat($("#credit-entry-fees").val());
    //var stationary = parseFloat($("#credit-stationary").val());
    //var buildingfund = parseFloat($("#credit-building-fund").val());
    //var reservefund = parseFloat($("#credit-reserve-fund").val());
    //var fee = $("#credit-modal input[name='fee[]']").map(function (idx, ele) {

    //    return $(ele).val();
    //}).get();


    debugger;


    //var array = $('input[id^=credit-]').map(function (i, ele) {
    //    return parseFloat($(ele).val());
    //}).get();

    //var total1 = array.reduce(function (a, b) {
    //    return parseFloat(a) + parseFloat(b);
    //}, 0);

    ///var number = 0;

    debugger;


    var balance = parseFloat($("#badge-amount").data("amount"));
    var sharesholdinglimit = parseFloat($("#shares-holding-limit").attr("data-sharesLimit"));
    var previouscertificatenumber = parseFloat($("#previous-certificate-number").attr("data-previousCertificateNumber"));
    var totals = parseFloat(balance + (sharesfacevalue * numberofshares));
    var start = parseFloat(previouscertificatenumber + 1);
    var end = parseFloat(previouscertificatenumber + numberofshares);
    $("#credit-start-certificate-number").val(start);
    $("#credit-end-certificate-number").val(end);
    if (totals <= sharesholdinglimit) {
        var value = ({ totals, allvalue});

        var total = 0;
        $.each(value, function (i, val) {

            total += parseInt(val);
        })



        $("#credit-transaction-amount").val(total);
        Credittransactionamountbadge(total);
    }
    else {
        alert("TransactionAmount Less than of Total Balance!")

    }


}
function number2text(value) {

    var fraction = Math.round(frac(value) * 100);
    var f_text = "";

    if (fraction > 0) {
        f_text = "AND " + convert_number(fraction) + " Paise";
    }
    else {

    }


    $(".diff-amount").html("( " + convert_number(value) + " RUPEE " + f_text + " ONLY" + " )");


}
function number3text(value) {

    var fraction = Math.round(frac(value) * 100);
    var f_text = "";

    if (fraction > 0) {
        f_text = "AND " + convert_number(fraction) + " Paise";

    }
    var number = convert_number(value);

    if (number === "zero") {
        $(".credit-amount").html("");

    }
    else {
        $(".credit-amount").html("( " + number + " RUPEE " + f_text + " ONLY" + " )");

    }



}
function number4text(value) {

    var fraction = Math.round(frac(value) * 100);
    var f_text = "";

    if (fraction > 0) {
        f_text = "AND " + convert_number(fraction) + " Paise";
    }

    var number = convert_number(value);

    if (number === "zero") {
        $(".debit-amount").html("");

    }
    else {
        $(".debit-amount").html("( " + number + " RUPEE " + f_text + " ONLY" + " )");

    }


}
function number5text(value) {

    var fraction = Math.round(frac(value) * 100);
    var f_text = "";

    if (fraction > 0) {
        f_text = "AND " + convert_number(fraction) + " Paise";
    }

    var number = convert_number(value);

    if (number === "zero") {
        $(".denomination-amount").html("");

    }
    else {
        $(".denomination-amount").html("( " + number + " RUPEE " + f_text + " ONLY" + " )");

    }

}
function Credittransactionamountbadge(value) {

    
    var fraction = Math.round(frac(value) * 100);
    var f_text = "";

    if (fraction > 0) {
        f_text = "AND " + convert_number(fraction) + " Paise";
    }
    else {

    }
    $("#credit-transaction-amount-badge").html(convert_number(value) + " RUPEE " + f_text + " ONLY");
    if ($("#credit-transaction-amount-badge").hover(function (event) {
    $("#credit-transaction-amount-badge").attr('data-toggle', 'tooltip');
    $("#credit-transaction-amount-badge").attr('data-placement', 'top');
    $("#credit-transaction-amount-badge").attr('data-trigger', "focus");
    $("#credit-transaction-amount-badge").attr('title', convert_number(value) + " RUPEE " + f_text + " ONLY");

    }));
}
// Get Remainder
function frac(f) {
    return f % 1;
}
// Convert Number To Word Function
function convert_number(number) {
    if ((number < 0) || (number > 999999999)) {
        return "NUMBER OUT OF RANGE!";
    }

    // Crore 
    var Gn = Math.floor(number / 10000000);
    number -= Gn * 10000000;

    // Lakhs
    var kn = Math.floor(number / 100000);
    number -= kn * 100000;

    // Thousand 
    var Hn = Math.floor(number / 1000);
    number -= Hn * 1000;

    // Tens (deca)
    var Dn = Math.floor(number / 100);
    number = number % 100;

    // Ones 
    var tn = Math.floor(number / 10);
    var one = Math.floor(number % 10);
    var res = "";

    if (Gn > 0) {
        res += (convert_number(Gn) + " Crore");
    }

    if (kn > 0) {
        res += (((res == "") ? "" : " ") + convert_number(kn) + " Lakh");
    }

    if (Hn > 0) {
        res += (((res == "") ? "" : " ") + convert_number(Hn) + " Thousand");
    }

    if (Dn) {
        res += (((res == "") ? "" : " ") + convert_number(Dn) + " Hundred");
    }

    var ones = Array("", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen");
    var tens = Array("", "", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety");

    if (tn > 0 || one > 0) {
        if (!(res == "")) {
            res += " AND ";
        }

        if (tn < 2) {
            res += ones[tn * 10 + one];
        }
        else {
            res += tens[tn];

            if (one > 0) {
                res += ("-" + ones[one]);
            }
        }
    }

    if (res == "") {
        res = "zero";
    }


    return res;
}
// Handling Save/Submit Click Event
$('#btnsave').on('click', function () {
    debugger;
    //$.validator.setDefaults({ ignore: "hidden" });
    // To Pass List Object Parameter, Create Array Objects And Get Values.
    //var _transactionCustomerAccountViewModelCredit = new Array();
    var _transactionCustomerAccountViewModelDebit = new Array();
    var _transactionCashDenomination = new Array();


    ////// Get Data Table Values In Credit  Array
    //$("#credit-data-table TBODY TR").each(function () {
    //    debugger;
    //    var currentRow = $(this).closest("tr");
        
    //    var columnvalue = columnvalue = (creditDataTable.row(currentRow).data());

    //    // Handling Code If Row Is Undefined Or Null
    //    if (typeof columnvalue == 'undefined' && columnvalue == null) {
    //        return false;
    //    }
    //    else {

         
    //        //_transactionCustomerAccountViewModelCredit=[
    //        //    {"BusinessOfficeId": columnvalue[3]},
    //        //    {"GeneralLedgerId": columnvalue[5]},
    //        //    {"TransactionCustomerAccountId": columnvalue[7]},
    //        //    {"SharesFaceValue": columnvalue[16]},
    //        //    {"NumberOfShares": columnvalue[17]},
    //        //    {"TransactionAmount": columnvalue[14]},
    //        //    {"StartSharesCertificateNumber": columnvalue[19]},
    //        //    {"EndSharesCertificateNumber": columnvalue[20]},
    //        //    {"Amount": columnvalue[13]},
    //        //    {"Gl1PrmKey": columnvalue[23]},
    //        //    {"Gl2PrmKey": columnvalue[24]},
    //        //    {"Gl3PrmKey": columnvalue[25]},
    //        //    {"Gl4PrmKey": columnvalue[26]},
    //        //    {"Gl5PrmKey": columnvalue[27]},
    //        //    {"Gl1Amount": columnvalue[28]},
    //        //    {"Gl3Amount": columnvalue[30]},
    //        //    {"Gl4Amount": columnvalue[31]},
    //        //    {"Gl5Amount": columnvalue[32]},
    //        //    {"Note": columnvalue[21]},
    //        //    {"Narration": columnvalue[22] }
    //        //];
          

    //        //_transactionCustomerAccountViewModelCredit1.push(tt);
    //        //tt = tt.trim(); // remove the unwanted whitespace
    //        //var  jsObj = JSON.parse(tt);
            
    //        //var tt = {
    //        //    "BusinessOfficeId": columnvalue[3],
    //        //    "GeneralLedgerId": columnvalue[5],
    //        //    "TransactionCustomerAccountId": columnvalue[7],
    //        //    "SharesFaceValue": columnvalue[16],
    //        //    "NumberOfShares": columnvalue[17],
    //        //    "Amount": columnvalue[13],
    //        //    "TransactionAmount": columnvalue[14],
    //        //    "StartSharesCertificateNumber": columnvalue[19],
    //        //    "EndSharesCertificateNumber": columnvalue[20],
    //        //    "Gl1PrmKey": columnvalue[23],
    //        //    "Gl2PrmKey": columnvalue[24],
    //        //    "Gl3PrmKey": columnvalue[25],
    //        //    "Gl4PrmKey": columnvalue[26],
    //        //    "Gl5PrmKey": columnvalue[27],
    //        //    "Gl1Amount": columnvalue[28],
    //        //    "Gl2Amount": columnvalue[29],
    //        //    "Gl3Amount": columnvalue[30],
    //        //    "Gl4Amount": columnvalue[31],
    //        //    "Gl5Amount": columnvalue[32],
    //        //    "Note": columnvalue[21],
    //        //    "Narration": columnvalue[22]
    //        //}

    //        //_transactionCustomerAccountViewModelCredit.push({ tt});
    //        _transactionCustomerAccountViewModelCredit.push({
    //                "BusinessOfficeId": columnvalue[3],
    //                "GeneralLedgerId": columnvalue[5],
    //                "TransactionCustomerAccountId": columnvalue[7],
    //                "SharesFaceValue": columnvalue[16],
    //                "NumberOfShares": columnvalue[17],
    //                "Amount": columnvalue[13],
    //                "TransactionAmount": columnvalue[14],
    //                "StartSharesCertificateNumber": columnvalue[19],
    //                "EndSharesCertificateNumber": columnvalue[20],
    //                "Gl1PrmKey": columnvalue[23],
    //                "Gl2PrmKey": columnvalue[24],
    //                "Gl3PrmKey": columnvalue[25],
    //                "Gl4PrmKey": columnvalue[26],
    //                "Gl5PrmKey": columnvalue[27],
    //                "Gl1Amount": columnvalue[28],
    //                "Gl2Amount": columnvalue[29],
    //                "Gl3Amount": columnvalue[30],
    //                "Gl4Amount": columnvalue[31],
    //                "Gl5Amount": columnvalue[32],
    //                "Note":columnvalue[21],
    //                "Narration":columnvalue[22]
    //            });
    //    }
        
    //});

    

    // Get Data Table Values In Debit  Array
    $("#debit-data-table TBODY TR").each(function () {
        debugger;
        var currentRow = $(this).closest("tr");

        var columnvalue  = (debitDataTable.row(currentRow).data());

        // Handling Code If Row Is Undefined Or Null
        if (typeof columnvalue == 'undefined' && columnvalue == null) {
            return false;
        }

        var BusinessOfficeId = columnvalue[3];
        var GeneralLedgerId = columnvalue[5];
        var CustomerAccountId = columnvalue[7];
        var TransactionAmount = columnvalue[9];
        var Narration = columnvalue[8];
         //var ch = [

         //   '{"BusinessOfficeId":"' + columnvalue[3] + '", "GeneralLedgerId":"' + columnvalue[5] + '"}'
         //];

        //_transactionCustomerAccountViewModelDebit.push(ch);

        ///var o = { 'SectionId': sid, 'Placeholder': ph, 'Position': i };

        // _transactionCustomerAccountViewModelDebit = {
        //     BusinessOfficeId: columnvalue[3],
        //     GeneralLedgerId: columnvalue[5],
        //     TransactionCustomerAccountId: columnvalue[7],
        //     Amount: columnvalue[9].toString()
        //}
        _transactionCustomerAccountViewModelDebit.push({

            "BusinessOfficeId":columnvalue[3],
            "GeneralLedgerId":columnvalue[5],
            "TransactionCustomerAccountId":columnvalue[7],
            "Amount":columnvalue[9],
            "SharesFaceValue":"0",
            "NumberOfShares":"0",
            "StartCertificateNumber":"0",
            "EndCertificateNumber":"0",
            "Note":"none",
            "Narration":"none"
        });
    })

    // Get Data Table Values In Debit  Array
    $("#denomination-data-table TBODY TR").each(function () {
        var currentRow = $(this).closest("tr");

        var columnvalue = columnvalue = (denominationDataTable.row(currentRow).data());

        // Handling Code If Row Is Undefined Or Null
        if (typeof columnvalue == 'undefined' && columnvalue == null) {
            return false;
        }

        //var row = $(this);
      
        //var DenominationId = columnvalue[1];
        //var Pieces = columnvalue[3];
        //var Note = columnvalue[5];

        _transactionCashDenomination.push({
           
                "DenominationId": columnvalue[1],
                "Pieces": columnvalue[3],
                "Note": columnvalue[5]
            });
    });
    debugger;
    // Call Cantroller Save Data Table Method 
    var transactionamount = parseFloat($("#credit-amount").attr("data-id"));
    var debittransactionamount = parseFloat($("#debit-amount").attr("data-id"));
    var denominationtotal = parseFloat($("#denomination-amount").attr("data-id"));
    var diffamount = parseFloat($("#total-amount").attr("data-id"));

    var transactiontype = $("#transaction-type-id option:selected").val();

    if ((denominationtotal == transactionamount) && (diffamount == 0)) {

        if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d") {
            save();
        }
        else if (transactiontype === "408d891d-244a-4b8f-b590-0e1b2a6d5afb") {
            save();
           
        }
        else if (diffamount != 0) {
            save();

        }
        else {
            alert("Amount Not Tally!");
            return false;
        }
    }
    else {
        if (transactiontype === "42882154-c991-468f-a645-59eb12939b1d")
        {
            alert("denominationtotal amount not same to cradit amount!");
            return false;
        }
        else {
            alert("denominationtotal amount not same to debit amount!");
            return false;
        }
        
    }

    // if (transactiontype === "408d891d-244a-4b8f-b590-0e1b2a6d5afb") {

    //    //diffamount = parseFloat($("#total-amount").attr('data-id', '0'));

    //    if ((denominationtotal == debittransactionamount) && (diffamount == 0)) {
    //        save();
    //    } else {

    //        alert("denominationtotal amount not same to debit amount!");

    //    }
    //}
    // if (diffamount != 0) {

    //    save();
    //}
    //else {
    //    alert("Amount Not Tally!");
    //    return false;
    //}
    //_transactionCustomerAccountViewModelCredit = JSON.stringify({ _transactionCustomerAccountViewModelCredit});
    //_transactionCustomerAccountViewModelCredit = JSON.stringify({ '_transactionCustomerAccountViewModelCredit': _transactionCustomerAccountViewModelCredit });
    function save() {
        debugger;
        $.ajax({
            url: saveDataTableUrl,
            type: 'post',
            //data: JSON.stringify(_transactionCustomerAccountViewModelCredit),
            //data: _transactionCustomerAccountViewModelCredit,
            ///data:_transactionCustomerAccountViewModelCredit,
            // data: "{ '_transactionCustomerAccountViewModelCredit': '" + _transactionCustomerAccountViewModelCredit1 + "'}",
            ///data: { '_transactionCustomerAccountViewModelCredit': _transactionCustomerAccountViewModelCredit},
            data: JSON.stringify({ '_transactionCustomerAccountViewModelDebit': _transactionCustomerAccountViewModelDebit, '_transactionCashDenomination': _transactionCashDenomination }),
            contentType: 'application/json; charset=utf-8',
            dataType: "JSON",
            cache:true,
            success: function (data) {
            },
            error: function (xhr, status, error) {
                alert("An Error Has Occured While Save Data In Contact Details DataTable!!! Error Message - " + error.toString());
            }

        })

    }
});



