using System.ComponentModel.DataAnnotations;

namespace BlazorDemoApplication.Data
{
    public class UserRegister
    {
        [Key]
        public int RegId { get; set; }
        public string RegEmail { get; set; }
        public int RegConfirmPassword { get; set; }
        public string RegPassword { get; set; }
      
    }
}
