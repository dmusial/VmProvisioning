using System;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;

namespace CloudProvisioningPortal.Jobs
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public JobFactory (IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            _serviceProvider = serviceProvider;
            _logger = loggerFactory.CreateLogger<JobFactory>();
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            _logger.LogInformation("Building job {0}", bundle.JobDetail.JobType.Name);
            var jobType = bundle.JobDetail.JobType;
            return (IJob)_serviceProvider.GetService(jobType);
        }

        public void ReturnJob(IJob job)
        {
            
        }
    }
}