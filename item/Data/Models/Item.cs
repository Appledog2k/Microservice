using System.ComponentModel.DataAnnotations;

namespace item.Data.Models
{
    public class Item
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Cost { get; set; }
    }
}