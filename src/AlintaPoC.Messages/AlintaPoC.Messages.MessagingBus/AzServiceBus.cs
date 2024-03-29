﻿using AlintaPoC.Messages.MessagingBus.Interface;
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
        private readonly ServiceBusClient _client;

        public AzServiceBus(string connectionString)
        {
            _client = new ServiceBusClient(connectionString);
        }

        public void Send(string message, string topicName)
        {
            SendTextStringAsync(message, topicName).Wait();
        }

        private async Task SendTextStringAsync(string text, string topicName)
        {
            ServiceBusSender sender = _client.CreateSender(topicName);

            var message = new ServiceBusMessage(text) 
            { 
                Subject = "AlintaPoC.Messages",
                ContentType = "application/json",
                // MessageId = "" // Set this for duplicate detection
                //CorrelationId = "" Set this for Correlation filter - routing
                //SessionId = "" // Set this for Correlation grouping
            };

            // Set this for Sql filter
            //message.ApplicationProperties.Add("region", "US");
            //message.ApplicationProperties.Add("items", 20);

            await sender.SendMessageAsync(message);
            await sender.CloseAsync();
        }
    }
}
