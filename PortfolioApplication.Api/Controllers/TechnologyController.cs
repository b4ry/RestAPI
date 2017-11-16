using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.CQRS.Queries;
using PortfolioApplication.Api.DataTransferObjects.Technology;
using PortfolioApplication.Entities.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.Controllers
{
    /// <summary>
    /// Controller processing requests for Technology entities. Produces JSON output.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TechnologyController : Controller
    {
        private readonly ITechnologyQuery _technologyQuery;

        /// <summary>
        /// TechnologyController constructor
        /// </summary>
        /// <param name="technologyQuery"> Query consumed to retrieve Technology entities </param>
        public TechnologyController(
            ITechnologyQuery technologyQuery)
        {
            _technologyQuery = technologyQuery;
        }

        /// <summary>
        /// GET endpoint retrieving Technology entity by its id
        /// </summary>
        /// <param name="id"> Identification number of Technology entity. <br>Constraints:</br>- must be bigger than 0</param>
        /// <returns> Technology entity in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TechnologyDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(NotFoundObjectResult))]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetTechnologyById([Required]int id)
        {
            Func<DbSet<TechnologyEntity>, Task<TechnologyEntity>> retrivalFunc =
                dbSet => dbSet.Include(tech => tech.TechnologyType)
                .Include(tech => tech.Projects)
                .ThenInclude(projs => projs.Project)
                .ThenInclude(proj => proj.ProjectType)
                .SingleAsync(tech => tech.Id == id);

            var technologyDto = await _technologyQuery.Get(id, retrivalFunc);

            return new JsonResult(technologyDto);
        }

        /// <summary>
        /// GET endpoint retrieving all Technology entities
        /// </summary>
        /// <returns> Technology entity collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IList<TechnologyDto>))]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetTechnologies()
        {
            Func<DbSet<TechnologyEntity>, Task<List<TechnologyEntity>>> retrivalFunc =
                dbSet => dbSet.Include(tech => tech.TechnologyType)
                .Include(tech => tech.Projects)
                .ThenInclude(projs => projs.Project)
                .ThenInclude(proj => proj.ProjectType)
                .ToListAsync();

            var technologyDtos = await _technologyQuery.Get(retrivalFunc);

            return new JsonResult(technologyDtos);
        }
    }
}
