using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.CQRS.Queries;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.Controllers
{
    /// <summary>
    /// Controller processing requests for TechnologyType entities. Produces JSON output.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TechnologyTypeController : Controller
    {
        private readonly ITechnologyTypeQuery _technologyTypeQuery;

        /// <summary>
        /// TechnologyTypeController constructor
        /// </summary>
        /// <param name="technologyTypeQuery"> Query consumed to retrieve TechnologyType entities </param>
        public TechnologyTypeController(
            ITechnologyTypeQuery technologyTypeQuery)
        {
            _technologyTypeQuery = technologyTypeQuery;
        }

        /// <summary>
        /// Retrieve TechnologyType entity by its id
        /// </summary>
        /// <param name="id"> Identification number of TechnologyType entity </param>
        /// <returns> TechnologyType entity in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetTechnologyTypeById([Required]int id)
        {
            var technologyTypeDto = await _technologyTypeQuery.GetAsync(id, dbSet => dbSet.SingleAsync(x => x.Id == id));

            return new JsonResult(technologyTypeDto);
        }

        /// <summary>
        /// Retrieve all TechnologyType entities
        /// </summary>
        /// <returns> TechnologyType entity collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetTechnologyTypes()
        {
            var technologyTypeDtos = await _technologyTypeQuery.GetAsync(dbSet => dbSet.ToListAsync());

            return new JsonResult(technologyTypeDtos);
        }
    }
}
