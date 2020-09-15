using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Models.ViewModels
{
    public class BestellingDetailViewModel
    {
        public Bestelling Bestelling { get; set; }
        
        public BestellingDetailViewModel(Bestelling bestelling)
        {
            this.Bestelling = bestelling;
        }
    }
}
