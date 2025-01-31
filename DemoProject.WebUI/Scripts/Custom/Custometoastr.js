if (status.toString().toLowerCase() != null)
{
    debugger;
    if (status === 'Amend')
    {
        toastr.success("Amend Operation Submitted Successfully").css({ "color": "white", "background-color":"Green", "font-size": "15", "weight": "500", "letter-spacing": "1px" });
    }
    else if (status === 'Exit')
    {
        toastr.warning("Page Has Been Exited Successfully").css({ "color": "white", "background-color": "Blue", "font-size": "15", "weight": "500", "letter-spacing": "1px" });
    }
    else if (status === 'Delete')
    {
        toastr.success("Delete Operation Submitted Successfully").css({ "color": "white", "background-color": "Green","font-size": "15", "weight": "500", "letter-spacing": "1px" });
    }
    else if (status === 'Modify')
    {
        toastr.success("Modify Operation Submitted Successfully").css({ "color": "white", "background-color": "Green", "font-size": "15", "weight": "500", "letter-spacing": "1px" });
    }
    else if (status === 'Reject')
    {
        toastr.success("Reject Operation Submitted Successfully").css({ "color": "white", "background-color": "Red","font-size": "15", "weight": "500", "letter-spacing": "1px" });
    }
    else if (status === 'Save')
    {
        toastr.success("Save Operation Submitted Successfully").css({ "color": "white", "background-color": "Green","font-size": "15", "weight": "500", "letter-spacing": "1px" });
    }
    else if (status === 'Verify')
    {
        toastr.success("Verify Operation Submitted Successfully").css({ "color": "white", "background-color": "Green","font-size": "15", "weight": "500", "letter-spacing": "1px" });
    }
    else if (status ==='ClearRecentSession')
    {
        toastr.success("Your Recent Login Session Has Been Cleared Successfully").css({ "color": "white", "background-color": "Green","font-size": "15", "weight": "500", "letter-spacing": "1px" });
    }
    else if (status === 'UserLocked') {
        toastr.success("Your User Account Has Been Cleared Successfully").css({ "color": "white", "background-color": "Green","font-size": "15", "weight": "500", "letter-spacing": "1px" });
    }
    else if (status === 'ResetPassword') {
        toastr.success("Your User Account Password Has Been Reset Successfully").css({ "color": "white", "background-color": "Green", "font-size": "15", "weight": "500", "letter-spacing": "1px" });
    }
}


    
