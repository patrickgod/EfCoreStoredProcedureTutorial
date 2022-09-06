using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EfCoreStoredProcedureTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly dotnetrpgContext _context;

        public CharacterController(dotnetrpgContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetAllCharacters()
        {
            var result = await _context.Characters.ToListAsync();

            return Ok(result);
        }

        [HttpGet("SP")]
        public async Task<ActionResult<List<Character>>> GetAllCharactersSP()
        {
            var result = await _context.Characters.FromSqlRaw("SelectAllCharacters").ToListAsync();

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Character>>> GetCharacters(int userId)
        {
            var result = await _context.Characters.FromSqlRaw($"SelectUserCharacters {userId}").ToListAsync();

            return Ok(result);
        }

        [HttpGet("hitpoints/{characterId}/{hitpoints}")]
        public async Task<ActionResult<int>> UpdateCharacterHitpoints(int characterId, int hitpoints)
        {
            var result = await _context.Database
                .ExecuteSqlRawAsync($"UpdateCharacterHitpoints {characterId}, {hitpoints}");

            return Ok(result);
        }
    }
}
