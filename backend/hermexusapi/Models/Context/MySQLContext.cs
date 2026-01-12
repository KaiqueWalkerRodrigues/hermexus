using Microsoft.EntityFrameworkCore;

namespace hermexusapi.Models.Context
{
    public class MySQLContext(DbContextOptions<MySQLContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            // Retrieves all entities that have been added or modified.
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var now = DateTime.Now;

                // 1. Checks if it's an insertion and if it has the "Updated_at" property.
                if (entry.Properties.Any(p => p.Metadata.Name == "Updated_at"))
                {
                    entry.Property("Updated_at").CurrentValue = now;
                }

                // 2. Check if it's an insertion and if it has the "Created_at" property.
                if (entry.State == EntityState.Added)
                {
                    if (entry.Properties.Any(p => p.Metadata.Name == "Created_at"))
                    {
                        entry.Property("Created_at").CurrentValue = now;
                    }
                }
            }
        }
    }
}
