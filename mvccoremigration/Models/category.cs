


using System.ComponentModel.DataAnnotations;

namespace mvccoremigration.Models
{
    public class category
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }

    }
}
