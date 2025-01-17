
using System.ComponentModel.DataAnnotations;

namespace mvccoremigration.Models
{
    public class register
    {
        [Key]
        public int r_id { get; set; }
        [Required]
        public string r_name { get; set;}
        [Required]
        public string r_des { get; set; }
    }
}
