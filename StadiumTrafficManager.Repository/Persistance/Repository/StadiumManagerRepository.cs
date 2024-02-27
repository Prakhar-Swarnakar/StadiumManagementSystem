using Microsoft.EntityFrameworkCore;
using StadiumTrafficManager.Common.Contracts;
using StadiumTrafficManager.Repository.Interface;
using StadiumTrafficManager.Repository.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StadiumTrafficManager.Repository.Persistance.Repository
{
    public class StadiumManagerRepository: IStadiumManagerRepository
    {
        public readonly IDbContextFactory<StadiumManagerContext> _contextFactory;

        public StadiumManagerRepository(IDbContextFactory<StadiumManagerContext> stadiumManagerContextFactory)
        {
            _contextFactory = stadiumManagerContextFactory;
        }

        // Create
        public async Task AddSensorData(SensorData sensorData)
        {
            
            var _dbContext = await _contextFactory.CreateDbContextAsync().ConfigureAwait(false);

            _dbContext.SensorData.Add(sensorData);
            _dbContext.SaveChanges();
        }

        // Read
        public async Task<List<SensorData>> SearchSensorData(string? gate, string? type, DateTime? startTime, DateTime? endTime)
        {
            var _dbContext = await _contextFactory.CreateDbContextAsync().ConfigureAwait(false); 
            IQueryable<SensorData> query = _dbContext.SensorData;

            if (!string.IsNullOrEmpty(gate))
            {
                query = query.Where(sr => sr.Gate == gate);
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(sr => sr.Type == type);
            }

            if (startTime.HasValue && endTime.HasValue)
            {
                query = query.Where(sr => sr.TimeStamp >= startTime && sr.TimeStamp <= endTime);
            }

            return query.ToList();
        }

        // Read by Id
        public async Task<SensorData> GetSensorDataById(Guid id)
        {
            var _dbContext = await _contextFactory.CreateDbContextAsync().ConfigureAwait(false);
            // StadiumManagerContext _dbContext = _contextFactory.CreateDbContext();
            return _dbContext.SensorData.FirstOrDefault(sr => sr.Id == id);
        }

        // Read all
        public async Task<List<SensorData>> GetAllSensorData()
        {
            var _dbContext = await _contextFactory.CreateDbContextAsync().ConfigureAwait(false);
            //StadiumManagerContext _dbContext = _contextFactory.CreateDbContext();
            return _dbContext.SensorData.ToList();
        }

    }    
}
