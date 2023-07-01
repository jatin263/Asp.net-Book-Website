using Deepak_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Deepak_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<CartModel>()
                .Has*/
            modelBuilder.Entity<UserModel>().HasIndex(u=>u.Email).IsUnique();
        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
