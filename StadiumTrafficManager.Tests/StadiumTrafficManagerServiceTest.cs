using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using StadiumTrafficManager.Repository.Interface;
using StadiumTrafficManager.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StadiumTrafficManager.Common.Contracts;

namespace StadiumTrafficManager.Tests
{
    [TestFixture]
    public class StadiumManagerServiceTests
    {
        private Mock<IStadiumManagerRepository> _mockRepository;

        [Test]
        public async Task AddSensorData_ValidSensorData_AddedSuccessfully()
        {
            // Arrange
            var sensorData = new SensorData(); // Assuming this is properly initialized
            var mockRepository = new Mock<IStadiumManagerRepository>();
            var mockLogger = new Mock<ILogger<StadiumManagerService>>();
            var service = new StadiumManagerService(mockRepository.Object, mockLogger.Object);

            // Act
            await service.AddSensorData(sensorData);

            // Assert
            mockRepository.Verify(r => r.AddSensorData(sensorData), Times.Once);
            mockLogger.Verify(l => l.LogInformation("Sensor data added successfully."), Times.Once);
        }

        [Test]
        public async Task GetFilteredResults_ValidParameters_ReturnsFilteredResults()
        {
            // Arrange
            var gateName = "Gate1";
            var type = "Type1";
            var startTime = new DateTime(2024, 1, 1);
            var endTime = new DateTime(2024, 1, 2);
            var mockRepository = new Mock<IStadiumManagerRepository>();
            mockRepository.Setup(r => r.SearchSensorData(gateName, type, startTime, endTime))
                          .ReturnsAsync(new List<SensorData>
                          {
                              new SensorData { Gate = gateName, Type = type, NoOfPeople = 100 }
                              // Add more sample sensor data if needed
                          });
            var mockLogger = new Mock<ILogger<StadiumManagerService>>();
            var service = new StadiumManagerService(mockRepository.Object, mockLogger.Object);

            // Act
            var result = await service.GetFilteredResults(gateName, type, startTime, endTime);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<SensorResult>>(result);
            Assert.That(result.Count, Is.EqualTo(1)); // Assuming we added only one sensor data in the mock
            Assert.That(result[0].Gate, Is.EqualTo(gateName));
            Assert.That(result[0].Type, Is.EqualTo(type));
            Assert.That(result[0].NoOfPeople, Is.EqualTo(100)); // Assuming 100 people were recorded
            mockLogger.Verify(l => l.LogInformation("Filtered results retrieved successfully."), Times.Once);
        }
    }
}
