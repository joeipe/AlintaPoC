using AlintaPoC.Messages.MessagingBus.Interface;
using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Messages.MessagingBus
{
    public class AzServiceBus : IBus
    {
        private readonly string _connectionString;

        public AzServiceBus(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Send(string message, string topicName)
        {
            SendTextStringAsync(message, topicName).Wait();
        }

        private async Task SendTextStringAsync(string text, string topicName)
        {
            await using (var client = new ServiceBusClient(_connectionString))
            {
                ServiceBusSender sender = client.CreateSender(topicName);

                await sender.SendMessageAsync(new ServiceBusMessage(text));
            }
        }
    }
}
