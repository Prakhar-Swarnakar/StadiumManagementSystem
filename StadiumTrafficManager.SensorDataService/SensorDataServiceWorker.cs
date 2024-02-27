using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StadiumTrafficManager.Common.Contracts;
using StadiumTrafficManager.Service.Implementation;
using StadiumTrafficManager.Service.Interface;

namespace StadiumTrafficManager.SensorDataService
{
    public class SensorDataServiceWorker : BackgroundService
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private readonly IStadiumManagerService _stadiumManagerService;


        public SensorDataServiceWorker(IStadiumManagerService stadiumManagerService)
        {
            // Configure RabbitMQ Client
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "telemetry",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            _stadiumManagerService = stadiumManagerService ?? throw new ArgumentNullException(nameof(stadiumManagerService));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);


     
                DoorEvent parseResult;

                //Parse Data 

                DoorEvent doorEvent = JsonConvert.DeserializeObject<DoorEvent>(message);

                SensorData sensorData = new SensorData{
                    Id = Guid.NewGuid(),
                    Gate = doorEvent.Gate,
                    NoOfPeople = doorEvent.NumberOfPeople,
                    TimeStamp = doorEvent.Timestamp,
                    Type = doorEvent.Type
                };

                 _stadiumManagerService.AddSensorData(sensorData);

            };
            _channel.BasicConsume(queue: "telemetry",
                                 autoAck: true,
                                 consumer: consumer);

            //return Task.CompletedTask;
        }
    }
}

public class DoorEvent
{
    public string Gate { get; set; }
    public DateTime Timestamp { get; set; }
    public int NumberOfPeople { get; set; }
    public string Type { get; set; }
}


