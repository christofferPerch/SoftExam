namespace FeedbackService.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int OrderId { get; set; } 
        public int CustomerId { get; set; } 
        public int DeliveryAgentId { get; set; } 
        public int FoodRating { get; set; } 
        public int DeliveryExperienceRating { get; set; } 
        public int DeliveryAgentRating { get; set; } 
        public string Comments { get; set; } 
        public DateTime FeedbackTimestamp { get; set; }
    }
}
