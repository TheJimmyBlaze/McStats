using McStatsAPI.MojangApi.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace McStatsAPI.MojangApi
{
    public class ProfileLookupService
    {
        private const string PROFILE_ROUTE = "session/minecraft/profile/";

        private readonly IConfiguration configuration;

        public ProfileLookupService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<Profile> GetProfile(string uuid)
        {
            Uri mojangApiUri = new Uri(configuration["MojangApiUrl"]);
            Uri profileUri = new Uri(mojangApiUri, PROFILE_ROUTE);
            Uri requestUri = new Uri(profileUri, uuid);

            using HttpClient client = new HttpClient();
            string serializedProfile = await client.GetStringAsync(requestUri);

            if (string.IsNullOrEmpty(serializedProfile))
                return null;

            Profile profile = JsonSerializer.Deserialize<Profile>(serializedProfile, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return profile;
        }
    }
}
