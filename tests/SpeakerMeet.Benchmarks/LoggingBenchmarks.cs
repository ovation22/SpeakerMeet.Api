using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;
using Serilog;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Infrastructure.Logging;

namespace SpeakerMeet.Benchmarks
{
    [MemoryDiagnoser()]
    public class LoggingBenchmarks
    {
        private readonly ILogger<LoggingBenchmarks> _logger;
        private readonly ILoggerAdapter<LoggingBenchmarks> _loggerAdapter;

        private readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddSerilog()
                .SetMinimumLevel(LogLevel.Warning);
        });

        public LoggingBenchmarks()
        {
            _logger = new Logger<LoggingBenchmarks>(_loggerFactory);
            _loggerAdapter = new LoggerAdapter<LoggingBenchmarks>(_logger);
        }

        [Benchmark]
        public void Logger_Without_If()
        {
            _logger.LogInformation("test {0}", 42);
        }

        [Benchmark]
        public void Logger_With_If()
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("test {0}", 42);
            }
        }

        [Benchmark]
        public void LoggerAdapter()
        {
            _loggerAdapter.LogInformation("test {0}", 42);
        }
    }
}