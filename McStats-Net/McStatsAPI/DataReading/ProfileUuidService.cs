using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace McStatsAPI.DataReading
{
    public class ProfileUuidService
    {
        private readonly IConfiguration configuration;

        public ProfileUuidService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<string> GetUuids()
        {
            string statsDirectory = configuration["StatsDirectoryPath"];

            string[] rawUuids = Directory.GetFiles(statsDirectory, "*.json");
            IEnumerable<string> uuids = rawUuids.Select(uuid => Path.GetFileNameWithoutExtension(uuid));

            return uuids;
        }
    }
}
