using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace AzureTopics
{
    public class InfrastructureManagement
    {
        public const string TopicPath = "Orders";

        public static NamespaceManager Namespace
        {
            get
            {
                return
                    NamespaceManager.CreateFromConnectionString(
                        CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString"));
            }
        }

        public static void CreateTopics()
        {

            if (Namespace.TopicExists(TopicPath))
            {
                //if topic exists delete it
                Namespace.DeleteTopic(TopicPath);
            }

            //Create the topic
            Namespace.CreateTopic(TopicPath);

            //Create subscription for all orders
            Namespace.CreateSubscription(TopicPath, "allOrdersSubscription");

            //Create subscriptions for regions
            Namespace.CreateSubscription(TopicPath, "usaSubscription", new SqlFilter("region='USA'"));
            Namespace.CreateSubscription(TopicPath, "euSubscription", new SqlFilter("region='EU'"));

            //Subscriptions for large orders, high value orders and loyal US customers

            Namespace.CreateSubscription(TopicPath, "largeOrderSubscription", new SqlFilter("items > 30"));
            Namespace.CreateSubscription(TopicPath, "highValueSubscription", new SqlFilter("value > 500"));
            Namespace.CreateSubscription(TopicPath, "loyaltySubscription",
                                         new SqlFilter("loyalty=true AND region='USA'"));

        }
    }

  
}
