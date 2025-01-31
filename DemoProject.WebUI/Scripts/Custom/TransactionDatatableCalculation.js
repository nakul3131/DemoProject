function Amountdifference() {

    //$(".amountcard").show();
    let creditamount = $('#credit-amount').attr('data-id');
    let debitamount = $('#debit-amount').attr('data-id');
    let diffamount = 0;

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
        numToWords2(diffamount)

        if (diffamount == 0) {

            $(".amountcard").hide();
        }

    } else {

        let diffamount = Math.abs(creditamount) + Math.abs(debitamount);
        $('#total-amount').attr('data-id', diffamount.toFixed(2));
        $('#total-amount').html(diffamount.toFixed(2));
        // numberToWords(diffamount);
        numToWords2(diffamount)
    };


}
function numToWords1(value) {

    let fraction = Math.round(frac(value) * 100);
    let f_text = "";

    if (fraction > 0) {
        f_text = "AND " + convert_number(fraction) + " Paise";
    }
    else {

    }


    $('.diff-amount').html("( '₹ ' + " + convert_number(value) + f_text + " ' Only')");


}
function numToWords2(value, _dataTableName) {
    debugger;
    let fraction = Math.round(frac(value) * 100);
    let f_text = "";

    if (fraction > 0) {
        f_text = " AND " + convert_number(fraction) + " Paise";

    }
    let credit = convert_number(value);
    if (credit === 'zero') {
        $("." + _dataTableName + "-amount").html("");
    }
    else {
        $("." + _dataTableName + "-amount").html("(₹ " + convert_number(value) + f_text + " Only)");

    }
}
function numToWords3(value) {

    let fraction = Math.round(frac(value) * 100);
    let f_text = "";

    if (fraction > 0) {
        f_text = "AND " + convert_number(fraction) + " Paise";
    }

    let number = convert_number(value);

    if (number === 'zero') {
        $('.denomination-amount').html('');

    }
    else {
        $('.denomination-amount').html("(₹ " + convert_number(value) + f_text + " Only)");

    }

}
function Credittransactionamountbadge(value) {

    let fraction = Math.round(frac(value) * 100);
    let f_text = "";

    if (fraction > 0) {
        f_text = "AND " + convert_number(fraction) + " Paise";
    }
    else
        if (value == 0) {
            $('#credit-transaction-amount-badge').html('');
        }
        else {
            $('#credit-transaction-amount-badge').html('₹ ' + convert_number(value) + f_text + ' Only');
        }

    if ($('#credit-transaction-amount-badge').hover(function (event) {
    $('#credit-transaction-amount-badge').attr('data-toggle', 'tooltip');
    $('#credit-transaction-amount-badge').attr('data-placement', 'top');
    $('#credit-transaction-amount-badge').attr('data-trigger', "focus");
    $('#credit-transaction-amount-badge').attr('title', '₹ ' + convert_number(value) + f_text + ' Only');

    }));
}
function Debittransactionamountbadge(value) {

    let fraction = Math.round(frac(value) * 100);
    let f_text = "";

    if (fraction > 0) {
        f_text = "AND " + convert_number(fraction) + " Paise";
    }
    else
        if (value == 0) {
            $('#debit-transaction-amount-badge').html('');
        }
        else {
            $('#debit-transaction-amount-badge').html('₹ ' + convert_number(value) + f_text + ' Only');
        }

    if ($('#debit-transaction-amount-badge').hover(function (event) {
    $('#debit-transaction-amount-badge').attr('data-toggle', 'tooltip');
    $('#debit-transaction-amount-badge').attr('data-placement', 'top');
    $('#debit-transaction-amount-badge').attr('data-trigger', "focus");
    $('#debit-transaction-amount-badge').attr('title', '₹ ' + convert_number(value) + f_text + ' Only');

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
    let Gn = Math.floor(number / 10000000);
    number -= Gn * 10000000;

    // Lakhs
    let kn = Math.floor(number / 100000);
    number -= kn * 100000;

    // Thousand 
    let Hn = Math.floor(number / 1000);
    number -= Hn * 1000;

    // Tens (deca)
    let Dn = Math.floor(number / 100);
    number = number % 100;

    // Ones 
    let tn = Math.floor(number / 10);
    let one = Math.floor(number % 10);
    let res = "";

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

    let ones = Array("", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen");
    let tens = Array("", "", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety");

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