using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public interface ITransportRepository
    {
        public List<Transport> getAllTransport();
        public Transport getTransportById(short id);
    }
}
