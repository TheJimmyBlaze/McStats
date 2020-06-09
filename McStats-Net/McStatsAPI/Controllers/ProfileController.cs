using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McStatsAPI.DataReading;
using McStatsAPI.MojangApi;
using McStatsAPI.MojangApi.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McStatsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileUuidService profileUuidService;
        private readonly ProfileLookupService profileLookupService;

        public ProfileController(ProfileUuidService profileUuidService, 
                                    ProfileLookupService profileLookupService)
        {
            this.profileUuidService = profileUuidService;
            this.profileLookupService = profileLookupService;
        }

        // GET: api/profile
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<string> uuids = profileUuidService.GetUuids();
            if (uuids == null)
                return NotFound();

            IEnumerable<Profile> profiles = await Task.WhenAll(
                uuids.Select(async uuid => await profileLookupService.GetProfile(uuid)));

            return Ok(profiles);
        }
    }
}