using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTrafficManager.Common.Contracts
{
    public class SensorResult
    {
        public string Gate { get; set; }
        public string Type { get; set; }
        public int NoOfPeople { get; set; }
    }
}
