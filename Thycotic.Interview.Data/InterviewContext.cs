using Microsoft.EntityFrameworkCore;
using Thycotic.Interview.Data.Models;

namespace Thycotic.Interview.Data
{
    public class InterviewContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public string DbPath { get; }

        public InterviewContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "interview.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.UserName)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}