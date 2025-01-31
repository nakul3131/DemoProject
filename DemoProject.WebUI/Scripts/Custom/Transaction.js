'use strict'
$(document).ready(function () {

    const DEPOSIT = 'DPST';
    const WITHDRAWAL = 'WDRL';
    const AGENT_COLLECTION_DEPOSIT = 'ACDP';
    const ASSET_PURCHASE = 'ASPR';
    const ASSET_SALE = 'ASSL';
    const FUND = 'FUND';
    const INCOME = 'INCM';
    const ONLINE_TRANSACTION = 'ONLT';
    const CASH = 'CAS';
    const CHEQUE = 'CHQ';
    const ONLINE_TRANSFER = 'ONT';
    const TRANSFER = 'TRF';
    const BRANCH_INTERNAL_TRANSACTION = 'BRIN';
    const SHARE_CAPITAL_LEDGER = 'Shares';

    const OTHER_TRANSACTION_MODE = "DPST,WDRL,ACDP,ASPR,ASSL,FUND,INCM";
    const ONLINE_TRANSACTION_MODE = "ONLT";

    // @@@@@@@@@@ Data Table Related Varible Declaration
    let tag = '';
    let id = '';
    let myModal;
    let selectedRowIndex;
    let row1;
    let row2;
    let row;
    let rowNum = 0;
    let rowData;
    let checked;
    let columnValues;
    let result = true;
    let dropdownListItems = '';
    let sysNameOfTransactionType = '';
    let enableCashDenomination = false;;
    let minimum;
    let maximum;
    let lastSelectedTransactionDate = '';
    let lastSelectedTransactionType = '';
    let lastSelectedPaymentMode = '';
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let arr = new Array();
    let isVerifyView = false;
    let sysNameOfGeneralLedger;
    let sGSTGeneralLedgerPrmKey;
    let cGSTGeneralLedgerPrmKey;
    let iGSTGeneralLedgerPrmKey;
    let cessGeneralLedgerPrmKey;

    // Array
    let finalDropdownListArray = [];
    let personDropdownListData = '';

    // Count
    let dropDownListItemCount = 0;
    let listItemCount = 0;

    //Credit
    let selectedCreditPersonId = '';
    let selectedCreditPersonText = '';
    let creditBranchNameId;
    let creditBranchNameText;
    let creditLedgerNameId;
    let creditLedgerNameText;
    let creditAccountNumberId;
    let creditAccountNumberText;
    let sharesFaceValue = 0;
    let numberOfShares = 0;
    let sharesAmount = 0;
    let admissionFeesMembership = 0;
    let maximumSharesHoldingLimit = 0;
    let titleFees = 0;
    let startCertificateNumber = 0;
    let endCertificateNumber = 0;
    let totalAmount = 0;
    let note = '';
    let narration = '';
    let generalLedgerPrmKey1;
    let generalLedgerPrmKey2;
    let gstRate;
    let iGSTRate;
    let cessRate;
    let cGSTAmount;
    let sGSTAmount;
    let iGSTAmount;
    let cessAmount;
    let taxableAmount;
    let isApplicableForReverseCharge;
    let particulars;
    let cashGeneralLedgerPrmKey;

    let principalAmount;
    let interestDate;
    let interestRate;
    let interestAmount;
    let penalInterestRate;
    let dueDays;
    let penalAmount;
    let npaInterestProvisionAmount;
    let interestProvisionAmount;
    let admissionFeeGeneralLedgerPrmKey1;
    let otherChargesGeneralLedgerPrmKey2;
    let receivedInterestGeneralLedgerPrmKey;
    let penalInterestGeneralLedgerPrmKey;
    let npaInterestProvisionGeneralLedgerPrmKey;
    let receivableInterestProvisionGeneralLedgerPrmKey;



    //Debit
    let selectedDebitPersonId ;
    let selectedDebitPersonText = '';

    //Cash-Denomination
    let denominationId;
    let denominationText = '';
    let numberOfPieces = 0;
    let transactionAmount;
    let denominationAmount;
    let selectedDenominationTotal;
    let totalDenominationAmount;

    //  ************** Create Data Table  **************

    let creditDataTable = CreateDataTable('credit');
    let debitDataTable = CreateDataTable('debit');
    let denominationDataTable = CreateDataTable('denomination');


    SetPageLoadingDefaultValues();

    // ###########################  U S E R      D E F I N E D       F U N C T I O N S    ###########################   
    // Set All Required Default Values On Page Loading
    function SetPageLoadingDefaultValues() {
        debugger;
        if ($('#verify-view').length > 0) {
            isVerifyView = true;
        }

        if ($('#amend-view').length > 0) {
            isAmendView = true;
        }

        lastSelectedTransactionDate = GetInputDateFormat(new Date());

        listItemCount = $('#transaction-type-id > option').not(':first').length;

        // If Dropdown Has No Record Display Error Message
        if (listItemCount === 0) {
            $('#transaction-type-id-db-error').removeClass('d-none');
        }

        // businessOfficeId Declare In View
        GetPersonDropDownList(businessOfficeId);

        SetVisibilityOfCreditDebit();

    }

    function GetPersonDropDownList(businessOfficeId) {

        $.get('/TransactionDynamicDropdownList/GetPersonDropdownListForTransaction', { _businessOfficeId: businessOfficeId, async: false }, function (data) {
            debugger;
            personDropdownListData = data;

            // If Dropdown Has No Record Display Error Message
            if (personDropdownListData.length === 0) {
                $('#credit-person-id-db-error').removeClass('d-none');
                $('#by-hand-db-error').removeClass('d-none');
            }
            else {
                $('#credit-person-id-db-error').addClass('d-none');
                $('#by-hand-db-error').addClass('d-none');
            }
        });
    }

    function SetVisibilityOfCreditDebit() {

        if ($('#transaction-type-id').prop('selectedIndex') < 1) {
            $('#credit-data-table').addClass('read-only');
            $('#debit-data-table').addClass('read-only');
            $('#cash-denomination-section').addClass('d-none');
        }

        //If Transaction Type is Branch Internal Transaction then Only shows Branch Dropdown In credit side
        if (sysNameOfTransactionType !== BRANCH_INTERNAL_TRANSACTION) {
            $('#branch-input').addClass('d-none');
            $('#credit-business-office-id').val(0);
        }
        else {
            $('#branch-input').removeClass('d-none');
        }

        if (sysNameOfTransactionType === DEPOSIT) {
            $('#credit-data-table').removeClass('read-only');
            $('#debit-data-table').addClass('read-only');
            SetVisibilityOfCashDenomination();
        }
        else if (sysNameOfTransactionType === WITHDRAWAL) {
            $('#credit-data-table').addClass('read-only');
            $('#debit-data-table').removeClass('read-only');
            SetVisibilityOfCashDenomination();
        }
        else {
            $('#credit-data-table').addClass('read-only');
            $('#debit-data-table').addClass('read-only');
            $('#cash-denomination-section').addClass('d-none');
        }
    }

    function GetCustomerAccountNumber() {
        debugger;
        let selectedBusinessOfficeId;

        if ($('#branch-input').hasClass('d-none') === false) {
            selectedBusinessOfficeId = $('#credit-business-office-id').val();
        }
        else {
            selectedBusinessOfficeId = businessOfficeId;
        }
        let generalLedgerId = $('#credit-general-ledger-id').val();

        $.get('/TransactionDynamicDropdownList/GetAccountNumberDropDownListForTransaction', { _businessOfficeId: selectedBusinessOfficeId, _generalLedgerId: generalLedgerId, _personId: selectedCreditPersonId, async: false }, function (data, textStatus, jqXHR) {
            debugger;
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select Customer Account Number --- </option>';

            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#credit-account-number-id').html(dropdownListItems);

            listItemCount = $('#credit-account-number-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount === 0) {
                $('#credit-account-number-id-db-error').removeClass('d-none');
            }
            else if (listItemCount === 1) {
                $('#credit-account-number-id').prop('selectedIndex', 1);
                $('#credit-account-number-id-db-error').addClass('d-none');
                SetSharesCapitalTransactionSettingValues();
            }
            else {
                $('#credit-account-number-id-db-error').addClass('d-none');
            }
        });

    }

    function SetGeneralLedgerDropdownList() {
        debugger;
        let selectedBusinessOfficeId;

        if ($('#branch-input').hasClass('d-none') === false) {
            selectedBusinessOfficeId = $('#credit-business-office-id').val();
        }
        else {
            selectedBusinessOfficeId = businessOfficeId;
        }

        $.get('/TransactionDynamicDropdownList/GetGeneralLedgerDropdownListForTransaction', { _personId: selectedCreditPersonId, _businessOfficeId: selectedBusinessOfficeId,  async: false }, function (data) {
            dropdownListItems = '<option value="00000000-0000-0000-0000-000000000000"> --- Select General Ledger --- </option>';

            debugger;
            $.each(data, function (index, selectListItemObj) {
                dropdownListItems += '<option value="' + selectListItemObj.Value + '">' + selectListItemObj.Text + '</option>';
            });

            $('#credit-general-ledger-id').html(dropdownListItems);

            listItemCount = $('#credit-general-ledger-id > option').not(':first').length;

            // Select Default First Record, If Dropdown Has Only One Record
            if (listItemCount === 0) {
                $('#credit-general-ledger-id-db-error').removeClass('d-none');
                $('#credit-shares-capital-input').addClass('d-none');
            }
            else if (listItemCount == 1) {
                $('#credit-general-ledger-id').prop('selectedIndex', 1);
                $('#credit-general-ledger-id-db-error').addClass('d-none');
                GetCustomerAccountNumber();
                GetSysNameOfGeneralLedger();
            }
            else {
                $('#credit-general-ledger-id-db-error').addClass('d-none');
                $('#credit-shares-capital-input').addClass('d-none');
            }

        });

    }

    function SetSharesCapitalTransactionSettingValues() {
        debugger;
        let transactionDate = $('#transaction-date').val();
        let customerAccountId = $('#credit-account-number-id').val();
        let isCredit = true;
        $.get('/TransactionChildAction/GetSharesCapitalTransactionSettingValues', { '_transactionDate': transactionDate, '_customerAccountId': customerAccountId, '_isCreditTransaction': isCredit, async: false }, function (data) {
            debugger;

            if (data.IsFirstTransaction === false) {
                $('#share-capital-fee-input').addClass('d-none');
                $('#member-admission-fee').val(0);
                $('#credit-title-for-fees1').val(0);

                //Set Min, Max to No. Of Shares if Transaction Exists For Selected Customer Account
                let minNumberOfShares = data.MinimumNumberOfShares;
                let maxNumberOfShares = data.MaximumNumberOfShares;

                $('#credit-number-of-shares').attr('min', minNumberOfShares);
                $('#credit-number-of-shares').attr('max', maxNumberOfShares);

                let maximumSharesHoldingAmount = data.MaximumSharesHolidingLimitAmount; 
                let customerAccountBalance = data.CustomerAccountBalance;
                maximumSharesHoldingLimit = maximumSharesHoldingAmount + customerAccountBalance; 
                $('#credit-shares-amount').attr('max', maximumSharesHoldingLimit);

            }
            else {
                $('#share-capital-fee-input').removeClass('d-none');
                $('#title-fees-input').removeClass('d-none');

                // Set Title For Other Fees
                let title = data.TitleForFees1;
                $('#credit-title-for-fees1').siblings('label').text(title);

                //set Value to ShareFaceValue And Other Fees
                admissionFeesMembership = data.AdmissionFeesForMembership;
                $('#member-admission-fee').val(admissionFeesMembership);

                titleFees = data.FeesAmount1;
                $('#credit-title-for-fees1').val(titleFees);

                //Set Min, Max to No. Of Shares for first Transaction For Selected Customer Account
                let minNumberOfShares = data.MinimumNumberOfShares;
                let maxNumberOfShares = data.MaximumNumberOfShares;

                $('#credit-number-of-shares').attr('min', minNumberOfShares);
                $('#credit-number-of-shares').attr('max', maxNumberOfShares);
            }

            admissionFeeGeneralLedgerPrmKey1 = data.AdmissionFeesGeneralLedgerPrmKey;
            generalLedgerPrmKey2 = data.OtherFeesGeneralLedgerPrmKey1;

            //Set Value to Shares Face Value
            sharesFaceValue = data.SharesFaceValue;
            $('#credit-shares-face-value').val(sharesFaceValue);

            //Check For Auto Generated Certificate Number
            if (data.EnableAutoCertificateNumber === false) {
                $('#shares-certificate-input').removeClass('d-none');
            }
            else {
                $('#shares-certificate-input').addClass('d-none');
            }

            //Gst Rate Setting
            let gstRate = data.GSTRate;
            if (gstRate > 0)
            {
                $('#gst-input').removeClass('d-none');

                    $('#credit-gst-rate').val(gstRate);
                    sGSTGeneralLedgerPrmKey = data.SGSTGeneralLedgerPrmKey;
                    cGSTGeneralLedgerPrmKey = data.CGSTGeneralLedgerPrmKey;
                    $('#sgst-cgst-input').removeClass('d-none');
                    $('#credit-sgst').val(data.SGSTAmount);
                    $('#credit-cgst').val(data.CGSTAmount);

                //IGST Rate Setting
                let igstRate = data.IGSTRate;
                $('#credit-igst-rate').val(igstRate);
                if (igstRate > 0) {
                    iGSTGeneralLedgerPrmKey = data.IGSTGeneralLedgerPrmKey;
                    $('#igst-amount-input').removeClass('d-none');
                    $('#credit-igst-amount').val(data.IGSTAmount);

                }
                else {
                    $('#igst-amount-input').addClass('d-none');
                    $('#credit-igst-amount').val(0);
                }

                //Cess Rate Setting
                let cessRate = data.CessRate;
                $('#credit-cess-rate').val(cessRate);
                if (cessRate > 0) {
                    cessGeneralLedgerPrmKey = data.CessGeneralLedgerPrmKey;
                    $('#cess-amount-input').removeClass('d-none');
                    $('#credit-cess-amount').val(data.CessAmount);
                }
                else {
                    $('#cess-amount-input').addClass('d-none');
                    $('#credit-cess-amount').val(0);
                }

            }
            else {
                $('#gst-input').addClass('d-none');
                $('#credit-gst-rate').val(0);
                $('#credit-cess-rate').val(0);
                $('#credit-igst-rate').val(0);
                $('#credit-cess-amount').val(0);
                $('#credit-igst-amount').val(0);
                $('#credit-sgst').val(0);
                $('#credit-cgst').val(0);

           }

        })
    }

    function SetPaymentModeDropdownList()
    {
        if (OTHER_TRANSACTION_MODE.indexOf(sysNameOfTransactionType) >= 0) {
            // Show and hide payment options in a single operation for each set
            $('#payment-mode-id').find('option[value="CAS"], option[value="CHQ"], option[value="ONT"]').removeClass('d-none');
            $('#payment-mode-id').find('option[value="NFT"], option[value="RTG"], option[value="IMP"], option[value="UPI"], option[value="TRF"]').addClass('d-none');
        }
        else if (ONLINE_TRANSACTION_MODE.indexOf(sysNameOfTransactionType) >= 0) {
            $('#payment-mode-id').find('option[value="CAS"], option[value="CHQ"], option[value="ONT"]').addClass('d-none');
            $('#payment-mode-id').find('option[value="NFT"], option[value="RTG"], option[value="IMP"], option[value="UPI"], option[value="TRF"]').removeClass('d-none');
        }
        else {
            $('#payment-mode-id').val(TRANSFER);
            $('#payment-mode-id').attr("disabled", true);
        }

        if ($('#payment-mode-id').prop('selectedIndex') < 1) {
            $('#by-hand-input').removeClass('d-none');
            $('#reference-number-input').addClass('d-none');
            $('#by-hand').val('Self');
            $('#reference-number').val('None');
        }

    }

    function GetTransactionTypeSetting() {
       
        let transactionTypeId = $('#transaction-type-id option:selected').val();

        $.get('/TransactionChildAction/GetTransactionTypeSetting', { _transactionTypeId: transactionTypeId, async: false }, function (data) {
            debugger;
            sysNameOfTransactionType = data.SysNameOfTransactionType;

            cashGeneralLedgerPrmKey = data.CashGeneralLedgerPrmKey;

            enableCashDenomination = data.EnableCashDenomination;

            let pastAllowedDate = data.UserAllowedLastPastDate;

            let subStringDate = pastAllowedDate.substring(6, pastAllowedDate.length - 2);

            // Convert to a JavaScript Date object
            let backDate = GetInputDateFormat(new Date(parseInt(subStringDate)));

            $('#transaction-date').attr("min", backDate);

            SetVisibilityOfCreditDebit();

            SetPaymentModeDropdownList();

        });
    }

    function UpdateCreditAmount(creditAmount) {
        // Update the text content
        $('#credit-amount').text(creditAmount.toFixed(2)); 

        // Optionally, update the data-id attribute
        $('#credit-amount').attr('data-id', creditAmount);
    }
    function UpdateDebitAmount(creditAmount) {
        // Update the text content
        $('#debit-amount').text(creditAmount.toFixed(2)); 

        // Optionally, update the data-id attribute
        $('#credit-amount').attr('data-id', creditAmount);
    }
    function UpdateDenominationAmount(totalDenominationAmount) {
        // Update the text content
        $('#denomination-amount').text(totalDenominationAmount.toFixed(2)); 

        // Optionally, update the data-id attribute
        $('#denomination-amount').attr('data-id', totalDenominationAmount);
    }

    function GetSysNameOfGeneralLedger() {
        let generalLedgerId = $('#credit-general-ledger-id option:selected').val();

        $.get('/TransactionChildAction/GetSysNameOfSchemeTypeByGeneralLedgerId', { _generalLedgerId: generalLedgerId, async: false }, function (data) {
            debugger;
            sysNameOfGeneralLedger = data;

            if (sysNameOfGeneralLedger === SHARE_CAPITAL_LEDGER) {
                $('#credit-shares-capital-input').removeClass('d-none');
            }
            else {
                $('#credit-shares-capital-input').addClass('d-none');
            }

        });

    }

    function SetVisibilityOfCashDenomination() {
        //If EnableCashDenomination Is true then show Cash Denomination Table Otherwise Hide it
        if (enableCashDenomination === true) {
            $('#cash-denomination-section').removeClass('d-none');
        }
        else {
            $('#cash-denomination-section').addClass('d-none');
        }
    }

    // @@@@@@@@@@@@@@@@@@@@@@  AutoComplete Events  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // AutoComplete Code for Customer Name
    $("#debit-person-id").autocomplete(
        {
            minLength: 0,
            appendTo: '#debit-person-name',
            scroll: true,
            autoFocus: true,
            source: function (request, response) {
                let responseDropdownListArray = [];
                dropDownListItemCount = finalDropdownListArray.length;

                if (parseInt(dropDownListItemCount) > 0) {
                    responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                        if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1)
                            return { label: key.Text, valueId: key.Value }
                        else
                            return null;
                    });

                    // The slice() method returns selected elements in an array, as a new array. 
                    // The slice() method selects from a given start, up to a (not inclusive) given end.
                    response(responseDropdownListArray.slice(0, 10));
                }
                else
                    response([{ label: 'No Records Found', value: -1 }]);
            },
            select: function (event, ui) {
                event.preventDefault();
                $('#debit-person-id1').val(ui.item.valueId);
                $('#debit-person-id').val(ui.item.label);
                selectedDebitPersonId = ui.item.valueId;
                selectedDebitPersonText = ui.item.label;
            }
        }).focus(function () {
            if (isVerifyView === false) {
                $('#debit-person-id').val('');
                selectedDebitPersonId = '';

                // Assign Array Without Reference  *** Use Slice Method
                finalDropdownListArray = personDropdownListData.slice();

                $(this).autocomplete('search');
            }
        });


    // AutoComplete Code for Credit Customer Name
    $("#credit-person-id").autocomplete(
        {
            minLength: 0,
            appendTo: '#credit-person-name',
            scroll: true,
            autoFocus: true,
            source: function (request, response) {
                let responseDropdownListArray = [];
                dropDownListItemCount = finalDropdownListArray.length;

                if (parseInt(dropDownListItemCount) > 0) {
                    responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                        if (key.Text.toUpperCase().indexOf(request.term.toUpperCase()) != -1)
                            return { label: key.Text, valueId: key.Value }
                        else
                            return null;
                    });

                    // The slice() method returns selected elements in an array, as a new array. 
                    // The slice() method selects from a given start, up to a (not inclusive) given end.
                    response(responseDropdownListArray.slice(0, 10));
                }
                else
                    response([{ label: 'No Records Found', value: -1 }]);
            },
            select: function (event, ui) {
                event.preventDefault();
                $('#credit-person-id1').val(ui.item.valueId);
                $('#credit-person-id').val(ui.item.label);
                selectedCreditPersonId = ui.item.valueId;
                selectedCreditPersonText = ui.item.label
            }
        }).focus(function () {
            if (isVerifyView === false) {
                $('#credit-person-id').val('');
                selectedCreditPersonId = '';

                // Assign Array Without Reference  *** Use Slice Method
                finalDropdownListArray = personDropdownListData.slice();

                $(this).autocomplete('search');
            }
        });

    $("#by-hand").autocomplete({
        minLength: 1, 
        appendTo: '#second-row',  
        scroll: true,  
        autoFocus: true,  
        source: function (request, response) {
            let responseDropdownListArray = [];

            if (finalDropdownListArray.length > 0) {
                let searchTerm = request.term.toUpperCase();

                responseDropdownListArray = $.map(finalDropdownListArray, function (key) {
                    let byHand = key.Text.toUpperCase();

                    if (byHand.indexOf(searchTerm) === 0) {
                        return { label: key.Text, valueId: key.Value };
                    }
                    return null;
                });

                if (responseDropdownListArray.length === 0) {
                    response([{ label: 'No Records Found', valueId: -1 }]);
                } else {
                    response(responseDropdownListArray.slice(0, 10));
                }
            } else {
                response([{ label: 'No Records Found', valueId: -1 }]);
            }
        },
        select: function (event, ui) {
            event.preventDefault();

            if (ui.item.valueId !== -1) {
                $('#by-hand').val(ui.item.valueId);
                $('#by-hand').val(ui.item.label);
            } else {
                // If "No Records Found" is selected, allow manual input
                $('#by-hand').val('');  // Clear the input
            }
        }
    }).focus(function () {
        if (isVerifyView === false) {
            $('#by-hand').val('');

            finalDropdownListArray = personDropdownListData.slice();

            $(this).autocomplete('search');
        }
    });

    // @@@@@@@@@@@@@@@@@@@@@@  FocusOut Events  @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // Transaction Date
    $('#transaction-date').focusout(function () {
        debugger;
        let currentSelectedDate = $('#transaction-date').val();
        let isValidTransactionDate = IsValidInputDate('#transaction-date');

        if (isValidTransactionDate === false) {
            // Clear and disable the fields
            $('select').prop('selectedIndex', 0);
            $('input[type="text"]').val('');
            creditDataTable.clear().draw();
            debitDataTable.clear().draw();
            denominationDataTable.clear().draw();

            $('#btn-add-credit-dt').prop("disabled", true);
            $('#btn-add-debit-dt').prop("disabled", true);
            $('#btn-add-denomination-dt').prop("disabled", true);
        }
        if (currentSelectedDate !== lastSelectedTransactionDate)
        {
            $('select').prop('selectedIndex', 0);
            $('input[type="text"]').val('');
            creditDataTable.clear().draw();
            debitDataTable.clear().draw();
            denominationDataTable.clear().draw();

            $('#btn-add-credit-dt').prop("disabled", true);
            $('#btn-add-debit-dt').prop("disabled", true);
            $('#btn-add-denomination-dt').prop("disabled", true);
        }

        lastSelectedTransactionDate = currentSelectedDate;

    });

    //Transaction Type
    $('#transaction-type-id').focusout(function () {
        debugger;
        $('#payment-mode-id').attr("disabled", false);

        let currentTransactionType = $('#transaction-type-id option:selected').val();
        if (currentTransactionType !== lastSelectedTransactionType) {
            $('#payment-mode-id').prop('disable', false);

            $('select').not('#transaction-type-id').prop('selectedIndex', 0);
            $('input[type="text"]').val('');
            creditDataTable.clear().draw();
            debitDataTable.clear().draw();
            denominationDataTable.clear().draw();
        }
        lastSelectedTransactionType = currentTransactionType;

        GetTransactionTypeSetting();
    });

    //payment Mode
    $('#payment-mode-id').focusout(function () {
        debugger;

        let paymentMode = $('#payment-mode-id option:selected').val();

        if (paymentMode !== lastSelectedPaymentMode) {
            $('select').not('#transaction-type-id , #payment-mode-id').prop('selectedIndex', 0);
            $('input[type="text"]').val('');
            creditDataTable.clear().draw();
            debitDataTable.clear().draw();
            denominationDataTable.clear().draw();
        }

        if (paymentMode === CASH) {
            $('#by-hand-input').removeClass('d-none');
            $('#reference-number-input').addClass('d-none');
            $('#reference-number').val('None');
        }

        if (paymentMode === CHEQUE) {
            $('#by-hand-input').addClass('d-none');
            $('#reference-number-input').removeClass('d-none');
            $('#reference-number').attr('placeholder', 'Please Enter Cheque Number Between 4 And 20 Characters Length.');
            $('#reference-number').siblings('label').text('Cheque Number / धनादेश क्रमांक');
            $('#by-hand').val('Self');
        }

        if (paymentMode === ONLINE_TRANSFER) {
            $('#by-hand-input').addClass('d-none');
            $('#reference-number-input').removeClass('d-none');
            $('#reference-number').attr('placeholder', 'Please Enter Online Transaction Reference (UPI / URN) Number Between 4 And 20 Characters Length.');
            $('#reference-number').siblings('label').text('Reference Number / संदर्भ क्रमांक');
            $('#by-hand').val('Self');
        }

        lastSelectedPaymentMode = paymentMode;
    });

    //Person Name
    $("#credit-person-id").focusout(function () {
        debugger;

        listItemCount = $('#credit-business-office-id > option').not(':first').length;

        // Select Default First Record, If Dropdown Has Only One Record
        if (listItemCount == 1) {
            $('#credit-business-office-id').prop('selectedIndex', 1);
            SetGeneralLedgerDropdownList();
        }
        else {
            SetGeneralLedgerDropdownList();
        }

    });

    //Business Office Id
    $('#credit-business-office-id').focusout(function () {
        debugger;
        SetGeneralLedgerDropdownList();
    });

    //General Ledger Id
    $('#credit-general-ledger-id').focusout(function () {
        debugger;
        $('#credit-shares-capital-input').addClass('d-none');

        GetSysNameOfGeneralLedger();
        GetCustomerAccountNumber();
    });

    //Account Number
    $('#credit-account-number-id').focusout(function () {
        debugger;
        SetSharesCapitalTransactionSettingValues();
    });

    //Number Of Shares
    $('#credit-number-of-shares').focusout(function () {
        debugger;
        let numberOfShares = $('#credit-number-of-shares').val();

        minimum = parseInt($('#credit-number-of-shares').attr('min'));
        maximum = parseInt($('#credit-number-of-shares').attr('max'));

        //Check If No. Of Shares Are Valid or not 
        if (parseInt(numberOfShares) < parseInt(minimum) || parseInt(numberOfShares) > parseInt(maximum)) {
            $('#credit-number-of-shares-error').removeClass('d-none');
        }
        else {
            let sharePrice = parseFloat($('#credit-shares-face-value').val());
            $('#credit-shares-amount').val(sharePrice * numberOfShares);

            maximum = parseFloat($('#credit-shares-amount').attr('max'));

            let shareAmount = parseFloat($('#credit-shares-amount').val());

            if (parseFloat(shareAmount) > parseFloat(maximum)) {
                $('#credit-shares-amount-limit-error').html('The Maximum Shareholding Limit For An Individual In The Cooperative Bank Has Been Exceeded. Please Ensure That Your Total Shareholding Does Not Surpass The Prescribed Limit ' + maximumSharesHoldingLimit +' Under The Applicable Cooperative Society Regulations.').removeClass('d-none');
            }

            let admissionFee = parseFloat($('#member-admission-fee').val());

            let totalAmount = shareAmount + admissionFee;

            let personName = selectedCreditPersonText;

            $('#credit-transaction-amount').val(totalAmount);

            let narration = "Under The Provisions Of The Maharashtra Cooperative Societies Act, 1960, Section 3 Rule 16 And The Society's Bye-Laws ‘" + personName + "’ Purchase ‘" + numberOfShares + "’ Shares At A Price  " + sharePrice + " With Admission Fees ‘" + admissionFee + "’.";

            $('#credit-narration').val(narration);
        }
    });

    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  D A T A      T A B L E S  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    //##################################  Credit - DataTable Code   #################

    //DataTable Add Button 
    $('#btn-add-credit-dt').click(function (event) {
        debugger;
        event.preventDefault();

        $('#credit-shares-capital-input').addClass('d-none');

        $('#share-capital-fee-input').addClass('d-none');

        let count = creditDataTable.rows().count();

        if (count >= 1) {
            $('#credit-modal').modal('hide');

            alert('You Have Not Permission To Add Second Record.');
        }
        else {
            SetModalTitle('credit', 'Add');
        }

    });

    // DataTable Edit Button 
    $('#btn-edit-credit-dt').click(function () {
        debugger;
        SetModalTitle('credit', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            myModal = $('#credit-modal').modal();

            columnValues = $('#btn-edit-credit-dt').data('rowindex');
            $('#credit-business-office-id', myModal).val(columnValues[24]);
            $('#credit-person-id', myModal).val(columnValues[2]);
            $('#credit-general-ledger-id', myModal).val(columnValues[26]);
            $('#credit-account-number-id', myModal).val(columnValues[27]);
            $('#credit-number-of-shares', myModal).val(columnValues[5]);
            $('#credit-start-certificate-number', myModal).val(columnValues[6]);
            $('#credit-end-certificate-number', myModal).val(columnValues[7]);

            $('#credit-shares-face-value', myModal).val(columnValues[8]);
            $('#credit-shares-amount', myModal).val(columnValues[9]);
            $('#member-admission-fee', myModal).val(columnValues[10]);
            $('#credit-title-for-fees1', myModal).val(columnValues[11]);
            $('#credit-gst-rate', myModal).val(columnValues[12]);
            $('#credit-sgst', myModal).val(columnValues[13]);
            $('#credit-cgst', myModal).val(columnValues[14]);
            $('#credit-igst-rate', myModal).val(columnValues[15]);
            $('#credit-igst-amount', myModal).val(columnValues[16]);
            $('#credit-cess-rate', myModal).val(columnValues[17]);
            $('#credit-cess-amount', myModal).val(columnValues[18]);
            $('#credit-is-applicable-for-reverse-charge', myModal).val(columnValues[19])
            $('#credit-transaction-amount', myModal).val(columnValues[20]);
            particulars = columnValues[21];
            $('#credit-narration', myModal).val(columnValues[22]);
            $('#credit-note', myModal).val(columnValues[23]);
            taxableAmount = columnValues[28];
            admissionFeeGeneralLedgerPrmKey1 = columnValues[29];
            generalLedgerPrmKey2 = columnValues[30];
            // Show Modals
            $('#credit-modal').modal('show');
        }
        else {
            $('#btn-edit-credit-dt').addClass('read-only');
            $('#credit-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-credit-modal').click(function () {
        debugger;
        if (IsValidCreditDetailModal()) {
            row1 = creditDataTable.row.add([
                tag,
                creditBranchNameText,//1
                selectedCreditPersonText,//2
                creditLedgerNameText,//3
                creditAccountNumberText,//4
                numberOfShares,//5
                startCertificateNumber,//6
                endCertificateNumber,//7
                sharesFaceValue,//8
                sharesAmount,//9
                admissionFeesMembership,//10
                titleFees,//11
                principalAmount,//12
                interestDate,//13
                interestRate,//14
                interestAmount,//15
                penalInterestRate,//16
                dueDays,//17
                penalAmount,//18
                npaInterestProvisionAmount,//19
                interestProvisionAmount,//20
                gstRate,//21
                sGSTAmount,//22
                cGSTAmount,//23
                iGSTRate,//24
                iGSTAmount,//25
                cessRate,//26
                cessAmount,//27
                isApplicableForReverseCharge,//28
                totalAmount,//29
                particulars,//30
                narration,//31
                note,//32
                creditBranchNameId,//33
                selectedCreditPersonId,//34
                creditLedgerNameId,//35
                creditAccountNumberId,//36
                taxableAmount,//37
                admissionFeeGeneralLedgerPrmKey1,//38
                otherChargesGeneralLedgerPrmKey2,//39
                receivedInterestGeneralLedgerPrmKey,//40
                penalInterestGeneralLedgerPrmKey,//41
                npaInterestProvisionGeneralLedgerPrmKey,//42
                receivableInterestProvisionGeneralLedgerPrmKey,//43
            ]).draw();

            row2 = debitDataTable.row.add([
                tag,
                creditBranchNameText,
                selectedCreditPersonText,
                creditLedgerNameText,
                creditAccountNumberText,
                numberOfShares,
                startCertificateNumber,
                endCertificateNumber,
                sharesFaceValue,
                sharesAmount,
                admissionFeesMembership,
                titleFees,
                gstRate,
                sGSTAmount,
                cGSTAmount,
                iGSTRate,
                iGSTAmount,
                cessRate,
                cessAmount,
                isApplicableForReverseCharge,
                totalAmount,
                particulars,
                narration,
                note,
                creditBranchNameId,
                selectedCreditPersonId,
                creditLedgerNameId,
                creditAccountNumberId,
                taxableAmount,
                admissionFeeGeneralLedgerPrmKey1,
                generalLedgerPrmKey2,
            ]).draw();

            rowNum++;
            row1.nodes().to$().attr('id', 'tr' + rowNum);
            row2.nodes().to$().attr('id', 'tr' + rowNum);

            HideCreditDataTableColumn();

            UpdateDebitAmount(totalAmount);

            UpdateCreditAmount(totalAmount);

            creditDataTable.columns.adjust().draw();
            debitDataTable.columns.adjust().draw();

            $('#credit-modal').modal('hide');

            EnableNewOperation('credit');
        }
    });

    // Modal update Button Event
    $('#btn-update-credit-modal').click(function () {
        debugger;
        $('#select-all-credit').prop('checked', false);
        if (IsValidCreditDetailModal()) {
            creditDataTable.row(selectedRowIndex).data([
                tag,
                creditBranchNameText,
                selectedCreditPersonText,
                creditLedgerNameText,
                creditAccountNumberText,
                numberOfShares,
                startCertificateNumber,
                endCertificateNumber,
                sharesFaceValue,
                sharesAmount,
                admissionFeesMembership,
                titleFees,
                principalAmount,
                interestDate,
                interestRate,
                interestAmount,
                penalInterestRate,
                dueDays,
                penalAmount,
                npaInterestProvisionAmount,
                interestProvisionAmount,
                gstRate,
                sGSTAmount,
                cGSTAmount,
                iGSTRate,
                iGSTAmount,
                cessRate,
                cessAmount,
                isApplicableForReverseCharge,
                totalAmount,
                particulars,
                narration,
                note,
                creditBranchNameId,
                selectedCreditPersonId,
                creditLedgerNameId,
                creditAccountNumberId,
                taxableAmount,
                admissionFeeGeneralLedgerPrmKey1,
                otherChargesGeneralLedgerPrmKey2,
                receivedInterestGeneralLedgerPrmKey,
                penalInterestGeneralLedgerPrmKey,
                npaInterestProvisionGeneralLedgerPrmKey,
                receivableInterestProvisionGeneralLedgerPrmKey,
            ]).draw();

            HideCreditDataTableColumn();

            UpdateCreditAmount(totalAmount);

            creditDataTable.columns.adjust().draw();

            $('#credit-modal').modal('hide');

            EnableNewOperation('credit');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-credit-dt').click(function () {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-credit tbody input[type="checkbox"]:checked').each(function () {
                    creditDataTable.row($('#tbl-credit tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();
                    debitDataTable.row($('#tbl-debit tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-credit-dt').data('rowindex');

                    UpdateCreditAmount(0);
                    UpdateDebitAmount(0);

                    EnableNewOperation('credit');
                    $('#select-all-credit').prop('checked', false);


                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of Credit Datatable
    $('#select-all-credit').click(function () {
        debugger;
        if ($(this).prop('checked')) {
            $('#tbl-credit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (creditDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-credit-dt').data('rowindex', arr);
                EnableDeleteOperation('credit')
            });
        }
        else {
            EnableNewOperation('credit')

            $('#tbl-credit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-credit tbody').click('input[type="checkbox"]', function () {
        $('#tbl-credit input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = creditDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (creditDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('credit');

                    $('#btn-update-credit-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-credit-dt').data('rowindex', rowData);
                    $('#btn-delete-credit-dt').data('rowindex', arr);
                    $('#select-all-credit').data('rowindex', arr);
                }
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

    // Validate Fund Module        changes done by indrayani
    function IsValidCreditDetailModal() {
        debugger;

        // Get Modal Inputs In Local letiable    
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        creditBranchNameId = $('#credit-business-office-id option:selected').val();
        creditBranchNameText = $('#credit-business-office-id option:selected').text();
        creditLedgerNameId = $('#credit-general-ledger-id option:selected').val();
        creditLedgerNameText = $('#credit-general-ledger-id option:selected').text();
        creditAccountNumberId = $('#credit-account-number-id option:selected').val();
        creditAccountNumberText = $('#credit-account-number-id option:selected').text();
        sharesFaceValue = parseFloat($('#credit-shares-face-value').val());
        numberOfShares = parseInt($('#credit-number-of-shares').val());
        sharesAmount = parseFloat($('#credit-shares-amount').val());
        admissionFeesMembership = parseFloat($('#member-admission-fee').val());
        titleFees = parseFloat($('#credit-title-for-fees1').val());
        startCertificateNumber = parseInt($('#credit-start-certificate-number').val());
        endCertificateNumber = parseInt($('#credit-end-certificate-number').val());
        totalAmount = parseFloat($('#credit-transaction-amount').val());
        note = $('#credit-note').val();
        narration = $('#credit-narration').val();
        isApplicableForReverseCharge = $('#credit-is-applicable-for-reverse-charge').is(':checked') ? true : false;
        gstRate = parseFloat($('#credit-gst-rate').val());
        iGSTRate = parseFloat($('#credit-igst-rate').val());
        cessRate = parseFloat($('#credit-cess-rate').val());
        cGSTAmount = parseFloat($('#credit-cgst').val());
        sGSTAmount = parseFloat($('#credit-sgst').val());
        iGSTAmount = parseFloat($('#credit-igst-amount').val());
        cessAmount = parseFloat($('#credit-cess-amount').val());

        taxableAmount = (cGSTAmount + sGSTAmount + iGSTAmount + cessAmount);
        particulars = 'None';

        let result = true;

        if (note === '') {
            note = 'None';
        }

        //Customer Id
        if (selectedCreditPersonId === '') {
            result = false;
            $('#credit-person-id-error').removeClass('d-none');
        }

        //Business Office Id
        if ($('#branch-input').hasClass('d-none') === false) {
            if ($('#credit-business-office-id').prop('selectedIndex') < 1) {
                result = false;
                $('#credit-business-office-id-error').removeClass('d-none');
            }
        }
        else {
            $('#credit-business-office-id').val(0);
        }

        //General Ledger Id
        if ($('#credit-general-ledger-id').prop('selectedIndex') < 1) {
            result = false;
            $('#credit-general-ledger-id-error').removeClass('d-none');
        }

        //Customer Account Id
        if ($('#credit-account-number-id').prop('selectedIndex') < 1) {
            result = false;
            $('#credit-account-number-id-error').removeClass('d-none');
        }


        if ($('#credit-shares-capital-input').hasClass('d-none') === false) {

            //Validation For Share Face Value
            if (isNaN(sharesFaceValue) === false) {
                minimum = parseFloat($('#credit-shares-face-value').attr('min'));
                maximum = parseFloat($('#credit-shares-face-value').attr('max'));

                if (parseFloat(sharesFaceValue) < parseFloat(minimum) || parseFloat(sharesFaceValue) > parseFloat(maximum)) {
                    result = false;
                    $('#credit-shares-face-value-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#credit-shares-face-value-db-error').removeClass('d-none');
            }

            //Validation For Number Of Shares
            if (isNaN(numberOfShares) === false) {
                minimum = parseInt($('#credit-number-of-shares').attr('min'));
                maximum = parseInt($('#credit-number-of-shares').attr('max'));

                if (parseInt(numberOfShares) < parseInt(minimum) || parseInt(numberOfShares) > parseInt(maximum)) {
                    result = false;
                    $('#credit-number-of-shares-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#credit-number-of-shares-error').removeClass('d-none');
            }

            //Validation For Shares Amount
            if (isNaN(sharesAmount) === false) {
                minimum = parseFloat($('#credit-shares-amount').attr('min'));
                maximum = parseFloat($('#credit-shares-amount').attr('max'));

                if (parseFloat(sharesAmount) < parseFloat(minimum) || parseFloat(sharesAmount) > parseFloat(maximum)) {
                    result = false;
                    $('#credit-shares-amount-holding-limit-error').html('The Maximum Shareholding Limit For An Individual In The Cooperative Bank Has Been Exceeded. Please Ensure That Your Total Shareholding Does Not Surpass The Prescribed Limit ' + maximum + ' Under The Applicable Cooperative Society Regulations.').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#credit-shares-amount-error').removeClass('d-none');
            }

            if ($('#share-capital-fee-input').hasClass('d-none') === false) {

                //Validation For Addmission Fee
                if (isNaN(admissionFeesMembership) === false) {
                    minimum = parseFloat($('#member-admission-fee').attr('min'));
                    maximum = parseFloat($('#member-admission-fee').attr('max'));

                    if (parseFloat(admissionFeesMembership) < parseFloat(minimum) || parseFloat(admissionFeesMembership) > parseFloat(maximum)) {
                        result = false;
                        $('#member-admission-fee-error').removeClass('d-none');
                    }
                }
                else {
                    result = false;
                    $('#member-admission-fee-db-error').removeClass('d-none');
                }

                //Validation For Title Fee
                if (isNaN(titleFees) === false) {
                    minimum = parseFloat($('#credit-title-for-fees1').attr('min'));
                    maximum = parseFloat($('#credit-title-for-fees1').attr('max'));

                    if (parseFloat(titleFees) < parseFloat(minimum) || parseFloat(titleFees) > parseFloat(maximum)) {
                        result = false;
                        $('#credit-title-for-fees1-error').removeClass('d-none');
                    }
                }
                else {
                    result = false;
                    $('#credit-title-for-fees1-db-error').removeClass('d-none');
                }

            }

            if ($('#shares-certificate-input').hasClass('d-none') === false) {

                //Validation For Start Certificate Number
                if (isNaN(startCertificateNumber) === false) {
                    minimum = parseInt($('#credit-start-certificate-number').attr('min'));
                    maximum = parseInt($('#credit-start-certificate-number').attr('max'));

                    if (parseInt(startCertificateNumber) < parseInt(minimum) || parseInt(startCertificateNumber) > parseInt(maximum)) {
                        result = false;
                        $('#credit-start-certificate-number-error').removeClass('d-none');
                    }
                }
                else {
                    result = false;
                    $('#credit-start-certificate-number-error').removeClass('d-none');
                }

                //Validation For End Certificate Number
                if (isNaN(endCertificateNumber) === false) {
                    minimum = parseInt($('#credit-end-certificate-number').attr('min'));
                    maximum = parseInt($('#credit-end-certificate-number').attr('max'));

                    if (parseInt(endCertificateNumber) < parseInt(minimum) || parseInt(endCertificateNumber) > parseInt(maximum)) {
                        result = false;
                        $('#credit-end-certificate-number-error').removeClass('d-none');
                    }
                }
                else {
                    result = false;
                    $('#credit-end-certificate-number-error').removeClass('d-none');
                }
            }

            if (gstRate > 0 || iGSTRate > 0 || cessRate > 0) {
                totalAmount = totalAmount + taxableAmount;
            }
        }

        return result;

    }

    // Hide Unnecessary Columns
    function HideCreditDataTableColumn() {
        creditDataTable.column(1).visible(false);

        if ($('#credit-branch-input').hasClass('d-none') === true) {
            creditDataTable.column(33).visible(false);
        }

        if (gstRate === 0) {
            creditDataTable.column(21).visible(false);
            creditDataTable.column(22).visible(false);
            creditDataTable.column(23).visible(false);
            creditDataTable.column(24).visible(false);
            creditDataTable.column(25).visible(false);
            creditDataTable.column(26).visible(false);
            creditDataTable.column(27).visible(false);
        }
        else {
            creditDataTable.column(21).visible(true);
            creditDataTable.column(22).visible(true);
            creditDataTable.column(23).visible(true);
            creditDataTable.column(24).visible(true);
            creditDataTable.column(25).visible(true);
            creditDataTable.column(26).visible(true);
            creditDataTable.column(27).visible(true);
        }

        creditDataTable.column(12).visible(false);
        creditDataTable.column(13).visible(false);
        creditDataTable.column(14).visible(false);
        creditDataTable.column(15).visible(false);
        creditDataTable.column(16).visible(false);
        creditDataTable.column(17).visible(false);
        creditDataTable.column(18).visible(false);
        creditDataTable.column(19).visible(false);
        creditDataTable.column(20).visible(false);

        creditDataTable.column(38).visible(false);
        creditDataTable.column(39).visible(false);
        creditDataTable.column(40).visible(false);
        creditDataTable.column(41).visible(false);
        creditDataTable.column(42).visible(false);
        creditDataTable.column(43).visible(false);

    }


    // #################  Debit - DataTable Code   #################

    // DataTable Add Button 
    $('#btn-add-debit-dt').click(function (event) {
        debugger;
        event.preventDefault();

        SetModalTitle('debit', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-debit-dt').click(function () {
        debugger;
        SetModalTitle('debit', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            myModal = $('#debit-modal').modal();

            columnValues = $('#btn-edit-debit-dt').data('rowindex');

            // Show Modals
            $('#debit-modal').modal('show');
        }
        else {
            $('#btn-edit-debit-dt').addClass('read-only');
            $('#debit-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-debit-modal').click(function () {
        debugger;
        if (IsValidDebitDetailModal()) {
            row = debitDataTable.row.add([
                tag,
                note,
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideDebitDetail();

            debitDataTable.columns.adjust().draw();

            // Hide Table Required Data Error Message
            $('#acquaitance-detail-accordion-error').addClass('d-none');

            $('#debit-modal').modal('hide');

            EnableNewOperation('debit');
        }
    });

    // Modal update Button Event
    $('#btn-update-debit-modal').click(function () {
        debugger;
        $('#select-all-debit').prop('checked', false);
        if (IsValidDebitDetailModal()) {
            debitDataTable.row(selectedRowIndex).data([

                tag,
                note,
            ]).draw();

            HideDebitDetail();

            debitDataTable.columns.adjust().draw();

            $('#debit-modal').modal('hide');

            EnableNewOperation('debit');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-debit-dt').click(function () {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-debit tbody input[type="checkbox"]:checked').each(function () {
                    debitDataTable.row($('#tbl-debit tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-debit-dt').data('rowindex');
                    EnableNewOperation('debit');
                    $('#select-all-debit').prop('checked', false);

                    // Add Required Error Message, If Table Has Not Any Record
                    if (!debitDataTable.data().any())
                        $('#acquaitance-detail-accordion-error').removeClass('d-none');


                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-debit').click(function () {
        debugger;
        if ($(this).prop('checked')) {
            $('#tbl-debit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (debitDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-debit-dt').data('rowindex', arr);
                EnableDeleteOperation('debit')
            });
        }
        else {
            EnableNewOperation('debit')

            $('#tbl-debit tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-debit tbody').click('input[type="checkbox"]', function () {
        $('#tbl-debit input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = debitDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (debitDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('debit');

                    $('#btn-update-debit-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-debit-dt').data('rowindex', rowData);
                    $('#btn-delete-debit-dt').data('rowindex', arr);
                    $('#select-all-debit').data('rowindex', arr);
                }
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

    // Validate Fund Module        changes done by indrayani
    function IsValidDebitDetailModal() {
        debugger;

        // Get Modal Inputs In Local letiable    
        tag = '<input type="checkbox" name="select-all" class="checks"/>';

        return result;

    }

    // Hide Unnecessary Columns
    function HideDebitDetail() {
        debitDataTable.column(1).visible(false);
        debitDataTable.column(3).visible(false);
    }


    // #################  Cash Denomination - DataTable Code   #################

    // DataTable Add Button 
    $('#btn-add-denomination-dt').click(function (event) {
        debugger;
        event.preventDefault();

        SetModalTitle('denomination', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-denomination-dt').click(function () {
        debugger;
        SetModalTitle('denomination', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            myModal = $('#denomination-modal').modal();

            columnValues = $('#btn-edit-denomination-dt').data('rowindex');

            $('#denomination-id', myModal).val(columnValues[1]);
            $('#pieces-cash-denomination', myModal).val(columnValues[3]);
            $('#note-cash-denomination', myModal).val(columnValues[4]);

            // Show Modals
            $('#denomination-modal').modal('show');
        }
        else {
            $('#btn-edit-denomination-dt').addClass('read-only');
            $('#denomination-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-denomination-modal').click(function () {
        debugger;
        if (IsValidDenominationDetailModal()) {
            row = denominationDataTable.row.add([
                tag,
                denominationId,
                denominationText,
                numberOfPieces,
                totalDenominationAmount,
                note,
            ]).draw();

            rowNum++;
            row.nodes().to$().attr('id', 'tr' + rowNum);

            HideDenominationDetail();

            denominationDataTable.columns.adjust().draw();

            // Hide Table Required Data Error Message
            //$('#acquaitance-detail-accordion-error').addClass('d-none');

            $('#denomination-modal').modal('hide');

            UpdateDenominationAmount(totalDenominationAmount);

            EnableNewOperation('denomination');
        }
    });

    // Modal update Button Event
    $('#btn-update-denomination-modal').click(function () {
        debugger;
        $('#select-all-denomination').prop('checked', false);
        if (IsValidDenominationDetailModal()) {
            denominationDataTable.row(selectedRowIndex).data([
                tag,
                denominationId,
                denominationText,
                numberOfPieces,
                totalDenominationAmount,
                note,
            ]).draw();

            HideDenominationDetail();

            denominationDataTable.columns.adjust().draw();

            $('#denomination-modal').modal('hide');

            UpdateDenominationAmount(totalDenominationAmount);

            EnableNewOperation('denomination');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-denomination-dt').click(function () {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-denomination tbody input[type="checkbox"]:checked').each(function () {
                    denominationDataTable.row($('#tbl-denomination tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-denomination-dt').data('rowindex');

                    UpdateDenominationAmount(0);

                    EnableNewOperation('denomination');
                    $('#select-all-denomination').prop('checked', false);

                }));
            }
        }
        else
            alert('Please select a checkbox');
    });

    // Select All Check Box Click Event - On Click Select All Checkbox Of OtherFundSubscrption Datatable
    $('#select-all-denomination').click(function () {
        debugger;
        if ($(this).prop('checked')) {
            $('#tbl-denomination tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                rowData = (denominationDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-denomination-dt').data('rowindex', arr);
                EnableDeleteOperation('denomination')
            });
        }
        else {
            EnableNewOperation('denomination')

            $('#tbl-denomination tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-denomination tbody').click('input[type="checkbox"]', function () {
        $('#tbl-denomination input[type="checkbox"]:checked').each(function () {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                row = $(this).closest('tr');
                selectedRowIndex = denominationDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (denominationDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('denomination');

                    $('#btn-update-denomination-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-denomination-dt').data('rowindex', rowData);
                    $('#btn-delete-denomination-dt').data('rowindex', arr);
                    $('#select-all-denomination').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-denomination tbody input[type="checkbox"]');

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
            $('#select-all-denomination').prop('checked', false);
    });

    // Validate Fund Module        changes done by indrayani
    function IsValidDenominationDetailModal() {
        debugger;

        // Get Modal Inputs In Local letiable    
        tag = '<input type="checkbox" name="select-all" class="checks"/>';
        denominationId = $('#denomination-id option:selected').val();
        denominationText = $('#denomination-id option:selected').text();
        numberOfPieces = parseInt($('#pieces-cash-denomination').val());
        note = $('#note-cash-denomination').val();
        totalDenominationAmount = parseInt(denominationText) * parseInt(numberOfPieces);
        transactionAmount = parseFloat($('#credit-amount').attr('data-id'));
        denominationAmount = parseFloat($('#denomination-amount').attr('data-id'));
        selectedDenominationTotal = parseInt(denominationText) * parseInt(numberOfPieces);

        let result = true;

        if (note === '') {
            note = 'None';
        }

        if ($('#denomination-id').prop('selectedIndex') < 1) {
            result = false;
        }

           // isValid = true;

     let isValid = true;

        if (transactionAmount <= denominationAmount) {
            debugger;
            let differenceAmt = denominationAmount - transactionAmount;

            if (selectedDenominationTotal > differenceAmt) {

                alert('Change Amount Is Less Than Selected Denomination Amount');
                isValid = false;
                return false;
            }
            else {
                numberOfPieces = -numberOfPieces;
                totalDenominationAmount = denominationAmount - selectedDenominationTotal;
                isValid = true;
            }
        }
        else if (transactionAmount >= denominationAmount) {
            let differenceAmt = denominationAmount - transactionAmount;

            if (selectedDenominationTotal < differenceAmt) {

                alert('Change Amount Is Less Than Selected Denomination Amount');
                isValid = false;
                return false;
            }
            else {
                numberOfPieces = +numberOfPieces;
                totalDenominationAmount = denominationAmount + selectedDenominationTotal;
                isValid = true;
            }

        }

        return result;

    }

    // Hide Unnecessary Columns
    function HideDenominationDetail() {
        denominationDataTable.column(1).visible(false);
    }

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@

    $('#btnsave').click(function (event) {
        debugger;
        let isValidAllInputs = true;
        if ($('form').valid()) {

            $('.lastrow').remove();

            // To Pass List Object Parameter, Create Array Objects And Get Values.

            
            let transactionCustomerAccountArray = new Array();
            let transactionGeneralLedgerArray = new Array();
            let transactionGSTDetailArray = new Array();

            let sharesCapitalTransactionArray = new Array();
            let denominationArray = new Array();


            // Create Array For person chronic disease Table To Pass Data

            if (creditDataTable.data().any()) {

                if (isValidAllInputs)
                {
                    // Credit Data Table
                    $('#tbl-credit > tbody > tr').each(function ()
                    {
                        debugger;
                        currentRow = $(this).closest('tr');

                        columnValues = (creditDataTable.row(currentRow).data());

                        // Handling Code If Row Is Undefined Or Null
                        if (typeof columnValues !== 'undefined' && columnValues !== null)
                        {
                            // Add In TransactionCustomerAccount If CustomerAccountId Is Not Zero
                            if (columnValues[36] !== '00000000-0000-0000-0000-000000000000')
                            {
                                transactionCustomerAccountArray.push(
                                    {
                                        'BusinessOfficeId': columnValues[33],
                                        'TransactionCustomerAccountId': columnValues[36],
                                        'Amount': columnValues[29],
                                        'IsCredit': true,
                                        'Balance': 0,
                                        'Narration': columnValues[31],
                                        'Note': columnValues[32]
                                    });
                            }
                            else
                            {
                                transactionGeneralLedgerArray.push(
                                    {
                                        'BusinessOfficeId': columnValues[33],
                                        'GeneralLedgerId': columnValues[35],
                                        'GeneralLedgerPrmKey': columnValues[YY],
                                        'PersonId': columnValues[34],
                                        'Particulars': columnValues[30],
                                        'Amount': columnValues[29],
                                        'IsCredit': true,
                                        'Narration': columnValues[31],
                                        'Note': columnValues[32]
                                    });
                            }

                            // Transaction General Ledger
                            // Shares For Admission Fee
                            if (parseFloat(columnValues[10]) > 0) {
                                transactionGeneralLedgerArray.push(
                                    {
                                        'BusinessOfficeId': columnValues[33],
                                        'GeneralLedgerId': '00000000-0000-0000-0000-000000000000',
                                        'GeneralLedgerPrmKey': columnValues[38],
                                        'PersonId': columnValues[34],
                                        'Particulars': columnValues[30],
                                        'Amount': columnValues[29],
                                        'IsCredit': true,
                                        'Narration': columnValues[31],
                                        'Note': columnValues[32]
                                    });
                            }

                            // Shares For OtherCharges
                            if (parseFloat(columnValues[11]) > 0) {
                                transactionGeneralLedgerArray.push(
                                    {
                                        'BusinessOfficeId': columnValues[33],
                                        'GeneralLedgerId': '00000000-0000-0000-0000-000000000000',
                                        'GeneralLedgerPrmKey': columnValues[39],
                                        'PersonId': columnValues[34],
                                        'Particulars': columnValues[30],
                                        'Amount': columnValues[29],
                                        'IsCredit': true,
                                        'Narration': columnValues[31],
                                        'Note': columnValues[32]
                                    });
                            }

                            // Shares Capital Transaction
                            if (parseInt(columnValues[5]) > 0)
                            {
                                sharesCapitalTransactionArray.push(
                                    {
                                        'NumberOfShares': columnValues[5],
                                        'StartSharesCertificateNumber': columnValues[6],
                                        'EndSharesCertificateNumber': columnValues[7],
                                        'SharesFaceValue': columnValues[8],
                                        'IsPrinted': false,
                                        'IsSharesCertificateIssued': false,
                                        'IsReturned': false
                                    });
                            }

                            // GST Details - If Only GST Rate > 0
                            if (columnValues[21] > 0)
                            {
                                // Add In TransactionGeneralLedger For CGST
                                if (columnValues[23] > 0) {
                                    transactionGeneralLedgerArray.push(
                                        {
                                            'BusinessOfficeId': columnValues[33],
                                            'GeneralLedgerId': '00000000-0000-0000-0000-000000000000',
                                            'GeneralLedgerPrmKey': columnValues[CGST_GENERAL_LEDGER_PRMKEY_COLUMN_NO],
                                            'PersonId': columnValues[25],
                                            'Particulars': columnValues[21],
                                            'Amount': columnValues[23],
                                            'IsCredit': true,
                                            'Narration': columnValues[22],
                                            'Note': columnValues[23]
                                        });
                                }

                                // Add In TransactionGeneralLedger For SGST
                                if (columnValues[22] > 0) {
                                    transactionGeneralLedgerArray.push(
                                        {
                                            'BusinessOfficeId': columnValues[23],
                                            'GeneralLedgerId': '00000000-0000-0000-0000-000000000000',
                                            'GeneralLedgerPrmKey': columnValues[SGST_GENERAL_LEDGER_PRMKEY_COLUMN_NO],
                                            'PersonId': columnValues[25],
                                            'Particulars': columnValues[21],
                                            'Amount': columnValues[22],
                                            'IsCredit': true,
                                            'Narration': columnValues[22],
                                            'Note': columnValues[23]
                                        });
                                }

                                // Add In TransactionGeneralLedger For IGST
                                if (columnValues[XX] > 0) {
                                    transactionGeneralLedgerArray.push(
                                        {
                                            'BusinessOfficeId': columnValues[23],
                                            'GeneralLedgerId': '00000000-0000-0000-0000-000000000000',
                                            'GeneralLedgerPrmKey': columnValues[IGST_GENERAL_LEDGER_PRMKEY_COLUMN_NO],
                                            'PersonId': columnValues[25],
                                            'Particulars': columnValues[21],
                                            'Amount': columnValues[IGST_AMOUNT],
                                            'IsCredit': true,
                                            'Narration': columnValues[22],
                                            'Note': columnValues[23]
                                        });
                                }

                                // Add In TransactionGeneralLedger For CESS
                                if (columnValues[XX] > 0) {
                                    transactionGeneralLedgerArray.push(
                                        {
                                            'BusinessOfficeId': columnValues[23],
                                            'GeneralLedgerId': '00000000-0000-0000-0000-000000000000',
                                            'GeneralLedgerPrmKey': columnValues[CESS_GENERAL_LEDGER_PRMKEY_COLUMN_NO],
                                            'PersonId': columnValues[25],
                                            'Particulars': columnValues[21],
                                            'Amount': columnValues[CESS_AMOUNT],
                                            'IsCredit': true,
                                            'Narration': columnValues[22],
                                            'Note': columnValues[23]
                                        });
                                }

                                // Finally Add In GST Detail Table
                                transactionGSTDetailArray.push(
                                    {
                                        'InvoiceNumber': 'None',
                                        'TaxableAmount': columnValues[28],
                                        'GSTRate': columnValues[12],
                                        'SGST': columnValues[13],
                                        'CGST': columnValues[14],
                                        'IGSTRate': columnValues[15],
                                        'GSTAmount': columnValues[16],
                                        'CessRate': columnValues[17],
                                        'CessAmount': columnValues[18],
                                        'IsApplicableForReverseCharge': columnValues[19],
                                        'Narration': columnValues[22],
                                        'Note': columnValues[23]
                                    });
                            }
                        }
                        else
                            return false;
                    });

                    // Debit Data Table

                }
            }
            else {
                isValidAllInputs = false;
            }

            // Cash Denomination - Create Array For vehicle Loan Photo Data Table To Pass Data
            if ($('#cash-denomination-section').hasClass('d-none') === false) {
                if (denominationDataTable.data().any()) {

                    let transactionAmount = parseFloat($('#credit-amount').attr('data-id'));
                    let denominationTotal = parseFloat($('#denomination-amount').attr('data-id'));
                    let diffAmount = denominationTotal - transactionAmount;

                    if ((denominationTotal !== transactionAmount) && (diffAmount !== 0)) {

                        if (transactionAmount < denominationTotal) {
                            alert("Denomination Amount Not Same To Cradit Amount!");
                            return false;
                        }
                    }
                    else {

                        if (isValidAllInputs) {

                            $('#tbl-denomination > tbody > tr').each(function () {
                                currentRow = $(this).closest('tr');

                                columnValues = (denominationDataTable.row(currentRow).data());

                                // Handling Code If Row Is Undefined Or Null
                                if (typeof columnValues !== 'undefined' && columnValues !== null) {
                                    denominationArray.push(
                                        {
                                            'DenominationId': columnValues[1],
                                            'Pieces': columnValues[3],
                                            'Note': columnValues[5],
                                        });

                                }
                                else
                                    return false;
                            });
                        }

                    }
                }
                else {
                    isValidAllInputs = false;
                }

            }

            // Call Controller Save Method 
            if (isValidAllInputs) {
                $.ajax({
                    url: saveDataTableUrl,
                    type: 'POST',
                    async: false,
                    data: {
                        '_customerAccount': transactionCustomerAccountArray, '_generalLedger': transactionGeneralLedgerArray, '_sharesCapital': sharesCapitalTransactionArray, '_gstDetail': transactionGSTDetailArray, '_cashDenomination': denominationArray,
                    },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {

                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Transaction!!! Error Message - ' + error.toString());
                    }
                })
            }
            else {
                alert('Oops, Something Went Wrong!, Please Provide Valid Data........');
                event.preventDefault();
            }

        }

        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });

});