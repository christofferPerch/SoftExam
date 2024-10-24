using System.ComponentModel.DataAnnotations;

namespace CustomerService.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        public required string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        public required string City { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        [MaxLength(10, ErrorMessage = "Invalid zip code length")]
        public required string ZipCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public Country Country { get; set; }
    }
}
