using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public interface IKlantLoginRepository
    {
        
        public IEnumerable<KlantLogin> getLogins();

        public KlantLogin getLoginByGebruikersNaam(string klantLogin);
    }
}
