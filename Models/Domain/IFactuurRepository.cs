using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public interface IFactuurRepository
    {
        public List<Factuur> getFacturen(KlantLogin klantLogin);
        public List<Factuur> getGefilterdeFactuur(string? productNaam, long? factuurNr, DateTime? vanDatum, DateTime? totDatum
            , long? zendNotaNr, string? serienummer, KlantLogin klantLogin);

        public Factuur getFactuur(long id, KlantLogin klantLogin);
    }
}
