﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    [NotMapped]
    public class KlantLogin
    {
        public int tKLLId { get; set; }
        
        public string tKLLNaam { get; set; }
        public string tKLLVoornaam { get; set; }
        public string tKLLLogin { get; set; }
        public string tKLLPaswoord { get; set; }
        

    }
}
