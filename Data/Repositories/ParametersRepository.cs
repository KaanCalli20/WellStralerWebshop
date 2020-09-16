using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Repositories
{
    public class ParametersRepository : IParameterRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<Parameters> _parameters;

        public ParametersRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._parameters = this._dbContext.Parameters;
        }
        public List<Parameters> GetParameters()
        {
            return this._parameters.ToList();
        }
    }
}
