using AutoMapper;
using repoitem.Data.Models;

namespace repoitem.Configuration
{
    public class ConfigMapper : Profile
    {
        public ConfigMapper()
        {

            CreateMap<RepoItem, CreateRepoItemDTO>().ReverseMap();
            CreateMap<RepoItem, UpdateRepoItemDTO>().ReverseMap();
            CreateMap<RepoItem, RepoItemDTO>().ReverseMap();
        }
    }
}