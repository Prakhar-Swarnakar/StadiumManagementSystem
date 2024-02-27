using Microsoft.EntityFrameworkCore;
using StadiumTrafficManager.Common;
using StadiumTrafficManager.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTrafficManager.Repository.Persistance.Context
{
    public class StadiumManagerContext: DbContext
    {
        public StadiumManagerContext(DbContextOptions<StadiumManagerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensorData>().ToTable("SensorData");
        }
        public DbSet<SensorData> SensorData { get; set; }
    }
}
