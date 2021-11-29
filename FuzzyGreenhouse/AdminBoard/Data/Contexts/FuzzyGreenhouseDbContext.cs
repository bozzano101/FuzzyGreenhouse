using AdminBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminBoard.Data
{
    public class FuzzyGreenhouseDbContext : DbContext
    {
        public FuzzyGreenhouseDbContext()
        {

        }

        public FuzzyGreenhouseDbContext(DbContextOptions<FuzzyGreenhouseDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
