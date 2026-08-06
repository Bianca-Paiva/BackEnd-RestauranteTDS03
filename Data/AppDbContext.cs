using API_BackEndMobile.Models;
using Microsoft.EntityFrameworkCore;

namespace RestauranteTDS03.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}
