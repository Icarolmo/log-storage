using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using log_storage.app.config;
using Microsoft.Extensions.Configuration;

namespace log_storage.app.aws
{
    public class AWSService
    {
        private readonly TransferUtility _fileTransferUtility;

        private readonly string _bucketName;

        private readonly string _keyName;

        private readonly string _fileName;

        public AWSService(Configuration config) 
        {
            var credentials = new BasicAWSCredentials(config._accessKey, config._secretKey);
            var s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.SAEast1);

            _fileTransferUtility = new TransferUtility(s3Client);
            _bucketName = config._bucketName;
            _fileName = $"{config._prefixFileName}{config._currentDate}{config._sufixFileName}";
            _keyName = $"log-storage{config._currentDate}{config._sufixFileName}";
        }

        public void UploadFile(string filePath)
        {   
            _fileTransferUtility.Upload(filePath + _fileName, _bucketName, _keyName);
        }
    }
}
