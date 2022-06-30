using AppBayBack.Models;
using Microsoft.EntityFrameworkCore;

namespace AppBayBack.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Feature> Features { get; set; }
    }
}
