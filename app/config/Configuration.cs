using DotNetEnv;
using Microsoft.Extensions.Logging;

namespace log_storage.app.config
{
    public class Configuration
    {
        public string _prefixFileName { get; private set; }
        public string _sufixFileName { get; private set; }
        public string _filePath { get; private set; }
        public string _formatDate { get; private set; }
        public string _bucketName { get; private set; }
        public string _currentDate { get; private set; }

        public string _accessKey { get; private set; }
        public string _secretKey {  get; private set; }

        private static ILogger _logger;

        public Configuration(ILogger logger) 
        {
            _logger = logger;
            Env.Load(); 
        }

        public bool LoadingEnviroments() 
        {
            var errors = "";
            _filePath = @"" + Environment.GetEnvironmentVariable("LOG_STORAGE_PATH_FILE");
            if (_filePath == null)
            {
                errors += "could not get env LOG_STORAGE_PATH_FILE. ";
            }

            _prefixFileName = Environment.GetEnvironmentVariable("LOG_STORAGE_PREFIX_FILENAME");
            if (_prefixFileName == null)
            {
                errors += "could not get env LOG_STORAGE_PREFIX_FILENAME. ";
            }

            _sufixFileName = Environment.GetEnvironmentVariable("LOG_STORAGE_SUFIX_FILENAME");
            if (_sufixFileName == null)
            {
                errors += "could not get env LOG_STORAGE_SUFIX_FILENAME. ";
            }

            _formatDate = Environment.GetEnvironmentVariable("LOG_STORAGE_FORMAT_DATE");
            if (_formatDate == null)
            {
                errors += "could not get env LOG_STORAGE_FORMAT_DATE. ";
            }

            _accessKey = Environment.GetEnvironmentVariable("LOG_STORAGE_ACCESS_KEY");
            if (_accessKey == null)
            {
                errors += "could not get env LOG_STORAGE_ACCESS_KEY. ";
            }

            _secretKey = Environment.GetEnvironmentVariable("LOG_STORAGE_SECRET_KEY");
            if (_secretKey == null)
            {
                errors += "could not get env LOG_STORAGE_SECRET_KEY. ";
            }

            _bucketName = Environment.GetEnvironmentVariable("LOG_STORAGE_BUCKET_NAME");
            if (_bucketName == null)
            {
                errors += "could not get env LOG_STORAGE_BUCKET_NAME. ";
            }

            if (errors != "")
            {
                _logger.LogError("could not get enviroments variables.");
                _logger.LogError(errors);
                return false;
            }

            _currentDate = DateTime.Today.ToString(_formatDate);

            _logger.LogInformation("loading enviroments variables with sucessfully");
            return true;
        }
    }
}
