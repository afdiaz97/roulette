using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
namespace roulette.Models
{
    public class RouletteContext : DbContext
    {
        public RouletteContext(DbContextOptions<RouletteContext> options)
            : base(options)
        {
        }

        public DbSet<Roulette> Roulettes { get; set; } = null!;
        public DbSet<Bet> Bets { get; set; } = null!;
    }
}
