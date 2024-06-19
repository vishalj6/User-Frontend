using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UsersProject.Models
{
    public class UserPerson
    {
        [Key]
        public int userID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s\-']+$", ErrorMessage = "First Name can only contain letters, hyphens, apostrophes, and spaces")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression(@"^[a-zA-Z\s\-']+$", ErrorMessage = "Last Name can only contain letters, hyphens, apostrophes, and spaces")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email  { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string city { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters")]
        public string address { get; set; }

        public string? country { get; set; }

        public string? state { get; set; }
        //[Required(ErrorMessage = "Password is required")]
        public string? password { get; set; }

    }
}