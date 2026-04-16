using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Screens.Models;
namespace Screens.data
{
    public class AppDbContext : DbContext 
    {
        public DbSet<Image> images { get; set; }
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


           
        }


    
    }

}
