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
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false,
                //ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
            };
            personMessageProcessor = client.CreateProcessor("persontopic", "AlintaPoCApiSubscriptions", options);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            personMessageProcessor.ProcessMessageAsync += PersonMessageHandlerAsync;
            personMessageProcessor.ProcessErrorAsync += ErrorHandlerAsync;

            await personMessageProcessor.StartProcessingAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await personMessageProcessor.StartProcessingAsync();
            await personMessageProcessor.CloseAsync();
        }

        private async Task PersonMessageHandlerAsync(ProcessMessageEventArgs args)
        {
            var message = args.Message.Body.ToString();

            await args.CompleteMessageAsync(args.Message);
        }

        private async Task ErrorHandlerAsync(ProcessErrorEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
