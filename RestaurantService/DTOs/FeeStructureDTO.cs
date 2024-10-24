namespace RestaurantService.DTOs
{
    public class FeeStructureDTO
    {
        public int Id { get; set; }
        public decimal MinimumOrderAmount { get; set; }
        public decimal MaximumOrderAmount { get; set; }
        public decimal FeePercentage { get; set; }
    }
}
