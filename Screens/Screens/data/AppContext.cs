using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Screens.Models;
namespace Screens.data
{
    public class AppContext:DbContext 
    {
        public DbSet<Image> images { get; set; }
        public AppContext()
        {
        }

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


           
        }


    
    }

}
