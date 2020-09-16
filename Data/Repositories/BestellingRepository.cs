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
            return _bestellingen.Include(m=>m.Bestelijnen).Include(m=>m.Klant).Where(m=>m.Klant==klantLogin.Klant).OrderByDescending(m=>m.Datum).ToList();
        }
        public Bestelling getBestellingById(int id, KlantLogin klantLogin)
        {
            return getBestellingen(klantLogin).Where(p=>p.Id==id && p.Klant==klantLogin.Klant).SingleOrDefault();
        }

        public List<Bestelling> getBestellingenByFilter(KlantLogin klantLogin, string? productNaam, DateTime? vanDatum, DateTime? totDatum, string? leverAdres, byte? geleverd)
        {
            List<Bestelling> bestellingen = new List<Bestelling>();
            if (productNaam != null)
            {
                foreach (Bestelling bestelling in getBestellingen(klantLogin))
                {
                    foreach (BestelLijn bestelLijn in bestelling.Bestelijnen)
                    {
                        if (bestelLijn.ProductOmschrijving.Contains(productNaam))
                        {
                            bestellingen.Add(bestelling);
                        }
                    }
                }
            }
            else
            {
                bestellingen = getBestellingen(klantLogin);
            }
            if(vanDatum != null )
            {
                bestellingen = bestellingen.Where(m => m.Datum > vanDatum).ToList();
            }
           
            if (totDatum != null )
            {
                bestellingen = bestellingen.Where(m => m.Datum < totDatum).ToList();
            }
           
            if (geleverd != null ||geleverd==3)
            {
                if (geleverd == 1)
                {
                    bestellingen = bestellingen.Where(m => m.Afgewerkt == 0).ToList();
                } else if (geleverd == 2)
                {
                    bestellingen = bestellingen.Where(m => m.Afgewerkt == 1).ToList();
                }
            }
            return bestellingen;

            

        }
    }
}
