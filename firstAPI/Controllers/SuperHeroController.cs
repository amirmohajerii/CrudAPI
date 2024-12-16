using firstAPI.Data;
using firstAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //   public async Task<IActionResult> GetAllRecords()
        public async Task<ActionResult<List<SuperHero>>> GetAllRecords()
        {
            var records = await _context.SuperHeroes.ToListAsync();

            return Ok(records);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetRecord(int id)
        {
            var record = await _context.SuperHeroes.FindAsync(id);
            if(record is null)
                return NotFound("Hero Not Found");
            return Ok(record);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddRecord(SuperHero hero)
        {
         _context.SuperHeroes.Add(hero);
         await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateRecord(SuperHero updatedHero)
        {
            var dbRecord = await _context.SuperHeroes.FindAsync(updatedHero.Id);
            if (dbRecord is null)
                return NotFound("Hero Not Found");
            dbRecord.name = updatedHero.name;
            dbRecord.FirstName = updatedHero.FirstName;
            dbRecord.LastName = updatedHero.LastName;
            dbRecord.Place = updatedHero.Place;
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteRecord(int id)
        {
            var dbRecord = await _context.SuperHeroes.FindAsync(id);
            if (dbRecord is null)
                return NotFound("Hero Not Found");
            _context.SuperHeroes.Remove(dbRecord);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

    }
}
