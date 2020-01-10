using JwtService.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtService.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base (options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> Tokens { get; set; }
        
    }
}
