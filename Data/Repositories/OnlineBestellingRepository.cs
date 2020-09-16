﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class OnlineBestellingRepository : IOnlineBestellingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<OnlineBestelling> _onlineBestellingen;

        public OnlineBestellingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _onlineBestellingen = _dbContext.OnlineBestellingen;
        }
        public IEnumerable<OnlineBestelling> getOnlineBestellingen(KlantLogin klantLogin)
        {
            return _onlineBestellingen.Include(p=>p.OnlineBesltelLijnen).Include(p=>p.Klant).Include(t=>t.LeverKlant).Include(p=>p.Transport)
                .OrderByDescending(m=>m.Datum).Where(m=>m.KlantLogin==klantLogin).ToList();
        }
        public void voegOnlineBestellingToe(OnlineBestelling onlineBestelling)
        {
            this._onlineBestellingen.Add(onlineBestelling);

        }
        public void SaveChanges()
        {
            this._dbContext.SaveChanges();
        }
    }
}
