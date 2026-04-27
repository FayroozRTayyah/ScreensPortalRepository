using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Screens.Models;
namespace Screens.data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

       
        public DbSet<Image> images { get; set; }
        public DbSet<Screen>screens { get; set; }
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Image>()
       .HasOne(i => i.screen)
      .WithMany(s => s.Images)
      .HasForeignKey(i => i.imageScreenId);


        }


    
    }

}
