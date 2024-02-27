using StadiumTrafficManager.Repository.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTrafficManager.Repository.Interface
{
    public interface IStadiumManagerContextFactory
    {
        StadiumManagerContext CreateDbContext();
    }
}
