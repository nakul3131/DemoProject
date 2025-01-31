$(document).ready(function () {
    $("#btn-add-opening-balance").on('click', function () {
        debugger;
        var generalLedgerId = $("#general-ledger-id").val();

        var entryStatus = $('#entrystatus').val();

        $.ajax({
            url: getOpeningBalance,
            dataType: "json",
            type: "POST",
            data: ({ _generalLedgerId: generalLedgerId, _entryStatus: entryStatus }),
            success: function () {
                debugger;
                $("#add-modal-opening-balance").modal('hide');
                var newurl = $("#RedirectTo").val();
                location.href = newurl;
            },
            error: function (xhr) {
                alert("An error has occured!!!");
            }
        });
        //$("#general-ledger-id").next("div.validation").remove();
    });

    $("#btn-modify-opening-balance").on("click", function () {
        debugger;
        var modifyOpeningBalance = new Array();
        var generalLedgerId = $("#general-ledger-id").val();
        var customerAccountId = $('#customer-account-name').val();

        var openingBalancePrmKey = $("#openingBalancePrmKey").val();
        var generalLedgerPrmKey = $("#generalLedgerPrmKey").val();
        var customerAccountPrmKey = $("#customerAccountPrmKey").val();
        var schemeTypePrmKey = $("#schemeTypePrmKey").val();

        var faceValueOfShares = $("#face-value-of-shares").val();
        var totalShares = $("#total-shares").val();
        var previousYearBalanceOfShares = $("#previous-year-balance-of-shares").val();

        var lastProvisionDateOfInvestment = $("#last-provision-date-of-investment").val();
        var provisionAmountOfInvestment = $("#provision-amount-of-investment").val();

        var lastProvisionDateOfDeposit = $("#last-provision-date-of-deposit").val();
        var provisionAmountOfDeposit = $("#provision-amount-of-deposit").val();
        var productMinBalance = $("#product-min-balance").val();

        var previousInterestDateOfLoan = $("#previous-interest-date-of-loan").val();
        var previousInstallmentDateOfLoan = $("#previous-installment-date-of-loan").val();
        var lastProvisionDateOfLoan = $("#last-provision-date-of-loan").val();
        var provisionAmountOfLoan = $("#provision-amount-of-loan").val();

        var amount = $("#amount").val();

        modifyOpeningBalance.push({
            "GeneralLedgerId": generalLedgerId,
            "CustomerAccountId": customerAccountId,
            "OpeningBalancePrmKey": openingBalancePrmKey,
            "GeneralLedgerPrmKey": generalLedgerPrmKey,
            "CustomerAccountPrmKey": customerAccountPrmKey,
            "SchemeTypePrmKey": schemeTypePrmKey,

            "FaceValueOfShares": faceValueOfShares,
            "TotalShares": totalShares,
            "PreviousYearBalanceOfShares": previousYearBalanceOfShares,
            "LastProvisionDateOfInvestment": lastProvisionDateOfInvestment,
            "ProvisionAmountOfInvestment": provisionAmountOfInvestment,

            "LastProvisionDateOfDeposit": lastProvisionDateOfDeposit,
            "ProvisionAmountOfDeposit": provisionAmountOfDeposit,
            "ProductMinBalance": productMinBalance,

            "PreviousInterestDateOfLoan": previousInterestDateOfLoan,
            "PreviousInstallmentDateOfLoan": previousInstallmentDateOfLoan,
            "LastProvisionDateOfLoan": lastProvisionDateOfLoan,
            "ProvisionAmountOfLoan": provisionAmountOfLoan,

            "Amount": amount
        });

        $.ajax({
            url: getModifyAllOpeningBalance,
            dataType: "json",
            type: "POST",
            data: { _modifyOpeningBalance: modifyOpeningBalance },
            success: function (data) {
                debugger;
                $("#add-modal-opening-balance").modal('hide');
                var newUrlModify = $("#ModifyRecord").val();
                $.post(newUrlModify)
            },
            error: function (xhr) {
                alert("An error has occured!!!");
            }
        })
    });

    //Dropdown Select List For menu-add
    $("#general-ledger-id").on("focusout", function () {
        debugger;
        $.ajax({
            type: "post",
            url: modelCustomerAccountUrl,
            data: { _generalLedgerId: $('#general-ledger-id').val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                debugger;
                $('#customer-account-name').empty();
                var MainMenuList = '<option value="All">--Select Customer Account--</option>';
                var MainMenuList;
                for (var i = 0; i < data.length; i++) {
                    MainMenuList += "<option value=" + data[i].Value + ">" + data[i].Text + "</option>";
                }
                $('#customer-account-name').append(MainMenuList);
            }
        });
    })

    $(".forScheme").on("focusout", function () {
        debugger;
        $.ajax({
            type: "GET",
            url: getSchemeType,
            data: { _generalLedgerId: $('#general-ledger-id').val() },
            datatype: "json",
            traditional: true,
            success: function (data) {
                debugger;
                switch (schemeType) {
                    case 1:
                        $('#shares').removeClass('d-none');
                        $('#schemeTypePrmKey').val(schemeType);
                        break;
                    case 2:
                        $('#investment').removeClass('d-none');
                        break;
                    case 3:
                        $('#deposit').removeClass('d-none');
                        break;
                    case 4:
                        $('#loan').removeClass('d-none');
                        break;

                    default:
                }
            }
        });
    });
    //Dropdown Select List For menu-add
    $("#customer-account-name").on("focusout", function () {
        debugger;
        var generalLedgerId = $("#general-ledger-id").val();

        var _personId = $('#customer-account-name').val();
        $.ajax({
            type: "get",
            url: getModifyOpeningBalance,
            data: { _generalLedgerId: generalLedgerId, _personId: _personId },
            datatype: "json",
            traditional: true,
            success: function (data) {
                debugger;
                $("#openingBalancePrmKey").val(data.OpeningBalancePrmKey);
                $("#generalLedgerPrmKey").val(data.GeneralLedgerPrmKey);
                $("#customerAccountPrmKey").val(data.CustomerAccountPrmKey);

                $("#face-value-of-shares").val(data.FaceValueOfShares);
                $("#total-shares").val(data.TotalShares);
                $("#previous-year-balance-of-shares").val(data.PreviousYearBalanceOfShares);

                $("#last-provision-date-of-investment").val(data.LastProvisionDateOfInvestment);
                $("#provision-amount-of-investment").val(data.ProvisionAmountOfInvestment);

                $("#last-provision-date-of-deposit").val(data.LastProvisionDateOfDeposit);
                $("#provision-amount-of-deposit").val(data.ProvisionAmountOfDeposit);
                $("#product-min-balance").val(data.ProductMinBalance);

                $("#previous-interest-date-of-loan").val(data.PreviousInterestDateOfLoan);
                $("#previous-installment-date-of-loan").val(data.PreviousInstallmentDateOfLoan);
                $("#last-provision-date-of-loan").val(data.LastProvisionDateOfLoan);
                $("#provision-amount-of-loan").val(data.ProvisionAmountOfLoan);

                $("#amount").val(data.Amount);
            }
        });
    })

    var openingBalanceTable = $('#opening-balance-table').DataTable(
        {
            //Search Lable text change
            "language":
            {
                sLengthMenu: "Show _MENU_",
                search: '',
                searchPlaceholder: 'Search records'
            },
            lengthMenu: [
                [10, 25, 50, -1],
                [10, 25, 50, 'All']
            ],
            "select": {
                style: 'single'
            },
            "keys": {
                keys: [13 /* ENTER */, 38 /* UP */, 40 /* DOWN */]
            },
            deferRender: true,
            scroller: true,
            "order": [[1, 'asc']],
            "paging": true,
            "responsive": true,
            "fixedHeader": true,
            "initComplete": function (settings, json) {
                $("#opening-balance-table").wrap("<div style='overflow:auto; width:100%;position:relative;'></div>");
            },
            "autoWidth": true,
            "scrollCollapse": true,
            "iDisplayLength": 10,
            "drawCallback": function () {
                var api = this.api();
                var sum = 0;
                var formated = 0;
                $('tr:eq(0) td:eq(0)', api.table().footer()).html('Page Total');
                $('tr:eq(1) td:eq(0)', api.table().footer()).html('All Total');
                debugger;

                switch (schemeType) {
                    case 1:
                        for (var i = 7; i <= 8; i++) {
                            debugger;
                            pagesum = api.column(i, { page: 'current' }).data().sum();
                            totalsum = api.column(i).data().sum();
                            pageformated = parseFloat(pagesum).toLocaleString(undefined, { minimumFractionDigits: 2 });
                            totalformated = parseFloat(totalsum).toLocaleString(undefined, { minimumFractionDigits: 2 });
                            $('tr:eq(0) td:eq(' + i + ')', api.table().footer()).html('&#8377;' + pageformated);
                            $('tr:eq(1) td:eq(' + i + ')', api.table().footer()).html('&#8377;' + totalformated);
                        }
                        break;

                    case 2:
                        for (var i = 6; i <= 7; i++) {
                            debugger;
                            pagesum = api.column(i, { page: 'current' }).data().sum();
                            totalsum = api.column(i).data().sum();
                            pageformated = parseFloat(pagesum).toLocaleString(undefined, { minimumFractionDigits: 2 });
                            totalformated = parseFloat(totalsum).toLocaleString(undefined, { minimumFractionDigits: 2 });
                            $('tr:eq(0) td:eq(' + i + ')', api.table().footer()).html('&#8377;' + pageformated);
                            $('tr:eq(1) td:eq(' + i + ')', api.table().footer()).html('&#8377;' + totalformated);
                        }
                        break;

                    case 3:
                        for (var i = 6; i <= 8; i++) {
                            debugger;
                            pagesum = api.column(i, { page: 'current' }).data().sum();
                            totalsum = api.column(i).data().sum();
                            pageformated = parseFloat(pagesum).toLocaleString(undefined, { minimumFractionDigits: 2 });
                            totalformated = parseFloat(totalsum).toLocaleString(undefined, { minimumFractionDigits: 2 });
                            $('tr:eq(0) td:eq(' + i + ')', api.table().footer()).html('&#8377;' + pageformated);
                            $('tr:eq(1) td:eq(' + i + ')', api.table().footer()).html('&#8377;' + totalformated);
                        }
                        break;

                    case 4:
                        for (var i = 8; i <= 9; i++) {
                            debugger;
                            pagesum = api.column(i, { page: 'current' }).data().sum();
                            totalsum = api.column(i).data().sum();
                            pageformated = parseFloat(pagesum).toLocaleString(undefined, { minimumFractionDigits: 2 });
                            totalformated = parseFloat(totalsum).toLocaleString(undefined, { minimumFractionDigits: 2 });
                            $('tr:eq(0) td:eq(' + i + ')', api.table().footer()).html('&#8377;' + pageformated);
                            $('tr:eq(1) td:eq(' + i + ')', api.table().footer()).html('&#8377;' + totalformated);
                        }
                        break;
                    default:
                }

            }
        });

    //SrNO AutoIncrese
    openingBalanceTable.on('order.dt search.dt', function () {
        openingBalanceTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    $('#opening-balance-table').on('key-focus.dt', function (e, datatable, cell) {
        debugger;
        endEdition();
        var a = openingBalanceTable.row(cell.index().row).select();
    });

    // Handle click on table cell
    $('#opening-balance-table').on('click', 'tbody td', function (e) {
        debugger;
        e.stopPropagation();
        var rowIdx = openingBalanceTable.cell(this).index().row;
        openingBalanceTable.row(rowIdx).select();
    });

    // Handle key event that hasn't been handled by KeyTable
    $('#opening-balance-table').on('key.dt', function (e, datatable, key, cell, originalEvent) {
        // If ENTER key is pressed
        debugger;
        if (key === 13) {
            debugger;
            var a = $('.focus').attr('contenteditable', 'true');
            a.focus();
            a.blur(endEdition);
        }
    });

    $("#opening-balance-table").on('keypress', '.edit', function (e) {
        debugger;
        var keyCode = e.which ? e.which : e.keyCode
        if (!(keyCode >= 48 && keyCode <= 57)) {
            return false;
        }

        if (e.which === 13) {
            debugger;
            return e.which != 13;
        }
    });

    function endEdition() {
        debugger;
        var el = $(this);
        openingBalanceTable.cell(el).invalidate().draw(false);
        el.attr('contenteditable', 'false');
        el.off('blur', endEdition); // To prevent another bind to this function
    }

    // Handling Save/Submit Click Event
    $('.opnbtnsave').on('click', function () {
        debugger
        var OpeningBalance = new Array();

        if (schemeType == 1) {
            $(openingBalanceTable.data()).each(function (index, obj) {
                OpeningBalance.push({
                    "OpeningBalancePrmKey": obj[1],
                    "OpeningBalanceSharesPrmKey": obj[2],
                    "CustomerAccountPrmKey": obj[3],
                    "FullName": obj[4],
                    "FaceValueOfShares": obj[5],
                    "TotalShares": obj[6],
                    "PreviousYearBalanceOfShares": obj[7],
                    "Amount": obj[8],
                    "GeneralLedgerPrmKey": obj[9],
                    "SchemeTypePrmKey": schemeType
                })
            })
            OpeningBalance = OpeningBalance.sort(function (a, b) { return a.CustomerAccountPrmKey - b.CustomerAccountPrmKey });
        }

        if (schemeType == 2) {
            $(openingBalanceTable.data()).each(function (index, obj) {
                OpeningBalance.push({
                    "OpeningBalancePrmKey": obj[1],
                    "OpeningBalanceInvestmentPrmKey": obj[2],
                    "CustomerAccountPrmKey": obj[3],
                    "FullName": obj[4],
                    "ProvisionAmountOfInvestment": obj[5],
                    "LastProvisionDateOfInvestment": obj[6],
                    "Amount": obj[7],
                    "SchemeTypePrmKey": schemeType
                })

            })

            OpeningBalance = OpeningBalance.sort(function (a, b) { return a.CustomerAccountPrmKey - b.CustomerAccountPrmKey });
        }

        if (schemeType == 3) {
            $(openingBalanceTable.data()).each(function (index, obj) {
                OpeningBalance.push({
                    "OpeningBalancePrmKey": obj[1],
                    "OpeningBalanceDepositPrmKey": obj[2],
                    "CustomerAccountPrmKey": obj[3],
                    "FullName": obj[4],
                    "LastProvisionDateOfDeposit": obj[5],
                    "ProvisionAmountOfDeposit": obj[6],
                    "ProductMinBalance": obj[7],
                    "Amount": obj[8],
                    "SchemeTypePrmKey": schemeType
                })
            })
            OpeningBalance = OpeningBalance.sort(function (a, b) { return a.CustomerAccountPrmKey - b.CustomerAccountPrmKey });
        }

        if (schemeType == 4) {
            $(openingBalanceTable.data()).each(function (index, obj) {
                OpeningBalance.push({
                    "OpeningBalancePrmKey": obj[1],
                    "OpeningBalanceLoanPrmKey": obj[2],
                    "CustomerAccountPrmKey": obj[3],
                    "FullName": obj[4],
                    "PreviousInterestDateOfLoan": obj[5],
                    "PreviousInstallmentDateOfLoan": obj[6],
                    "ProvisionAmountOfLoan": obj[7],
                    "Amount": obj[8],
                    "LastProvisionDateOfLoan": obj[9],
                    "SchemeTypePrmKey": schemeType
                })
            })
            OpeningBalance = OpeningBalance.sort(function (a, b) { return a.CustomerAccountPrmKey - b.CustomerAccountPrmKey });
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: { '_data': OpeningBalance },
            success: function (data) {
            },

            error: function (xhr, status, error) {
                alert("An Error Has Occured In Contact Details DataTable!!! Error Message - " + error.toString());
            }
        })
    });
});