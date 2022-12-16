using ApiProject.Data;
using ApiProject.Dtos.Character;
using ApiProject.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiProject.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper,DataContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == GetUserId());
        
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters.Where(x=>x.User!.Id == GetUserId()).Select(x => _mapper.Map<GetCharacterDto>(x)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {

                var characater =  await _context.Characters.FirstOrDefaultAsync(x => x.Id == id && x.User!.Id == GetUserId());

                if (characater is null)
                {
                    throw new Exception($"Character with Id '{id}' not found.");
                }
                _context.Characters.Remove(characater);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Characters.Where(x=>x.User!.Id == GetUserId()).Select(x => _mapper.Map<GetCharacterDto>(x)).ToListAsync();
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.Include(x => x.Weapon).Include(x => x.Skills).Where(x=>x.User!.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(x => _mapper.Map<GetCharacterDto>(x)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacater = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacater);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {

                var characater = await _context.Characters.Include(x=>x.User).FirstOrDefaultAsync(x => x.Id == updatedCharacter.Id);

                if (characater is null || characater.User!.Id != GetUserId())
                {
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");
                }
                

                characater.Name = updatedCharacter.Name;
                characater.HitPoints = updatedCharacter.HitPoints;
                characater.Strenght = updatedCharacter.Strenght;
                characater.Defense = updatedCharacter.Defense;
                characater.Intelligence = updatedCharacter.Intelligence;
                characater.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(characater);
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters.Include(x=>x.Weapon).Include(x=>x.Skills).FirstOrDefaultAsync(x => x.Id == newCharacterSkill.CharacterId && x.User!.Id == GetUserId());
                if (character is null)
                {
                    response.Success = false;
                    response.Message = "Character not Found";
                    return response;
                }

                var skill = await _context.Skills.FirstOrDefaultAsync(x => x.Id == newCharacterSkill.SkillId);
                if (character is null)
                {
                    response.Success=false;
                    response.Message = "Skill not found";
                    return response;
                }

                character.Skills!.Add(skill);
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
