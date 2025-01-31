var lang;
var destinationInput;
var sourceInputId;
var engTextArray;


function Maketranslation(e)
{
    debugger;
    // Get Source Input Id
    sourceInputId = event.target.id;

    if (!(event.keyCode === 32 && event.target.selectionStart === 0))
    {
        // Get Source Input By Its Id
        var sourceInput = document.getElementById(sourceInputId);

        // Get English Text Of Source Input
        var engText = $(document.querySelector("#" + sourceInputId)).val() + " ";

        // Get Selected String Of Source Input Useful For Back Space Or Empty Input
        var selectedString = sourceInput.value.substr(sourceInput.selectionStart, sourceInput.selectionEnd - sourceInput.selectionStart);

        // Regional Language Input Id Start With #trans-
        destinationInput = "#trans-";

        // BackSpace And Delete KeyPress Event Handling 
        if (event.keyCode === 8 || event.keyCode === 46)
        {
            // Make Regional Languge Input Empty On Source Input Empty
            if (engText.trim().length < 2 || engText.trim().length == selectedString.trim().length) {
                ///$(document.querySelector(destinationInput + sourceInputId)).val($(document.querySelector("#" + sourceInputId)).val().trim() + " ");
                $(document.querySelector(destinationInput + sourceInputId)).val("");
            }
        }

        if (event.keyCode == 32) {
            destinationInput = "#trans-";
            engTextArray = engText.split(" ");

            $(document.querySelector(destinationInput + sourceInputId)).val($(document.querySelector(destinationInput + sourceInputId)).val() + engTextArray[engTextArray.length - 2]);

            for (var i = 0; i < engTextArray.length; i++) {
                transtr = engTextArray[i];
            }

            document.querySelector(destinationInput + sourceInputId).focus()
        }

        // On BackSpace Key Press Make Destination/Translator Input Empty
        $(document.querySelector(destinationInput + sourceInputId)).bind("keyup", function (event) {
            // The setTimeout() method calls a function or evaluates an expression after a specified number of milliseconds. (1000 ms = 1 second.)
            setTimeout(function () {
                if (event.keyCode === 32) {
                    // Add Space In Source Input Text (English)
                    $(document.querySelector("#" + sourceInputId)).val($(document.querySelector("#" + sourceInputId)).val().trim() + " ");


                    document.querySelector("#" + sourceInputId).focus()
                }
            }, 0);
        });
    }
    else
        event.preventDefault();
}

function OnLoad()
{
    var Options =
    {
        sourceLanguage:
            google.elements.transliteration.LanguageCode.ENGLISH,

        destinationLanguage:
            [google.elements.transliteration.LanguageCode[lang]],

        shortcutKey: 'ctrl+g',

        transliterationEnabled: true
    };

    var control = new google.elements.transliteration.TransliterationControl(Options);

    //JQuery For Enabling Translation Configuration Of All Valid Inputs
    for (let elem of document.querySelectorAll("[id^=trans-]"))
    {
        control.makeTransliteratable([elem.id]);
    }
}

google.setOnLoadCallback(OnLoad());

