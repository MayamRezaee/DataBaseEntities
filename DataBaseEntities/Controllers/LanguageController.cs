using DataBaseEntities.Data;
using DataBaseEntities.Models;
using DataBaseEntities.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Controllers
{
    public class LanguageController : Controller
    {
        public readonly ApplicationDbContext _context;

        public LanguageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {

            return View(_context.Languages.Select(a=>new LanguageViewModel() { Name = a.LanguageName}).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddLanguages()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLanguages(LanguageViewModel language)
        {
            if (ModelState.IsValid)
            {
                _context.Languages.Add(new Language() { LanguageName = language.Name});
                _context.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteLanguage(string languageName)
        {
            var languageToDelete = _context.Languages.FirstOrDefault(x => x.LanguageName == languageName);
            _context.Languages.Remove(languageToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}

