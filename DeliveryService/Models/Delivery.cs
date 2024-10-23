namespace DeliveryService.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public int OrderId { get; set; } 
        public int DeliveryAgentId { get; set; } 
        public DateTime DeliveryStartTime { get; set; }
        public DateTime DeliveryEndTime { get; set; }
        public DeliveryStatus Status { get; set; } 
        public decimal BonusAmount { get; set; } 
    }

    public enum DeliveryStatus
    {
        Pending = 1,
        InTransit = 2,
        Delivered = 3,
        Failed = 4
    }
}
