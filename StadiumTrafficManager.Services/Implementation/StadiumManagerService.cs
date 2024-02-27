using Microsoft.Extensions.Logging;
using StadiumTrafficManager.Common.Contracts;
using StadiumTrafficManager.Repository.Interface;
using StadiumTrafficManager.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StadiumTrafficManager.Service.Implementation
{
    public class StadiumManagerService : IStadiumManagerService
    {
        private readonly IStadiumManagerRepository _stadiumManagerRepository;
        private readonly ILogger<StadiumManagerService> _logger;

        public StadiumManagerService(IStadiumManagerRepository stadiumManagerRepository, ILogger<StadiumManagerService> logger)
        {
            _stadiumManagerRepository = stadiumManagerRepository ?? throw new ArgumentNullException(nameof(stadiumManagerRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddSensorData(SensorData sensorData)
        {
            if (sensorData == null)
            {
                _logger.LogError("Sensor data is null.");
                return;
            }
            try
            {
                await _stadiumManagerRepository.AddSensorData(sensorData);
                _logger.LogInformation("Sensor data added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding sensor data.");
                throw; // Re-throwing the exception to propagate it up the call stack
            }
        }

        public async Task<List<SensorResult>> GetFilteredResults(string? gateName, string? type, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                var sensorData = await _stadiumManagerRepository.SearchSensorData(gateName, type, startTime, endTime).ConfigureAwait(false);
                var sensorResults = new List<SensorResult>();
                foreach (var sensor in sensorData)
                {
                    sensorResults.Add(new SensorResult
                    {
                        Gate = sensor.Gate,
                        Type = sensor.Type,
                        NoOfPeople = sensor.NoOfPeople
                    });
                }
                _logger.LogInformation("Filtered results retrieved successfully.");
                return sensorResults;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving filtered results.");
                throw; // Re-throwing the exception to propagate it up the call stack
            }
        }
    }
}
