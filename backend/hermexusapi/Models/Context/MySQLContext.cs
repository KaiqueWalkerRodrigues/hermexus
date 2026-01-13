using Microsoft.EntityFrameworkCore;
using hermexusapi.Models.Base;
using hermexusapi.Models;

namespace hermexusapi.Models.Context
{
    public class MySQLContext(DbContextOptions<MySQLContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sector> Sectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configures a Global Query Filter for Soft Delete on all supported entities.
            // This automatically adds "WHERE Deleted_at IS NULL" to all LINQ queries.
            modelBuilder.Entity<User>().HasQueryFilter(u => u.Deleted_at == null);
            modelBuilder.Entity<Role>().HasQueryFilter(r => r.Deleted_at == null);

            // Fluent API configuration for Role entity
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
                entity.HasKey(e => e.Id);
            });

            // Fluent API configuration for User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);
            });
        }

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
            // Retrieves all entities that have been added or modified in the current ChangeTracker session.
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var now = DateTime.Now;

                // 1. Automatically updates the "Updated_at" field for any modified or added entity.
                if (entry.Properties.Any(p => p.Metadata.Name == "Updated_at"))
                {
                    entry.Property("Updated_at").CurrentValue = now;
                }

                // 2. Automatically sets the "Created_at" field only when a new entity is first added.
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