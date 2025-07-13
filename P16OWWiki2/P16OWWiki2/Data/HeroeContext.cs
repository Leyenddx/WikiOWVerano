using Microsoft.EntityFrameworkCore;
using P16OWWiki2.Models;

namespace P16OWWiki2.Data
{
    public class HeroeContext : DbContext
    {
        public HeroeContext(DbContextOptions<HeroeContext> options) : base(options) { }
        public DbSet<Heroe> HeroeSet { get; set; }
    }
}
