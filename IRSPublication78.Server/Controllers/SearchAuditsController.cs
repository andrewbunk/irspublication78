using IRSPublication78.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IRSPublication78.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchAuditsController : ControllerBase
    {
        private PubContext _pubContext;
        public SearchAuditsController(PubContext pubContext)
        {
            _pubContext = pubContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<SearchAudit>>> GetAll([FromQuery] int? top)
        {
            var query = _pubContext.SearchAudits.OrderByDescending(x => x.Id).Where(x => true);
            if (top != null)
            {
                query = query.Take((int)top);
            }
            return await query.ToListAsync();
        }
    }
}
