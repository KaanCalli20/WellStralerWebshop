using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using WellStralerWebshop.Data.Mappers;

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

        public Product()
        {
            productKoppelingen = new List<ProductKoppeling>();
        }

        public int convertID(long longId)
        {
            return Convert.ToInt32(longId);
        }

        public List<List<ProductKoppeling>> gekoppeldProductenLijst()
        {
            List<List<ProductKoppeling>> productLijst = new List<List<ProductKoppeling>>();

            List<short> volgorde;

            List<ProductKoppeling> lijstVolgordeTellerProductKoppeling;

            List<int> KoppelTypes;

            List<ProductKoppeling> lijstKoppelTypeTellerProductKoppeling;

            List<ProductKoppeling> tellerProductLijst;
            


            volgorde = productKoppelingen.Select(p => p.KoppelVolgorde).Distinct().ToList();

            volgorde.Sort();

            foreach (short volgordeTeller in volgorde)
            { 
                lijstVolgordeTellerProductKoppeling= productKoppelingen.Where(p => p.KoppelVolgorde == volgordeTeller).ToList();

                KoppelTypes = lijstVolgordeTellerProductKoppeling.Select(p => p.KoppelType.Id).Distinct().ToList();
                KoppelTypes.Sort();

                foreach (int koppelTypeTeller in KoppelTypes)
                {
                     lijstKoppelTypeTellerProductKoppeling=
                        lijstVolgordeTellerProductKoppeling.Where(p => p.KoppelType.Id==koppelTypeTeller).ToList();

                    tellerProductLijst = new List<ProductKoppeling>();
                    foreach (ProductKoppeling product in lijstKoppelTypeTellerProductKoppeling)
                    {
                        tellerProductLijst.Add(product);
                    }
                    productLijst.Add(tellerProductLijst);
                }
            }
            return productLijst;
        }
        public string haalStringObject()
        {
            return JsonConvert.SerializeObject(gekoppeldProductenLijst());
        }
    }
    
}
