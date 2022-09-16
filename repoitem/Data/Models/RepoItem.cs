using System.ComponentModel.DataAnnotations;

namespace repoitem.Data.Models
{
    public class RepoItem
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