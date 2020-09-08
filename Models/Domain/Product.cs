using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class Product
    {
        public long Id { get; set; }
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
        public byte Typekorting { get; set; }
        public ProductType ProductType { get; set; }
        public long SoortProduct { get; set; }
        public byte Verwijderd { get; set; }

        public ICollection<ProductKoppeling> productKoppelingen { get; set; }

         
        
    }
}
