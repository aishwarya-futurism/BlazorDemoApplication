using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string ProfileImage { get; set; }
        public List<UploadFileInfo> MultiFilesUpload { get; set; }
        public string Address { get; set; }
        // Foreign keys
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

        // Navigation properties
        public Country Country { get; set; }
        public State State { get; set; }
        public City City { get; set; }
    }
}
