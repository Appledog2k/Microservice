namespace repoitem.Data.Models
{
    public class CreateRepoItemDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Cost { get; set; }

    }
    public class UpdateRepoItemDTO : CreateRepoItemDTO
    {

    }
    public class RepoItemDTO : CreateRepoItemDTO
    {
        public int Id { get; set; }

    }
}