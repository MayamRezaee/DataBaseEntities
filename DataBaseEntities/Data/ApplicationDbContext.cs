using DataBaseEntities.Models;
using DataBaseEntities.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DataBaseEntities.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Person> People { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguagePerson> LanguagePerson { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
               



        /*    modelBuilder.Entity<Country>().HasData(
       new Country
       {
           CountryName = "Iran",

       });

            modelBuilder.Entity<City>().HasData(new { CountryName = "Iran", CityName = "Tehran" },
                new { CountryName = "Iran", CityName = "Isfahan" },
                new { CountryName = "Iran", CityName = "Mashhad" },
                new { CountryName = "Iran", CityName = "Kerman" });
*/



            /*modelBuilder.Entity<Country>().HasData(new Country { CountryName = "Iran" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryName = "Sweden" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryName = "France" });

            modelBuilder.Entity<City>().HasData(new City { CityName = "Tehran" });
            modelBuilder.Entity<City>().HasData(new City { CityName = "Tehran" });
            modelBuilder.Entity<City>().HasData(new City { CityName = "Tehran" });
*/




        }
    }
}
