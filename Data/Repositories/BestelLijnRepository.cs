using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class BestelLijnRepository : IBestelLijnRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<BestelLijn> _bestelLijnen;

        public BestelLijnRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _bestelLijnen = _dbContext.BestelLijnen;
        }

        public List<BestelLijn> getBestelLijnen()
        {
            return _bestelLijnen.Include(m=>m.Product).Include(m=>m.Bestelling).ToList();
        }
    }
}
