using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CiqualAPI.Models;

namespace CiqualAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Aliments")]
    public class AlimentsController : Controller
    {
        private readonly CiqualContext _context;

        public AlimentsController(CiqualContext context)
        {
            _context = context;
        }

        // GET: api/Aliments
        [HttpGet]
        public IEnumerable<Aliment> GetAliments()
        {
            return _context.Aliment;
        }

        // GET: api/Aliments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAliments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var aliment = await _context.Aliment.Where(m => m.IdAliment == id)
  .Include(m => m.Composition).ThenInclude(c => c.IdConstituantNavigation).AsNoTracking().ToListAsync();

            if (aliment == null)
            {
                return NotFound();
            }


            return Ok(aliment);
        }

        // PUT: api/Aliments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAliment([FromRoute] int id, [FromBody] Aliment aliment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aliment.IdAliment)
            {
                return BadRequest();
            }

            _context.Entry(aliment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlimentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Aliments
        [HttpPost]
        public async Task<IActionResult> PostAliment([FromBody] Aliment aliment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Aliment.Add(aliment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AlimentExists(aliment.IdAliment))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAliment", aliment.IdAliment, aliment);
        }

        // DELETE: api/Aliments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAliment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aliment = await _context.Aliment.SingleOrDefaultAsync(m => m.IdAliment == id);
            if (aliment == null)
            {
                return NotFound();
            }

            _context.Aliment.Remove(aliment);
            await _context.SaveChangesAsync();

            return Ok(aliment);
        }

        private bool AlimentExists(int id)
        {
            return _context.Aliment.Any(e => e.IdAliment == id);
        }
    }
}