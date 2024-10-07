using IRSPublication78.Server.Models;
using IRSPublication78.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO.Compression;

namespace IRSPublication78.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : ControllerBase
    {
        private PubContext pubContext;
        private IConfiguration Configuration;
        private OrganizationService _organizationService;
        public OrganizationsController(PubContext pubContext, OrganizationService organizationService)
        {
            this.pubContext = pubContext;
            this._organizationService = organizationService;
        }

        [HttpGet]
        public async Task<ActionResult<OrgSearchResult>> GetAll([FromQuery] string searchText, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            return await _organizationService.SearchAsync(searchText, pageSize, pageIndex);
        }

        [HttpPost]
        [Route("Load")]
        public async Task<ActionResult<bool>> LoadOrganizations(CancellationToken token)
        {
            await _organizationService.StartAsync(token);
            return Ok(true);
        }
       
    }
}
