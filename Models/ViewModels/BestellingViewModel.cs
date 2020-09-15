using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Models.ViewModels
{
    public class BestellingViewModel
    {
        public List<Bestelling> Bestellingen { get; set; }

        public BestellingViewModel(List<Bestelling> bestellingen) 
        {
            this.Bestellingen = bestellingen;
        }
        

    }
}
