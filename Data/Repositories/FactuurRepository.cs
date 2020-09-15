using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class FactuurRepository : IFactuurRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<Factuur> _facturen;

        public FactuurRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _facturen = _dbContext.Facturen;
        }
        public List<Factuur> getFacturen(KlantLogin klantLogin)
        {
            return _facturen.Include(m => m.FactuurLijnen).Include(m => m.Klant).Where(m=>m.Klant==klantLogin.Klant).ToList();
        }

        public Factuur getFactuur(long id, KlantLogin klantLogin)
        {
            return getFacturen(klantLogin).SingleOrDefault(m=>m.Id==id);
        }

        public List<Factuur> getGefilterdeFactuur(string? productNaam, long? factuurNr, DateTime? vanDatum, DateTime? totDatum, long? zendNotaNr, string serienummer, KlantLogin klantLogin)
        {
            List<Factuur> facturen = getFacturen(klantLogin).ToList();
            List<Factuur> gefilterdeFacturen = new List<Factuur>();
            if (productNaam!=null)
            {
                if (!productNaam.Equals(""))
                {
                    foreach (Factuur item in facturen)
                    {
                        foreach (FactuurLijn lijn in item.FactuurLijnen)
                        {
                            if (lijn.ProductOmschrijving != null)
                            {
                                if (lijn.ProductOmschrijving.ToUpper().Contains(productNaam.ToUpper()))
                                {
                                    gefilterdeFacturen.Add(item);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    gefilterdeFacturen = facturen;
                }
            }
            else
            {
                gefilterdeFacturen = facturen;
            }

            if (factuurNr!=null)
            {
                if (!factuurNr.Equals(""))
                {
                    gefilterdeFacturen = gefilterdeFacturen.Where(m => m.FactuurNummer == factuurNr).ToList();
                }
            }
            
            if (vanDatum != null)
            {
                gefilterdeFacturen = gefilterdeFacturen.Where(m => m.Datum >= vanDatum).ToList();
            }
            if (totDatum != null)
            {
                gefilterdeFacturen = gefilterdeFacturen.Where(m => m.Datum <= totDatum).ToList();
            }

            if ( zendNotaNr !=null){

                if (zendNotaNr != 0)
                {
                    List<Factuur> filterHulp = new List<Factuur>();
                    foreach (Factuur factuur in gefilterdeFacturen)
                    {
                        foreach (FactuurLijn lijn in factuur.FactuurLijnen)
                        {

                            if (lijn.ZendNummer == zendNotaNr)
                            {
                                filterHulp.Add(factuur);
                                break;
                            }

                        }
                    }
                    gefilterdeFacturen = filterHulp;
                }
            }

            if (serienummer != null)
            {

                if (!serienummer.Equals(""))
                {
                    List<Factuur> filterHulp = new List<Factuur>();
                    foreach (Factuur factuur in gefilterdeFacturen)
                    {
                        foreach (FactuurLijn lijn in factuur.FactuurLijnen)
                        {

                            if (lijn.SerieNummer.Equals(serienummer))
                            {
                                filterHulp.Add(factuur);
                                break;
                            }

                        }
                    }
                    gefilterdeFacturen = filterHulp;
                }
            }
            return gefilterdeFacturen;
        }
    }
}
