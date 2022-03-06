using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roulette.Models;

namespace roulette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoulettesController : ControllerBase
    {
        private readonly RouletteContext _context;

        public RoulettesController(RouletteContext context)
        {
            _context = context;
        }

        // GET: api/Roulettes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roulette>>> GetRoulettes()
        {
            return await _context.Roulettes.ToListAsync();
        }

        // GET: api/Roulettes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roulette>> GetRoulette(long id)
        {
            var roulette = await _context.Roulettes.FindAsync(id);

            if (roulette == null)
            {
                return NotFound();
            }

            return roulette;
        }

        

        // POST: api/Roulettes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Roulette>> PostRoulette()
        {
            Roulette roulette = new Roulette();
            roulette.Status = "close";
            _context.Roulettes.Add(roulette);
            await _context.SaveChangesAsync();

            return  roulette;
        }
    }
}
