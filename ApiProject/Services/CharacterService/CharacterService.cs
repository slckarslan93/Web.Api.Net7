using ApiProject.Models;

namespace ApiProject.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Id  =1,Name = "Sam"}
        };

        
        public List<Character> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        public List<Character> GetAllCharacters()
        {
            return characters;
        }

        public Character GetCharacterById(int id)
        {
            var charcater = characters.FirstOrDefault(c => c.Id == id); 
            if (charcater is not null)
            {
                return charcater;
            }
            throw new Exception("Character not found");
        }
    }
}
