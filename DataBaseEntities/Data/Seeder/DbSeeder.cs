using DataBaseEntities.Models;
using DataBaseEntities.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Data.Seeder
{
    public class DbSeeder : ISeeder
    {
        ApplicationDbContext applicationDbContext;
        public DbSeeder(ApplicationDbContext dbContext) {
            applicationDbContext = dbContext;
        }

        public void Seed()
        {
            if(applicationDbContext.Countries.FirstOrDefault(a=>a.CountryName=="Sweden") != null)
            {
                return;
            }

            //Seeding country
            applicationDbContext.Countries.Add(new Models.ViewModel.Country()
            {
                CountryName = "Sweden"
            });

            applicationDbContext.SaveChanges();

            //Seeding cities in country
            var country = applicationDbContext.Countries.First(a => a.CountryName == "Sweden");
            applicationDbContext.Cities.AddRange(new Models.ViewModel.City[]{ 
                new Models.ViewModel.City(){CityName = "Stockholm",Country = country},
                new Models.ViewModel.City(){CityName = "Uppsala",Country = country}
            });

            applicationDbContext.SaveChanges();
            

            //Seeding Languages
            applicationDbContext.Languages.AddRange(new Language[] { 
                new Language(){LanguageName="Swedish"},            
                new Language(){LanguageName="English"},
                new Language(){LanguageName="Chinese"}
            });

            //Seeding Persons
            List<Person> peopleToAdd = new List<Person> {
                new Person{Name="Maryam Rezaee",City=applicationDbContext.Cities.First(a=>a.CityName == "Stockholm"),PhoneNumber=123456789,Country=country},
                new Person{Name="Hampus Andersson",City=applicationDbContext.Cities.First(a=>a.CityName == "Uppsala"),PhoneNumber=987654321,Country=country},
            };

            applicationDbContext.People.AddRange(peopleToAdd);
            applicationDbContext.SaveChanges();

           
            //Seeding Languages to persons
            applicationDbContext.LanguagePerson.AddRange(new LanguagePerson[]{ 
                new LanguagePerson(){LanguageName="Swedish",Person = applicationDbContext.People.FirstOrDefault(a=>a.Name == "Maryam Rezaee")},
                new LanguagePerson(){LanguageName="English",Person = applicationDbContext.People.FirstOrDefault(a=>a.Name == "Maryam Rezaee")},
                new LanguagePerson(){LanguageName="Chinese",Person = applicationDbContext.People.FirstOrDefault(a=>a.Name == "Hampus Andersson")}
            });

            applicationDbContext.SaveChanges();
        }

    }
}
