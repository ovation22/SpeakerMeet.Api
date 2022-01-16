using System;
using Microsoft.Extensions.Logging;
using SpeakerMeet.Core.Interfaces.Logging;

namespace SpeakerMeet.Infrastructure.Logging
{
    public class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string? message)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(message);
            }
        }

        public void LogInformation<T0>(string? message, T0? arg0)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(message, arg0);
            }
        }

        public void LogInformation<T0, T1>(string message, T0 arg0, T1 arg1)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(message, arg0, arg1);
            }
        }

        public void LogInformation<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(message, arg0, arg1, arg2);
            }
        }

        public void LogWarning(string? message, params object?[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogWarning(Exception? ex, string? message, params object?[] args)
        {
            _logger.LogWarning(ex, message, args);
        }

        public void LogError(Exception? ex, string? message, params object?[] args)
        {
            _logger.LogError(ex, message, args);
        }
    }
}
