using AutoMapper;
using TryitterApi.Models;

namespace TryitterApi.DTOs.Mappings
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}