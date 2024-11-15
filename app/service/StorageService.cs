using log_storage.app.config;
using Microsoft.Extensions.Logging;
using Amazon.S3;
using Amazon.S3.Transfer;
using log_storage.app.aws;
using Microsoft.Extensions.Configuration;


namespace log_storage.app.service
{
    public class StorageService
    {
        private static ILogger _logger;

        private static AtomicBoolean _flag;
        private readonly AWSService _S3Service;

        public StorageService(ILogger logger, Configuration config, AtomicBoolean flag)
        {
            _flag = flag;
            _logger = logger;
            _S3Service = new AWSService(config);
        }

        public void Start(Configuration config) 
        {
            try
            {
                _logger.LogInformation($"trying to save a file to the S3 bucket: {config._bucketName}");
                _S3Service.UploadFile(config._filePath);
                _logger.LogInformation($"save file with successfully. URL to file: " +
                    $"https://{config._bucketName}.s3.sa-east-1.amazonaws.com/log-storage{config._currentDate}{config._sufixFileName}");
            }
            catch(Exception e)
            {   
                _logger.LogError(e.Message);
            }
            finally
            {
                _flag.SetFalse();
            }
        }
    }
}
