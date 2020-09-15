﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public interface IBestellingRepository
    {
        public List<Bestelling> getBestellingen(KlantLogin klantLogin);
        public Bestelling getBestellingById(int id, KlantLogin klantLogin);
    }
}
