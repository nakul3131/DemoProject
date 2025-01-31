using DemoProject.Services.Constants;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Services.HtmlHelpers
{
    public static class CustomHtmlHelper
    {
        //
        // If User Enter Valid Credentials Then It Provide Unlock Now Link
        // Otherwise It Provide Administrator Contact Details
        public static IHtmlString TroubleshootLink(string _trouble)
        {
            bool IsValidUserPassword = (bool)HttpContext.Current.Session["IsValidUserPassword"];

            if (IsValidUserPassword)
            {
                TagBuilder tagBuilder = new TagBuilder("button");

                if (_trouble == "UnlockUser")
                {
                    tagBuilder.Attributes.Add("class", "btn-lg mt-4 bg-darkgray text-white");
                    tagBuilder.Attributes.Add("id", "unlock");
                    tagBuilder.Attributes.Add("value", "UnLock Now");
                    tagBuilder.Attributes.Add("onclick", "location.href='/Employee/UnlockUserByToken'");
                    tagBuilder.SetInnerText("UnLock Now");
                }

                if (_trouble == "ClearLoginSession")
                {
                    tagBuilder.Attributes.Add("class", "btn-lg btn-danger mt-4 text-white");
                    tagBuilder.Attributes.Add("id", "clear-session");
                    tagBuilder.Attributes.Add("value", "Clear Session");
                    tagBuilder.Attributes.Add("onclick", "location.href='/Employee/ClearRecentLoginSession'");
                    tagBuilder.SetInnerText("Clear Session");
                }

                return new MvcHtmlString(tagBuilder.ToString());
            }
            else
            {
                TagBuilder tagBuilder = new TagBuilder("button");
                tagBuilder.Attributes.Add("class", "btn-lg mt-4 bg-darkgray text-white");
                tagBuilder.Attributes.Add("id", "contact");
                tagBuilder.Attributes.Add("value", "Contact To Administrator");
                tagBuilder.Attributes.Add("onclick", "location.href='/Employee/AdministratorContactDetails'");
                tagBuilder.SetInnerText("Contact");

                return new MvcHtmlString(tagBuilder.ToString());
            }
        }

        //
        // If User Configured Mobile OTP It Provide Input For OTP
        // If User Configured EmailVCode It Provide Input For EmailVCode
        // If User Configured EmailVCode It Provide Input For EmailVCode
        public static MvcHtmlString Token(string _deliveryChannel, string _registeredEmailId, string _registeredMobileNumber, string _smsResponseResult)
        {
            string headerText = "";

            TagBuilder tagBuilderForFormGroupDiv = new TagBuilder("div");
            TagBuilder tagBuilderForFormGroupDiv1 = new TagBuilder("div");
            TagBuilder tagBuilderForHeaderTextParagraph = new TagBuilder("p");
            tagBuilderForHeaderTextParagraph.AddCssClass("pt-3 text-justify");

            // Header Text
            if (_deliveryChannel == StringLiteralValue.Both)
            {
                headerText = "We Have Sent An OTP And Email Verification Code On Your Registered Mobile Number " + _registeredMobileNumber + " And Email Id. " + _registeredEmailId + ". Please Enter It Below To Complete Verification";
            }
            else if (_deliveryChannel == StringLiteralValue.Mobile)
            {
                headerText = "We Have Sent An OTP On Your Registered Mobile Number " + _registeredMobileNumber + ". Please Enter It Below To Complete Verification";
            }
            else if (_deliveryChannel == StringLiteralValue.Email)
            {
                headerText = "We Have Sent An Email Verification Code On Your Registered Email ID." + _registeredEmailId + "Please Enter It Below To Complete Verification";
            }

            tagBuilderForHeaderTextParagraph.SetInnerText(headerText);

            // Resend Link and Mobile OTP
            if ((_deliveryChannel == StringLiteralValue.Both) || (_deliveryChannel == StringLiteralValue.Mobile))
            {
                tagBuilderForFormGroupDiv.AddCssClass("form-group pt-3 ");

                TagBuilder tagBuilderForClearFixDiv = new TagBuilder("div");
                tagBuilderForClearFixDiv.AddCssClass("clearfix mt-2");

                TagBuilder tagBuilderForFloatLeftDiv = new TagBuilder("div");
                tagBuilderForFloatLeftDiv.AddCssClass("float-left ");


                TagBuilder tagBuilderForMobileIcon = new TagBuilder("i");
                tagBuilderForMobileIcon.AddCssClass("fas fa-mobile-alt");

                TagBuilder tagBuilderForMobileOTPLabel = new TagBuilder("label");
                tagBuilderForMobileOTPLabel.AddCssClass("float-left font-weight-bold ml-1 text-dark");
                tagBuilderForMobileOTPLabel.SetInnerText("Mobile OTP/मोबाइल OTP");

                TagBuilder tagBuilderForDisplayPropertyDiv = new TagBuilder("div");
                tagBuilderForDisplayPropertyDiv.AddCssClass("d-none d-md-block");

                TagBuilder tagBuilderForFloatRightDiv = new TagBuilder("div");
                tagBuilderForFloatRightDiv.AddCssClass("float-right ");

                TagBuilder tagBuilderForAlertDiv = new TagBuilder("div");
                tagBuilderForAlertDiv.AddCssClass("link float-right text-primary font-weight-bold");



                TagBuilder tagBuilderForResendButtonLink = new TagBuilder("button");
                tagBuilderForAlertDiv.MergeAttribute("type", "button");
                tagBuilderForResendButtonLink.AddCssClass("btn btn-link sans-serif-family float-right");
                tagBuilderForAlertDiv.MergeAttribute("style", "cursor: pointer;");
                tagBuilderForAlertDiv.MergeAttribute("id", "close");
                tagBuilderForAlertDiv.MergeAttribute("data-dismiss", "alert");
                tagBuilderForAlertDiv.MergeAttribute("href", "javascript:void(0)");
                tagBuilderForAlertDiv.MergeAttribute("onclick", "ResendSMS()");
                tagBuilderForAlertDiv.SetInnerText("Resend");

                TagBuilder tagBuilderForToastDiv = new TagBuilder("div");
                tagBuilderForToastDiv.AddCssClass("toast text-center ");
                tagBuilderForToastDiv.MergeAttribute("style", "z-index:10; max-width:100%;margin-top:-50%;margin-left:20%;position: absolute;");
                tagBuilderForToastDiv.MergeAttribute("id", "myToast");
                tagBuilderForToastDiv.MergeAttribute("role", "alert");
                tagBuilderForToastDiv.MergeAttribute("data-delay", "4000");
                tagBuilderForToastDiv.MergeAttribute("data-animation", "true");
                tagBuilderForToastDiv.MergeAttribute("aria-live", "assertive");
                tagBuilderForToastDiv.MergeAttribute("data-autohide", "true");

                TagBuilder tagBuilderForToastBodyDiv = new TagBuilder("div");
                if (_smsResponseResult == "success")
                {
                    tagBuilderForToastBodyDiv.AddCssClass("toast-body bg-success text-white");
                }
                else
                {
                    tagBuilderForToastBodyDiv.AddCssClass("toast-body bg-danger text-white");
                }

                TagBuilder tagBuilderForToastIconDiv = new TagBuilder("div");
                tagBuilderForToastIconDiv.AddCssClass("toast-icon toast-h6");

                TagBuilder tagBuilderForRowDiv = new TagBuilder("div");
                tagBuilderForRowDiv.AddCssClass("row");

                TagBuilder tagBuilderForCol2Div = new TagBuilder("div");
                tagBuilderForCol2Div.AddCssClass("col-md-2");

                TagBuilder tagBuilderForCheckMarkIcon = new TagBuilder("i");
                if (_smsResponseResult == "success")
                {
                    tagBuilderForCheckMarkIcon.AddCssClass("fas fa-check text-white");
                }
                else
                {
                    tagBuilderForCheckMarkIcon.AddCssClass("fas fa-times text -white");
                }
                tagBuilderForCheckMarkIcon.MergeAttribute("aria-hidden", "true");

                TagBuilder tagBuilderForCol10Div = new TagBuilder("div");
                tagBuilderForCol10Div.AddCssClass("col-md-10");

                TagBuilder tagBuilderStrongText = new TagBuilder("strong");

                if (_smsResponseResult == "success")
                {
                    tagBuilderStrongText.SetInnerText("Sms Send Successfully");
                }
                else
                {
                    tagBuilderStrongText.SetInnerText("Sms Sending Failed");
                }

                // Mobile OTP - Input
                TagBuilder tagBuilderForOTPInput = new TagBuilder("input");
                tagBuilderForOTPInput.AddCssClass("form-control mandatory-mark deny-multiple-space");
                tagBuilderForOTPInput.MergeAttribute("type", "text");
                tagBuilderForOTPInput.MergeAttribute("Id", "MobileOTP");
                tagBuilderForOTPInput.MergeAttribute("Name", "MobileOTP");
                tagBuilderForOTPInput.MergeAttribute("placeholder", "Enter Mobile OTP");
                tagBuilderForOTPInput.MergeAttribute("autocomplete", "off");
                tagBuilderForOTPInput.MergeAttribute("required", "required");

                // Validation Message For Mobile OTP
                TagBuilder tagBuilderForOTPValidationMessage = new TagBuilder("span");
                tagBuilderForOTPValidationMessage.AddCssClass("field-validation-valid");
                tagBuilderForOTPValidationMessage.MergeAttribute("data-valmsg-for", "MobileOTP");
                tagBuilderForOTPValidationMessage.MergeAttribute("data-valmsg-replace", "true");

                // Bind All Div
                tagBuilderForFloatLeftDiv.InnerHtml += tagBuilderForMobileIcon;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForFloatLeftDiv;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForMobileOTPLabel;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForDisplayPropertyDiv;


                tagBuilderForFloatRightDiv.InnerHtml += tagBuilderForAlertDiv;
                tagBuilderForDisplayPropertyDiv.InnerHtml += tagBuilderForAlertDiv;
                tagBuilderForCol2Div.InnerHtml += tagBuilderForCheckMarkIcon;

                tagBuilderForCol10Div.InnerHtml += tagBuilderStrongText;
                tagBuilderForRowDiv.InnerHtml += tagBuilderForCol2Div;
                tagBuilderForRowDiv.InnerHtml += tagBuilderForCol10Div;
                tagBuilderForToastIconDiv.InnerHtml += tagBuilderForRowDiv;
                tagBuilderForToastBodyDiv.InnerHtml += tagBuilderForToastIconDiv;
                tagBuilderForToastDiv.InnerHtml += tagBuilderForToastBodyDiv;

                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForDisplayPropertyDiv;
                tagBuilderForFormGroupDiv.InnerHtml += tagBuilderForToastDiv;

                tagBuilderForFormGroupDiv.InnerHtml += tagBuilderForClearFixDiv;
                tagBuilderForFormGroupDiv.InnerHtml += tagBuilderForOTPInput;
                tagBuilderForFormGroupDiv.InnerHtml += tagBuilderForOTPValidationMessage;

            }

            // Resend Link and Email Verification Code
            if ((_deliveryChannel == StringLiteralValue.Both) || (_deliveryChannel == StringLiteralValue.Email))
            {
                tagBuilderForFormGroupDiv1.AddCssClass("form-group pt-3");

                TagBuilder tagBuilderForClearFixDiv = new TagBuilder("div");
                tagBuilderForClearFixDiv.AddCssClass("clearfix");

                TagBuilder tagBuilderForFloatLeftDiv = new TagBuilder("div");
                tagBuilderForClearFixDiv.AddCssClass("float-left");

                TagBuilder tagBuilderForMobileIcon = new TagBuilder("i");
                tagBuilderForMobileIcon.AddCssClass("fas fa-mobile-alt");

                TagBuilder tagBuilderForMobileOTPLabel = new TagBuilder("label");
                tagBuilderForMobileOTPLabel.AddCssClass("float-left font-weight-bold ml-1");
                tagBuilderForMobileOTPLabel.SetInnerText("Mobile OTP/मोबाइल OTP");

                TagBuilder tagBuilderForDisplayPropertyDiv = new TagBuilder("div");
                tagBuilderForDisplayPropertyDiv.AddCssClass("d-none d-md-block");

                TagBuilder tagBuilderForAlertDiv = new TagBuilder("div");
                tagBuilderForAlertDiv.AddCssClass("alert alert-dismissable");
                tagBuilderForAlertDiv.MergeAttribute("id", "alert");
                tagBuilderForAlertDiv.MergeAttribute("role", "alert");
                tagBuilderForAlertDiv.MergeAttribute("aria-atomic", "false");

                TagBuilder tagBuilderForResendButtonLink = new TagBuilder("button");
                tagBuilderForAlertDiv.MergeAttribute("type", "button");
                tagBuilderForResendButtonLink.AddCssClass("btn btn-link float-right sans-serif-family");
                tagBuilderForAlertDiv.MergeAttribute("id", "close");
                tagBuilderForAlertDiv.MergeAttribute("data-dismiss", "alert");
                tagBuilderForAlertDiv.MergeAttribute("href", "javascript:void(0)");
                tagBuilderForAlertDiv.MergeAttribute("onclick", "ResendSMS()");
                tagBuilderForAlertDiv.SetInnerText("Resend");

                TagBuilder tagBuilderForToastDiv = new TagBuilder("div");
                tagBuilderForToastDiv.AddCssClass("toast text-center");
                tagBuilderForToastDiv.MergeAttribute("id", "myToast");
                tagBuilderForToastDiv.MergeAttribute("role", "alert");
                tagBuilderForToastDiv.MergeAttribute("data-pause", "true");
                tagBuilderForToastDiv.MergeAttribute("data-delay", "3000");
                tagBuilderForToastDiv.MergeAttribute("data-animation", "true");
                tagBuilderForToastDiv.MergeAttribute("aria-live", "assertive");
                tagBuilderForToastDiv.MergeAttribute("aria-atomic", "true");

                TagBuilder tagBuilderForToastBodyDiv = new TagBuilder("div");
                if (_smsResponseResult == "Success")
                {
                    tagBuilderForToastBodyDiv.AddCssClass("toast-body bg-success text-white");
                }
                else
                {
                    tagBuilderForToastBodyDiv.AddCssClass("toast-body bg-danger text-white");
                }

                TagBuilder tagBuilderForToastIconDiv = new TagBuilder("div");
                tagBuilderForToastIconDiv.AddCssClass("toast-icon toast-h6");

                TagBuilder tagBuilderForRowDiv = new TagBuilder("div");
                tagBuilderForRowDiv.AddCssClass("row");

                TagBuilder tagBuilderForCol2Div = new TagBuilder("div");
                tagBuilderForCol2Div.AddCssClass("col-md-2");

                TagBuilder tagBuilderForCheckMarkIcon = new TagBuilder("i");
                if (_smsResponseResult == "success")
                {
                    tagBuilderForCheckMarkIcon.AddCssClass("fas fa-check text-white");
                }
                else
                {
                    tagBuilderForCheckMarkIcon.AddCssClass("fas fa-exclamation text -white");
                }
                tagBuilderForCheckMarkIcon.MergeAttribute("aria-hidden", "true");

                TagBuilder tagBuilderForCol10Div = new TagBuilder("div");
                tagBuilderForCol10Div.AddCssClass("col-md-10");

                TagBuilder tagBuilderStrongText = new TagBuilder("strong");
                tagBuilderStrongText.AddCssClass("ml-1");
                if (_smsResponseResult == "success")
                {
                    tagBuilderStrongText.SetInnerText("Send Successfully");
                }
                else
                {
                    tagBuilderStrongText.SetInnerText("Sending Failed");
                }

                // Mobile OTP - Input
                TagBuilder tagBuilderForOTPInput = new TagBuilder("input");
                tagBuilderForOTPInput.AddCssClass("form-control mandatory-mark deny-multiple-space");
                tagBuilderForOTPInput.MergeAttribute("type", "text");
                tagBuilderForOTPInput.MergeAttribute("Id", "EmailVCode");
                tagBuilderForOTPInput.MergeAttribute("Name", "EmailVCode");
                tagBuilderForOTPInput.MergeAttribute("placeholder", "Enter Email Verification Code");
                tagBuilderForOTPInput.MergeAttribute("autocomplete", "off");
                tagBuilderForOTPInput.MergeAttribute("required", "required");

                // Validation Message For Mobile OTP
                TagBuilder tagBuilderForOTPValidationMessage = new TagBuilder("span");
                tagBuilderForOTPValidationMessage.AddCssClass("field-validation-valid");
                tagBuilderForOTPValidationMessage.MergeAttribute("data-valmsg-for", "EmailVCode");
                tagBuilderForOTPValidationMessage.MergeAttribute("data-valmsg-replace", "true");

                // Bind All Div
                tagBuilderForFloatLeftDiv.InnerHtml += tagBuilderForMobileIcon;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForFloatLeftDiv;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForMobileOTPLabel;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForDisplayPropertyDiv;
                tagBuilderForAlertDiv.InnerHtml += tagBuilderForResendButtonLink;
                tagBuilderForDisplayPropertyDiv.InnerHtml += tagBuilderForAlertDiv;
                tagBuilderForCol2Div.InnerHtml += tagBuilderForCheckMarkIcon;
                tagBuilderForCol10Div.InnerHtml += tagBuilderStrongText;
                tagBuilderForRowDiv.InnerHtml += tagBuilderForCol2Div;
                tagBuilderForRowDiv.InnerHtml += tagBuilderForCol10Div;
                tagBuilderForToastIconDiv.InnerHtml += tagBuilderForRowDiv;
                tagBuilderForToastBodyDiv.InnerHtml += tagBuilderForToastIconDiv;
                tagBuilderForToastDiv.InnerHtml += tagBuilderForToastBodyDiv;



                tagBuilderForFormGroupDiv1.InnerHtml += tagBuilderForClearFixDiv;
                tagBuilderForFormGroupDiv1.InnerHtml += tagBuilderForOTPInput;
                tagBuilderForFormGroupDiv1.InnerHtml += tagBuilderForOTPValidationMessage;

            }

            if (_deliveryChannel == StringLiteralValue.Mobile)
            {
                return new MvcHtmlString(tagBuilderForHeaderTextParagraph.ToString() + tagBuilderForFormGroupDiv.ToString());
            }
            else if (_deliveryChannel == StringLiteralValue.Email)
            {
                return new MvcHtmlString(tagBuilderForHeaderTextParagraph.ToString() + tagBuilderForFormGroupDiv1.ToString());
            }
            else
            {
                return new MvcHtmlString(tagBuilderForHeaderTextParagraph.ToString() + tagBuilderForFormGroupDiv.ToString() + tagBuilderForFormGroupDiv1.ToString());
            }
        }


        //
        // If User Configured Mobile OTP It Provide Input For OTP
        // If User Configured EmailVCode It Provide Input For EmailVCode
        // If User Configured EmailVCode It Provide Input For EmailVCode
        public static MvcHtmlString MobileVerificationToken(string _deliveryChannel, string _smsResponseResult)
        {
            TagBuilder tagBuilderForFormGroupDiv = new TagBuilder("div");
            TagBuilder tagBuilderForFormGroupDiv1 = new TagBuilder("div");

            // Resend Link and Mobile OTP
            if ((_deliveryChannel == StringLiteralValue.Both) || (_deliveryChannel == StringLiteralValue.Mobile))
            {
                tagBuilderForFormGroupDiv.AddCssClass("form-group pt-3");

                TagBuilder tagBuilderForClearFixDiv = new TagBuilder("div");
                tagBuilderForClearFixDiv.AddCssClass("clearfix mt-2");

                TagBuilder tagBuilderForFloatLeftDiv = new TagBuilder("div");
                tagBuilderForFloatLeftDiv.AddCssClass("float-left ");


                TagBuilder tagBuilderForMobileIcon = new TagBuilder("i");
                tagBuilderForMobileIcon.AddCssClass("fas fa-mobile-alt");

                TagBuilder tagBuilderForVerificationCodeLabel = new TagBuilder("label");
                tagBuilderForVerificationCodeLabel.AddCssClass("float-left font-weight-bold ml-1 text-dark");
                tagBuilderForVerificationCodeLabel.SetInnerText("Mobile OTP/मोबाइल OTP");

                TagBuilder tagBuilderForDisplayPropertyDiv = new TagBuilder("div");
                tagBuilderForDisplayPropertyDiv.AddCssClass("d-none d-md-block");

                TagBuilder tagBuilderForFloatRightDiv = new TagBuilder("div");
                tagBuilderForFloatRightDiv.AddCssClass("float-right ");

                TagBuilder tagBuilderForAlertDiv = new TagBuilder("div");
                tagBuilderForAlertDiv.AddCssClass("link float-right text-primary font-weight-bold");



                TagBuilder tagBuilderForResendButtonLink = new TagBuilder("button");
                tagBuilderForAlertDiv.MergeAttribute("type", "button");
                tagBuilderForResendButtonLink.AddCssClass("btn btn-link sans-serif-family float-right");
                tagBuilderForAlertDiv.MergeAttribute("style", "cursor: pointer;");
                tagBuilderForAlertDiv.MergeAttribute("id", "resend");
                tagBuilderForAlertDiv.MergeAttribute("data-dismiss", "alert");
                tagBuilderForAlertDiv.MergeAttribute("href", "javascript:void(0)");
                tagBuilderForAlertDiv.MergeAttribute("onclick", "ResendSMS()");
                tagBuilderForAlertDiv.SetInnerText("Resend");

                TagBuilder tagBuilderForToastDiv = new TagBuilder("div");
                tagBuilderForToastDiv.AddCssClass("toast text-center ");
                tagBuilderForToastDiv.MergeAttribute("style", "z-index:10; max-width:100%;margin-top:-50%;margin-left:20%;position: absolute;");
                tagBuilderForToastDiv.MergeAttribute("id", "myToast");
                tagBuilderForToastDiv.MergeAttribute("role", "alert");
                tagBuilderForToastDiv.MergeAttribute("data-delay", "4000");
                tagBuilderForToastDiv.MergeAttribute("data-animation", "true");
                tagBuilderForToastDiv.MergeAttribute("aria-live", "assertive");
                tagBuilderForToastDiv.MergeAttribute("data-autohide", "true");

                TagBuilder tagBuilderForToastBodyDiv = new TagBuilder("div");
                tagBuilderForToastBodyDiv.MergeAttribute("id", "t-body");
                if (_smsResponseResult == "success")
                {
                    tagBuilderForToastBodyDiv.AddCssClass("toast-body bg-success text-white");
                }
                else
                {
                    tagBuilderForToastBodyDiv.AddCssClass("toast-body bg-danger text-white");
                }

                TagBuilder tagBuilderForToastIconDiv = new TagBuilder("div");
                tagBuilderForToastIconDiv.AddCssClass("toast-icon toast-h6");

                TagBuilder tagBuilderForRowDiv = new TagBuilder("div");
                tagBuilderForRowDiv.AddCssClass("row");

                TagBuilder tagBuilderForCol2Div = new TagBuilder("div");
                tagBuilderForCol2Div.AddCssClass("col-md-2");

                TagBuilder tagBuilderForCheckMarkIcon = new TagBuilder("i");
                tagBuilderForCheckMarkIcon.MergeAttribute("id", "t-icon");

                if (_smsResponseResult == "success")
                    tagBuilderForCheckMarkIcon.AddCssClass("fas fa-check text-white");
                else
                    tagBuilderForCheckMarkIcon.AddCssClass("fas fa-times text -white");

                tagBuilderForCheckMarkIcon.MergeAttribute("aria-hidden", "true");

                TagBuilder tagBuilderForCol10Div = new TagBuilder("div");
                tagBuilderForCol10Div.AddCssClass("col-md-10");

                TagBuilder tagBuilderStrongText = new TagBuilder("strong");
                tagBuilderStrongText.MergeAttribute("id", "t-text");

                if (_smsResponseResult == "success")
                {
                    tagBuilderStrongText.SetInnerText("Sms Send Successfully");
                }
                else
                {
                    tagBuilderStrongText.SetInnerText("Sms Sending Failed");
                }

                // Mobile OTP - Input
                TagBuilder tagBuilderForOTPInput = new TagBuilder("input");
                tagBuilderForOTPInput.AddCssClass("form-control mandatory-mark deny-multiple-space");
                tagBuilderForOTPInput.MergeAttribute("type", "text");
                tagBuilderForOTPInput.MergeAttribute("Id", "verification-code");
                tagBuilderForOTPInput.MergeAttribute("Name", "VerificationCode");
                tagBuilderForOTPInput.MergeAttribute("placeholder", "Enter Mobile OTP");
                tagBuilderForOTPInput.MergeAttribute("autocomplete", "off");
                tagBuilderForOTPInput.MergeAttribute("required", "required");

                // Validation Message For Mobile OTP
                TagBuilder tagBuilderForOTPValidationMessage = new TagBuilder("span");
                tagBuilderForOTPValidationMessage.AddCssClass("field-validation-valid");
                tagBuilderForOTPValidationMessage.MergeAttribute("data-valmsg-for", "VerificationCode");
                tagBuilderForOTPValidationMessage.MergeAttribute("data-valmsg-replace", "true");

                // Bind All Div
                tagBuilderForFloatLeftDiv.InnerHtml += tagBuilderForMobileIcon;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForFloatLeftDiv;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForVerificationCodeLabel;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForDisplayPropertyDiv;


                tagBuilderForFloatRightDiv.InnerHtml += tagBuilderForAlertDiv;
                tagBuilderForDisplayPropertyDiv.InnerHtml += tagBuilderForAlertDiv;
                tagBuilderForCol2Div.InnerHtml += tagBuilderForCheckMarkIcon;

                tagBuilderForCol10Div.InnerHtml += tagBuilderStrongText;
                tagBuilderForRowDiv.InnerHtml += tagBuilderForCol2Div;
                tagBuilderForRowDiv.InnerHtml += tagBuilderForCol10Div;
                tagBuilderForToastIconDiv.InnerHtml += tagBuilderForRowDiv;
                tagBuilderForToastBodyDiv.InnerHtml += tagBuilderForToastIconDiv;
                tagBuilderForToastDiv.InnerHtml += tagBuilderForToastBodyDiv;

                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForDisplayPropertyDiv;
                tagBuilderForFormGroupDiv.InnerHtml += tagBuilderForToastDiv;

                tagBuilderForFormGroupDiv.InnerHtml += tagBuilderForClearFixDiv;
                tagBuilderForFormGroupDiv.InnerHtml += tagBuilderForOTPInput;
                tagBuilderForFormGroupDiv.InnerHtml += tagBuilderForOTPValidationMessage;

            }

            // Resend Link and Email Verification Code
            if ((_deliveryChannel == StringLiteralValue.Both) || (_deliveryChannel == StringLiteralValue.Email))
            {
                tagBuilderForFormGroupDiv1.AddCssClass("form-group pt-3 deny-multiple-space");

                TagBuilder tagBuilderForClearFixDiv = new TagBuilder("div");
                tagBuilderForClearFixDiv.AddCssClass("clearfix");

                TagBuilder tagBuilderForFloatLeftDiv = new TagBuilder("div");
                tagBuilderForClearFixDiv.AddCssClass("float-left");

                TagBuilder tagBuilderForMobileIcon = new TagBuilder("i");
                tagBuilderForMobileIcon.AddCssClass("fas fa-mobile-alt");

                TagBuilder tagBuilderForVerificationCodeLabel = new TagBuilder("label");
                tagBuilderForVerificationCodeLabel.AddCssClass("float-left font-weight-bold ml-1");
                tagBuilderForVerificationCodeLabel.SetInnerText("Mobile OTP");

                TagBuilder tagBuilderForDisplayPropertyDiv = new TagBuilder("div");
                tagBuilderForDisplayPropertyDiv.AddCssClass("d-none d-md-block");

                TagBuilder tagBuilderForAlertDiv = new TagBuilder("div");
                tagBuilderForAlertDiv.AddCssClass("alert alert-dismissable");
                tagBuilderForAlertDiv.MergeAttribute("id", "alert");
                tagBuilderForAlertDiv.MergeAttribute("role", "alert");
                tagBuilderForAlertDiv.MergeAttribute("aria-atomic", "false");

                TagBuilder tagBuilderForResendButtonLink = new TagBuilder("button");
                tagBuilderForAlertDiv.MergeAttribute("type", "button");
                tagBuilderForResendButtonLink.AddCssClass("btn btn-link float-right sans-serif-family");
                tagBuilderForAlertDiv.MergeAttribute("id", "resend");
                tagBuilderForAlertDiv.MergeAttribute("data-dismiss", "alert");
                tagBuilderForAlertDiv.MergeAttribute("href", "javascript:void(0)");
                tagBuilderForAlertDiv.MergeAttribute("onclick", "ResendSMS()");
                tagBuilderForAlertDiv.SetInnerText("Resend");

                TagBuilder tagBuilderForToastDiv = new TagBuilder("div");
                tagBuilderForToastDiv.AddCssClass("toast text-center");
                tagBuilderForToastDiv.MergeAttribute("id", "myToast");
                tagBuilderForToastDiv.MergeAttribute("role", "alert");
                tagBuilderForToastDiv.MergeAttribute("data-pause", "true");
                tagBuilderForToastDiv.MergeAttribute("data-delay", "3000");
                tagBuilderForToastDiv.MergeAttribute("data-animation", "true");
                tagBuilderForToastDiv.MergeAttribute("aria-live", "assertive");
                tagBuilderForToastDiv.MergeAttribute("aria-atomic", "true");

                TagBuilder tagBuilderForToastBodyDiv = new TagBuilder("div");
                if (_smsResponseResult == "Success")
                {
                    tagBuilderForToastBodyDiv.AddCssClass("toast-body bg-success text-white");
                }
                else
                {
                    tagBuilderForToastBodyDiv.AddCssClass("toast-body bg-danger text-white");
                }

                TagBuilder tagBuilderForToastIconDiv = new TagBuilder("div");
                tagBuilderForToastIconDiv.AddCssClass("toast-icon toast-h6");

                TagBuilder tagBuilderForRowDiv = new TagBuilder("div");
                tagBuilderForRowDiv.AddCssClass("row");

                TagBuilder tagBuilderForCol2Div = new TagBuilder("div");
                tagBuilderForCol2Div.AddCssClass("col-md-2");

                TagBuilder tagBuilderForCheckMarkIcon = new TagBuilder("i");
                if (_smsResponseResult == "success")
                {
                    tagBuilderForCheckMarkIcon.AddCssClass("fas fa-check text-white");
                }
                else
                {
                    tagBuilderForCheckMarkIcon.AddCssClass("fas fa-exclamation text -white");
                }

                tagBuilderForCheckMarkIcon.MergeAttribute("aria-hidden", "true");

                TagBuilder tagBuilderForCol10Div = new TagBuilder("div");
                tagBuilderForCol10Div.AddCssClass("col-md-10");

                TagBuilder tagBuilderStrongText = new TagBuilder("strong");

                tagBuilderStrongText.AddCssClass("ml-1");
                if (_smsResponseResult == "success")
                {
                    tagBuilderStrongText.SetInnerText("Send Successfully");
                }
                else
                {
                    tagBuilderStrongText.SetInnerText("Sending Failed");
                }

                // Mobile OTP - Input
                TagBuilder tagBuilderForOTPInput = new TagBuilder("input");
                tagBuilderForOTPInput.AddCssClass("form-control mandatory-mark deny-multiple-space");
                tagBuilderForOTPInput.MergeAttribute("type", "text");
                tagBuilderForOTPInput.MergeAttribute("Id", "EmailVCode");
                tagBuilderForOTPInput.MergeAttribute("Name", "EmailVCode");
                tagBuilderForOTPInput.MergeAttribute("placeholder", "Enter Email Verification Code");
                tagBuilderForOTPInput.MergeAttribute("autocomplete", "off");

                // Validation Message For Mobile OTP
                TagBuilder tagBuilderForOTPValidationMessage = new TagBuilder("span");
                tagBuilderForOTPValidationMessage.AddCssClass("field-validation-valid");
                tagBuilderForOTPValidationMessage.MergeAttribute("data-valmsg-for", "EmailVCode");
                tagBuilderForOTPValidationMessage.MergeAttribute("data-valmsg-replace", "true");

                // Bind All Div
                tagBuilderForFloatLeftDiv.InnerHtml += tagBuilderForMobileIcon;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForFloatLeftDiv;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForVerificationCodeLabel;
                tagBuilderForClearFixDiv.InnerHtml += tagBuilderForDisplayPropertyDiv;
                tagBuilderForAlertDiv.InnerHtml += tagBuilderForResendButtonLink;
                tagBuilderForDisplayPropertyDiv.InnerHtml += tagBuilderForAlertDiv;
                tagBuilderForCol2Div.InnerHtml += tagBuilderForCheckMarkIcon;
                tagBuilderForCol10Div.InnerHtml += tagBuilderStrongText;
                tagBuilderForRowDiv.InnerHtml += tagBuilderForCol2Div;
                tagBuilderForRowDiv.InnerHtml += tagBuilderForCol10Div;
                tagBuilderForToastIconDiv.InnerHtml += tagBuilderForRowDiv;
                tagBuilderForToastBodyDiv.InnerHtml += tagBuilderForToastIconDiv;
                tagBuilderForToastDiv.InnerHtml += tagBuilderForToastBodyDiv;



                tagBuilderForFormGroupDiv1.InnerHtml += tagBuilderForClearFixDiv;
                tagBuilderForFormGroupDiv1.InnerHtml += tagBuilderForOTPInput;
                tagBuilderForFormGroupDiv1.InnerHtml += tagBuilderForOTPValidationMessage;

            }

            if (_deliveryChannel == StringLiteralValue.Mobile)
            {
                return new MvcHtmlString(tagBuilderForFormGroupDiv.ToString());
            }
            else if (_deliveryChannel == StringLiteralValue.Email)
            {
                return new MvcHtmlString(tagBuilderForFormGroupDiv1.ToString());
            }
            else
            {
                return new MvcHtmlString(tagBuilderForFormGroupDiv.ToString() + tagBuilderForFormGroupDiv1.ToString());
            }
        }

    }
}
