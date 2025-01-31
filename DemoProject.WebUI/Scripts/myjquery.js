//"use strict"; Defines that JavaScript code should be executed in "strict mode".
'use strict';
//The ready event occurs when the DOM(document object model) has been loaded.
//Parameter
//  function -	Required. Specifies the function to run after the document is loaded

$(document).ready(function ()
{
    // Default value
    $('.default-nn').val('NN');
    $('.default-nnn').val('NNN');
    $('.default-thousand').val('1000');
    $('.default-one').val('1');
    $('.digit').mask('0');
    $('.two-digits').mask('00');
    $('.three-digits').mask('000');
    $('.four-digits').mask('0000');
    $('.five-digits').mask('00000');
    $('.six-digits').mask('000000');
    $('.seven-digits').mask('0000000');
    $('.eight-digits').mask('00000000');
    $('.nine-digits').mask('000000000');
    $('.number').mask('000000000000000');
    $('.date').mask('00/00/0000');
    $('.date2').mask('00-00-0000');
    $('.hour').mask('00:00:00');
    $('.dateHour').mask('00/00/0000 00:00:00');
    $('.mobno').mask('0000000000');
    $('.phone').mask('0000-0000');
    $('.telphone-with-code').mask('(00) 0000-0000');
    $('.us-telephone').mask('(000) 000-0000');
    $('.ip').mask('000.000.000.000');
    $('.ipv4').mask('000.000.000.0000');
    $('.ipv6').mask('0000:0000:0000:0:000:0000:0000:0000');
});

// To make Text Capitalize
function ToTextCapitalize(e)
{
    var sourceInputId = event.target.id;
    var str = $(document.querySelector("#" + sourceInputId)).val();
    const titleCase = str.replace(/\b(\w)/g, s => s.toUpperCase());
    $(document.querySelector("#" + sourceInputId)).val(titleCase);
}


// To make full screen
function toggleFullScreen()
{
    var b = $(window).height() - 10;
    if (!document.fullscreenElement && !document.mozFullScreenElement && !document.webkitFullscreenElement)
    {
        if (document.documentElement.requestFullscreen)
        {
            document.documentElement.requestFullscreen()
        }
        else
        {
            if (document.documentElement.mozRequestFullScreen)
            {
                document.documentElement.mozRequestFullScreen()
            }
            else
            {
                if (document.documentElement.webkitRequestFullscreen)
                {
                    document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT)                    
                }
            }
        }
    }
    else
    {
        if (document.cancelFullScreen)
        {
            document.cancelFullScreen()
        }
        else
        {
            if (document.mozCancelFullScreen)
            {
                document.mozCancelFullScreen()
            }
            else
            {
                if (document.webkitCancelFullScreen)
                {
                    document.webkitCancelFullScreen()
                }
            }
        }
    }
    //The toggleClass() method toggles between adding and removing one or more class names from the selected elements.
    //Selects full-screen class where the parent is a <i>.
    $(".full-screen > i").toggleClass("icon-maximize");
    $(".full-screen > i").toggleClass("icon-minimize")
}
