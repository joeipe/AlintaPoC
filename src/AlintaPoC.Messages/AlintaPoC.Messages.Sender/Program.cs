using AlintaPoC.Messages.MessagingBus;
using Azure.Messaging.ServiceBus.Administration;
using System;
using System.Threading.Tasks;

namespace AlintaPoC.Messages.Sender
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var serviceBusConnectionString = "Endpoint=sb://alintapocmessaging.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=UY6efbRVDvNzW1BL5a4soZB6qT1iZX/nI/Qgq6DGu98=";
            var topicName = "persontopic";

            /*
            // Create an administraion client to manage artifacts
            var serviceBusAdministrationClient = new ServiceBusAdministrationClient(serviceBusConnectionString);

            //Create a topic if it doesnot exist
            if (!await serviceBusAdministrationClient.TopicExistsAsync(topicName))
            {
                await serviceBusAdministrationClient.CreateTopicAsync(topicName);
            }
            */

            MessageBus messageBus = new MessageBus(new AzServiceBus(serviceBusConnectionString));
            var person = new { id = 0, firstName = "J", lastName = "Bond", doB = "26/04/1981" };
            messageBus.SendMessage(person, topicName);
        }
    }
}
