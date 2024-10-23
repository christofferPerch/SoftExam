namespace DeliveryService.Models
{
    public class DeliveryAgent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInformation { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; } // Enum for full-time, part-time
        public decimal BaseSalary { get; set; }
    }

    public enum EmploymentStatus
    {
        FullTime = 1,
        PartTime = 2
    }
}
