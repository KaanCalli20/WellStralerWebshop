using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class OnlineBestelLijnenRepository : IOnlineBestelLijnRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<OnlineBestelLijn> _onlineBestelLijnen;

        public OnlineBestelLijnenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _onlineBestelLijnen = _dbContext.OnlineBestelLijnen;
        }

        public List<OnlineBestelLijn> getOnlineBestelLijnen(KlantLogin klantLogin)
        {
            return _onlineBestelLijnen.Include(p => p.KlantLogin).Include(p => p.Klant).Include(p => p.Product)
                .Where(p=>p.KlantId==klantLogin.KlantId&& p.BestellingId==0).OrderBy(m=>m.Id).ThenBy(m=>m.HoofdProdBestelLijnId)
                .ToList();
        }

        public OnlineBestelLijn getOnlineBestellijn(long id)
        {
            return _onlineBestelLijnen.SingleOrDefault(m=>m.Id==id);
        }

        public OnlineBestelLijn getHoofdProduct(long klantLoginId, long klantID, long productId, int aantal, decimal prijs )
        {
            return _onlineBestelLijnen.FirstOrDefault(m => m.KlantLoginId == klantLoginId
            && m.KlantId == klantID && m.ProductId == productId
            && m.Aantal == aantal && m.Prijs == prijs);
        }

        public void voegOnlineBestelLijnToe(OnlineBestelLijn onlineBestelLijn)
        {
            this._onlineBestelLijnen.Add(onlineBestelLijn);

        }
        public void voegOnlineBestelLijnenToe(List<OnlineBestelLijn> lijst)
        {
            this._onlineBestelLijnen.AddRange(lijst);
        }

        public void verwijderOBestelLijn(OnlineBestelLijn onlineBestelLijn)
        {
            this._onlineBestelLijnen.Remove(onlineBestelLijn);
        }

        public void SaveChanges()
        {
            this._dbContext.SaveChanges();
        }

       
    }
}
