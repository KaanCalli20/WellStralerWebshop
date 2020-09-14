using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class FactuurRepository : IFactuurRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<Factuur> _factuurLijnen;

        public FactuurRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _factuurLijnen = _dbContext.Facturen;
        }
        public List<Factuur> getFacturen()
        {
            return _factuurLijnen.Include(m=>m.FactuurLijnen).Include(m=>m.Klant).ToList();
        }
    }
}
