using System;
using Microsoft.Extensions.Logging;

namespace ExemploLogCore.ExtensionLogger
{
    public static class AppLoggerExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory,
            Func<string, LogLevel, bool> filter = null, string connectionString = null)
        {
            factory.AddProvider(new AppLoggerProvider(filter));
            return factory;
        }

        public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minLevel)
        {
            return AddContext(factory,(_, logLevel) => logLevel >= minLevel);
        }
    }
}
