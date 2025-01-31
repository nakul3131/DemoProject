using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using DemoProject.Services.ViewModel.Configuration;

namespace DemoProject.Services.HtmlHelpers
{
    public static class HtmlHelperExtension
    {
        // If User Enter InValid password Then It Provide Forgot Passwor Action Link
        // Otherwise It Provide Empty string                                                    
        public static MvcHtmlString CustomActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, bool? _isVisible)
        {
            if (_isVisible != null && _isVisible == true)
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName);
            }
            
            return MvcHtmlString.Empty;
        }

        //
        // User Accessible Menus
        public static IHtmlString DashBoardMenu(this HtmlHelper htmlHelper, List<MenuViewModel> Model)
        {
            // Create UrlHelper Objct For generate link
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            // Tag Builder For Unordered List For Menu
            TagBuilder tagBuilderForMenuUnOrderedList = new TagBuilder("ul");

            var memoryCache = MemoryCache.Default;

            if (!memoryCache.Contains("section"))
            {
                tagBuilderForMenuUnOrderedList.AddCssClass("navbar-nav");
                {
                    int mainMenuPrmKey = -1;
                    int subMainMenuPrmKey = -1;

                    foreach (var dr in Model)
                    {
                        // Tag Builder For List Item For Menu
                        TagBuilder tagBuilderForMenuListItem = new TagBuilder("li");
                        tagBuilderForMenuListItem.AddCssClass("sidebar-dropdown nav-item");

                        // Check For Main Menu
                        if (dr.ParentMenuPrmKey == 0)
                        {
                            TagBuilder tagBuilderForMenuAnchor = new TagBuilder("a");
                            tagBuilderForMenuAnchor.MergeAttribute("href", "javascript:void(0)");
                            tagBuilderForMenuAnchor.MergeAttribute("onclick", "window.location.href='#'");
                            tagBuilderForMenuAnchor.MergeAttribute("class", dr.MenuPrmKey.ToString() + " mt-2");
                            tagBuilderForMenuAnchor.MergeAttribute("id", "nav-link");

                            TagBuilder tagBuilderForMenuAnchorIcon = new TagBuilder("i");
                            tagBuilderForMenuAnchorIcon.AddCssClass("i-medium");
                            tagBuilderForMenuAnchorIcon.AddCssClass(dr.IconImageClass);


                            TagBuilder tagBuilderForMenuSpan = new TagBuilder("span");
                            tagBuilderForMenuSpan.AddCssClass("font-weight-bold pl-3");
                            tagBuilderForMenuSpan.SetInnerText(dr.NameOfMenu);

                            tagBuilderForMenuAnchor.InnerHtml += tagBuilderForMenuAnchorIcon;
                            tagBuilderForMenuAnchor.InnerHtml += tagBuilderForMenuSpan;

                            tagBuilderForMenuListItem.InnerHtml += tagBuilderForMenuAnchor;

                            mainMenuPrmKey = dr.MenuPrmKey;
                        }

                        // Check For SubMenu / Parent Menu
                        if (mainMenuPrmKey == dr.ParentMenuPrmKey)
                        {
                            TagBuilder tagBuilderForSubMenuDiv = new TagBuilder("div");
                            tagBuilderForSubMenuDiv.AddCssClass("sidebar-submenu submenu");
                            tagBuilderForSubMenuDiv.AddCssClass(dr.ParentMenuPrmKey.ToString());

                            TagBuilder tagBuilderForSubMenuUnOrderedList = new TagBuilder("ul");
                            tagBuilderForSubMenuUnOrderedList.AddCssClass("navbar-nav ml-4");

                            TagBuilder tagBuilderForSubMenuListItem = new TagBuilder("li");
                            tagBuilderForSubMenuListItem.AddCssClass("sidebar-dropdown nav-item");

                            TagBuilder tagBuilderForSubMenuAnchor = new TagBuilder("a");
                            tagBuilderForSubMenuAnchor.MergeAttribute("href", "javascript:void(0)");
                            tagBuilderForSubMenuAnchor.AddCssClass(dr.MenuPrmKey.ToString());
                            tagBuilderForSubMenuAnchor.MergeAttribute("id", "nav-link");
                            tagBuilderForSubMenuAnchor.MergeAttribute("onclick", "LoadPage('" + dr.NameOfController + "','" + dr.NameOfActionMethod + "')");

                            TagBuilder tagBuilderForSubMenuAnchorIcon = new TagBuilder("i");
                            tagBuilderForSubMenuAnchorIcon.AddCssClass("i-medium");
                            tagBuilderForSubMenuAnchorIcon.AddCssClass(dr.IconImageClass);

                            TagBuilder tagBuilderForSubMenuSpan = new TagBuilder("span");
                            tagBuilderForSubMenuSpan.AddCssClass("pl-3");
                            tagBuilderForSubMenuSpan.SetInnerText(dr.NameOfMenu);

                            tagBuilderForSubMenuAnchor.InnerHtml += tagBuilderForSubMenuAnchorIcon;
                            tagBuilderForSubMenuAnchor.InnerHtml += tagBuilderForSubMenuSpan;

                            tagBuilderForSubMenuListItem.InnerHtml += tagBuilderForSubMenuAnchor;

                            tagBuilderForSubMenuUnOrderedList.InnerHtml += tagBuilderForSubMenuListItem;

                            tagBuilderForSubMenuDiv.InnerHtml += tagBuilderForSubMenuUnOrderedList;

                            tagBuilderForMenuListItem.InnerHtml += tagBuilderForSubMenuDiv;

                            subMainMenuPrmKey = dr.MenuPrmKey;
                        }

                        // Check For Child Menu 
                        if (subMainMenuPrmKey == dr.ParentMenuPrmKey)
                        {
                            TagBuilder tagBuilderForChildMenuDiv = new TagBuilder("div");
                            tagBuilderForChildMenuDiv.AddCssClass("sidebar-submenu subsubmenu");
                            tagBuilderForChildMenuDiv.AddCssClass(dr.ParentMenuPrmKey.ToString());

                            TagBuilder tagBuilderForChildMenuUnOrderedList = new TagBuilder("ul");
                            tagBuilderForChildMenuUnOrderedList.AddCssClass("navbar-nav ml-5");

                            TagBuilder tagBuilderForChildMenuListItem = new TagBuilder("li");
                            tagBuilderForChildMenuListItem.AddCssClass("sidebar-dropdown nav-item");

                            TagBuilder tagBuilderForChildMenuAnchor = new TagBuilder("a");
                            tagBuilderForChildMenuAnchor.MergeAttribute("href", "javascript:void(0)");
                            tagBuilderForChildMenuAnchor.MergeAttribute("id", "nav-link");
                            //tagBuilderForChildMenuAnchor.MergeAttribute("onclick", "LoadPage('" + dr.NameOfController + "','" + dr.NameOfActionMethod + "')");
                            tagBuilderForChildMenuAnchor.MergeAttribute("onclick", "window.location.href='" + urlHelper.Action(dr.NameOfActionMethod, dr.NameOfController) + "'");
                            tagBuilderForChildMenuAnchor.AddCssClass(dr.MenuPrmKey.ToString());


                            TagBuilder tagBuilderForChildMenuAnchorIcon = new TagBuilder("i");
                            tagBuilderForChildMenuAnchorIcon.AddCssClass("i-medium");
                            tagBuilderForChildMenuAnchorIcon.AddCssClass(dr.IconImageClass);

                            TagBuilder tagBuilderForChildMenuSpan = new TagBuilder("span");
                            tagBuilderForChildMenuSpan.AddCssClass("pl-3 font-weight-light");
                            tagBuilderForChildMenuSpan.SetInnerText(dr.NameOfMenu);

                            tagBuilderForChildMenuAnchor.InnerHtml += tagBuilderForChildMenuAnchorIcon;
                            tagBuilderForChildMenuAnchor.InnerHtml += tagBuilderForChildMenuSpan;

                            tagBuilderForChildMenuListItem.InnerHtml += tagBuilderForChildMenuAnchor;

                            tagBuilderForChildMenuUnOrderedList.InnerHtml += tagBuilderForChildMenuListItem;

                            tagBuilderForChildMenuDiv.InnerHtml += tagBuilderForChildMenuUnOrderedList;

                            tagBuilderForMenuListItem.InnerHtml += tagBuilderForChildMenuDiv;
                        }

                        tagBuilderForMenuUnOrderedList.InnerHtml += tagBuilderForMenuListItem;
                    }

                    string result = tagBuilderForMenuUnOrderedList.ToString();
                    var expiration = DateTime.Now.AddMinutes(20);
                    
                    memoryCache.Add("section", result, expiration);
                }
            }

            var list = memoryCache.Get("section", null);

            return new MvcHtmlString(list.ToString());
        }

        // Label With Regional Language
        public static IHtmlString TextWithRegionalLanguage(this HtmlHelper htmlHelper, string _text)
        {
            string result;

            TranslatorViewModel translatorViewModel = new TranslatorViewModel();

            // Translate Given Text In Regional Language
            result = _text + " / " + translatorViewModel.TranslateInRegionalLanguage(_text);

            return new MvcHtmlString(result.ToString());
        }

        //Custom Label
        public static IHtmlString CustomLabel(this HtmlHelper htmlHelper, string _labelName)
        {
            TranslatorViewModel translatorViewModel = new TranslatorViewModel();

            // Tag Builder For Label
            TagBuilder customlabel = new TagBuilder("Label");
            customlabel.AddCssClass("font-weight-bold");

            // If Label Name Contain Unicode Then Lable Name Does Not Required To Translate
            customlabel.InnerHtml = _labelName.Any(l => l > 255) ? _labelName : _labelName + " / " + translatorViewModel.TranslateInRegionalLanguage(_labelName);

            return new MvcHtmlString(customlabel.ToString());
        }

        ///Custom Footer
        public static MvcHtmlString OperationFooter(this HtmlHelper htmlHelper, string operation, string action, string controller, string Remark)
        {
            // Create UrlHelper Objct For generate link
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            // Tag Builder For footerDiv
            TagBuilder footerDiv = new TagBuilder("div");
            footerDiv.AddCssClass("card-footer bg-white");

            // Tag Builder For footerDiv
            TagBuilder OuterDiv = new TagBuilder("div");
            OuterDiv.AddCssClass("d-flex justify-content-center mb-2");

            if ((operation == "Create") || (operation == "Modify"))
            {
                // Tag Builder For createButton/ModifyButton
                TagBuilder createButton = new TagBuilder("button");
                createButton.MergeAttribute("type", "submit");
                //createButton.MergeAttribute("name", "Command");
                createButton.MergeAttribute("id", "btnsave");
                createButton.MergeAttribute("class", "opnbtnsave");
                createButton.AddCssClass("btn btn-success btn-save");
                //createButton.MergeAttribute("value", "Save");

                //For request-url

                if ((action == "None") & (controller == "None"))
                {

                }
                else
                {
                    var url = urlHelper.Action(action, controller);

                    createButton.MergeAttribute("request-url", url);
                }

                TagBuilder headerTagForCreate = new TagBuilder("h5");

                TagBuilder boldTagForCreate = new TagBuilder("b");

                boldTagForCreate.InnerHtml = "Create";

                if (operation == "Modify")
                {
                    boldTagForCreate.InnerHtml = "Modify";

                    // Tag Builder For remarkButton
                    TagBuilder remarkButton = new TagBuilder("div");
                    remarkButton.AddCssClass("form-group");

                    TagBuilder remarkLabel = new TagBuilder("Label");
                    remarkLabel.AddCssClass("font-weight-bold");
                    remarkLabel.InnerHtml = "Remark";

                    // Remark - Input
                    TagBuilder remarkInput = new TagBuilder("input");
                    remarkInput.AddCssClass("form-control default-none deny-multiple-space");
                    remarkInput.MergeAttribute("type", "text");
                    remarkInput.MergeAttribute("Id", "o-remark");
                    remarkInput.MergeAttribute("Name", "Remark");
                    remarkInput.MergeAttribute("value", Remark);
                    remarkInput.MergeAttribute("placeholder", "Enter Remark");
                    remarkInput.MergeAttribute("autocomplete", "off");

                    remarkButton.InnerHtml += remarkLabel;
                    remarkButton.InnerHtml += remarkInput;
                    footerDiv.InnerHtml += remarkButton;
                }

                headerTagForCreate.InnerHtml += boldTagForCreate;
                createButton.InnerHtml += headerTagForCreate;
                //createButton.InnerHtml += createButton;
                OuterDiv.InnerHtml += createButton;
            }

            if (operation == "Verify")
            {
                // Tag Builder For remarkButton
                TagBuilder remarkButton = new TagBuilder("div");
                remarkButton.AddCssClass("form-group");

                TagBuilder remarkLabel = new TagBuilder("Label");
                remarkLabel.AddCssClass("font-weight-bold");
                remarkLabel.InnerHtml = "Remark";

                // Remark - Input
                TagBuilder remarkInput = new TagBuilder("input");
                remarkInput.AddCssClass("form-control default-none deny-multiple-space");
                remarkInput.MergeAttribute("type", "text");
                remarkInput.MergeAttribute("Id", "o-remark");
                remarkInput.MergeAttribute("Name", "Remark");
                remarkInput.MergeAttribute("value", Remark);
                remarkInput.MergeAttribute("placeholder", "Enter Remark");
                remarkInput.MergeAttribute("autocomplete", "off");

                remarkButton.InnerHtml += remarkLabel;
                remarkButton.InnerHtml += remarkInput;
                footerDiv.InnerHtml += remarkButton;

                // Tag Builder For verifyButton
                TagBuilder verifyButton = new TagBuilder("button");
                verifyButton.MergeAttribute("type", "submit");
                verifyButton.MergeAttribute("name", "Command");
                verifyButton.AddCssClass("btn btn-success");
                verifyButton.MergeAttribute("value", "Verify");
                verifyButton.MergeAttribute("onclick", "Verifyfun('Verify')");

                TagBuilder headerTagForVerify = new TagBuilder("h5");

                TagBuilder boldTagForVerify = new TagBuilder("b");

                boldTagForVerify.InnerHtml = "Verify";

                headerTagForVerify.InnerHtml += boldTagForVerify;
                verifyButton.InnerHtml += headerTagForVerify;
                OuterDiv.InnerHtml += verifyButton;

                // Tag Builder For rejectButton
                TagBuilder rejectButton = new TagBuilder("button");
                rejectButton.MergeAttribute("type", "submit");
                rejectButton.MergeAttribute("name", "Command");
                rejectButton.AddCssClass("btn btn-link mr-2 font-weight-bold");
                rejectButton.MergeAttribute("value", "Reject");
                rejectButton.MergeAttribute("onclick", "Rejectfun('Reject')");

                TagBuilder headerTagForReject = new TagBuilder("h5");

                TagBuilder unArticulatedTagForReject = new TagBuilder("u");

                unArticulatedTagForReject.InnerHtml = "Reject";

                headerTagForReject.InnerHtml += unArticulatedTagForReject;
                rejectButton.InnerHtml += headerTagForReject;
                OuterDiv.InnerHtml += rejectButton;
            }

            if (operation == "Amend")
            {
                // Tag Builder For remarkButton
                TagBuilder remarkButton = new TagBuilder("div");
                remarkButton.AddCssClass("form-group");

                TagBuilder remarkLabel = new TagBuilder("Label");
                remarkLabel.AddCssClass("font-weight-bold");
                remarkLabel.InnerHtml = "Remark";

                // Remark - Input
                TagBuilder remarkInput = new TagBuilder("input");
                remarkInput.AddCssClass("form-control default-none deny-multiple-space");
                remarkInput.MergeAttribute("type", "text");
                remarkInput.MergeAttribute("Id", "o-remark");
                remarkInput.MergeAttribute("Name", "Remark");
                remarkInput.MergeAttribute("value", Remark);
                remarkInput.MergeAttribute("placeholder", "Enter Remark");
                remarkInput.MergeAttribute("autocomplete", "off");

                remarkButton.InnerHtml += remarkLabel;
                remarkButton.InnerHtml += remarkInput;
                footerDiv.InnerHtml += remarkButton;

                // Tag Builder For amendButton
                TagBuilder amendButton = new TagBuilder("button");
                amendButton.MergeAttribute("type", "submit");
                amendButton.MergeAttribute("name", "Command");
                amendButton.MergeAttribute("class", "opnbtnsave");
                amendButton.AddCssClass("btn btn-success btn-save mr-2");
                amendButton.MergeAttribute("value", "Amend");
                amendButton.MergeAttribute("id", "btnsave");

                //For request-url
                if ((action == "None") & (controller == "None"))
                {

                }
                else
                {
                    var url = urlHelper.Action(action, controller);

                    amendButton.MergeAttribute("request-url", url);
                }

                TagBuilder headerTagForAmend = new TagBuilder("h5");

                TagBuilder boldTagForAmend = new TagBuilder("b");

                boldTagForAmend.InnerHtml = "Amend";

                headerTagForAmend.InnerHtml += boldTagForAmend;
                amendButton.InnerHtml += headerTagForAmend;
                OuterDiv.InnerHtml += amendButton;

                TagBuilder deleteButton = new TagBuilder("button");
                deleteButton.MergeAttribute("type", "submit");
                deleteButton.MergeAttribute("name", "Command");
                deleteButton.AddCssClass("btn btn-link mr-2 font-weight-bold");
                deleteButton.MergeAttribute("value", "Delete");
                deleteButton.MergeAttribute("onclick", "Deletefun('Delete')");

                TagBuilder headerTagForDelete = new TagBuilder("h5");

                TagBuilder unArticulatedTagForDelete = new TagBuilder("u");

                unArticulatedTagForDelete.InnerHtml = "Delete";

                headerTagForDelete.InnerHtml += unArticulatedTagForDelete;
                deleteButton.InnerHtml += headerTagForDelete;
                OuterDiv.InnerHtml += deleteButton;
            }

            // Tag Builder For Exit Button
            TagBuilder exitButton = new TagBuilder("button");
            exitButton.MergeAttribute("type", "button");
            //exitButton.MergeAttribute("name", "Command");

            if (operation == "None")
            {
                exitButton.AddCssClass("btn-success btn-lg-custom font-weight-bold");
                footerDiv.AddCssClass("mb-5");
            }
            else
            {
                exitButton.AddCssClass("btn btn-link font-weight-bold");
            }
            //exitButton.MergeAttribute("value", "Exit");
            exitButton.MergeAttribute("onclick", "window.location.href='" + urlHelper.Action("Default", "Home") + "'");

            TagBuilder headerTagForExit = new TagBuilder("h5");

            TagBuilder unArticulatedTagForExit = new TagBuilder("u");

            unArticulatedTagForExit.InnerHtml = "Exit";

            headerTagForExit.InnerHtml += unArticulatedTagForExit;
            exitButton.InnerHtml += headerTagForExit;

            OuterDiv.InnerHtml += exitButton;
            footerDiv.InnerHtml += OuterDiv;

            return new MvcHtmlString(footerDiv.ToString());
        }
    }
}
