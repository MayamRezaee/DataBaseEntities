using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseEntities.Data;
using DataBaseEntities.Models;
using DataBaseEntities.Models.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace DataBaseEntities.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListPeople()
        {
            var cityLanguageCountry = _context.People
                .Include(a => a.City)
                .Include(k => k.Country)
                .Include(a=>a.LanguagePerson)
                .ToList();
            //return View(_context.People.Include(a => a.City), (l => l.Language).ToList());
            return View(cityLanguageCountry);

        }

        public IActionResult AddPerson()
        {
            ViewBag.Cities = _context.Cities.ToList();
            ViewBag.Languages = _context.Languages.ToList();
            ViewBag.Countries = _context.Countries.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddPerson(PersonCreationViewModel person)
        {
            if (ModelState.IsValid)
            {
                City city = _context.Cities.Where(a => a.CityName == person.CityName).Include(a=>a.People).FirstOrDefault();
                // O O O O O O O O O O O O
                //     b
                // farsi english chinese france
                IEnumerable<Language> languages = _context.Languages.Where(b => person.LanguageName.Contains(b.LanguageName));
                Country country = _context.Countries.FirstOrDefault(k => k.CountryName == person.CountryName);
                /*if(city == null)
                {
                    // log
                    // statusCode = 422
                    return UnprocessableEntity("What! from where do you bring city name!?");
                }*/


                Person p = new Person
                {
                    Name = person.FullName,
                    PhoneNumber = person.PhoneNum,
                    City = city,
                    Country = country
                };

                _context.People.Add(p);
                _context.SaveChanges();

                Person insertedPerson = _context.People.FirstOrDefault(a => a.Name == p.Name);
                
                List<LanguagePerson> languagePerson = new List<LanguagePerson>();
                foreach(var l in languages)
                {
                    languagePerson.Add(new LanguagePerson()
                    {
                        Language = l,
                        Person = insertedPerson
                    });
                }
                _context.LanguagePerson.AddRange(languagePerson);
                city.People.Add(p);

                _context.Cities.Update(city);
                _context.SaveChanges();
            }

            return RedirectToAction("ListPeople");
        }
        public IActionResult DeletePerson(int id)
        {

            var personToDelete = _context.People.Find(id);
            _context.People.Remove(personToDelete);
            _context.SaveChanges();

            return RedirectToAction("ListPeople");
        }
    }
}
