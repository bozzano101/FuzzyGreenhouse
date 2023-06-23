public class FuzzyGreenhouseDbContext : DbContext
    {
        public FuzzyGreenhouseDbContext() { }

        public FuzzyGreenhouseDbContext(DbContextOptions<FuzzyGreenhouseDbContext> options) : base(options) { }

        public DbSet<Rule> Rule { get; set; }
        public DbSet<Set> Set { get; set; }
        public DbSet<Value> Value { get; set; }
        public DbSet<Version> Version { get; set; }
        public DbSet<Subsystem> Subsystem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RuleConfiguration());
            modelBuilder.ApplyConfiguration(new SetConfiguration());
            modelBuilder.ApplyConfiguration(new ValueConfiguration());
            modelBuilder.ApplyConfiguration(new VersionConfiguration());
            modelBuilder.ApplyConfiguration(new SubsystemConfiguration());
        }
    }