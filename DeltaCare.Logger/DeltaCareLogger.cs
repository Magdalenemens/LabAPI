using Serilog;

namespace DeltaCare.Logger
{
    public static class DeltaCareLogger
    {
        private static readonly ILogger _logger;
        static DeltaCareLogger()
        {
            //_logger = LogManager.GetCurrentClassLogger();
            _logger = new LoggerConfiguration()
           .WriteTo.Console()
           .CreateLogger();
        }

        public static void Info(string message)
        {
            _logger.Information(message);
        }
        public static void Debug(string message)
        {
            _logger.Debug(message);
        }
        public static void Error(string message)
        {
            _logger.Error(message);
        }
    }
}
