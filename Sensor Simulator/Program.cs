using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using Newtonsoft.Json;

Console.WriteLine("Door Sensor started.");
Send();

void Send()
{
    //setting up the connection to the RabbitMQ server
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using (var connection = factory.CreateConnection())
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: "telemetry", durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

        //to run in loop
        while (true)
        {
            int i = 0;
            while (i < 5)
            {

                DoorEvent doorEvent = new DoorEvent
                {
                    Gate = "Gate B",
                    Timestamp = DateTime.UtcNow,
                    NumberOfPeople = 110 + i,
                    Type = "exit"
                };
                //dummy data to send to the queue

                //in case u wanna enter specific data 
                if (false)
                {
                    Console.Write("Please enter the gate detail to send from the device to the queue:");
                    doorEvent.Gate = Console.ReadLine();
                    Console.Write("Please enter the No of People detail to send from the device to the queue:");
                    doorEvent.NumberOfPeople = int.Parse(Console.ReadLine()); //should check it here
                    Console.Write("Please enter the type detail to send from the device to the queue:");
                    doorEvent.Type = Console.ReadLine();

                    doorEvent.Timestamp = DateTime.UtcNow;

                    DoorEvent doorEvent2 = new DoorEvent
                    {
                        Gate = "Gate B",
                        Timestamp = DateTime.UtcNow,
                        NumberOfPeople = 110 + i,
                        Type = "Entry"
                    };
                }

                string jsonMessage = JsonConvert.SerializeObject(doorEvent);
                var sensorbody = Encoding.UTF8.GetBytes(jsonMessage);

                channel.BasicPublish(exchange: "",
                                routingKey: "telemetry",
                                basicProperties: null,
                                body: sensorbody);

                Console.WriteLine(" Sensor x just Sent {0}", jsonMessage);

                i++;
                //wait for 4 sec
                Thread.Sleep(4000);
            }
            Thread.Sleep(10000);
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
