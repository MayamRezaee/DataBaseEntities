using DataBaseEntities.Models.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseEntities.Models
{
    public class Person
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }


        [Display(Name = "City")]
        public City City { get; set; }


        public int PhoneNumber { get; set; }

        
        public Country Country { get; set; }

        public IList<LanguagePerson> LanguagePerson { get; set; }

    }


}
