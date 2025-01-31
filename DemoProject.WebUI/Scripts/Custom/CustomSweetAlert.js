//Delete//
function Deletefun(Delete, mainurl = window.location.href) {

    $("#lastrow").remove();

    debugger
    var data = $("#form").serialize() + '&' + $.param({ Command: Delete }, true);
    event.preventDefault();
    swal({
        title: "Are you sure?",
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3bbe5e",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                debugger;
                $.ajax({
                    type: 'POST',
                    url: mainurl,
                    data: data,
                    success: function (data) {
                        debugger;

                        if (data.result) {
                            swal({ title: "Deleted!", text: "", type: "success", confirmButtonText: "OK", closeOnConfirm: true },
                                function () {
                                    window.location.href = data.redirectTo;
                                    return true;
                                });
                        }
                        else {
                            swal({ title: "OOps!", type: "error", confirmButtonText: "OK", closeOnConfirm: true },
                                function () {
                                    debugger;
                                    $(".content").html(data);
                                    return true;
                                });
                        }

                    },
                    error: function (data) {
                        swal("NOT Deleted!", "", "error");
                    }
                });
            } else {
                swal("Cancelled", "", "error");
            }
        });
    return false;

}
//Verify//
function Verifyfun(Verify, mainurl = window.location.href) {
    debugger
    var data = $("#form").serialize() + '&' + $.param({ Command: Verify }, true);
    event.preventDefault();
    swal({
        title: "Are you sure?",
        text: "",
        type: "warning",
        showCloseButton: true,
        showCancelButton: true,
        confirmButtonColor: "#3bbe5e",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                debugger;
                $.ajax({
                    type: 'POST',
                    url: mainurl,
                    data: data,
                    dataType: "JSON",
                    success: function (data) {
                        debugger;
                        if (data.result) {
                            swal({ title: "Verified!", text: "", type: "success", confirmButtonText: "OK", closeOnConfirm: true },
                                function () {
                                    window.location.href = data.redirectTo;
                                    return true;
                                });
                        }
                        else {
                            swal({ title: "OOps!", text: "Something went wrong!", type: "error", confirmButtonText: "OK", closeOnConfirm: true },
                                function () {
                                    window.location.href = data.redirectTo;
                                    return false;

                                });
                        }

                    },
                    error: function (data) {
                        debugger;
                        swal("NOT Verifyed!", "", "error");
                    }
                });
            } else {
                swal("Cancelled", "", "error");
            }
        });

    return false;
}
//Reject//
function Rejectfun(Reject, mainurl = window.location.href) {
    debugger
    var data = $("#form").serialize() + '&' + $.param({ Command: Reject }, true);
    event.preventDefault();
    swal({
        title: "Are you sure?",
        text: "",
        type: "warning",
        showCloseButton: true,
        showCancelButton: true,
        confirmButtonColor: "#3bbe5e",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                debugger;
                $.ajax({
                    type: 'POST',
                    url: mainurl,
                    data: data,
                    dataType: "JSON",
                    success: function (data) {

                        debugger;
                        if (data.result) {
                            swal({ title: "Rejected!", text: "", type: "success", confirmButtonText: "OK", closeOnConfirm: true },
                                function () {
                                    window.location.href = data.redirectTo;
                                    return true;
                                });
                        }
                        else {
                            swal({ title: "OOps!", text: data.messge, type: "error", confirmButtonText: "OK", closeOnConfirm: true },
                                function () {
                                    window.location.href = data.redirectTo;
                                    return false;
                                });
                        }

                    },
                    error: function (data) {
                        debugger;
                        swal("NOT Rejected!", "", "error");
                    }
                });
            } else {
                swal("Cancelled", "", "error");
            }
        });
    return false;

}





