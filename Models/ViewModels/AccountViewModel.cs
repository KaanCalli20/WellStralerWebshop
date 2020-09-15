using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Models.ViewModels
{
    public class AccountViewModel
    {
        public string Taal { get; set; }
        public Klant Klant { get; set; }

        public AccountViewModel(Klant klant)
        {
            this.Klant = klant;
            zetTaal();
        }
        public void zetTaal()
        {
            if (Klant.Taal == 0)
            {
                Taal = "nl";
            }
            else if (Klant.Taal == 1)
            {
                Taal = "fr";
            }
            else
            {
                Taal = "en";
            }
        }
    }
}
