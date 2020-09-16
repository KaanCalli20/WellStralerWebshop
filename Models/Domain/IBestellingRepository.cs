using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public interface IBestellingRepository
    {
        public List<Bestelling> getBestellingen(KlantLogin klantLogin);
        public Bestelling getBestellingById(long id, KlantLogin klantLogin);
        public List<Bestelling> getBestellingenByFilter(KlantLogin klantLogin, string? productNaam, DateTime? vanDatum, DateTime? totDatum
            , string? leverAdres, byte? geleverd);
    }
}
