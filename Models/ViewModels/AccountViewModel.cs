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
        
        public AccountViewModel(Klant klant,string taal)
        {
            this.Klant = klant;
            this.Taal = taal;
        }
        
    }
}
