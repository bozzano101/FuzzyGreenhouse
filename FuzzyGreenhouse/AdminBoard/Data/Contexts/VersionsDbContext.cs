using AdminBoard.Models.FuzzyGreenHouse;
using Microsoft.EntityFrameworkCore;

namespace AdminBoard.Data.Contexts
{
    public class VersionsDbContext : DbContext
    {
        public VersionsDbContext()
        {

        }

        public VersionsDbContext(DbContextOptions<VersionsDbContext> options) : base(options)
        {

        }

        public DbSet<Version> Version { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VersionConfiguration());
        }
    }
}
