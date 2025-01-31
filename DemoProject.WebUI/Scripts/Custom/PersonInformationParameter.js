'use strict';
$(document).ready(function () {

    // @@@@@@@@@@ Data Table Related letible Declaration
    debugger;
    debugger;
    const ALL = 'All';
    const DISABLE_VALUE = 'D';
    const MANDATORY_VALUE = 'M';

    let tag = '';
    let id = '';
    let myModal;
    let selectedRowIndex;
    let row;
    let rowData;
    let checked;
    let columnValues;
    let isChecked = false;
    let isCheckedAll = false;
    let currentRow;
    let minimum;
    let maximum;
    let multiSelectCount;
    let arr = new Array();

    //Sms Alert
    let noticeTypeId = '';
    let noticeTypeIdText = '';
    let nameOfNoticeType = '';
    let enableNoticeInRegionalLanguage = false;
    let maximumResendsOnFailure = '';
    let activationDate = '';
    let expiryDate = '';
    let closeDate = '';
    let note;
    let result = true;
    let PrmKey = 0;
    let scheduletime = [];
    let eventObjId = [];
    let len = 0;

    //KYC Document
    let documentId = '';
    let documentIdText = '';
    let isMandatory = false;
    let documentList = '';
    let i;
    let editedDocumentTypeId = '';
    //let reasonForModification = '';
    const DOCUMENT_TYPE_DROPDOWN_LIST = $('#document-type-id').html();

    let smsAlertDataTable = CreateDataTable('sms-alert');
    let KycDataTable = CreateDataTable('kyc-document');

    // Declaration
    let disableValue = 'D';

    // *************** C A L L   P A G E   L O A D I N G   D E F A U L T   V A L U E S ***************

    SetPageLoadingDefaultValues();

    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  E V E N T   H A N D L I N G  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    // @@@@@@@@@@@@@@@@@@@@@@@@@@   C H A N G E / F O C U S O U T   E V E N T  @@@@@@@@@@@@@@@@@@@@@@@@@@

    // Visiblility Of Mobile OTP Verification
    $('#enable-mobile-verification').change(function () {
        debugger;
        if ($(this).is(':checked'))
            $('.otp-moblie-input').val('');
    });

    // Add Minimum, Maximum Attribute To Person Information Number Increment By
    $('#person-information-number-increment-by').focus(function (event) {
        $('#person-information-number-increment-by').attr('min', 1);
        //$('#person-information-number-increment-by').attr('max', $('#end-person-information-number').val());
    });

    function SetDocumentUploadInput(_uploadInputId) {
        // Document Upload Input Visibility Based On Selection i.e. Mandatory, Optional, Disable

        // eventObjId Is Collection Of All Accordions Document Value i.e. Mandatory, Optional, Disable
        // Using Naming Convention Based On eventObjId All Corrospondent Inputs Are Show And Hide
        debugger;
        let eventObjId = [];
        let len = 0;

        // Document Upload Visibility Based On Selecting Parameter

        // Other Remaining Accordion Visibility
        // -db-ts = database toggle switch         -ls-ts = local storage toggle switch
        // -db-tf = database text fields           -ls-tf = local storage text fields
        // -ls-pt = Local Storage Path
        let documentUploadValue = '';

        if (_uploadInputId === ALL) {
            eventObjId = ['photo-document-upload', 'sign-document-upload', 'kyc-document-upload', 'bank-document-upload', 'gst-document-upload', 'incometax-document-upload', 'financial-document-upload', 'agriculture-document-upload', 'immovable-document-upload', 'movable-document-upload', 'machinery-document-upload', 'death-document-upload'];
        }
        else
        {
            eventObjId = [];
            eventObjId.push(_uploadInputId);
        }

        len = eventObjId.length

        for (let i = 0; i < len; i++) {
            if (eventObjId[i] === 'document-upload') {
                documentUploadValue = MANDATORY_VALUE;
            }
            else {
                documentUploadValue = $('input[id=' + eventObjId[i] + ']:checked').val();
            }

            if (documentUploadValue === DISABLE_VALUE || typeof documentUploadValue === 'undefined') {
                ResetFileUpload(eventObjId[i]);

                // ****************** Remove Code If Above Function Work Properly
                //$('.' + eventObjId[i] + '-ls-ts').addClass('read-only');
                //$('.' + eventObjId[i] + '-db-ts').addClass('read-only');
                //$('.' + eventObjId[i] + '-ls-ms').prop('disabled', true);
                //$('.' + eventObjId[i] + '-ls-pt').addClass('read-only');
                //$('.' + eventObjId[i] + '-ls-tf').addClass('read-only');
                //$('.' + eventObjId[i] + '-db-ms').prop('disabled', true);
                //$('.' + eventObjId[i] + '-db-tf').addClass('read-only');
                //$('.' + eventObjId[i] + '-db-tf').prop('min', 0);
                //$('.' + eventObjId[i] + '-ls-tf').prop('min', 0);

                //$('.' + eventObjId[i] + '-ls-ts').prop('checked', false);
                //$('.' + eventObjId[i] + '-db-ts').prop('checked', false);

                //$('.' + eventObjId[i] + "-ls-ms > option").prop('selected', false);
                //$('.' + eventObjId[i] + "-db-ms > option").prop('selected', false);

                //$('.' + eventObjId[i] + '-ls-pt').val('');
                //$('.' + eventObjId[i] + '-db-tf').val('');
                //$('.' + eventObjId[i] + '-ls-tf').val('');

                //$('.' + eventObjId + '-db-ms').prop('disabled', true);
                //$('.' + eventObjId + '-db-tf').prop('disabled', true);

                //$('.' + eventObjId + '-ls-ms').prop('disabled', true);
                //$('.' + eventObjId + '-ls-tf').prop('disabled', true);
                //$('.' + eventObjId + '-ls-pt').prop('disabled', true);
            }
            else {
                // Hide Error
                $('#' + eventObjId[i] + '-error').addClass('d-none');

                // if Database Storage Is True 
                if ($('#' + eventObjId[i] + '-dbts').is(':checked')) {
                    $('.' + eventObjId[i] + '-db-ms').prop('disabled', false);
                    $('.' + eventObjId[i] + '-db-tf').addClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-db-tf').prop('disabled', false);
                    $('.' + eventObjId[i] + '-db-tf').prop('min', 1);

                    $('.' + eventObjId[i] + '-ls-ts').prop('disabled', true);
                    $('.' + eventObjId + '-ls-ms').prop('disabled', true);
                    $('.' + eventObjId + '-ls-tf').prop('disabled', true);
                    $('.' + eventObjId + '-ls-pt').prop('disabled', true);
                }
                else {
                    $('.' + eventObjId[i] + '-db-ms').prop('disabled', true);
                    $('.' + eventObjId[i] + '-db-ms > option').prop('selected', false);
                    $('.' + eventObjId[i] + '-db-tf').removeClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-db-tf').prop('disabled', true);
                    $('.' + eventObjId[i] + '-db-tf').prop('min', 0);
                    $('.' + eventObjId[i] + '-db-tf').val(0);

                    $('.' + eventObjId[i] + '-ls-ts').prop('disabled', false);

                    objSelect2.trigger('change');
                }

                // if Local Storage Is True 
                if ($('#' + eventObjId[i] + '-lsts').is(':checked')) {
                    $('.' + eventObjId[i] + '-ls-ms').prop('disabled', false);
                    $('.' + eventObjId[i] + '-ls-tf').addClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-ls-tf').prop('disabled', false);
                    $('.' + eventObjId[i] + '-ls-tf').prop('min', 1);
                    $('.' + eventObjId[i] + '-ls-pt').prop('disabled', false);
                    $('.' + eventObjId[i] + '-ls-pt').addClass('mandatory-mark');

                    $('.' + eventObjId[i] + '-db-ts').prop('disabled', true);
                    $('.' + eventObjId + '-db-ms').prop('disabled', true);
                    $('.' + eventObjId + '-db-tf').prop('disabled', true);
                }
                else {
                    $('.' + eventObjId[i] + '-ls-ms').prop('disabled', true);
                    $('.' + eventObjId[i] + '-ls-ms > option').prop('selected', false);
                    $('.' + eventObjId[i] + '-ls-tf').removeClass('mandatory-mark');
                    $('.' + eventObjId[i] + '-ls-tf').prop('disabled', true);
                    $('.' + eventObjId[i] + '-ls-tf').attr('min', 0);
                    $('.' + eventObjId[i] + '-ls-tf').val(0);
                    $('.' + eventObjId[i] + '-ls-pt').prop('disabled', true);
                    $('.' + eventObjId[i] + '-ls-pt').removeClass('mandatory-mark');

                    $('.' + eventObjId[i] + '-db-ts').prop('disabled', false);

                    objSelect2.trigger('change');
                }
            }
        }
    }

    // On Any Document Upload Option Button Change Event (i.e. Every Mandatory, Optional, Disable)
    $('.document-upload').change(function () {
        // Get Id
        let eventObjId = this.id;

        // If Values Are Mandatory Or Optional - Make It Writable i.e. Remove Read-Only Class
        // -db-ts = database toggle switch         -ls-ts = local storage toggle switch
        // -db-tf = database text fields           -ls-tf = local storage text fields
        // -db-ms = database multiselect           -ls-ms = local storage multi select i.e file formats

        // If Not Disable i.e For Mandatory Or Optional
        if ($(this).val() !== DISABLE_VALUE) {
            SetDocumentUploadInput(eventObjId);
        }
        else {
            ResetFileUpload(eventObjId);
        }
    });


    // On Every Toggle Switch Button Change Event (i.e. Every Enable Upload In Database Storage)
    $('.toggle-switch-db').change(function () {
        debugger;
        let eventObjId = this.id;

        SetDocumentUploadInput(eventObjId.replace('-dbts', ''));
    });

    // On Every Toggle Switch Button Change Event (i.e. Every Enable Upload In Local Storage)
    $('.toggle-switch-ls').change(function () {
        debugger;
        let eventObjId = this.id;

        SetDocumentUploadInput(eventObjId.replace('-lsts', ''));
    });

    // Reset File Upload Of All Upload Configuration Or On Disable
    function ResetFileUpload(_uploadInputId)
    {
        // Document Upload Visibility Based On Selecting Parameter

        // Other Remaining Accordion Visibility
        // -db-ts = database toggle switch         -ls-ts = local storage toggle switch
        // -db-tf = database text fields           -ls-tf = local storage text fields
        // -ls-pt = Local Storage Path

        if (_uploadInputId === 'document-upload') {
            $('.' + _uploadInputId + '-ls-ts').prop('disabled', false);
            $('.' + _uploadInputId + '-db-ts').prop('disabled', false);
        }
        else {
            $('.' + _uploadInputId + '-ls-ts').prop('checked', false);
            $('.' + _uploadInputId + '-db-ts').prop('checked', false);

            $('.' + _uploadInputId + '-ls-ts').prop('disabled', true);
            $('.' + _uploadInputId + '-db-ts').prop('disabled', true);
        }

        $('.' + _uploadInputId + '-ls-ms').prop('disabled', true);

        $('.' + _uploadInputId + "-ls-ms > option").prop('selected', false);

        $('.' + _uploadInputId + '-ls-pt').prop('disabled', true);
        $('.' + _uploadInputId + '-ls-pt').removeClass('mandatory-mark');
        $('.' + _uploadInputId + '-ls-pt').val('None');
        $('.' + _uploadInputId + '-ls-tf').prop('disabled', true);
        $('.' + _uploadInputId + '-ls-tf').removeClass('mandatory-mark');
        $('.' + _uploadInputId + '-ls-tf').attr('min', 0);
        $('.' + _uploadInputId + '-ls-tf').val('0');


        $('.' + _uploadInputId + '-db-ms').prop('disabled', true);

        $('.' + _uploadInputId + '-db-ms > option').prop('selected', false);

        $('.' + _uploadInputId + '-db-tf').prop('disabled', true);
        $('.' + _uploadInputId + '-db-tf').removeClass('mandatory-mark');
        $('.' + _uploadInputId + '-db-tf').attr('min', 0);
        $('.' + _uploadInputId + '-db-tf').val('0');

        $('.' + _uploadInputId + '-ls-tf').prop('disabled', true);
        $('.' + _uploadInputId + '-ls-pt').prop('disabled', true);
        $('.' + _uploadInputId + '-ls-ms').prop('disabled', true);

        $('.' + _uploadInputId + '-db-tf').prop('disabled', true);
        $('.' + _uploadInputId + '-db-ms').prop('disabled', true);

        // Clear Error Fro Ex - vehicle-photo-upload-error, vehicle-photo-upload-required-error
        $('#' + _uploadInputId + '-error').addClass('d-none');
        $('#' + _uploadInputId + '-required-error').addClass('d-none');

        // Its Object Of MultiSelect Required For Changing Effects Create At View Footer.
        objSelect2.trigger('change');
    }

    // Person Number For Toggle Switch Click
    $('#enable-auto-person-number').change(function () {
        IsValidPersonInformationNumberAccordionInputs();
    });

    // Person Number Parameter
    $('.person-number-input').focusout(function () {
        IsValidPersonInformationNumberAccordionInputs();
    });

    // Photo Sign Accordion Input Validation
    $('.photo-sign-input').focusout(function () {
        IsValidPhotoSignAccordionInputs();
    });

    // KYC Accordion Input Validation
    $('.kyc-input').focusout(function () {
        IsValidKycDocumentAccordionInputs();
    });

    // Icome Tax Parameter Accordion Input Validation
    $('.income-tax-input').focusout(function () {
        IsValidIncomeTaxAccordionInputs();
    });

    //Icome Tax Parameter Accordion Input Validation
    $('#enable-income-tax-document-upload').change(function () {
        IsValidIncomeTaxAccordionInputs();
 
    });
     
    // Bank Accordion Input Validation
    $('.bank-input').focusout(function () {
        IsValidBankDetailsAccordionInputs();
    });

    // Bank Accordion Input Validation
    $('#enable-bank-document-upload').change(function () {
        IsValidBankDetailsAccordionInputs();
    });

    // Financial Asset Accordion Input Validation
    $('.financial-input').focusout(function () {
        IsValidFinancialAssetAccordionInputs();
    });

    // Financial Asset Accordion Input Validation
    $('#enable-financial-asset-document-upload').change(function () {
        IsValidFinancialAssetAccordionInputs();
    });

    // Movable Asset Accordion Input Validation
    $('.movable-input').focusout(function () {
        IsValidMovableAssetAccordionInputs();
    });

    // Movable Asset Accordion Input Validation
    $('#enable-movable-asset-document-upload').change(function () {
        IsValidMovableAssetAccordionInputs();
    });

    // Immovable Asset Accordion Input Validation
    $('.immovable-input').focusout(function () {
        IsValidImmovableAssetAccordionInputs();
    });

    // Immovable Asset Accordion Input Validation
    $('#enable-immovable-asset-document-upload').change(function () {
        IsValidImmovableAssetAccordionInputs();
    });

    // Agriculture Asset Accordion Input Validation
    $('.agriculture-input').focusout(function () {
        IsValidAgricultureAssetAccordionInputs();
    });

    // Agriculture Asset Accordion Input Validation
    $('#enable-agriculture-asset-document-upload').change(function () {
        IsValidAgricultureAssetAccordionInputs();
    });

    // Machinery Asset Accordion Input Validation
    $('.machinery-input').focusout(function () {
        IsValidMachineryAssetAccordionInputs();
    });

    // Machinery Asset Accordion Input Validation
    $('#enable-machinery-asset-document-upload').change(function () {
        IsValidMachineryAssetAccordionInputs();
    });

    // Machinery Asset Accordion Input Validation
    $('.death-input').focusout(function () {
        IsValidPersonDeathAssetAccordionInputs();
    });

    // GST Document Accordion Input Validation
    $('.gst-input').focusout(function () {
        IsValidGstDetailAccordionInputs();
    });

    // GST Document Accordion Input Validation
    $('#enable-gst-document-upload').change(function () {
        IsValidGstDetailAccordionInputs();
    });


    // Visiblility Of Person Information Number
    function IsValidPersonInformationNumberAccordionInputs() {
        debugger;
        result = true;
        if ($('#enable-person-information-number-branchwise').is(':checked')) {
            let multiSelectcount = 0;

            if ($('#enable-auto-person-number').is(':checked')) {

                let checksumAlgorithm = $('#checksum-algorithm-id option:selected').val();
                let checksumAlgorithmText = $('#checksum-algorithm-id option:selected').text();

                let startNumber = parseInt($('#start-person-information-number').val());
                let endNumber = parseInt($('#end-person-information-number').val());
                let personNumberIncrementBy = parseInt($('#person-information-number-increment-by').val());

                multiSelectcount = $('#person-information-number-mask option:selected').length;


                //Person Information Mask
                if (multiSelectcount === 0)
                    result = false;


                if ($('#checksum-algorithm-id').val() === '') {
                    result = false;
                }

                // Start Application Number
                if (isNaN(startNumber) === false) {
                    minimum = parseInt($('#start-person-information-number').attr('min'));
                    maximum = parseInt($('#start-person-information-number').attr('max'));

                    if (parseInt(startNumber) < parseInt(minimum) || parseInt(startNumber) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;

                // End Application Number
                if (isNaN(endNumber) === false) {
                    minimum = parseInt($('#end-person-information-number').attr('min'));
                    maximum = parseInt($('#end-person-information-number').attr('max'));

                    if ((parseInt(startNumber) + 100) > parseInt(endNumber) || parseInt(endNumber) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;

                // Application Number Increment By
                if (isNaN(personNumberIncrementBy) === false) {
                    minimum = parseInt($('#person-information-number-increment-by').attr('min'));
                    maximum = parseInt($('#person-information-number-increment-by').attr('max'));

                    if (parseInt(personNumberIncrementBy) === 0 || parseInt(personNumberIncrementBy) > parseInt(((parseInt(endNumber) - parseInt(startNumber)) / 100)))
                        result = false;
                }
                else
                    result = false;
            }
        }

        if (result)
            $('#auto-person-number-error').addClass('d-none');
        else
            $('#auto-person-number-error').removeClass('d-none');

        return result;
    }

    //Photo Sign Accordion Input Validation
    function IsValidPhotoSignAccordionInputs() {
        debugger;

        // Get the text and value of the selected item
        let photoDocumentUploadText = $('#photo-document-upload:checked').next('label').text();
        let photoDocumentUpload = $('#photo-document-upload:checked').val();
        let enablePhotoDb = $('#photo-document-upload-dbts').is(':checked');
        let enablePhotoLocalStorage = $('#photo-document-upload-lsts').is(':checked');
        let maximumFileSizePhotoDb = parseInt($('#maximum-file-size-db-photo').val());
        let maximumFileSizePhotoLs = parseInt($('#maximum-file-size-local-storage-photo').val());
        let photoLocalStoragePath = $('#photo-local-storage-path').val();

        let signDocumentUploadText = $('#sign-document-upload:checked').next('label').text();
        let signDocumentUpload = $('#sign-document-upload:checked').val();
        let enableSignDb = $('#sign-document-upload-dbts').is(':checked');
        let maximumFileSizeSignDb = parseInt($('#sign-maximum-file-size-db').val());
        let enableSignLocalStorage = $('#sign-document-upload-lsts').is(':checked');
        let maximumFileSizeSignLs = parseInt($('#sign-maximum-file-size-ls').val());
        let signLocalStoragePath = $('#sign-local-storage-path').val();
        multiSelectCount = 0;

        result = true;
        if (!$('#heading-photo-sign-document-upload').hasClass('d-none')) {
            // Check if any input is empty or not within specified range

            if (photoDocumentUploadText === '') {
                result = false;
                $('#photo-document-upload-error').removeClass('d-none');
            }
            else
                $('#photo-document-upload-error').addClass('d-none');


            if (photoDocumentUpload === 'M' || photoDocumentUpload === 'O') {
                if (!enablePhotoDb && !enablePhotoLocalStorage) {
                    result = false;
                    $('#photo-document-error').removeClass('d-none');
                } else {
                    $('#photo-document-error').addClass('d-none');
                }
            }
            else
                $('#photo-document-error').addClass('d-none');


            // Check If Gold Upload In DB Is Enabled
            if (enablePhotoDb === false && enablePhotoDb === false) {
                $('#photo-document-error').removeClass('d-none');
                $('#photo-sign-accordion-error').removeClass('d-none');

            }
            else {
                $('#photo-document-error').addClass('d-none');
                $('#photo-sign-accordion-error').addClass('d-none');

            }
            if (enablePhotoDb) {

                multiSelectCount = parseInt($('#file-format-db-photo option:selected').length);

                // Agreement Number Mask
                if (multiSelectCount === 0)
                    result = false;

                //Maximum File Size For Local Storage
                if (isNaN(maximumFileSizePhotoDb) === false) {
                    minimum = parseInt($('#maximum-file-size-db-photo').attr('min'));
                    maximum = parseInt($('#maximum-file-size-db-photo').attr('max'));

                    if (parseInt(maximumFileSizePhotoDb) < parseInt(minimum) || parseInt(maximumFileSizePhotoDb) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

            }

            // Check If Gold Upload In Local Storage Is Enabled
            if (enablePhotoLocalStorage) {
                debugger;

                if (photoLocalStoragePath === '') {
                    result = false;
                }


                multiSelectCount = parseInt($('#file-format-local-storage-photo option:selected').length);

                // Agreement Number Mask
                if (multiSelectCount === 0)
                    result = false;

                //Maximum File Size For Local Storage
                if (isNaN(maximumFileSizePhotoLs) === false) {
                    minimum = parseInt($('#maximum-file-size-local-storage-photo').attr('min'));
                    maximum = parseInt($('#maximum-file-size-local-storage-photo').attr('max'));

                    if (parseInt(maximumFileSizePhotoLs) < parseInt(minimum) || parseInt(maximumFileSizePhotoLs) > parseInt(maximum)) {
                        result = false;
                    }
                }
                else {
                    result = false;
                }

            }

            // Check if any input is empty or not within specified range
            if (signDocumentUploadText === '') {
                result = false;
                $('#sign-document-upload-error').removeClass('d-none');
            } else
                $('#sign-document-upload-error').addClass('d-none');


            if (signDocumentUpload === 'M' || signDocumentUpload === 'O') {
                if (!enableSignDb && !enableSignLocalStorage) {
                    result = false;
                    $('#sign-document-error').removeClass('d-none');
                }
                else
                    $('#sign-document-error').addClass('d-none');
            }
            else
                $('#sign-document-error').addClass('d-none');

            // Check If Sign Upload In DB Is Enabled
            if (enableSignDb) {
                if (isNaN(maximumFileSizeSignDb) == false && $('#file-format-db-sign').val() != '') {
                    // minimum  For Tenure 
                    minimum = parseInt($('#sign-maximum-file-size-db').attr('min'));
                    maximum = parseInt($('#sign-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeSignDb) < parseInt(minimum) || parseInt(maximumFileSizeSignDb) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }


            // Check If Sign Upload In Local Storage Is Enabled
            if (enableSignLocalStorage) {
                debugger;
                if (isNaN(maximumFileSizeSignLs) == false && signLocalStoragePath.trim() !== '' && $('#file-format-local-storage-sign').val() != '') {
                    minimum = parseInt($('#sign-local-storage-path').attr('min'));
                    maximum = parseInt($('#sign-local-storage-path').attr('max'));

                    if (signLocalStoragePath.length < minimum || signLocalStoragePath.length > maximum)
                        result = false;

                    // minimum  For Tenure 
                    minimum = parseInt($('#sign-maximum-file-size-ls').attr('min'));
                    maximum = parseInt($('#sign-maximum-file-size-ls').attr('max'));

                    if (parseInt(maximumFileSizeSignLs) < parseInt(minimum) || parseInt(maximumFileSizeSignLs) > parseInt(maximum))
                        result = false;
                }

                else
                    result = false;
            }
        }
        else
            result = false;

        if (result) {
            $('#photo-document-upload-error').addClass('d-none');
            $('#sign-document-upload-error').addClass('d-none');
            $('#photo-document-error').addClass('d-none');
            $('#sign-document-error').addClass('d-none');
            $('#photo-sign-accordion-error').addClass('d-none');
        }

        else {

            $('#photo-sign-accordion-error').removeClass('d-none');
        }

        return result;
    }

    //Kyc Document Accordion Input Validation
    function IsValidKycDocumentAccordionInputs() {
        debugger;
        // Get the text and value of the selected item
        let lowRiskReVerificationTimeInYear = parseInt($('#low-risk-kyc-reverification-time').val());
        let mediumRiskReVerificationTimeInYear = parseInt($('#medium-risk-kyc-reverification-time').val());
        let highRiskReVerificationTimeInYear = parseInt($('#high-risk-kyc-reverification-time').val());
        let veryHighRiskReVerificationTimeInYear = parseInt($('#very-high-risk-kyc-reverification-time').val());
        let kycDocumentUploadText = $('#kyc-document-upload:checked').next('label').text();
        let kycDocumentUpload = $('#kyc-document-upload:checked').val();
        let enableKycDb = $('#kyc-document-upload-dbts').is(':checked');
        let maximumFileSizeKycDb = parseInt($('#kyc-maximum-file-size-db').val());
        let enableKycLocalStorage = $('#kyc-document-upload-lsts').is(':checked');
        let maximumFileSizeKycLs = parseInt($('#kyc-maximum-file-size-ls').val());
        let kycLocalStoragePath = $('#kyc-local-storage-path').val();

        let result = true;
        if (!$('#heading-kyc-document-upload').hasClass('d-none')) {
            if (isNaN(lowRiskReVerificationTimeInYear) == false && isNaN(mediumRiskReVerificationTimeInYear) == false && isNaN(highRiskReVerificationTimeInYear) == false && isNaN(veryHighRiskReVerificationTimeInYear) == false) {

                // timePeriodForNewCustomerFlag 
                minimum = parseInt($('#low-risk-kyc-reverification-time').attr('min'));
                maximum = parseInt($('#low-risk-kyc-reverification-time').attr('max'));

                if (parseInt(lowRiskReVerificationTimeInYear) < parseInt(minimum) || parseInt(lowRiskReVerificationTimeInYear) > parseInt(maximum))
                    result = false;

                // Minimum Loan Amount For Individual
                minimum = parseInt($('#medium-risk-kyc-reverification-time').attr('max'));
                maximum = parseInt($('#medium-risk-kyc-reverification-time').attr('max'));

                if (parseInt(mediumRiskReVerificationTimeInYear) < lowRiskReVerificationTimeInYear || parseInt(mediumRiskReVerificationTimeInYear) > parseInt(maximum)) {
                    result = false;
                    $('#medium-risk-kyc-reverification-time-error').removeClass('d-none');
                }
                else
                    $('#medium-risk-kyc-reverification-time-error').addClass('d-none');

                // Minimum Loan Amount For Individual
                minimum = parseInt($('#high-risk-kyc-reverification-time').attr('min'));
                maximum = parseInt($('#high-risk-kyc-reverification-time').attr('max'));

                if (parseInt(highRiskReVerificationTimeInYear) < parseInt(mediumRiskReVerificationTimeInYear) || parseInt(highRiskReVerificationTimeInYear) > parseInt(maximum)) {
                    result = false;
                    $('#high-risk-kyc-reverification-time-error').removeClass('d-none');
                }
                else
                    $('#high-risk-kyc-reverification-time-error').addClass('d-none');

                // Minimum Loan Amount For Individual
                minimum = parseInt($('#very-high-risk-kyc-reverification-time').attr('min'));
                maximum = parseInt($('#very-high-risk-kyc-reverification-time').attr('max'));

                if (parseInt(veryHighRiskReVerificationTimeInYear) < parseInt(highRiskReVerificationTimeInYear) || parseInt(veryHighRiskReVerificationTimeInYear) > parseInt(maximum)) {
                    result = false;
                    $('#very-high-risk-kyc-reverification-time-error').removeClass('d-none');
                }
                else
                    $('#very-high-risk-kyc-reverification-time-error').addClass('d-none');

            }
            else {
                result = false;
            }
            // Check if any input is empty or not within specified range
            if (kycDocumentUploadText === '') {
                result = false;
                $('#kyc-document-upload-error').removeClass('d-none');
            } else
                $('#kyc-document-upload-error').addClass('d-none');


            if (kycDocumentUpload === 'M' || kycDocumentUpload === 'O') {
                if (!enableKycDb && !enableKycLocalStorage) {
                    result = false;
                    $('#kyc-document-error').removeClass('d-none');
                }
                else
                    $('#kyc-document-error').addClass('d-none');
            }
            else
                $('#kyc-document-error').addClass('d-none');

            // Check If KYC Upload In DB Is Enabled
            if (enableKycDb) {
                if (isNaN(maximumFileSizeKycDb) == false && $('#file-format-db-kyc').val() != '') {
                    // minimum  For Tenure 
                    let minimum = parseInt($('#kyc-maximum-file-size-db').attr('min'));
                    let maximum = parseInt($('#kyc-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeKycDb) < parseInt(minimum) || parseInt(maximumFileSizeKycDb) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }
            // Check If KYC Upload In Local Storage Is Enabled
            if (enableKycLocalStorage) {
                debugger;
                if (isNaN(maximumFileSizeKycLs) == false && kycLocalStoragePath.trim() !== '' && $('#file-format-local-storage-kyc').val() != '') {
                    let minimum = parseInt($('#kyc-local-storage-path').attr('min'));
                    let maximum = parseInt($('#kyc-local-storage-path').attr('max'));

                    if (kycLocalStoragePath.length < minimum || kycLocalStoragePath.length > maximum)
                        result = false;

                    // minimum  For Tenure 
                    minimum = parseInt($('#kyc-maximum-file-size-ls').attr('min'));
                    maximum = parseInt($('#kyc-maximum-file-size-ls').attr('max'));

                    if (parseInt(maximumFileSizeKycLs) < parseInt(minimum) || parseInt(maximumFileSizeKycLs) > parseInt(maximum))
                        result = false;
                }

                else
                    result = false;
            }

        }
        if (result) {
            $('#kyc-document-upload-error').addClass('d-none');
            $('#kyc-document-error').addClass('d-none');
            $('#kyc-document-accordion-error').addClass('d-none');
        } else
            $('#kyc-document-accordion-error').removeClass('d-none');


        return result;
    }

    //Income Tax Document Accordion Input Validation
    function IsValidIncomeTaxAccordionInputs() {
        debugger;
        // Get the text and value of the selected item
        let incomeTaxDocumentUploadText = $('#incometax-document-upload:checked').next('label').text();
        let incomeTaxDocumentUpload = $('#incometax-document-upload:checked').val();
        let enableIncomeTaxDb = $('#incometax-document-upload-dbts').is(':checked');
        let maximumFileSizeIncomeTaxDb = parseInt($('#income-tax-maximum-file-size-db').val());
        let enableIncomeTaxLocalStorage = $('#incometax-document-upload-lsts').is(':checked');
        let maximumFileSizeIncomeTaxLs = parseInt($('#income-tax-maximum-file-size-ls').val());
        let incomeTaxLocalStoragePath = $('#income-tax-local-storage-path').val();

        let result = true;
        if ($('#enable-income-tax-document-upload').is(':checked')) {

            // Check if any input is empty or not within specified range
            if (incomeTaxDocumentUploadText === '') {
                result = false;
                $('#incometax-document-upload-error').removeClass('d-none');
            } else
                $('#incometax-document-upload-error').addClass('d-none');

            if (incomeTaxDocumentUpload === 'M' || incomeTaxDocumentUpload === 'O') {
                $('#incometax-document-upload-error').addClass('d-none');

                if (enableIncomeTaxDb === true || enableIncomeTaxLocalStorage === true) {
                    $('#incometax-document-error').addClass('d-none');

                    // Check If Income Tax Upload In DB Is Enabled
                    if (enableIncomeTaxDb) {
                        if (isNaN(maximumFileSizeIncomeTaxDb) == false && $('#file-format-db-income-tax').val() != '') {
                            // minimum For Tenure 
                            minimum = parseInt($('#income-tax-maximum-file-size-db').attr('min'));
                            maximum = parseInt($('#income-tax-maximum-file-size-db').attr('max'));

                            if (parseInt(maximumFileSizeIncomeTaxDb) < parseInt(minimum) || parseInt(maximumFileSizeIncomeTaxDb) > parseInt(maximum))
                                result = false;
                        }
                        else
                            result = false;
                    }

                    // Check If Income Tax Upload In Local Storage Is Enabled
                    if (enableIncomeTaxLocalStorage) {
                        debugger;
                        if (isNaN(maximumFileSizeIncomeTaxLs) == false && incomeTaxLocalStoragePath.trim() !== '' && $('#file-format-local-storage-income-tax').val() != '') {
                            minimum = parseInt($('#income-tax-local-storage-path').attr('min'));
                            maximum = parseInt($('#income-tax-local-storage-path').attr('max'));

                            if (incomeTaxLocalStoragePath.length < minimum || incomeTaxLocalStoragePath.length > maximum)
                                result = false;

                            // minimum For Tenure 
                            minimum = parseInt($('#income-tax-maximum-file-size-ls').attr('min'));
                            maximum = parseInt($('#income-tax-maximum-file-size-ls').attr('max'));

                            if (parseInt(maximumFileSizeIncomeTaxLs) < parseInt(minimum) || parseInt(maximumFileSizeIncomeTaxLs) > parseInt(maximum))
                                result = false;
                        }

                        else
                            result = false;
                    }

                }
                else {
                    result = false;
                    $('#incometax-document-error').removeClass('d-none');
                }
            }
            else {
                result = false;
                $('#incometax-document-upload-error').removeClass('d-none');
            }
        }

        if (result) {
            $('#incometax-document-upload-error').addClass('d-none');
            $('#incometax-document-error').addClass('d-none');
            $('#income-tax-accordion-error').addClass('d-none');
        }

        else
            $('#income-tax-accordion-error').removeClass('d-none');

        return result;
    }

    //Bank Details Accordion Input Validation
    function IsValidBankDetailsAccordionInputs() {
        debugger;
        // Get the text and value of the selected item
        let bankDocumentUploadText = $('#bank-document-upload:checked').next('label').text();
        let bankDocumentUpload = $('#bank-document-upload:checked').val();
        let enableBankDb = $('#bank-document-upload-dbts').is(':checked');
        let maximumFileSizeBankDb = parseInt($('#bank-maximum-file-size-db').val());
        let enableBankLocalStorage = $('#bank-document-upload-lsts').is(':checked');
        let maximumFileSizeBankLs = parseInt($('#maximum-file-size-bank-local-storage').val());
        let bankLocalStoragePath = $('#bank-local-storage-path').val();

        result = true;
        if ($('#enable-bank-document-upload').is(':checked')) {
            // Check if any input is empty or not within specified range

            if (bankDocumentUploadText === '') {
                result = false;
                $('#bank-document-upload-error').removeClass('d-none');
            }
            else
                $('#bank-document-upload-error').addClass('d-none');

            if (bankDocumentUpload === 'M' || bankDocumentUpload === 'O') {
                if (!enableBankDb && !enableBankLocalStorage) {
                    result = false;
                    $('#bank-document-error').removeClass('d-none');
                } else {
                    $('#bank-document-error').addClass('d-none');
                }
            }
            else
                $('#bank-document-error').addClass('d-none');


            // Check If Gold Upload In DB Is Enabled
            if (enableBankDb) {
                if (isNaN(maximumFileSizeBankDb) == false && $('#file-format-db-bank').val() != '') {

                    // minimum  For Tenure 
                    minimum = parseInt($('#bank-maximum-file-size-db').attr('min'));
                    maximum = parseInt($('#bank-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeBankDb) < parseInt(minimum) || parseInt(maximumFileSizeBankDb) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }

            // Check If Gold Upload In Local Storage Is Enabled
            if (enableBankLocalStorage) {
                debugger;
                if (isNaN(maximumFileSizeBankLs) == false && bankLocalStoragePath.trim() !== '' && $('#file-format-local-storage-bank').val() != '') {

                    let minimum = parseInt($('#bank-local-storage-path').attr('min'));
                    let maximum = parseInt($('#bank-local-storage-path').attr('max'));

                    if (bankLocalStoragePath.length < minimum || bankLocalStoragePath.length > maximum)
                        result = false;


                    // minimum  For Tenure 
                    minimum = parseInt($('#maximum-file-size-bank-local-storage').attr('min'));
                    maximum = parseInt($('#maximum-file-size-bank-local-storage').attr('max'));


                    if (parseInt(maximumFileSizeBankLs) < parseInt(minimum) || parseInt(maximumFileSizeBankLs) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }

        }
        else
            result = false;

        if (result) {
            $('#bank-document-upload-error').addClass('d-none');
            $('#bank-document-error').addClass('d-none');
            $('#bank-accordion-error').addClass('d-none');
        }
        else
            $('#bank-accordion-error').removeClass('d-none');

        return result;
    }

    //Financial Asset Accordion Input Validation
    function IsValidFinancialAssetAccordionInputs() {
        debugger;
        // Get the text and value of the selected item
        let financialDocumentUploadText = $('#financial-document-upload:checked').next('label').text();
        let financialDocumentUpload = $('#financial-document-upload:checked').val();
        let enableFinancialDb = $('#financial-document-upload-dbts').is(':checked');
        let maximumFileSizeFinancialDb = parseInt($('#financial-maximum-file-size-db').val());
        let enableFinancialLocalStorage = $('#financial-document-upload-lsts').is(':checked');
        let maximumFileSizeFinancialLs = parseInt($('#financial-maximum-file-size-ls').val());
        let financialLocalStoragePath = $('#financial-local-storage-path').val();

        result = true;
        if ($('#enable-financial-asset-document-upload').is(':checked')) {
            // Check if any input is empty or not within specified range

            if (financialDocumentUploadText === '') {
                result = false;
                $('#financial-document-upload-error').removeClass('d-none');
            }
            else
                $('#financial-document-upload-error').addClass('d-none');

            if (financialDocumentUpload === 'M' || financialDocumentUpload === 'O') {
                if (!enableFinancialDb && !enableFinancialLocalStorage) {
                    result = false;
                    $('#financial-document-error').removeClass('d-none');
                } else {
                    $('#financial-document-error').addClass('d-none');
                }
            }
            else
                $('#financial-document-error').addClass('d-none');


            // Check If Gold Upload In DB Is Enabled
            if (enableFinancialDb) {
                if (isNaN(maximumFileSizeFinancialDb) == false && $('#file-format-db-financial').val() != '') {


                    // minimum  For Tenure 
                    minimum = parseInt($('#financial-maximum-file-size-db').attr('min'));
                    maximum = parseInt($('#financial-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeFinancialDb) < parseInt(minimum) || parseInt(maximumFileSizeFinancialDb) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;

            }

            // Check If Gold Upload In Local Storage Is Enabled
            if (enableFinancialLocalStorage) {
                debugger;
                if (isNaN(maximumFileSizeFinancialLs) == false && financialLocalStoragePath.trim() !== '' && $('#file-format-local-storage-financial').val() != '') {

                    minimum = parseInt($('#financial-local-storage-path').attr('min'));
                    maximum = parseInt($('#financial-local-storage-path').attr('max'));

                    if (financialLocalStoragePath.length < minimum || financialLocalStoragePath.length > maximum)
                        result = false;


                    // minimum  For Tenure 
                    minimum = parseInt($('#financial-maximum-file-size-ls').attr('min'));
                    maximum = parseInt($('#financial-maximum-file-size-ls').attr('max'));


                    if (parseInt(maximumFileSizeFinancialLs) < parseInt(minimum) || parseInt(maximumFileSizeFinancialLs) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }

        }
        else
            result = false;

        if (result) {
            $('#financial-document-upload-error').addClass('d-none');
            $('#financial-document-error').addClass('d-none');
            $('#financial-accordion-error').addClass('d-none');
        }

        else
            $('#financial-accordion-error').removeClass('d-none');

        return result;
    }

    //Movable Assets Accordion Input Validation
    function IsValidMovableAssetAccordionInputs() {
        debugger;
        // Get the text and value of the selected item
        let movableDocumentUploadText = $('#movable-document-upload:checked').next('label').text();
        let movableDocumentUpload = $('#movable-document-upload:checked').val();
        let enableMovableDb = $('#movable-document-upload-dbts').is(':checked');
        let maximumFileSizeMovableDb = parseInt($('#movable-maximum-file-size-db').val());
        let enableMovableLocalStorage = $('#movable-document-upload-lsts').is(':checked');
        let maximumFileSizeMovableLs = parseInt($('#movable-maximum-file-size-ls').val());
        let movableLocalStoragePath = $('#movable-local-storage-path').val();

        result = true;
        if ($('#enable-movable-asset-document-upload').is(':checked')) {
            // Check if any input is empty or not within specified range

            if (movableDocumentUploadText === '') {
                result = false;
                $('#movable-document-upload-error').removeClass('d-none');
            }
            else
                $('#movable-document-upload-error').addClass('d-none');

            if (movableDocumentUpload === 'M' || movableDocumentUpload === 'O') {
                if (!enableMovableDb && !enableMovableLocalStorage) {
                    result = false;
                    $('#movable-document-error').removeClass('d-none');
                } else {
                    $('#movable-document-error').addClass('d-none');
                }
            }
            else
                $('#movable-document-error').addClass('d-none');


            // Check If Gold Upload In DB Is Enabled
            if (enableMovableDb) {
                if (isNaN(maximumFileSizeMovableDb) == false && $('#file-format-db-movable').val() != '') {

                    // minimum  For Tenure 
                    minimum = parseInt($('#movable-maximum-file-size-db').attr('min'));
                    maximum = parseInt($('#movable-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeMovableDb) < parseInt(minimum) || parseInt(maximumFileSizeMovableDb) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;

            }

            // Check If Gold Upload In Local Storage Is Enabled
            if (enableMovableLocalStorage) {
                debugger;
                if (isNaN(maximumFileSizeMovableLs) == false && movableLocalStoragePath.trim() !== '' && $('#file-format-local-storage-movable').val() != '') {

                    minimum = parseInt($('#movable-local-storage-path').attr('min'));
                    maximum = parseInt($('#movable-local-storage-path').attr('max'));

                    if (movableLocalStoragePath.length < minimum || movableLocalStoragePath.length > maximum)
                        result = false;


                    // minimum  For Tenure 
                    minimum = parseInt($('#movable-maximum-file-size-ls').attr('min'));
                    maximum = parseInt($('#movable-maximum-file-size-ls').attr('max'));


                    if (parseInt(maximumFileSizeMovableLs) < parseInt(minimum) || parseInt(maximumFileSizeMovableLs) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }

        }
        else
            result = false;

        if (result) {
            $('#movable-document-upload-error').addClass('d-none');
            $('#movable-document-error').addClass('d-none');
            $('#movable-accordion-error').addClass('d-none');
        }

        else
            $('#movable-accordion-error').removeClass('d-none');

        return result;
    }

    //Immovable Assets Accordion Input Validation
    function IsValidImmovableAssetAccordionInputs() {
        debugger;
        // Get the text and value of the selected item
        let immovableDocumentUploadText = $('#immovable-document-upload:checked').next('label').text();
        let immovableDocumentUpload = $('#immovable-document-upload:checked').val();
        let enableImmovableDb = $('#immovable-document-upload-dbts').is(':checked');
        let maximumFileSizeImmovableDb = parseInt($('#immovable-maximum-file-size-db').val());
        let enableImmovableLocalStorage = $('#immovable-document-upload-lsts').is(':checked');
        let maximumFileSizeImmovableLs = parseInt($('#immovable-maximum-file-size-ls').val());
        let immovableLocalStoragePath = $('#immovable-local-storage-path').val();

        result = true;
        if ($('#enable-immovable-asset-document-upload').is(':checked')) {
            // Check if any input is empty or not within specified range

            if (immovableDocumentUploadText === '') {
                result = false;
                $('#immovable-document-upload-error').removeClass('d-none');
            }
            else
                $('#immovable-document-upload-error').addClass('d-none');

            if (immovableDocumentUpload === 'M' || immovableDocumentUpload === 'O') {
                if (!enableImmovableDb && !enableImmovableLocalStorage) {
                    result = false;
                    $('#immovable-document-error').removeClass('d-none');
                } else {
                    $('#immovable-document-error').addClass('d-none');
                }
            }
            else
                $('#immovable-document-error').addClass('d-none');


            // Check If Gold Upload In DB Is Enabled
            if (enableImmovableDb) {
                if (isNaN(maximumFileSizeImmovableDb) == false && $('#file-format-db-immovable').val() != '') {

                    // minimum  For Tenure 
                    minimum = parseInt($('#immovable-maximum-file-size-db').attr('min'));
                    maximum = parseInt($('#immovable-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeImmovableDb) < parseInt(minimum) || parseInt(maximumFileSizeImmovableDb) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;

            }

            // Check If Gold Upload In Local Storage Is Enabled
            if (enableImmovableLocalStorage) {
                debugger;
                if (isNaN(maximumFileSizeImmovableLs) == false && immovableLocalStoragePath.trim() !== '' && $('#file-format-local-storage-immovable').val() != '') {

                    minimum = parseInt($('#immovable-local-storage-path').attr('min'));
                    maximum = parseInt($('#immovable-local-storage-path').attr('max'));

                    if (immovableLocalStoragePath.length < minimum || immovableLocalStoragePath.length > maximum)
                        result = false;


                    // minimum  For Tenure 
                    minimum = parseInt($('#immovable-maximum-file-size-ls').attr('min'));
                    maximum = parseInt($('#immovable-maximum-file-size-ls').attr('max'));


                    if (parseInt(maximumFileSizeImmovableLs) < parseInt(minimum) || parseInt(maximumFileSizeImmovableLs) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }

        }
        else
            result = false;

        if (result) {
            $('#immovable-document-upload-error').addClass('d-none');
            $('#immovable-document-error').addClass('d-none');
            $('#immovable-accordion-error').addClass('d-none');
        }

        else
            $('#immovable-accordion-error').removeClass('d-none');

        return result;
    }

    // Machinery Assets Accordion Input Validation
    function IsValidMachineryAssetAccordionInputs() {
        debugger;
        // Get the text and value of the selected item
        let machineryDocumentUploadText = $('#machinery-document-upload:checked').next('label').text();
        let machineryDocumentUpload = $('#machinery-document-upload:checked').val();
        let enableMachineryDb = $('#machinery-document-upload-dbts').is(':checked');
        let maximumFileSizeMachineryDb = parseInt($('#machinery-maximum-file-size-db').val());
        let enableMachineryLocalStorage = $('#machinery-document-upload-lsts').is(':checked');
        let maximumFileSizeMachineryLs = parseInt($('#machinery-maximum-file-size-ls').val());
        let machineryLocalStoragePath = $('#machinery-local-storage-path').val();

        result = true;
        if ($('#enable-machinery-asset-document-upload').is(':checked')) {
            // Check if any input is empty or not within specified range

            if (machineryDocumentUploadText === '') {
                result = false;
                $('#machinery-document-upload-error').removeClass('d-none');
            }
            else
                $('#machinery-document-upload-error').addClass('d-none');

            if (machineryDocumentUpload === 'M' || machineryDocumentUpload === 'O') {
                if (!enableMachineryDb && !enableMachineryLocalStorage) {
                    result = false;
                    $('#machinery-document-error').removeClass('d-none');
                } else {
                    $('#machinery-document-error').addClass('d-none');
                }
            }
            else
                $('#machinery-document-error').addClass('d-none');

            // Check If Gold Upload In DB Is Enabled
            if (enableMachineryDb) {
                if (isNaN(maximumFileSizeMachineryDb) == false && $('#file-format-db-machinery').val() != '') {

                    // minimum For Tenure 
                    minimum = parseInt($('#machinery-maximum-file-size-db').attr('min'));
                    maximum = parseInt($('#machinery-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeMachineryDb) < parseInt(minimum) || parseInt(maximumFileSizeMachineryDb) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }

            // Check If Gold Upload In Local Storage Is Enabled
            if (enableMachineryLocalStorage) {
                debugger;
                if (isNaN(maximumFileSizeMachineryLs) == false && machineryLocalStoragePath.trim() !== '' && $('#file-format-local-storage-machinery').val() != '') {

                    minimum = parseInt($('#machinery-local-storage-path').attr('min'));
                    maximum = parseInt($('#machinery-local-storage-path').attr('max'));

                    if (machineryLocalStoragePath.length < minimum || machineryLocalStoragePath.length > maximum)
                        result = false;

                    // minimum For Tenure 
                    minimum = parseInt($('#machinery-maximum-file-size-ls').attr('min'));
                    maximum = parseInt($('#machinery-maximum-file-size-ls').attr('max'));

                    if (parseInt(maximumFileSizeMachineryLs) < parseInt(minimum) || parseInt(maximumFileSizeMachineryLs) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }

        }
        else
            result = false;

        if (result) {
            $('#machinery-document-upload-error').addClass('d-none');
            $('#machinery-document-error').addClass('d-none');
            $('#machinery-accordion-error').addClass('d-none');
        }

        else
            $('#machinery-accordion-error').removeClass('d-none');

        return result;
    }

    //Agriculture Assets Accordion Input Validation
    function IsValidAgricultureAssetAccordionInputs() {
        debugger;
        // Get the text and value of the selected item
        let agricultureDocumentUploadText = $('#agriculture-document-upload:checked').next('label').text();
        let agricultureDocumentUpload = $('#agriculture-document-upload:checked').val();
        let enableAgricultureDb = $('#agriculture-document-upload-dbts').is(':checked');
        let maximumFileSizeAgricultureDb = parseInt($('#agriculture-maximum-file-size-db').val());
        let enableAgricultureLocalStorage = $('#agriculture-document-upload-lsts').is(':checked');
        let maximumFileSizeAgricultureLs = parseInt($('#agriculture-maximum-file-size-ls').val());
        let agricultureLocalStoragePath = $('#agriculture-local-storage-path').val();

        result = true;
        if ($('#enable-agriculture-asset-document-upload').is(':checked')) {
            // Check if any input is empty or not within specified range

            if (agricultureDocumentUploadText === '') {
                result = false;
                $('#agriculture-document-upload-error').removeClass('d-none');
            }
            else
                $('#agriculture-document-upload-error').addClass('d-none');

            if (agricultureDocumentUpload === 'M' || agricultureDocumentUpload === 'O') {
                if (!enableAgricultureDb && !enableAgricultureLocalStorage) {
                    result = false;
                    $('#agriculture-document-error').removeClass('d-none');
                } else {
                    $('#agriculture-document-error').addClass('d-none');
                }
            }
            else
                $('#agriculture-document-error').addClass('d-none');


            // Check If Gold Upload In DB Is Enabled
            if (enableAgricultureDb) {
                if (isNaN(maximumFileSizeAgricultureDb) == false && $('#file-format-db-agriculture').val() != '') {

                    // minimum  For Tenure 
                    minimum = parseInt($('#agriculture-maximum-file-size-db').attr('min'));
                    maximum = parseInt($('#agriculture-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeAgricultureDb) < parseInt(minimum) || parseInt(maximumFileSizeAgricultureDb) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;

            }

            // Check If Gold Upload In Local Storage Is Enabled
            if (enableAgricultureLocalStorage) {
                debugger;
                if (isNaN(maximumFileSizeAgricultureLs) == false && agricultureLocalStoragePath.trim() !== '' && $('#file-format-local-storage-agriculture').val() != '') {

                    minimum = parseInt($('#agriculture-local-storage-path').attr('min'));
                    maximum = parseInt($('#agriculture-local-storage-path').attr('max'));

                    if (agricultureLocalStoragePath.length < minimum || agricultureLocalStoragePath.length > maximum)
                        result = false;


                    // minimum  For Tenure 
                    minimum = parseInt($('#agriculture-maximum-file-size-ls').attr('min'));
                    maximum = parseInt($('#agriculture-maximum-file-size-ls').attr('max'));


                    if (parseInt(maximumFileSizeAgricultureLs) < parseInt(minimum) || parseInt(maximumFileSizeAgricultureLs) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }

        }
        else
            result = false;

        if (result) {
            $('#agriculture-document-upload-error').addClass('d-none');
            $('#agriculture-document-error').addClass('d-none');
            $('#agriculture-accordion-error').addClass('d-none');
        }

        else
            $('#agriculture-accordion-error').removeClass('d-none');

        return result;
    }

    // PersonDeath Assets Accordion Input Validation
    function IsValidPersonDeathAssetAccordionInputs() {
        debugger;
        // Get the text and value of the selected item
        let deathDocumentUploadText = $('#death-document-upload:checked').next('label').text();
        let deathDocumentUpload = $('#death-document-upload:checked').val();
        let enableDeathDb = $('#death-document-upload-dbts').is(':checked');
        let maximumFileSizeDeathDb = parseInt($('#death-maximum-file-size-db').val());
        let enableDeathLocalStorage = $('#death-document-upload-lsts').is(':checked');
        let maximumFileSizeDeathLs = parseInt($('#death-maximum-file-size-ls').val());
        let deathLocalStoragePath = $('#death-local-storage-path').val();

        let result = true;
        if (!$('#heading-person-death').hasClass('d-none')) {

            // Check if any input is empty or not within specified range
            if (deathDocumentUploadText === '') {
                result = false;
                $('#death-document-upload-error').removeClass('d-none');
            } else
                $('#death-document-upload-error').addClass('d-none');


            if (deathDocumentUpload === 'M' || deathDocumentUpload === 'O') {
                if (!enableDeathDb && !enableDeathLocalStorage) {
                    result = false;
                    $('#death-document-error').removeClass('d-none');
                }
                else
                    $('#death-document-error').addClass('d-none');
            }
            else
                $('#death-document-error').addClass('d-none');

            // Check If Death Upload In DB Is Enabled
            if (enableDeathDb) {
                if (isNaN(maximumFileSizeDeathDb) == false && $('#file-format-db-death').val() != '') {
                    // minimum  For Tenure 
                    let minimum = parseInt($('#death-maximum-file-size-db').attr('min'));
                    let maximum = parseInt($('#death-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeDeathDb) < parseInt(minimum) || parseInt(maximumFileSizeDeathDb) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }


            // Check If Death Upload In Local Storage Is Enabled
            if (enableDeathLocalStorage) {
                debugger;
                if (isNaN(maximumFileSizeDeathLs) == false && deathLocalStoragePath.trim() !== '' && $('#file-format-local-storage-death').val() != '') {
                    let minimum = parseInt($('#death-local-storage-path').attr('min'));
                    let maximum = parseInt($('#death-local-storage-path').attr('max'));

                    if (deathLocalStoragePath.length < minimum || deathLocalStoragePath.length > maximum)
                        result = false;

                    // minimum  For Tenure 
                    minimum = parseInt($('#death-maximum-file-size-ls').attr('min'));
                    maximum = parseInt($('#death-maximum-file-size-ls').attr('max'));

                    if (parseInt(maximumFileSizeDeathLs) < parseInt(minimum) || parseInt(maximumFileSizeDeathLs) > parseInt(maximum))
                        result = false;
                }

                else
                    result = false;

            }

        }

        if (result) {
            $('#death-document-upload-error').addClass('d-none');
            $('#death-document-error').addClass('d-none');
            $('#death-accordion-error').addClass('d-none');
        } else
            $('#death-accordion-error').removeClass('d-none');

        return result;
    }

    // GST Detail Accordion Input Validation
    function IsValidGstDetailAccordionInputs() {
        debugger;
        // Get the text and value of the selected item
        let gstDocumentUploadText = $('#gst-document-upload:checked').next('label').text();
        let gstDocumentUpload = $('#gst-document-upload:checked').val();
        let enableGSTDb = $('#gst-document-upload-dbts').is(':checked');
        let maximumFileSizeGSTDb = parseInt($('#gst-maximum-file-size-db').val());
        let enableGSTLocalStorage = $('#gst-document-upload-lsts').is(':checked');
        let maximumFileSizeGSTLs = parseInt($('#gst-maximum-file-size-ls').val());
        let gstLocalStoragePath = $('#gst-local-storage-path').val();

        let result = true;
        if ($('#enable-gst-document-upload').is(':checked')) {

            // Check if any input is empty or not within specified range
            if (gstDocumentUploadText === '') {
                result = false;
                $('#gst-document-upload-error').removeClass('d-none');
            } else
                $('#gst-document-upload-error').addClass('d-none');

            if (gstDocumentUpload === 'M' || gstDocumentUpload === 'O') {
                if (!enableGSTDb && !enableGSTLocalStorage) {
                    result = false;
                    $('#gst-document-error').removeClass('d-none');
                }
                else
                    $('#gst-document-error').addClass('d-none');
            }
            else
                $('#gst-document-error').addClass('d-none');

            // Check If GST Upload In DB Is Enabled
            if (enableGSTDb) {
                if (isNaN(maximumFileSizeGSTDb) == false && $('#file-format-db-gst').val() != '') {
                    // minimum For Tenure 
                    minimum = parseInt($('#gst-maximum-file-size-db').attr('min'));
                    maximum = parseInt($('#gst-maximum-file-size-db').attr('max'));

                    if (parseInt(maximumFileSizeGSTDb) < parseInt(minimum) || parseInt(maximumFileSizeGSTDb) > parseInt(maximum))
                        result = false;
                }
                else
                    result = false;
            }

            // Check If GST Upload In Local Storage Is Enabled
            if (enableGSTLocalStorage) {
                debugger;
                if (isNaN(maximumFileSizeGSTLs) == false && gstLocalStoragePath.trim() !== '' && $('#file-format-local-storage-gst').val() != '') {
                    minimum = parseInt($('#gst-local-storage-path').attr('min'));
                    maximum = parseInt($('#gst-local-storage-path').attr('max'));

                    if (gstLocalStoragePath.length < minimum || gstLocalStoragePath.length > maximum)
                        result = false;

                    // minimum For Tenure 
                    minimum = parseInt($('#gst-maximum-file-size-ls').attr('min'));
                    maximum = parseInt($('#gst-maximum-file-size-ls').attr('max'));

                    if (parseInt(maximumFileSizeGSTLs) < parseInt(minimum) || parseInt(maximumFileSizeGSTLs) > parseInt(maximum))
                        result = false;
                }

                else
                    result = false;
            }
        }
        else
            result = false;

        if (result) {
            $('#gst-document-upload-error').addClass('d-none');
            $('#gst-document-error').addClass('d-none');
            $('#gst-accordion-error').addClass('d-none');
        }
        else
            $('#gst-accordion-error').removeClass('d-none');

        return result;
    }


    // For Select 2 Focusout Input Validation
    objSelect2.on('select2:close', function (e) {
        debugger;
        let myId = $(this).attr('id');
        if (myId == 'person-information-number-mask')
            IsValidPersonInformationNumberAccordionInputs();

        if (myId == 'file-format-db-photo', 'file-format-local-storage-photo') {
            IsValidPhotoSignAccordionInputs();
        }
        if (myId == 'file-format-db-sign', 'file-format-local-storage-sign') {
            IsValidPhotoSignAccordionInputs();
        }
        if (myId == 'file-format-db-kyc', 'file-format-local-storage-kyc') {
            IsValidKycDocumentAccordionInputs();
        }
        if (myId == 'file-format-db-income-tax', 'file-format-local-storage-income-tax') {
            IsValidIncomeTaxAccordionInputs();
        }

        if (myId == 'file-format-db-bank', 'file-format-local-storage-bank') {
            IsValidBankDetailsAccordionInputs();
        }
        if (myId == 'file-format-db-financial', 'file-format-local-storage-financial') {
            IsValidFinancialAssetAccordionInputs();
        }
        if (myId == 'file-format-db-movable', 'file-format-local-storage-movable') {
            IsValidMovableAssetAccordionInputs();
        }
        if (myId == 'file-format-db-immovable', 'file-format-local-storage-immovable') {
            IsValidImmovableAssetAccordionInputs();
        }
        if (myId == 'file-format-db-agriculture', 'file-format-local-storage-agriculture') {
            IsValidAgricultureAssetAccordionInputs();
        }
        if (myId == 'file-format-db-gst', 'file-format-local-storage-gst') {
            IsValidPersonDeathAssetAccordionInputs();
        }
        if (myId == 'file-format-db-gst', 'file-format-local-storage-gst') {
            IsValidGstDetailAccordionInputs();
        }
    })


    //  ################################### D A T A     T A B L E ###################################


    /// @@@@@@@@@@@@@@@@@@@@@@   KYC Document  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('kyc-document');

    // DataTable Add Button 
    $('#btn-add-kyc-document-dt').click(function () {
        debugger;
        event.preventDefault();
        SetDocumentTypeUniqueDropdownList();
        SetModalTitle('kyc-document', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-kyc-document-dt').click(function () {
        debugger;
        SetModalTitle('kyc-document', 'Edit');
        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-kyc-document-dt').data('rowindex');
            id = $('#kyc-document-modal').attr('id');
            editedDocumentTypeId = columnValues[1];
            myModal = $('#' + id).modal();

            let kYCActivationDate = GetOnlyDate(columnValues[4]);
            let kYCExpiryDate = GetOnlyDate(columnValues[5]);
            let kYCCloseDate = GetOnlyDate(columnValues[6]);

            // Display Value In Modal Inputs
            $('#document-type-id', myModal).val(columnValues[1]);
            $('#activation-date-document-type', myModal).val(kYCActivationDate);
            $('#expiry-date-document-type', myModal).val(kYCExpiryDate);
            $('#close-date-document-type', myModal).val(kYCCloseDate);
            $('#note-document-type', myModal).val(columnValues[7]);
            //$('#reason-for-modification-document-type', myModal).val(columnValues[8]);

            $('#is-mandatory', myModal).val(columnValues[3]);
            if (columnValues[3] === 'True') {
                $('#is-mandatory').prop('checked', true);
            }
            else {
                $('#is-mandatory').prop('checked', false);
            }

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-kyc-document-edit-dt').addClass('read-only');
            $('#kyc-document-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-kyc-document-modal').click(function (event) {
        debugger;
        if (IsValidKycDocumentModal()) {
            row = KycDataTable.row.add([
                    tag,
                    documentId,
                    documentIdText,
                    isMandatory,
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                    PrmKey

            ]).draw();
            $('#kyc-document-accordion-error').addClass('d-none');

            HideColumnsKycDocumentDataTable();

            KycDataTable.columns.adjust().draw();

            ClearModal('kyc-document');

            $('#kyc-document-modal').modal('hide');

            EnableNewOperation('kyc-document');
        }
    });

    // Modal update Button Event
    $('#btn-update-kyc-document-modal').click(function (event) {
        debugger;
        $('#select-all-kyc-document').prop('checked', false);
        if (IsValidKycDocumentModal()) {
            KycDataTable.row(selectedRowIndex).data([
                    tag,
                    documentId,
                    documentIdText,
                    isMandatory,
                    activationDate,
                    expiryDate,
                    closeDate,
                    note,
                    PrmKey

            ]).draw();

            HideColumnsKycDocumentDataTable();

            SetDocumentTypeUniqueDropdownList();

            KycDataTable.columns.adjust().draw();

            columnValues = $('#btn-update-kyc-document').data('rowindex');

            $('#tbl-kyc-document > tbody > tr').each(function () {
                currentRow = $(this).closest('tr');
                columnValues = (KycDataTable.row(currentRow).data());
                // Handling Code If Row Is Undefined Or Null
                if (typeof columnValues != 'undefined' && columnValues != null) {
                    $.get('/PersonChildAction/GetSysNameOfDocumentByDocumentId', { _documentTypeId: columnValues[3], async: false }, function (data) {
                        if (data == 'PAN') {
                            $('#heading-gst-registration-details').removeClass('d-none');

                            panCardNumber = documentNumber;

                            $('#registrations-number').val(documentNumber);
                            $('#collapse-person-kyc-document-validations span').html('');
                        }
                    });
                }
                else
                    return false;
            });

            $('#kyc-document-modal').modal('hide');

            EnableNewOperation('kyc-document');
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-kyc-document-dt').click(function (event) {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-kyc-document tbody input[type="checkbox"]:checked').each(function () {
                 KycDataTable.row($('#tbl-kyc-document tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                 rowData = $('#btn-delete-kyc-document-dt').data('rowindex');

                 SetDocumentTypeUniqueDropdownList();

                 EnableNewOperation('kyc-document');

                  $('#select-all-kyc-document').prop('checked', false);
                    // Add Required Error Message, If Table Has Not Any Record
                    if (!KycDataTable.data().any())
                    $('#kyc-document-accordion-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event -  
    $('#select-all-kyc-document').click(function () {
        debugger;
        if ($(this).prop('checked')) {
            $('#tbl-kyc-document tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = KycDataTable.row(row).index();

                rowData = (KycDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-kyc-document-dt').data('rowindex', arr);
                EnableDeleteOperation('kyc-document');
            });
        }
        else {
            EnableNewOperation('kyc-document');

            $('#tbl-kyc-document tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-kyc-document tbody').click("input[type=checkbox]", function () {
        debugger;
        $('#tbl-kyc-document input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                debugger;
                let row = $(this).closest('tr');
                selectedRowIndex = KycDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (KycDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('kyc-document');

                    $('#btn-update-kyc-document-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-kyc-document-dt').data('rowindex', rowData);
                    $('#btn-delete-kyc-document-dt').data('rowindex', arr);
                    $('#select-all-kyc-document').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-kyc-document tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('kyc-document');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('kyc-document');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('kyc-document');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-kyc-document').prop('checked', true);
        else
            $('#select-all-kyc-document').prop('checked', false);
    });

    // Validate Kyc Module
    function IsValidKycDocumentModal() {
        debugger;
        result = true;
        // Get Modal Inputs In Local letiable
        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        documentId = $('#document-type-id option:selected').val();
        documentIdText = $('#document-type-id option:selected').text();
        isMandatory = $('input[name="PersonInformationParameterDocumentTypeViewModel.IsMandatory"]').is(':checked') ? 'True' : "False";
        activationDate = $('#activation-date-document-type').val();
        expiryDate = $('#expiry-date-document-type').val();
        closeDate = $('#close-date-document-type').val();
        note = $('#note-document-type').val();
        //reasonForModification = $('#reason-for-modification-document-type').val();
        PrmKey = 0;
        if (note == '')
            note = 'None';

        //if (reasonForModification == '')
        //    reasonForModification = 'None';

        if (documentId == '' || documentId == 0) {
            result = false;
            $('#document-type-id-error').removeClass('d-none');
        }
        else
            $('#document-type-id-error').addClass('d-none');

        let isValidActivationDate = IsValidInputDate('#activation-date-document-type');

        if (!isValidActivationDate) {
            result = false;
            $('#activation-date-document-type-error').removeClass('d-none');
        } else
            $('#activation-date-document-type-error').addClass('d-none');

        return result;
    }

    function HideColumnsKycDocumentDataTable() {
        debugger;
        KycDataTable.column(1).visible(false);
        KycDataTable.column(6).visible(false);
    }

    function SetDocumentTypeUniqueDropdownList() {
        debugger;
        // Show All List Items
        $('#document-type-id').html('');
        $('#document-type-id').append(DOCUMENT_TYPE_DROPDOWN_LIST);

        // Hide Added Joint Account DropdownList Items
        $('#tbl-kyc-document > tbody > tr').each(function () {
            currentRow = $(this).closest('tr');

            let myColumnValues = (KycDataTable.row(currentRow).data());

            if (typeof myColumnValues != 'undefined' && myColumnValues != null)
                if (myColumnValues[1] != editedDocumentTypeId)
                    $('#document-type-id').find('option[value="' + myColumnValues[1] + '"]').remove();
        });
    }

    /// @@@@@@@@@@@@@@@@@@@@@@   SMS Alert  - DataTable Code @@@@@@@@@@@@@@@@@@@@@@@@@@@

    // ClearFundModalInputs();
    ClearModal('sms-alert');

    // DataTable Add Button 
    $('#btn-add-sms-alert-dt').click(function () {
        debugger;
        event.preventDefault();
        SetModalTitle('sms-alert', 'Add');
    });

    // DataTable Edit Button 
    $('#btn-edit-sms-alert-dt').click(function () {
        debugger;
        SetModalTitle('sms-alert', 'Edit');

        isChecked = $('.checks').is(':checked');
        if (isChecked) {
            columnValues = $('#btn-edit-sms-alert-dt').data('rowindex');
            id = $('#sms-alert-modal').attr('id');
            myModal = $('#' + id).modal();


            let smsAlertActivationDate = GetOnlyDate(columnValues[5]);
            let smsAlertExpiryDate = GetOnlyDate(columnValues[6]);
            let smsAlertCloseDate = GetOnlyDate(columnValues[7]);

            $('#notice-type-id', myModal).val(columnValues[1]);
            $('#maximum-resends-on-failure', myModal).val(columnValues[4]);
            $('#activation-date-notice-type', myModal).val(smsAlertActivationDate);
            $('#expiry-date-notice-type', myModal).val(smsAlertExpiryDate);
            $('#close-date-notice-type', myModal).val(smsAlertCloseDate);
            $('#note-notice-type', myModal).val(columnValues[8]);


            $('#enable-notice-in-regional-language', myModal).val(columnValues[3]);
            if (columnValues[3] === 'True') {
                $('#enable-notice-in-regional-language').prop('checked', true);
            }
            else {
                $('#enable-notice-in-regional-language').prop('checked', false);
            }

            // Show Modals
            myModal.modal({ show: true });
        }
        else {
            $('#btn-edit-sms-alert-edit-dt').addClass('read-only');
            $('#sms-alert-modal').modal('hide');
        }
    });

    // Modal Add Button Event
    $('#btn-add-sms-alert-modal').click(function (event) {
        debugger;
        if (IsValidSmsAlertModal()) {
            row = smsAlertDataTable.row.add([
                tag,
                noticeTypeId,
                noticeTypeIdText,
                enableNoticeInRegionalLanguage,
                maximumResendsOnFailure,
                activationDate,
                expiryDate,
                closeDate,
                note

            ]).draw();

            // Error Message In Span
            $('#sms-alert-data-table-error').addClass('d-none');

            HideColumnsSmsAlertDataTable();

            smsAlertDataTable.columns.adjust().draw();

            ClearModal('sms-alert');

            $('#sms-alert-modal').modal('hide');

            EnableNewOperation('sms-alert');
        }
    });

    // Modal update Button Event
    $('#btn-update-sms-alert-modal').click(function (event) {
        debugger
        $('#select-all-sms-alert').prop('checked', false);
        if (IsValidSmsAlertModal()) {

            smsAlertDataTable.row(selectedRowIndex).data([
                tag,
                noticeTypeId,
                noticeTypeIdText,
                enableNoticeInRegionalLanguage,
                maximumResendsOnFailure,
                activationDate,
                expiryDate,
                closeDate,
                note

            ]).draw();

            HideColumnsSmsAlertDataTable();

            smsAlertDataTable.columns.adjust().draw();

            $('#sms-alert-modal').modal('hide');

            EnableNewOperation('sms-alert');

            scheduletime = [];
        }
    });

    // Modal Delete Button Event
    $('#btn-delete-sms-alert-dt').click(function (event) {
        debugger;
        isChecked = $('input[type="checkbox"]').is(':checked');
        if (isChecked) {
            if (confirm('Are You Sure To Delete This Row?')) {
                if ($('#tbl-sms-alert tbody input[type="checkbox"]:checked').each(function () {
                    smsAlertDataTable.row($('#tbl-sms-alert tbody input[type="checkbox"]:checked').parents('tr').get(0)).remove().draw();

                    rowData = $('#btn-delete-sms-alert-dt').data('rowindex');
                    EnableNewOperation('sms-alert');

                    $('#select-all-sms-alert').prop('checked', false);
                    if (!smsAlertDataTable.data().any())
                    $('#sms-alert-data-table-error').removeClass('d-none');

                }));
            }
        }
        else
            alert('Please Select A Checkbox');
    });

    // Select All Check Box Click Event 
    $('#select-all-sms-alert').click(function () {
        debugger;
        if ($(this).prop('checked')) {
            $('#tbl-sms-alert tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', true);

                row = $(this).closest('tr');

                selectedRowIndex = smsAlertDataTable.row(row).index();

                rowData = (smsAlertDataTable.row(selectedRowIndex).data());

                arr.push({ arrayCloumn1: rowData[1] });

                $('#btn-delete-sms-alert-dt').data('rowindex', arr);
                EnableDeleteOperation('sms-alert');
            });
        }
        else {
            EnableNewOperation('sms-alert');

            $('#tbl-sms-alert tbody input[type="checkbox"]').each(function () {
                $(this).prop('checked', false);
            });
        }
    });

    // Checkbox Click Event - On Checkbox Click For Edit And Delete
    $('#tbl-sms-alert tbody').click('input[type=checkbox]', function () {
        debugger;
        $('#tbl-sms-alert input[type="checkbox"]:checked').each(function (index) {

            isChecked = $(this).prop('checked');

            if (isChecked) {
                let row = $(this).closest('tr');
                selectedRowIndex = smsAlertDataTable.row(row).index();

                if (selectedRowIndex >= 0) {
                    rowData = (smsAlertDataTable.row(selectedRowIndex).data());

                    arr.push({ arrayCloumn1: rowData[1] });

                    EnableEditDeleteOperation('sms-alert');

                    $('#btn-update-sms-alert-dt').attr('rowindex', selectedRowIndex);
                    $('#btn-edit-sms-alert-dt').data('rowindex', rowData);
                    $('#btn-delete-sms-alert-dt').data('rowindex', arr);
                    $('#select-all-sms-alert').data('rowindex', arr);
                }
            }
        });
        // To Check / Uncheck Select All Checkbox And Enable Edit Or Delete Operation
        // getting all the checkboxes within the tbody:
        isCheckedAll = $('#tbl-sms-alert tbody input[type="checkbox"]');

        // getting only the checked checkboxes from that collection:
        checked = isCheckedAll.filter(':checked');

        // Enable New Button If No Any One Checkbox Checked
        if (checked.length == 0)
            EnableNewOperation('sms-alert');

        // Enable Edit & Delete Button If Any One Checkbox Checked
        if (checked.length == 1)
            EnableEditDeleteOperation('sms-alert');

        // Enable Only Delete Button If More Than One Checkbox Checked
        if (checked.length > 1)
            EnableDeleteOperation('sms-alert');

        // Check / Uncheck Select All Checkbox On Check Marking All Checkbox Or Uncheck Mark
        if (checked.length > 0 && isCheckedAll.length === checked.length)
            $('#select-all-sms-alert').prop('checked', true);
        else
            $('#select-all-sms-alert').prop('checked', false);
    });

    // Validate Fund Module
    function IsValidSmsAlertModal() {
        debugger;
        result = true;

        tag = '<input type="checkbox" name="check_all" class="checks"/>';
        noticeTypeId = $('#notice-type-id option:selected').val();
        noticeTypeIdText = $('#notice-type-id option:selected').text();
        enableNoticeInRegionalLanguage = $('input[name="PersonInformationParameterNoticeTypeViewModel.EnableNoticeInRegionalLanguage"]').is(':checked') ? 'True' : "False";
        maximumResendsOnFailure = $('#maximum-resends-on-failure').val();
        activationDate = $('#activation-date-notice-type').val();
        expiryDate = $('#expiry-date-notice-type').val().trim();
        closeDate = $('#close-date-notice-type').val().trim();
        note = $('#note-notice-type').val();
        //reasonForModification = $('#reason-for-modification-notice-type').val();


        // Set Default Value, If Empty
        if (note == '')
            note = 'None';

        //if (hasDivClass == true) {
        //    reasonForModification = 'None';
        //    rvisibility = 0;
        //}
        //else {
        //    rvisibility = 1;
        //    if (reasonForModification == '')
        //        reasonForModification = 'None';
        //}

        if (noticeTypeId == '') {
            result = false;
            $('#notice-type-id-error').removeClass('d-none');
        }
        else
            $('#notice-type-id-error').addClass('d-none');

        if (maximumResendsOnFailure == '') {
            result = false;
            $('#maximum-resends-on-failure-error').removeClass('d-none');
        }
        else
            $('#maximum-resends-on-failure-error').addClass('d-none');

        let isValidActivationDate = IsValidInputDate('#activation-date-notice-type');

        if (!isValidActivationDate) {
            result = false;
            $('#activation-date-notice-type-error').removeClass('d-none');
        } else
            $('#activation-date-notice-type-error').addClass('d-none');

        return result;

    }

    // Hide Unnecessary Columns
    function HideColumnsSmsAlertDataTable() {
        smsAlertDataTable.column(1).visible(false);
        smsAlertDataTable.column(7).visible(false);
    }


    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Set Page Loading Default Values @@@@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Set All Required Default Values On Page Loading
    function SetPageLoadingDefaultValues() {

        if ($('#o-remark').val().length === 0)
            $('#o-remark').val('None');
        // Hide All Accordion Or Div Blocks Based On Toggle Switch
        SetToggleSwitchBasedAccordions();

        SetDocumentUploadInput(ALL);
    }



    // @@@@@@@@@@@@@@@@@@@@@@@@@@@ Handling Save/Submit Click Event @@@@@@@@@@@@@@@@@@@@@@@@@@@


    // Handling Save/Submit Click Event
    $('#btnsave').on('click', function () {
        debugger;
        let isValidAllInputs = true;

        // Validate Inputs Of Full Page 
        if ($('form').valid()) {
            // Not Add event.preventDefault
            $('.lastrow').remove();

            // Accordion 1 - Person Information Number Validation
            if ($('#enable-auto-person-number').is(':checked')) {
                if (!IsValidPersonInformationNumberAccordionInputs())
                    isValidAllInputs = false;
            }
            // Accordion 2 - Photo Sign Validation
            if (!IsValidPhotoSignAccordionInputs())
                isValidAllInputs = false;

            // Accordion 3 - Kyc Document Validation
            if (!IsValidKycDocumentAccordionInputs())
                isValidAllInputs = false;

            // Accordion 4 - Income Tax Validation
            if ($('#enable-income-tax-document-upload').is(':checked')) {
                if (!IsValidIncomeTaxAccordionInputs())
                    isValidAllInputs = false;
            }

            // Accordion 5 - Bank Details Validation
            if ($('#enable-bank-document-upload').is(':checked')) {
                if (!IsValidBankDetailsAccordionInputs())
                    isValidAllInputs = false;
            }
            // Accordion 6 - Financial Asset Validation
            if ($('#enable-financial-asset-document-upload').is(':checked')) {
                if (!IsValidFinancialAssetAccordionInputs())
                    isValidAllInputs = false;
            }

            // Accordion 7 - Movable Asset Validation
            if ($('#enable-movable-asset-document-upload').is(':checked')) {
                if (!IsValidMovableAssetAccordionInputs())
                    isValidAllInputs = false;
            }

            // Accordion 8 - Immovable Asset Validation
            if ($('#enable-immovable-asset-document-upload').is(':checked')) {
                if (!IsValidImmovableAssetAccordionInputs())
                    isValidAllInputs = false;
            }

            // Accordion 9 - Immovable Asset Validation
            if ($('#enable-machinery-asset-document-upload').is(':checked')) {
                if (!IsValidMachineryAssetAccordionInputs())
                    isValidAllInputs = false;
            }
            // Accordion 10 - Agriculture Asset Validation
            if ($('#enable-agriculture-asset-document-upload').is(':checked')) {
                if (!IsValidAgricultureAssetAccordionInputs())
                    isValidAllInputs = false;
            }

            // Accordion 11 - Person Death Validation
            if (!IsValidPersonDeathAssetAccordionInputs())
                isValidAllInputs = false;

            // Accordion 12 - Gst Detail Validation
            if ($('#enable-gst-document-upload').is(':checked')) {
                if (!IsValidGstDetailAccordionInputs())
                    isValidAllInputs = false;
            }
            // Return List Object, Hence Create Array
            let kycDocumentArray = new Array();
            let smsAlertArray = new Array();


            if (!$('#heading-kyc-document-upload').hasClass('d-none')) {
                debugger;
                if (KycDataTable.data().any()) {
                    debugger;
                    $('#kyc-document-accordion-error').addClass('d-none');

                    if (isValidAllInputs) {
                        $('#tbl-kyc-document > tbody > tr').each(function () {
                            currentRow = $(this).closest('tr');

                            columnValues = (KycDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                debugger;
                                kycDocumentArray.push(
                                {
                                    'DocumentTypeId': columnValues[1],
                                    'ActivationDate': columnValues[4],
                                    'ExpiryDate': columnValues[5],
                                    'CloseDate': columnValues[8],
                                    'Note': columnValues[7],
                                    'PersonInfoParameterKYCDocumentPrmKey': columnValues[8]

                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#kyc-document-accordion-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            if ($('#enable-sms-alert').is(':checked')) {
                debugger;
                if (smsAlertDataTable.data().any()) {
                    debugger;
                    $('#sms-alert-data-table-error').addClass('d-none');

                    if (isValidAllInputs) {

                        $('#tbl-sms-alert > tbody > tr').each(function () {
                            debugger;
                            currentRow = $(this).closest('tr');

                            columnValues = (smsAlertDataTable.row(currentRow).data());

                            // Handling Code If Row Is Undefined Or Null
                            if (typeof columnValues != 'undefined' && columnValues != null) {
                                smsAlertArray.push(
                                {
                                    'noticeTypeId': columnValues[1],
                                    'maximumResendsOnFailure': columnValues[4],
                                    'activationDate': columnValues[5],
                                    'Note': columnValues[8],
                                });
                            }
                            else
                                return false;
                        });
                    }
                }
                else {
                    $('#sms-alert-data-table-error').removeClass('d-none');
                    isValidAllInputs = false;
                }
            }

            debugger;
            // Call Cantroller Save Data Table Method 
            if (isValidAllInputs) {
                $.ajax
                ({
                    url: saveDataTableUrl,
                    type: 'POST',
                    data: { '_personInformationParameterDocumentType': kycDocumentArray, '_personInformationParameterNoticeType': smsAlertArray },
                    ContentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    success: function (data) {
                    },
                    error: function (xhr, status, error) {
                        alert('An Error Has Occured In Notice Details DataTable!!! Error Message - ' + error.toString());
                    }
                });
            }
            else {
                // Stop Create Post Method
                alert('Oops, Something Went Wrong!, Please Provide Valid Data');

                event.preventDefault();
            }
        }
        else {
            alert('Oops, Something Went Wrong!, Please Provide Valid Data');
            event.preventDefault();
        }
    });
});