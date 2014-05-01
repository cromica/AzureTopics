namespace AzureTopics.Orders
{
    public class Order
    {
        public string Name { get; set; }

        public bool HasLoyaltyCard { get; set; }

        public int Items { get; set; }

        public decimal Value { get; set; }

        public string Region { get; set; }
    }
}