using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<Product> _producten;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _producten = _dbContext.Producten;
        }

        public IEnumerable<Product> getProducten()
        {
            return _producten.Include(p => p.ProductType).Include(p => p.productKoppelingen).ThenInclude(p => p.KoppelType).ToList();
        }

        public Product getProductById(long Id)
        {
            return getProducten().SingleOrDefault(p => p.Id == Id);
        }

        public IEnumerable<Product> getProductenByTaalOmschrijving(string taal, string omschrijving)
        {


                switch (taal)
                {
                    case "en":
                        return getProducten().Where(p => p.OmschrijvingEN.Contains(omschrijving) != true ? p.OmschrijvingNL.Contains(omschrijving) : p.OmschrijvingEN.Contains(omschrijving)).ToList();
                    case "fr":
                        return getProducten().Where(p => p.OmschrijvingFR.Contains(omschrijving) != true ? p.OmschrijvingNL.Contains(omschrijving) : p.OmschrijvingFR.Contains(omschrijving)).ToList();
                    case "nl":
                        return getProducten().Where(p => p.OmschrijvingNL.Contains(omschrijving)).ToList();
                default:
                        return getProducten().Where(p => p.OmschrijvingNL.Contains(omschrijving)).ToList();
                }

        }

    }
}
