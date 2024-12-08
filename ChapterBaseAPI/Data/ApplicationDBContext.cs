using ChapterBaseAPI.Models;
using Microsoft.EntityFrameworkCore;



namespace ChapterBaseAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Users> Users{ get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Banner?> Banners { get; set; }
        
    }
}
