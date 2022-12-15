using ApiProject.Dtos.Character;
using ApiProject.Models;
using AutoMapper;

namespace ApiProject
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>().ReverseMap();
            CreateMap<Character, AddCharacterDto>().ReverseMap();
        }
    }
}
