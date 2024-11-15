using log_storage.app.config;
using log_storage.app.service;
using Microsoft.Extensions.Logging;

namespace log_storage.app
{ 
    public class Application
    {
        public void Start()
        {
            var logger = new Logger();
            var configuration = new Configuration(logger.getInstance());
            var success = configuration.LoadingEnviroments();
            if (!success)
            {
                Environment.Exit(0);
            }

            AtomicBoolean flag = new AtomicBoolean();
            var storageService = new StorageService(logger.getInstance(), configuration,  flag);
            
            new Thread(() =>
            {
                storageService.Start(configuration);
            }).Start();

            while (flag.IsTrue)
            {
                Thread.Sleep(1000 * 5);
            }

            logger.getInstance().LogInformation("finish job. Stopping application....");
        }
    }
}
