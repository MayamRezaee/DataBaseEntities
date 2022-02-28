using DataBaseEntities.Models;
using DataBaseEntities.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataBaseEntities.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguagePerson> LanguagePerson { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>()
            .HasMany(c => c.People)
            .WithOne(e => e.City);
           

            modelBuilder.Entity<Country>()
            .HasMany(c => c.Cities)
            .WithOne(e => e.Country);
           


            modelBuilder.Entity<LanguagePerson>()
            .HasKey(sc => new { sc.LanguageName, sc.PersonId });


            modelBuilder.Entity<LanguagePerson>()
                .HasOne<Language>(sc => sc.Language)
                .WithMany(s => s.LanguagePerson)
                .HasForeignKey(sc => sc.LanguageName);




            modelBuilder.Entity<LanguagePerson>()
                .HasOne<Person>(sc => sc.Person)
                .WithMany(s => s.LanguagePerson)
                .HasForeignKey(sc => sc.PersonId);


            string roleId = Guid.NewGuid().ToString();
            string  userRoleId = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                { 
                    Id = roleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {

                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER"
            });

            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = userId,
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.Com",
                PasswordHash = hasher.HashPassword(null, "password"),
                FirstName = "Admin",
                LastName = "Admini",
                BirthDay = new DateTime( 1995, 10, 10)

            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId

            });
        }
    }
}
