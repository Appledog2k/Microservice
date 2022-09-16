namespace item.Data.Models
{
    public class CreateItemDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Cost { get; set; }

    }
    public class UpdateItemDTO : CreateItemDTO
    {

    }
    public class ItemDTO : CreateItemDTO
    {
        public int Id { get; set; }

    }
}