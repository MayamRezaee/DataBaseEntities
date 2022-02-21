using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Models.ViewModel
{
    public class CityReadViewModel
    {
        [Display(Name = "City Name")]
        public string name { get; set; }
    }
}
