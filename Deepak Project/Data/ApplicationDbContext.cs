using Deepak_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Deepak_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        public DbSet<BookModel> Books { get; set; }
    }
}
