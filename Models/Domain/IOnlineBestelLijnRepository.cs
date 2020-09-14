using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public interface IOnlineBestelLijnRepository 
    {
        public List<OnlineBestelLijn> getOnlineBestelLijnen(KlantLogin klantLogin);
        public OnlineBestelLijn getOnlineBestellijn(long id);
        public void voegOnlineBestelLijnToe(OnlineBestelLijn onlineBestelLijn);
        public void voegOnlineBestelLijnenToe(List<OnlineBestelLijn> lijst);
        public void verwijderOBestelLijn(OnlineBestelLijn onBestelLijn);
        public void SaveChanges();
    }
}
