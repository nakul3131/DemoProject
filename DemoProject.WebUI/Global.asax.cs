﻿using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using DemoProject.Mapping;
using DemoProject.Services.Abstract.Security;

namespace DemoProject.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly IAuthProviderRepository authProviderRepository;

        public MvcApplication()
        {
            authProviderRepository = DependencyResolver.Current.GetService<IAuthProviderRepository>();
        }
        protected void Application_Start()
        {
            DevExpress.ExpressApp.FrameworkSettings.DefaultSettingsCompatibilityMode = DevExpress.ExpressApp.FrameworkSettingsCompatibilityMode.v20_1;
            DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Disabled;
            AutoMapper.Mapper.Initialize(acb => acb.AddProfile<AutomapperProfile>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ViewEngines.Engines.Clear(); //clear all engines
            //ViewEngines.Engines.Add(new RazorViewEngine());
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            DevExpress.Web.Mvc.MVCxWebDocumentViewer.StaticInitialize();
        }
    }
}
