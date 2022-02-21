using DataBaseEntities.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Models
{
    public class LanguagePerson
    {
        public string LanguageName { get; set; }
        public Language Language { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

    }
}
