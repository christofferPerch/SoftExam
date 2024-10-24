namespace RestaurantService.DTOs
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInformation { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
        public List<MenuItemDTO> MenuItems { get; set; }
        public List<FeeStructureDTO> FeeStructures { get; set; }
    }
}
