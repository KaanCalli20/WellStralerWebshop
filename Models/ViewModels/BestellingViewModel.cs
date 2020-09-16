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

        public IEnumerable<OnlineBestelling> OnlineBestellingen { get; set; }
        public BestellingViewModel(List<Bestelling> bestellingen,IEnumerable<OnlineBestelling> onlineBestellingen) 
        {
            this.Bestellingen = bestellingen;
            this.OnlineBestellingen = onlineBestellingen;
        }
        

    }
}
