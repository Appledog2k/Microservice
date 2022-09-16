using AutoMapper;
using item.Data.Models;

namespace repoitem.Configuration
{
    public class ConfigMapper : Profile
    {
        public ConfigMapper()
        {

            CreateMap<Item, CreateItemDTO>().ReverseMap();
            CreateMap<Item, UpdateItemDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
        }
    }
}