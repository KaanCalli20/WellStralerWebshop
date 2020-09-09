using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class ProductRepository:IProductRepository
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
            return _producten.Include(p=>p.ProductType).Include(p=>p.productKoppelingen).ThenInclude(p=>p.KoppelType).ToList();
        }

        public Product getProductById(long Id)
        {
            return getProducten().SingleOrDefault(p => p.Id == Id);
        }

    }
}
