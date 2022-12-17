using ApiProject.Dtos.Character;
using ApiProject.Dtos.Fight;
using ApiProject.Dtos.Skill;
using ApiProject.Dtos.Weapon;
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
            CreateMap<Character, UpdateCharacterDto>().ReverseMap();
            CreateMap<Weapon, GetWeaponDto>().ReverseMap();
            CreateMap<Skill, GetSkillDto>().ReverseMap();
            CreateMap<Character, HighscoreDto>().ReverseMap();
        }
    }
}
