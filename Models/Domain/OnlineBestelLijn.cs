using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    [NotMapped]
    public class OnlineBestelLijn
    {
        public long Id { get; set; }
        public long BestellingId { get; set; }
        public long KlantLoginId { get; set; }
        public KlantLogin KlantLogin {get;set;}
        public long KlantId { get; set; }
        public Klant Klant { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public int Aantal { get; set; }
        public decimal Prijs { get; set; }
        public decimal BtwPerc { get; set; }

        public DateTime DatumInbreng { get; set; }
        public long HoofdProdBestelLijnId { get; set; }

        public OnlineBestelLijn(long BestellingId, KlantLogin klantLogin, Klant klant, Product product, int aantal,decimal prijs,
            decimal btwPerc, DateTime datumInbreng, long hoofdProdBestelLijnId)
        {
            this.KlantLogin = klantLogin;
            this.Klant = klant;
            this.Product = product;
            this.Aantal = aantal;
            this.Prijs = prijs;
            this.BtwPerc = btwPerc;
            this.DatumInbreng = datumInbreng;
            this.HoofdProdBestelLijnId = hoofdProdBestelLijnId;
        }
        public OnlineBestelLijn()
        {

        }
       
    }
}
