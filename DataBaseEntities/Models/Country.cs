using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Models.ViewModel
{
    public class Country
    {
        [Key]
        [MaxLength(100)]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public ICollection<City> Cities { get; set; }

    }
}
