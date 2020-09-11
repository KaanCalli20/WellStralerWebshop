using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class KlantLoginRepository : IKlantLoginRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<KlantLogin> _klantLogins;

        public KlantLoginRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _klantLogins = _dbContext.KlantLogins;
        }
        public IEnumerable<KlantLogin> getLogins()
        {
            return _klantLogins.Include(p => p.Klant).ToList();
        }

        public KlantLogin getLoginByGebruikersNaam(string klantLogin)
        {
            return _klantLogins.Include(p=>p.Klant).SingleOrDefault(kl => kl.Gebruikersnaam.Equals(klantLogin));
        }
        public Klant getKlantByGebruikersnaam(string klantLogin)
        {
            return _klantLogins.Include(m => m.Klant).SingleOrDefault(p => p.Gebruikersnaam == klantLogin).Klant;
        }
    }
}
