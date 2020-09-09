using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> getProducten();

        Product getProductById(long Id);

        IEnumerable<Product> getProductenByOmschrijving(String omschrijving);
    }
}
