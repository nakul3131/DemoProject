// Document Ready Function
// Whenever you use jQuery to manipulate your web page, you wait until the 
// document ready event has fired. 
// The document ready event signals that the DOM of the page is now ready, 
// so you can manipulate it without worrying that parts of the DOM has not yet been created. 
// The document ready event fires before all images etc. are loaded, 
// but after the whole DOM itself is ready.

// Multiple Document Ready Listeners
// jQuery allows you to register multiple document ready listeners. 
// Just call $(document).ready() multiple times.

// The two listener functions registered in this example will both get called 
// when the DOM is ready. They will get called in the order they were registered.

// Registering multiple document ready event listeners can be really useful 
// if you include HTML pages inside other HTML pages 
// (e.g. using server side include features of your backend / web server). 
// You may need some page initialization to occur both in the outer and inner page. 
// Thus both the outer and inner page can register a document ready listener, and perform the page initialization they both need.

// Nominee Details Show / Hide Based On Selection
//SchemeSharesCertificateParameterViewModel.EnableAutoCertificateNumber
//SchemeSharesCertificateParameterViewModel.EnableDigitalCodeForCertificate

'use strict';
$(document).ready(function ()
{
    debugger;
    const MANDATORY = 'M';

    // @@@@@@@@@@ Data Table Related letible Declaration

    debugger;
    let result = true;
    let arr = new Array();

    // Photo
    let filePath = '';
    let fileUploader;
    let fileUploaderInputHtml = '';
    let imageTagHtml = '';
    let fileCaption = '';
    let localStoragePath = '';
    let isChangedPhoto = false;
    let isChangedSign = false;

    // Document
    let files;
    let selectedDocumentObject;

    SetPageLoadingDefaultValues();

    function SetPageLoadingDefaultValues() {
        debugger;
        if (($('#photo-document-upload').val()) === MANDATORY)
        {
            $('#photo-file-uploader').addClass('mandatory-mark');
            $('#photo-file-uploader').attr('required');
            $('#photo-file-caption').attr('required');
            $('#photo-file-caption').addClass('mandatory-mark');
        }
        else
        {
            $('#photo-file-uploader').removeClass('mandatory-mark');
            $('#photo-file-uploader').removeAttr('required');
            $('#photo-file-caption').removeAttr('required');
            $('#photo-file-caption').removeClass('mandatory-mark');
        }

        if (($('#sign-document-upload').val()) === MANDATORY)
        {
            $('#sign-file-uploader').addClass('mandatory-mark');
            $('#sign-file-uploader').attr('required');
            $('#sign-file-caption').attr('required');
            $('#sign-file-caption').addClass('mandatory-mark');
        }
        else
        {
            $('#sign-file-uploader').removeClass('mandatory-mark');
            $('#sign-file-uploader').removeAttr('required');
            $('#sign-file-caption').removeAttr('required');
            $('#sign-file-caption').removeClass('mandatory-mark');
        }
    }

    // Document File Uploader
    $('.doc-upload').change(function ()
    {
        debugger;
        let docInput = '';
        let myId = $(this).attr('id');  

        // Document type determination
        switch (myId) {
            case 'photo-file-uploader':
                docInput = 'Photo';
                break;
            case 'sign-file-uploader':  
                docInput = 'Sign';  
                break;
            default:
                docInput = 'None';
        }

        // Hide the error message for the current uploader
        $('#' + myId + '-error').addClass('d-none');

        if (this.files.length > 0)
        {
               debugger;
            const uploadFile = this.files[0];
            // Upload File
            if (IsValidFile(docInput, uploadFile)) {  
                let reader = new FileReader();

                reader.onload = function (e) {
                    // Update the correct image preview based on the document type
                    if (docInput === 'Photo')
                    {
                        $('#photo-file-uploader-image-preview').attr('src', e.target.result);
                        $('#photo-file-caption').val('');
                    }
                    else if (docInput === 'Sign') {
                        $('#sign-file-uploader-image-preview').attr('src', e.target.result);
                        $('#sign-file-caption').val('');
                    }
                }

                reader.readAsDataURL(uploadFile);
            }
        } else {
            // Clear image preview if no file is selected
            if (docInput === 'Photo')
            {
                $('#photo-file-uploader').val('');
                $('#photo-file-uploader-image-preview').attr('src', '');
            } else if (docInput === 'Sign') {
                $('#sign-file-uploader-image-preview').attr('src', '');
                $('#sign-file-uploader').val('');
            }
        }
    });

    // Validate Uploaded File
    function IsValidFile(_inputSource, _uploadFile) {  
        debugger;
        let result = true;

        if (_inputSource === 'Photo')
            isChangedPhoto = true;

        if (_inputSource === 'Sign')
            isChangedSign = true;      

        if (_uploadFile)
        {
            const fileName = _uploadFile.name;
            const fileSize = Math.round(_uploadFile.size / 1024);
            const fileExtension = fileName.split('.').pop().toLowerCase();

            let validDocumentFileExtensions = '';
            let maxDocumentFileSize = 0;

            let uploaderId = _inputSource.replace('Asset', '').toLowerCase();

            // Photo
            if (_inputSource === 'Photo') {
                uploaderId = 'photo';
            }

            // Sign
            if (_inputSource === 'Sign') {
                uploaderId = 'sign';
            }

            let isUploadInLocalStorage = personInformationParameterViewModel[`Enable${_inputSource}DocumentUploadInLocalStorage`];

            // Get File Formats And File Size By Storage
            if (isUploadInLocalStorage === true) {
                validDocumentFileExtensions = personInformationParameterViewModel[`${_inputSource}DocumentAllowedFileFormatsForLocalStorage`].toLowerCase().replace('.', '');
                maxDocumentFileSize = personInformationParameterViewModel[`MaximumFileSizeFor${_inputSource}DocumentUploadInLocalStorage`];
            } else {
                validDocumentFileExtensions = personInformationParameterViewModel[`${_inputSource}DocumentAllowedFileFormatsForDb`].toLowerCase().replace('.', '');
                maxDocumentFileSize = personInformationParameterViewModel[`MaximumFileSizeFor${_inputSource}DocumentUploadInDb`];
            }

            // Check Valid File Formats Or Size
            if (validDocumentFileExtensions.indexOf(fileExtension) === -1 || parseInt(fileSize) > parseInt(maxDocumentFileSize)) {
                $('#' + uploaderId + '-file-uploader-error').text('Invalid File Type. Only ' + validDocumentFileExtensions + ' Are Allowed.' + ' The Maximum File Size Limit Of ' + maxDocumentFileSize);
                $('#' + uploaderId + '-file-uploader-error').removeClass('d-none');

                $('#' + uploaderId + '-file-uploader-image-preview').attr('src', '#');
                $('#' + uploaderId + '-file-uploader').val('');

                result = false;
            }
        }

        return result;
    }

    function IsValidPhoto() {
        result = true;
        debugger;
        let fileUploader = $('#photo-file-uploader').get(0);

        // Validate Photo Document
        if (fileUploader.files.length === 0) {
            debugger;
            if ($('#photo-document-upload').val() === MANDATORY) {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isChangedPhoto === true) {
                    result = false;
                    $('#photo-file-uploader-error').removeClass('d-none');
                }
            }
                // ****** New Changes
            else {
                let photoSrc = $('#photo-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    $('#photo-file-caption').val('NotApplicable');
                    localStoragePath = 'None';
                }
            }
        }
        else {
            $('#photo-file-caption').val('None');
        }

        return result;
    }

    function IsValidSign() {
        debugger;
        result = true;
        let fileUploader = $('#sign-file-uploader').get(0);

        // Validate Photo Document
        if (fileUploader.files.length === 0)
        {
            debugger;
            if ($('#sign-document-upload').val() === MANDATORY)
            {
                // Validate Only New Record ** (i.e. Skip Uploaded From Database)
                if (isChangedSign === true)
                {
                    result = false;
                    $('#sign-file-uploader-error').removeClass('d-none');
                }
            }
                // ****** New Changes
            else {
                let photoSrc = $('#sign-file-uploader-image-preview').attr('src');

                // Don't Change, It Is Refereed For AttachFileUploader()
                if (photoSrc.toString().length < 2) {
                    fileCaption = 'NotApplicable';
                    $('#sign-file-caption').val('NotApplicable');
                    localStoragePath = 'None';
                }
            }
        }
        else {
            $('#sign-file-caption').val('None');
        }
        return result;
    }

    // @@@@@@@@@@@@@@@@@ Code to save changes .submit(function(e) @@@@@@@@@@@@@@@@

    $('#btnsave').click(function ()
    {
        debugger;
 
        let isValidAllInputs = true;

        if ($('form').valid())
        {
            debugger;
            // Validate photo and sign 
            if (IsValidPhoto() === false)
            {
                isValidAllInputs = false;
            }

            if (IsValidSign() === false)
            {
                isValidAllInputs = false;  
            }

            if (isValidAllInputs === false)
            {
                alert('Oops, Something Went Wrong!, Please Provide Valid Data');
                event.preventDefault();
            }
        }
        else
        {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });

});