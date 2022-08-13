using Microsoft.AspNetCore.Mvc;
using rpg.Dtos.Character;
using rpg.Services.CharacterService;

namespace rpg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Get(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Add(AddCharacterDto charcter)
        {
            return Ok(await _characterService.AddCharacter(charcter));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Update(int id, UpdateCharacterDto character)
        {
            var response = await _characterService.UpdateCharacter(id, character);

            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
        {

            var response = await _characterService.DeleteCharacter(id);

            if (response.Data == null)
            {
                return NotFound(response);
            }


            return Ok(response);
        }
    }
}
