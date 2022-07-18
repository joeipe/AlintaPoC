using Azure;
using Azure.Messaging.ServiceBus.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Messages.MessagingBus
{
    public class ManagementHelper
    {
        private readonly ServiceBusAdministrationClient _administrationClient;

        public ManagementHelper(string connectionString)
        {
            _administrationClient = new ServiceBusAdministrationClient(connectionString);
        }

        public async Task CreateQueueAsync(string queueName)
        {
            Console.Write("Creating queue {0}...", queueName);
            var description = GetQueueDescription(queueName);
            var createdDescription = await _administrationClient.CreateQueueAsync(description);
            Console.WriteLine("Done!");
        }

        public async Task DeleteQueueAsync(string queueName)
        {
            Console.Write("Deleting queue {0}...", queueName);
            await _administrationClient.DeleteQueueAsync(queueName);
            Console.WriteLine("Done!");
        }

        public async Task ListQueuesAsync()
        {
            AsyncPageable<QueueProperties> allQueueProperties = _administrationClient.GetQueuesAsync();
            List<QueueProperties> queuePropertiesList = await allQueueProperties.ToListAsync();
            Console.WriteLine("Listing queues...");
            foreach (QueueProperties queueProperties in queuePropertiesList)
            {
                Console.WriteLine("\t{0}", queueProperties.Name);
            }
            Console.WriteLine("Done!");
        }

        public async Task GetQueueAsync(string queuePath)
        {
            QueueProperties queueProperties = await _administrationClient.GetQueueAsync(queuePath);
            Console.WriteLine($"Queue description for {queuePath}");
            Console.WriteLine($"    Name:                                   {queueProperties.Name}");
            Console.WriteLine($"    MaxSizeInMegabytes:                     {queueProperties.MaxSizeInMegabytes}");
            Console.WriteLine($"    RequiresSession:                        {queueProperties.RequiresSession}");
            Console.WriteLine($"    RequiresDuplicateDetection:             {queueProperties.RequiresDuplicateDetection}");
            Console.WriteLine($"    DuplicateDetectionHistoryTimeWindow:    {queueProperties.DuplicateDetectionHistoryTimeWindow}");
            Console.WriteLine($"    LockDuration:                           {queueProperties.LockDuration}");
            Console.WriteLine($"    DefaultMessageTimeToLive:               {queueProperties.DefaultMessageTimeToLive}");
            Console.WriteLine($"    DeadLetteringOnMessageExpiration:       {queueProperties.DeadLetteringOnMessageExpiration}");
            Console.WriteLine($"    EnableBatchedOperations:                {queueProperties.EnableBatchedOperations}");
            Console.WriteLine($"    MaxSizeInMegabytes:                     {queueProperties.MaxSizeInMegabytes}");
            Console.WriteLine($"    MaxDeliveryCount:                       {queueProperties.MaxDeliveryCount}");
            Console.WriteLine($"    Status:                                 {queueProperties.Status}");
        }

        public async Task CreateTopicAsync(string topicName)
        {
            Console.Write("Creating topic {0}...", topicName);
            var description = GetTopicDescription(topicName);
            //var createdDescription = await _administrationClient.CreateTopicAsync(topicName);
            var createdDescription = await _administrationClient.CreateTopicAsync(description);
            Console.WriteLine("Done!");
        }

        public async Task CreateSubscriptionAsync(string topicName, string subscriptionName)
        {
            Console.Write("Creating subscription {0}/subscriptions/{1}...", topicName, subscriptionName);
            var description = await _administrationClient.CreateSubscriptionAsync(topicName, subscriptionName);
            Console.WriteLine("Done!");
        }

        // "region = 'USA'"
        // "items > 30"
        // "loyalty = true AND region = 'USA'"
        public async Task CreateSubscriptionWithSqlFilter(string topicName, string subscriptionName, string sqlExpression) 
        {
            Console.WriteLine($"Creating Subscription with SQL Filter{topicName}/{subscriptionName} ({sqlExpression})");
            var subscriptionOptions = new CreateSubscriptionOptions(topicName, subscriptionName);
            var ruleOptions = new CreateRuleOptions("Default", new SqlRuleFilter(sqlExpression));
            var description = await _administrationClient.CreateSubscriptionAsync(subscriptionOptions, ruleOptions);
            Console.WriteLine("Done!");
        }

        public async Task CreateSubscriptionWithCorrelationFilter(string topicName, string subscriptionName, string correlationId)
        {
            Console.WriteLine($"Creating Subscription with Correlation Filter{topicName}/{subscriptionName} ({correlationId})");
            var subscriptionOptions = new CreateSubscriptionOptions(topicName, subscriptionName);
            var ruleOptions = new CreateRuleOptions("Default", new CorrelationRuleFilter(correlationId));
            var description = await _administrationClient.CreateSubscriptionAsync(subscriptionOptions, ruleOptions);
            Console.WriteLine("Done!");
        }

        public async Task ListTopicsAndSubscriptionsAsync()
        {
            AsyncPageable<TopicProperties> allTopicProperties = _administrationClient.GetTopicsAsync();
            List<TopicProperties> topicPropertiesList = await allTopicProperties.ToListAsync();
            Console.WriteLine("Listing topics and subscriptions...");
            foreach (TopicProperties topicProperties in topicPropertiesList)
            {
                Console.WriteLine("\t{0}", topicProperties.Name);
                AsyncPageable<SubscriptionProperties> allSubscriptionProperties = _administrationClient.GetSubscriptionsAsync(topicProperties.Name);
                List<SubscriptionProperties> subscriptionPropertiesList = await allSubscriptionProperties.ToListAsync();
                foreach (SubscriptionProperties subscriptionProperties in subscriptionPropertiesList)
                {
                    Console.WriteLine("\t\t{0}", subscriptionProperties.SubscriptionName);
                }
            }
            Console.WriteLine("Done!");
        }

        public CreateQueueOptions GetQueueDescription(string name)
        {
            return new CreateQueueOptions(name)
            {
                //RequiresDuplicateDetection = true,
                //DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(10),
                //RequiresSession = true,
                //MaxDeliveryCount = 20,
                //DefaultMessageTimeToLive = TimeSpan.FromHours(1),
                //EnableDeadLetteringOnMessageExpiration = true
            };
        }

        public CreateTopicOptions GetTopicDescription(string name)
        {
            return new CreateTopicOptions(name)
            {
                //RequiresDuplicateDetection = true,
                //DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(10),
                //RequiresSession = true,
                //MaxDeliveryCount = 20,
                //DefaultMessageTimeToLive = TimeSpan.FromHours(1),
                //EnableDeadLetteringOnMessageExpiration = true
            };
        }
    }
}
