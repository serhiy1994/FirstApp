using Microsoft.EntityFrameworkCore;
using FirstApp.Models;

namespace FirstApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Activity> Activity { get; set; }
    }
}
