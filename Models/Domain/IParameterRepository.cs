﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public interface IParameterRepository
    {
        public List<Parameters> GetParameters();
    }
}
