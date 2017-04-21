using NLog;
using NLog.Config;
using NLog.Targets;
using System;

namespace Warden.Business.Helper
{
    public static class TransactionImportTracer
    {
        private static bool configured;
        private static string logsFolderPath;

        private static Logger GetTracer(string name)
        {
            if (!configured)
                throw new InvalidOperationException("Tracer should be configured");

            return Execute(() => LogManager.GetLogger(name), name);
        }

        public static void Trace(string tracerName, string message)
        {
            GetTracer(tracerName).Trace($"[{tracerName}] | {message}");
        }
        
        public static string GetTracerFileName(string tracerName)
        {
            return Execute(() =>
            {
                var fileTarget = (FileTarget)LogManager.GetLogger(tracerName).Factory.Configuration.FindTargetByName(tracerName);
                var logEventInfo = new LogEventInfo { TimeStamp = DateTime.Now };
                return fileTarget.FileName.Render(logEventInfo);
            }, tracerName);
        }

        public static void Configurate(string logsPath)
        {
            logsFolderPath = logsPath;
            configured = true;
        }

        private static void AddRule(string name)
        {
            var configuration = LogManager.Configuration ?? new LoggingConfiguration();

            var target = new FileTarget(name)
            {
                FileName = string.Format(@"${{basedir}}/{0}/{1}/log.txt", logsFolderPath, name),
                Layout = "Date: ${date:format=yyyy-MM-dd HH\\:mm\\:ss} | Message: ${message}"
            };

            configuration.AddTarget(target);

            var rule = new LoggingRule(name, target);
            rule.EnableLoggingForLevel(LogLevel.Trace);
            configuration.LoggingRules.Add(rule);

            LogManager.Configuration = configuration;
        }

        private static T Execute<T>(Func<T> expression, string tracerNameToValidate)
        {
            var logger = LogManager.GetLogger(tracerNameToValidate);
            if (logger.Factory.Configuration == null || logger.Factory.Configuration.FindTargetByName(tracerNameToValidate) == null)
                AddRule(tracerNameToValidate);

            return expression();
        }
    }
}
