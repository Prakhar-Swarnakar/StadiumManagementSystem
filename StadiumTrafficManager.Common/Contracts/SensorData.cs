using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTrafficManager.Common.Contracts
{
    public class SensorData
    {
        public Guid Id { get; set; }
        public string Gate { get; set; }
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public int NoOfPeople { get; set; }
    }
}
