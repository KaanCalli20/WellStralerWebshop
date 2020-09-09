using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class OnlineBestelLijnenRepository : IOnlineBestelLijnRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<OnlineBestelLijn> _onlineBestelLijnen;

        public OnlineBestelLijnenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _onlineBestelLijnen = _dbContext.OnlineBestelLijnen;
        }

        public IEnumerable<OnlineBestelLijn> getOnlineBestelLijnen()
        {
            return _onlineBestelLijnen.Include(p => p.KlantLogin).Include(p => p.Klant).Include(p => p.Product).ToList();
        }
    }
}
