using System;
using Microsoft.ServiceBus.Messaging;

namespace AzureTopics.Orders
{
    internal class OrderSubscriptionQuery
    {
        public void ReceiveOrder()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Receiving information from {0} subscriptions", InfrastructureManagement.TopicPath);

            //loop to all topic subscriptions
            foreach (var subsDescription in InfrastructureManagement.Namespace.GetSubscriptions(InfrastructureManagement.TopicPath))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Receiving information from subscription {0} ...", subsDescription.Name);

                //Create subscription client
                var subsClient = SubscriptionClient.Create(InfrastructureManagement.TopicPath, subsDescription.Name);

                //Receive all the messages from the subscription
                Console.ForegroundColor = ConsoleColor.Green;
                
                //read subscription message
                ReadMessageValue(subsClient);

                subsClient.Close();
            }

            Console.ResetColor();
        }

        private void ReadMessageValue(SubscriptionClient subsClient)
        {
            if (subsClient == null) throw new ArgumentNullException("subsClient");
            while (true)
            {
                //Receive any message with a one second timeout
                var msg = subsClient.Receive(TimeSpan.FromSeconds(1));
                if (msg != null)
                {
                    //Deserialize the message body
                    var order = msg.GetBody<Order>();

                    ProcessOrder(order);

                    //Mark the message as complete
                    msg.Complete();
                }
                else
                {
                    break;
                }
            }
        }

        private void ProcessOrder(Order order)
        {
            Console.WriteLine("Name {0} {1} items {2} ${3} {4}", order.Name, order.Region, order.Items,
                              order.Value, order.HasLoyaltyCard ? "Loyal" : "Not Loyal");
        }
    }
}