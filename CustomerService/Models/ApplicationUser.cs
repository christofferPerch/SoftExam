using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CustomerService.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public required string City { get; set; }

        [Required]
        [MaxLength(10)]
        public required string ZipCode { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Country { get; set; }
    }

}