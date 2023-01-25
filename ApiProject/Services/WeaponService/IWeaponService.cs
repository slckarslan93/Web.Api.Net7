using ApiProject.Dtos.Character;
using ApiProject.Dtos.Weapon;
using ApiProject.Models;

namespace ApiProject.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}