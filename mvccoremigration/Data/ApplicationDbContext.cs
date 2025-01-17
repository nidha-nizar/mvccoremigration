
using Microsoft.EntityFrameworkCore;
using mvccoremigration.Models;

namespace mvccoremigration.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<category> categories { get; set; }
        public DbSet<register> reg_tb { get; set; }
        public DbSet<product> products { get; set; }
        public DbSet<photo> file_tb { get; set; }
    }
}
