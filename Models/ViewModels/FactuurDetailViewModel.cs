using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Models.ViewModels
{
    public class FactuurDetailViewModel
    {
        public Factuur Factuur { get; set; }
        public decimal Totaalbedrag { get; set; }

        public FactuurDetailViewModel(Factuur factuur)
        {
            this.Factuur = factuur;
            this.Totaalbedrag = totaalprijs();
        }
        public decimal totaalprijs()
        {
            decimal totaal = 0;
            foreach (FactuurLijn lijn in Factuur.FactuurLijnen)
            {
                totaal = totaal+ lijn.Prijs*lijn.Aantal;
            }
            return totaal;
        }
    }
}
