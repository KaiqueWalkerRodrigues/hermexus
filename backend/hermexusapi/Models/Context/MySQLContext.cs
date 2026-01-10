using Microsoft.EntityFrameworkCore;

namespace hermexusapi.Models.Context
{
    public class MySQLContext(DbContextOptions<MySQLContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
