using System;
using System.Threading.Tasks;
using CloudProvisioningPortal.Persistence;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CloudProvisioningPortal.Jobs
{
    public class FulfillRequestJob : IJob
    {
        private readonly InMemoryProvisioningStore _requestStore;
        private ILogger _logger; 
        
        public FulfillRequestJob (InMemoryProvisioningStore requestStore, ILoggerFactory loggerFactory)
        {
            _requestStore = requestStore;
            _logger = loggerFactory.CreateLogger<FulfillRequestJob>();
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() => 
            {
                var requestId = new Guid(context.JobDetail.JobDataMap.GetString("requestId"));
                var request = _requestStore.Get(requestId);

                if (request != null)
                {
                    _logger.LogInformation("Starting request fulfillment job for request {0}", requestId);

                    System.Threading.Thread.Sleep(20000);
                    request.StartProvisioning();
                    System.Threading.Thread.Sleep(10000);
                    request.Complete();

                    _logger.LogInformation("Request {0} successfully fulfilled", requestId);
                }
                else
                {
                    _logger.LogError("Attempted to run a job for a request that is not tracked (requestId: {0})", requestId);
                }
            });
        }
    }
}