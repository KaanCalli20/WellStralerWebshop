using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Afk { get; set; }
        public string NaamNL { get; set; }
        public string OmschrijvingNL { get; set; }
        public string NaamEN { get; set; }
        public string OmschrijvingEN { get; set; }
        public string NaamFR { get; set; }
        public string OmschrijvingFR { get; set; }
        public Decimal Prijs { get; set; }
        public Decimal Prijs1 { get; set; }
        public Decimal Prijs2 { get; set; }
        public Decimal PrijsGekoppeld { get; set; }
        public Decimal BtwPerc { get; set; }
        public int Stock { get; set; }
        public int StockBestelId { get; set; }
        public int Typekorting { get; set; }
        public ProductType ProductType { get; set; }
        public int SoortProduct { get; set; }
        public int Verwijderd { get; set; }

        public IEnumerable<ProductKoppeling> productKoppelingen { get; set; }

         
        
    }
}
