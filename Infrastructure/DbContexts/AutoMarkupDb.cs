using Domain.AccountEntity;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts
{
    public class AutoMarkupDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<StyleRule> StyleRules { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public AutoMarkupDb(DbContextOptions options) : base(options)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = AutoMarkupDB");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        //    modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        //}
    }
}
