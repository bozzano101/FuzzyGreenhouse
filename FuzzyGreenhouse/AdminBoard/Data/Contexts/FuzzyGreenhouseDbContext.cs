using AdminBoard.Models.FuzzyGreenHouse;
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

        public DbSet<Rule> Rule { get; set; }
        public DbSet<Set> Set { get; set; }
        public DbSet<Value> Value { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RuleConfiguration());
            modelBuilder.ApplyConfiguration(new SetConfiguration());
            modelBuilder.ApplyConfiguration(new ValueConfiguration());
        }
    }
}
