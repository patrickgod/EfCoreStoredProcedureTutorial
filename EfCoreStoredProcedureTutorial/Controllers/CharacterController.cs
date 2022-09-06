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
    }
}
