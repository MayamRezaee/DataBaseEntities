using DataBaseEntities.Data;
using DataBaseEntities.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Controllers
{
    public class CountryController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {

            return View(_context.Countries.Select(a=>new CountryViewModel { Name= a.CountryName}).ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddCountry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCountry(CountryViewModel country)
        {
            if (ModelState.IsValid)
            {
                _context.Countries.Add(new Country() { CountryName = country.Name });
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet("country/cities/{countryName}")]
        public IActionResult GetCitisByCountryName([FromRoute] string countryName) {
            Country chosenCountry = _context.Countries.Where(a => a.CountryName == countryName).Include(b=>b.Cities).FirstOrDefault();
            if (chosenCountry == null) return NoContent(); //status : 202
            IEnumerable<string> cityNames = chosenCountry.Cities.Select(a => a.CityName);
            return Ok(cityNames);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCountry(string countryName)
        {
            var countryToDelete = _context.Countries.FirstOrDefault(x => x.CountryName == countryName);
            _context.Countries.Remove(countryToDelete);
            _context.SaveChanges();

                return RedirectToAction("Index");
        }
            
        
    }
}
