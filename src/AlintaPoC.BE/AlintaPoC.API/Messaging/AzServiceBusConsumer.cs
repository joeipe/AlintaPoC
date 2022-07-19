using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AlintaPoC.API.Messaging
{
    public class AzServiceBusConsumer : IHostedService
    {
        private readonly IConfiguration _configuration;

        private readonly ServiceBusProcessor personMessageProcessor;

        public AzServiceBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;

            var serviceBusConnectionString = _configuration.GetConnectionString("ServiceBusConnectionString");
            var client = new ServiceBusClient(serviceBusConnectionString);

            var options = new ServiceBusProcessorOptions()
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 1,
                MaxAutoLockRenewalDuration = TimeSpan.FromMinutes(10),
                //SubQueue = SubQueue.DeadLetter
            };
            personMessageProcessor = client.CreateProcessor("persontopic", "AlintaPoCApiSubscriptions", options);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            personMessageProcessor.ProcessMessageAsync += ProcessPersonMessageAsync;
            personMessageProcessor.ProcessErrorAsync += ProcessErrorAsync;

            await personMessageProcessor.StartProcessingAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await personMessageProcessor.StopProcessingAsync();
            await personMessageProcessor.CloseAsync();
        }

        private async Task ProcessPersonMessageAsync(ProcessMessageEventArgs args)
        {
            try
            {
                var message = args.Message.Body.ToString();

                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                //Complete, Abandon, Dead-lettter, Defer
                if (args.Message.DeliveryCount > 5)
                {
                    await args.DeadLetterMessageAsync(args.Message, ex.Message, ex.ToString());
                }
            }
        }

        private async Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            //throw new NotImplementedException();
        }
    }
}
