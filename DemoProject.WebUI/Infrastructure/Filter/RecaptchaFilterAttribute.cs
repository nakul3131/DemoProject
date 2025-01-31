using System;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.WebUI.Infrastructure.Filter
{
    public class RecaptchaFilterAttribute : ActionFilterAttribute 
    {
        //
        // Summary:
        //     Called by the ASP.NET MVC framework after the action result executes.
        //
        // Parameters:
        //   filterContext:
        //     The filter context.
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            CheckRequestStatus(filterContext);
        }

        //
        // Summary:
        //     Called by the OnResultExecuted method for checking Human Behavior 
        //
        // Parameters:
        //   ResultExecutedContext:
        //     The filter context.
        private void CheckRequestStatus(ResultExecutedContext filterContext)
        {
            //
            // Summary:
            //     Get Count of Repeated Request Of Same Page  
            //
            HttpCookie repeatedRequestCountCookie = filterContext.HttpContext.Request.Cookies["RepeatedRequestCountCookie"];

            //
            // Summary:
            //     Get Name of Previous Requested Page  
            //
            HttpCookie previousPageCookie = filterContext.HttpContext.Request.Cookies["PreviousRequestedPage"];

            // If User Request First Time (i.e. No Any Previous Page Available)
            if (filterContext.HttpContext.Request.Cookies["PreviousRequestedPage"] == null)
            {
                // Assign Current Page As Previous Requested Page Which Expire After 365 Days
                HttpCookie tmpPreviousPageCookie = new HttpCookie("PreviousRequestedPage", filterContext.RouteData.Values["action"].ToString());
                tmpPreviousPageCookie.Expires = DateTime.Now.AddDays(365);
                filterContext.HttpContext.Response.Cookies.Add(tmpPreviousPageCookie);
            }
            // If Any Previous Page Request Available
            else
            {
                // If Same Page Requested Repeatedly
                if (previousPageCookie.Value == filterContext.RouteData.Values["action"].ToString())
                {
                    if (repeatedRequestCountCookie is null)
                    {
                        // Assign 1 (One) To RepeatedRequestCount
                        HttpCookie tmpRepeatedRequestCountCookie = new HttpCookie("RepeatedRequestCountCookie", "1");
                        tmpRepeatedRequestCountCookie.Expires = DateTime.Now.AddDays(365);
                        filterContext.HttpContext.Response.Cookies.Add(tmpRepeatedRequestCountCookie);
                    }
                    else
                    {
                        // Increment Count
                        int a = int.Parse(repeatedRequestCountCookie.Value);
                        repeatedRequestCountCookie.Value = (a + 1).ToString();
                        filterContext.HttpContext.Response.Cookies.Add(repeatedRequestCountCookie);
                    }
                }
                // If Another Page Requested reset all counts or coockies
                else
                {
                    if (repeatedRequestCountCookie != null)
                    {
                        // If Another Page Requested then Expired Session (i.e. Reset Count)
                        repeatedRequestCountCookie.Expires = DateTime.Now.AddDays(-1);
                        filterContext.HttpContext.Response.Cookies.Add(repeatedRequestCountCookie);
                    }
                    if (previousPageCookie != null)
                    {
                        previousPageCookie.Expires = DateTime.Now.AddDays(-1);
                        filterContext.HttpContext.Response.Cookies.Add(previousPageCookie);
                    }
                }
            }
        }
    }
}