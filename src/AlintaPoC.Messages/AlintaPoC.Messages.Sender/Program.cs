using AlintaPoC.Messages.MessagingBus;
using Azure.Messaging.ServiceBus.Administration;
using System;
using System.Threading.Tasks;

namespace AlintaPoC.Messages.Sender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var serviceBusConnectionString = "Endpoint=sb://alintapocmessaging.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=UY6efbRVDvNzW1BL5a4soZB6qT1iZX/nI/Qgq6DGu98=";
            var topicName = "persontopic";

            Manage(serviceBusConnectionString);

            MessageBus messageBus = new MessageBus(new AzServiceBus(serviceBusConnectionString));
            var person = new { id = 0, firstName = "J", lastName = "Bond", doB = "26/04/1981" };
            messageBus.SendMessage(person, topicName);
        }

        private static void Manage(string serviceBusConnectionString)
        {
            var helper = new ManagementHelper(serviceBusConnectionString);

            bool done = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(">");
                string commandLine = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Magenta;
                string[] commands = commandLine.Split(' ');

                try
                {
                    if (commands.Length > 0)
                    {
                        switch (commands[0])
                        {
                            case "createqueue":
                            case "cq":
                                if (commands.Length > 1)
                                {
                                    helper.CreateQueueAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue path not specified.");
                                }
                                break;
                            case "listqueues":
                            case "lq":
                                helper.ListQueuesAsync().Wait();
                                break;
                            case "getqueue":
                            case "gq":
                                if (commands.Length > 1)
                                {
                                    helper.GetQueueAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue path not specified.");
                                }
                                break;
                            case "deletequeue":
                            case "dq":
                                if (commands.Length > 1)
                                {
                                    helper.DeleteQueueAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Queue path not specified.");
                                }
                                break;
                            case "createtopic":
                            case "ct":
                                if (commands.Length > 1)
                                {
                                    helper.CreateTopicAsync(commands[1]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Topic path not specified.");
                                }
                                break;
                            case "createsubscription":
                            case "cs":
                                if (commands.Length > 2)
                                {
                                    helper.CreateSubscriptionAsync(commands[1], commands[2]).Wait();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Topic path not specified.");
                                }
                                break;
                            case "listtopics":
                            case "lt":
                                helper.ListTopicsAndSubscriptionsAsync().Wait();
                                break;
                            case "exit":
                                done = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }

            } while (!done);
        }
    }
}
