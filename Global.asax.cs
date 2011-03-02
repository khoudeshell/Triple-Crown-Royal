using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SharpArch.Web.Areas;
using SharpArch.Web.ModelBinder;
using SharpArch.Core.NHibernateValidator.ValidatorProvider;
using HorseLeague.Controllers;
using Castle.Windsor;
using SharpArch.Web.Castle;
using TripleCrownRoyal.Web.CastleWindsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using SharpArch.Web.NHibernate;
using SharpArch.Data.NHibernate;
using System.Reflection;

namespace HorseLeague
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new AreaViewEngine());

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();

            ModelValidatorProviders.Providers.Add(new NHibernateValidatorProvider());

            InitializeServiceLocator();

            AreaRegistration.RegisterAllAreas();
            RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);

#if DEBUG
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();//.InitializeOfflineProfiling(@"C:\Temp\msft\proflog.nhprof");
#endif
        }

        /// <summary>
        /// Instantiate the container and add all Controllers that derive from
        /// WindsorController to the container.  Also associate the Controller
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            container.RegisterControllers(typeof(HomeController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        public override void Init()
        {
            base.Init();

            // The WebSessionStorage must be created during the Init() to tie in HttpApplication events
            webSessionStorage = new WebSessionStorage(this);
        }

 
        /// <summary>
        /// Due to issues on IIS7, the NHibernate initialization cannot reside in Init() but
        /// must only be called once.  Consequently, we invoke a thread-safe singleton class to
        /// ensure it's only initialized once.
        /// </summary>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(
                () => InitializeNHibernateSession());
        }

        /// <summary>
        /// If you need to communicate to multiple databases, you'd add a line to this method to
        /// initialize the other database as well.
        /// </summary>
        private void InitializeNHibernateSession()
        {
            NHibernateSession.Init(
                webSessionStorage,
                new string[] { Server.MapPath("~/bin/HorseLeague.dll") },
                Server.MapPath("~/NHibernate.config"));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Useful for debugging
            Exception ex = Server.GetLastError();
            ReflectionTypeLoadException reflectionTypeLoadException = ex as ReflectionTypeLoadException;
        }

        private WebSessionStorage webSessionStorage;

    }
}