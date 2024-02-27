using Microsoft.AspNetCore.Mvc;
using StadiumTrafficManager.Common.Contracts;
using StadiumTrafficManager.Repository.Interface;
using StadiumTrafficManager.Service.Interface;
using System;

namespace StadiumTrafficManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StadiumManagerController : ControllerBase
    {
        private readonly IStadiumManagerService _stadiumManagerService;
        private readonly IStadiumManagerRepository _stadiumManagerRepository;
        public StadiumManagerController(IStadiumManagerService stadiumManagerService, IStadiumManagerRepository stadiumManagerRepository)
        {
            _stadiumManagerService = stadiumManagerService;
            _stadiumManagerRepository = stadiumManagerRepository;
        }

        [HttpGet("GetSensorResults", Name = "GetSensorResults")]
        public async Task<IActionResult> GetSensorResults(
            [FromQuery] string? gate,
            [FromQuery] string? type,
            [FromQuery] DateTime? startTime,
            [FromQuery] DateTime? endTime)
        {
            if ((startTime.HasValue && !endTime.HasValue) || (!startTime.HasValue && endTime.HasValue))
            {
                return BadRequest("Both startTime and endTime must be provided or omitted together.");
            }

            var allSensorData =  await _stadiumManagerService.GetFilteredResults(gate, type, startTime, endTime);

            return Ok();
        }

        [HttpPost(Name ="AddSensorData")]
        public async Task<IActionResult> AddSensorData(
            [FromQuery] string gate,
            [FromQuery] int noOfPeople,
            [FromQuery] string type)
        {
            var sensorData = new SensorData
            {
                Id = Guid.NewGuid(),
                Gate = gate,
                Type = type,
                TimeStamp = DateTime.UtcNow,
                NoOfPeople = noOfPeople
            };
             await _stadiumManagerService.AddSensorData(sensorData);
            return Ok();
        }

        [HttpGet("GetAllSensorData", Name = "GetAllSensorData")]
        public async Task<IActionResult> GetAllSensorData()
        {
            var allSensorData = await _stadiumManagerRepository.GetAllSensorData();
            return Ok(allSensorData);
        }
    }
}
