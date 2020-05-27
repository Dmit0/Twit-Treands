using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Objects
{
    public class State
    {
        [JsonProperty("polygons")]
        public List<List<List<double>>> Polygons { get; set; }
        public double Weight { get; set; }
        public List<Tweet> Tweets { get; set; }
    }
}
