'use strict'

$(document).ready(function ()
{
    //- Check Wheter Application Number Is Visible Or Not For Selected Scheme.
    $('#scheme').focusout(function()
    {
        debugger;
        // Set Page Input As Per Scheme Selection
        $.get('/AccountChildAction/IsVisibleSharesApplicationNumber', { _schemeId: $('#scheme').val(), async: false }, function (data, textStatus, jqXHR)
        {
            if (data)
                $('#app-number-grp').removeClass('d-none');
            else
            {
                $('#app-number-grp').addClass('d-none');
                $('#application-number').val(0);
            }
        });
    });            

    //- Unique / Valid Application Number Validation
    $('#application-number').focusout(function ()
    {    
        $.get('/AccountChildAction/IsValidApplicationNumber', { _schemeId: $("#scheme").val(), _applicationNumber: $('#application-number').val(), async: false }, function (data, textStatus, jqXHR)
        {
            if (data)
                $('#application-number-error').addClass('d-none')
            else
                $('#application-number-error').removeClass('d-none')
        });
    });

    // - Nominee Details Visibility Validation By MemberType
    $('#member-type').focusout(function()
    {       
        if ($('#member-type').val() > 0)
            $('#nominee-detail').addClass('d-none');      
        else
            $('#nominee-detail').removeClass('d-none');            
    });            
});

