using StadiumTrafficManager.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTrafficManager.Service.Interface
{
    public interface IStadiumManagerService
    {
        Task AddSensorData(SensorData sensorData);
        Task<List<SensorResult>> GetFilteredResults(String? gateName, String? type, DateTime? startTime, DateTime? endTime);
    }
}
