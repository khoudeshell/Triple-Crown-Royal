using System;
using System.Web;
using log4net;
using System.Transactions;

namespace HorseLeague.Logger
{
    public class Logger : ILogger
    {
        private const string LOGGER = "hrlog";

        public void LogError(string message, Exception e)
        {
            DoLog(x => { return x.IsErrorEnabled; },
                y => { y.Error(message, e); });
        }

        public void LogInfo(string message)
        {
            DoLog(x => { return x.IsInfoEnabled; }, 
                y => { y.Info(message); }); 
        }

        private void DoLog(Func<ILog, bool> isEnabled, Action<ILog> logIt)
        {
            var logger = LogManager.GetLogger(LOGGER);

            if (isEnabled(logger))
            {
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
                    log4net.LogicalThreadContext.Properties["user"] = HttpContext.Current.User.Identity.Name;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    logIt(logger);
                }
            }

        }

        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.Repository.Hierarchy.Hierarchy hier = log4net.LogManager.GetRepository() as log4net.Repository.Hierarchy.Hierarchy;
            if (hier != null)
            {
                var logger = hier.GetLogger(LOGGER,
                    hier.LoggerFactory);

                log4net.Appender.AdoNetAppender adoAppender =
                  (log4net.Appender.AdoNetAppender)logger.GetAppender("AdoNetAppender");
                if (adoAppender != null)
                {
                    adoAppender.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HRLeagueConnectionString1"].ConnectionString;
                    adoAppender.ActivateOptions(); //refresh settings of appender
                }
            }
        }
    }

    public interface ILogger
    {
        void LogError(string message, Exception e);
        void LogInfo(string message);
    }
}