using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvccoremigration.Models
{
    public class photo
    {
        [Key]
        public int photo_id { get; set; }
        public string filename { get; set; }
        public string filepath { get; set; }
        [NotMapped]
        public IFormFile fileobj { get; set; }
    }
}
