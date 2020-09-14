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
            try
            {
                switch (taal)
                {
                    case "en":
                        return getProducten().Where(p =>p.OmschrijvingEN != null & p.OmschrijvingEN != "" ).Where(p=>p.OmschrijvingEN.ToUpper().Contains(omschrijving)).ToList();
                    case "fr":
                        return getProducten().Where(p =>p.OmschrijvingFR != null & p.OmschrijvingFR!="").Where(p=> p.OmschrijvingFR.ToUpper().Contains(omschrijving)).ToList();
                    default:
                        return getProducten().Where(p => p.OmschrijvingNL != null & p.OmschrijvingNL != "").Where(p => p.OmschrijvingNL.ToUpper().Contains(omschrijving)).ToList();
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Omschrijving in taal niet gevonden overgegaan naar default taal!!!!!!" + ex.Message);
                return getProducten().Where(p => p.OmschrijvingNL.Contains(omschrijving)).ToList();
            }


        }

    }
}
