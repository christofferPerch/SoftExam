namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; } 
        public int DeliveryAgentId { get; set; } 
        public decimal TotalAmount { get; set; }
        public decimal VATAmount { get; set; }
        public DateTime OrderPlacedTimestamp { get; set; }
        public OrderStatusEnum Status { get; set; } 
        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatusEnum
    {
        Pending = 1,
        InPreparation = 2,
        OutForDelivery = 3,
        Delivered = 4
    }
}
