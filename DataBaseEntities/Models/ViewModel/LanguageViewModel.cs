﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Models.ViewModel
{
    public class LanguageViewModel
    {
        [Display(Name="Language name")]
        public string Name { get; set; }
    }
}
