function GetInputDateFormat(_date) {
    let result = '';

    if (!isNaN(_date))
        result = _date.toISOString().split('T')[0];

    return result;
}

function GetOnlyDate(_date)
{
    let result = '';

    if (_date.length > 6) {
        result = new Date(_date);

        // slice is used for two digit year and day
        result = result.getFullYear() + '-' + ('0' + (result.getMonth() + 1)).slice(-2) + '-' + ('0' + result.getDate()).slice(-2);
    }

    return result;
}

function MonthDiffernece(_startDate, _endDate)
{
    // _startDate Is First i.e Is Small Date
    // _endDate Is Last i.e Is Large Date Than _startDate

    let months = 0;
    let firstDate = new Date(_startDate);
    let lastDate = new Date(_endDate);

    months = (lastDate.getFullYear() - firstDate.getFullYear()) * 12;
    months -= firstDate.getMonth();
    months += lastDate.getMonth();

    return months <= 0 ? 0 : months;
}

// Deny Future Date From Reference Date
function DenyFutureDateFromNextReferenceDate(_referenceDateId) {
    var referenceDate = new Date(document.getElementById(_referenceDateId).value);

    var futureday = new Date(referenceDate);

    futureday.setDate(futureday.getDate() + 1);

    $('.deny-future-date-from-next-reference-date').attr('min', GetInputDateFormat(futureday));
}

// Deny Future Date From Reference Date
function DenyFutureDateFromReferenceDate(_referenceDateId) {
    var referenceDate = new Date(document.getElementById(_referenceDateId).value);

    var futureday = new Date(referenceDate);

    futureday.setDate(futureday.getDate());

    $('.deny-future-date-from-reference-date').attr('min', GetInputDateFormat(futureday));
}

// Deny Past Date From Reference Date
function DenyPastDateFromReferenceDate(_referenceDateId) {

    let referenceDate = new Date(document.getElementById(_referenceDateId).value);

    let pastdate = new Date(referenceDate);

    pastdate.setDate(pastdate.getDate() + 1);

    $('.deny-past-date-from-reference-date').attr('min', GetInputDateFormat(pastdate));
}

// Validate Marriage Date Range
function ValidMarriageDateRange(birthDateId) {
    var birthDate = new Date($('#' + birthDateId).val());

    var today = new Date();
    var validMarriageDate = new Date(birthDate);

    // Enable Date Older Than 16 Years
    validMarriageDate.setDate(validMarriageDate.getDate() + 5844);

    if (validMarriageDate > today)
    {
        validMarriageDate = today;
    }

    $('.valid-marriage-date-range').attr('min', GetInputDateFormat(validMarriageDate));
    $('.valid-marriage-date-range').attr('max', GetInputDateFormat(today));
}

// Validate Date 
function IsValidInputDate(_dateId)
{
    let result = true;
    let inputdateValue = new Date($(_dateId).val());
    let today = new Date();
    let minDate = new Date($(_dateId).attr('min'));
    let maxDate = new Date($(_dateId).attr('max'));
    let isMandatory = $(_dateId).hasClass('mandatory-mark');

    if (isNaN(inputdateValue) == false) {
        if (inputdateValue < minDate)
            result = false;

        if (inputdateValue > maxDate)
            result = false;
    }
    else {
        if (isMandatory)
            result = false;
    }

    return result;
}

// Deny Future Date From Reference Date
function MinMax(referenceminId, referencemaxId) {
    var min = parseInt(document.getElementById(referenceminId).value);

    var max = parseInt(document.getElementById(referencemaxId).value);

    if (parseInt(min) > parseInt(max)) {
        $('#' + referencemaxId).next("div.error").remove();

        $('#' + referencemaxId).after('<div class="error" style="color:red">Enter Maximum Value</div>');
    }
    else {
        $('#' + referencemaxId).next("div.error").remove();
    }

}

// Manage Events
$(document).ready(function () {
    // getmonth() start from 0. We may want to have d1.getMonth() + 1 to achieve what you want / actual Current month.
    const today = new Date();

    const pastDateBefore1Year = new Date();
    pastDateBefore1Year.setFullYear(today.getFullYear() - 1);

    const pastDateBefore150Year = new Date();
    pastDateBefore150Year.setFullYear(today.getFullYear() - 150);

    const pastDateBefore100Year = new Date();
    pastDateBefore100Year.setFullYear(today.getFullYear() - 100);

    const pastDateBefore50Year = new Date();
    pastDateBefore50Year.setFullYear(today.getFullYear() - 50);
    
    const pastDateBefore30Year = new Date();
    pastDateBefore30Year.setFullYear(today.getFullYear() - 30);

    const pastDateBefore25Year = new Date();
    pastDateBefore25Year.setFullYear(today.getFullYear() - 25);

    const pastDateBefore10Year = new Date();
    pastDateBefore10Year.setFullYear(today.getFullYear() - 10);

    const pastDateBefore5Year = new Date();
    pastDateBefore5Year.setFullYear(today.getFullYear() - 5);

    const pastDateBefore6Month = new Date();
    pastDateBefore6Month.setMonth(today.getMonth() - 6);

    const futureDateAfter30Year = new Date();
    futureDateAfter30Year.setFullYear(today.getFullYear() + 30);

    const futureDateAfter50Year = new Date();
    futureDateAfter50Year.setFullYear(today.getFullYear() + 50);

    const futureDateAfter20Year = new Date();
    futureDateAfter20Year.setFullYear(today.getFullYear() + 20);

    const futureDateAfter10Year = new Date();
    futureDateAfter10Year.setFullYear(today.getFullYear() + 10);

    let tmpDate = '';

    // Get Opened Financial Year First Date.
    $.get('/AccountChildAction/GetFirstDateOfOpenFinancialYear', { async: false }, function (data, textStatus, jqXHR) {
        tmpDate = data;
    });

    const firstDateOfOpenFinancialYear = tmpDate;
    const currentMonth = today.getMonth() + 1;
    const startFinancialYear = parseInt(currentMonth) < 4 ? parseInt(today.getFullYear()) - 1 : today.getFullYear();
    const endFinancialYear = parseInt(currentMonth) > 3 ? parseInt(today.getFullYear()) + 1 : today.getFullYear();
    const currentFinancialYearStartDate = startFinancialYear + '-04-01';
    const currentFinacialYearLastDate = endFinancialYear + '-03-31';

    // Set Default Current Date
    $('.set-current-date').val(GetInputDateFormat(today));

    // Deny Future Date From Today.
    $('.deny-future-date').click(function () {
        $(this).attr('max', GetInputDateFormat(today));
    });

    // Deny Next Financial Year Date.
    $('.deny-next-financial-year-date').click(function () {
        $(this).attr('max', currentFinacialYearLastDate);
    });

    // Deny Past Date From Today.
    $('.deny-past-date').click(function () {
        $(this).attr('min', GetInputDateFormat(today));
    });

    // Allow Past Date Upto 1 Years. 
    $('.allow-past-date-upto-1y').click(function () {
        $(this).attr('min', GetInputDateFormat(pastDateBefore1Year));
    });

    // Allow Past Date Upto 150 Years. (For - BirthDate)
    $('.allow-past-date-upto-150y').click(function () {
        $(this).attr('min', GetInputDateFormat(pastDateBefore150Year));
    });

    // Allow Past Date Upto 100 Years. (For - BirthDate)
    $('.allow-past-date-upto-100y').click(function () {
        $(this).attr('min', GetInputDateFormat(pastDateBefore100Year));
    });

    // Allow Past Date Upto 50 Years. (For - Vehicle / Land / Flat Purchase Date)
    $('.allow-past-date-upto-50y').click(function () {
        $(this).attr('min', GetInputDateFormat(pastDateBefore50Year));
    });

    // Allow Past Date Upto 50 Years. (For - Vehicle / Land / Flat Purchase Date)
    $('.allow-past-date-upto-30y').click(function () {
        $(this).attr('min', GetInputDateFormat(pastDateBefore30Year));
    });

    // Allow Past Date Upto 25 Years.  (For - Fixed Deposit Date / Insurance)
    $('.allow-past-date-upto-25y').click(function () {
        $(this).attr('min', GetInputDateFormat(pastDateBefore25Year));
    });

    // Allow Past Date Upto 10 Years. (For - Any Document Issue Date)
    $('.allow-past-date-upto-10y').click(function () {
        $(this).attr('min', GetInputDateFormat(pastDateBefore10Year));
    });

    // Allow Past Date Upto 5 Years. (For - Any Document Issue Date)
    $('.allow-past-date-upto-5y').click(function () {
        $(this).attr('min', GetInputDateFormat(pastDateBefore5Year));
    });

    // Allow Past Date Upto 6 Months. (For - Any Document Issue Date)
    $('.allow-past-date-upto-6m').click(function () {
        $(this).attr('min', GetInputDateFormat(pastDateBefore6Month));
    });

    // Allow Past Date Upto Open Finacial Years.
    $('.allow-past-date-upto-open-fy').click(function () {
        $(this).attr('min', GetInputDateFormat(firstDateOfOpenFinancialYear));
    });

    // Allow Past Date Upto Current Financial Year.
    $('.allow-past-date-upto-current-fy').click(function () {
        $(this).attr('min', GetInputDateFormat(currentFinancialYearStartDate));
    });

    // Allow Future Date Upto 50 Years. (For - Insurance Premium Paying Date)
    $('.allow-future-date-upto-50y').click(function () {
        $(this).attr('max', GetInputDateFormat(futureDateAfter50Year));
    });

    // Allow Future Date Upto 30 Years. (For - Insurance Premium Paying Date)
    $('.allow-future-date-upto-30y').click(function () {
        $(this).attr('max', GetInputDateFormat(futureDateAfter30Year));
    });

    // Allow Future Date Upto 20 Years.  (For - Fixed Deposit Date / Insurance)
    $('.allow-future-date-upto-20y').click(function () {
        $(this).attr('max', GetInputDateFormat(futureDateAfter20Year));
    });

    // Allow Future Date Upto 10 Years. (For - Any Document Issue Date)
    $('.allow-future-date-upto-10y').click(function () {
        $(this).attr('max', GetInputDateFormat(futureDateAfter10Year));
    });

    // Deny Minor Birth Date
    $('.deny-minor-birth-date').click(function () {
        let senoirBirthDate = new Date(today);

        // Add 18 Years In Today Date To Get Non Minor (Senior BirthDate)
        senoirBirthDate.setDate(senoirBirthDate.getDate() - (365 * 18));

        $(this).attr('max', GetInputDateFormat(senoirBirthDate));
    });

    // Validate Date Range
    $('.valid-date-range').click(function () {
        let nextYearDate = new Date(today);
        let previousYearDate = new Date(today);

        nextYearDate.setDate(nextYearDate.getDate() + 365);
        previousYearDate.setDate(previousYearDate.getDate() - 730);

        $(this).attr('min', GetInputDateFormat(previousYearDate));
        $(this).attr('max', GetInputDateFormat(nextYearDate));
    });

    //Validate Birth Date Range
    $('.valid-birth-date-range').click(function () {
        let previousYearDate = new Date(today);

        previousYearDate.setDate(previousYearDate.getDate() - 43000);

        $(this).attr('min', GetInputDateFormat(previousYearDate));
        $(this).attr('max', GetInputDateFormat(today));
    });

    // Deny Next Year Future Date
    $('.deny-next-year-date').click(function () {
        let nextYearDate = new Date(today);
        nextYearDate.setDate(nextYearDate.getDate() + 365);

        $(this).attr('max', GetInputDateFormat(nextYearDate));
    });

    // Activation Date
    $('.activation-date').click(function () {
        let myId = $(this).attr('id');
        let expiryId = myId.replace('activation', '#expiry');

        $(expiryId).val('');
    });

    // Expiry Date
    $('.expiry-date').click(function () {
        let myId = $(this).attr('id');
        let activationDateId = myId.replace('expiry', '#activation');

        let activationDate = new Date($(activationDateId).val());

        let futureday = new Date(activationDate);

        futureday.setDate(futureday.getDate() + 1);

        $(this).attr('min', GetInputDateFormat(futureday));
    });

});
