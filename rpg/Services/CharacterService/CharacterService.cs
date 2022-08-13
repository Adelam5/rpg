using AutoMapper;
using rpg.Dtos.Character;

namespace rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static readonly List<Character> characters = new()
        {
        new Character(),
        new Character{Id = 1, Name = "Sam", Class = RpgClass.Mage, Intelligence = 15, HitPoints= 110 },
        };

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character newCharacter = _mapper.Map<Character>(character);
            newCharacter.Id = characters.Max(c => c.Id) + 1;
            characters.Add(newCharacter);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>> { Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList() };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters[id];
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(int id, UpdateCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var characterToUpdate = characters[id];

                // _mapper.Map(character, characterToUpdate);

                characterToUpdate.Name = character.Name;
                characterToUpdate.HitPoints = character.HitPoints;
                characterToUpdate.Strength = character.Strength;
                characterToUpdate.Defense = character.Defense;
                characterToUpdate.Intelligence = character.Intelligence;
                characterToUpdate.Class = character.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(characterToUpdate);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            var characterToDelete = characters[id];
            characters.Remove(characterToDelete);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
    }
}
