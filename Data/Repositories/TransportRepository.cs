using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{

    public class TransportRepository : ITransportRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<Transport> _transportlijst;

        public TransportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            this._transportlijst = _dbContext.TransportLijst;
        }

        public List<Transport> getAllTransport()
        {
            return this._transportlijst.ToList();
        }
        public Transport getTransportById(short id)
        {
            return this._transportlijst.SingleOrDefault(m => m.Id == id);
        }
    }
}
