using Microsoft.EntityFrameworkCore;
using StadiumTrafficManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTrafficManager.Repository.Persistance.Context
{
    
    public class StadiumManagerContextFactory : IStadiumManagerContextFactory
    {
        private readonly DbContextOptions<StadiumManagerContext> _options;

        public StadiumManagerContextFactory(DbContextOptions<StadiumManagerContext> options)
        {
            _options = options;
        }

        public StadiumManagerContext CreateDbContext()
        {
            return new StadiumManagerContext(_options);
        }
    }

}
