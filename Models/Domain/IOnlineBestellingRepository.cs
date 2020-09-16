using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public interface IOnlineBestellingRepository
    {
        public IEnumerable<OnlineBestelling> getOnlineBestellingen(KlantLogin klantLogin);
        public void voegOnlineBestellingToe(OnlineBestelling onlineBestelling);
        public void SaveChanges();
    }
}
