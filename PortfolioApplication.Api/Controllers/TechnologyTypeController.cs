using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApplication.Api.CQRS.Queries;
using PortfolioApplication.Api.DataTransferObjects.Technology;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
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
        /// <param name="technologyTypeEntityQuery"> Query consumed to retrieve TechnologyType entities </param>
        public TechnologyTypeController(
            ITechnologyTypeQuery technologyTypeEntityQuery)
        {
            _technologyTypeQuery = technologyTypeEntityQuery;
        }

        /// <summary>
        /// Get endpoint retrieving TechnologyType entity by its id
        /// </summary>
        /// <param name="id"> Identification number of TechnologyType entity </param>
        /// <returns> TechnologyEntity in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TechnologyTypeDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetTechnologyTypeById([Required]int id)
        {
            var technologyTypeDto = await _technologyTypeQuery.Get(id);

            return new JsonResult(technologyTypeDto);
        }

        /// <summary>
        /// Get endpoint retrieving all TechnologyType entities
        /// </summary>
        /// <returns> TechnologyEntity collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<TechnologyTypeDto>))]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetTechnologyTypes()
        {
            var technologyTypeDtos = await _technologyTypeQuery.Get();

            return new JsonResult(technologyTypeDtos);
        }
    }
}
