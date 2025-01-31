
[assembly: WebActivator.PreApplicationStartMethod(typeof(DemoProject.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(DemoProject.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace DemoProject.WebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

//
//  Summary
//          This class defines methods called automatically when the applicaton starts, in order to integrate into the
//          ASP.NET request lifecycle.
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        //
        //  Summary
        //          In this method, we creates an instance of NinjectDependencyResolver class and uses the static SetResolver method
        //          defined by the System.Web.Mvc.DependencyResolver class to register the resolver with the MVC Framework.
        private static void RegisterServices(IKernel kernel)
        {
            //          This statement is create a bridge between Ninject and the MVC Framework support for DI.
            System.Web.Mvc.DependencyResolver.SetResolver(new DemoProject.WebUI.Infrastructure.NinjectDependencyResolver(kernel));
        }        
    }
}
