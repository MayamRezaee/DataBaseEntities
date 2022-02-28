using DataBaseEntities.Data;
using DataBaseEntities.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Controllers
{
    public class CityController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {

            return View(_context.Cities.Select(a=>new CityReadViewModel() {name = a.CityName }).ToList());
        }

        //Action for Adding City 
        [Authorize(Roles = "Admin")]
        public IActionResult AddCity()
        {
            ViewBag.Countries = _context.Countries.ToList();
            return View();
        }

        //Sending user input for adding city
        [HttpPost]
        public IActionResult AddCity(CityCreationViewModel userInput)
        {
            if (ModelState.IsValid)
            {
                Country selectedCountry = _context.Countries.FirstOrDefault(a => a.CountryName == userInput.CountryName);
                

                City newCity = new City {CityName = userInput.CityName , Country = selectedCountry };
                _context.Cities.Add(newCity);
                selectedCountry.Cities.Add(newCity);
                _context.Countries.Update(selectedCountry);
                _context.SaveChanges();
            }

        
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCity(string cityName)
        {
            var cityToDelete = _context.Cities.FirstOrDefault(x => x.CityName == cityName);
            _context.Cities.Remove(cityToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
