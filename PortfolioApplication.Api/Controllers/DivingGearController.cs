using PortfolioApplication.Services.CQRS.Commands;
using PortfolioApplication.Services.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class DivingGearController : Controller
    {

        private readonly IDivingGearTypeQuery _divingGearTypeQuery;
        private readonly ITechnologyTypeEntityQuery _technologyTypeEntityQuery;
        private readonly ICommandBus _commandBus;

        public DivingGearController(
            ITechnologyTypeEntityQuery technologyTypeEntityQuery, 
            IDivingGearTypeQuery divingGearTypeQuery, 
            ICommandBus commandBus)
        {
            _divingGearTypeQuery = divingGearTypeQuery;
            _commandBus = commandBus;
            _technologyTypeEntityQuery = technologyTypeEntityQuery;
        }

        [HttpGet]
        public async Task<IActionResult> GetDivingGearTypes()
        {
            var a = await _technologyTypeEntityQuery.Get(1);
            return new JsonResult(a);
        }
    }
}
