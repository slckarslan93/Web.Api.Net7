using ApiProject.Dtos.Fight;
using ApiProject.Models;

namespace ApiProject.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);

        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);

        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);

        Task<ServiceResponse<List<HighscoreDto>>> GetHighscore();
    }
}