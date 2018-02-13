namespace Watermelons.Repository
{
    using EntityFramework;
    using System.Data.Entity;
    using System.Diagnostics;

    /// <summary>
    /// The fit cloud commissions context.
    /// </summary>
    public partial class WatermelonDbContext : DbContext
    {
        /// <summary>
        /// Initializes static members of the <see cref="WatermelonDbContext"/> class.
        /// </summary>
        static WatermelonDbContext()
        {
            Database.SetInitializer<WatermelonDbContext>(null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WatermelonDbContext"/> class.
        /// </summary>
        public WatermelonDbContext()
            : base("Name=WatermelonConnectionString")
        {
            this.Database.CommandTimeout = 180;
            this.Database.Log = sql => Debug.Write(sql);
        }

        public DbSet<CompetitionEntry> CompetitionEntry { get; set; }
        

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Configurations.Add(new Account_UncommissionableMap());
        }
    }
}
