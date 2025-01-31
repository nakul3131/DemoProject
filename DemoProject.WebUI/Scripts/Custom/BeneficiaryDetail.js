

// Page Load Rea-Only Confirm Account Number
if ($("#account-number").val() === "") {
    $("#confirm-account-number").attr('readonly', 'readonly');
}

$("#account-number").on('change', function () {
    $("#confirm-account-number").removeAttr("readonly");
});


$(document).ready(function () {
    $("#confirm-account-number").on('keyup', function (event) {
        debugger;
        var txtAccountNumber = $('#account-number').val();
        var txtConfirmAccountNumber = $('#confirm-account-number').val();
       
        if (txtAccountNumber != "" && txtConfirmAccountNumber != "") {
            if (txtAccountNumber == txtConfirmAccountNumber) {
                $('#confirm-account-number').next("div.error").remove();
                $('#confirm-account-number').after('<div class="error" style="color:green">Account Number Matched</div>');
                return true;
            }
            else {
                $('#confirm-account-number').next("div.error").remove();

                $('#confirm-account-number').after('<div class="error" style="color:red">Please enter a valid AccountNumber</div>');
                return false;
            }
        }
    });

    // Clear Text Box
    $("#account-number").on('keyup', function () {
        $('#confirm-account-number').next("div.error").remove();
        var txtConfirmAccountNumber = $('#confirm-account-number').val('');
    });

    // Email Address
    $('#email-id').change(function () {
        var status = false;
        var email = $('#email-id').val();
        var emailLength = email.length;
        if (email != "" && emailLength > 4) {
            email = email.replace(/\s/g, "");  //To remove space if available
            var dotIndex = email.indexOf(".");
            var atIndex = email.indexOf("@");

            if (dotIndex > -1 && atIndex > -1) {   //To check (.) and (@) present in the string
                if (dotIndex != 0 && dotIndex != emailLength && atIndex != 0 && atIndex != emailLength && email.slice(email.length - 1) != ".") {   //To check (.) and (@) are not the firat or last character of string
                    var dotCount = email.split('.').length
                    var atCount = email.split('@').length

                    if (atCount == 2 && (dotIndex - atIndex > 1)) { //To check (@) present multiple times or not in the string. And (.) and (@) not located subsequently
                        status = true;
                        if (dotCount > 2) {    //To check (.) are not located subsequently
                            for (i = 2; i <= dotCount - 1; i++) {
                                if (email.split('.')[i - 1].length == 0) {
                                    status = false;
                                }
                            }
                        }
                    }
                }
            }

            $('#email-id').next("div.error").remove();
        }

        $('#email-id').val(email);
        if (!status) {
            $('#email-id').next("div.error").remove();
            $('#email-id').after('<div class="error" style="color:red">Please enter a valid Email Address For Example - example@email.com</div>');
        }
    });

    //Code Ifsc code
    start();
    function start() {
        $('#ifsc-code').change(function () {
            $('#show-card').removeClass('d-none');

            var ifsc = String($('#ifsc-code').val());

            $.getJSON('https://ifsc.razorpay.com/' + ifsc, function (data) {

                var table = '<table class="striped">'
              + '<thead>'

              + '</thead>'
              + '<tbody>'

              + '<tr><td style="padding:3px;padding-left:25px; padding-top:8px; font-weight:bold">Bank Name</td><td style="padding-top:8px; font-weight:bold">-</td><td style="padding-top:8px; padding-right:380px">' + data.BANK + '</td></tr>'
              + '<tr><td style="padding:3px; padding-left:25px; font-weight:bold","color:red">Branch</td><td style="font-weight:bold">-</td><td style="padding-right:380px">' + data.BRANCH + '</td></tr>'
              + '<tr><td style="padding:3px;padding-left:25px; font-weight:bold">City</td><td style="font-weight:bold">-</td><td style="padding-right:380px">' + data.CITY + '</td></tr>'
              + '<tr><td style="padding:3px;padding-left:25px; padding-bottom:8px; font-weight:bold">State</td><td style="padding-bottom:8px; font-weight:bold">-</td><td style="padding-bottom:8px; padding-right:380px">' + data.STATE + '</td></tr>'
              + '</tbody>'
              + '</table>';

                $('#container').append('<div></div>');
                $('#container').append(table);

            }).fail(function () {
                var msg = '<div style="color:red">Invalid IFSC code</div>';
                $('#container').append(msg);
            });
            $('#container').css({
                'width': '100%',
                'background-color': '#efeef5',
                'color': 'blue'
            });
            $('body').append();

        });
    }

    // clear Ifsc text box
    $("#ifsc-code").on('keyup', function () {
        $('#container').next("div.error").remove();
        $('#container').empty();
    });

    // Mobile Number
    $("#mobile-number").change(function () {
        debugger;
        var mobNum = $(this).val();
            if (mobNum.length != 10) {
                $('#mobile-number').next("div.error").remove();
                $('#mobile-number').after('<div class="error" style="color:red">Please put 10  digit mobile number</div>');
                return false;
            } else {
                
                $('#mobile-number').next("div.error").remove();
                return true;
            }             
    });
  
    // Only Allow Numeric Value
    $('#mobile-number, #account-number, #customer-number, #confirm-account-number').keypress(function (e) {

        var charCode = (e.which) ? e.which : event.keyCode

        if (String.fromCharCode(charCode).match(/[^0-9]/g))

            return false;
    });

});



