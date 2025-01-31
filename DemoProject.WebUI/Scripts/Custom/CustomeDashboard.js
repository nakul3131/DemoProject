//session expire
var counter = 60;
var idleInterval = 0;
var countdown = null;
var leSecondsTimer = null;

//Increment the idle time counter every minute.
leSecondsTimer = setInterval(timerIncrement, 60000000);

$(window).mousemove(function () {
    idleInterval = 0;
    counter = 60;
    clearInterval(countdown);
    $('#myModal-time').modal('hide');
})
$(window).click(function () {
    idleInterval = 0;
    counter = 60;
    clearInterval(countdown);
    $('#myModal-time').modal('hide');
})
$(window).keyup(function () {
    counter = 60;
    clearInterval(countdown);
    idleInterval = 0;
    $('#myModal-time').modal('hide');
})
$('#keep').click(function () {
    idleInterval = 0;
    $('#myModal-time').modal('hide');
});
//Increment the idle time counter every minute.
function timerIncrement() {
    idleInterval++;
    if (idleInterval >= Timeout) {
        $('#myModal-time').modal('show');
        countDownClock();
    }
    if (idleInterval >= Timeout + 1) {
        clearInterval(leSecondsTimer);
        window.location.href = loginUrl;
    }
};

function countDownClock() {
    var ele = $('#time');
    if (counter > 0) {
        counter--;
        if (counter < 10) {
            ele.html("0" + counter);
        }
        else {
            ele.html(counter);
        }
        countdown = setTimeout(function () {
            countDownClock();
        }, 1000);
    }
    else {
        clearInterval(countdown);
        //ele.text("Time Up!")

    }
}
$(".mydiv").css({ "width": '24.3%', "height": "100%" });
$(".sidebar").css({ 'width': '24.3%' });

$("#row1").css({ "box-shadow": "0 4.5px 6px -6px black" });
$(document).ready(function () {
    $vWidth = $(window).width();
    // $('#test').html($vWidth);
    //if($(window).width() >= 1024) {
    //    //$('a.expand').click();
    //    $(".rightnav").css({ 'width': '75%' });
    //    $(".mydiv").css({ "width": "23%", "height": "100%" });
    //    $(".sidebar").css({ 'width': '23%' });
    //}
    //alert($vWidth)
    if (screen.width == window.innerWidth) {
        //alert("you are on normal page with 100% zoom");
        //$(".mydiv").css({ "width": "320px", "height": "100%" });
        $(".mydiv").css({ "width": "24.3%" });
        $(".rightnav").css({ 'width': '75%' });
        $(".sidebar").css({ 'width': '24.3%' });
    } else if (screen.width > window.innerWidth) {
        //$(".mydiv").css({ "width": "350px", "height": "100%" });
        $(".rightnav").css({ 'width': '75%' });
        $(".mydiv").css({ "width": "24.3%" });
        $(".sidebar").css({ 'width': '24.3%' });

    } else {
        //alert("you have zoomed out i.e less than 100%");
        //$(".mydiv").css({ "width": "23.3%", "height": "100%" });
        $(".mydiv").css({ "width": "24.3%" });
        $(".rightnav").css({ 'width': '75%' });
        $(".sidebar").css({ 'width': '24.3%' });
    }
})

// Tool Tip Text
$(document).tooltip({ selector: '[data-toggle="tooltip"]' });

$(document).ready(function () {
    //checkWidth();
    var events = [];
    var selectedEvent = null;
    FetchEventAndRenderCalendar();
    function FetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "GET",
            url: "/EventMaster/GetEventMasters",
            success: function (data) {
                $.each(data, function (i, v) {
                    events.push({
                        prmKey: v.PrmKey,
                        eventMasterId: v.EventMasterId,
                        eventTypePrmKey: v.EventTypePrmKey,
                        title: v.NameOfEvent,
                        aliasName: v.AliasName,
                        nameOnReport: v.NameOnReport,
                        eventDescription: v.EventDescription,
                        start: moment(v.ScheduledFrom),
                        scheduledTo: moment(v.ScheduledTo),
                        triggeredAt: moment(v.TriggeredAt),
                        isFullDayEvent: v.IsFullDayEvent,
                        isActive: v.IsActive,
                        redirectUrl: v.RedirectUrl,
                        note: v.Note,
                        enableReminder: v.EnableReminder,
                        eventMasterTranslationPrmKey: v.EventMasterTranslationPrmKey,
                        eventMasterTranslationId: v.EventMasterTranslationId,
                        languagePrmKey: v.LanguagePrmKey,
                        transModificationNumber: v.TransModificationNumber,
                        transNameOfEvent: v.TransNameOfEvent,
                        transAliasName: v.TransAliasName,
                        transNameOnReport: v.TransNameOnReport,
                        transNote: v.TransNote,
                        transReasonForModification: v.TransReasonForModification,
                    });
                })
                GenerateCalender(events);
            },
            error: function (error) {
                alert('failed');
            }
        })
    }

    function GenerateCalender(events) {
        $('#calender').fullCalendar('destroy');
        $('#calender').fullCalendar({
            contentHeight: 400,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev today',
                center: 'title',
                right: 'next month,agendaWeek,agendaDay'
            },
            displayEventTime: false,
            eventLimit: true,
            eventColor: '#378006',
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                selectedEvent = calEvent;
                $('#popUpModal #eventTitle').text(calEvent.title);
                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>Start:</b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.scheduledTo != null) {
                    $description.append($('<p/>').html('<b>End:</b>' + calEvent.scheduledTo.format("DD-MMM-YYYY HH:mm a")));
                }
                $description.append($('<p/>').html('<b>Description:</b>' + calEvent.eventDescription));
                $('#popUpModal #pDetails').empty().html($description);

                $('#popUpModal').modal();
            },
            selectable: true,
            select: function (start, end) {
                selectedEvent = {
                    prmKey: 0,
                    eventMasterPrmKey: '',
                    eventMasterId: '',
                    eventTypePrmKey: '',
                    nameOfEvent: '',
                    aliasName: 'None',
                    nameOnReport: 'None',
                    eventDescription: '',
                    start: start,
                    scheduledTo: end,
                    triggeredAt: end,
                    isFullDayEvent: '',
                    isActive: '',
                    redirectUrl: '',
                    note: 'None',
                    enableReminder: '',
                    eventMasterTranslationPrmKey: '',
                    eventMasterTranslationId: '',
                    languagePrmKey: '',
                    transModificationNumber: '',
                    transNameOfEvent: '',
                    transAliasName: 'नाही',
                    transNameOnReport: 'नाही',
                    transNote: 'नाही',
                    transReasonForModification: 'नाही',
                };
                openAddEditForm();
                $('#calendar').fullCalendar('unselect');
            },
            editable: true,
            eventDrop: function (event) {
                var data = {
                    PrmKey: event.eventID,
                    EventTypePrmKey: event.eventTypePrmKey,
                    Nameofevent: event.nameofevent,
                    Transnameofevent: event.transNameOfEvent,
                    Aliasname: event.aliasName,
                    Transaliasname: event.transAliasName,
                    Nameonreport: event.nameOnReport,
                    Transnameonreport: event.transNameOnReport,
                    Eventdescription: event.eventDescription,
                    Scheduledfrom: event.start.format('DD-MM-YYYY'),
                    Scheduledto: event.scheduledTo.format('DD-MM-YYYY'),
                    Triggeredat: event.triggeredAt.format('DD-MM-YYYY'),
                    Isfulldayevent: event.isFullDayEvent,
                    Isactive: event.isActive,
                    Redirecturl: event.redirectUrl,
                    Enablereminder: event.enableReminder,
                    Note: event.note,
                    Transnote: event.transNote,
                };
                SaveEvent(data);
            }
        })
    }

    $('#btnEdit').click(function () {
        //Open modal dialog for edit event
        openAddEditForm();
    })

    $('#btnDelete').click(function () {
        if (selectedEvent != null && confirm('Are you sure?')) {
            $.ajax({
                type: "POST",
                url: '/EventMaster/Delete',
                data: { 'prmKey': selectedEvent.prmKey },
                success: function (data) {
                    //Refresh the calender
                    FetchEventAndRenderCalendar();
                    $('#myModal').modal('hide');
                },
                error: function () {
                    alert('Failed');
                }
            })
        }
    })

    //eventTypes = [];
    $.ajax({
        type: "GET",
        url: "/EventMaster/GetEventTypeDropDown",
        success: function (data) {
            var s = '<option value="-1">Please Select a Name Of EventType</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].PrmKey + '">' + data[i].NameOfEventType + '</option>';
            }
            $("#eventTypeId").html(s);
        },
        error: function (error) {
            alert('failed');
        }
    })

    function openAddEditForm() {
        if (selectedEvent != null) {
            $('#hdEventID').val(selectedEvent.prmKey);
            $('#eventTypeId').val(selectedEvent.eventTypePrmKey);
            $('#name-of-event').val(selectedEvent.title);
            $('#trans-name-of-event').val(selectedEvent.transNameOfEvent);
            $('#alias-name').val(selectedEvent.aliasName);
            $('#trans-alias-name').val(selectedEvent.transAliasName);
            $('#name-on-report').val(selectedEvent.nameOnReport);
            $('#trans-name-on-report').val(selectedEvent.transNameOnReport);
            $('#event-description').val(selectedEvent.eventDescription);
            $('#scheduled-from').val(selectedEvent.start.format('YYYY-MM-DD'));
            $('#scheduled-to').val(selectedEvent.scheduledTo.format('YYYY-MM-DD'));
            $('#triggered-at').val(selectedEvent.triggeredAt.format('YYYY-MM-DD'));
            $('#is-full-day-event').prop("checked", selectedEvent.isFullDayEvent || false);
            $('#is-full-day-event').change();
            $('#is-active').prop("checked", selectedEvent.isActive || false);
            $('#is-active').change();
            $('#redirect-url').val(selectedEvent.redirectUrl);
            $('#enable-reminder').prop("checked", selectedEvent.enableReminder || false);
            $('#enable-reminder').change();
            $('#note').val(selectedEvent.note);
            $('#trans-note').val(selectedEvent.transNote);
        }
        $('#myModal').modal('hide');
        $('#myModalSave').modal();
    }

    $('#btnSave').click(function () {
        //Validation/
        if ($('#name-of-event').val().trim() == "") {
            alert('Name of event required');
            return;
        }
        if ($('#event-description').val().trim() == "") {
            alert('Event description required');
            return;
        }

        var data = {
            PrmKey: $('#hdEventID').val(),
            EventTypePrmKey: $('#eventTypeId').val(),
            Nameofevent: $('#name-of-event').val().trim(),
            Transnameofevent: $('#trans-name-of-event').val().trim(),
            Aliasname: $('#alias-name').val().trim(),
            Transaliasname: $('#trans-alias-name').val().trim(),
            Nameonreport: $('#name-on-report').val().trim(),
            Transnameonreport: $('#trans-name-on-report').val().trim(),
            Eventdescription: $('#event-description').val().trim(),
            Scheduledfrom: $('#scheduled-from').val().trim(),
            Scheduledto: $('#scheduled-to').val().trim(),
            Triggeredat: $('#triggered-at').val().trim(),
            Isfulldayevent: $('#is-full-day-event').is(':checked'),
            Isactive: $('#is-active').is(':checked'),
            Redirecturl: $('#redirect-url').val().trim(),
            Enablereminder: $('#enable-reminder').is(':checked'),
            Note: $('#note').val().trim(),
            Transnote: $('#trans-note').val().trim()
        }
        Create(data);
        // call function for submit data to the server 
    })

    function Create(data) {
        $.ajax({
            type: "POST",
            url: '/EventMaster/Create',
            data: {
                '_eventMasterViewModel': data
            },
            success: function (data) {
                //Refresh the calender
                FetchEventAndRenderCalendar();
                $('#myModalSave').modal('hide');
            },
            error: function () {
                alert('Failed');
            }
        })
    }
})

// To Dashboard Chart
$(document).ready(function () {

    var ctx = $("#chart-line1");
    var myLineChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: ["Spring", "Summer", "Fall", "Winter"],
            datasets: [{
                data: [1200, 1700, 800, 200],
                backgroundColor: ["rgba(255, 0, 0, 0.5)", "rgba(100, 255, 0, 0.5)", "rgba(200, 50, 255, 0.5)", "rgba(0, 100, 255, 0.5)"]
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Weather'
            }
        }
    });

    var ctxP = document.getElementById("pieChart").getContext('2d');
    var myPieChart = new Chart(ctxP, {
        type: 'pie',
        data: {
            labels: ["Home", "Vheical", "Gold"],
            datasets: [{
                data: [300, 50, 100],
                backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C"],
                hoverBackgroundColor: ["#FF5A5E", "#5AD3D1", "#FFC870"]
            }]
        },
        options: {
            responsive: true
        }
    });
    var ctx = $("#chart-lines");
    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
            datasets: [{
                data: [86, 114, 106, 106, 107, 111, 133, 221, 783, 2478],
                label: "Home",
                borderColor: "#458af7",
                backgroundColor: '#458af7',
                fill: false
            }, {
                data: [282, 350, 411, 502, 635, 809, 947, 1402, 3700, 5267],
                label: "Vheicla",
                borderColor: "#8e5ea2",
                fill: true,
                backgroundColor: '#8e5ea2'
            }, {
                data: [168, 170, 178, 190, 203, 276, 408, 547, 675, 734],
                label: "Gold",
                borderColor: "#3cba9f",
                fill: false,
                backgroundColor: '#3cba9f'
            }]
        },
        options: {
            title: {
                display: true,
                text: 'loan per region (in millions)'
            }
        }
    });
    var ctx = $("#chart-line");
    var myLineChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
            datasets: [{
                data: [86, 114, 106, 106, 107, 111, 133, 221, 783, 2478],
                label: "Home",
                borderColor: "#458af7",
                backgroundColor: '#458af7',
                fill: false
            }, {
                data: [282, 350, 411, 502, 635, 809, 947, 1402, 3700, 5267],
                label: "Vheicla",
                borderColor: "#8e5ea2",
                fill: true,
                backgroundColor: '#8e5ea2'
            }, {
                data: [168, 170, 178, 190, 203, 276, 408, 547, 675, 734],
                label: "Gold",
                borderColor: "#3cba9f",
                fill: false,
                backgroundColor: '#3cba9f'
            }]
        },
        options:
        {
            title:
            {
                display: true,
                text: 'loan per region (in millions)'
            }
        }
    });
});

$(document).ready(function () {
    // JQuery For Auto Hide Side Menu Bar
    $(document).mousemove(function (event) {

        if (event.pageX < 10) {
            if ($(".sidebar").hasClass('toggled')) {
                $(".sidebar").removeClass('showToggledSlideOut');
                $(".content").removeClass('showToggledContent');
                $(".menupopover").popover('hide');
                $('.sidebar.toggled').find("a.active").removeClass("active");


            }
            else {
                $(".sidebar").removeClass('showSlideOut');
                $(".content").removeClass('showContent');
                $(".menupopover").popover('hide');
                $('.sidebar.toggled').find("a.active").removeClass("active");


            }
        }
        else if (event.pageX > 350) {
            if ($(".sidebar").hasClass('toggled')) {
                $(".sidebar.toggled").addClass('showToggledSlideOut');
                $(".content.toggled").addClass('showToggledContent');
                $(".menupopover").popover('hide');
                $("sidebar.toggled").find("a.active").removeClass("active");
            }
            else {

                $("sidebar").find("a.active").removeClass("active");
                $(".sidebar").addClass('showSlideOut');
                $(".content").addClass('showContent');
                $(".menupopover").hide();
                $('.sidebar.toggled').find("a.active").removeClass("active");
                $('[data-toggle="tooltip"], .tooltip').tooltip("hide");

            }
        }
    });

    this.$slideOut = $('#slideOut');

    $("#sidebarToggleTop").on('click', function (e) {
        $('.sidebar').find("a.active").removeClass("active");
        // $(".popover").remove();
        $('[data-popover]').popover('hide');
        $('[rel="popover"]').popover('hide');

        if ($(".sidebar").hasClass("showSlideOut")) {
            $("#slideOut").toggleClass('showSlideOut');
            $(".content").toggleClass('showContent');
            $(".menupopover").popover('hide');
        }

        if ($(".sidebar").hasClass("showToggledSlideOut")) {
            $("#slideOut").toggleClass('showToggledSlideOut');
            $(".content").toggleClass('showToggledContent');
            $(".menupopover").popover('hide');
        }
        else {
            $("#slideOut").toggleClass("toggled");
            $(".content").toggleClass("toggled");
            $(" .submenu").hide();
            $(" .subsubmenu").hide();
            $(".menupopover").popover('hide');
        }

        if ($(".sidebar").hasClass("toggled")) {
            $(".submenu").hide();
            $(".navbar-brand span").hide();
            $(".navbar-brand img").css({ "width": "200%", "text-align": "center;", "margin-left": "-30%" });
            $(".mydiv").css({ "width": "6%" });
            $(".sidebar").css({ 'width': '6%' });
            $(".rightnav").attr('style', 'width: 94%');
        }
        else {
            $(".navbar-brand span").show();
            $(".navbar-brand img").css({ "width": "30%", "margin-left": "-5%" });
            $(".mydiv").css({ "width": "24.3%" });
            $(".rightnav").attr('style', 'width: 75%');
            $(".sidebar").css({ 'width': '24.3%' });
            $(".menupopover").popover('hide');

        }
    });
})


// JQuery for Sliding Menu Clicks By Default Hide All Child Contents
$(" .submenu").hide();
$(" .subsubmenu").hide();
$('body').on('mouseenter', '.menupopover ', function () {
    $('[data-toggle="tooltip"], .tooltip').tooltip("hide");

    $("#slideOut").show();
})
$('body').on('click', '.menupopover>.popover-body a', function (e) {
    $('[data-popover]').popover('hide');
    var classid = $(this).attr('class');
    classid = classid.replace(" mt-2", "");

    $('.submenu li.sidebar-dropdown > a').removeClass('active');
    $(this).parent().parent().find('a.mt-2').removeClass('active');
    var rowNum = 0;
    let element = $(this).css({ "color": "pink" });
    element.addClass('active')
    var subsubmenu = [];
    var classname = classid.split(' ');
    var active = classname[1];

    if (active === "active") {
        //$(this).find('.icon').toggleClass("fas fa-chevron-down fas fa-chevron-right")
        element.removeClass('active');
        $('[data-popover]').popover('hide');

        if ($(this).parent().closest('div').hasClass("menu")) {
            $(".submenu").hide();
            $(".subsubmenu").hide();

        }
        else if ($(this).parent().closest('div').hasClass("submenu")) {
            // On Click SubMenu - Display Child Content Of SubMenu With
            $(" .subsubmenu").hide();

        }
        else if ($(this).parent().closest('div').hasClass("subsubmenu")) {
            // On Click SubSubMenu - Hide All Child Content i.e. SubMenu and Sub Sub Menu
            $(".submenu").hide();
            $(".subsubmenu").hide();
        }

    }
    else {
        if ($(".sidebar").hasClass("toggled")) {
             
            $(" .subsubmenu").hide();
            if ($(this).parent().closest('div').hasClass("submenu")) {

                $(".subsubmenu." + classid).each(function () {
                     
                    subsubmenu.push($(this).closest("li").find(".subsubmenu").html());

                })
                //Duplicate record remove in array
                var menulist = [];
                $.each(subsubmenu, function (i, e) {
                     
                    if ($.inArray(e, menulist) == -1) {

                        menulist.push(e);
                    }
                    else {
                        return false;
                    }
                });

                var newHTML = '';
                for (var i = 0; i < menulist.length; i++) {
                    newHTML = newHTML + '<li class="sidebar-dropdown nav-item mt-2"><div class="sidebar-submenu subsubmenu ' + classid + '">' + menulist[i] + '</div></li>';
                }
                $(".menupopover>.popover-body .sidebar-dropdown .nav-item a.active").after(newHTML);

            }
        }

    }


});
$("#slideOut ul > li > a").on("click", function (event) {
     

    $('[data-toggle="tooltip"], .tooltip').tooltip("hide");

    $('[data-popover]').popover('hide');
     
    var classid = $(this).attr('class');
    classid = classid.replace(" mt-2", "");

    $('.submenu li.sidebar-dropdown > a').removeClass('active');
    $(this).parent().parent().find('a.mt-2').removeClass('active');

    let element = $(this);
    element.addClass('active');

    var classname = classid.split(' ');
    var active = classname[1];



    if ($(".sidebar").hasClass("toggled")) {
         
        var submenulist = [];
        {
            if (active === "active") {
                //$(this).find('.icon').toggleClass("fas fa-chevron-down fas fa-chevron-right")
                element.removeClass('active');
                $('[data-popover]').popover('hide');
                $('[data-toggle="tooltip"], .tooltip').tooltip("hide");
            }

            else {

                if ($(this).parent().closest('div').hasClass("menu")) {
                     
                    // On Click Main Menu - Display Child Content Of SubMenu and hide all other Opened Menu
                    $(".submenu").hide();
                    $(".submenu." + classid).each(function (i) {
                        var submenu = $(this).closest("div").html();
                        submenulist.push(submenu);

                    })
                    var name = $(this).find("span").html();

                    var tt = $(this).find('i').attr("class");

                    $(this).attr('data-toggle', 'popover');

                    $(this).attr('data-original-title', '<i class="' + tt + '" style="color: white !important;font-size:17.5px!important;"></i><span class="ml-3" style="color: white !important; font-weight:bold!important; font-size:18.5px!important;word-spacing:530px!important;">' + name + '</span>');

                    var newHTML = '';
                    for (var i = 0; i < submenulist.length; i++) {
                        newHTML = newHTML + '<ul class="navbar-nav"><li class="sidebar-dropdown nav-item"><div class="sidebar-submenu submenu ' + classid + '">' + submenulist[i] + '</div></li></br></ul>';
                    }

                    $(".popover").popover('hide');

                    $(this).popover({
                        container: 'body',
                        html: true,
                        trigger: "click",
                        placement: "right",
                        content: newHTML,
                        template: '<div class="popover menupopover" role="tooltip"><div class="arrow" style="top:50%!important"></div><div class="popover-header"></div><div class="popover-body"></div></div>'


                    })


                    $(this).popover('show');

                }
                else if ($(this).parent().closest('div').hasClass("subsubmenu")) {
                    // On Click SubSubMenu - Hide All Child Content i.e. SubMenu and Sub Sub Menu
                    $(".submenu").hide();
                    $(".subsubmenu").hide();
                    $(".subsubmenu." + classid).toggle(300);
                }
            }

        }
    }
    else {


        $('[data-popover]').popover('hide');

        var classname = classid.split(' ');
        var active = classname[1];

        if (active === "active") {
            //$(this).find('.icon').toggleClass("fas fa-chevron-down fas fa-chevron-right")

            element.removeClass('active');


            if ($(this).parent().closest('div').hasClass("menu")) {
                $(".submenu").hide();
                $(".subsubmenu").hide();
                $(".submenu." + classid).toggle(300);

            }
            else if ($(this).parent().closest('div').hasClass("submenu")) {
                // On Click SubMenu - Display Child Content Of SubMenu With
                $(" .subsubmenu").hide();
                $(".subsubmenu." + classid).toggle(300);


            }
            else if ($(this).parent().closest('div').hasClass("subsubmenu")) {
                // On Click SubSubMenu - Hide All Child Content i.e. SubMenu and Sub Sub Menu
                $(".submenu").hide();
                $(".subsubmenu").hide();
            }
            $(".menupopover").attr("aria-describedby").remove();
        }

            //Replace mt-2 class to get menu prmkey classname
        else {

            if ($(this).parent().closest('div').hasClass("menu")) {
                // On Click Main Menu - Display Child Content Of SubMenu and hide all other Opened Menu
                $(".submenu").hide();
                $(".subsubmenu").hide();
                $(".submenu." + classid).toggle(300);

                $(".menupopover").attr("aria-describedby").remove();

            }

            else if ($(this).parent().closest('div').hasClass("submenu")) {
                // On Click SubMenu - Display Child Content Of SubMenu With
                $(" .subsubmenu").hide();
                $(".subsubmenu." + classid).toggle(300);
            }
            else if ($(this).parent().closest('div').hasClass("subsubmenu")) {
                // On Click SubSubMenu - Hide All Child Content i.e. SubMenu and Sub Sub Menu
                $(".submenu").hide();
                $(".subsubmenu").hide();
            }
        }
    }

})
if ($(".sidebar ul > li > a").hover(function (event) {
if ($(".sidebar.toggled").hasClass('toggled')) {
        var name = $(this).find("span").html();

        $(this).attr('data-toggle', 'tooltip');
        $(this).attr('data-placement', 'left');
        $(this).attr('data-trigger', "focus");
        $(this).attr('data-original-title', name);
}
else {
    $(this).removeAttr('title');
    $(this).removeAttr('data-toggle');
    $(this).removeAttr('data-placement');
    $(this).removeAttr('data-original-title', name);
}

}));
