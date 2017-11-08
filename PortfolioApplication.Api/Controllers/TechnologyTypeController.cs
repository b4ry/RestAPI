using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Services.CQRS.Queries;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.Controllers
{
    /// <summary>
    /// Controller processing requests for TechnologyType entities. Produces json output.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TechnologyTypeController : Controller
    {
        private readonly ITechnologyTypeEntityQuery _technologyTypeEntityQuery;

        /// <summary>
        /// TechnologyTypeController constructor
        /// </summary>
        /// <param name="technologyTypeEntityQuery"> Query consumed to retrieve TechnologyType entities </param>
        public TechnologyTypeController(
            ITechnologyTypeEntityQuery technologyTypeEntityQuery)
        {
            _technologyTypeEntityQuery = technologyTypeEntityQuery;
        }

        /// <summary>
        /// Get to retrieve TechnologyType entity by its id
        /// </summary>
        /// <param name="id"> Identification number of TechnologyType entity </param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTechnologyTypeById(int id)
        {
            var technologyTypeEntity = await _technologyTypeEntityQuery.Get(id);
            var technologyTypeDto = Mapper.Map<TechnologyTypeDto>(technologyTypeEntity);

            return new JsonResult(technologyTypeDto);
        }
    }
}
