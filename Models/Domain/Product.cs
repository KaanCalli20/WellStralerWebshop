using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class Product
    {
        public int tPROID { get; set; }
        public string tPROAfk { get; set; }
        public string tPRONedNaam { get; set; }
        public string tProNedOmschrijving { get; set; }
        public string tPROEngNaam { get; set; }
        public string tPROEngOmschrijving { get; set; }
        public string tPROFraNaam { get; set; }
        public string tPROFraOmschrijving { get; set; }
        public double tPROPrijs { get; set; }
        public double tPROPrijs1 { get; set; }
        public double tPROPrijs2 { get; set; }
        public double tPROPrijsGekoppeld { get; set; }
        public double tPROBtwPerc { get; set; }
        public double tPROStock { get; set; }
        public double tPROStockBestelId { get; set; }
        public string tPROTypekorting { get; set; }
        public ProductType TProductType { get; set; }
         
        
    }
}
