using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class KlantRepository : IKlantRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private DbSet<Klant> _klanten;

        public KlantRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _klanten = _dbContext.Klanten;
        }

        public IEnumerable<Klant> getKlanten()
        {
            return _klanten.Include(p=>p.KlantKoppelingen).ToList();
        }
        
        public Klant getKlant(long id)
        {
            return _klanten.Where(m => m.Id == id).Include(m => m.KlantKoppelingen).ThenInclude(m => m.GekoppeldKlant).SingleOrDefault();
        }
    }
}
