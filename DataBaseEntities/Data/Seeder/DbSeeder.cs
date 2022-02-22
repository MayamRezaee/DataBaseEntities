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
            applicationDbContext.Countries.AddRange(new Models.ViewModel.Country[]
            {
                new Models.ViewModel.Country(){CountryName = "Sweden" },
                new Models.ViewModel.Country(){CountryName = "Iran" },
                new Models.ViewModel.Country(){CountryName = "France" },
                new Models.ViewModel.Country(){CountryName = "Germany" },
                new Models.ViewModel.Country(){CountryName = "Norway" }


            });

            applicationDbContext.SaveChanges();


            //Seeding cities in country
            var countryS = applicationDbContext.Countries.First(a => a.CountryName == "Sweden");
            applicationDbContext.Cities.AddRange(new Models.ViewModel.City[]{ 
                new Models.ViewModel.City(){CityName = "Stockholm",Country = countryS},
                new Models.ViewModel.City(){CityName = "Uppsala",Country = countryS}
            });
            applicationDbContext.SaveChanges();

            var countryI = applicationDbContext.Countries.First(a => a.CountryName == "Iran");
            applicationDbContext.Cities.AddRange(new Models.ViewModel.City[]{
                new Models.ViewModel.City(){CityName = "Tehran",Country = countryI},
                new Models.ViewModel.City(){CityName = "Isfahan",Country = countryI}
            });

            applicationDbContext.SaveChanges();

            var countryG = applicationDbContext.Countries.First(a => a.CountryName == "Germany");
            applicationDbContext.Cities.AddRange(new Models.ViewModel.City[]{
                new Models.ViewModel.City(){CityName = "Berlin",Country = countryG},
                new Models.ViewModel.City(){CityName = "Munich",Country = countryG }
            });
            applicationDbContext.SaveChanges();

            var countryN = applicationDbContext.Countries.First(a => a.CountryName == "Norway");
            applicationDbContext.Cities.AddRange(new Models.ViewModel.City[]{
                new Models.ViewModel.City(){CityName = "Oslo",Country = countryN},
                new Models.ViewModel.City(){CityName = "Arendal",Country = countryN}
            });

            applicationDbContext.SaveChanges();

            //Seeding Languages
            applicationDbContext.Languages.AddRange(new Language[] { 
                new Language(){LanguageName="Swedish"},            
                new Language(){LanguageName="English"},
                new Language(){LanguageName="Persian"}

            });

            //Seeding Persons
            List<Person> peopleToAdd = new List<Person> {
                new Person{Name="Maryam Rezaee",City=applicationDbContext.Cities.First(a=>a.CityName == "Stockholm"),PhoneNumber=123456789,Country=countryS},
                new Person{Name="Hampus Andersson",City=applicationDbContext.Cities.First(a=>a.CityName == "Berlin"),PhoneNumber=987654321,Country=countryG},
                new Person{Name="Hilda Ekman",City=applicationDbContext.Cities.First(a=>a.CityName == "Arendal"),PhoneNumber=987879998,Country=countryN},

            };

            applicationDbContext.People.AddRange(peopleToAdd);
            applicationDbContext.SaveChanges();

           
            //Seeding Languages to persons
            applicationDbContext.LanguagePerson.AddRange(new LanguagePerson[]{ 
                new LanguagePerson(){LanguageName="Swedish",Person = applicationDbContext.People.FirstOrDefault(a=>a.Name == "Maryam Rezaee")},
                new LanguagePerson(){LanguageName="English",Person = applicationDbContext.People.FirstOrDefault(a=>a.Name == "Maryam Rezaee")},
                new LanguagePerson(){LanguageName="Persian",Person = applicationDbContext.People.FirstOrDefault(a=>a.Name == "Maryam Rezaee")},
                new LanguagePerson(){LanguageName="English",Person = applicationDbContext.People.FirstOrDefault(a=>a.Name == "Hampus Andersson")},
                new LanguagePerson(){LanguageName="Swedish",Person = applicationDbContext.People.FirstOrDefault(a=>a.Name == "Hilda Ekman")},
                new LanguagePerson(){LanguageName="English",Person = applicationDbContext.People.FirstOrDefault(a=>a.Name == "Hilda Ekman")}
            });

            applicationDbContext.SaveChanges();
        }

    }
}
