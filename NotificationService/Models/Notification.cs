namespace NotificationService.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Message { get; set; } 
        public NotificationMethod NotificationMethod { get; set; } 
        public DateTime Timestamp { get; set; } 
    }

    public enum NotificationMethod
    {
        Email = 1,
        SMS = 2
    }
}
