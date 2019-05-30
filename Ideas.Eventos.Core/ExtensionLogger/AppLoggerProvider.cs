using System;
using Microsoft.Extensions.Logging;

namespace ExemploLogCore.ExtensionLogger
{
    public class AppLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filtro;

        public AppLoggerProvider(Func<string, LogLevel, bool> filtro)
        {
            _filtro = filtro;
        }

        public ILogger CreateLogger(string nomeCategoria)
        {
            return new AppLogger(nomeCategoria, _filtro);
        }

        public void Dispose()
        {

        }
    }
}
