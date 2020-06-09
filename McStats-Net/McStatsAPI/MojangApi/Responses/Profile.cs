using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McStatsAPI.MojangApi.Responses
{
    public class Profile
    {
        public string Id { get; set; }
        public string Name { get; set; }

        //Properties has been omitted as it is not required for stats.
    }
}
