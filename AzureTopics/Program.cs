using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureTopics.Orders;

namespace AzureTopics
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Creating topics and subscriptions ... ");

            //create predefined topics for the sample application
            InfrastructureManagement.CreateTopics();
            
            Console.WriteLine("Done!");
            Console.ResetColor();

            WaitForEnter("Press enter to send the messages");

            //place orders
            var command = new OrderCommand();
            command.PlaceOrders();

            WaitForEnter("Press enter to receive messages");

            var orderSubscriptionQuery = new OrderSubscriptionQuery();
            orderSubscriptionQuery.ReceiveOrder();

            WaitForEnter("Press any key to continue");
        }

        private static void WaitForEnter(string waitingMessage,bool supressWaitMesasge = false)
        {
            if (!supressWaitMesasge)
            {
                Console.WriteLine(waitingMessage);
            }
            var keyInfo =  Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                return;
            }
            WaitForEnter(string.Empty, true);
        }
    }

  
}
