using System;
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

        

        public ProductDetailViewModel(List<Product> geselecteerdeProducten, List<List<ProductKoppeling>> productKoppelingen, int index)
        {
            
            this.geselecteerdeProducten = geselecteerdeProducten;
            if (!isEmpty(productKoppelingen))
            {
                this.productKoppelingen = productKoppelingen;
            }
            this.index = index;
            
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
