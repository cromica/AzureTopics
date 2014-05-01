using System;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace AzureTopics.Orders
{
    public class OrderCommand
    {
        public static List<Order> CreateOrders()
        {
            var orders = new List<Order>
                {
                    new Order()
                        {
                            HasLoyaltyCard = true,
                            Items = 1,
                            Name = "Loyal Customer",
                            Region = "USA",
                            Value = 19.99m
                        },
                    new Order()
                        {
                            HasLoyaltyCard = false,
                            Items = 50,
                            Name = "Large Order",
                            Region = "USA",
                            Value = 49.99m
                        },
                    new Order()
                        {
                            HasLoyaltyCard = false,
                            Items = 45,
                            Name = "High Value",
                            Region = "USA",
                            Value = 749.45m
                        },
                    new Order()
                        {
                            HasLoyaltyCard = true,
                            Items = 3,
                            Name = "Loyal Europe Order",
                            Region = "EU",
                            Value = 49.45m
                        },
                    new Order()
                        {
                            HasLoyaltyCard = false,
                            Items = 3,
                            Name = "UK Order",
                            Region = "UK",
                            Value = 49.95m
                        },
                };

            return orders;
        }

        public void PlaceOrders()
        {
            //create orders
            var orders = CreateOrders();

            orders.ForEach(SendOrder);
        }

        public void SendOrder(Order order)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Sending {0} ...", order.Name);

            //create a message from the order
            var orderMsg = new BrokeredMessage(order);

            orderMsg.SetProperties(order.Properties());

            //send the message via SB
            var topicClient = TopicClient.Create(InfrastructureManagement.TopicPath);
            topicClient.Send(orderMsg);
            
            Console.WriteLine("Done sending {0} !", order.Name);
            Console.ResetColor();
        }
    }
}