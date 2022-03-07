using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roulette.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        

       [HttpPost]
        public async Task<ActionResult<Roulette>> PostRoulette()
        {
            Roulette roulette = new Roulette();
            roulette.Status = "close";
            _context.Roulettes.Add(roulette);
            await _context.SaveChangesAsync();

            return  roulette;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoulette(long id)
        {
            
                if (!RouletteExists(id))
                {
                    return NotFound();
                }
            var roulette = await _context.Roulettes.FindAsync(id);
            roulette.Status = "open";
            try
            {
                await _context.SaveChangesAsync();
                string status = "ruleta abierta";
                return Content(status);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NoContent();
            }
        }
        private bool RouletteExists(long id)
        {
            return _context.Roulettes.Any(e => e.Id == id);
        }
        [HttpPost("betcolor/{idRoulette}/{money}/{color}")]
        public async Task<ActionResult<Bet>> PostBet(long idRoulette,int money,string color)
        {
            if (!RouletteExists(idRoulette))
            {
                string status = "ruleta no existe";
                return Content(status);
            }
            var roulette = await _context.Roulettes.FindAsync(idRoulette);
            if (roulette.Status == "close")
            {
                string status = "ruleta cerrada";
                return Content(status);
            }
            if (money > 10000)
            {
                string status = "Apuesta mayor a lo permitida";
                return Content(status);
            }
            if (color != "rojo" && color != "negro")
            {
                string status = "Color no valido";
                return Content(status);
            }
            Bet bet = new Bet();
            bet.number = 100;
            bet.money = money;
            bet.color = color;

            _context.Bets.Add(bet);
            await _context.SaveChangesAsync();
            return bet;
        }
        [HttpPost("betnumber/{idRoulette}/{money}/{number}")]
        public async Task<ActionResult<Bet>> PostBetcolor(long idRoulette, int money, int number)
        {
            if (!RouletteExists(idRoulette))
            {
                string status = "ruleta no existe";
                return Content(status);
            }
            var roulette = await _context.Roulettes.FindAsync(idRoulette);
            if (roulette.Status == "close")
            {
                string status = "ruleta cerrada";
                return Content(status);
            }
            if (money > 10000)
            {
                string status = "Apuesta mayor a lo permitida";
                return Content(status);
            }
            if (number <0 || number >36)
            {
                string status = "Numero no valido";
                return Content(status);
            }
            Bet bet = new Bet();
            bet.number = number;
            bet.money = money;

            _context.Bets.Add(bet);
            await _context.SaveChangesAsync();
            return bet;
        }
    }
}
