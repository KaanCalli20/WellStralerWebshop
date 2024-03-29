﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Models.ViewModels
{
    public class ProductDetailViewModel
    {
        public IEnumerable<Product> geselecteerdeProducten { get; set; }
        public IEnumerable<IEnumerable<ProductKoppeling>> productKoppelingen { get; set; }
        
        public List<long> selectedProds { get; set; }

        //----------links----------
        public int min { get; set; }
        public int max { get; set; }
        public int index { get; set; }

        public int showIndex { get; set; }
        public int showMax { get; set; }

        public Decimal prijs { get; set; }

        public int aantal { get; set; }
        public Klant klant { get; set; }

        public ProductDetailViewModel(List<Product> geselecteerdeProducten, List<List<ProductKoppeling>> productKoppelingen, int index, int aantal,Klant klant)
        {
            
            this.geselecteerdeProducten = geselecteerdeProducten;
            if (!isEmpty(productKoppelingen))
            {
                this.productKoppelingen = productKoppelingen;
            }
            this.index = index;
            this.max = productKoppelingen.Count - 1;
            this.showIndex = this.index+1;
            this.showMax = this.max +1;
            this.aantal = aantal;
            this.klant = klant;
        }
        public ProductDetailViewModel()
        {
            //this.selectedProds = new List<long>();
        }
        
        private Boolean isEmpty(List<List<ProductKoppeling>> productKoppelingen)
        {
            return productKoppelingen.Count == 0 ? true : false;
        }

    }
}
