using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using static MudBlazor.CategoryTypes;

namespace BlazorDemoApplication.Data
{
    public class User 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    
        public string ProfileImage { get; set; }
        public List<UploadFileInfo> MultiFilesUpload { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        // Foreign keys
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        // Navigation properties
        [Required]
        public Country Country { get; set; }
        [Required]
        public State State { get; set; }
        [Required]
        public City City { get; set; }
    }
}
