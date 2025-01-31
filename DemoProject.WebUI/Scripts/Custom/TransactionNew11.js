'use strict'
$(document).ready(function () {
    // ************** D E C L A R A T I O N  ***************
    let disableValue = 'D';
    let demandDeposit = 'DMN';
    let fixedDeposit = 'FDP';
    let recurringDeposit = 'REC';
    jQuery('#credit-amount').attr('data-id', '0');
    jQuery('#debit-amount').attr('data-id', '0');
    jQuery('#total-amount').attr('data-id', '0');
    jQuery('#badge-amounts').attr('data-id', '100000');
    let id;
    let day;
    let month;
    let datepart;
    let activationDate;
    let expiryDate;
    let closeDate;
    let maturityDate;
    let depositType;
    let isValid = false;
    let data1 = false;
    // @@@@@@@@@@ Data Table Related Varible Declaration
    let tag = '';
    let myModal;
    let rowNum = 0;
    let selectedRowIndex;
    let row;
    let rowData;
    let checked;
    let columnValues = "";
    let accountOpeningAmount = 0;
    let schemeClosingChargesViewModel = '';
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let arr = new Array();
    let arrayCloumn1;
    let autocomplete = "";
    let model = "";

    //CreditDataTable 
    let checkbox;
    let PersonId = 0;
    let PersonText = "";
    let businessOfficeId;
    let businessOfficeText;
    let generalLedgerId;
    let generalLedgerText;
    let customerAccountId;
    let customerAccountText;
    let admissionfeesformembership;
    let sharesfacevalue;
    let numberofshares;
    let transactionAmount;
    let startCertificateNumber;
    let endCertificateNumber;
    let note;
    let narration;
    let min;
    let max;
    let getGl = [];
    let nameofGLprmkey1 = "";
    let nameofGLprmkey2 = "";
    let nameofGLprmkey3 = "";
    let nameofGLprmkey4 = "";
    let nameofGLprmkey5 = "";
    let admissionfee = "";
    let stationaryfee = "";
    let buildingfund = "";
    let reservefund = "";
    let admissionfee2 = "";
    let admissionfee3 = "";
    $("#credit-businessOffice-id").prop('selectedIndex', 1);
    $("#debit-businessOffice-id").prop('selectedIndex', 1);
    //debitDataTable
    let minuteofMeetingAgendaId;
    let minuteofmeetingAgendaText;
    let CessionReason;
    let CessionReasonText;
    let debitSharesFacevalue;
    let debitNumberofShares;
    let ceasedSharesCertificateNumbers;
    let startNumber = 0;
    let endNumber = 0;
    //denominationDataTable
    let denominationId;
    let denominationText;
    let pieces;
    let denominationAmount;
    let selectedDenominationTotal;
    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************
    ///SetPageLoadingDefaultValues();
    // *************** E V E N T   H A N D L I N G ***************
    // Get TransactionDate
    $(function () {
        debugger;
        let DaysAgo = new Date(new Date().setDate(new Date().getDate() - parseFloat(UserPastDaysPermissionForTransaction))).toISOString().split("T")[0];
        let DaysInTheFuture = new Date(new Date(DaysAgo).setDate(new Date(DaysAgo).getDate() + parseFloat(UserPastDaysPermissionForTransaction))).toISOString().split("T")[0];
        $('#TransactionDate').prop("min", DaysAgo);
        $('#TransactionDate').prop('max', DaysInTheFuture);
        $('#TransactionDate').val(DaysInTheFuture);
        $.get('/TransactionChildAction/GetAccountByhand', function (data) {
            $('#by-hand').autocomplete({
                minLength: 1,
                appendTo: '#byhand',
                source: function (request, response) {
                    debugger;
                    if (data.length > 0) {
                        let resp = $.map(data, function (key, value) {
                            debugger;
                            //let date =new Date(parseInt(key.DateOfBirth.substr(6))).toISOString().split("T")[0];
                            //let dates= date.toUpperCase();
                            let ByHand = key.toUpperCase();
                            if (ByHand.indexOf(request.term.toUpperCase()) != -1) {
                                return {
                                    label: key,
                                    value: key
                                }
                            } else {
                                response([{ label: 'No Records Found', value: -1 }]);
                            }
                        });
                        response(resp.slice(0, 10));
                    }
                    else {
                        response([{ label: 'No Records Found', value: -1 }]);
                    }
                },
               
                // Once a value in the drop down list is selected, do the following:
                select: function (event, ui) {
                    debugger;
                    // place the person.given_name value into the textfield called 'select_origin'...
                    $(this).val(ui.item.value);
                    // and place the person.id into the hidden textfield called 'link_origin_id'. 
                    return false;
                }
            })
        })
    })
    //GET GetPersonAutoCompleteList Bind Input Filed
    $.get('/TransactionChildAction/GetPersonAutoCompleteList', function (data) {
        $("#credit-person").autocomplete({
            source: function (request, response) {
                let result = [];
                let arry1 = [];
                if (data.length > 0) {
                    let resp = $.map(data, function (item, i) {
                        debugger;
                        let arry = item.split(',');
                        let fullname = arry[0] + " --> " + arry[1];
                        let PersonId = arry[4];
                        if (PersonId === "5293c8ec-64a3-44f1-9e5f-beb72aeab9e9") {
                            let TransctionNumber1 = 5;
                            arry1.push(TransctionNumber1);
                        }
                        if (PersonId === "3b7b055d-27db-42ec-9209-504222eea0fb") {
                            let TransctionNumber2 = 3;
                            arry1.push(TransctionNumber2);
                        }
                        if (PersonId === "c8b75020-9d9c-4f1b-b3f0-a0d38e28788a") {
                            let TransctionNumber3 = 2;
                            arry1.push(TransctionNumber3);
                        }
                        if (PersonId === "3a059746-d2c9-456f-86e7-5d71aa0e0028") {
                            let TransctionNumber4 = 1;
                            arry1.push(TransctionNumber4);
                        }
                        let trannumber1 = arry1[0];
                        let trannumber2 = arry1[1];
                        let trannumber3 = arry1[0];
                        let trannumber4 = arry1[1];
                        if (fullname.toUpperCase().indexOf(request.term.toUpperCase()) != -1) {

                            return { label: fullname, value: PersonId }
                        }
                    });
                    response(resp.slice(0, 10));
                }
                else {
                    response([{ label: 'No Records Found', value: -1 }]);
                }
            },
            focus: function (event, ui) {

                event.preventDefault();
                let tt = ui.item.label.split("<li>");
                //let array = tt.split(',');
                $(this).val(tt);
            },
            appendTo: "#credit-modal",
            minLength: 0,
            scroll: true,
            clearButton: true,
            autoFocus: false,
            select: function (event, ui, item) {
                event.preventDefault();
                PersonId = ui.item.value;
                PersonText = ui.item.label;
                //$('input[id^=credit-]').trigger("change");
                $('#credit-businessOffice-id').change();
                GetMemberType(PersonId, "badge-text");
                ///$(this).prop( "disabled", true );
                return false;
            },

        }).focus(function (event, ui) {
            $(this).autocomplete('search');
        })
        $("#person-debit").autocomplete({

            source: function (request, response) {
                debugger;
                let result = [];
                let arry1 = [];
                if (data.length > 0) {
                    let resp = $.map(data, function (item, i) {

                        let arry = item.split(',');
                        let fullname = arry[0] + " --> " + arry[1];
                        let PersonId = arry[4];

                        if (PersonId === "5293c8ec-64a3-44f1-9e5f-beb72aeab9e9") {
                            let TransctionNumber1 = 5;
                            arry1.push(TransctionNumber1);
                        }
                        if (PersonId === "3b7b055d-27db-42ec-9209-504222eea0fb") {
                            let TransctionNumber2 = 3;
                            arry1.push(TransctionNumber2);
                        }
                        if (PersonId === "c8b75020-9d9c-4f1b-b3f0-a0d38e28788a") {
                            let TransctionNumber3 = 2;
                            arry1.push(TransctionNumber3);
                        }
                        if (PersonId === "3a059746-d2c9-456f-86e7-5d71aa0e0028") {
                            let TransctionNumber4 = 1;
                            arry1.push(TransctionNumber4);
                        }
                        let trannumber1 = arry1[0];
                        let trannumber2 = arry1[1];
                        let trannumber3 = arry1[0];
                        let trannumber4 = arry1[1];
                        if (fullname.toUpperCase().indexOf(request.term.toUpperCase()) != -1) {

                            return { label: fullname, value: PersonId, trannumber1, trannumber2, trannumber3, trannumber4 }
                        }
                    });
                    response(resp.slice(0, 4));
                }
                else {
                    response([{ label: 'No Records Found', value: -1 }]);
                }
            },
            focus: function (event, ui) {

                event.preventDefault();
                let tt = ui.item.label.split("<li>");
                //let array = tt.split(',');
                $(this).val(tt);
            },
            appendTo: "#debit-modal",
            minLength: 0,
            scroll: true,
            clearButton: true,
            autoFocus: true,
            select: function (event, ui, item) {
                debugger;
                event.preventDefault();
                PersonId = ui.item.value;
                PersonText = ui.item.label;
                $('input[id^=credit-]').trigger("change");
                $('#debit-businessOffice-id').change();

                GetMemberType(PersonId, "badge-text1");

                return false;
            },

        }).focus(function (event, ui) {
            $(this).autocomplete('search');
        })
        $('#credit-person-id11').autocomplete({
            source: function (request, response) {
                debugger;
                let result = [];
                let arry1 = [];
                if (data.length > 0) {
                    let resp = $.map(data, function (item, i) {
                        debugger;
                        let arry = item.split(',');
                        let fullname = arry[0] + " --> " + arry[1];
                        let PersonId = arry[4];
                        if (PersonId === "5293c8ec-64a3-44f1-9e5f-beb72aeab9e9") {
                            let TransctionNumber1 = 5;
                            arry1.push(TransctionNumber1);
                        }
                        if (PersonId === "3b7b055d-27db-42ec-9209-504222eea0fb") {
                            let TransctionNumber2 = 3;
                            arry1.push(TransctionNumber2);
                        }
                        if (PersonId === "c8b75020-9d9c-4f1b-b3f0-a0d38e28788a") {
                            let TransctionNumber3 = 2;
                            arry1.push(TransctionNumber3);
                        }
                        if (PersonId === "3a059746-d2c9-456f-86e7-5d71aa0e0028") {
                            let TransctionNumber4 = 1;
                            arry1.push(TransctionNumber4);
                        }
                        let trannumber1 = arry1[0];
                        let trannumber2 = arry1[1];
                        let trannumber3 = arry1[0];
                        let trannumber4 = arry1[1];
                        if (fullname.toUpperCase().indexOf(request.term.toUpperCase()) != -1) {

                            return { label: fullname, value: PersonId }
                        }
                    });
                    response(resp.slice(0, 10));
                }
                else {
                    response([{ label: 'No Records Found', value: -1 }]);
                }
            },
            clearButton: true,
            minLength: 0,
            scroll: true,
            autoFocus: true,
            appendTo: "#container",
            select: function (event, ui, item) {
                debugger;
                event.preventDefault();
                $(this).val(ui.item.label);
                $('#credit-businessOffice-id').change();
                PersonId = ui.item.value;
                PersonText = ui.item.label;

            },
        }).focus(function (event, ui) {
            $(this).autocomplete('search');
        });
    })

    //Credit-BusinessOffice Change Event
    $('#credit-businessOffice-id').change(function () {
        if ($(this).val() != "") {
            GetAuthorizedGeneralLedger($(this).val(), "credit");
        }
    });
    //General-ledger Change Event
    $('#credit-general-ledger-id').change(function () {
        if ($(this).val() != "") {
            GetCustomerWithJointAccount($(this).val(), "credit");
        }
    });
    //Customer-Account Change Event
    $('#credit-customer-account-id').change(function () {
        debugger;
        if ($(this).val() != "") {
            GetSharesCapitalSettingValues($(this).val(), "credit");
        }
    });

    $('#debit-businessOffice-id').change(function () {
        debugger;
        GetAuthorizedGeneralLedger($(this).val(), "debit");
    });

    $('#debit-general-ledger-id').change(function (event) {
        debugger;
        event.stopImmediatePropagation();

        GetCustomerWithJointAccount($(this).val(), "debit");
       


    });

    $('#debit-customer-account-id').change(function (event) {
        //event.preventDefault();
        if ($(this).val() != "") {
            GetSharesCapitalSettingValues($(this).val(), "debit");
        }
       

    });

    $('#transaction-type-id').change(function () {
        debugger;
        let person = $('#transaction-type-id option:selected').val();
        if (person === "") {
            //$('.btn-add-credit').prop('disabled', true);
            //$('.btn-edit-credit').prop('disabled', true);
            //$('.btn-delete-credit').prop('disabled', true);
            //$('.btn-add-debit').prop('disabled', true);
            //$('.btn-edit-debit').prop('disabled', true);
            //$('.btn-delete-debit').prop('disabled', true);
            //$('#btn-add-denomination-dt').prop('disabled', true);
            //$('#btn-delete-denomination-dt').prop('disabled', true);
            $('.cash-denomination').addClass('d-none');
            //$('.debit-amount').addClass("d-none");
            creditTable.clear().draw();
            //debitTable.clear().draw();
            //denominationDataTable.clear().draw();
            return false;
        }
        if (person === '42882154-c991-468f-a645-59eb12939b1d') {
            model += "#credit-modal";
            //$('#credit-businessOffice-id').change();
            //$('#credit-general-ledger-id').change();

            //let idarray = $(".model1").find("input").map(function () { return this.id; }).get();
            //autocomplete = "#"+idarray[0];
            //model = "#credit-model";
            jQuery('#total-amount').attr('data-id', '0');
            //$('.btn-add-credit').prop('disabled', false);
            //$('.btn-add-debit').prop('disabled', true);
            $('.btn-add-denomination').addClass('disabled');
            $('#select-all-credit').prop('disabled', false);
            $('#select-all-debit').prop('disabled', false);
            $('.amountcard').hide();
            $('#credit-amount').attr('data-id', '0');
            creditTable.clear().draw();
            debitTable.clear().draw();
            //denominationDataTable.clear().draw();
            //$('.cash-denomination').removeClass("d-none");
        }
        else if (person === '408d891d-244a-4b8f-b590-0e1b2a6d5afb') {

            //$('#debit-businessOffice-id').change();
            //$('#debit-general-ledger-id').change();
            //let idarray = $(".model1").find("input").map(function () { return this.id; }).get();
            //autocomplete = idarray[8];
            jQuery('#total-amount').attr('data-id', '0');
            //$('.btn-add-debit').prop('disabled', false);
            //$('.btn-add-credit').prop('disabled', true);
            $('.denomination-btn-add').addClass('disabled');
            $('#select-all-debit').prop('disabled', false);

            debitTable.clear().draw();
            creditTable.clear().draw();
            //denominationTable.clear().draw();
            // $('.cash-denomination').removeClass("d-none");
        }
        else {
            //$('.btn-add-debit').addClass('disabled');
            //$('.btn-add-credit').addClass('disabled');
            //$('#btn-add-credit-dt').removeClass('disabled');
            //$('#btn-add-debit-dt').removeClass('disabled');
            $('.cash-denomination').addClass("d-none");
            debitTable.clear().draw();
            creditTable.clear().draw();

        }
    });
    
    //GET GetMemberType function
    function GetMemberType(PersonId, id) {
        $.get('/TransactionChildAction/GetMemberType', { "_personId": PersonId, async: false }, function (data) {

            if (data != null) {
                $("#" + id).html(data);
                $("#" + id).show();
            }
            else {
                $("#" + id).hide();
            }

        });
    }
    //GET GetAuthorizedGeneralLedger function
    function GetAuthorizedGeneralLedger(businessOfficeId, InputId) {

        $.get('/TransactionChildAction/GetAuthorizedGeneralLedger', { _businessOfficeId: businessOfficeId, personId: PersonId, async: false }, function (data) {
            let items = '<option value="0">Select General Ledger</option>';

            $.each(data, function (i, generalLedger) {
                items += "<option value='" + generalLedger.Value + "'>" + generalLedger.Text + "</option>";
                $('#' + InputId + '-general-ledger-id').html(items);
            })
            let len = $('#' + InputId + '-general-ledger-id> option').not(':first').length;
            if (len == 1) {
                $('#' + InputId + '-general-ledger-id').prop('selectedIndex', 1);
                $('#' + InputId + '-general-ledger-id').change();
            }
        });

    }
    //GET GetCustomerWithJointAccount function
    function GetCustomerWithJointAccount(generalLedgerId, InputId) {
        debugger;
        $.get('/TransactionChildAction/GetCustomerWithJointAccount', { _generalLedgerId: generalLedgerId, _personId: PersonId, async: false }, function (data) {
            let creditarray = [];
            $('#tbl-credit > tbody > tr').each(function () {
                currentRow = $(this).closest("tr");
                columnValues = (creditTable.row(currentRow).data());
                if (typeof columnValues !== 'undefined' && columnValues != null) {
                    creditarray.push({ 'Value': columnValues[7], 'Text': columnValues[8] })
                }
            });

            //Remove Duplicate Option
            var i = data.length;
            while (i--) {
                for (var j of creditarray) {
                    if (data[i] && data[i].Text === j.Text) {
                        data.splice(i, 1);
                    }
                }
            }
            let items = '<option value="0">Select Account Number</option>';
            $.each(data, function (i, CustomerWithJointAccount) {
                items += "<option value='" + CustomerWithJointAccount.Value + "'>" + CustomerWithJointAccount.Text + "</option>";
            });
            $('#' + InputId + '-customer-account-id').html(items);
            let len = $('#' + InputId + '-customer-account-id > option').not(':first').length;
            if (len == 1) {
                $('#' + InputId + '-customer-account-id').prop('selectedIndex', 1);
                $('#' + InputId + '-customer-account-id').change();
            }

        });

        //$.get('/TransactionChildAction/GetSharesCurrentFinancialYearWithradwal', { _generalLedgerId: $(this).val(), async: false }, function (result) {
        //})
        //$.get('/TransactionChildAction/IsCrossedAggeregateSharesWithdrawalLimit', { _generalLedgerId: $(this).val(), async: false }, function (result) {
        //    if (result = true)
        //        alert("Under Maharashtra Cooperative Society Act 1960, Section 29. (3) \n The Total Payment Of Share Capital Of A Society In Any Financial Year Does Not Exceed Ten Per Cent.");
        //})
    }
    //GET GetSharesCapitalSettingValues function
    function GetSharesCapitalSettingValues(customerAccountId, InputId) {
        debugger
        let count = 0;
        let _balanceDate = $('#TransactionDate').val();
        if (typeof _balanceDate != 'undefined' && customerAccountId != "") {

            $.get('/TransactionChildAction/GetClosingBalance', { "_balanceDate": _balanceDate, "_customerAccountId": customerAccountId, async: false }, function (data) {

                let text = "";
                if (data > 0) {
                    $('.amount-badge-text').show();
                    $(".amount-badge-text").html('');

                    text += '<span class="badge badge-pill badge-success float-md-right text-white badge-text" id="badge-amount" data-amount="' + data + '"><i class="fa fa-inr text-white fa-1x mr-1" aria-hidden="true"></i>' + data + '</span>';
                    $(".amount-badge-text").append(text);
                }
                else {
                    $(".amount-badge-text").html('');

                    text += '<span class="badge badge-pill badge-success float-md-right text-white badge-text" id="badge-amount" data-amount="' + data + '"><i class="fa fa-inr text-white fa-1x mr-1" aria-hidden="true"></i>' + data + '</span>';
                    //text += '<span class="badge badge-pill badge-success float-right text-white badge-text" id="badge-amount" data-amount="******">******</span>';
                    $(".amount-badge-text").append(text);
                }
            });

            $.get('/TransactionChildAction/GetSharesCapitalSettingValues', { '_customerAccountId': customerAccountId, async: false }, function (data) {
                debugger;
                if (data.GetTransactionType === 'SCP') {
                    $('.shares-capital-credit').removeClass('d-none');
                }
                if (data.GetTransactionType === 'DMN') {
                    $('.shares-capital-credit').addClass('d-none');
                }

                let items = "";
                $.each(data.GetSubscribedFund, function (key, val) {
                    $('#credit-modal').find('#fund-amount').html('');
                    alert(val.length);
                        items += "<div class='form-group mt-2'><label class='font-weight-bold' id='credit-" + count++ + "'>" + val.NameOfGL + "</label><input type='text' name='gl[]' id='credit-" + count++ + "' class='form-control' placeholder ='Enter Entry Fees'/><label id='general-Ledger-PrmKey' hidden>" + val.GeneralLedgerPrmKey + "</label></div>";
                        $('#credit-modal').find('#fund-amount').append(items);
                    
                });

                $('#debit-number-of-shares').val(data.TotalNumberOfShares);
                $('#Ceased-Shares-Certificate-Numbers').val(data.AllSharesCertificateNumbers);

                if (data.IsNewCustomer == true) {

                    $('.badge-text-new').removeClass('d-none');
                    $('#credit-modal').find('#fund-amount').html('');
                }
                else {
                    $('.badge-text-new').addClass('d-none');
                }
                if (data.IsTransactionExists == false) {
                    $('#admissionfeesformembership').removeClass('d-none');
                    $('#credit-number-of-shares').val(data.NumberOfShares);
                    $('#credit-number-of-shares').prop('min', data.NumberOfShares);
                }
                else {
                    $('#admissionfeesformembership').addClass('d-none');
                }
                schemeClosingChargesViewModel = data.schemeClosingChargesViewModel;
            })
        }
        else {
            $('.badge-text-new').addClass('d-none');
            $('#badge-amount').hide();
            $('#badge-amount').attr('data-amount', '');
            ///$('.badge-text').hide();
        }
    }
    //Click Search Icon Button Clear All Input Filed
    document.getElementById('credit-person').addEventListener('search', function (event) {
        debugger;
        PersonText = "";
        Credittransactionamountbadge(100);
        ClearCreditModalInputs();

    });
    //Click Search Icon Button Clear All Input Filed
    document.getElementById('person-debit').addEventListener('search', function (event) {
        debugger;
        PersonText = "";
        $("#debit-transaction-amount").val("100.00");
        Credittransactionamountbadge(100);
        ClearDebitModalInputs();

    });
   
    $('.checkbox_check').click(function () {
        debugger;
        let CessionReason = $(this).next("label").text();

        if (CessionReason === "Other") {
            $('.debit-number-of-shares').removeClass('read-only');
        }
        else {
            $.get('/TransactionChildAction/IsAllowedPartialSharesWithdrawal', { async: false }, function (data) {
                debugger;
                if (data) {
                    $('.div1').addClass("read-only");

                }
                else {
                    $('.div1').removeClass("read-only");
                }

            });
            $('.debit-number-of-shares').addClass('read-only');
        }

    })
    ////// @@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@
    //#########################    credit     #################################
    ClearModal('credit');
    // Create Datatable For credit  (Name Of Table - credit)
    let creditTable = CreateDataTable('credit');

    $('#transaction-type-id').change();
    // DataTable Add Button 
    $('#btn-add-credit-dt').click(function (event) {
        debugger;
        event.preventDefault();
        if ($('#credit-person-id11').val() != "") {
            let len = $('#credit-businessOffice-id > option').not(':first').length;
            if (len == 1) {
                $('#credit-businessOffice-id').prop('selectedIndex', 1);
                $('#credit-businessOffice-id').change();
            }
        }
        ClearCreditModalInputs();
        ClearCreditDivErrors();
        SetModalTitle('credit', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-credit-dt').click(function () {
        debugger;
        SetModalTitle('credit', 'Edit');
        ClearCreditDivErrors();
        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            debugger;
            columnValues = $('.btn-edit-credit').data('rowindex');
            let id = $('#credit-modal').attr("id");
            let myModal = $('#' + id).modal();
            PersonId = columnValues[1];
            PersonText = columnValues[2];
            $('#credit-person', myModal).val(columnValues[2]);
            $('#credit-businessOffice-id', myModal).val(columnValues[3]);
            $("#credit-general-ledger-id").html("<option value='" + columnValues[5] + "'>" + columnValues[6] + "</option>");
            $('#credit-general-ledger-id option[value="' + columnValues[5] + '"]', myModal).prop("selected", true);
            $("#credit-customer-account-id").html("<option value='" + columnValues[7] + "'>" + columnValues[8] + "</option>");
            $('#credit-customer-account-id option[value="' + columnValues[7] + '"]', myModal).prop("selected", true);
            $('#credit-1', myModal).val(columnValues[28]);
            $('#credit-3', myModal).val(columnValues[29]);
            $('#credit-5', myModal).val(columnValues[30]);
            $('#fund-amount').find('#credit-1').val(columnValues[28]);
            $('#credit-shares-face-value', myModal).val(columnValues[16]);
            $('#credit-number-of-shares', myModal).val(columnValues[17]);
            $('#credit-transaction-amount', myModal).val(columnValues[18]);
            $('#credit-start-certificate-number', myModal).val(columnValues[19]);
            $('#credit-end-certificate-number', myModal).val(columnValues[20]);
            $('#credit-note', myModal).val(columnValues[21]);
            $('#credit-narration', myModal).val(columnValues[22]);
            myModal.modal({ show: true });
            $('#btn-update-credit-modal').data('rowindex', columnValues);
            //$('#credit-customer-account-id').change();
        }
        else {
            $('.btn-edit-delete').addClass('disabled');
            $('#credit-modal').modal("hide");
        }

        // Hide Selected Dropdown Id Column
        arr.map(function (obj) {
            $('#person-id').find("option[value='" + obj.td0 + "']").hide();
        });

    });

    // Modal Add Button Event
    $('#btn-add-credit-modal').click(function (event) {
        debugger;
        if (IsValidCreditTableModal()) {
            let row = creditTable.row.add([
                                checkbox,
                                PersonId,
                                PersonText,
                                businessOfficeId,
                                businessOfficeText,
                                generalLedgerId,
                                generalLedgerText,
                                customerAccountId,
                                customerAccountText,
                                "",
                                "0",
                                "0",
                                "0",
                                "0",
                                "0",
                                "0",
                                sharesfacevalue,
                                numberofshares,
                                admissionfeesformembership,
                                transactionAmount,
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

            ]).draw(false);

            // Error Message In Span
            $('#credit-validation span').html('');

            creditTable.columns.adjust().draw();

            credit('credit');
          
            HideCreditDataTableColumns(row);

            ClearModal('credit');

            EnableNewOperation('credit');

            $('.cash-denomination').removeClass('d-none');

            $('#btn-add-denomination-dt').removeClass('disabled');

            $('#credit-modal').modal('hide');
        }

    });

    // Modal Update Button Event
    $('#btn-update-credit-modal').click(function (event) {
        $('#select-all-credit').prop('checked', false);
        debugger;
        if (IsValidCreditTableModal()) {
            let row = creditTable.row.add([
                                 checkbox,
                                 PersonId,
                                 PersonText,
                                 businessOfficeId,
                                 businessOfficeText,
                                 generalLedgerId,
                                 generalLedgerText,
                                 customerAccountId,
                                 customerAccountText,
                                 "",
                                 "0",
                                 "0",
                                 "0",
                                 "0",
                                 "0",
                                 "0",
                                 sharesfacevalue,
                                 numberofshares,
                                 admissionfeesformembership,
                                 transactionAmount,
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

            ]).draw(false);

            $('#credit-validation span').html('');

            creditTable.columns.adjust().draw();

            credit('credit');

            HideCreditDataTableColumns(row);

            ClearModal('credit');

            EnableNewOperation('credit');

            $('.cash-denomination').removeClass('d-none');

            $('#btn-add-denomination-dt').prop('disabled', false);

            $('#credit-modal').modal('hide');
        }
    })

    // Modal Delete Button Event
    $('#btn-delete-credit-dt').click(function (event) {
        debugger
        isChecked = $("input[type='checkbox']").is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Record?')) {
                if ($("#tbl-credit tbody input[type='checkbox']:checked").each(function () {
                    creditTable.row($("#tbl-credit tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                    let transactiontype = $('#transaction-type-id option:selected').val();
                    let total = parseInt($('#credit-amount').attr('data-id'));
                    if (transactiontype === '42882154-c991-468f-a645-59eb12939b1d') {
                    debugger
                    $('#tbl-debit > tbody > tr').each(function (index) {
                     debugger;
                      let currentRow = $(this).closest('tr');
                      let columnValue = (debitTable.row(currentRow).data());
                      if (typeof columnValue != 'undefined' && columnValue != null) {
                      if (total > 0) {
                     debitTable.cell(0, 9).data(total);
                     $('#debit-amount').attr('data-id', total.toFixed(2));
                     $('#debit-amount').html(total.toFixed(2));
                     numToWords2(total, 'credit');
                    credit('credit');
                }
                else {
                    debitTable.row($(this).closest("tr").get(0)).remove().draw();
                     $('#debit-amount').attr('data-id', total.toFixed(2));
                     $('#debit-amount').html(total.toFixed(2));
                     numToWords2(total, 'credit');
                }
                }

                })
                    }
                    credit('credit')
                   // numToWords2(total, 'credit');
                    EnableNewOperation('credit');

                    $('#select-all-credit').prop('checked', false);
                }));
            }
        }
        else
            alert('Please Select Any One Checkbox');
    });

    $('#select-all-credit').click(function () {
        if ($(this).prop('checked')) {
            // Check Mark All Checkboxes
            $('#tbl-credit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');
                selectedRowIndex = creditTable.row(row).index();
                rowData = (creditTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                // Bind All Rows To Delete Button / Operation
                $('#btn-delete-credit-dt').data('rowindex', arr);

                EnableDeleteOperation('credit')
            });
        }
        else {
            EnableNewOperation('credit');
            // Unmark All Checkboxes
            $('#tbl-agent-incentive tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Call Click Event
    $('#tbl-credit tbody').click("input[type=checkbox]", function () {
        // Get Each Row Of Table
        debugger;
        $('#tbl-credit input[type="checkbox"]:checked').each(function (index) {
            debugger
            isChecked = $(this).prop('checked');

            if (isChecked) {
                debugger
                row = $(this).closest("tr");
                selectedRowIndex = creditTable.row(row).index();
                rowData = (creditTable.row(selectedRowIndex).data());

                // ******* Clear Purpose Of td0 And Rename It, If Necessary
                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('credit');
                $('#btn-add-' + 'credit' + '-dt').addClass('read-only');
                $('#btn-delete-' + 'credit' + '-dt').removeClass('read-only');
                $('#btn-edit-' + 'credit' + '-dt').removeClass('read-only');
                
                $('#btn-update-credit-modal').attr('rowindex', selectedRowIndex);
                $('.btn-edit-credit').data('rowindex', rowData);
                $('.btn-delete-credit').data('rowindex', arr);
                $('#select-all-credit').data('rowindex', arr);

            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-credit tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('credit');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('credit');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('credit');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-credit').prop('checked', true);
        else
            $('#select-all-credit').prop('checked', false);
    });

    // Hide Dropdown List Item For Unique Purpose
    $('#tbl-credit > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');

        rowData = (creditTable.row(currentRow).data());

        if (typeof rowData != 'undefined' && rowData != null)
            $('#minimum-collection-amount').find('option[value="' + rowData[0] + '"]').hide();
        else
            return true;
    });

    function credit(_dataTableName) {
        debugger;
        let arr = [];
        let principleamountarry = [];
        let principleamount2 = [];
        let other = 0;
        let principleamount = 0
        $('#tbl-credit TBODY TR').each(function () {
            debugger;
            let currentRow = $(this).closest('tr');
            let columnValue = (creditTable.row(currentRow).data());
            if (typeof columnValue == 'undefined' && columnValue == null) {
                debugger;
                numToWords2(0, 'credit');
                numToWords2(0, 'debit');
                $('#debit-amount').html('0.00');
                debitTable.clear().draw();
                return false;
            }
            else {
                let arr1 = [];
                let principleamount = (Math.round(columnValue[16] * columnValue[17]));
                arr1.push(columnValue[28], columnValue[29], columnValue[30], columnValue[31], columnValue[32])
                let other = 0;
                $.each(arr1, function (i, val) {
                    if (!isNaN(parseFloat(val))) {
                        other += parseFloat(val);
                    }
                })
                let total = principleamount + other;
                creditTable.cell(currentRow, 9).data(principleamount);
                creditTable.cell(currentRow, 13).data(other);
                creditTable.cell(currentRow, 14).data(total);
                principleamountarry.push(columnValue[14]);
               
            }
        })
        $('#tbl-debit > tbody > tr').each(function (index) {
            debugger;
            currentRow = $(this).closest('tr');
            columnValues = (debitTable.row(currentRow).data());
            if (typeof columnValues != 'undefined' && columnValues != null) {
                debugger;
                const credittotal = principleamountarry.reduce((a, b) =>  parseFloat(a) + parseFloat(b), 0);
                debitTable.cell(0, 9).data(credittotal);
                $('td', currentRow).eq(0).find(".checks").attr('disabled', true);
                $('th', currentRow).find("#select-all-debit").attr('disabled', true);
                $('#debit-amount').attr('data-id', credittotal.toFixed(2));
                $('#debit-amount').html(credittotal.toFixed(2));
                numToWords2(credittotal, 'debit');
                 }
             })
            
                    const credittotal = principleamountarry.reduce((a, b) =>  parseFloat(a) + parseFloat(b), 0);
                    //let credittotal=0;
                    let getcreditamount = $('#credit-amount').data("data-id");
                    ///credittotal += total;
                    let transactiontype = $('#transaction-type-id option:selected').val();
                    if (transactiontype === '42882154-c991-468f-a645-59eb12939b1d') {

                        if (credittotal > 0) {
                            $('#total-amount').attr('data-id', '0')
                            jQuery('#credit-amount').attr('data-id', credittotal.toFixed(2));
                            jQuery('#debit-amount').attr('data-id', credittotal.toFixed(2));
                            $('#credit-amount').html(credittotal.toFixed(2));
                            $('#debit-amount').html(credittotal.toFixed(2));
                            $(".amountcard").hide();
                            Amountdifference();
                            numToWords2(credittotal, 'credit');
                            numToWords2(credittotal, 'debit');
                            

                        }
                        else {
                            jQuery('#credit-amount').attr('data-id', credittotal.toFixed(2));
                            $('#credit-amount').html(credittotal.toFixed(2));
                            $(".amountcard").hide();
                            numToWords2(credittotal, _dataTableName);
                        }

                    }
                    else {
                        if (total > 0) {

                            jQuery('#credit-amount').attr('data-id', credittotal.toFixed(2));
                            $('#credit-amount').html(credittotal.toFixed(2));
                            //$(".amountcard").show();
                            Amountdifference();
                            numToWords2(credittotal, _dataTableName);


                        }
                        else {
                            jQuery('#credit-amount').attr('data-id', credittotal.toFixed(2));
                            $('#credit-amount').html(getcreditamount.toFixed(2));
                            Amountdifference();
                            numToWords2(credittotal, _dataTableName);

                        }
                    }
                
            
        
    }
    // Validate Credit Module
    function IsValidCreditTableModal() {
        debugger;
        let count = parseFloat($("#badge-text").html());
        //let generalledgerprmkey = $("#general-Ledger-PrmKey").data("generalledgerprmkey");
        //let transactionAmount = $("#credit-modal input[name='amount[]']").map(function (idx, ele) {
        //    return $(ele).val().trim().length == 0 ? 0 : parseFloat($(ele).val().trim());
        //}).get();
        let nameofGLprmkey = $('label[id^=general-]').map(function (i, ele) {
            return $(ele).html();
        }).get();

        let nameofGL = $("#credit-modal input[name='gl[]']").map(function (idx, ele) {

            return $(ele).val() ? $(ele).val() : 0;
        }).get();

         admissionfee="";
         stationaryfee="";
         buildingfund="";
         reservefund="";

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

        if (nameofGL[0] > 0) {
            admissionfee = "";
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
        checkbox = '<input type="checkbox" name="check_all" class="checks"/>';
        businessOfficeId = $('#credit-businessOffice-id option:selected').val();
        businessOfficeText = $('#credit-businessOffice-id option:selected').text();
        generalLedgerId = $('#credit-general-ledger-id option:selected').val();
        generalLedgerText = $('#credit-general-ledger-id option:selected').text();
        customerAccountId = $('#credit-customer-account-id option:selected').val();
        customerAccountText = $('#credit-customer-account-id option:selected').text();
        admissionfeesformembership = parseInt($('#admission-fees-for-member-ship').val()) ? parseInt($('#admission-fees-for-member-ship').val()) : 0;
        sharesfacevalue = $('#credit-shares-face-value').val();
        numberofshares = parseInt($('#credit-number-of-shares').val());
        transactionAmount = parseInt($('#credit-transaction-amount').val()) ? parseInt($('#credit-transaction-amount').val()) : 0;
        startCertificateNumber = $('#credit-start-certificate-number').val() ? $('#credit-start-certificate-number').val() : 0;
        endCertificateNumber = $('#credit-end-certificate-number').val() ? $('#credit-end-certificate-number').val() : 1;
        note = $('#credit-note').val().trim();
        narration = $("#credit-narration").val().trim();
        min = parseFloat($('#credit-transaction-amount').attr('min'));
        max = parseFloat($('#credit-transaction-amount').attr('max'));
        min = parseFloat($('#credit-number-of-shares').attr('min'));
        //max = parseFloat($('#credit-number-of-shares').attr('max'));

        if (min >= numberofshares) {
            $('#credit-number-of-shares').next('div.error').remove();
            $('#credit-number-of-shares').after('<div class="error" style="color:red">Please enter a value greater than or equal to ' + min + ' </div>');
            return false;

        }
        else {
            $('#credit-number-of-shares').next('div.error').remove();
        }
        //if (Closebalance < transactionAmount)
        //{
        //    $('#myModal1').modal({
        //                    backdrop: 'static',
        //                    keyboard: false
        //    });
        //    return false;
        //}
        //if (transactionAmount >= min && transactionAmount <= max) {
        //    debugger;
        //    $('#credit-transaction-amount').next('div.error').remove();

        //} else {
        //    // ClearCreditDivErrors();
        //    $('#credit-transaction-amount').next('div.error').remove();
        //    $('#credit-transaction-amount').after('<div class="error" style="color:red">Please enter a value greater than or less than or equal to ' + min + ' and  ' + max + ' </div>');
        //    return false;
        //}

        if ((generalLedgerId.trim().length < 36) || (customerAccountId.trim().length < 36) || (startCertificateNumber == "") || (endCertificateNumber == "") || (narration == "") || (min >= transactionAmount || (max <= transactionAmount))) {
            ClearCreditDivErrors();
            if (businessOfficeId.trim().length < 36)
                $('#credit-businessOffice-id').after('<div class="error" style="color:red">Please Select Customer Name </div>');

            if (generalLedgerId.trim().length < 36)
                $('#credit-general-ledger-id').after('<div class="error" style="color:red">Please Select General Ledger </div>');

            if (customerAccountId.trim().length < 36)
                $('#credit-customer-account-id').after('<div class="error" style="color:red">Please Select Customer Aaccount </div>');

            if ((min >= transactionAmount) || (max <= transactionAmount))
                $('#credit-transaction-amount').after('<div class="error" style="color:red">Please enter a value greater than or less than or equal to ' + min + ' and  ' + max + ' </div>');

            if (startCertificateNumber == "")
                $('#credit-start-certificate-number').after('<div class="error" style="color:red">Please Enter Start Number</div>');

            if (endCertificateNumber == "")
                $('#credit-end-certificate-number').after('<div class="error" style="color:red">Please Enter End Number</div>');


            return false;
        }
        else

            return true;
    }
    // Hide Unnecessary Columns
    function HideCreditDataTableColumns(row) {

        if ($("#cash-denomination").hasClass('d-none')) {

            $('#btn-edit-denomination-dt').addClass("d-none");

            $('.cash-denomination').removeClass("d-none");
        }
        else {
            $('.cash-denomination').addClass("d-none");
        }
        $('#btn-add-denomination-dt').prop('disabled', false);
        creditTable.column(1).visible(false);
        creditTable.column(3).visible(false);
        creditTable.column(5).visible(false);
        creditTable.column(7).visible(false);
        //creditDataTable.column(14).visible(false);
        creditTable.column(16).visible(false);
        //creditTable.column(15).visible(false);
        creditTable.column(17).visible(false);
        creditTable.column(18).visible(false);
        creditTable.column(19).visible(false);
        creditTable.column(20).visible(false);
        creditTable.column(21).visible(false);
        creditTable.column(22).visible(false);
        creditTable.column(23).visible(false);
        creditTable.column(24).visible(false);
        creditTable.column(25).visible(false);
        creditTable.column(26).visible(false);
        creditTable.column(27).visible(false);
        creditTable.column(28).visible(false);
        creditTable.column(29).visible(false);
        creditTable.column(30).visible(false);
        creditTable.column(31).visible(false);
        creditTable.column(32).visible(false);
        creditTable.column(33).visible(false);
        let total = parseInt($('#credit-amount').attr('data-id'));
        if ($('#transaction-type-id option:selected').val() === '42882154-c991-468f-a645-59eb12939b1d') {
            $('#tbl-debit > tbody > tr').each(function (index) {
                debugger;
                currentRow = $(this).closest('tr');
                columnValues = (debitTable.row(currentRow).data());
                if (typeof columnValues != 'undefined' && columnValues != null) {
                    debugger;
                    debitTable.cell(0, 9).data(total);
                    $('td', currentRow).eq(0).find(".checks").attr('disabled', true);
                    $('th', currentRow).find("#select-all-debit").attr('disabled', true);
                    $('#debit-amount').attr('data-id', total.toFixed(2));
                    $('#debit-amount').html(total.toFixed(2));
                    numToWords2(total, 'debit');
                }
                else {

                    debugger;
                    debitTable.row.add(JSON.parse(JSON.stringify(row.data()))).draw();
                    debitTable.cell(0, 2).data("none");
                    debitTable.cell(0, 4).data("none");
                    debitTable.cell(0, 6).data("Cash");
                    //debitDataTable.cell(0, 7).data("none");
                    debitTable.cell(0, 8).data("none");
                    debitTable.cell(0, 9).data(total);
                    //debitDataTable.cell(0, 9).data("none");
                    //debitDataTable.cell(0, 8).data("none");
                    debitTable.cell(0, 10).data("none");
                    debitTable.cell(0, 11).data("none");
                    debitTable.cell(0, 12).data("none");
                    debitTable.cell(0, 13).data("none");
                    debitTable.cell(0, 16).data("none");
                    debitTable.cell(0, 17).data("none");
                    debitTable.column(1).visible(false);
                    debitTable.column(3).visible(false);
                    debitTable.column(5).visible(false);
                    debitTable.column(7).visible(false);
                    debitTable.column(10).visible(false);
                    debitTable.column(11).visible(false);
                    debitTable.column(12).visible(false);
                    debitTable.column(13).visible(false);
                    debitTable.column(14).visible(false);
                    debitTable.column(15).visible(false);
                    $('#debit-amount').attr('data-id', total.toFixed(2));
                    $('#debit-amount').html(total.toFixed(2));
                    numToWords2(total, 'debit');
                    $('#select-all-debit').prop('disabled', true);
                    $('table#tbl-debit input[type=checkbox]').attr('disabled', 'true');
                    $('.denomination-btn-add').removeClass('disabled');

                }
            })
        }

        //creditTable.columns.adjust().draw();
        //ClearCreditModalInputs();
        //ClearCreditDivErrors();
        //$('#credit-modal').modal('hide');
        //$('#credit-person-id2').val('');

        //if ($("#cash-denomination").hasClass('d-none')) {

        //    $('.cash-denomination').addClass("d-none");
        //}
        //else {
        //    $('.cash-denomination').removeClass("d-none");
        //}

        //$("#debit-data-table_filter").addClass('disabled');
        //$("#btn-add-denomination-dt").removeClass("disabled");
        //Credittransactionamountbadge(0);
        //$('.debit-amount').removeClass("d-none");


    }

    // Clear Credit Modal Input Values
    function ClearCreditModalInputs() {
        debugger;

        //if ($('#credit-person-id11').val() == "")
        //{
        //    $('#credit-person').val('');
        //}
        //else {
        //    $('#credit-person').val(PersonText);
        //}
        if ($('#credit-person-id11').val() == "") {
            $('#credit-person').val('');
        }
        else {

            $('#credit-person').val($('#credit-person-id11').val());
            //PersonId = "";
            //PersonText = "";
        }

        //$('#credit-person').val('');
        $('#credit-businessOffice-id').val('');
        $('#credit-businessOffice-id').prop('selectedIndex', 1);
        $('#credit-general-ledger-id').html('<option value="0">Select General Ledger</option>');
        $('#credit-customer-account-id').html('<option value="0">Select Account Number</option>');
        $('#demo').html('');
        $('#credit-number-of-shares').val($('#credit-number-of-shares').data('numberofshares'));
        $('#credit-number-of-shares').removeAttr("min");
        $('#fund-amount').html('');
        $('#credit-note').val('');
        $("#credit-narration").val('');
        $("#debit-narration").val('none');
        $('#credit-transaction-amount').val('');
        //$('#credit-transaction-amount-badge').html('');
        $('#credit-transaction-amount-badge').hide();
        $('#by-hand').val('None');
        $('#badge-amount').attr('data-amount', '0');
        $('#badge-text').hide();
        //$('.amount-badge-text').hide();
        $('.amount-badge-text').hide();

    }

    function HideCreditUpdateDataTableColumns(row) {

        // Hide Id Column Of Datatable
        creditTable.column(1).visible(false);
        creditTable.column(3).visible(false);
        creditTable.column(5).visible(false);
        let total = parseInt($('#credit-amount').attr('data-id'));
        let transactiontype = $('#transaction-type-id option:selected').val();

        if (transactiontype === '42882154-c991-468f-a645-59eb12939b1d') {
            $('#tbl-debit> tbody > tr').each(function (index) {

                let currentRow = $(this).closest('tr');
                let columnValue = (debitTable.row(currentRow).data());
                if (typeof columnValue != 'undefined' && columnValue != null) {

                    debitTable.cell(0, 9).data(total);
                    $('#debit-amount').attr('data-id', total.toFixed(2));
                    $('#debit-amount').html(total.toFixed(2));
                    numToWords1(total,'debit');
                }
            })
        }

       

    }
    // Clear Div Errors 
    function ClearCreditDivErrors() {
        $('#credit-person').next("div.error").remove();
        $('#credit-general-ledger-id').next("div.error").remove();
        $('#credit-customer-account-id').next("div.error").remove();
        $('#credit-transaction-amount').next("div.error").remove();
        $('#credit-start-certificate-number').next("div.error").remove();
        $('#credit-end-certificate-number').next("div.error").remove();
        $('#narration').next("div.error").remove();
    }

    $('.close').click(function () {
        ClearCreditDivErrors();
        ClearCreditModalInputs();
        EnableNewOperation('credit');
        //$('.btn-delete-credit').addClass('disabled');
        //$('.btn-edit-credit').addClass('disabled');
        //$('.checks').prop('checked', false);
        $('#select-all-credit').prop('checked', false);

    });
    //On Clicking Checkbox Setting (i.e. Enable Create Operation)
    function EnableEditDeleteOperation(_dataTableName) {
        debugger;

        $('#btn-add-' + _dataTableName + '-dt').addClass('read-only');
        $('#btn-delete-' + _dataTableName + '-dt').removeClass('read-only');
        $('#btn-edit-' + _dataTableName + '-dt').removeClass('read-only');
    }

    function EnableNewOperation(_dataTableName) {
        debugger
        $('.checks').prop('checked', false);
        $('#btn-add-' + _dataTableName + '-dt').removeClass('read-only');
        $('#btn-delete-' + _dataTableName + '-dt').addClass('read-only');
        $('#btn-edit-' + _dataTableName + '-dt').addClass('read-only');
    }

    function EnableDeleteOperation(_dataTableName) {
        $('#btn-add-' + _dataTableName + '-dt').addClass('read-only');
        $('#btn-delete-' + _dataTableName + '-dt').removeClass('read-only');
        $('#btn-edit-' + _dataTableName + '-dt').addClass('read-only');
    }
    //#################### debitTable ###########################

    //CleardebitValues()
    ClearModal('debit');

    let debitTable = CreateDataTable('debit');

    $('#transaction-type-id').change();
    // DataTable Add Button 
    $('#btn-add-debit-dt').click(function () {

        event.stopImmediatePropagation();
        if ($("#credit-person-id11").val() != "") {
            $('#debit-businessOffice-id').change();
            let len = $('#debit-businessOffice-id > option').not(':first').length;
            if (len == 1) {
                $('#debit-businessOffice-id').prop('selectedIndex', 1);
                $('#debit-businessOffice-id').change();
            }
        }
        ClearDebitModalInputs();
        ClearModal('debit');
        SetModalTitle('debit', 'Add');

    });

    // DataTable Edit Button 
    $('#btn-edit-debit-dt').click(function () {
        debugger;
        SetModalTitle('debit', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            debugger;
            columnValues = $('.btn-edit-debit').data('rowindex');
            id = $('#debit-modal').attr("id");
            myModal = $('#' + id).modal();
            PersonId = columnValues[1];
            PersonText = columnValues[2];
            $('#person-debit', myModal).val(columnValues[2]);
            $('#debit-BusinessOffice-id', myModal).val(columnValues[3]).trigger("change");
            $('#debit-general-ledger-id').html("<option value='" + columnValues[5] + "'>" + columnValues[6] + "</option>");
            $('#debit-general-ledger-id option[value="' + columnValues[5] + '"]', myModal).prop("selected", true);
            $('#debit-customer-account-id').html("<option value='" + columnValues[7] + "'>" + columnValues[8] + "</option>");
            $('#debit-customer-account-id option[value="' + columnValues[7] + '"]', myModal).prop("selected", true);
            $('#debit1-transaction-amount').val(columnValues[9]);
            $('#debit-start-number').val(columnValues[10]);
            $('#debit-end-number').val(columnValues[11]);
            $('#debit-note', myModal).val(columnValues[16]);
            $('#debit-narration', myModal).val(columnValues[17]);
            myModal.modal({ show: true });

        }
        else {
            $('#btn-edit-debit-dt').prop('disabled', true);
            $('#add-debit-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-debit-modal').click(function (event) {
        debugger;
        
        if (IsValidDebitModal()) {
            let row = debitTable.row.add([
                              checkbox,
                              PersonId,
                              PersonText,
                              businessOfficeId,
                              businessOfficeText,
                              generalLedgerId,
                              generalLedgerText,
                              customerAccountId,
                              customerAccountText,
                              transactionAmount,
                              minuteofMeetingAgendaId,
                              minuteofmeetingAgendaText,
                              CessionReason,
                              debitSharesFacevalue,
                              debitNumberofShares,
                              ceasedSharesCertificateNumbers,
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

            PersonText = "";

            // Error Message In Span
            $('#debit-validation span').html('');

            debitTable.columns.adjust().draw();

            debit('debit');

            HideColumnsDebit(row);

            EnableNewOperation('debit');

            ClearModal('debit');

            $('.cash-denomination').removeClass('d-none');

            $('#debit-modal').modal('hide');
        }
    });

    // Modal Update Button Event =>
    $('#btn-update-debit-modal').click(function (event) {
        debugger;
        $('#select-all-debit').prop('checked', false);
        //var t = $('#btn-update-debit-modal').data('rowindex', columnValues);

        if (IsValidDebitModal()) {
            debugger;
            let row= debitTable.row($(this).attr('rowindex')).data([
                              checkbox,
                              PersonId,
                              PersonText,
                              businessOfficeId,
                              businessOfficeText,
                              generalLedgerId,
                              generalLedgerText,
                              customerAccountId,
                              customerAccountText,
                              transactionAmount,
                              minuteofMeetingAgendaId,
                              minuteofmeetingAgendaText,
                              CessionReason,
                              debitSharesFacevalue,
                              debitNumberofShares,
                              ceasedSharesCertificateNumbers,
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
            debit('debit');
            HideColumnsDebit(row);
            HideCreditUpdateDataTableColumns();
            debitTable.columns.adjust().draw();
            $('#debit-modal').modal('hide');
            EnableNewOperation('debit');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-debit-dt').click(function (event) {
        isChecked = $("input[type='checkbox']").is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Record?')) {
                if ($("#tbl-debit tbody input[type='checkbox']:checked").each(function () {
                     debitTable.row($("#tbl-debit tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();
                     rowData = $('#btn-delete-debit-dt').data('rowindex');
                            let len = debitTable.data().length;
                            if (len <= 0) {
                                   $('.cash-denomination').addClass("d-none");
                                   $('.debit-amount').addClass("d-none");
                    //denominationDataTable.clear().draw();
                    }
                            let arr = $('.btn-delete-debit').data('rowindex');
                            arr.map(function (obj) {

                                        $('#person-id1').find("option[value='" + obj.td0 + "']").show();
                                        $("#person-id1").prop("selectedIndex", 0);

                                    let transactiontype = $('#transaction-type-id option:selected').val();
                                   if (transactiontype === '408d891d-244a-4b8f-b590-0e1b2a6d5afb') {
                     let total = parseInt($('#debit-amount').attr('data-id'));
                    $('#tbl-credit> tbody > tr').each(function (index) {

                                        let currentRow = $(this).closest('tr');
                                        let columnValue = (creditTable.row(currentRow).data());
                                        if (typeof columnValue != 'undefined' && columnValue != null) {
                                        if (total > 0) {
                                        creditTable.cell(0, 14).data(total);
                                        $('#credit-amount').attr('data-id', total.toFixed(2));
                                        $('#credit-amount').html(total.toFixed(2));
                                        numToWords2(total,'debit');
                }
                else {
                                           creditTable.row($(this).closest("tr").get(0)).remove().draw();
                }
                }
                })
                }
                            });
                    debit('debit');
                    EnableNewOperation('debit');

                    $('#select-all-debit').prop('checked', false);
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Table Select All Rows
    $('#select-all-debit').click(function () {
        if ($(this).prop('checked')) {
            $('#tbl-interest-payout-frequency tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                row = $(this).closest('t');
                selectedRowIndex = schemeInterestPayoutFrequencyTable.row(row).index();
                rowData = (schemeInterestPayoutFrequencyTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-interest-payout-frequency-dt').data('rowindex', arr);
                EnableDeleteOperation('interest-payout-frequency')
            });
        }
        else {
            EnableNewOperation('interest-payout-frequency');
            $('#tbl-interest-payout-frequency tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Call Click Event
    $('#tbl-debit tbody').on('click', "input[type=checkbox]", function () {
        debugger;
        // Get Each Row Of Table
        $('#tbl-debit tbody input[type="checkbox"]:checked').each(function (index) {
            isChecked = $(this).prop('checked');
            debugger;
            if (isChecked) {
                row = $(this).closest("tr");
                selectedRowIndex = debitTable.row(row).index();
                rowData = (debitTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('debit');

                $('#btn-update-debit-modal').attr('rowindex', selectedRowIndex);
                $('.btn-edit-debit').data('rowindex', rowData);
                $('.btn-delete-debit').data('rowindex', arr);
                $('#select-all-debit').data('rowindex', arr);

            }
        });

        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-debit tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('debit');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('debit');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('debit');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-debit').prop('checked', true);
        else
            $('#select-all-debit').prop('checked', false);
    });

    //To page load table each row get value & dropdown value Hide 
    $('#tbl-debit > tbody > tr').each(function () {

        currentRow = $(this).closest('tr');
        columnValues = (debitTable.row(currentRow).data());
        if (typeof columnValues != 'undefined' && columnValues != null)

            $('#frequency-id').find("option[value='" + columnValues[1] + "']").hide();
        else
            return true;
    });

    function debit(_dataTableName) {
        debugger;
        let arr = [];
        let principleamountarry = [];
        let principleamount2 = [];
        let other = 0;
        let principleamount = 0
        $('#tbl-debit TBODY TR').each(function () {
            debugger;
            let currentRow = $(this).closest('tr');
            let columnValue = (debitTable.row(currentRow).data());
            if (typeof columnValue == 'undefined' && columnValue == null) {
                debugger;
                numToWords2(0, 'credit');
                numToWords2(0, 'debit');
                $('#debit-amount').html('0.00');
                creditTable.clear().draw();
                return false;
            }
            else {
                let total = (Math.round(columnValue[13] * columnValue[14]));
                debitTable.cell(currentRow, 9).data(total);
                principleamountarry.push(columnValue[9]);

            }
        })
        $('#tbl-credit > tbody > tr').each(function (index) {
            debugger;
            currentRow = $(this).closest('tr');
            columnValues = (creditTable.row(currentRow).data());
            if (typeof columnValues != 'undefined' && columnValues != null) {
                debugger;
                const credittotal = principleamountarry.reduce((a, b) =>  parseFloat(a) + parseFloat(b), 0);
                creditTable.cell(0, 14).data(credittotal);
                $('td', currentRow).eq(0).find(".checks").attr('disabled', true);
                $('th', currentRow).find("#select-all-debit").attr('disabled', true);
                $('#credit-amount').attr('data-id', credittotal.toFixed(2));
                $('#credit-amount').html(credittotal.toFixed(2));
                numToWords2(credittotal, 'credit');
            }
        })
        const credittotal = principleamountarry.reduce((a, b) =>  parseFloat(a) + parseFloat(b), 0);
        let getcreditamount = $('#credit-amount').data("data-id");
        let transactiontype = $('#transaction-type-id option:selected').val();
        if (transactiontype === '42882154-c991-468f-a645-59eb12939b1d') {

            if (credittotal > 0) {
                $('#total-amount').attr('data-id', '0')
                jQuery('#credit-amount').attr('data-id', credittotal.toFixed(2));
                jQuery('#debit-amount').attr('data-id', credittotal.toFixed(2));
                $('#credit-amount').html(credittotal.toFixed(2));
                $('#debit-amount').html(credittotal.toFixed(2));
                $(".amountcard").hide();
                Amountdifference();
                numToWords2(credittotal, 'credit');
                numToWords2(credittotal, 'debit');

            }
            else {
                jQuery('#credit-amount').attr('data-id', credittotal.toFixed(2));
                $('#credit-amount').html(credittotal.toFixed(2));
                $(".amountcard").hide();
                numToWords2(credittotal, _dataTableName);
            }

        }
        else {
            if (credittotal > 0) {

                jQuery('#debit-amount').attr('data-id', credittotal.toFixed(2));
                $('#debit-amount').html(credittotal.toFixed(2));
                jQuery('#credit-amount').attr('data-id', credittotal.toFixed(2));
                $('#credit-amount').html(credittotal.toFixed(2));
                //$(".amountcard").show();
                Amountdifference();
                numToWords2(credittotal, _dataTableName);


            }
            else {
                jQuery('#debit-amount').attr('data-id', credittotal.toFixed(2));
                $('#debit-amount').html(credittotal.toFixed(2));
                 jQuery('#credit-amount').attr('data-id', credittotal.toFixed(2));
                $('#credit-amount').html(credittotal.toFixed(2));
                Amountdifference();
                numToWords2(credittotal, _dataTableName);

            }
        }



    }
    // Validate Debit Modal Module
    function IsValidDebitModal() {
        let ayy = [];
        let array = $('#debit-modal').find('select option:selected, textarea, input,checkbox:checked').map(function (i, ele) {
            let obj = [
                $(ele).val() ? $(ele).val() : "None",
                $(ele).text(),
            ]
            return obj;
        }).get();

        //let transactionAmount = $("#debit-modal input[name='amount[]']").map(function (idx, ele) {
        //    return $(ele).val().trim().length == 0 ? 0 : parseFloat($(ele).val().trim());
        //}).get();
        checkbox = '<input type="checkbox" name="check_all" class="checks"/>';
        //let person = $('#person-id-debit').data("ui-autocomplete").selectedItem.value;
        //let personText = $('#person-id-debit').data("ui-autocomplete").selectedItem.label;

        //PersonId = $('.person-id').data("ui-autocomplete").selectedItem.value = "";
        //PersonText = $('.person-id').data("ui-autocomplete").selectedItem.lebel = "";
        businessOfficeId = $('#debit-businessOffice-id option:selected').val();
        businessOfficeText = $('#debit-businessOffice-id option:selected').text();
        generalLedgerId = $('#debit-general-ledger-id option:selected').val();
        generalLedgerText = $('#debit-general-ledger-id option:selected').text();
        customerAccountId = $('#debit-customer-account-id option:selected').val();
        customerAccountText = $('#debit-customer-account-id option:selected').text();
        transactionAmount = parseFloat($('#debit-transaction-amount').val());
        minuteofMeetingAgendaId = $('#minute-of-meeting-agenda-id option:selected').val();
        minuteofmeetingAgendaText = $('#minute-of-meeting-agenda-id').text();
        CessionReason = $('input[name="transactionGeneralLedgerViewModel.SharesCessationTransactionViewModel.CessionReason"]:checked').val() ? $('input[name="transactionGeneralLedgerViewModel.SharesCessationTransactionViewModel.CessionReason"]:checked').val() : 'None';
        CessionReasonText = $('input[id^=CessionReason-]:checked').next("label").text();
        debitSharesFacevalue = $('#debit-shares-face-value').val() ? $('#debit-shares-face-value').val() : 0;
        debitNumberofShares = $('#debit-number-of-shares').val() ? $('#debit-number-of-shares').val() : 0;
        ceasedSharesCertificateNumbers = $('#Ceased-Shares-Certificate-Numbers').val() ? $('#Ceased-Shares-Certificate-Numbers').val() : 0;
        note = $('#debit-note').val();
        narration = $('#debit-narration').val();
        if ((generalLedgerId.length < 36) || (customerAccountId.length < 36) || (narration == "")) {

            if (generalLedgerId.trim().length < 36)
                $('#debit-general-ledger-id').after('<div class="error" style="color:red">Please Select General Ledger </div>');

            if (customerAccountId.trim().length < 36)
                $('#debit-customer-account-id').after('<div class="error" style="color:red">Please Select Customer Aaccount </div>');

            if (transactionAmount == "")
                $('#debit-transaction-amount').after('<div class="error" style="color:red">Please Select Transaction Amount </div>');

            return false;
        }
        else
            return true;
    }

    // Hide Unnecessary Columns
    function HideColumnsDebit(row) {
        if ($("#cash-denomination").hasClass('d-none'))
            $('.cash-denomination').addClass("d-none");
        else
            $('.cash-denomination').removeClass("d-none");
        $('#btn-add-denomination-dt').prop('disabled', false);
        debitTable.column(1).visible(false);
        debitTable.column(3).visible(false);
        debitTable.column(5).visible(false);
        debitTable.column(7).visible(false);
        debitTable.column(10).visible(false);
        debitTable.column(11).visible(false);
        debitTable.column(13).visible(false);
        debitTable.column(14).visible(false);
        debitTable.column(15).visible(false);
        debitTable.column(17).visible(false);
        debitTable.columns.adjust().draw();

        let transactiontype = $('#transaction-type-id option:selected').val();
        let total = parseInt($('#debit-amount').attr('data-id'));
        if (transactiontype === '408d891d-244a-4b8f-b590-0e1b2a6d5afb') {
            debugger;
            $('#tbl-credit > tbody > tr').each(function (index) {
                debugger;
                currentRow = $(this).closest('tr');
                columnValues = (creditTable.row(currentRow).data());
                if (typeof columnValues != 'undefined' && columnValues != null) {

                    creditTable.cell(0, 14).data(total);
                    $('td', currentRow).eq(0).find(".checks").attr('disabled', true);
                    $('th', currentRow).eq(0).find("#select-all-debit").attr('disabled', true);
                    $('#credit-amount').attr('data-id', total);
                    $('#credit-amount').html(total);
                    numToWords2(total, 'credit');
                }
                else {
                    let total = parseInt($('#debit-amount').attr('data-id'));
                    creditTable.row.add(JSON.parse(JSON.stringify(row.data()))).draw();
                    creditTable.cell(0, 2).data("none");
                    creditTable.cell(0, 4).data("none");
                    creditTable.cell(0, 6).data("Cash");
                    //creditDataTable.cell(0, 7).data("none");
                    creditTable.cell(0, 8).data("none");
                    creditTable.cell(0, 9).data("none");
                    creditTable.cell(0, 10).data("none");
                    creditTable.cell(0, 11).data("none");
                    creditTable.cell(0, 12).data("nane");
                    creditTable.cell(0, 13).data("nane");
                    creditTable.cell(0, 14).data(total);
                    creditTable.cell(0, 15).data("nane");
                    creditTable.cell(0, 16).data("100");
                    creditTable.cell(0, 15).data("none");
                    creditTable.cell(0, 17).data("1");
                    creditTable.cell(0, 18).data("none");
                    creditTable.cell(0, 19).data("11");
                    creditTable.cell(0, 20).data('15');
                    creditTable.cell(0, 21).data("none");
                    creditTable.cell(0, 22).data("none");
                    creditTable.cell(0, 23).data("none");
                    creditTable.cell(0, 24).data("none");
                    creditTable.cell(0, 25).data("none");
                    creditTable.cell(0, 26).data("none");
                    creditTable.cell(0, 27).data("none");
                    creditTable.cell(0, 28).data("none");
                    creditTable.cell(0, 29).data("none");
                    creditTable.cell(0, 30).data("none");
                    creditTable.cell(0, 31).data("none");
                    creditTable.cell(0, 32).data("none");

                    creditTable.column(1).visible(false);
                    creditTable.column(3).visible(false);
                    creditTable.column(5).visible(false);
                    creditTable.column(7).visible(false);
                    //creditDataTable.column(14).visible(false);
                    creditTable.column(16).visible(false);
                    creditTable.column(15).visible(false);
                    creditTable.column(17).visible(false);
                    creditTable.column(18).visible(false);
                    creditTable.column(19).visible(false);
                    creditTable.column(20).visible(false);
                    creditTable.column(21).visible(false);
                    creditTable.column(22).visible(false);
                    creditTable.column(23).visible(false);
                    creditTable.column(24).visible(false);
                    creditTable.column(25).visible(false);
                    creditTable.column(26).visible(false);
                    creditTable.column(27).visible(false);
                    creditTable.column(28).visible(false);
                    creditTable.column(29).visible(false);
                    creditTable.column(30).visible(false);
                    creditTable.column(31).visible(false);
                    creditTable.column(32).visible(false);
                    creditTable.column(33).visible(false);
                    $('#credit-amount').attr('data-id', total);
                    $('#credit-amount').html(total);
                    $('#debit-amount').html(total);
                    numToWords2(total, 'credit');
                    //numToWords2(total,);
                    //creditTable.column(1).visible(false);
                    //creditTable.column(3).visible(false);
                    //creditTable.column(5).visible(false);
                    //creditTable.column(7).visible(false);
                    ////creditDataTable.column(14).visible(false);
                    //creditTable.column(16).visible(false);
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
    }

    // Clear Credit Modal Input Values
    function ClearDebitModalInputs() {
        debugger;
        if ($('#credit-person-id11').val() == "") {
            $('#person-debit').val('');
        }
        else {

            $('#person-debit').val($('#credit-person-id11').val());
            //PersonId = "";
            //PersonText = "";
        }
        $('#debit-businessOffice-id').val('');
        $('#debit-businessOffice-id').prop('selectedIndex', 1);
        $('#debit-general-ledger-id').html('<option value="0">Select General Ledger</option>');
        $('#debit-customer-account-id').html('<option value="0">Select Account Number</option>');
        $('#demo').html('');
        $('#debit-number-of-shares').val($('#debit-number-of-shares').data('numberofshares'));
        $('#fund-amount').html('');
        $('#debit-note').val('');
        $("#debit-narration").val('');
        $("#debit-narration").val('none');
        $('#debit-transaction-amount').val('100');
        //$('#debit-transaction-amount-badge').html('');
        ///$('#credit-transaction-amount-badge').hide();
        $('#by-hand').val('None');
        $('#badge-amount').attr('data-amount', '0');
        $('#badge-text1').hide();

    }

    $('#credit-transaction-amount').change(function (event) {
        debugger;
        if (accountOpeningAmount != 0 && $(this).val() < accountOpeningAmount) {

            alert(`Transaction Amount Must Be Greater Than OR Equal To Account Opening Amount (${accountOpeningAmount})`);
        }
    })

    $('#debit-transaction-amount').change(function (event) {
        debugger;
        event.stopImmediatePropagation();
        if ($(this).val() != "") {

            let Closebalance = parseInt($('#badge-amount').data("amount"));
            if (parseInt($(this).val()) == Closebalance) {
                $('.is-close-account').removeClass('d-none');
            }
            else {
                alert(`Transaction Amount Must Be Less Than OR Equal To Close balance Amount (${Closebalance})`);
            }

        }

        //let iscloseaccount = $('#is-close-account').is(':checked');
        //if(iscloseaccount==true)
        //{

        //     $('#debit-modal').find('#debit-fd').empty();
        //     $("#debit-fd").append(`<div class='form-group mt-2'><label class='font-weight-bold' id='credit'>ChargeCloseAmount</label><input type='text' name='gl[]' id='credit' class='form-control' placeholder ='Please Enter'/><label id='general-Ledger-PrmKey' hidden>{data.schemeClosingChargesViewModel.GeneralLedgerPrmKey}</label></div>`);

        //}

        if (accountOpeningAmount != 0 && $(this).val() < accountOpeningAmount) {

            alert(`Transaction Amount Must Be Greater Than OR Equal To Account Opening Amount (${accountOpeningAmount})`);
        }
    })

    $("#is-close-account").on("click", function () {
        if (schemeClosingChargesViewModel.MaximumChargesAmount != null) {
            if ($(this).is(':checked')) {

                $('#debit-modal').find('#debit-fd').empty();
                $("#debit-fd").append(`<div class='form-group mt-2 schemeClosingCharges'><label class='font-weight-bold' id='credit'>Closing Charges Amount</label><input type='text' value=${schemeClosingChargesViewModel.MaximumChargesAmount} name='gl[]' id='credit' class='form-control' placeholder ='Please Enter'/><label id='general-Ledger-PrmKey' hidden>{data.schemeClosingChargesViewModel.GeneralLedgerPrmKey}</label></div>`);
            }
            else {
                $('#debit-modal').find('#debit-fd').empty();
            }
        }
    })

    $(document).on("change", "input[id^=credit-]", function () {
        debugger;
        let arr = 0;
        let txt = "";
        let text = "";
        let nameofGL = $('label[id^=credit-]').map(function (i, ele) {
            return $(ele).html();
        }).get();

        let array = $('input[id^=credit-]').map(function (i, ele) {
            return $(ele).val();
        }).get();

        arr = $("input[name='gl[]']").map(function (idx, ele) {
            return $(ele).val().trim().length == 0 ? 0 : parseFloat($(ele).val().trim());
        }).get();

        var personText = array[0].split("-->");

        //var BuildingFund = "";
        let admissionfee = "";
        let stationaryfee = "";
        let buildingfund = "";
        let reservefund = "";

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
        let personText1 = array[1].split("-->");

        if (arr.length === 0 || arr[0] === 0) {

            txt += "Being " + array[3] + " Shares Sold @ Face Value RS " + array[2].trim() + " To Mr. " + personText1[0].trim() + " And Receiving Rs " + admissionfee.trim() + " " + stationaryfee.trim() + " " + buildingfund.trim() + " " + reservefund.trim() + "";

            text += txt.slice(0, -20);
        }
        else {
            txt += "Being " + array[3] + " Shares Sold @ Face Value RS " + array[2].trim() + " To Mr. " + personText1[0].trim() + " And Receiving Rs " + admissionfee.trim() + " " + stationaryfee.trim() + " " + buildingfund.trim() + " " + reservefund.trim() + "";

            text += txt.slice(0, -9);
        }
        $('#credit-modal').find($('#credit-narration').val(text.trim()));
        let Closebalance = parseInt($('#badge-amount').data("amount"));
        let sharesfacevalue = parseFloat($('#credit-shares-face-value').val());
        let numberofshares = parseFloat($('#credit-number-of-shares').val());
        //let admissionfeesformembership =  parseFloat($('admission-fees-for-member-ship').val()
        //let balance = parseFloat($('#badge-amount').data("amount"));
        let sharesholdinglimit = parseFloat($('#shares-holding-limit').attr('data-sharesLimit'));
        let previouscertificatenumber = parseFloat($('#previous-certificate-number').attr('data-previousCertificateNumber'));
        let totals = parseFloat(sharesfacevalue * numberofshares);
        let html = "";
        if (Closebalance > 0) {
            if (Closebalance < totals) {
                html += '<div class="modal-dialog modal-dialog-top modal-dialog-zoom modal-lg"><div class="modal-content"> <div class="modal-header bg-primary"><h6 class="modal-title font-weight-bold text-white justify-content-center "><img src="" width="40" height="40" />  &nbsp;&nbsp;&nbsp;&nbsp;<u class="mt-5">Under Maharashtra Cooperative Society Act 1960, Section 28 And State Government <span style="margin-left:7.5%;">Notification In The Official Gazette.</span></u></h6></div><div class="modal-body">No Any Individual Member Hold More Than One Fifth Portion Of Total Shares Or Specified In State Government By Notification In The Official Gazette (i.e. One Lakh Only).</div><div class="modal-footer justify-content-center"><button type="button" class="btn btn-primary btn-credit-no">OK</button></div></div></div>';
                $('#myModal').html(html);
                $('#myModal').find('img').attr("src", "/Icons/imageedit_10_9461575285.png");
                $('#myModal').modal({
                    backdrop: 'static',
                    keyboard: false
                }).on("click", ".btn-credit-no", function () {
                    ClearCreditModalInputs();
                    $('#myModal').modal('hide');
                })
            }
        }
        let start = parseFloat(previouscertificatenumber + 1);
        let end = parseFloat(previouscertificatenumber + numberofshares);
        $('#credit-start-certificate-number').val(start);
        $('#credit-end-certificate-number').val(end);
        //let id = 3;
        //if (id == 3) {
        //    $('#credit-transaction-amount').focusout();
        //    Credittransactionamountbadge($('#credit-transaction-amount').val());
        //}
        //else {
        if (totals <= sharesholdinglimit) {
            let value = ({ totals });
            arr.push(totals);
            let total = 0;
            $.each(arr, function (i, val) {
                total += parseFloat(val);
            });

            $('#credit-transaction-amount').val(total.toFixed(2));
            if ($('#credit-transaction-amount').val() !== "") {
                Credittransactionamountbadge(total);
                $('#credit-transaction-amount-badge').show();
            }
            //let mx = parseInt($('#credit-transaction-amount').val());
            let min = $('#credit-transaction-amount').attr('min');
            let max = $('#credit-transaction-amount').attr('max');

            //$('form').validate({
            //    rules: {
            //        number: { required: true, min: min, max: max }
            //    }
            //});
        }
        else {
            alert("TransactionAmount Less than of Total Balance!");

        }

     
    })
    $(document).on("change", "input[id^=debit-]", function () {
        debugger;
        let arr = 0;
        let text = "";
        let array = $('input[id^=debit-]').map(function (i, ele) {
            return $(ele).val();
        }).get();

        arr = $("input[name='gl[]']").map(function (idx, ele) {
            return $(ele).val().trim().length == 0 ? 0 : parseFloat($(ele).val().trim());
        }).get();
        let personText1 = PersonText.split('-->');
        text += "Being " + array[1] + " Shares Purchased @ Face Value RS " + array[0].trim() + " From Mr. " + personText1[0].trim() + " For Cash. ";
        $('#debit-narration').val(text);
        let Closebalance = parseInt($('#badge-amount').data("amount"));
        let sharesfacevalue = parseFloat($('#debit-shares-face-value').val());
        let numberofshares = parseFloat($('#debit-number-of-shares').val());
        //let balance = parseFloat($('#badge-amount').data("amount"));
        let sharesholdinglimit = parseFloat($('#shares-holding-limit').attr('data-sharesLimit'));
        let previouscertificatenumber = parseFloat($('#previous-certificate-number').attr('data-previousCertificateNumber'));
        let totals = parseFloat(sharesfacevalue * numberofshares);
        let html = "";
        if (Closebalance > 0) {
            if (Closebalance < totals) {
                html += '<div class="modal-dialog modal-dialog-top modal-dialog-zoom modal-lg"><div class="modal-content"> <div class="modal-header bg-primary"><h6 class="modal-title font-weight-bold text-white justify-content-center "><img src="" width="40" height="40" />  &nbsp;&nbsp;&nbsp;&nbsp;<u class="mt-5">Under Maharashtra Cooperative Society Act 1960, Section 28 And State Government <span style="margin-left:7.5%;">Notification In The Official Gazette.</span></u></h6></div><div class="modal-body">No Any Individual Member Hold More Than One Fifth Portion Of Total Shares Or Specified In State Government By Notification In The Official Gazette (i.e. One Lakh Only).</div><div class="modal-footer justify-content-center"><button type="button" class="btn btn-primary btn-credit-no">OK</button></div></div></div>';
                $('#myModal').html(html);
                $('#myModal').find('img').attr("src", "/Icons/imageedit_10_9461575285.png");
                $('#myModal').modal({
                    backdrop: 'static',
                    keyboard: false
                }).on("click", ".btn-credit-no", function () {
                    ClearCreditModalInputs();
                    $('#myModal').modal('hide');
                })
            }
        }
        let start = parseFloat(previouscertificatenumber + 1);
        let end = parseFloat(previouscertificatenumber + numberofshares);
        $('#debit-start-certificate-number').val(start);
        $('#debit-end-certificate-number').val(end);
        //let id = 3;
        //if (id == 3) {
        //    $('#credit-transaction-amount').focusout();
        //    Credittransactionamountbadge($('#credit-transaction-amount').val());
        //}
        //else {
        if (totals <= sharesholdinglimit) {
            let value = ({ totals });
            arr.push(totals);
            let total = 0;
            $.each(arr, function (i, val) {
                total += parseFloat(val);
            })
            $('#debit-transaction-amount').val(total);
            $('#debit-transaction-amount').change();
            //let mx = parseInt($('#credit-transaction-amount').val());
            let min = $('#debit-transaction-amount').attr('min');
            let max = $('#debit-transaction-amount').attr('max');
            $('form').validate({
                rules: {
                    number: { required: true, min: min, max: max }
                }
            });
            Debittransactionamountbadge($('#debit-transaction-amount').val());
            //Credittransactionamountbadge(total);
        }
        else {
            //alert("TransactionAmount Less than of Total Balance!")

        }

    })
    //#################### SchemeNumberOfTransactionLimit ###########################

    // ClearNumberOfTransactionLimitValues()
    ClearModal('denomination');

    let denominationTable = CreateDataTable('denomination');

    $('#transaction-type-id').change();
    // DataTable Add Button 
    $('#btn-add-denomination-dt').click(function () {

        event.preventDefault();

        SetModalTitle('denomination', 'Add');
    });
    // Modal Add Button Event
    $('#btn-add-denomination-modal').click(function (event) {
        debugger;

        if (IsValiddenominationModal()) {

            denominationTable.row.add([
                      checkbox,
                      denominationId,
                      denominationText,
                      pieces,
                      "",
                     note
            ]).order([1, 'desc']).draw(false);

            // Error Message In Span
            $('#debit-validation span').html('');

            Denomination('denomination');

            HideColumnsdenominationTable();

            denominationTable.columns.adjust().draw();

            EnableNewOperation('denomination');

            ClearModal('denomination');

            $('#denomination-modal').modal('hide');
        }

    });
    // Modal Delete Button Event
    $('#btn-delete-denomination-dt').click(function (event) {

        let isChecked = $("#tbl-denomination tbody input[type='checkbox']").is(':checked');

        if (isChecked) {
            if (confirm('Are You Sure To Delete This Record?')) {
                if ($("#tbl-denomination tbody input[type='checkbox']:checked").each(function () {
                    denominationTable.row($("#tbl-denomination tbody input[type='checkbox']:checked").parents('tr').get(0)).remove().draw();

                     rowData = $('#btn-delete-denomination-dt').data('rowindex');
                      EnableNewOperation('denomination');

                  $('#select-all-denomination').prop('checked', false);
                }));
            }
        }
        else
            alert('Please select a checkbox');
    });
    // Modal Select Button Event
    $('#select-all-denomination').click(function () {
        if ($(this).prop('checked')) {

            $('#tbl-denomination tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);
                row = $(this).closest('tr');
                selectedRowIndex = denominationTable.row(row).index();
                rowData = (denominationTable.row(selectedRowIndex).data());
                arr.push({ arrayCloumn1: rowData[1] });
                $('#btn-delete-denomination-dt').data('rowindex', arr);
                EnableDeleteOperation('denomination');
            });
        }
        else {
            EnableNewOperation('denomination')
            $('#tbl-denomination tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });
    // Table Select Button Event
    $('#tbl-denomination tbody').click("input[type=checkbox]", function () {
        $('#tbl-denomination tbody input[type="checkbox"]:checked').each(function (index) {
            debugger;
            isChecked = $(this).prop('checked');

            if (isChecked) {


                row = $(this).closest('tr');
                selectedRowIndex = denominationTable.row(row).index();
                rowData = (denominationTable.row(selectedRowIndex).data());
                arr.push({ arrayCloumn1: rowData[1] });

                EnableEditDeleteOperation('denomination');

                $('#btn-update-denomination-dt').attr('rowindex', selectedRowIndex);
                $('#btn-edit-denomination-dt').data('rowindex', rowData);
                $('#btn-delete-denomination-dt').data('rowindex', arr);
                $('#select-all-denomination').data('rowindex', arr);
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-denomination tbody input[type="checkbox"]');
        debugger;
        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('denomination');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('denomination');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('denomination');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-denomination').prop('checked', true);
        else
            $('#select-all-ndenomination').prop('checked', false);
    });
    //To page load table each row get value & dropdown value Hide 
    $('#tbl-denomination > tbody > tr').each(function () {
        currentRow = $(this).closest('tr');
        columnValues = (denominationTable.row(currentRow).data());
        if (typeof columnValues != 'undefined' && columnValues != null)
            $('#transaction-type-id').find("option[value='" + columnValues[0] + "']").hide();
        else
            return true;
    });
    function Denomination(_dataTableName) {
        // let api = this.api();
        debugger;
        let denomination = denominationTable.column(2).data().toArray();
        let pices = denominationTable.column(3).data().toArray();
        let denominationtotal = 0;
        for (let i = 0; i < denomination.length; i++) {

            denominationtotal += denomination[i] * pices[i];
        }
        denominationTable.cell(0, 4).data(denominationtotal);
        let result = Math.abs(denominationtotal); //  12
        let transactionamount = parseFloat($('#credit-amount').attr('data-id'));
        let debittransactionamount = parseFloat($('#debit-amount').attr('data-id'));
        let diffamount = parseFloat($('#total-amount').data("id"));

        let transactiontype = $('#transaction-type-id option:selected').val();

        if (transactiontype === '42882154-c991-468f-a645-59eb12939b1d') {


            if ((result <= transactionamount) && (diffamount == 0)) {

                if (result > 0) {

                    jQuery('#denomination-amount').attr('data-id', result.toFixed(2));
                    $('#denomination-amount').html(result.toFixed(2));
                    $(".amountcard").hide();
                    Amountdifference();
                    numToWords3(result)
                    //Amountdifference1();

                }
                else {

                    jQuery('#denomination-amount').attr('data-id', result.toFixed(2));
                    $('#denomination-amount').html(result.toFixed(2));
                    Amountdifference();
                    numToWords3(result);
                }

            }
            else {
                if (denominationtotal > 0) {

                    jQuery('#denomination-amount').attr('data-id', result.toFixed(2));
                    $('#denomination-amount').html(result.toFixed(2));
                    $(".amountcard").hide();
                    Amountdifference();
                    numToWords3(result)

                }
                else {

                    jQuery('#denomination-amount').attr('data-id', result.toFixed(2));
                    $('#denomination-amount').html(result.toFixed(2));
                    Amountdifference();
                    numToWords3(result)
                }
            }

        }
        else {
            if ((result <= transactionamount) && (diffamount == 0)) {

                let getcreditamount = $('#denomination-amount').data("id");
                //let credittotal = parseInt(denominationtotal) + parseInt(getcreditamount);


                if (result > 0) {

                    jQuery('#denomination-amount').attr('data-id', result.toFixed(2));
                    $('#denomination-amount').html(result.toFixed(2));
                    //$(".amountcard").show();
                    numToWords3(result)
                    Amountdifference();

                }
                else {

                    jQuery('#denomination-amount').attr('data-id', result.toFixed(2));
                    $('#denomination-amount').html(result.toFixed(2));
                    numToWords3(result)
                    Amountdifference();
                }

            }
            else {
                if (result > 0) {

                    jQuery('#denomination-amount').attr('data-id', result.toFixed(2));
                    $('#denomination-amount').html(result.toFixed(2));
                    $(".amountcard").hide();
                    Amountdifference();
                    numToWords3(result)

                }
                else {

                    jQuery('#denomination-amount').attr('data-id', result.toFixed(2));
                    $('#denomination-amount').html(result.toFixed(2));
                    Amountdifference();
                    numToWords3(result)
                }
            }

        }
    }
    // Validate Number Of Transaction Limit Module
    function IsValiddenominationModal() {
        debugger;
        checkbox = '<input type="checkbox" name="select-all" class="checks" />';
        transactionAmount = parseFloat($('#debit-amount').attr('data-id'));
        denominationAmount = parseFloat($('#denomination-amount').attr('data-id'));
        denominationId = $('#denomination-id option:selected').val();
        denominationText = $('#denomination-id option:selected').text();
        pieces = parseInt($('#pieces-cash-denomination').val());
        selectedDenominationTotal = parseInt(denominationText) * parseInt(pieces);
        note = $('#note-cash-denomination').val();

        // Validate All Modal Inputs
        if (denominationId.trim().length < 36 || (pieces == "")) {
            if (denominationId.trim().length < 36)
                $('#denomination-id').after('<div class="error" style="color:red">Please Select Denomination </div>');

            if (pieces == "")
                $('#pieces').after('<div class="error" style="color:red">Please Enter Pieces </div>');

            return false;
        }
        else {
            isValid = true;

            if (transactionAmount <= denominationAmount) {
                debugger;
                let differenceAmt = denominationAmount - transactionAmount;

                if (selectedDenominationTotal > differenceAmt) {

                    alert('Change Amount Is Less Than Selected Denomination Amount');
                    isValid = false;
                    return false;
                }
                else {
                    pieces = -pieces;

                    isValid = true;
                }
            }
        }
        return true;
    }

    // Hide Unnecessary Columns
    function HideColumnsdenominationTable() {
        denominationTable.column(1).visible(false);


    }

    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger;
        //$.validator.setDefaults({ ignore: "hidden" });
        // To Pass List Object Parameter, Create Array Objects And Get Values.
        var isCreditDataTableValid = creditTable.data().any();
        var isDebitDataTableValid = debitTable.data().any();
        var isDenominationDataTableValid = denominationTable.data().any();

        let _transactionCustomerAccountViewModelCredit = [];
        let _transactionCustomerAccountViewModelDebit = [];
        let _transactionCustomerAccountViewModelList = [];
        let _transactionCustomerAccountOtherSubscriptionList = [];
        let _sharesTransactionViewModelList = [];
        let _transactionCashDenomination = [];
        let sharesCessationTransactionViewModels = [];
       
        if ($('form').valid()) {
            debugger;
            if (isCreditDataTableValid && isDebitDataTableValid && isDenominationDataTableValid) {

                //Get Data Table Values In Credit  Array
                $('#tbl-credit TBODY TR').each(function () {
                    debugger;
                    let currentRow = $(this).closest('tr');

                    let columnValue = (creditTable.row(currentRow).data());

                    // Handling Code If Row Is Undefined Or Null
                    if (typeof columnValue == 'undefined' && columnValue == null) {
                        return false;
                    }
                    else {

                        //_transactionCustomerAccountViewModelCredit=[
                        //    {"BusinessOfficeId": columnValue[3]},
                        //    {"GeneralLedgerId": columnValue[5]},
                        //    {"TransactionCustomerAccountId": columnValue[7]},
                        //    {"SharesFaceValue": columnValue[16]},
                        //    {"NumberOfShares": columnValue[17]},
                        //    {"TransactionAmount": columnValue[14]},
                        //    {"StartSharesCertificateNumber": columnValue[19]},
                        //    {"EndSharesCertificateNumber": columnValue[20]},
                        //    {"Amount": columnValue[13]},
                        //    {"Gl1PrmKey": columnValue[23]},
                        //    {"Gl2PrmKey": columnValue[24]},
                        //    {"Gl3PrmKey": columnValue[25]},
                        //    {"Gl4PrmKey": columnValue[26]},
                        //    {"Gl5PrmKey": columnValue[27]},
                        //    {"Gl1Amount": columnValue[28]},
                        //    {"Gl3Amount": columnValue[30]},
                        //    {"Gl4Amount": columnValue[31]},
                        //    {"Gl5Amount": columnValue[32]},
                        //    {"Note": columnValue[21]},
                        //    {"Narration": columnValue[22] }
                        //];


                        //_transactionCustomerAccountViewModelCredit1.push(tt);
                        //tt = tt.trim(); // remove the unwanted whitespace
                        //let  jsObj = JSON.parse(tt);

                        //let tt = {
                        //    "BusinessOfficeId": columnValue[3],
                        //    "GeneralLedgerId": columnValue[5],
                        //    "TransactionCustomerAccountId": columnValue[7],
                        //    "SharesFaceValue": columnValue[16],
                        //    "NumberOfShares": columnValue[17],
                        //    "Amount": columnValue[13],
                        //    "TransactionAmount": columnValue[14],
                        //    "StartSharesCertificateNumber": columnValue[19],
                        //    "EndSharesCertificateNumber": columnValue[20],
                        //    "Gl1PrmKey": columnValue[23],
                        //    "Gl2PrmKey": columnValue[24],
                        //    "Gl3PrmKey": columnValue[25],
                        //    "Gl4PrmKey": columnValue[26],
                        //    "Gl5PrmKey": columnValue[27],
                        //    "Gl1Amount": columnValue[28],
                        //    "Gl2Amount": columnValue[29],
                        //    "Gl3Amount": columnValue[30],
                        //    "Gl4Amount": columnValue[31],
                        //    "Gl5Amount": columnValue[32],
                        //    "Note": columnValue[21],
                        //    "Narration": columnValue[22]
                        //}

                        //_transactionCustomerAccountViewModelCredit.push({ tt});
                        //_transactionCustomerAccountViewModelCredit.push({
                        //        "BusinessOfficeId": columnValue[1],
                        //        "GeneralLedgerId": columnValue[3],
                        //        "TransactionCustomerAccountId": columnValue[5],
                        //        //"SharesFaceValue": columnValue[16],
                        //        //"NumberOfShares": columnValue[17],
                        //        //"TransactionAmount": columnValue[14],
                        //        //"StartSharesCertificateNumber": columnValue[19],
                        //        //"EndSharesCertificateNumber": columnValue[20],
                        //        "Amount": columnValue[13],
                        //        //"Gl1PrmKey": columnValue[23]},
                        //        //"Gl2PrmKey": columnValue[24]},
                        //        //"Gl3PrmKey": columnValue[25]},
                        //        //"Gl4PrmKey": columnValue[26]},
                        //        //"Gl5PrmKey": columnValue[27]},
                        //        //"Gl1Amount": columnValue[28]},
                        //        //"Gl3Amount": columnValue[30]},
                        //        //"Gl4Amount": columnValue[31]},
                        //        //"Gl5Amount": columnValue[32]},
                        //        "Note": columnValue[21],
                        //        "Narration": columnValue[22] 
                        //});

                        _transactionCustomerAccountViewModelCredit.push({
                            "BusinessOfficeId": columnValue[3],
                            "GeneralLedgerId": columnValue[5],
                            "TransactionCustomerAccountId": columnValue[7],
                            "Amount": columnValue[14],
                            "Note": columnValue[21],
                            "Narration": columnValue[22],

                        });
                        _transactionCustomerAccountOtherSubscriptionList.push({
                            "Gl1PrmKey": columnValue[23],
                            "Gl2PrmKey": columnValue[24],
                            "Gl3PrmKey": columnValue[25],
                            "Gl4PrmKey": columnValue[26],
                            "Gl5PrmKey": columnValue[27],
                            "Gl1Amount": columnValue[28],
                            "Gl2Amount": columnValue[29],
                            "Gl3Amount": columnValue[30],
                            "Gl4Amount": columnValue[31],
                            "Gl5Amount": columnValue[32],
                        });
                        _sharesTransactionViewModelList.push({
                            "SharesFaceValue": columnValue[16],
                            "NumberOfShares": columnValue[17],
                            "StartSharesCertificateNumber": columnValue[19],
                            "EndSharesCertificateNumber": columnValue[20],
                        });


                    }

                });

                ///Get Data Table Values In Debit  Array
                $('#tbl-debit TBODY TR').each(function () {
                    debugger;
                    let currentRow = $(this).closest('tr');

                    let columnValue = (debitTable.row(currentRow).data());

                    // Handling Code If Row Is Undefined Or Null
                    if (typeof columnValue == 'undefined' && columnValue == null) {
                        return false;
                    }
                    //let BusinessOfficeId = columnValue[3];
                    //let GeneralLedgerId = columnValue[5];
                    //let CustomerAccountId = columnValue[7];
                    //let TransactionAmount = columnValue[9];
                    //let Narration = columnValue[8];
                    //let ch = [

                    //   '{"BusinessOfficeId":"' + columnValue[3] + '", "GeneralLedgerId":"' + columnValue[5] + '"}'
                    //];

                    //_transactionCustomerAccountViewModelDebit.push(ch);

                    ///let o = { 'SectionId': sid, 'Placeholder': ph, 'Position': i };

                    // _transactionCustomerAccountViewModelDebit = {
                    //     BusinessOfficeId: columnValue[3],
                    //     GeneralLedgerId: columnValue[5],
                    //     TransactionCustomerAccountId: columnValue[7],
                    //     Amount: columnValue[9].toString()
                    //}

                    _transactionCustomerAccountViewModelDebit.push({
                        "BusinessOfficeId": columnValue[3],
                        "GeneralLedgerId": columnValue[5],
                        "TransactionCustomerAccountId": columnValue[7],
                        "Amount": columnValue[9],
                        "Note": "none",
                        "Narration": columnValue[17],

                    });
                    sharesCessationTransactionViewModels.push({
                        "TransactionCustomerAccountId": columnValue[7],
                        "MinuteOfMeetingAgendaId": columnValue[10],
                        "CessionReason": columnValue[12],
                        "SharesFaceValue": columnValue[13],
                        "NumberOfSharesCessation": columnValue[14],
                        "CeasedSharesCertificateNumbers": columnValue[15],
                    })
                    //_transactionCustomerAccountViewModelDebit.push({
                    //    "BusinessOfficeId": columnValue[3],
                    //    "GeneralLedgerId": columnValue[5],
                    //    "TransactionCustomerAccountId": columnValue[7],
                    //    "Amount": columnValue[9],
                    //    "NumberOfShares": "0",
                    //    "StartCertificateNumber": "0",
                    //    "EndCertificateNumber": "0",
                    //    "Note": "none",
                    //    "Narration": columnValue[17],

                    //});
                    // sharesCessationTransactionViewModels.push({
                    //    "TransactionCustomerAccountId": columnValue[7],
                    //    "MinuteOfMeetingAgendaId": columnValue[10],
                    //    "CessionReason": columnValue[12],
                    //    "SharesFaceValue": columnValue[13],
                    //    "NumberOfSharesCessation": columnValue[14],
                    //    "CeasedSharesCertificateNumbers": columnValue[15],
                    //})
                })

                // Get Data Table Values In Debit  Array
                $('#tbl-denomination TBODY TR').each(function () {
                    debugger;
                    let currentRow = $(this).closest('tr');

                    let columnValue = (denominationTable.row(currentRow).data());

                    // Handling Code If Row Is Undefined Or Null
                    if (typeof columnValue == 'undefined' && columnValue == null) {
                        return false;
                    }

                    _transactionCashDenomination.push({

                        "DenominationId": columnValue[1],
                        "Pieces": columnValue[3],
                        "Note": columnValue[5]
                    });
                });
                debugger;
                // Call Cantroller Save Data Table Method 
                let transactionamount = parseFloat($('#credit-amount').attr('data-id'));
                let debittransactionamount = parseFloat($('#debit-amount').attr('data-id'));
                let denominationtotal = parseFloat($('#denomination-amount').attr('data-id'));
                let diffamount = parseFloat($('#total-amount').attr('data-id'));
                let transactiontype = $('#transaction-type-id option:selected').val();

                if ($("#cash-denomination").hasClass('d-none')) {
                    debugger
                    if (transactiontype === '42882154-c991-468f-a645-59eb12939b1d') {
                        save();
                    }
                    else if (transactiontype === '408d891d-244a-4b8f-b590-0e1b2a6d5afb') {
                        save();
                    }
                }
                else {
                    if ((denominationtotal == transactionamount) && (diffamount == 0)) {

                        if (transactiontype === '42882154-c991-468f-a645-59eb12939b1d') {
                            save();
                            //$.post(saveDataTableUrl, { "_transactionCustomerAccountViewModelCredit": _transactionCustomerAccountViewModelDebit, "_transactionCustomerAccountViewModelDebit": _transactionCustomerAccountViewModelDebit, "_transactionCustomerAccountViewModelList": _transactionCustomerAccountViewModelList, "_transactionCustomerAccountOtherSubscriptionList": _transactionCustomerAccountOtherSubscriptionList, "_sharesTransactionViewModelList": _sharesTransactionViewModelList, "_transactionCashDenomination": _transactionCashDenomination },
                            //    function () {
                            //        alert("success");
                            //    })
                            //  .fail(function () {
                            //      alert("error");
                            //  });
                        }
                        else if (transactiontype === '408d891d-244a-4b8f-b590-0e1b2a6d5afb') {
                            save();

                        }
                        else if (diffamount != 0) {
                            save();
                        }
                        else {
                            alert('Amount Not Tally!');
                            return false;
                        }
                    }
                    else {
                        if (transactiontype === '42882154-c991-468f-a645-59eb12939b1d') {
                            alert("Denominationtotal Amount Not Same To Cradit Amount!");
                            return false;
                        }
                        else {
                            alert('Denominationtotal Amount Not Same To Bebit Amount!');
                            return false;
                        }

                    }
                }
                function save() {
                    $.post(saveDataTableUrl, { "_transactionCustomerAccountViewModelCredit": _transactionCustomerAccountViewModelCredit, "_transactionCustomerAccountViewModelDebit": _transactionCustomerAccountViewModelDebit, "_sharesCessationTransactionViewModel": sharesCessationTransactionViewModels, "_transactionCustomerAccountOtherSubscriptionList": _transactionCustomerAccountOtherSubscriptionList, "_sharesTransactionViewModelList": _sharesTransactionViewModelList, "_transactionCashDenomination": _transactionCashDenomination },
                          function () {
                              alert("success");
                          })
                        .fail(function () {
                            alert("error");
                        });
                }
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
        // if (transactiontype === '408d891d-244a-4b8f-b590-0e1b2a6d5afb') {

        //    //diffamount = parseFloat($('#total-amount').attr('data-id', '0'));

        //    if ((denominationtotal == debittransactionamount) && (diffamount == 0)) {
        //        save();
        //    } else {

        // alert("denominationtotal amount not same to debit amount!");

        //    }
        //}
        // if (diffamount != 0) {

        //    save();
        //}
        //else {
        //    alert("Amount Not Tally!");
        //    return false;
        //}
        //_transactionCustomerAccountViewModelCredit = JSON.stringify({'_transactionCustomerAccountViewModelCredit' :_transactionCustomerAccountViewModelCredit});
        //_transactionCustomerAccountViewModelCredit = JSON.stringify({ '_transactionCustomerAccountViewModelCredit': _transactionCustomerAccountViewModelCredit });

        //function save() {
        //    debugger;
        //   // let data = JSON.stringify({
        //   //     //transactionCustomerAccountViewModelCredit: array_installationControl,
        //   //     "transactionCustomerAccountViewModelDebit": _transactionCustomerAccountViewModelDebit,
        //   //     "transactionCashDenomination": _transactionCashDenomination,

        //   // });
        //   //$.post(saveDataTableUrl, $.param({ data:data }, true), function (data) { });

        //    //$.post(saveDataTableUrl, { _transactionCustomerAccountViewModelDebit: _transactionCustomerAccountViewModelDebit, _transactionCashDenomination: _transactionCashDenomination },
        //    //        function () {
        //    //            alert("success");
        //    //        })
        //    //      .fail(function () {
        //    //          alert("error");
        //    //      });
        //    //$.ajax({
        //    //    type: 'post',
        //    //    url: saveDataTableUrl,
        //    //    //data: JSON.stringify({ '_transactionCustomerAccountViewModelCredit': _transactionCustomerAccountViewModelCredit }),

        //    //    //data: JSON.stringify(_transactionCustomerAccountViewModelCredit),
        //    //    //data: _transactionCustomerAccountViewModelCredit,
        //    //    ///data:_transactionCustomerAccountViewModelCredit,
        //    //    // data: "{ '_transactionCustomerAccountViewModelCredit': '" + _transactionCustomerAccountViewModelCredit1 + "'}",
        //    //    ///data: { '_transactionCustomerAccountViewModelCredit': _transactionCustomerAccountViewModelCredit},
        //    //    //data: {'_transactionCashDenomination': _transactionCashDenomination },
        //    //    //data:_transactionCustomerAccountViewModelCredit,
        //    //    data:data,
        //    //    contentType: 'application/json; charset=utf-8',
        //    //    dataType: "JSON",
        //    //    //cache: false,
        //    //    //traditional: true,
        //    //    success: function (data) {
        //    //    },
        //    //    error: function (xhr, status, error) {
        //    //        alert("An Error Has Occured While Save Data In Contact Details DataTable!!! Error Message - " + xhr.toString());
        //    //    }

        //    //})
        //}

    })
})