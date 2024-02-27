using StadiumTrafficManager.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTrafficManager.Repository.Interface
{
    public interface IStadiumManagerRepository
    {
        Task AddSensorData(SensorData sensorData);

        // Read
        Task<List<SensorData>> SearchSensorData(string? gate, string? type, DateTime? startTime, DateTime? endTime);
        Task<List<SensorData>> GetAllSensorData();

        
    }
}
