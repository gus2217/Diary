using DiaryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Data
{
    public class DiaryDbContext : DbContext
    {
        public DiaryDbContext(DbContextOptions<DiaryDbContext> options) : base(options)
        {
        }

        public DbSet<DiaryEntry> DiaryEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DiaryEntry>().HasData(
                new DiaryEntry
                {
                    Id = 1,
                    Title = "Went Hiking",
                    Content = "Went Hiking with Joe",
                    Created = new DateTime(2024, 01, 01, 12, 00, 00),

                },
                new DiaryEntry
                {
                    Id = 2,
                    Title = "Wwent Shopping",
                    Content = "Went shopping with Joe",
                    Created = new DateTime(2024, 01, 01, 12, 00, 00),
                },
                new DiaryEntry
                { 
                    Id = 3,
                    Title = "Went Diving",
                    Content = "Went diving with Joe",
                    Created = new DateTime(2024, 01, 01, 12, 00, 00),
                }


                );
        }
    }
}
