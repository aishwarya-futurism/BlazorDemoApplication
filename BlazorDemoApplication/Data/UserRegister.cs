using System.ComponentModel.DataAnnotations;

namespace BlazorDemoApplication.Data
{
    public class UserRegister
    {
        [Key]
        public int RegId { get; set; }

        [Required]
        [EmailAddress]
        public string RegEmail { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
        public string RegPassword { get; set; } // Add this property

        [Required]
        [Compare(nameof(RegPassword))]
        public int  RegConfirmPassword { get; set; }
    }

}
