namespace PaymentService.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; } 
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string PaymentMethod { get; set; } 
        public string Currency { get; set; }
        public DateTime Timestamp { get; set; } 
    }

    public enum PaymentStatus
    {
        Paid,
        Failed,
        Pending
    }
}
