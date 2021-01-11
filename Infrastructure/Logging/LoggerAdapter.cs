using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TaskTodo.ApplicationCore.Interfaces;

namespace Infrastructure.Logging
{
    /// <summary>
    /// 提供日志适配器Adapter的作用在于日后日志中间件的可替换性
    /// 当然你也可以直接使用内置的ILogger
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}
