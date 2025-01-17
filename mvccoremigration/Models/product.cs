using System.ComponentModel.DataAnnotations;

namespace mvccoremigration.Models
{
    public class product
    {
        [Key]
        public int p_id { get; set; }
        [Required]
        public string pro_name { get; set; }
        [Required]
        public int id{ get; set; }

    }
}
