using System.IO;
using System.Web;
using System.Web.Routing;

namespace DemoProject.WebUI.Utility
{
    public class RoutingUtility
    {
        public RouteData GetRouteDataFromUrl (string _url)
        {
            // Split the url to url + query string
            string fullUrl = _url; //HttpContext.Current.Request.UrlReferrer.ToString();
            var questionMarkIndex = fullUrl.IndexOf('?');

            string queryString = null;
            string url = fullUrl;

            if (questionMarkIndex != -1) // There is a QueryString
            {
                url = fullUrl.Substring(0, questionMarkIndex);
                queryString = fullUrl.Substring(questionMarkIndex + 1);
            }

            // Arranges
            var request =  new HttpRequest(null, url, queryString);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, response);

            RouteData routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            return routeData;

            // Retrive the data    
            //var values = routeData.Values;
            //var controllerName = values["controller"];
            //var actionName = values["action"];
            //var areaName = values["area"];
        }
    }
}