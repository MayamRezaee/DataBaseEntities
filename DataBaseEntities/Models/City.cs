using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Models.ViewModel
{
    public class City
    {
        [Key]
        [MaxLength(100)]
        [Display(Name = "City Name")]
        public string CityName { get; set; }
       
        public Country Country { get; set; }
        public List<Person> People { get; set;  }




    }
}
