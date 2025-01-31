$(document).ready(function () {
    debugger;
    var prmkey = $("#prm-key").val();

    if (prmkey < 1) {
        // Check Record Existance
        const eventObjId = ['authentication-mobile-otp', 'authentication-email-code', 'clearing-session-mobile-otp', 'clearing-session-email-code', 'unlock-account-mobile-otp', 'unlock-account-email-code'];
        var len = eventObjId.length;

        for (var i = 0; i < len; i++) {
            // if Enable Field Is True 
                $("#" + eventObjId[i] + "-display").addClass('d-none');            
        }
    }
    else {
        const eventObjId = ['authentication-mobile-otp', 'authentication-email-code', 'clearing-session-mobile-otp', 'clearing-session-email-code', 'unlock-account-mobile-otp', 'unlock-account-email-code'];
        var len = eventObjId.length;

        for (var i = 0; i < len; i++) {
            // if Enable Field Is True 
            if ($('#' + eventObjId[i]).is(":checked")) {
                $("#" + eventObjId[i] + "-display").removeClass('d-none');
            }
            else {
                $("#" + eventObjId[i] + "-display").addClass('d-none');
            }
        }
    }
});

$(".toggle").on('change', function () {
    debugger;
    var eventObjId = this.id;

    if ($('#' + eventObjId).is(":checked")) {

        $("#" + eventObjId + "-display").removeClass('d-none');
        $("#" + eventObjId + "-data-type").val('');
        $("#" + eventObjId + "-length").val('');
        $("#prefix-string-for-" + eventObjId).val('');
        $("#postfix-string-for-" + eventObjId).val('');
        $("#included-characters-for-" + eventObjId).val('');
        $("#excluded-characters-for-" + eventObjId).val('');
        $("#" + eventObjId + "-expiry-time").val('');
        $("#maximum-resend-for-" + eventObjId).val('');
    }
    else {
        $("#" + eventObjId + "-display").addClass('d-none');
    }
});