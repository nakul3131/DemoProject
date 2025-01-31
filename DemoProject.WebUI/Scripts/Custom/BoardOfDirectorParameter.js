
$(document).ready(function () {
    debugger
    var total = $("#total-number-of-directors").val();
    $(".reserve-seats").each(function () {
        $(this).keyup(function () {
            debugger
            calculateSum();
        });
    });
    $("#total-number-of-directors").each(function () {
        $(this).keyup(function () {
            debugger
            calculateSum();
        });
    });
});

function calculateSum() {
    var total = $("#total-number-of-directors").val();// get total value
    $(".reserve-seats").each(function () {
        debugger
        $(this).next("div.error").remove();

        if (!isNaN(this.value) && this.value.length != 0) {
            total -= (this.value);
            if (total < 0) {
                this.value = 0;
                if (this.value > 0) {
                    $(this).after('<div class="error" style="color:red">  Total Number Of Directors are exceeded</div>');
                    return false;
                }
            }
        }
    });
}
