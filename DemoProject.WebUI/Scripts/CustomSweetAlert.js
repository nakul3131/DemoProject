
function Allfun(e) {
       
        var defaultAction = url;
        debugger;
        swal({
            title: "Are you sure?",
            text: "",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#19bb6c",
            cancelButtonClass: 'btn-secondary waves-effect',
            confirmButtonClass: 'btn-success waves-effect waves-light',
            cancelButtonText: "No",
            confirmButtonText: "Yes",
            closeOnConfirm: false,

        },

            function (isConfirm) {
                if (isConfirm) {
                    swal({ title: "Deleted!", text: "", type: "success", confirmButtonText: "OK!", closeOnConfirm: true },
                        function () {
                            // RESUME THE DEFAULT LINK ACTION
                            window.location.href = defaultAction;
                            return true;
                        });
                } else {
                    swal("Cancelled", "", "error");
                    e.preventDefault();
                    return false;
                }
            });
    }

   

