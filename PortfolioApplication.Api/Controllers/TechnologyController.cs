using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApplication.Api.CQRS.Commands;
using PortfolioApplication.Api.CQRS.Commands.Technologies;
using PortfolioApplication.Api.CQRS.Queries;
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
        private readonly ICommandBus _commandBus;

        /// <summary>
        /// TechnologyController constructor
        /// </summary>
        /// <param name="technologyQuery"> Query consumed to retrieve Technology entities </param>
        /// <param name="commandBus"> Command bus managing incoming Technology commands </param>
        public TechnologyController(
            ITechnologyQuery technologyQuery,
            ICommandBus commandBus)
        {
            _technologyQuery = technologyQuery;
            _commandBus = commandBus;
        }

        /// <summary>
        /// Retrieve Technology entity by its id
        /// </summary>
        /// <param name="id"> Identification number of Technology entity. <br>Constraints:</br>- must be bigger than 0</param>
        /// <returns> Technology entity in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully retrieved enquired entity from database")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Enquired entity does not exist in database")]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetTechnologyById([Required]int id)
        {
            Func<DbSet<TechnologyEntity>, Task<TechnologyEntity>> retrivalFunc =
                dbSet => dbSet.Include(tech => tech.TechnologyType)
                .Include(tech => tech.Projects)
                .ThenInclude(projs => projs.Project)
                .ThenInclude(proj => proj.ProjectType)
                .SingleAsync(tech => tech.Id == id);

            var technologyDto = await _technologyQuery.GetAsync(id, retrivalFunc);

            return new JsonResult(technologyDto);
        }

        /// <summary>
        /// Retrieve all Technology entities
        /// </summary>
        /// <returns> Technology entity collection in JSON format </returns>
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Successfully retrieved enquired entities from database")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, description: "Collection of enquired entities is empty")]
        [HttpGet]
        public async Task<IActionResult> GetTechnologies()
        {
            Func<DbSet<TechnologyEntity>, Task<List<TechnologyEntity>>> retrivalFunc =
                dbSet => dbSet.Include(tech => tech.TechnologyType)
                .Include(tech => tech.Projects)
                .ThenInclude(projs => projs.Project)
                .ThenInclude(proj => proj.ProjectType)
                .ToListAsync();

            var technologyDtos = await _technologyQuery.GetAsync(retrivalFunc);

            return new JsonResult(technologyDtos);
        }

        /// <summary>
        /// Create new Technology entity in database
        /// </summary>
        /// <param name="createTechnologyCommand"> Command containing parameters to create a new Technology entity </param>
        /// <returns> JSON containing information about processed command </returns>
        [SwaggerResponse((int)HttpStatusCode.Created, description: "Successfully created new entity in database")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, description: "Entity already exists in database")]
        [SwaggerResponse((int)HttpStatusCode.NotAcceptable, description: "Provided values are not acceptable, e.g. empty entity")]
        [HttpPost]
        public async Task<IActionResult> CreateTechnology([FromBody]CreateTechnologyCommand createTechnologyCommand)
        {
            await _commandBus.SendAsync(createTechnologyCommand);

            return new JsonResult($"Processed command '{createTechnologyCommand}'.");
        }
    }
}
