using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class FactuurLijnRepository : IFactuurLijnRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<FactuurLijn> _factuurLijnen;

        public FactuurLijnRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _factuurLijnen = _dbContext.FactuurLijnen;
        }

        public List<FactuurLijn> getFactuurLijnen()
        {
            return _factuurLijnen.ToList();
        }

        
    }
}
