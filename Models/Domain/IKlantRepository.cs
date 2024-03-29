﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public interface IKlantRepository
    {
        IEnumerable<Klant> getKlanten();
        public Klant getKlant(long id);
    }
}
