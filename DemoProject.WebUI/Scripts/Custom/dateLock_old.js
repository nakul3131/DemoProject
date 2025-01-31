
function GetInputDateFormat(date)
{
    debugger;
    return date.toISOString().split("T")[0];
}

// Deny Future Date
function DenyFutureDate()
{
    var today = new Date();

    $(".deny-future-date").attr('max', GetInputDateFormat(today));
}

// Deny Next Year Future Date
function DenyNextYearDate()
{
    var today = new Date();

    var nextYearDate = new Date(today);

    nextYearDate.setDate(nextYearDate.getDate() + 365);

    $(".deny-next-year-date").attr('max', GetInputDateFormat(nextYearDate));
}

// Deny Future Date From Reference Date
function DenyFutureDateFromNextReferenceDate(referenceDateId)
{
    var referenceDate = new Date(document.getElementById(referenceDateId).value);

    var futureday = new Date(referenceDate);

    futureday.setDate(futureday.getDate() + 1);

    $(".deny-future-date-from-next-reference-date").attr('min', GetInputDateFormat(futureday));
}

// Deny Future Date From Reference Date
function DenyFutureDateFromReferenceDate(referenceDateId) {
    var referenceDate = new Date(document.getElementById(referenceDateId).value);

    var futureday = new Date(referenceDate);

    futureday.setDate(futureday.getDate());

    $(".deny-future-date-from-reference-date").attr('min', GetInputDateFormat(futureday));
}

// Deny Past Date 
function DenyPastDate()
{
    var today = new Date();

    $(".deny-past-date").attr('min', GetInputDateFormat(today));
}

// Deny Past Date From Reference Date
function DenyPastDateFromReferenceDate(referenceDateId)
{
    var referenceDate = new Date(document.getElementById(referenceDateId).value);

    $(".deny-past-date-from-reference-date").attr('min', GetInputDateFormat(referenceDate));
}

// Deny Minor Birth Date
function DenyMinorBirthDate()
{
    var today = new Date();

    var senoirBirthDate = new Date(today);

    // Add 18 Years In Today Date To Get Non Minor (Senior BirthDate)
    senoirBirthDate.setDate(senoirBirthDate.getDate() - (365 * 18));

    $(".deny-minor-birth-date").attr('max', GetInputDateFormat(senoirBirthDate));
}


// Validate Date Range
function ValidDateRange() {
    var today = new Date();

    var nextYearDate = new Date(today);
    var previousYearDate = new Date(today);

    nextYearDate.setDate(nextYearDate.getDate() + 365);
    previousYearDate.setDate(previousYearDate.getDate() - 730);

    $(".valid-date-range").attr('min', GetInputDateFormat(previousYearDate));
    $(".valid-date-range").attr('max', GetInputDateFormat(nextYearDate));
}

// Validate Birth Date Range
function ValidBirthDateRange() {
    var today = new Date();
    var previousYearDate = new Date(today);

    previousYearDate.setDate(previousYearDate.getDate() - 43000);

    $(".valid-birth-date-range").attr('min', GetInputDateFormat(previousYearDate));
    $(".valid-birth-date-range").attr('max', GetInputDateFormat(today));
}

// Validate Marriage Date Range
function ValidMarriageDateRange(birthDateId) {
    debugger;
    var birthDate = new Date(document.getElementById(birthDateId).value);

    var today = new Date();
    var validMarriageDate = new Date(birthDate);

    validMarriageDate.setDate(validMarriageDate.getDate() + 6000);

    $(".valid-marriage-date-range").attr('min', GetInputDateFormat(validMarriageDate));
    $(".valid-marriage-date-range").attr('max', GetInputDateFormat(today));
}


// Deny Future Date From Reference Date
function MinMax(referenceminId, referencemaxId)
{
    var min = parseInt(document.getElementById(referenceminId).value);

    var max = parseInt(document.getElementById(referencemaxId).value);

    if (parseInt(min) > parseInt(max))
    {
        $('#' + referencemaxId).next("div.error").remove();

        $('#' + referencemaxId).after('<div class="error" style="color:red">Enter Maximum Value</div>');
    }
    else
    {
        $('#' + referencemaxId).next("div.error").remove();
    }

}

// Manage Events
$(document).ready(function ()
{
    debugger;
    const today = new Date();
    const currentMonth = today.getMonth();
    const nextFinancialYear = currentMonth > 3 ? parseInt(today.getFullYear()) + 1 : today.getFullYear();
    const currentFinacialYearLastDate = nextFinancialYear + '-03-31';
    const nextFinancialYearDate = nextFinancialYear + '-04-01';

    // Deny Past Date From Today.
    $('.deny-past-date').click(function ()
    {
        $('.deny-past-date').attr('min', GetInputDateFormat(today));
    });

    // Deny Future Date From Today.
    $('.deny-future-date').click(function ()
    {
        $('.deny-future-date').attr('max', GetInputDateFormat(today));
    });


    // Deny Next Financial Year Date.
    $('.deny-next-financial-year-date').click(function ()
    {
        $('.deny-next-financial-year-date').attr('max', currentFinacialYearLastDate);
    });
});
