using DataBaseEntities.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Models.ViewModel
{ 
    public class Language
    {
        [Key]
        public string LanguageName { get; set; }
        
        public List<LanguagePerson> LanguagePerson { get; set; }
        public List<Person> People { get; set; }



    }
}
