using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class BestellingRepository : IBestellingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<Bestelling> _bestellingen;

        public BestellingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _bestellingen = _dbContext.Bestellingen;
        }
        public List<Bestelling> getBestellingen(KlantLogin klantLogin)
        {
            return _bestellingen.Include(m=>m.Bestelijnen).Include(m=>m.Klant).Where(m=>m.Klant==klantLogin.Klant).ToList();
        }
        public Bestelling getBestellingById(int id, KlantLogin klantLogin)
        {
            return getBestellingen(klantLogin).Where(p=>p.Id==id && p.Klant==klantLogin.Klant).SingleOrDefault();
        }

    }
}
