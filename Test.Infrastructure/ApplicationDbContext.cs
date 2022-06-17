using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;

namespace Test.Infrastructure
{
    public class ApplicationDbContext :  IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
             Database.EnsureCreated();
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<DateField> DateFields { get; set; }

        public DbSet<StringField> StringFields { get; set; }

        public DbSet<LongField> LongFields { get; set; }

        public DbSet<Field> Fields { get; set; }

        public DbSet<DocumentTemplate> DocumentTemplates { get; set; }

        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}