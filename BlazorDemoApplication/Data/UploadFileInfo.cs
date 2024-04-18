using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorDemoApplication.Data
{
    public class UploadFileInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileId { get; set; }   
        public int UserId { get; set; }
        public string FileName { get; set; }
        public byte[] Files { get; set; }
        public string ContentType { get; set; }
        public string Base64File { get; set; }
    }
}
