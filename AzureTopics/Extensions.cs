using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureTopics.Orders;
using Microsoft.ServiceBus.Messaging;

namespace AzureTopics
{
    internal static class Extensions
    {
        internal static IDictionary<string,object> Properties(this Order order)
        {
            IDictionary<string, object> properties = new Dictionary<string, object>
                {
                    {"loyalty", order.HasLoyaltyCard},
                    {"items", order.Items},
                    {"value", order.Value},
                    {"region",order.Region}
                };
            return properties;
        }

        internal static void SetProperties(this BrokeredMessage message, IDictionary<string,object> properties)
        {
            foreach (var property in properties)
            {
                message.Properties.Add(property);
            }
        }

    }
}
