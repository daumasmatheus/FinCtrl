[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FinCtrl.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FinCtrl.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace FinCtrl.WebUI.App_Start
{
    using FinCtrl.Application.Interfaces.Financas;
    using FinCtrl.Application.Interfaces.Tipos;
    using FinCtrl.Application.Services.Financas;
    using FinCtrl.Application.Services.Tipos;
    using FinCtrl.Persistence.Interfaces;
    using FinCtrl.Persistence.UnitOfWork;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using System;
    using System.Web;

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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                kernel.Bind(x => {
                    x.FromThisAssembly().SelectAllClasses().BindDefaultInterface();
                });

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IFinancasServices>().To<FinancasServices>();
            kernel.Bind<ITiposService>().To<TiposService>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }        
    }
}