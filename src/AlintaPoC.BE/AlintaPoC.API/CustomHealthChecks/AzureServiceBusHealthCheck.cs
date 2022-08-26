using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlintaPoC.API.CustomHealthChecks
{
    public class AzureServiceBusHealthCheck : IHealthCheck
    {
        private readonly ServiceBusAdministrationClient _adminClient;
        private readonly string _topic;

        public AzureServiceBusHealthCheck(string connectionString, string topicName)
        {
            _adminClient = new ServiceBusAdministrationClient(connectionString);
            _topic = topicName;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (await _adminClient.TopicExistsAsync(_topic))
                    return HealthCheckResult.Healthy();

                return new HealthCheckResult(context.Registration.FailureStatus);
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
        }
    }
}
