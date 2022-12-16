using ApiProject.Data;
using ApiProject.Dtos.Character;
using ApiProject.Dtos.Weapon;
using ApiProject.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiProject.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context,IHttpContextAccessor httpContextAccessor,IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == newWeapon.CharacterId && x.User!.Id == int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!));
                if (character == null)
                {
                    response.Success= false;
                    response.Message = "Character not found.";
                    return response;
                }

                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character,
                };

                _context.Weapons.Add(weapon);   
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }
    }
}
